using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004C0 RID: 1216
	internal class SapBwMetadataAstCreator
	{
		// Token: 0x060027E4 RID: 10212 RVA: 0x0007598E File Offset: 0x00073B8E
		public SapBwMetadataAstCreator(long? top)
		{
			this.top = top;
			this.selectColumns = new List<SelectItem>();
			this.tables = new List<FromItem>();
			this.conditions = new List<Condition>();
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000759C0 File Offset: 0x00073BC0
		public string GenerateStatement()
		{
			PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
			pagingQuerySpecification.SelectItems.AddRange(this.selectColumns);
			pagingQuerySpecification.FromItems.AddRange(this.tables);
			pagingQuerySpecification.WhereClause = SapBwMetadataAstCreator.AndConditions(this.conditions);
			pagingQuerySpecification.RepeatedRowOption = RepeatedRowOption.None;
			pagingQuerySpecification.PagingClause = new PagingClause
			{
				FetchExpression = this.top
			};
			pagingQuerySpecification.OrderByClause = null;
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, new SqlSettings
				{
					PagingStrategy = PagingStrategy.TopAndRowCount
				});
				pagingQuerySpecification.WriteCreateScript(scriptWriter);
				stringWriter.Write(" WITH-OPTIONS(UseDataCompression=FALSE)");
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x00075A84 File Offset: 0x00073C84
		public void AddSelectColumns(params string[] columns)
		{
			foreach (string text in columns)
			{
				this.selectColumns.Add(new SelectItem(new ColumnReference(Alias.NewNativeAlias(text))));
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x00075AC0 File Offset: 0x00073CC0
		public void AddTable(string table)
		{
			this.tables.Add(new FromTable
			{
				Table = new TableReference(null, Alias.NewNativeAlias(table))
			});
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x00075AE4 File Offset: 0x00073CE4
		public void AddCondition(string column, Value value)
		{
			this.conditions.Add(new SapBwMetadataAstCreator.NoExtraParenthesesCondition(SapBwMetadataAstCreator.CreateVariableReference(column), SqlLanguageSymbols.EqualsSqlString, SapBwMetadataAstCreator.Constant(value)));
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00075B08 File Offset: 0x00073D08
		public void AddCondition(string column, ListValue values)
		{
			if (values.Count == 1)
			{
				this.AddCondition(column, values[0]);
				return;
			}
			if (values.Count > 1)
			{
				this.conditions.Add(new SapBwMetadataAstCreator.NoExtraParenthesesCondition(SapBwMetadataAstCreator.CreateVariableReference(column), SqlLanguageStrings.InSqlString, SapBwMetadataAstCreator.CreateInArrayExpression(values)));
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00075B58 File Offset: 0x00073D58
		public void AddCommandParameters(IDbCommand command)
		{
			string text = SapBwMetadataAstCreator.WriterToString(delegate(ScriptWriter writer)
			{
				bool flag = false;
				foreach (SelectItem selectItem in this.selectColumns)
				{
					flag = writer.WriteLineCommaIfNeeded(flag);
					selectItem.WriteCreateScript(writer);
				}
			});
			string text2 = SapBwMetadataAstCreator.WriterToString(delegate(ScriptWriter writer)
			{
				bool flag2 = false;
				foreach (FromItem fromItem in this.tables)
				{
					flag2 = writer.WriteLineCommaIfNeeded(flag2);
					fromItem.WriteCreateScript(writer);
				}
			});
			string text3 = ((this.conditions.Count == 0) ? null : SapBwMetadataAstCreator.WriterToString(delegate(ScriptWriter writer)
			{
				SapBwMetadataAstCreator.AndConditions(this.conditions).WriteCreateScript(writer);
			}));
			if (this.top != null)
			{
				command.CreateParameter(ParameterDirection.Input, "rowcount", this.top.Value);
			}
			command.CreateParameter(ParameterDirection.Input, "fields", text);
			command.CreateParameter(ParameterDirection.Input, "table", text2);
			command.CreateParameter(ParameterDirection.Input, "where", text3);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00075BFC File Offset: 0x00073DFC
		private static string WriterToString(Action<ScriptWriter> writeAction)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				ScriptWriter scriptWriter = new ScriptWriter(stringWriter, SapBwMetadataAstCreator.decomposedSqlSettings);
				writeAction(scriptWriter);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		private static string NoQuoteIdentifier(string identifier)
		{
			return identifier;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00075C4C File Offset: 0x00073E4C
		private static Condition AndConditions(List<Condition> conditions)
		{
			Condition condition = conditions.FirstOrDefault<Condition>();
			foreach (Condition condition2 in conditions.Skip(1))
			{
				condition = new SapBwMetadataAstCreator.NoExtraParenthesesCondition(condition, SqlLanguageStrings.AndSqlString, condition2);
			}
			return condition;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00075CA8 File Offset: 0x00073EA8
		private static SqlExpression CreateInArrayExpression(ListValue list)
		{
			InArrayExpression inArrayExpression = new InArrayExpression();
			for (int i = 0; i < list.Count; i++)
			{
				inArrayExpression.Add(SapBwMetadataAstCreator.Constant(list[i]));
			}
			return inArrayExpression;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00075CDF File Offset: 0x00073EDF
		private static VariableReference CreateVariableReference(string column)
		{
			return new VariableReference(Alias.NewNativeAlias(column));
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00075CEC File Offset: 0x00073EEC
		private static SqlConstant Constant(Value constant)
		{
			string @string = constant.AsText.String;
			if (@string == null)
			{
				return SqlConstant.Null;
			}
			if (ScriptWriter.IsAnsiString(@string))
			{
				return new SqlConstant(ConstantType.AnsiString, @string);
			}
			return new SqlConstant(ConstantType.UnicodeString, @string);
		}

		// Token: 0x040010E6 RID: 4326
		private static readonly SqlSettings decomposedSqlSettings = new SqlSettings
		{
			QuoteIdentifier = new Func<string, string>(SapBwMetadataAstCreator.NoQuoteIdentifier)
		};

		// Token: 0x040010E7 RID: 4327
		private readonly List<SelectItem> selectColumns;

		// Token: 0x040010E8 RID: 4328
		private readonly List<FromItem> tables;

		// Token: 0x040010E9 RID: 4329
		private readonly List<Condition> conditions;

		// Token: 0x040010EA RID: 4330
		private readonly long? top;

		// Token: 0x020004C1 RID: 1217
		private class NoExtraParenthesesCondition : Condition
		{
			// Token: 0x060027F5 RID: 10229 RVA: 0x00075E0F File Offset: 0x0007400F
			public NoExtraParenthesesCondition(SqlExpression left, ConstantSqlString op, SqlExpression right)
			{
				this.left = left;
				this.op = op;
				this.right = right;
			}

			// Token: 0x17000F93 RID: 3987
			// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00075E2C File Offset: 0x0007402C
			public override int Precedence
			{
				get
				{
					return 5;
				}
			}

			// Token: 0x060027F7 RID: 10231 RVA: 0x00075E2F File Offset: 0x0007402F
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.WriteSubexpression(this.left.Precedence + 1, this.left);
				writer.WriteSpaceBeforeAndAfter(this.op);
				writer.WriteSubexpression(this.right.Precedence + 1, this.right);
			}

			// Token: 0x040010EB RID: 4331
			private readonly SqlExpression left;

			// Token: 0x040010EC RID: 4332
			private readonly ConstantSqlString op;

			// Token: 0x040010ED RID: 4333
			private readonly SqlExpression right;

			// Token: 0x040010EE RID: 4334
			private static readonly HashSet<ConstantSqlString> validOperators = new HashSet<ConstantSqlString>
			{
				SqlLanguageSymbols.EqualsSqlString,
				SqlLanguageStrings.InSqlString,
				SqlLanguageStrings.AndSqlString
			};
		}
	}
}
