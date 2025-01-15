using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001226 RID: 4646
	internal sealed class VerbatimSqlQueryExpression : SqlQueryExpression
	{
		// Token: 0x06007ABE RID: 31422 RVA: 0x001A797A File Offset: 0x001A5B7A
		public VerbatimSqlQueryExpression(string verbatimQuery, IEnumerable<DynamicParameter> parameters)
		{
			this.verbatimQuery = verbatimQuery;
			this.parameters = parameters;
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x001A7990 File Offset: 0x001A5B90
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (this.parameters != null)
			{
				foreach (DynamicParameter dynamicParameter in this.parameters)
				{
					writer.AddDynamicParameter(dynamicParameter);
				}
			}
			writer.Write(new VerbatimString(this.verbatimQuery));
		}

		// Token: 0x04004405 RID: 17413
		private readonly string verbatimQuery;

		// Token: 0x04004406 RID: 17414
		private readonly IEnumerable<DynamicParameter> parameters;
	}
}
