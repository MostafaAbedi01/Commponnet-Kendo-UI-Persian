﻿namespace Kendo.Mvc.UI
{
    using Kendo.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Linq;

    public class PivotDataSourceSchema : JsonObject
    {
        public string Axes { get; set; }

        public ClientHandlerDescriptor FunctionAxes { get; set; }

        public string Cubes { get; set; }

        public ClientHandlerDescriptor FunctionCubes { get; set; }

        public string Catalogs { get; set; }

        public ClientHandlerDescriptor FunctionCatalogs { get; set; }

        public string Measures { get; set; }

        public ClientHandlerDescriptor FunctionMeasures { get; set; }

        public string Dimensions { get; set; }

        public ClientHandlerDescriptor FunctionDimensions { get; set; }

        public string Hierarchies { get; set; }

        public ClientHandlerDescriptor FunctionHierarchies { get; set; }

        public string Levels { get; set; }

        public ClientHandlerDescriptor FunctionLevels { get; set; }

        public string Type { get; set; }

        public PivotDataSourceSchemaCube Cube { get; set; }

        public string Data { get; set; }

        public string Total { get; set; }

        public string Errors { get; set; }

        public ModelDescriptor Model
        {
            get;
            set;
        }

        public PivotDataSourceSchema()
        {
            FunctionAxes = new ClientHandlerDescriptor();
            FunctionCubes = new ClientHandlerDescriptor();
            FunctionCatalogs = new ClientHandlerDescriptor();
            FunctionMeasures = new ClientHandlerDescriptor();
            FunctionDimensions = new ClientHandlerDescriptor();
            FunctionHierarchies = new ClientHandlerDescriptor();
            FunctionLevels = new ClientHandlerDescriptor();
            Cube = new PivotDataSourceSchemaCube();
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            if (FunctionAxes.HasValue())
            {
                json.Add("axes", FunctionAxes);
            }
            else
            {
                FluentDictionary.For(json).Add("axes", Axes, string.Empty);
            }

            if (FunctionCubes.HasValue())
            {
                json.Add("cubes", FunctionCubes);
            }
            else
            {
                FluentDictionary.For(json).Add("cubes", Cubes, string.Empty);
            }

            if (FunctionCatalogs.HasValue())
            {
                json.Add("catalogs", FunctionCatalogs);
            }
            else
            {
                FluentDictionary.For(json).Add("catalogs", Catalogs, string.Empty);
            }

            if (FunctionMeasures.HasValue())
            {
                json.Add("measures", FunctionMeasures);
            }
            else
            {
                FluentDictionary.For(json).Add("measures", Measures, string.Empty);
            }

            if (FunctionDimensions.HasValue())
            {
                json.Add("dimensions", FunctionDimensions);
            }
            else
            {
                FluentDictionary.For(json).Add("dimensions", Dimensions, string.Empty);
            }

            if (FunctionHierarchies.HasValue())
            {
                json.Add("hierarchies", FunctionHierarchies);
            }
            else
            {
                FluentDictionary.For(json).Add("hierarchies", Hierarchies, string.Empty);
            }

            if (FunctionLevels.HasValue())
            {
                json.Add("levels", FunctionLevels);
            }
            else
            {
                FluentDictionary.For(json).Add("levels", Levels, string.Empty);
            }

            if (!string.IsNullOrEmpty(Data))
            {
                json.Add("data", Data);
            }

            if (!string.IsNullOrEmpty(Total))
            {
                json.Add("total", Total);
            }

            if (!string.IsNullOrEmpty(Errors))
            {
                json.Add("errors", Errors);
            }

            if (!string.IsNullOrEmpty(Type))
            {
                json.Add("type", Type);
            }

            var cube = Cube.ToJson();

            if (cube.Keys.Any())
            {
                json["cube"] = cube;
            }

            if (Model != null)
            {
                var model = Model.ToJson();
                if (model.Count > 0)
                {
                    json.Add("model", model);
                }
            }
        }
    }
}
