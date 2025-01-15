using System;
using System.Globalization;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000FA RID: 250
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Rule
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000293FF File Offset: 0x000275FF
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x00029407 File Offset: 0x00027607
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value ?? string.Empty;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00029419 File Offset: 0x00027619
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x00029421 File Offset: 0x00027621
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value ?? string.Empty;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00029433 File Offset: 0x00027633
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002943B File Offset: 0x0002763B
		public bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00029444 File Offset: 0x00027644
		public FilterCollection Filters
		{
			get
			{
				return this.m_filters;
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002944C File Offset: 0x0002764C
		public virtual bool AppliesTo(DsvItem dsvItem)
		{
			return Filter.IsMatch(dsvItem, this.m_filters);
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000C85 RID: 3205 RVA: 0x0002945C File Offset: 0x0002765C
		// (remove) Token: 0x06000C86 RID: 3206 RVA: 0x00029494 File Offset: 0x00027694
		public event EventHandler ContextChanged;

		// Token: 0x06000C87 RID: 3207 RVA: 0x000294C9 File Offset: 0x000276C9
		protected virtual void OnContextChanged(EventArgs e)
		{
			if (this.ContextChanged != null)
			{
				this.ContextChanged(this, e);
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000294E0 File Offset: 0x000276E0
		internal void SetContext(SemanticModel model, ExistingBindingContext bindingContext)
		{
			this.m_model = model;
			this.m_bindingContext = bindingContext;
			foreach (Filter filter in this.m_filters)
			{
				filter.SetContext(model, bindingContext);
			}
			this.OnContextChanged(EventArgs.Empty);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002954C File Offset: 0x0002774C
		internal SemanticModel Model
		{
			get
			{
				return this.m_model;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00029554 File Offset: 0x00027754
		internal CultureInfo StringCulture
		{
			get
			{
				return this.m_model.Culture ?? CultureInfo.CurrentCulture;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002956A File Offset: 0x0002776A
		internal ExistingBindingContext BindingContext
		{
			get
			{
				return this.m_bindingContext;
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00029572 File Offset: 0x00027772
		internal void Load(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			xr.LoadObject("rule", new Rule.RuleLoader(this, objectFactory));
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00029588 File Offset: 0x00027788
		internal virtual bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "name")
				{
					this.m_name = xr.ReadValueAsString();
					return true;
				}
				if (localName == "description")
				{
					this.m_description = xr.ReadValueAsString();
					return true;
				}
				if (localName == "enabled")
				{
					this.m_enabled = xr.ReadValueAsBoolean();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000295F8 File Offset: 0x000277F8
		internal virtual bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "filter")
			{
				this.m_filters.Add(objectFactory.CreateFilter(xr));
				return true;
			}
			return false;
		}

		// Token: 0x0400052A RID: 1322
		internal const string RuleElem = "rule";

		// Token: 0x0400052B RID: 1323
		private const string NameAttr = "name";

		// Token: 0x0400052C RID: 1324
		private const string DescriptionAttr = "description";

		// Token: 0x0400052D RID: 1325
		private const string EnabledAttr = "enabled";

		// Token: 0x0400052E RID: 1326
		private string m_name = string.Empty;

		// Token: 0x0400052F RID: 1327
		private string m_description = string.Empty;

		// Token: 0x04000530 RID: 1328
		private bool m_enabled = true;

		// Token: 0x04000531 RID: 1329
		private readonly FilterCollection m_filters = new FilterCollection();

		// Token: 0x04000532 RID: 1330
		private SemanticModel m_model;

		// Token: 0x04000533 RID: 1331
		private ExistingBindingContext m_bindingContext;

		// Token: 0x020001CE RID: 462
		private class RuleLoader : ModelingXmlLoaderBase<Rule>
		{
			// Token: 0x0600116F RID: 4463 RVA: 0x000367FD File Offset: 0x000349FD
			internal RuleLoader(Rule item, IXmlObjectFactory objectFactory)
				: base(item)
			{
				this.m_objectFactory = objectFactory;
			}

			// Token: 0x06001170 RID: 4464 RVA: 0x0003680D File Offset: 0x00034A0D
			public override bool LoadXmlAttribute(ModelingXmlReader xr)
			{
				return base.Item.LoadXmlAttribute(xr, this.m_objectFactory);
			}

			// Token: 0x06001171 RID: 4465 RVA: 0x00036821 File Offset: 0x00034A21
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				return base.Item.LoadXmlElement(xr, this.m_objectFactory);
			}

			// Token: 0x040007E6 RID: 2022
			private readonly IXmlObjectFactory m_objectFactory;
		}
	}
}
