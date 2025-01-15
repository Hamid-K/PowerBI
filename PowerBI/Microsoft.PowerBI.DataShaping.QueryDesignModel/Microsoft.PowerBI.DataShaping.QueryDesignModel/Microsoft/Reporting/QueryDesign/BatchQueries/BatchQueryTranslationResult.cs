using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000262 RID: 610
	internal sealed class BatchQueryTranslationResult
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x00049902 File Offset: 0x00047B02
		internal BatchQueryTranslationResult(string commandText, IEnumerable<BatchQueryTranslationTableResult> tables, IReadOnlyList<QueryItemSourceLocation> querySourceMap)
		{
			this._commandText = commandText;
			this._tables = tables.ToReadOnlyCollection<BatchQueryTranslationTableResult>();
			this._querySourceMap = querySourceMap;
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00049924 File Offset: 0x00047B24
		public string CommandText
		{
			get
			{
				return this._commandText;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0004992C File Offset: 0x00047B2C
		public ReadOnlyCollection<BatchQueryTranslationTableResult> Tables
		{
			get
			{
				return this._tables;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00049934 File Offset: 0x00047B34
		public IReadOnlyList<QueryItemSourceLocation> QuerySourceMap
		{
			get
			{
				return this._querySourceMap;
			}
		}

		// Token: 0x04000EA6 RID: 3750
		private readonly string _commandText;

		// Token: 0x04000EA7 RID: 3751
		private readonly ReadOnlyCollection<BatchQueryTranslationTableResult> _tables;

		// Token: 0x04000EA8 RID: 3752
		private readonly IReadOnlyList<QueryItemSourceLocation> _querySourceMap;
	}
}
