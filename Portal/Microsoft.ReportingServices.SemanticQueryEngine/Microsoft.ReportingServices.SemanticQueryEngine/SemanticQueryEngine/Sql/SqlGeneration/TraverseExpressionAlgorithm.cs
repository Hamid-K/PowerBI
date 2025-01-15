using System;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000033 RID: 51
	internal abstract class TraverseExpressionAlgorithm<T>
	{
		// Token: 0x06000237 RID: 567 RVA: 0x0000AF60 File Offset: 0x00009160
		internal T Traverse(Expression expression)
		{
			if (expression.NodeAsLiteral != null)
			{
				return this.LiteralVisitor(expression.NodeAsLiteral);
			}
			if (expression.NodeAsAttributeRef != null)
			{
				AttributeRefNode nodeAsAttributeRef = expression.NodeAsAttributeRef;
				if (nodeAsAttributeRef.Attribute.ModelAttribute == null || nodeAsAttributeRef.Attribute.ModelAttribute.Binding == null || nodeAsAttributeRef.Attribute.ModelAttribute.Binding.GetColumn() == null)
				{
					throw SQEAssert.AssertFalseAndThrow("attrRefNode must point to a model attribute with valid binding.", Array.Empty<object>());
				}
				return this.AttributeRefVisitor(nodeAsAttributeRef.Attribute.ModelAttribute.Binding.GetColumn());
			}
			else
			{
				if (expression.NodeAsEntityRef != null)
				{
					Type[] entityKeyPartTypes = QueryPlanBuilder.GetEntityKeyPartTypes(expression.NodeAsEntityRef.Entity);
					return this.EntityRefVisitor(entityKeyPartTypes);
				}
				if (expression.NodeAsFunction != null)
				{
					return this.FunctionVisitor(expression.NodeAsFunction);
				}
				throw SQEAssert.AssertFalseAndThrow("Unexpected expression node type.", Array.Empty<object>());
			}
		}

		// Token: 0x06000238 RID: 568
		protected abstract T LiteralVisitor(LiteralNode literalNode);

		// Token: 0x06000239 RID: 569
		protected abstract T AttributeRefVisitor(DsvColumn dsvColumn);

		// Token: 0x0600023A RID: 570
		protected abstract T EntityRefVisitor(Type[] keyPartTypes);

		// Token: 0x0600023B RID: 571
		protected abstract T FunctionVisitor(FunctionNode functionNode);
	}
}
