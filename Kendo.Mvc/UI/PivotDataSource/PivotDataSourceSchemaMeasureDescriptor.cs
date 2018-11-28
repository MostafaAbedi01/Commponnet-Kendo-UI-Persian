﻿namespace Kendo.Mvc.UI
{
    public class PivotDataSourceSchemaMeasureDescriptor
    {
        public PivotDataSourceSchemaMeasureDescriptor()
        {
            Aggregate = new ClientHandlerDescriptor();
        }

        public string Caption { get; set; }
        public string Measure { get; set; }
        public string Field { get; set; }
        public string Format { get; set; }
        public ClientHandlerDescriptor Aggregate { get; set; }
    }
}
