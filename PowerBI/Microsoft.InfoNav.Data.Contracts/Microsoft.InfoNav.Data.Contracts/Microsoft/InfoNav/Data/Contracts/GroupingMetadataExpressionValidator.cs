using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200008E RID: 142
	internal sealed class GroupingMetadataExpressionValidator : DefaultQueryExpressionVisitor<bool>
	{
		// Token: 0x0600033B RID: 827 RVA: 0x000093B4 File Offset: 0x000075B4
		private GroupingMetadataExpressionValidator(bool shouldThrow)
		{
			this.shouldThrow = shouldThrow;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000093C3 File Offset: 0x000075C3
		public static bool Validate(QueryExpressionContainer c)
		{
			return GroupingMetadataExpressionValidator.ThrowingInstance.VisitExpression(c);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000093D0 File Offset: 0x000075D0
		public bool Validate(bool condition, string errorMessage, QueryExpression expression = null)
		{
			if (condition || !this.shouldThrow)
			{
				return condition;
			}
			if (expression != null)
			{
				throw new GroupingMetadataExpressionValidator.ValidationException("{0}: {1}", new object[]
				{
					errorMessage,
					expression.ToTraceString()
				});
			}
			throw new GroupingMetadataExpressionValidator.ValidationException(errorMessage, new object[0]);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000941D File Offset: 0x0000761D
		public override bool VisitExpression(QueryExpressionContainer container)
		{
			return this.Validate(container.Expression != null, "Empty container expression", null) && container.Expression.Accept<bool>(this);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009447 File Offset: 0x00007647
		protected internal override bool VisitUnhandledExpression(QueryExpression expression)
		{
			return this.Validate(false, "Unhandled Expression type", expression);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00009456 File Offset: 0x00007656
		protected internal override bool Visit(QuerySourceRefExpression expression)
		{
			return this.Validate(expression.Entity != null && expression.Source == null, "Enitity should exist", expression);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009478 File Offset: 0x00007678
		protected internal override bool Visit(QueryColumnExpression expression)
		{
			return this.Validate(expression.Expression != null, "Empty column inner expression", expression) && this.VisitExpression(expression.Expression) && this.Validate(!string.IsNullOrEmpty(expression.Property), "Empty column name", expression) && this.Validate(expression.Expression.SourceRef != null, "Columns in GroupingMetadata must be based on Entities", expression);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000094E8 File Offset: 0x000076E8
		protected internal override bool Visit(QueryHierarchyExpression expression)
		{
			return this.Validate(expression.Expression != null, "Empty hierarchy inner expression", expression) && this.VisitExpression(expression.Expression) && this.Validate(!string.IsNullOrEmpty(expression.Hierarchy), "Empty hierarchy name", expression) && this.Validate(expression.Expression.SourceRef != null, "Hierarchies in GroupingMetadata must be based on Entities", expression);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009558 File Offset: 0x00007758
		protected internal override bool Visit(QueryHierarchyLevelExpression expression)
		{
			return this.Validate(expression.Expression != null, "Empty hierarchy level inner expression", expression) && this.Validate(expression.Expression.Hierarchy != null, "Invalid hierarchy level inner expression type", expression) && this.VisitExpression(expression.Expression) && this.Validate(!string.IsNullOrEmpty(expression.Level), "Empty hierarchy level name", expression);
		}

		// Token: 0x040001C4 RID: 452
		internal static readonly GroupingMetadataExpressionValidator Instance = new GroupingMetadataExpressionValidator(false);

		// Token: 0x040001C5 RID: 453
		private static readonly GroupingMetadataExpressionValidator ThrowingInstance = new GroupingMetadataExpressionValidator(true);

		// Token: 0x040001C6 RID: 454
		private readonly bool shouldThrow;

		// Token: 0x02000303 RID: 771
		private sealed class ValidationException : Exception
		{
			// Token: 0x0600194A RID: 6474 RVA: 0x0002D967 File Offset: 0x0002BB67
			internal ValidationException(string message, params object[] args)
				: base(StringUtil.FormatInvariant(message, args))
			{
			}
		}
	}
}
