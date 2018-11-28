﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;

namespace Kendo.Mvc.UI
{
    public class DataSource : JsonObject
    {
        public DataSource()
        {
            Transport = new Transport();

            Filters = new List<IFilterDescriptor>();
            OrderBy = new List<SortDescriptor>();
            Groups = new List<GroupDescriptor>();
            Aggregates = new List<AggregateDescriptor>();

            Events = new Dictionary<string, object>();

            Schema = new DataSourceSchema();

            OfflineStorage = new Dictionary<string, object>();
        }

        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public string OfflineStorageKey { get; set; }
        public IDictionary<string, object> OfflineStorage { get; set; }
        public DataSourceSchema Schema { get; private set; }
        public IDictionary<string, object> Events { get; private set; }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (Transport.Read.Url == null & Type != DataSourceType.Custom)
            {
                // If Url is not set assume the current url (used in server binding)
                Transport.Read.Url = "";
            }

            if (Type != null)
            {
                if (Type == DataSourceType.Ajax || Type == DataSourceType.Server)
                {
                    json["type"] = new ClientHandlerDescriptor() { HandlerName = GenerateTypeFunction(true) };
                }
                else if (Type == DataSourceType.Custom)
                {
                    if (!string.IsNullOrEmpty(CustomType))
                    {
                        json["type"] = CustomType;
                    }
                }
                else
                {
                    json["type"] = new ClientHandlerDescriptor() { HandlerName = GenerateTypeFunction(false) };

                    if (Type == DataSourceType.WebApi && Schema.Model.Id != null)
                    {
                        Transport.IdField = Schema.Model.Id.Name;
                    }
                }
            }

            if (CustomTransport != null)
            {
                json["transport"] = CustomTransport;
            }
            else
            {
                var transport = Transport.ToJson();

                if (transport.Keys.Any())
                {
                    json["transport"] = transport;
                }
            }

            if (!string.IsNullOrEmpty(OfflineStorageKey))
            {
                json["offlineStorage"] = OfflineStorageKey;
            }

            if (OfflineStorage.Any())
            {
                json["offlineStorage"] = OfflineStorage;
            }

            if (PageSize > 0)
            {
                json["pageSize"] = PageSize;
                json["page"] = Page;
                json["total"] = Total;
            }

            if (ServerPaging)
            {
                json["serverPaging"] = ServerPaging;
            }

            if (ServerSorting)
            {
                json["serverSorting"] = ServerSorting;
            }

            if (ServerFiltering)
            {
                json["serverFiltering"] = ServerFiltering;
            }

            if (ServerGrouping)
            {
                json["serverGrouping"] = ServerGrouping;
            }

            if (ServerAggregates)
            {
                json["serverAggregates"] = ServerAggregates;
            }

            if (OrderBy.Any())
            {
                json["sort"] = OrderBy.ToJson();
            }

            if (Groups.Any())
            {
                json["group"] = Groups.ToJson();
            }

            if (Aggregates.Any())
            {
                json["aggregate"] = Aggregates.SelectMany(agg => agg.Aggregates.ToJson());
            }

            if (Filters.Any() || ServerFiltering)
            {
                json["filter"] = Filters.OfType<FilterDescriptorBase>().ToJson();
            }

            if (Events.Keys.Any())
            {
                json.Merge(Events);
            }

            var schema = Schema.ToJson();
            if (schema.Keys.Any())
            {
                json["schema"] = schema;
            }

            if (Batch)
            {
                json["batch"] = Batch;
            }

            if (AutoSync)
            {
                json["autoSync"] = AutoSync;
            }

            if (IsClientOperationMode && Type == DataSourceType.Custom && CustomType != "aspnetmvc-ajax")
            {
                RawData = Data;
            }

            if (IsClientOperationMode && RawData != null)
            {
                SerializeData(json, RawData);
            }
            else if (IsClientBinding && !IsClientOperationMode && Data != null)
            {
                SerializeData(json, Data);
            }
        }

        private string GenerateTypeFunction(bool isAspNetMvc)
        {
            string baseFunction = "(function(){{if(kendo.data.transports['{0}{1}']){{return '{0}{1}';}}" +
                         " else{{throw new Error('The kendo.aspnetmvc.min.js script is not included.');}}}})()";

            if (isAspNetMvc)
            {
                return string.Format(baseFunction, "aspnetmvc-", Type.ToString().ToLower());
            }
            else
            {
                return string.Format(baseFunction, "", Type.ToString().ToLower());
            }
        }

        private void SerializeData(IDictionary<string, object> json, IEnumerable data)
        {
            if (string.IsNullOrEmpty(Schema.Data))
            {
                json["data"] = SerializeDataSource(data);
            }
            else
            {
                json["data"] = new Dictionary<string, object>()
                    {
                        { Schema.Data,  SerializeDataSource(data) },
                        { Schema.Total, Total }
                    };
            }
        }

        private object SerializeDataSource(IEnumerable data)
        {
            var dataTableEnumerable = RawData as DataTableWrapper;

