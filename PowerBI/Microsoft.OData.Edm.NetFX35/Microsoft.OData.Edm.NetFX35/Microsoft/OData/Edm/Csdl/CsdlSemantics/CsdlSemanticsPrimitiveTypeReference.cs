using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000BB RID: 187
	internal class CsdlSemanticsPrimitiveTypeReference : CsdlSemanticsElement, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000742C File Offset: 0x0000562C
		public CsdlSemanticsPrimitiveTypeReference(CsdlSemanticsSchema schema, CsdlPrimitiveTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.Reference = reference;
			this.definition = EdmCoreModel.Instance.GetPrimitiveType(this.Reference.Kind);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000745E File Offset: 0x0000565E
		public bool IsNullable
		{
			get
			{
				return this.Reference.IsNullable;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000746B File Offset: 0x0000566B
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00007473 File Offset: 0x00005673
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00007480 File Offset: 0x00005680
		public override CsdlElement Element
		{
			get
			{
				return this.Reference;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007488 File Offset: 0x00005688
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x04000153 RID: 339
		internal readonly CsdlPrimitiveTypeReference Reference;

		// Token: 0x04000154 RID: 340
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000155 RID: 341
		private readonly IEdmPrimitiveType definition;
	}
}
