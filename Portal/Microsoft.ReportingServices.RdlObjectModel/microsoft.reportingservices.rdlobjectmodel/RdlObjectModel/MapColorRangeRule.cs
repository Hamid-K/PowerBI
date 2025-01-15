using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000181 RID: 385
	public class MapColorRangeRule : MapColorRule
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x00020FA9 File Offset: 0x0001F1A9
		public MapColorRangeRule()
		{
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00020FB1 File Offset: 0x0001F1B1
		internal MapColorRangeRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00020FBA File Offset: 0x0001F1BA
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00020FC9 File Offset: 0x0001F1C9
		[ReportExpressionDefaultValue(typeof(ReportColor), "Green")]
		public ReportExpression<ReportColor> StartColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00020FDE File Offset: 0x0001F1DE
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00020FED File Offset: 0x0001F1ED
		[ReportExpressionDefaultValue(typeof(ReportColor), "Yellow")]
		public ReportExpression<ReportColor> MiddleColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00021002 File Offset: 0x0001F202
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00021011 File Offset: 0x0001F211
		[ReportExpressionDefaultValue(typeof(ReportColor), "Red")]
		public ReportExpression<ReportColor> EndColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00021028 File Offset: 0x0001F228
		public override void Initialize()
		{
			base.Initialize();
			this.StartColor = new ReportExpression<ReportColor>("Green", CultureInfo.InvariantCulture);
			this.MiddleColor = new ReportExpression<ReportColor>("Yellow", CultureInfo.InvariantCulture);
			this.EndColor = new ReportExpression<ReportColor>("Red", CultureInfo.InvariantCulture);
		}

		// Token: 0x020003AF RID: 943
		internal new class Definition : DefinitionStore<MapColorRangeRule, MapColorRangeRule.Definition.Properties>
		{
			// Token: 0x06001853 RID: 6227 RVA: 0x0003B619 File Offset: 0x00039819
			private Definition()
			{
			}

			// Token: 0x020004C7 RID: 1223
			internal enum Properties
			{
				// Token: 0x04000E90 RID: 3728
				DataValue,
				// Token: 0x04000E91 RID: 3729
				DistributionType,
				// Token: 0x04000E92 RID: 3730
				BucketCount,
				// Token: 0x04000E93 RID: 3731
				StartValue,
				// Token: 0x04000E94 RID: 3732
				EndValue,
				// Token: 0x04000E95 RID: 3733
				MapBuckets,
				// Token: 0x04000E96 RID: 3734
				LegendName,
				// Token: 0x04000E97 RID: 3735
				LegendText,
				// Token: 0x04000E98 RID: 3736
				DataElementName,
				// Token: 0x04000E99 RID: 3737
				DataElementOutput,
				// Token: 0x04000E9A RID: 3738
				ShowInColorScale,
				// Token: 0x04000E9B RID: 3739
				StartColor,
				// Token: 0x04000E9C RID: 3740
				MiddleColor,
				// Token: 0x04000E9D RID: 3741
				EndColor,
				// Token: 0x04000E9E RID: 3742
				PropertyCount
			}
		}
	}
}
