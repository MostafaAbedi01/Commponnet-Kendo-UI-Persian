namespace Kendo.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Extensions;
    using Infrastructure;
    using Kendo.Mvc.Resources;

    public class TreeView : WidgetBase, INavigationItemComponent<TreeViewItem>
    {
        internal bool isPathHighlighted;

        public TreeView(ViewContext viewContext, IJavaScriptInitializer initializer, IUrlGenerator urlGenerator, INavigationItemAuthorization authorization)
            : base(viewContext, initializer)
        {
            UrlGenerator = urlGenerator;
            Authorization = authorization;
            
            Animation = new ExpandableAnimation();

            this.DragAndDrop = false;

            Items = new LinkedObjectCollection<TreeViewItem>(null);

            SelectedIndex = -1;
            SecurityTrimming = new SecurityTrimming();

            LoadOnDemand = true;

            Checkboxes = new TreeViewCheckboxesSettings();

            DataSource = new DataSource();
            DataSource.ModelType(typeof(object));

            AutoBind = true;

//>> Initialization
        
            Messages = new TreeViewMessagesSettings();
                
        //<< Initialization
        }

//>> Fields
        
        public string DataImageUrlField { get; set; }
        
        public string DataSpriteCssClassField { get; set; }
        
        public string DataTextField { get; set; }
        
        public string DataUrlField { get; set; }
        
        public TreeViewMessagesSettings Messages
        {
            get;
            set;
        }
        
        //<< Fields

        public DataSource DataSource
        {
            get;
            private set;
        }

        public ExpandableAnimation Animation
        {
            get;
            private set;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public INavigationItemAuthorization Authorization
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the items of the treeview.
        /// </summary>
        public IList<TreeViewItem> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the item action.
        /// </summary>
        public Action<TreeViewItem> ItemAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the effects.
        /// </summary>
        public Effects Effects
        {
            get;
            set;
        }

        public TreeViewCheckboxesSettings Checkboxes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether all the item is expanded.
        /// </summary>
        /// <value><c>true</c> if expand all is enabled; otherwise, <c>false</c>. The default value is <c>false</c></value>
        public bool ExpandAll
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get;
            set;
        }

        public bool HighlightPath
        {
            get;
            set;
        }

        public SecurityTrimming SecurityTrimming
        {
            get;
            set;
        }

        public bool DragAndDrop
        {
            get;
            set;
        }

        public bool AutoBind
        {
            get;
            set;
        }

        public string Template
        {
            get;
            set;
        }

        public string TemplateId
        {
            get;
            set;
        }

        public bool LoadOnDemand
        {
            get;
            set;
        }

        internal bool UsesTemplates()
        {
            return !string.IsNullOrEmpty(TemplateId) || !string.IsNullOrEmpty(Template) || Checkboxes.Template as string != TreeViewCheckboxesSettings.DefaultTemplate;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            var json = new Dictionary<string, object>(Events);

            // use client-side rendering if templates are set
            if (Items.Any() && this.UsesTemplates())
            {
                this.DataSource.Data = SerializeItems(Items);
                this.LoadOnDemand = false;
            }

            if (!string.IsNullOrEmpty(DataSource.Transport.Read.Url) || DataSource.Type == DataSourceType.Custom)
            {
                json["dataSource"] = DataSource.ToJson();
            }
            else if (DataSource.Data != null)
            {
                json["dataSource"] = DataSource.Data;
            }

            if (DragAndDrop)
            {
                json["dragAndDrop"] = true;
            }

            if (!AutoBind)
            {
                json["autoBind"] = false;
            }

            if (!LoadOnDemand)
            {
                json["loadOnDemand"] = false;
            }

            var idPrefix = "#";
            if (IsInClientTemplate)
            {
                idPrefix = "\\" + idPrefix;
            }

            if (!string.IsNullOrEmpty(TemplateId))
            {
                json["template"] = new ClientHandlerDescriptor { HandlerName = string.Format("jQuery('{0}{1}').html()", idPrefix, TemplateId) };
            }
            else if (!string.IsNullOrEmpty(Template))
            {
                json["template"] = Template;
            }

            var checkboxes = Checkboxes.ToJson();

            if (checkboxes.Keys.Any())
            {
                json["checkboxes"] = checkboxes["checkboxes"];
            }

            var animation = Animation.ToJson();

            if (animation.Keys.Any())
            {
                json["animation"] = animation["animation"];
            }

//>> Serialization
        
            if (DataImageUrlField.HasValue())
            {
                json["dataImageUrlField"] = DataImageUrlField;
            }
            
            if (DataSpriteCssClassField.HasValue())
            {
                json["dataSpriteCssClassField"] = DataSpriteCssClassField;
            }
            
            if (DataTextField.HasValue())
            {
                json["dataTextField"] = DataTextField;
            }
            
            if (DataUrlField.HasValue())
            {
                json["dataUrlField"] = DataUrlField;
            }
            
            var messages = Messages.ToJson();
            if (messages.Any())
            {
                json["messages"] = messages;
            }
                
        //<< Serialization

            writer.Write(Initializer.Initialize(Selector, "TreeView", json));

            base.WriteInitializationScript(writer);
        }
  
        private IEnumerable SerializeItems(IList<TreeViewItem> items)
        {
            var urlHelper = new UrlHelper(this.ViewContext.RequestContext);
            return from item in items select item.Serialize(urlHelper);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {

            var builder = new TreeViewHtmlBuilder(this);

            IHtmlNode treeViewTag = builder.TreeViewTag();

            if (Items.Any() && !this.UsesTemplates())
            {
                if (SelectedIndex != -1 && Items.Count < SelectedIndex)
                {
                    throw new ArgumentOutOfRangeException(Exceptions.IndexOutOfRange);
                }

                //this loop is required because of SelectedIndex feature.
                if (HighlightPath)
                {
                    Items.Each(HighlightSelectedItem);
                }

                Items.Each((item, index) =>
                {
                    if (!this.isPathHighlighted)
                    {
                        if (index == this.SelectedIndex)
                        {
                            item.Selected = true;

                            if (item.Items.Any() || item.Template.HasValue())
                            {
                                item.Expanded = true;
                            }
                        }
                    }

                    if (item.HasChildren)
                    {
                        item.Expanded = false;
                    }

                    if (ExpandAll)
                    {
                        ExpandAllChildrens(item);
                    }

                    if (string.IsNullOrEmpty(item.Id))
                    {
                        item.Id = item.Text;
                    }

                    WriteItem(item, treeViewTag.Children[0], builder);
                });
            }

            treeViewTag.WriteTo(writer);

            base.WriteHtml(writer);
        }

        private void ExpandAllChildrens(TreeViewItem treeViewItem)
        {
            treeViewItem.Expanded = true;

            foreach (var item in treeViewItem.Items)
            {
                ExpandAllChildrens(item);
            }
        }

        private void WriteItem(TreeViewItem item, IHtmlNode parentTag, TreeViewHtmlBuilder builder)
        {
            if (ItemAction != null)
            {
                ItemAction(item);
            }

            if (item.Visible)
            {
                var accessible = true;
                if (this.SecurityTrimming.Enabled)
                {
                    accessible = item.IsAccessible(Authorization, ViewContext);
                }


                if (accessible)
                {
                    var hasAccessibleChildren = item.Items.Any(x => x.Visible);
                    if (hasAccessibleChildren && this.SecurityTrimming.Enabled)
                    {
                        hasAccessibleChildren = item.Items.IsAccessible(Authorization, ViewContext);

                        if (this.SecurityTrimming.HideParent && !hasAccessibleChildren)
                        {
                            return;
                        }
                    }

                    IHtmlNode itemTag = builder.ItemTag(item, hasAccessibleChildren).AppendTo(parentTag);

                    builder.ItemInnerContent(item).AppendTo(itemTag.Children[0]);

                    if (item.Template.HasValue())
                    {
                        builder.ItemContentTag(item).AppendTo(itemTag);
                    }
                    else if (hasAccessibleChildren)
                    {
                        IHtmlNode ul = builder.ChildrenTag(item).AppendTo(itemTag);

                        item.Items.Each(child => WriteItem(child, ul, builder));
                    }
                }
            }
        }

        private void HighlightSelectedItem(TreeViewItem item)
        {
            if (item.IsCurrent(ViewContext, UrlGenerator))
            {
                item.Selected = true;
                isPathHighlighted = true;

                TreeViewItem tmpItem = item.Parent;

                while (tmpItem != null)
                {
                    tmpItem.Expanded = true;
                    tmpItem = tmpItem.Parent;
                }
            }

            item.Items.Each(HighlightSelectedItem);
        }
    }
}