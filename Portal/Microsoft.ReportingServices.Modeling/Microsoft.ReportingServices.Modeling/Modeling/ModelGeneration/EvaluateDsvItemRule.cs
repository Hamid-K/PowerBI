using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D2 RID: 210
	public sealed class EvaluateDsvItemRule : Rule
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000267EA File Offset: 0x000249EA
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x000267F2 File Offset: 0x000249F2
		public bool Exclude
		{
			get
			{
				return this.m_exclude;
			}
			set
			{
				this.m_exclude = value;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000267FC File Offset: 0x000249FC
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "semantics")
				{
					EvaluateDsvItemRule.Semantics semantics = xr.ReadValueAsEnum<EvaluateDsvItemRule.Semantics>();
					if (semantics != EvaluateDsvItemRule.Semantics.Include)
					{
						if (semantics == EvaluateDsvItemRule.Semantics.Exclude)
						{
							this.m_exclude = true;
						}
					}
					else
					{
						this.m_exclude = false;
					}
					return true;
				}
				if (localName == "dsvItemType")
				{
					PropertyFilter propertyFilter = new PropertyFilter();
					propertyFilter.Property = "DsvItemType";
					propertyFilter.ExactValue = xr.ReadValueAsString();
					base.Filters.Add(propertyFilter);
					return true;
				}
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x040004BC RID: 1212
		private const string SemanticsAttr = "semantics";

		// Token: 0x040004BD RID: 1213
		private const string DsvItemTypeAttr = "dsvItemType";

		// Token: 0x040004BE RID: 1214
		private bool m_exclude;

		// Token: 0x020001C2 RID: 450
		private enum Semantics
		{
			// Token: 0x040007D1 RID: 2001
			Include,
			// Token: 0x040007D2 RID: 2002
			Exclude
		}
	}
}
