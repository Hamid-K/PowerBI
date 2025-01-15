using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001212 RID: 4626
	internal sealed class ValuesInsertSource : SqlInsertSource
	{
		// Token: 0x06007A1C RID: 31260 RVA: 0x001A58B4 File Offset: 0x001A3AB4
		public ValuesInsertSource(IEnumerable<IEnumerable<SqlExpression>> values)
		{
			this.values = values;
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x001A58C3 File Offset: 0x001A3AC3
		public IEnumerable<IEnumerable<SqlExpression>> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x001A58CC File Offset: 0x001A3ACC
		public override void WriteCreateScript(ScriptWriter writer)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (IEnumerable<SqlExpression> enumerable in this.values)
			{
				if (enumerable.Any<SqlExpression>())
				{
					if (!flag)
					{
						writer.Write(SqlLanguageStrings.ValuesSqlString);
						writer.WriteLine();
						flag = true;
					}
					flag2 = writer.WriteLineCommaIfNeeded(flag2);
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					bool flag3 = false;
					foreach (SqlExpression sqlExpression in enumerable)
					{
						flag3 = writer.WriteCommaIfNeeded(flag3);
						sqlExpression.WriteCreateScript(writer);
					}
					writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				}
				else
				{
					writer.Settings.EmptyRowInsertStrategy.WriteEmptyValuesClause(writer);
				}
			}
		}

		// Token: 0x0400427F RID: 17023
		private readonly IEnumerable<IEnumerable<SqlExpression>> values;
	}
}
