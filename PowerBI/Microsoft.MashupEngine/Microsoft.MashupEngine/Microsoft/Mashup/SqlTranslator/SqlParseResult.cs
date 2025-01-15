using System;
using System.Collections.Generic;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200203E RID: 8254
	public struct SqlParseResult
	{
		// Token: 0x0600C9E7 RID: 51687 RVA: 0x00286738 File Offset: 0x00284938
		public static SqlParseResult NewUnrecognized()
		{
			return new SqlParseResult(null, null, null);
		}

		// Token: 0x0600C9E8 RID: 51688 RVA: 0x00286742 File Offset: 0x00284942
		internal static SqlParseResult NewRecognized(string sql, SelectStatement select, IEnumerable<string> resourceNames)
		{
			return new SqlParseResult(sql, select, resourceNames);
		}

		// Token: 0x0600C9E9 RID: 51689 RVA: 0x0028674C File Offset: 0x0028494C
		private SqlParseResult(string sql, SelectStatement select, IEnumerable<string> resourceNames)
		{
			this.sql = sql;
			this.select = select;
			this.resourceNames = resourceNames;
		}

		// Token: 0x1700309F RID: 12447
		// (get) Token: 0x0600C9EA RID: 51690 RVA: 0x00286763 File Offset: 0x00284963
		public bool IsRecognized
		{
			get
			{
				return this.select != null;
			}
		}

		// Token: 0x170030A0 RID: 12448
		// (get) Token: 0x0600C9EB RID: 51691 RVA: 0x0028676E File Offset: 0x0028496E
		public bool IsPassthrough
		{
			get
			{
				return this.select != null && SqlExpressionHelper.IsPassthrough(this.select);
			}
		}

		// Token: 0x170030A1 RID: 12449
		// (get) Token: 0x0600C9EC RID: 51692 RVA: 0x00286785 File Offset: 0x00284985
		public string Sql
		{
			get
			{
				return this.sql;
			}
		}

		// Token: 0x170030A2 RID: 12450
		// (get) Token: 0x0600C9ED RID: 51693 RVA: 0x0028678D File Offset: 0x0028498D
		internal SelectStatement Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x170030A3 RID: 12451
		// (get) Token: 0x0600C9EE RID: 51694 RVA: 0x00286795 File Offset: 0x00284995
		public IEnumerable<string> ResourceNames
		{
			get
			{
				return this.resourceNames;
			}
		}

		// Token: 0x040066C8 RID: 26312
		private readonly string sql;

		// Token: 0x040066C9 RID: 26313
		private readonly SelectStatement select;

		// Token: 0x040066CA RID: 26314
		private readonly IEnumerable<string> resourceNames;
	}
}
