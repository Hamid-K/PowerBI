using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000183 RID: 387
	public class MapCustomColorRule : MapColorRule
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x000210C3 File Offset: 0x0001F2C3
		public MapCustomColorRule()
		{
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000210CB File Offset: 0x0001F2CB
		internal MapCustomColorRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000210D4 File Offset: 0x0001F2D4
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x000210E8 File Offset: 0x0001F2E8
		[XmlElement(typeof(RdlCollection<ReportExpression<ReportColor>>))]
		[XmlArrayItem("MapCustomColor", typeof(ReportExpression<ReportColor>))]
		public IList<ReportExpression<ReportColor>> MapCustomColors
		{
			get
			{
				return (IList<ReportExpression<ReportColor>>)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public override void Initialize()
		{
			base.Initialize();
			this.MapCustomColors = new RdlCollection<ReportExpression<ReportColor>>();
		}

		// Token: 0x020003B1 RID: 945
		internal new class Definition : DefinitionStore<MapCustomColorRule, MapCustomColorRule.Definition.Properties>
		{
			// Token: 0x06001855 RID: 6229 RVA: 0x0003B629 File Offset: 0x00039829
			private Definition()
			{
			}

			// Token: 0x020004C9 RID: 1225
			internal enum Properties
			{
				// Token: 0x04000EAE RID: 3758
				DataValue,
				// Token: 0x04000EAF RID: 3759
				DistributionType,
				// Token: 0x04000EB0 RID: 3760
				BucketCount,
				// Token: 0x04000EB1 RID: 3761
				StartValue,
				// Token: 0x04000EB2 RID: 3762
				EndValue,
				// Token: 0x04000EB3 RID: 3763
				MapBuckets,
				// Token: 0x04000EB4 RID: 3764
				LegendName,
				// Token: 0x04000EB5 RID: 3765
				LegendText,
				// Token: 0x04000EB6 RID: 3766
				DataElementName,
				// Token: 0x04000EB7 RID: 3767
				DataElementOutput,
				// Token: 0x04000EB8 RID: 3768
				ShowInColorScale,
				// Token: 0x04000EB9 RID: 3769
				MapCustomColors,
				// Token: 0x04000EBA RID: 3770
				PropertyCount
			}
		}
	}
}
