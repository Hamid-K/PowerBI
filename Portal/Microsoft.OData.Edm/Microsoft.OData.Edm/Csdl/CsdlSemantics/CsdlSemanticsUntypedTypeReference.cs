using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000161 RID: 353
	internal class CsdlSemanticsUntypedTypeReference : CsdlSemanticsElement, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x0001B36B File Offset: 0x0001956B
		public CsdlSemanticsUntypedTypeReference(CsdlSemanticsSchema schema, CsdlUntypedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.Reference = reference;
			this.definition = EdmCoreModel.Instance.GetUntypedType();
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001B392 File Offset: 0x00019592
		public bool IsNullable
		{
			get
			{
				return this.Reference.IsNullable;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001B39F File Offset: 0x0001959F
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001B3A7 File Offset: 0x000195A7
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public override CsdlElement Element
		{
			get
			{
				return this.Reference;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000C065 File Offset: 0x0000A265
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x040005DC RID: 1500
		internal readonly CsdlUntypedTypeReference Reference;

		// Token: 0x040005DD RID: 1501
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040005DE RID: 1502
		private readonly IEdmUntypedType definition;
	}
}
