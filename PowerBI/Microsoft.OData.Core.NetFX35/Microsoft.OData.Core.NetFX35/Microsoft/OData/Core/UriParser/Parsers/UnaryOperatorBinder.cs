using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001DE RID: 478
	internal sealed class UnaryOperatorBinder
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x0003E44D File Offset: 0x0003C64D
		internal UnaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003E45C File Offset: 0x0003C65C
		internal QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorToken>(unaryOperatorToken, "unaryOperatorToken");
			SingleValueNode singleValueNode = this.GetOperandFromToken(unaryOperatorToken);
			IEdmTypeReference edmTypeReference = UnaryOperatorBinder.PromoteOperandType(singleValueNode, unaryOperatorToken.OperatorKind);
			singleValueNode = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			return new UnaryOperatorNode(unaryOperatorToken.OperatorKind, singleValueNode);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
		private static IEdmTypeReference PromoteOperandType(SingleValueNode operand, UnaryOperatorKind unaryOperatorKind)
		{
			IEdmTypeReference typeReference = operand.TypeReference;
			if (!TypePromotionUtils.PromoteOperandType(unaryOperatorKind, ref typeReference))
			{
				string text = ((operand.TypeReference == null) ? "<null>" : operand.TypeReference.FullName());
				throw new ODataException(Strings.MetadataBinder_IncompatibleOperandError(text, unaryOperatorKind));
			}
			return typeReference;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		private SingleValueNode GetOperandFromToken(UnaryOperatorToken unaryOperatorToken)
		{
			SingleValueNode singleValueNode = this.bindMethod.Invoke(unaryOperatorToken.Operand) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_UnaryOperatorOperandNotSingleValue(unaryOperatorToken.OperatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x04000797 RID: 1943
		private readonly Func<QueryToken, QueryNode> bindMethod;
	}
}
