using System;
using Microsoft.AnalysisServices.Tabular.Tmdl;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016B RID: 363
	internal struct TmdlSerializationConfiguration
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x000A0F9D File Offset: 0x0009F19D
		public TmdlSerializationConfiguration(IMetadataFilter filter)
		{
			this.Filter = filter;
			this.IncludeRestrictedInformation = false;
			this.TrimStyle = TmdlExpressionTrimStyle.NoTrim;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000A0FB4 File Offset: 0x0009F1B4
		public TmdlSerializationConfiguration(MetadataSerializationOptions options)
		{
			this.Filter = (options.IncludeChildren ? TmdlSerializationHelper.DefaultFilter : TmdlSerializationHelper.IgnoreChildrenFilter);
			this.IncludeRestrictedInformation = options.IncludeRestrictedInformation;
			TmdlSerializationOptions tmdlSerializationOptions = options as TmdlSerializationOptions;
			if (tmdlSerializationOptions != null)
			{
				this.TrimStyle = tmdlSerializationOptions.ExpressionTrimStyle;
				return;
			}
			this.TrimStyle = TmdlExpressionTrimStyle.NoTrim;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000A1005 File Offset: 0x0009F205
		internal bool IsValid
		{
			get
			{
				return this.Filter != null;
			}
		}

		// Token: 0x04000441 RID: 1089
		public readonly IMetadataFilter Filter;

		// Token: 0x04000442 RID: 1090
		public readonly bool IncludeRestrictedInformation;

		// Token: 0x04000443 RID: 1091
		public readonly TmdlExpressionTrimStyle TrimStyle;
	}
}
