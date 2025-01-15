using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011BF RID: 4543
	internal sealed class CaseFunction : ScalarExpression
	{
		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06007818 RID: 30744 RVA: 0x001A0B3F File Offset: 0x0019ED3F
		// (set) Token: 0x06007819 RID: 30745 RVA: 0x001A0B47 File Offset: 0x0019ED47
		public SqlExpression CaseExpression { get; set; }

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x0600781A RID: 30746 RVA: 0x001A0B50 File Offset: 0x0019ED50
		// (set) Token: 0x0600781B RID: 30747 RVA: 0x001A0B58 File Offset: 0x0019ED58
		public SqlExpression ElseExpression { get; set; }

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x0600781C RID: 30748 RVA: 0x0014025A File Offset: 0x0013E45A
		public override int Precedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x0600781D RID: 30749 RVA: 0x001A0B61 File Offset: 0x0019ED61
		public IList<WhenItem> WhenItems
		{
			get
			{
				return this.whenItems;
			}
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x001A0B6C File Offset: 0x0019ED6C
		private void WriteCreateSwitchScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.SwitchSqlString);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			writer.WriteLine();
			writer.Indent();
			bool flag = false;
			foreach (WhenItem whenItem in this.whenItems)
			{
				if (this.CaseExpression != null)
				{
					whenItem.When = new BinaryLogicalOperation(this.CaseExpression, BinaryLogicalOperator.Equals, whenItem.When);
				}
				flag = writer.WriteLineCommaIfNeeded(flag);
				whenItem.WriteCreateScript(writer);
			}
			writer.Unindent();
			if (this.ElseExpression != null)
			{
				writer.Indent();
				writer.WriteLine(SqlLanguageSymbols.CommaSqlString);
				SqlConstant.NumericTrue.WriteCreateScript(writer);
				writer.WriteSpaceAfter(SqlLanguageSymbols.CommaSqlString);
				writer.WriteSubexpression(this.Precedence, (this.CaseExpression == null) ? this.ElseExpression : new BinaryLogicalOperation(this.ElseExpression, BinaryLogicalOperator.Equals, this.CaseExpression));
				writer.WriteLine();
				writer.Unindent();
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			this.CaseExpression = null;
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x001A0C8C File Offset: 0x0019EE8C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (this.whenItems.Count < 1)
			{
				throw new InvalidOperationException();
			}
			if (writer.Settings.SupportsCaseExpression)
			{
				this.WriteCreateCaseScript(writer);
				return;
			}
			this.WriteCreateSwitchScript(writer);
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x001A0CC0 File Offset: 0x0019EEC0
		private void WriteCreateCaseScript(ScriptWriter writer)
		{
			writer.Write(SqlLanguageStrings.CaseSqlString);
			if (this.CaseExpression != null)
			{
				writer.WriteSpace();
				writer.WriteSubexpression(this.Precedence, this.CaseExpression);
			}
			writer.WriteLine();
			writer.Indent();
			foreach (WhenItem whenItem in this.whenItems)
			{
				whenItem.WriteCreateScript(writer);
			}
			writer.Unindent();
			if (this.ElseExpression != null)
			{
				writer.Indent();
				writer.WriteSpaceAfter(SqlLanguageStrings.ElseSqlString);
				writer.WriteSubexpression(this.Precedence, this.ElseExpression);
				writer.WriteLine();
				writer.Unindent();
			}
			writer.Write(SqlLanguageStrings.EndSqlString);
		}

		// Token: 0x04004158 RID: 16728
		private readonly List<WhenItem> whenItems = new List<WhenItem>();
	}
}
