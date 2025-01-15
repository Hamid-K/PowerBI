using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011DF RID: 4575
	internal sealed class JoinOperation : FromItem
	{
		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x060078A5 RID: 30885 RVA: 0x001A1904 File Offset: 0x0019FB04
		// (set) Token: 0x060078A6 RID: 30886 RVA: 0x001A190C File Offset: 0x0019FB0C
		public Condition JoinCondition { get; set; }

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x060078A7 RID: 30887 RVA: 0x001A1915 File Offset: 0x0019FB15
		// (set) Token: 0x060078A8 RID: 30888 RVA: 0x001A191D File Offset: 0x0019FB1D
		public FromItem Left { get; set; }

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x060078A9 RID: 30889 RVA: 0x001A1926 File Offset: 0x0019FB26
		// (set) Token: 0x060078AA RID: 30890 RVA: 0x001A192E File Offset: 0x0019FB2E
		public JoinOperator Operator { get; set; }

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x060078AB RID: 30891 RVA: 0x001A1937 File Offset: 0x0019FB37
		// (set) Token: 0x060078AC RID: 30892 RVA: 0x001A193F File Offset: 0x0019FB3F
		public FromItem Right { get; set; }

		// Token: 0x060078AD RID: 30893 RVA: 0x001A1948 File Offset: 0x0019FB48
		public static bool RequireJoinCondition(JoinOperator @operator)
		{
			return @operator - JoinOperator.CrossJoin > 1;
		}

		// Token: 0x060078AE RID: 30894 RVA: 0x001A1954 File Offset: 0x0019FB54
		public override FromItem ShallowCopy()
		{
			return new JoinOperation
			{
				Alias = base.Alias,
				JoinCondition = this.JoinCondition,
				Left = this.Left,
				Operator = this.Operator,
				Right = this.Right
			};
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x001A19A4 File Offset: 0x0019FBA4
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.Left.WriteCreateScript(writer);
			writer.WriteLine();
			switch (this.Operator)
			{
			case JoinOperator.InnerJoin:
				writer.WriteSpaceAfter(SqlLanguageStrings.InnerJoinSqlString);
				break;
			case JoinOperator.LeftOuterJoin:
				writer.WriteSpaceAfter(SqlLanguageStrings.LeftOuterJoinSqlString);
				break;
			case JoinOperator.RightOuterJoin:
				writer.WriteSpaceAfter(SqlLanguageStrings.RightOuterJoinSqlString);
				break;
			case JoinOperator.FullOuterJoin:
				writer.WriteSpaceAfter(SqlLanguageStrings.FullOuterJoinSqlString);
				break;
			case JoinOperator.CrossJoin:
				if (writer.Settings.UseCommaForCrossJoin)
				{
					writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				}
				else
				{
					writer.Write(SqlLanguageStrings.CrossJoinSqlString);
				}
				break;
			case JoinOperator.CrossApply:
				writer.WriteSpaceAfter(SqlLanguageStrings.CrossApplySqlString);
				break;
			}
			this.Right.WriteCreateScript(writer);
			if (JoinOperation.RequireJoinCondition(this.Operator))
			{
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.OnSqlString);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				this.JoinCondition.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}
		}
	}
}
