using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A1 RID: 417
	internal class CsdlSemanticsUntypedTypeReference : CsdlSemanticsElement, IEdmUntypedTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x0001F9EB File Offset: 0x0001DBEB
		public CsdlSemanticsUntypedTypeReference(CsdlSemanticsSchema schema, CsdlUntypedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.Reference = reference;
			this.definition = EdmCoreModel.Instance.GetUntypedType();
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001FA12 File Offset: 0x0001DC12
		public bool IsNullable
		{
			get
			{
				return this.Reference.IsNullable;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001FA1F File Offset: 0x0001DC1F
		public IEdmType Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001FA27 File Offset: 0x0001DC27
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001FA34 File Offset: 0x0001DC34
		public override CsdlElement Element
		{
			get
			{
				return this.Reference;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0000C768 File Offset: 0x0000A968
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x0400068F RID: 1679
		internal readonly CsdlUntypedTypeReference Reference;

		// Token: 0x04000690 RID: 1680
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000691 RID: 1681
		private readonly IEdmUntypedType definition;
	}
}
