using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000151 RID: 337
	internal sealed class TranslationResult
	{
		// Token: 0x0600128E RID: 4750 RVA: 0x00035C33 File Offset: 0x00033E33
		internal TranslationResult(string commandText, IReadOnlyList<TranslationResultField> dataFields, IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			this._commandText = ArgumentValidation.CheckNotNullOrWhiteSpace(commandText, "commandText");
			this._dataFields = dataFields;
			this._querySourceMap = querySourceMap;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00035C5A File Offset: 0x00033E5A
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00035C62 File Offset: 0x00033E62
		public IReadOnlyList<TranslationResultField> DataFields
		{
			get
			{
				return this._dataFields;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00035C6A File Offset: 0x00033E6A
		public IReadOnlyList<QueryItemSourceLocation> QuerySourceMap
		{
			get
			{
				return this._querySourceMap;
			}
		}

		// Token: 0x04000AF0 RID: 2800
		private readonly string _commandText;

		// Token: 0x04000AF1 RID: 2801
		private readonly IReadOnlyList<TranslationResultField> _dataFields;

		// Token: 0x04000AF2 RID: 2802
		private readonly IReadOnlyList<QueryItemSourceLocation> _querySourceMap;
	}
}
