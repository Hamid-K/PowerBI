using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000EB RID: 235
	internal sealed class LambdaBinder
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		internal LambdaBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		internal LambdaNode BindLambdaToken(LambdaToken lambdaToken, BindingState state)
		{
			ExceptionUtils.CheckArgumentNotNull<LambdaToken>(lambdaToken, "LambdaToken");
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			CollectionNode collectionNode = this.BindParentToken(lambdaToken.Parent);
			RangeVariable rangeVariable = null;
			if (lambdaToken.Parameter != null)
			{
				rangeVariable = NodeFactory.CreateParameterNode(lambdaToken.Parameter, collectionNode);
				state.RangeVariables.Push(rangeVariable);
			}
			SingleValueNode singleValueNode = this.BindExpressionToken(lambdaToken.Expression);
			LambdaNode lambdaNode = NodeFactory.CreateLambdaNode(state, collectionNode, singleValueNode, rangeVariable, lambdaToken.Kind);
			if (rangeVariable != null)
			{
				state.RangeVariables.Pop();
			}
			return lambdaNode;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001DC90 File Offset: 0x0001BE90
		private CollectionNode BindParentToken(QueryToken queryToken)
		{
			QueryNode queryNode = this.bindMethod(queryToken);
			CollectionNode collectionNode = queryNode as CollectionNode;
			if (collectionNode != null)
			{
				return collectionNode;
			}
			SingleValueOpenPropertyAccessNode singleValueOpenPropertyAccessNode = queryNode as SingleValueOpenPropertyAccessNode;
			if (singleValueOpenPropertyAccessNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_LambdaParentMustBeCollection);
			}
			return new CollectionOpenPropertyAccessNode(singleValueOpenPropertyAccessNode.Source, singleValueOpenPropertyAccessNode.Name);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		private SingleValueNode BindExpressionToken(QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_AnyAllExpressionNotSingleValue);
			}
			IEdmTypeReference edmTypeReference = singleValueNode.GetEdmTypeReference();
			if (edmTypeReference != null && !edmTypeReference.AsPrimitive().IsBoolean())
			{
				throw new ODataException(Strings.MetadataBinder_AnyAllExpressionNotSingleValue);
			}
			return singleValueNode;
		}

		// Token: 0x04000694 RID: 1684
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
