using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011BB RID: 4539
	internal sealed class BinaryQueryOperation : SqlQueryExpression
	{
		// Token: 0x06007808 RID: 30728 RVA: 0x001A091E File Offset: 0x0019EB1E
		public BinaryQueryOperation(SqlQueryExpression left, BinaryQueryOperator op, SqlQueryExpression right)
		{
			this.left = left;
			this.@operator = op;
			this.right = right;
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06007809 RID: 30729 RVA: 0x001A093B File Offset: 0x0019EB3B
		public SqlQueryExpression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x0600780A RID: 30730 RVA: 0x001A0943 File Offset: 0x0019EB43
		public SqlQueryExpression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x0600780B RID: 30731 RVA: 0x001422C0 File Offset: 0x001404C0
		public override int Precedence
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x001A094C File Offset: 0x0019EB4C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			switch (this.@operator)
			{
			case BinaryQueryOperator.Union:
				this.WriteBinaryOperator(writer, SqlLanguageStrings.UnionSqlString);
				return;
			case BinaryQueryOperator.Except:
				this.WriteBinaryOperator(writer, SqlLanguageStrings.ExceptSqlString);
				return;
			case BinaryQueryOperator.Intersect:
				this.WriteBinaryOperator(writer, SqlLanguageStrings.IntersectSqlString);
				return;
			case BinaryQueryOperator.UnionAll:
				this.WriteBinaryOperator(writer, SqlLanguageStrings.UnionAllSqlString);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x001A09AC File Offset: 0x0019EBAC
		private void WriteBinaryOperator(ScriptWriter writer, ConstantSqlString operatorText)
		{
			writer.WriteSubexpression(this.Precedence, this.left);
			writer.WriteLine();
			writer.WriteSpaceAfter(operatorText);
			writer.WriteSubexpression(this.Precedence, this.right);
		}

		// Token: 0x04004143 RID: 16707
		private readonly SqlQueryExpression left;

		// Token: 0x04004144 RID: 16708
		private readonly BinaryQueryOperator @operator;

		// Token: 0x04004145 RID: 16709
		private readonly SqlQueryExpression right;
	}
}
