using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200011D RID: 285
	internal sealed class BatchTranslationTableResult
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x0002C6BC File Offset: 0x0002A8BC
		internal BatchTranslationTableResult(DaxExpression daxExpression, IReadOnlyList<TranslationResultField> dataFields)
		{
			this._daxExpression = daxExpression;
			this._dataFields = dataFields;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0002C6D2 File Offset: 0x0002A8D2
		public DaxExpression DaxExpression
		{
			get
			{
				return this._daxExpression;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0002C6DA File Offset: 0x0002A8DA
		public IReadOnlyList<TranslationResultField> DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x04000A64 RID: 2660
		private readonly DaxExpression _daxExpression;

		// Token: 0x04000A65 RID: 2661
		private readonly IReadOnlyList<TranslationResultField> _dataFields;
	}
}
