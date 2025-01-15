using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200011C RID: 284
	internal sealed class BatchTranslationResult
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x0002C678 File Offset: 0x0002A878
		internal BatchTranslationResult(string commandText, IEnumerable<BatchTranslationTableResult> tables, IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			this._commandText = ArgumentValidation.CheckNotNullOrWhiteSpace(commandText, "commandText");
			this._tables = tables.ToReadOnlyCollection<BatchTranslationTableResult>();
			this._querySourceMap = querySourceMap;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0002C6A4 File Offset: 0x0002A8A4
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0002C6AC File Offset: 0x0002A8AC
		public ReadOnlyCollection<BatchTranslationTableResult> Tables
		{
			get
			{
				return this._tables;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0002C6B4 File Offset: 0x0002A8B4
		public IReadOnlyList<QueryItemSourceLocation> QuerySourceMap
		{
			get
			{
				return this._querySourceMap;
			}
		}

		// Token: 0x04000A61 RID: 2657
		private readonly string _commandText;

		// Token: 0x04000A62 RID: 2658
		private readonly ReadOnlyCollection<BatchTranslationTableResult> _tables;

		// Token: 0x04000A63 RID: 2659
		private readonly IReadOnlyList<QueryItemSourceLocation> _querySourceMap;
	}
}
