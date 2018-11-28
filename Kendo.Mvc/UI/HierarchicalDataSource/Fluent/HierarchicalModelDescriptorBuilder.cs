namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="HierarchicalModelDescriptor"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of the model</typeparam>
    public class HierarchicalModelDescriptorBuilder<TModel> : DataSourceModelDescriptorFactoryBase<TModel>
         where TModel : class
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly ViewContext viewContext;

        public HierarchicalModelDescriptorBuilder(ModelDescriptor model, ViewContext viewContext, IUrlGenerator urlGenerator)
            : base(model)
        {
            this.urlGenerator = urlGenerator;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Specify the member used to identify an unique Model instance.
        /// </summary>
        /// <param name="fieldName">The member name.</param>
        public new HierarchicalModelDescriptorBuilder<TModel> Id(string fieldName)
        {
            base.Id(fieldName);

            return this;
        }

        /// <summary>
        /// Specify the model children member name.
        /// </summary>
        /// <param name="fieldName">The member name.</param>
        public HierarchicalModelDescriptorBuilder<TModel> Children(string fieldName)
        {
            model.ChildrenMember = fieldName;

            return this;
        }

        /// <summary>
        /// Specify the children DataSource configuration.
        /// </summary>
        /// <param name="fieldName">The configurator action.</param>
        public HierarchicalModelDescriptorBuilder<TModel> Children(Action<HierarchicalDataSourceBuilder<object>> configurator)
        {
            model.ChildrenDataSource = new DataSource();
            model.ChildrenDataSource.ModelType(typeof(object));
            configurator(new HierarchicalDataSourceBuilder<object>(model.ChildrenDataSource, viewContext, urlGenerator));

            return this;
        }

        /// <summary>
        /// Specify the member name used to determine if the model has children.
        /// </summary>
        /// <param name="fieldName">The member name.</param>
        public HierarchicalModelDescriptorBuilder<TModel> HasChildren(string fieldName)
        {
            model.HasChildrenMember = fieldName;

            return this;
        }

        /// <summary>
        /// Describes a Model field
        /// </summary>
        /// <param name="memberName">Field name</param>
        /// <param name="memberType">Field type</param>        
        public virtual HierarchicalModelDescriptorBuilder<TModel> Field(string memberName, Type memberType)
        {
            var descriptor = model.AddDescriptor(memberName);

            descriptor.MemberType = memberType;

            return this;
        }
    }
}