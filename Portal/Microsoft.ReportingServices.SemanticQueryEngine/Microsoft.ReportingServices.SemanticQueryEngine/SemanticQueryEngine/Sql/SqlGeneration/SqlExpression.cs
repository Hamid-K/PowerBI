using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000029 RID: 41
	internal abstract class SqlExpression : ISqlSnippet
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000078AB File Offset: 0x00005AAB
		protected SqlExpression(bool nullable)
		{
			this.m_nullable = nullable;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000078C8 File Offset: 0x00005AC8
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			for (int i = 0; i < this.Values.Count; i++)
			{
				if (i > 0)
				{
					fsw.Write(", ");
				}
				this.Values[i].Compile(fsw);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000790C File Offset: 0x00005B0C
		internal SqlExpression.ValuesCollection Values
		{
			[DebuggerStepThrough]
			get
			{
				if (this.__values.IsReadOnly)
				{
					return this.__values;
				}
				SqlExpression.ValuesCollection _values = this.__values;
				SqlExpression.ValuesCollection _values2;
				lock (_values)
				{
					if (!this.__values.IsReadOnly && !this.m_valuesInitializing)
					{
						this.m_valuesInitializing = true;
						try
						{
							this.InitValues();
						}
						finally
						{
							this.m_valuesInitializing = false;
						}
						if (this.__values.Count == 0)
						{
							throw SQEAssert.AssertFalseAndThrow("Failed to initialize SqlExpression.Values collection.", Array.Empty<object>());
						}
						this.__values.SetReadOnly();
					}
					_values2 = this.__values;
				}
				return _values2;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001A1 RID: 417
		internal abstract bool CanGroupBy { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000079C4 File Offset: 0x00005BC4
		internal virtual bool IsNullable
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nullable;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004555 File Offset: 0x00002755
		internal virtual bool IsLogicalBooleanValue
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000079CC File Offset: 0x00005BCC
		internal static string GetCandidateAlias(object expressionKey)
		{
			Expression expression = expressionKey as Expression;
			DsvItem dsvItem = expressionKey as DsvItem;
			if (expression != null)
			{
				if (expression.Name.Length > 0)
				{
					return expression.Name;
				}
				if (expression.NodeAsFunction != null)
				{
					if (expression.NodeAsFunction.Arguments.Count == 1)
					{
						return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}Of{1}", new object[]
						{
							expression.NodeAsFunction.FunctionName,
							SqlExpression.GetCandidateAlias(expression.NodeAsFunction.Arguments[0])
						});
					}
					return expression.NodeAsFunction.FunctionName.ToString();
				}
				else
				{
					if (expression.NodeAsAttributeRef != null)
					{
						return expression.NodeAsAttributeRef.Attribute.Name;
					}
					if (expression.NodeAsEntityRef != null)
					{
						return expression.NodeAsEntityRef.Entity.Name;
					}
					if (expression.NodeAsLiteral != null)
					{
						return "Literal";
					}
					if (expression.NodeAsNull != null)
					{
						return "Null";
					}
				}
			}
			else if (dsvItem != null)
			{
				return dsvItem.Name;
			}
			return null;
		}

		// Token: 0x060001A5 RID: 421
		protected abstract void InitValues();

		// Token: 0x0400007C RID: 124
		protected static readonly ISqlSnippet SumOpenParenSnippet = new SqlStringSnippet("SUM(");

		// Token: 0x0400007D RID: 125
		protected static readonly ISqlSnippet AvgOpenParenSnippet = new SqlStringSnippet("AVG(");

		// Token: 0x0400007E RID: 126
		protected static readonly ISqlSnippet MaxOpenParenSnippet = new SqlStringSnippet("MAX(");

		// Token: 0x0400007F RID: 127
		protected static readonly ISqlSnippet MinOpenParenSnippet = new SqlStringSnippet("MIN(");

		// Token: 0x04000080 RID: 128
		protected static readonly ISqlSnippet CountOpenParenSnippet = new SqlStringSnippet("COUNT(");

		// Token: 0x04000081 RID: 129
		protected static readonly ISqlSnippet CountOpenParenDistinctSnippet = new SqlStringSnippet("COUNT(DISTINCT ");

		// Token: 0x04000082 RID: 130
		protected static readonly ISqlSnippet StDevOpenParenSnippet = new SqlStringSnippet("STDEV(");

		// Token: 0x04000083 RID: 131
		protected static readonly ISqlSnippet StDevPOpenParenSnippet = new SqlStringSnippet("STDEVP(");

		// Token: 0x04000084 RID: 132
		protected static readonly ISqlSnippet VarOpenParenSnippet = new SqlStringSnippet("VAR(");

		// Token: 0x04000085 RID: 133
		protected static readonly ISqlSnippet VarPOpenParenSnippet = new SqlStringSnippet("VARP(");

		// Token: 0x04000086 RID: 134
		protected static readonly ISqlSnippet CoalesceOpenParenSnippet = new SqlStringSnippet("COALESCE(");

		// Token: 0x04000087 RID: 135
		protected static readonly ISqlSnippet IsNullSnippet = new SqlStringSnippet(" IS NULL");

		// Token: 0x04000088 RID: 136
		protected static readonly ISqlSnippet CaseWhenSnippet = new SqlStringSnippet("CASE WHEN ");

		// Token: 0x04000089 RID: 137
		protected static readonly ISqlSnippet IsNullThenSnippet = new SqlStringSnippet(" IS NULL THEN ");

		// Token: 0x0400008A RID: 138
		protected static readonly ISqlSnippet CaseSnippet = new SqlStringSnippet("CASE");

		// Token: 0x0400008B RID: 139
		protected static readonly ISqlSnippet WhenSnippet = new SqlStringSnippet(" WHEN ");

		// Token: 0x0400008C RID: 140
		protected static readonly ISqlSnippet ThenSnippet = new SqlStringSnippet(" THEN ");

		// Token: 0x0400008D RID: 141
		protected static readonly ISqlSnippet ElseSnippet = new SqlStringSnippet(" ELSE ");

		// Token: 0x0400008E RID: 142
		protected static readonly ISqlSnippet EndSnippet = new SqlStringSnippet(" END");

		// Token: 0x0400008F RID: 143
		internal static readonly ISqlSnippet NullSnippet = new SqlStringSnippet("NULL");

		// Token: 0x04000090 RID: 144
		protected static readonly ISqlSnippet CommaZeroCloseParenSnippet = new SqlStringSnippet(", 0)");

		// Token: 0x04000091 RID: 145
		protected static readonly ISqlSnippet CommaEmpStrCloseParenSnippet = new SqlStringSnippet(", '')");

		// Token: 0x04000092 RID: 146
		protected static readonly ISqlSnippet AddSnippet = new SqlStringSnippet(" + ");

		// Token: 0x04000093 RID: 147
		protected static readonly ISqlSnippet SubtractSnippet = new SqlStringSnippet(" - ");

		// Token: 0x04000094 RID: 148
		protected static readonly ISqlSnippet MultiplySnippet = new SqlStringSnippet(" * ");

		// Token: 0x04000095 RID: 149
		protected static readonly ISqlSnippet DivideSnippet = new SqlStringSnippet(" / ");

		// Token: 0x04000096 RID: 150
		protected static readonly ISqlSnippet ModSnippet = new SqlStringSnippet(" % ");

		// Token: 0x04000097 RID: 151
		protected static readonly ISqlSnippet EqualsSnippet = new SqlStringSnippet(" = ");

		// Token: 0x04000098 RID: 152
		protected static readonly ISqlSnippet GreaterThanSnippet = new SqlStringSnippet(" > ");

		// Token: 0x04000099 RID: 153
		protected static readonly ISqlSnippet GreaterThanOrEqualsSnippet = new SqlStringSnippet(" >= ");

		// Token: 0x0400009A RID: 154
		protected static readonly ISqlSnippet LessThanSnippet = new SqlStringSnippet(" < ");

		// Token: 0x0400009B RID: 155
		protected static readonly ISqlSnippet LessThanOrEqualsSnippet = new SqlStringSnippet(" <= ");

		// Token: 0x0400009C RID: 156
		protected static readonly ISqlSnippet AndSnippet = new SqlStringSnippet(" AND ");

		// Token: 0x0400009D RID: 157
		protected static readonly ISqlSnippet OrSnippet = new SqlStringSnippet(" OR ");

		// Token: 0x0400009E RID: 158
		protected static readonly ISqlSnippet NotOpenParenSnippet = new SqlStringSnippet("NOT(");

		// Token: 0x0400009F RID: 159
		protected static readonly ISqlSnippet InOpenParenSnippet = new SqlStringSnippet(" IN (");

		// Token: 0x040000A0 RID: 160
		internal static readonly ISqlSnippet CastOpenParenSnippet = new SqlStringSnippet("CAST(");

		// Token: 0x040000A1 RID: 161
		protected static readonly ISqlSnippet AsDecimalOpenParen28Comma = new SqlStringSnippet(" AS DECIMAL(28,");

		// Token: 0x040000A2 RID: 162
		protected static readonly ISqlSnippet AsFloatCloseParenSnippet = new SqlStringSnippet(" AS FLOAT)");

		// Token: 0x040000A3 RID: 163
		protected static readonly ISqlSnippet AsRealCloseParenSnippet = new SqlStringSnippet(" AS REAL)");

		// Token: 0x040000A4 RID: 164
		protected static readonly ISqlSnippet AsBitCloseParenSnippet = new SqlStringSnippet(" AS BIT)");

		// Token: 0x040000A5 RID: 165
		protected static readonly ISqlSnippet AsGuidCloseParenSnippet = new SqlStringSnippet(" AS UNIQUEIDENTIFIER)");

		// Token: 0x040000A6 RID: 166
		protected static readonly ISqlSnippet AsVarChar255CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(255))");

		// Token: 0x040000A7 RID: 167
		protected static readonly ISqlSnippet PowerOpenParenSnippet = new SqlStringSnippet("POWER(");

		// Token: 0x040000A8 RID: 168
		protected static readonly ISqlSnippet RoundOpenParenSnippet = new SqlStringSnippet("ROUND(");

		// Token: 0x040000A9 RID: 169
		protected static readonly ISqlSnippet LeftOpenParenSnippet = new SqlStringSnippet("LEFT(");

		// Token: 0x040000AA RID: 170
		protected static readonly ISqlSnippet RightOpenParenSnippet = new SqlStringSnippet("RIGHT(");

		// Token: 0x040000AB RID: 171
		protected static readonly ISqlSnippet LowerOpenParenSnippet = new SqlStringSnippet("LOWER(");

		// Token: 0x040000AC RID: 172
		protected static readonly ISqlSnippet UpperOpenParenSnippet = new SqlStringSnippet("UPPER(");

		// Token: 0x040000AD RID: 173
		protected static readonly ISqlSnippet LTrimOpenParenSnippet = new SqlStringSnippet("LTRIM(");

		// Token: 0x040000AE RID: 174
		protected static readonly ISqlSnippet RTrimOpenParenSnippet = new SqlStringSnippet("RTRIM(");

		// Token: 0x040000AF RID: 175
		protected static readonly ISqlSnippet ReplaceOpenParenSnippet = new SqlStringSnippet("REPLACE(");

		// Token: 0x040000B0 RID: 176
		protected static readonly ISqlSnippet LenOpenParenSnippet = new SqlStringSnippet("LEN(");

		// Token: 0x040000B1 RID: 177
		protected static readonly ISqlSnippet FloorOpenParenSnippet = new SqlStringSnippet("FLOOR(");

		// Token: 0x040000B2 RID: 178
		protected static readonly ISqlSnippet PlusSnippet = new SqlStringSnippet(" + ");

		// Token: 0x040000B3 RID: 179
		protected static readonly ISqlSnippet MinusSnippet = new SqlStringSnippet(" - ");

		// Token: 0x040000B4 RID: 180
		protected static readonly ISqlSnippet CommaSnippet = new SqlStringSnippet(", ");

		// Token: 0x040000B5 RID: 181
		protected static readonly ISqlSnippet BoolFalseSnippet = new SqlStringSnippet("1<>1");

		// Token: 0x040000B6 RID: 182
		protected static readonly ISqlSnippet ZeroSnippet = new SqlStringSnippet("0");

		// Token: 0x040000B7 RID: 183
		protected static readonly ISqlSnippet OneSnippet = new SqlStringSnippet("1");

		// Token: 0x040000B8 RID: 184
		protected static readonly ISqlSnippet DivideBy7CloseParenSnippet = new SqlStringSnippet(" / 7)");

		// Token: 0x040000B9 RID: 185
		protected static readonly ISqlSnippet SevenSnippet = new SqlStringSnippet("7");

		// Token: 0x040000BA RID: 186
		internal static readonly ISqlSnippet OpenParenSnippet = new SqlStringSnippet("(");

		// Token: 0x040000BB RID: 187
		internal static readonly ISqlSnippet CloseParenSnippet = new SqlStringSnippet(")");

		// Token: 0x040000BC RID: 188
		protected static readonly SqlSnippetExpression SqlIntOneExpression = new SqlSnippetExpression(SqlExpression.OneSnippet, false);

		// Token: 0x040000BD RID: 189
		private readonly SqlExpression.ValuesCollection __values = new SqlExpression.ValuesCollection();

		// Token: 0x040000BE RID: 190
		private bool m_valuesInitializing;

		// Token: 0x040000BF RID: 191
		private readonly bool m_nullable;

		// Token: 0x020000B7 RID: 183
		internal sealed class ValuesCollection : CheckedCollection<ISqlSnippet>
		{
			// Token: 0x060006AE RID: 1710 RVA: 0x0001ACD3 File Offset: 0x00018ED3
			internal ValuesCollection()
			{
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0001ACDB File Offset: 0x00018EDB
			internal void SetReadOnly()
			{
				base.SetReadOnlyIndicator();
			}
		}
	}
}
