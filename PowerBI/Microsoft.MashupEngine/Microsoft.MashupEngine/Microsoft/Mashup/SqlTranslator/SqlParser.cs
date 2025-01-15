using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.ScriptDom;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200203D RID: 8253
	public sealed class SqlParser
	{
		// Token: 0x0600C9E4 RID: 51684 RVA: 0x00286690 File Offset: 0x00284890
		public static SqlParseResult Parse(string sql)
		{
			IList<ParseError> list;
			TSqlFragment tsqlFragment = new TSql100Parser(false).Parse(new StringReader(sql), out list);
			SelectStatement selectStatement;
			if (tsqlFragment != null && SqlExpressionHelper.TryGetSelectStatement(tsqlFragment, out selectStatement))
			{
				IEnumerable<string> enumerable = SqlResourceExtractor.ExtractResources(tsqlFragment);
				return SqlParseResult.NewRecognized(sql, selectStatement, enumerable);
			}
			return SqlParseResult.NewUnrecognized();
		}

		// Token: 0x0600C9E5 RID: 51685 RVA: 0x002866D4 File Offset: 0x002848D4
		public static SqlParseResult Parse(IEngineHost host, string sql)
		{
			SqlParseResult sqlParseResult;
			using (IHostTrace hostTrace = TracingService.ScopedPerformanceTrace(host, "SqlTranslator/SqlParser/Parse", TraceEventType.Information, null))
			{
				hostTrace.Add("SQL", sql, true);
				try
				{
					sqlParseResult = SqlParser.Parse(sql);
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					throw;
				}
			}
			return sqlParseResult;
		}
	}
}
