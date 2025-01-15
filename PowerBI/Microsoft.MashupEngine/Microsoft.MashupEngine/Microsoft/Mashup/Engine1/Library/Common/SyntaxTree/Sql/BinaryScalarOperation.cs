using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011BD RID: 4541
	internal sealed class BinaryScalarOperation : ScalarExpression
	{
		// Token: 0x0600780E RID: 30734 RVA: 0x001A09DF File Offset: 0x0019EBDF
		public BinaryScalarOperation(SqlExpression left, BinaryScalarOperator op, SqlExpression right)
		{
			this.Left = left;
			this.Operator = op;
			this.Right = right;
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x0600780F RID: 30735 RVA: 0x001A09FC File Offset: 0x0019EBFC
		// (set) Token: 0x06007810 RID: 30736 RVA: 0x001A0A04 File Offset: 0x0019EC04
		public SqlExpression Left { get; private set; }

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06007811 RID: 30737 RVA: 0x001A0A0D File Offset: 0x0019EC0D
		// (set) Token: 0x06007812 RID: 30738 RVA: 0x001A0A15 File Offset: 0x0019EC15
		public BinaryScalarOperator Operator { get; private set; }

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06007813 RID: 30739 RVA: 0x001A0A1E File Offset: 0x0019EC1E
		// (set) Token: 0x06007814 RID: 30740 RVA: 0x001A0A26 File Offset: 0x0019EC26
		public SqlExpression Right { get; private set; }

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06007815 RID: 30741 RVA: 0x001A0A30 File Offset: 0x0019EC30
		public override int Precedence
		{
			get
			{
				switch (this.Operator)
				{
				case BinaryScalarOperator.Add:
				case BinaryScalarOperator.Subtract:
					return 3;
				case BinaryScalarOperator.Concatenate:
					return 5;
				case BinaryScalarOperator.Multiply:
				case BinaryScalarOperator.Divide:
				case BinaryScalarOperator.Modulo:
				case BinaryScalarOperator.InlineModulo:
					return 2;
				case BinaryScalarOperator.Power:
				case BinaryScalarOperator.PowerAsteriskVariant:
					return 1;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06007816 RID: 30742 RVA: 0x001A0A88 File Offset: 0x0019EC88
		private ConstantSqlString OperatorText
		{
			get
			{
				switch (this.Operator)
				{
				case BinaryScalarOperator.Add:
					return SqlLanguageSymbols.AddSqlString;
				case BinaryScalarOperator.Concatenate:
					return SqlLanguageSymbols.StringConcatenateSqlString;
				case BinaryScalarOperator.Subtract:
					return SqlLanguageSymbols.SubtractSqlString;
				case BinaryScalarOperator.Multiply:
					return SqlLanguageSymbols.MultiplySqlString;
				case BinaryScalarOperator.Divide:
					return SqlLanguageSymbols.DivideSqlString;
				case BinaryScalarOperator.Modulo:
					return SqlLanguageSymbols.ModuloSqlString;
				case BinaryScalarOperator.Power:
					return SqlLanguageSymbols.PowerSqlString;
				case BinaryScalarOperator.InlineModulo:
					return SqlLanguageSymbols.InlineModSqlString;
				case BinaryScalarOperator.PowerAsteriskVariant:
					return SqlLanguageSymbols.PowerAsteriskVariantSqlString;
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			}
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x001A0B0D File Offset: 0x0019ED0D
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(this.Precedence, this.Left);
			writer.WriteSpaceBeforeAndAfter(this.OperatorText);
			writer.WriteSubexpression(this.Precedence, this.Right);
		}
	}
}
