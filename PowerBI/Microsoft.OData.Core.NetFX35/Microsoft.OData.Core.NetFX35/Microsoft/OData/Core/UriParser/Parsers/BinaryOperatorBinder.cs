using System;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C2 RID: 450
	internal sealed class BinaryOperatorBinder
	{
		// Token: 0x060010DE RID: 4318 RVA: 0x0003ACC6 File Offset: 0x00038EC6
		internal BinaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod, ODataUriResolver resolver)
		{
			this.bindMethod = bindMethod;
			this.resolver = resolver ?? ODataUriResolver.Default;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003ACE8 File Offset: 0x00038EE8
		internal QueryNode BindBinaryOperator(BinaryOperatorToken binaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorToken>(binaryOperatorToken, "binaryOperatorToken");
			SingleValueNode operandFromToken = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Left);
			SingleValueNode operandFromToken2 = this.GetOperandFromToken(binaryOperatorToken.OperatorKind, binaryOperatorToken.Right);
			IEdmTypeReference edmTypeReference;
			this.resolver.PromoteBinaryOperandTypes(binaryOperatorToken.OperatorKind, ref operandFromToken, ref operandFromToken2, out edmTypeReference);
			return new BinaryOperatorNode(binaryOperatorToken.OperatorKind, operandFromToken, operandFromToken2, edmTypeReference);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003AD4C File Offset: 0x00038F4C
		internal static void PromoteOperandTypes(BinaryOperatorKind binaryOperatorKind, ref SingleValueNode left, ref SingleValueNode right)
		{
			IEdmTypeReference edmTypeReference;
			IEdmTypeReference edmTypeReference2;
			if (!TypePromotionUtils.PromoteOperandTypes(binaryOperatorKind, left, right, out edmTypeReference, out edmTypeReference2))
			{
				string text = ((left.TypeReference == null) ? "<null>" : left.TypeReference.FullName());
				string text2 = ((right.TypeReference == null) ? "<null>" : right.TypeReference.FullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandsError(text, text2, binaryOperatorKind));
			}
			left = MetadataBindingUtils.ConvertToTypeIfNeeded(left, edmTypeReference);
			right = MetadataBindingUtils.ConvertToTypeIfNeeded(right, edmTypeReference2);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003ADCC File Offset: 0x00038FCC
		private SingleValueNode GetOperandFromToken(BinaryOperatorKind operatorKind, QueryToken queryToken)
		{
			SingleValueNode singleValueNode = this.bindMethod.Invoke(queryToken) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_BinaryOperatorOperandNotSingleValue(operatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x04000774 RID: 1908
		private readonly Func<QueryToken, QueryNode> bindMethod;

		// Token: 0x04000775 RID: 1909
		private readonly ODataUriResolver resolver;
	}
}
