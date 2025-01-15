using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000129 RID: 297
	internal sealed class LambdaBinder
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x000294CC File Offset: 0x000276CC
		internal LambdaBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000294E8 File Offset: 0x000276E8
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

		// Token: 0x06001003 RID: 4099 RVA: 0x0002956C File Offset: 0x0002776C
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

		// Token: 0x06001004 RID: 4100 RVA: 0x000295B8 File Offset: 0x000277B8
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

		// Token: 0x040007A7 RID: 1959
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