            if (dataTableEnumerable != null && dataTableEnumerable.Table != null)
            {
                return data.SerializeToDictionary(dataTableEnumerable.Table);
            }
            else if (data is IEnumerable<AggregateFunctionsGroup>)
            {
                if (!ServerGrouping)
                {
                    return data.Cast<IGroup>().Leaves();               
                }                
            }
            return data;
        }       

        public bool IsClientOperationMode
        {
            get
            {
                return IsClientBinding && !(ServerPaging && ServerSorting && ServerGrouping && ServerFiltering && ServerAggregates);
            }
        }

        public bool IsClientBinding
        {
            get
            {
                return Type == DataSourceType.Ajax || Type == DataSourceType.WebApi || Type == DataSourceType.Custom;
            }
        }

        public void ModelType(Type modelType)
        {
            Schema.Model = new ModelDescriptor(modelType);
        }

        public bool Batch
        {
            get;
            set;
        }

        public DataSourceType? Type
        {
            get;
            set;
        }

        public string CustomType
        {
            get;
            set;
        }

        public IList<IFilterDescriptor> Filters
        {
            get;
            private set;
        }

        public IList<SortDescriptor> OrderBy
        {
            get;
            private set;
        }

        public IList<GroupDescriptor> Groups
        {
            get;
            private set;
        }

        public IList<AggregateDescriptor> Aggregates
        {
            get;
            private set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public bool ServerPaging
        {
            get;
            set;
        }

        public bool ServerSorting
        {
            get;
            set;
        }

        public bool ServerFiltering
        {
            get;
            set;
        }

        public bool ServerGrouping
        {
            get;
            set;
        }

        public bool ServerAggregates
        {
            get;
            set;
        }

        public Transport Transport
        {
            get;
            protected set;
        }

        public IDictionary<string, object> CustomTransport
        {
            get;
            set;
        }

        public IEnumerable Data
        {
            get;
            set;
        }

        public bool AutoSync
        {
            get;
            set;
        }

        public IEnumerable RawData
        {
            get;
            set;
        }

        public IEnumerable<AggregateResult> AggregateResults
        {
            get;
            set;
        }

        public void Process(DataSourceRequest request, bool processData)
        {
            RawData = Data;

            if (request.Sorts == null)
            {
                request.Sorts = OrderBy;
            }
            else if (request.Sorts.Any())
            {
                OrderBy.Clear();
                OrderBy.AddRange(request.Sorts);
            }
            else
            {
                OrderBy.Clear();
            }

            if (request.PageSize == 0)
            {
                request.PageSize = PageSize;
            }

            PageSize = request.PageSize;

            if (request.Groups == null)
            {
                request.Groups = Groups;
            }
            else if (request.Groups.Any())
            {
                Groups.Clear();
                Groups.AddRange(request.Groups);
            }
            else
            {
                Groups.Clear();
            }

            if (request.Filters == null)
            {
                request.Filters = Filters;
            }
            else if (request.Filters.Any())
            {
                Filters.Clear();
                Filters.AddRange(request.Filters);
            }
            else
            {
                Filters.Clear();
            }

            if (!request.Aggregates.Any())
            {
                request.Aggregates = Aggregates;
            }
            else if (request.Aggregates.Any())
            {
                MergeAggregateTypes(request);

                Aggregates.Clear();
                Aggregates.AddRange(request.Aggregates);
            }
            else
            {
                Aggregates.Clear();
            }

            if (Groups.Any() && Aggregates.Any() && Data == null)
            {
                Groups.Each(g => g.AggregateFunctions.AddRange(Aggregates.SelectMany(a => a.Aggregates)));
            }

            if (Data != null)
            {
                if (processData)
                {
                    var result = Data.AsQueryable().ToDataSourceResult(request);

                    Data = result.Data;

                    Total = result.Total;

                    AggregateResults = result.AggregateResults;
                }
                else
                {
                    var wrapper = Data as IGridCustomGroupingWrapper;
                    if (wrapper != null)
                    {
                        RawData = Data = wrapper.GroupedEnumerable.AsGenericEnumerable();
                    }
                }
            }

            Page = request.Page;

            if (Total == 0 || PageSize == 0)
            {
                TotalPages = 1;
            }
            else
            {
                TotalPages = (Total + PageSize - 1) / PageSize;
            }
        }

        private void MergeAggregateTypes(DataSourceRequest request)
        {
            if (Aggregates.Any())
            {
                foreach (var requestAggregate in request.Aggregates)
                {
                    var match = Aggregates.SingleOrDefault(agg => agg.Member.Equals(requestAggregate.Member, StringComparison.InvariantCultureIgnoreCase));
                    if (match != null)
                    {
                        requestAggregate.Aggregates.Each(function =>
                        {
                            var innerFunction = match.Aggregates.SingleOrDefault(matchFunction => matchFunction.AggregateMethodName == function.AggregateMethodName);
                            if (innerFunction != null && innerFunction.MemberType != null)
                            {
                                function.MemberType = innerFunction.MemberType;
                            }
                        });
                    }
                }
            }
        }

    }
}