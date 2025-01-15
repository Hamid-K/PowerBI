using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C4 RID: 4548
	internal sealed class ConditionOperation : Condition
	{
		// Token: 0x06007834 RID: 30772 RVA: 0x001A0EB6 File Offset: 0x0019F0B6
		internal ConditionOperation(ConditionOperator op, Condition operand)
		{
			this.Operator = op;
			this.Left = operand;
		}

		// Token: 0x06007835 RID: 30773 RVA: 0x001A0ECC File Offset: 0x0019F0CC
		internal ConditionOperation(Condition left, ConditionOperator op, Condition right)
		{
			this.Left = left;
			this.Operator = op;
			this.Right = right;
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x001A0EE9 File Offset: 0x0019F0E9
		// (set) Token: 0x06007837 RID: 30775 RVA: 0x001A0EF1 File Offset: 0x0019F0F1
		internal ConditionOperator Operator { get; private set; }

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06007838 RID: 30776 RVA: 0x001A0EFA File Offset: 0x0019F0FA
		// (set) Token: 0x06007839 RID: 30777 RVA: 0x001A0F02 File Offset: 0x0019F102
		private Condition Left { get; set; }

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x0600783A RID: 30778 RVA: 0x001A0F0B File Offset: 0x0019F10B
		// (set) Token: 0x0600783B RID: 30779 RVA: 0x001A0F13 File Offset: 0x0019F113
		private Condition Right { get; set; }

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x0600783C RID: 30780 RVA: 0x001A0F1C File Offset: 0x0019F11C
		public override int Precedence
		{
			get
			{
				switch (this.Operator)
				{
				case ConditionOperator.And:
					return 7;
				case ConditionOperator.Or:
					return 8;
				case ConditionOperator.Not:
					return 6;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			}
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x001A0F5C File Offset: 0x0019F15C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			switch (this.Operator)
			{
			case ConditionOperator.And:
				writer.WriteSubexpression(this.Precedence, this.Left);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AndSqlString);
				writer.WriteSubexpression(this.Precedence, this.Right);
				return;
			case ConditionOperator.Or:
				writer.WriteSubexpression(this.Precedence, this.Left);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.OrSqlString);
				writer.WriteSubexpression(this.Precedence, this.Right);
				return;
			case ConditionOperator.Not:
				writer.Write(SqlLanguageStrings.NotSqlString);
				writer.WriteSpace();
				writer.WriteSubexpression(this.Precedence, this.Left);
				return;
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}
	}
}
