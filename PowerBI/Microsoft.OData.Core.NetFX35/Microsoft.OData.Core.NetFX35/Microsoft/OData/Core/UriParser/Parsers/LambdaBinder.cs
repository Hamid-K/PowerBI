using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001CD RID: 461
	internal sealed class LambdaBinder
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x0003CB70 File Offset: 0x0003AD70
		internal LambdaBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003CB8C File Offset: 0x0003AD8C
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

		// Token: 0x06001131 RID: 4401 RVA: 0x0003CC0C File Offset: 0x0003AE0C
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

		// Token: 0x06001132 RID: 4402 RVA: 0x0003CC58 File Offset: 0x0003AE58
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

		// Token: 0x04000787 RID: 1927
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
