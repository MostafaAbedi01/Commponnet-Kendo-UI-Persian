namespace Kendo.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Extensions;
    using Kendo.Mvc.Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="DataSource"/> Model definition.
    /// </summary>
    /// <typeparam name="TModel">Type of the model</typeparam>
    public class DataSourceModelDescriptorFactory<TModel> : DataSourceModelDescriptorFactoryBase<TModel>, IHideObjectMembers
        where TModel : class
    {
        public DataSourceModelDescriptorFactory(ModelDescriptor model)
            : base(model)
        {
        }

        /// <summary>
        /// Specify the member used to identify an unique Model instance.
        /// </summary>
        /// <param name="fieldName">The member name.</param>
        public new void Id(string fieldName)
        {
            base.Id(fieldName);
        }

        /// <summary>
        /// Specify the member used to identify an unique Model instance.
        /// </summary>
        /// <typeparam name="TValue">Type of the field</typeparam>
        /// <param name="expression">Member access expression which describes the member</param>
        public virtual void Id<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var dataKey = new GridDataKey<TModel, TValue>(expression);
            dataKey.RouteKey = dataKey.Name;

            model.Id = dataKey;
        }

        /// <summary>
        /// Describes a Model field
        /// </summary>
        /// <typeparam name="TValue">Field type</typeparam>
        /// <param name="expression">Member access expression which describes the field</param>        
        public virtual DataSourceModelFieldDescriptorBuilder<TValue> Field<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return AddFieldDescriptor<TValue>(expression.MemberWithoutInstance(), typeof(TValue));
        }

        /// <summary>
        /// Describes a Model field
        /// </summary>
        /// <param name="memberName">Field name</param>
        /// <param name="memberType">Field type</param>        
        public virtual DataSourceModelFieldDescriptorBuilder<object> Field(string memberName, Type memberType)
        {
            return AddFieldDescriptor<object>(memberName, memberType);
        }

        /// <summary>
        /// Describes a Model field
        /// </summary>
        /// <typeparam name="TValue">Field type</typeparam>
        /// <param name="memberName">Member name</param>        
        public virtual DataSourceModelFieldDescriptorBuilder<TValue> Field<TValue>(string memberName)
        {
            return AddFieldDescriptor<TValue>(memberName, typeof(TValue));
        }

        private DataSourceModelFieldDescriptorBuilder<TValue> AddFieldDescriptor<TValue>(string memberName, Type memberType)
        {
            var descriptor = model.AddDescriptor(memberName);
            
            descriptor.MemberType = memberType;            

            return new DataSourceModelFieldDescriptorBuilder<TValue>(descriptor);            
        }
    }
}
