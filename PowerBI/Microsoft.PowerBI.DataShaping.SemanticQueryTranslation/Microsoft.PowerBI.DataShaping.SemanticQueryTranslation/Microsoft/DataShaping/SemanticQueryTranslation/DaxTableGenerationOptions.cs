using System;
using Microsoft.InfoNav.DataShapeQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000007 RID: 7
	internal sealed class DaxTableGenerationOptions
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000235D File Offset: 0x0000055D
		internal DaxTableGenerationOptions(DataShapeGenerationOptions dsqGenOptions, DataShapeQueryTranslationOptions dsqtOptions)
		{
			this._dsqGenOptions = dsqGenOptions;
			this._dsqtOptions = dsqtOptions;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002373 File Offset: 0x00000573
		internal DataShapeGenerationOptions DsqGenOptions
		{
			get
			{
				return this._dsqGenOptions;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000237B File Offset: 0x0000057B
		internal DataShapeQueryTranslationOptions DsqtOptions
		{
			get
			{
				return this._dsqtOptions;
			}
		}

		// Token: 0x04000030 RID: 48
		internal static readonly DaxTableGenerationOptions Empty = new DaxTableGenerationOptions(DataShapeGenerationOptions.Empty, DataShapeQueryTranslationOptions.Default);

		// Token: 0x04000031 RID: 49
		private readonly DataShapeGenerationOptions _dsqGenOptions;

		// Token: 0x04000032 RID: 50
		private readonly DataShapeQueryTranslationOptions _dsqtOptions;
	}
}
