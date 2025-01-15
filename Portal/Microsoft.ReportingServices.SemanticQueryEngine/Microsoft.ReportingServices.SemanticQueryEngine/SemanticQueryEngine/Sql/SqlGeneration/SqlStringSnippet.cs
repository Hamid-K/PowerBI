using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200003A RID: 58
	internal sealed class SqlStringSnippet : ISqlSnippet
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000C377 File Offset: 0x0000A577
		internal SqlStringSnippet(string sql)
		{
			this.m_sql = sql;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C386 File Offset: 0x0000A586
		internal SqlStringSnippet(string format, params object[] args)
		{
			this.m_sql = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(format, args);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C39B File Offset: 0x0000A59B
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			fsw.Write(this.m_sql);
		}

		// Token: 0x040000EC RID: 236
		private readonly string m_sql;
	}
}
