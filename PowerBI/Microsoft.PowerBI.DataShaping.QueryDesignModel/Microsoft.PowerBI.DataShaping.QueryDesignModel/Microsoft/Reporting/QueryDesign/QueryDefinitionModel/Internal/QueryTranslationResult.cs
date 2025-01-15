using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000117 RID: 279
	public sealed class QueryTranslationResult
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		public static QueryTranslationResult Empty
		{
			get
			{
				return QueryTranslationResult._emptyInstance;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0002C2E7 File Offset: 0x0002A4E7
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0002C2EF File Offset: 0x0002A4EF
		public string CommandText { get; private set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0002C2F8 File Offset: 0x0002A4F8
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0002C300 File Offset: 0x0002A500
		public ReadOnlyCollection<QueryResultField> ResultFields { get; private set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0002C309 File Offset: 0x0002A509
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x0002C311 File Offset: 0x0002A511
		public IReadOnlyList<QueryItemSourceLocation> QuerySourceMap { get; private set; }

		// Token: 0x0600100A RID: 4106 RVA: 0x0002C31A File Offset: 0x0002A51A
		private QueryTranslationResult()
		{
			this.CommandText = string.Empty;
			this.ResultFields = Util.EmptyReadOnlyCollection<QueryResultField>();
			this.QuerySourceMap = null;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0002C33F File Offset: 0x0002A53F
		internal QueryTranslationResult(string commandText, IEnumerable<QueryResultField> resultFields, IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			this.CommandText = ArgumentValidation.CheckNotNullOrEmpty(commandText, "commandText");
			this.ResultFields = ArgumentValidation.CheckNotNullOrEmpty<QueryResultField>(resultFields, "resultFields").ToReadOnlyCollection<QueryResultField>();
			this.QuerySourceMap = querySourceMap;
		}

		// Token: 0x04000A56 RID: 2646
		private static QueryTranslationResult _emptyInstance = new QueryTranslationResult();
	}
}
