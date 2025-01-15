using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000053 RID: 83
	internal sealed class DataShapeQueryTranslationOptions
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		internal DataShapeQueryTranslationOptions(double? enhancedSamplingAdditionalKeyPointsRatio = null, bool? applyTransformsInQuery = null, bool omitOrderBy = false, bool generateComposableQueryColumnNames = false, bool generateComposableQuery = false, bool suppressModelGrouping = false)
		{
			this.EnhancedSamplingAdditionalKeyPointsRatio = enhancedSamplingAdditionalKeyPointsRatio;
			this.ApplyTransformsInQuery = applyTransformsInQuery;
			this.GenerateComposableQueryColumnNames = generateComposableQueryColumnNames;
			this.GenerateComposableQuery = generateComposableQuery;
			this.SuppressModelGrouping = suppressModelGrouping;
			if (generateComposableQuery)
			{
				this.OmitOrderBy = true;
				return;
			}
			this.OmitOrderBy = omitOrderBy;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public double? EnhancedSamplingAdditionalKeyPointsRatio { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public bool? ApplyTransformsInQuery { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public bool OmitOrderBy { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public bool GenerateComposableQueryColumnNames { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public bool GenerateComposableQuery { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public bool SuppressModelGrouping { get; }

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public DataShapeQueryTranslationOptions CloneWithOverrides(bool applyTransformsInQuery)
		{
			return new DataShapeQueryTranslationOptions(this.EnhancedSamplingAdditionalKeyPointsRatio, new bool?(applyTransformsInQuery), this.OmitOrderBy, this.GenerateComposableQueryColumnNames, this.GenerateComposableQuery, this.SuppressModelGrouping);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		// Note: this type is marked as 'beforefieldinit'.
		static DataShapeQueryTranslationOptions()
		{
			double? num = null;
			bool? flag = null;
			DataShapeQueryTranslationOptions.Default = new DataShapeQueryTranslationOptions(num, flag, false, false, false, false);
			flag = new bool?(false);
			DataShapeQueryTranslationOptions.DefaultWithApplyTransformsInQueryFalse = new DataShapeQueryTranslationOptions(null, flag, false, false, false, false);
		}

		// Token: 0x040001A4 RID: 420
		internal static readonly DataShapeQueryTranslationOptions Default;

		// Token: 0x040001A5 RID: 421
		internal static readonly DataShapeQueryTranslationOptions DefaultWithApplyTransformsInQueryFalse;
	}
}
