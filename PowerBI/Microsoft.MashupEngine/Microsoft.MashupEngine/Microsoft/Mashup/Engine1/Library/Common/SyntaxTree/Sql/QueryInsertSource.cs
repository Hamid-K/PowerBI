using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001213 RID: 4627
	internal sealed class QueryInsertSource : SqlInsertSource
	{
		// Token: 0x06007A1F RID: 31263 RVA: 0x001A59B4 File Offset: 0x001A3BB4
		public QueryInsertSource(SqlQueryExpression query)
		{
			this.query = query;
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06007A20 RID: 31264 RVA: 0x001A59C3 File Offset: 0x001A3BC3
		public SqlQueryExpression Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x001A59CB File Offset: 0x001A3BCB
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.query.WriteCreateScript(writer);
		}

		// Token: 0x04004280 RID: 17024
		private readonly SqlQueryExpression query;
	}
}
