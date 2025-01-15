using System;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000054 RID: 84
	internal sealed class DataShapeQueryTranslationResult
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		internal DataShapeQueryTranslationResult(DataShapeDefinition unifiedDataShapeDefinition, QueryPatternKind queryPatternKind, TranslationErrorContext errorContext)
		{
			this.UnifiedDataShapeDefinition = unifiedDataShapeDefinition;
			this.QueryPatternKind = queryPatternKind;
			this.ErrorContext = errorContext;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000CE0E File Offset: 0x0000B00E
		internal QueryPatternKind QueryPatternKind { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000CE16 File Offset: 0x0000B016
		public DataShapeDefinition UnifiedDataShapeDefinition { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000CE1E File Offset: 0x0000B01E
		internal TranslationErrorContext ErrorContext { get; }
	}
}
