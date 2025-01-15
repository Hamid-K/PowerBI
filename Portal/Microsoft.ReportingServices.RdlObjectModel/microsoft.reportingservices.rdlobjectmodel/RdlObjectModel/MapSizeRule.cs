using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000184 RID: 388
	public class MapSizeRule : MapAppearanceRule
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x0002110B File Offset: 0x0001F30B
		public MapSizeRule()
		{
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00021113 File Offset: 0x0001F313
		internal MapSizeRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002111C File Offset: 0x0001F31C
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0002112B File Offset: 0x0001F32B
		public ReportExpression<ReportSize> StartSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00021140 File Offset: 0x0001F340
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0002114F File Offset: 0x0001F34F
		public ReportExpression<ReportSize> EndSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00021164 File Offset: 0x0001F364
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003B2 RID: 946
		internal new class Definition : DefinitionStore<MapSizeRule, MapSizeRule.Definition.Properties>
		{
			// Token: 0x06001856 RID: 6230 RVA: 0x0003B631 File Offset: 0x00039831
			private Definition()
			{
			}

			// Token: 0x020004CA RID: 1226
			internal enum Properties
			{
				// Token: 0x04000EBC RID: 3772
				DataValue,
				// Token: 0x04000EBD RID: 3773
				DistributionType,
				// Token: 0x04000EBE RID: 3774
				BucketCount,
				// Token: 0x04000EBF RID: 3775
				StartValue,
				// Token: 0x04000EC0 RID: 3776
				EndValue,
				// Token: 0x04000EC1 RID: 3777
				MapBuckets,
				// Token: 0x04000EC2 RID: 3778
				LegendName,
				// Token: 0x04000EC3 RID: 3779
				LegendText,
				// Token: 0x04000EC4 RID: 3780
				DataElementName,
				// Token: 0x04000EC5 RID: 3781
				DataElementOutput,
				// Token: 0x04000EC6 RID: 3782
				StartSize,
				// Token: 0x04000EC7 RID: 3783
				EndSize,
				// Token: 0x04000EC8 RID: 3784
				PropertyCount
			}
		}
	}
}
