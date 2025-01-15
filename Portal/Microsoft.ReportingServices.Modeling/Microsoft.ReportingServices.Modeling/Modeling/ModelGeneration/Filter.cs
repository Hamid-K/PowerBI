using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E0 RID: 224
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Filter
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00026D20 File Offset: 0x00024F20
		public virtual bool IsMatch(DsvItem dsvItem)
		{
			if (dsvItem is DsvTable && this is ITableFilter)
			{
				return ((ITableFilter)this).IsMatch((DsvTable)dsvItem);
			}
			if (dsvItem is DsvColumn && this is IColumnFilter)
			{
				return ((IColumnFilter)this).IsMatch((DsvColumn)dsvItem);
			}
			return dsvItem is DsvRelation && this is IRelationFilter && ((IRelationFilter)this).IsMatch((DsvRelation)dsvItem);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00026D94 File Offset: 0x00024F94
		protected virtual bool ShouldCombineWith(Filter other)
		{
			return other.GetType() == base.GetType();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00026DA8 File Offset: 0x00024FA8
		public static bool IsMatch(DsvItem dsvItem, FilterCollection filters)
		{
			if (dsvItem == null)
			{
				throw new ArgumentNullException("dsvItem");
			}
			if (filters == null)
			{
				throw new ArgumentNullException("filters");
			}
			bool[] array = new bool[filters.Count];
			for (int i = 0; i < filters.Count; i++)
			{
				if (!array[i])
				{
					bool flag = filters[i].IsMatch(dsvItem);
					array[i] = true;
					for (int j = i + 1; j < filters.Count; j++)
					{
						if (filters[i].ShouldCombineWith(filters[j]))
						{
							flag |= filters[j].IsMatch(dsvItem);
							array[j] = true;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00026E47 File Offset: 0x00025047
		internal SemanticModel Model
		{
			get
			{
				return this.m_model;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00026E4F File Offset: 0x0002504F
		internal ExistingBindingContext BindingContext
		{
			get
			{
				return this.m_bindingContext;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00026E57 File Offset: 0x00025057
		internal void Load(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			xr.LoadObject("filter", new Filter.FilterLoader(this, objectFactory));
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00026E6B File Offset: 0x0002506B
		internal virtual bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			return false;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00026E6E File Offset: 0x0002506E
		internal virtual bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			return false;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00026E71 File Offset: 0x00025071
		internal void SetContext(SemanticModel model, ExistingBindingContext bindingContext)
		{
			this.m_model = model;
			this.m_bindingContext = bindingContext;
		}

		// Token: 0x040004E1 RID: 1249
		internal const string FilterElem = "filter";

		// Token: 0x040004E2 RID: 1250
		private SemanticModel m_model;

		// Token: 0x040004E3 RID: 1251
		private ExistingBindingContext m_bindingContext;

		// Token: 0x020001C5 RID: 453
		private class FilterLoader : ModelingXmlLoaderBase<Filter>
		{
			// Token: 0x06001159 RID: 4441 RVA: 0x0003652B File Offset: 0x0003472B
			internal FilterLoader(Filter item, IXmlObjectFactory objectFactory)
				: base(item)
			{
				this.m_objectFactory = objectFactory;
			}

			// Token: 0x0600115A RID: 4442 RVA: 0x0003653B File Offset: 0x0003473B
			public override bool LoadXmlAttribute(ModelingXmlReader xr)
			{
				return base.Item.LoadXmlAttribute(xr, this.m_objectFactory);
			}

			// Token: 0x0600115B RID: 4443 RVA: 0x0003654F File Offset: 0x0003474F
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				return base.Item.LoadXmlElement(xr, this.m_objectFactory);
			}

			// Token: 0x040007D8 RID: 2008
			private readonly IXmlObjectFactory m_objectFactory;
		}
	}
}
