using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000137 RID: 311
	internal sealed class UnaryOperatorBinder
	{
		// Token: 0x06001055 RID: 4181 RVA: 0x0002BA08 File Offset: 0x00029C08
		internal UnaryOperatorBinder(Func<QueryToken, QueryNode> bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002BA18 File Offset: 0x00029C18
		internal QueryNode BindUnaryOperator(UnaryOperatorToken unaryOperatorToken)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorToken>(unaryOperatorToken, "unaryOperatorToken");
			SingleValueNode singleValueNode = this.GetOperandFromToken(unaryOperatorToken);
			IEdmTypeReference edmTypeReference = UnaryOperatorBinder.PromoteOperandType(singleValueNode, unaryOperatorToken.OperatorKind);
			singleValueNode = MetadataBindingUtils.ConvertToTypeIfNeeded(singleValueNode, edmTypeReference);
			return new UnaryOperatorNode(unaryOperatorToken.OperatorKind, singleValueNode);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002BA5C File Offset: 0x00029C5C
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

		// Token: 0x06001058 RID: 4184 RVA: 0x0002BAA8 File Offset: 0x00029CA8
		private SingleValueNode GetOperandFromToken(UnaryOperatorToken unaryOperatorToken)
		{
			SingleValueNode singleValueNode = this.bindMethod(unaryOperatorToken.Operand) as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException(Strings.MetadataBinder_UnaryOperatorOperandNotSingleValue(unaryOperatorToken.OperatorKind.ToString()));
			}
			return singleValueNode;
		}

		// Token: 0x040007B1 RID: 1969
		private readonly Func<QueryToken, QueryNode> bindMethod;
	}
}
