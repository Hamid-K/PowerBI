using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011E RID: 286
	internal sealed class BinaryOperatorBinder
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x0002734A File Offset: 0x0002554A
		internal BinaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod, ODataUriResolver resolver)
		{
			this.bindMethod = bindMethod;
			this.resolver = resolver;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00027360 File Offset: 0x00025560
		internal QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorToken>(binaryOperatorToken, "binaryOperatorToken");
			SingleValueNode operandFromToken = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Left);
			SingleValueNode operandFromToken2 = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Right);
			IEdmTypeReference edmTypeReference;
			this.resolver.PromoteBinaryOperandTypes(binaryOperatorToken.OperatorKind, ref operandFromToken, ref operandFromToken2, out edmTypeReference);
			return new BinaryOperatorNode(binaryOperatorToken.OperatorKind, operandFromToken, operandFromToken2, edmTypeReference);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000273C4 File Offset: 0x000255C4
		internal static void PromoteOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode left, ref SingleValueNode right, TypeFacetsPromotionRules facetsPromotionRules)
		{
			IEdmTypeReference edmTypeReference;
			IEdmTypeReference edmTypeReference2;
			if (!TypePromotionUtils.PromoteOperandTypes(binaryOperatorKind, left, right, out edmTypeReference, out edmTypeReference2, facetsPromotionRules))
			{
				string text = ((left.TypeReference == null) ? "<null>" : left.TypeReference.FullName());
				string text2 = ((right.TypeReference == null) ? "<null>" : right.TypeReference.FullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandsError(text, text2, binaryOperatorKind));
			}
			left = MetadataBindingUtils.ConvertToTypeIfNeeded(left, edmTypeReference);
			right = MetadataBindingUtils.ConvertToTypeIfNeeded(right, edmTypeReference2);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00027444 File Offset: 0x00025644
		private SingleValueNode GetOperandFromToken(BinaryOperatorKind operatorKind, QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BinaryOperatorOperandNotSingleValue(operatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x04000795 RID: 1941
		private readonly Func<QueryToken, QueryNode> bindMethod;

		// Token: 0x04000796 RID: 1942
		private readonly ODataUriResolver resolver;
	}
}
