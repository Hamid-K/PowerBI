using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011B9 RID: 4537
	internal sealed class BinaryLogicalOperation : Condition
	{
		// Token: 0x060077FE RID: 30718 RVA: 0x001A07E9 File Offset: 0x0019E9E9
		public BinaryLogicalOperation(SqlExpression left, BinaryLogicalOperator op, SqlExpression right)
		{
			this.Left = left;
			this.Operator = op;
			this.Right = right;
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x060077FF RID: 30719 RVA: 0x001A0806 File Offset: 0x0019EA06
		// (set) Token: 0x06007800 RID: 30720 RVA: 0x001A080E File Offset: 0x0019EA0E
		public SqlExpression Left { get; private set; }

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x06007801 RID: 30721 RVA: 0x001A0817 File Offset: 0x0019EA17
		// (set) Token: 0x06007802 RID: 30722 RVA: 0x001A081F File Offset: 0x0019EA1F
		public BinaryLogicalOperator Operator { get; private set; }

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x06007803 RID: 30723 RVA: 0x001A0828 File Offset: 0x0019EA28
		private ConstantSqlString OperatorText
		{
			get
			{
				switch (this.Operator)
				{
				case BinaryLogicalOperator.Equals:
					return SqlLanguageSymbols.EqualsSqlString;
				case BinaryLogicalOperator.GreaterThan:
					return SqlLanguageSymbols.GreaterThanSqlString;
				case BinaryLogicalOperator.LessThan:
					return SqlLanguageSymbols.LessThanSqlString;
				case BinaryLogicalOperator.GreaterThanOrEqual:
					return SqlLanguageSymbols.GreaterThanOrEqualSqlString;
				case BinaryLogicalOperator.LessThanOrEqual:
					return SqlLanguageSymbols.LessThanOrEqualSqlString;
				case BinaryLogicalOperator.NotEqualTo:
					return SqlLanguageSymbols.NotEqualToSqlString;
				case BinaryLogicalOperator.In:
					return SqlLanguageStrings.InSqlString;
				case BinaryLogicalOperator.NotIn:
					return SqlLanguageStrings.NotInSqlString;
				case BinaryLogicalOperator.Like:
					return SqlLanguageStrings.LikeSqlString;
				case BinaryLogicalOperator.NotLike:
					return SqlLanguageStrings.NotLikeSqlString;
				default:
					throw new InvalidOperationException();
				}
			}
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06007804 RID: 30724 RVA: 0x001A08B0 File Offset: 0x0019EAB0
		public override int Precedence
		{
			get
			{
				BinaryLogicalOperator @operator = this.Operator;
				if (@operator - BinaryLogicalOperator.Equals <= 5)
				{
					return 4;
				}
				if (@operator - BinaryLogicalOperator.In > 3)
				{
					throw new InvalidOperationException();
				}
				return 8;
			}
		}

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06007805 RID: 30725 RVA: 0x001A08DB File Offset: 0x0019EADB
		// (set) Token: 0x06007806 RID: 30726 RVA: 0x001A08E3 File Offset: 0x0019EAE3
		public SqlExpression Right { get; private set; }

		// Token: 0x06007807 RID: 30727 RVA: 0x001A08EC File Offset: 0x0019EAEC
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(this.Precedence, this.Left);
			writer.WriteSpaceBeforeAndAfter(this.OperatorText);
			writer.WriteSubexpression(this.Precedence, this.Right);
		}
	}
}
