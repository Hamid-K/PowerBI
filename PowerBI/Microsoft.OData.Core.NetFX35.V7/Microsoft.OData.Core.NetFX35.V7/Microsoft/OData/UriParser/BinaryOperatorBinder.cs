using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000DF RID: 223
	internal sealed class BinaryOperatorBinder
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0001BBDA File Offset: 0x00019DDA
		internal BinaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod, ODataUriResolver resolver)
		{
			this.bindMethod = bindMethod;
			this.resolver = resolver;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		internal QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorToken>(binaryOperatorToken, "binaryOperatorToken");
			SingleValueNode operandFromToken = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Left);
			SingleValueNode operandFromToken2 = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Right);
			IEdmTypeReference edmTypeReference;
			this.resolver.PromoteBinaryOperandTypes(binaryOperatorToken.OperatorKind, ref operandFromToken, ref operandFromToken2, out edmTypeReference);
			return new BinaryOperatorNode(binaryOperatorToken.OperatorKind, operandFromToken, operandFromToken2, edmTypeReference);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001BC54 File Offset: 0x00019E54
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

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		private SingleValueNode GetOperandFromToken(BinaryOperatorKind operatorKind, QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod.Invoke(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BinaryOperatorOperandNotSingleValue(operatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x04000683 RID: 1667
		private readonly Func<QueryToken, QueryNode> bindMethod;

		// Token: 0x04000684 RID: 1668
		private readonly ODataUriResolver resolver;
	}
}
