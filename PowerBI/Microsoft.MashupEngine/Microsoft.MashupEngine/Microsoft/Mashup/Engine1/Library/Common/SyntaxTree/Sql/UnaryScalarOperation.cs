using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001223 RID: 4643
	internal sealed class UnaryScalarOperation : ScalarExpression
	{
		// Token: 0x06007AB3 RID: 31411 RVA: 0x001A78A1 File Offset: 0x001A5AA1
		public UnaryScalarOperation(UnaryScalarOperator op, SqlExpression operand)
		{
			this.Operator = op;
			this.Operand = operand;
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06007AB4 RID: 31412 RVA: 0x001A78B7 File Offset: 0x001A5AB7
		// (set) Token: 0x06007AB5 RID: 31413 RVA: 0x001A78BF File Offset: 0x001A5ABF
		public SqlExpression Operand { get; private set; }

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x06007AB6 RID: 31414 RVA: 0x001A78C8 File Offset: 0x001A5AC8
		// (set) Token: 0x06007AB7 RID: 31415 RVA: 0x001A78D0 File Offset: 0x001A5AD0
		public UnaryScalarOperator Operator { get; private set; }

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x06007AB8 RID: 31416 RVA: 0x001A78DC File Offset: 0x001A5ADC
		private ConstantSqlString OperatorText
		{
			get
			{
				UnaryScalarOperator @operator = this.Operator;
				if (@operator == UnaryScalarOperator.Negative)
				{
					return SqlLanguageSymbols.SubtractSqlString;
				}
				if (@operator == UnaryScalarOperator.Positive)
				{
					return SqlLanguageSymbols.AddSqlString;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x06007AB9 RID: 31417 RVA: 0x001A7914 File Offset: 0x001A5B14
		public override int Precedence
		{
			get
			{
				UnaryScalarOperator @operator = this.Operator;
				if (@operator <= UnaryScalarOperator.Positive)
				{
					return 3;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x001A793D File Offset: 0x001A5B3D
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(this.OperatorText);
			writer.WriteSubexpression(this.Precedence, this.Operand);
		}
	}
}
