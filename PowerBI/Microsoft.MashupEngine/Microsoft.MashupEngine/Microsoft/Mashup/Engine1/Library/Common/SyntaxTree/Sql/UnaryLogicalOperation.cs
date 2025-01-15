using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001221 RID: 4641
	internal sealed class UnaryLogicalOperation : Condition
	{
		// Token: 0x06007AAB RID: 31403 RVA: 0x001A7785 File Offset: 0x001A5985
		internal UnaryLogicalOperation(UnaryLogicalOperator op, SqlExpression operand)
		{
			this.Operator = op;
			this.Operand = operand;
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06007AAC RID: 31404 RVA: 0x001A779B File Offset: 0x001A599B
		// (set) Token: 0x06007AAD RID: 31405 RVA: 0x001A77A3 File Offset: 0x001A59A3
		public SqlExpression Operand { get; private set; }

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06007AAE RID: 31406 RVA: 0x001A77AC File Offset: 0x001A59AC
		// (set) Token: 0x06007AAF RID: 31407 RVA: 0x001A77B4 File Offset: 0x001A59B4
		public UnaryLogicalOperator Operator { get; private set; }

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06007AB0 RID: 31408 RVA: 0x001A77C0 File Offset: 0x001A59C0
		private ConstantSqlString OperatorText
		{
			get
			{
				switch (this.Operator)
				{
				case UnaryLogicalOperator.Exists:
					return SqlLanguageStrings.ExistsSqlString;
				case UnaryLogicalOperator.NotExists:
					return SqlLanguageStrings.NotExistsSqlString;
				case UnaryLogicalOperator.IsNull:
					return SqlLanguageStrings.IsNullSqlString;
				case UnaryLogicalOperator.IsNotNull:
					return SqlLanguageStrings.IsNotNullSqlString;
				default:
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06007AB1 RID: 31409 RVA: 0x001A780C File Offset: 0x001A5A0C
		public override int Precedence
		{
			get
			{
				UnaryLogicalOperator @operator = this.Operator;
				if (@operator - UnaryLogicalOperator.Exists <= 1)
				{
					return 8;
				}
				if (@operator - UnaryLogicalOperator.IsNull <= 1)
				{
					return 4;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007AB2 RID: 31410 RVA: 0x001A7838 File Offset: 0x001A5A38
		public override void WriteCreateScript(ScriptWriter writer)
		{
			UnaryLogicalOperator @operator = this.Operator;
			if (@operator - UnaryLogicalOperator.Exists > 1)
			{
				if (@operator - UnaryLogicalOperator.IsNull <= 1)
				{
					writer.WriteSubexpression(this.Precedence, this.Operand);
					writer.WriteSpace();
					writer.Write(this.OperatorText);
					return;
				}
			}
			else
			{
				writer.Write(this.OperatorText);
				writer.WriteSpace();
				writer.WriteSubexpression(this.Precedence, this.Operand);
			}
		}
	}
}
