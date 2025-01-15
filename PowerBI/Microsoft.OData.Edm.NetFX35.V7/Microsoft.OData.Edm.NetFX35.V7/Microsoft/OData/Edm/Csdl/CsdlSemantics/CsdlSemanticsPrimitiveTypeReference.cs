using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000198 RID: 408
	internal class CsdlSemanticsPrimitiveTypeReference : CsdlSemanticsElement, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0001E7FB File Offset: 0x0001C9FB
		public CsdlSemanticsPrimitiveTypeReference(CsdlSemanticsSchema schema, CsdlPrimitiveTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.Reference = reference;
			this.definition = EdmCoreModel.Instance.GetPrimitiveType(this.Reference.Kind);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001E82D File Offset: 0x0001CA2D
		public bool IsNullable
		{
			get
			{
				return this.Reference.IsNullable;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001E83A File Offset: 0x0001CA3A
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0001E842 File Offset: 0x0001CA42
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0001E84F File Offset: 0x0001CA4F
		public override CsdlElement Element
		{
			get
			{
				return this.Reference;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000C768 File Offset: 0x0000A968
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x04000667 RID: 1639
		internal readonly CsdlPrimitiveTypeReference Reference;

		// Token: 0x04000668 RID: 1640
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000669 RID: 1641
		private readonly IEdmPrimitiveType definition;
	}
}
