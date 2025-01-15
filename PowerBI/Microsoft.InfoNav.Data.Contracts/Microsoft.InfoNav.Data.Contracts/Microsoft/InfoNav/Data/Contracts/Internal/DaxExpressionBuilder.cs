using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000174 RID: 372
	public static class DaxExpressionBuilder
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x000137CC File Offset: 0x000119CC
		public static string Union(IEnumerable<string> tables)
		{
			StringBuilder stringBuilder = null;
			int num = 0;
			string text = null;
			foreach (string text2 in tables)
			{
				if (num == 0)
				{
					text = text2;
					num = 1;
				}
				else
				{
					if (num == 1)
					{
						stringBuilder = new StringBuilder(50);
						stringBuilder.Append("UNION");
						stringBuilder.Append('(');
						stringBuilder.Append(text);
					}
					stringBuilder.Append(',');
					stringBuilder.Append(text2);
					num++;
				}
			}
			if (num == 0)
			{
				return string.Empty;
			}
			if (num == 1)
			{
				return text;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001387C File Offset: 0x00011A7C
		public static string AddColumns(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("ADDCOLUMNS", parameters);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00013889 File Offset: 0x00011A89
		public static string SelectColumns(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("SELECTCOLUMNS", parameters);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00013896 File Offset: 0x00011A96
		public static string SummarizeColumns(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("SUMMARIZECOLUMNS", parameters);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000138A3 File Offset: 0x00011AA3
		public static string TreatAs(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("TREATAS", parameters);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000138B0 File Offset: 0x00011AB0
		public static string Calculate(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("CALCULATE", parameters);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000138BD File Offset: 0x00011ABD
		public static string ColumnStatistics()
		{
			return DaxExpressionBuilder.CreateFunction("COLUMNSTATISTICS", new string[0]);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000138CF File Offset: 0x00011ACF
		public static string Filter(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("FILTER", parameters);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000138DC File Offset: 0x00011ADC
		public static string All(string edmProperty)
		{
			return DaxExpressionBuilder.CreateFunction("ALL", new string[] { edmProperty });
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000138F2 File Offset: 0x00011AF2
		public static string Values(string tableOrColumn)
		{
			return DaxExpressionBuilder.CreateFunction("VALUES", new string[] { tableOrColumn });
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00013908 File Offset: 0x00011B08
		public static string Max(string column)
		{
			return DaxExpressionBuilder.CreateFunction("MAX", new string[] { column });
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001391E File Offset: 0x00011B1E
		public static string Max(string expressionOne, string expressionTwo)
		{
			return DaxExpressionBuilder.CreateFunction("MAX", new string[] { expressionOne, expressionTwo });
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00013938 File Offset: 0x00011B38
		public static string If(string condition, string trueExpression, string falseExpression)
		{
			return DaxExpressionBuilder.CreateFunction("IF", new string[] { condition, trueExpression, falseExpression });
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00013956 File Offset: 0x00011B56
		public static string Len(string column)
		{
			return DaxExpressionBuilder.CreateFunction("LEN", new string[] { column });
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001396C File Offset: 0x00011B6C
		public static string Row(params string[] parameters)
		{
			return DaxExpressionBuilder.CreateFunction("ROW", parameters);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00013979 File Offset: 0x00011B79
		public static string Date(int year, int month, int day)
		{
			return DaxExpressionBuilder.CreateFunction("DATE", new string[]
			{
				year.ToStringInvariant(),
				month.ToStringInvariant(),
				day.ToStringInvariant()
			});
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000139A6 File Offset: 0x00011BA6
		public static string Time(int hour, int minute, int second)
		{
			return DaxExpressionBuilder.CreateFunction("TIME", new string[]
			{
				hour.ToStringInvariant(),
				minute.ToStringInvariant(),
				second.ToStringInvariant()
			});
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000139D3 File Offset: 0x00011BD3
		public static string ApproximateDistinctCount(string column)
		{
			return DaxExpressionBuilder.CreateFunction("APPROXIMATEDISTINCTCOUNT", new string[] { column });
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000139E9 File Offset: 0x00011BE9
		public static string LessThanOrEqualTo(string left, string right)
		{
			return left + "<=" + right;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000139F7 File Offset: 0x00011BF7
		public static string EqualTo(string left, string right)
		{
			return left + "==" + right;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00013A05 File Offset: 0x00011C05
		public static string StringLiteral(string value)
		{
			return "\"" + value + "\"";
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00013A17 File Offset: 0x00011C17
		public static string EscapeString(string value)
		{
			return value.Replace("\"", "\"\"");
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00013A29 File Offset: 0x00011C29
		public static string EntityReference(string entitySetName)
		{
			return StringUtil.FormatInvariant("'{0}'", entitySetName.Replace("'", "''"));
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00013A45 File Offset: 0x00011C45
		public static string DaxReference(EdmPropertyRef property)
		{
			return DaxExpressionBuilder.PropertyReference(property.EntitySetReferenceName, property.PropertyReferenceName);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00013A5A File Offset: 0x00011C5A
		public static string PropertyReference(string entitySetName, string propertyName)
		{
			return StringUtil.FormatInvariant("{0}[{1}]", DaxExpressionBuilder.EntityReference(entitySetName), propertyName.Replace("]", "]]"));
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00013A7C File Offset: 0x00011C7C
		public static string ValueList(IEnumerable<string> values)
		{
			StringBuilder stringBuilder = new StringBuilder(30);
			stringBuilder.Append('{');
			DaxExpressionBuilder.AppendWithCommaDelimited(stringBuilder, values.Select((string e) => DaxExpressionBuilder.StringLiteral(DaxExpressionBuilder.EscapeString(e))).AsReadOnlyList<string>());
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00013AD8 File Offset: 0x00011CD8
		public static string LegacyOrConditionsList(string edmProperty, IEnumerable<string> values)
		{
			StringBuilder stringBuilder = new StringBuilder(30);
			IEnumerable<string> enumerable = values.Select((string e) => DaxExpressionBuilder.StringLiteral(DaxExpressionBuilder.EscapeString(e))).AsReadOnlyList<string>();
			bool flag = true;
			foreach (string text in enumerable)
			{
				if (!flag)
				{
					stringBuilder.Append("||");
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(edmProperty);
				stringBuilder.Append('=');
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00013B80 File Offset: 0x00011D80
		private static string CreateFunction(string name, params string[] parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(30);
			stringBuilder.Append(name);
			stringBuilder.Append('(');
			DaxExpressionBuilder.AppendWithCommaDelimited(stringBuilder, parameters);
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00013BB0 File Offset: 0x00011DB0
		private static void AppendWithCommaDelimited(StringBuilder stringBuilder, IEnumerable<string> values)
		{
			bool flag = true;
			foreach (string text in values)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(text);
			}
		}
	}
}
