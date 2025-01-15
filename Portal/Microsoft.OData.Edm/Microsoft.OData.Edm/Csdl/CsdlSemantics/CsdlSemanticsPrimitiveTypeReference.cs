using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A9 RID: 425
	internal class CsdlSemanticsPrimitiveTypeReference : CsdlSemanticsElement, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x00020F93 File Offset: 0x0001F193
		public CsdlSemanticsPrimitiveTypeReference(CsdlSemanticsSchema schema, CsdlPrimitiveTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.Reference = reference;
			this.definition = EdmCoreModel.Instance.GetPrimitiveType(this.Reference.Kind);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00020FC5 File Offset: 0x0001F1C5
		public bool IsNullable
		{
			get
			{
				return this.Reference.IsNullable;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00020FD2 File Offset: 0x0001F1D2
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00020FDA File Offset: 0x0001F1DA
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00020FE7 File Offset: 0x0001F1E7
		public override CsdlElement Element
		{
			get
			{
				return this.Reference;
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0000C065 File Offset: 0x0000A265
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x040006F2 RID: 1778
		internal readonly CsdlPrimitiveTypeReference Reference;

		// Token: 0x040006F3 RID: 1779
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040006F4 RID: 1780
		private readonly IEdmPrimitiveType definition;
	}
}
