using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000016 RID: 22
	public sealed class BinaryOperatorQueryNode : SingleValueQueryNode
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003628 File Offset: 0x00001828
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003630 File Offset: 0x00001830
		public BinaryOperatorKind OperatorKind { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003639 File Offset: 0x00001839
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003641 File Offset: 0x00001841
		public SingleValueQueryNode Left { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000364A File Offset: 0x0000184A
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003652 File Offset: 0x00001852
		public SingleValueQueryNode Right { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000365C File Offset: 0x0000185C
		public override IEdmTypeReference TypeReference
		{
			get
			{
				if (this.Left == null || this.Right == null || this.Left.TypeReference == null || this.Right.TypeReference == null)
				{
					return null;
				}
				if (!this.Left.TypeReference.IsODataPrimitiveTypeKind() || !this.Right.TypeReference.IsODataPrimitiveTypeKind())
				{
					throw new ODataException(Strings.BinaryOperatorQueryNode_InvalidOperandType(this.Left.TypeReference.ODataFullName(), this.Right.TypeReference.ODataFullName()));
				}
				if (!this.Left.TypeReference.Definition.IsEquivalentTo(this.Right.TypeReference.Definition))
				{
					throw new ODataException(Strings.BinaryOperatorQueryNode_OperandsMustHaveSameTypes(this.Left.TypeReference.ODataFullName(), this.Right.TypeReference.ODataFullName()));
				}
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = this.Left.TypeReference.AsPrimitive();
				return QueryNodeUtils.GetBinaryOperatorResultType(edmPrimitiveTypeReference, this.OperatorKind);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003753 File Offset: 0x00001953
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.BinaryOperator;
			}
		}
	}
}
