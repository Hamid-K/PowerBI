using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000178 RID: 376
	internal class CsdlSemanticsNamedTypeReference : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x0001B200 File Offset: 0x00019400
		public CsdlSemanticsNamedTypeReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.reference = reference;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001B222 File Offset: 0x00019422
		public IEdmType Definition
		{
			get
			{
				return this.definitionCache.GetValue(this, CsdlSemanticsNamedTypeReference.ComputeDefinitionFunc, null);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001B236 File Offset: 0x00019436
		public bool IsNullable
		{
			get
			{
				return this.reference.IsNullable;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0001B243 File Offset: 0x00019443
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0001B250 File Offset: 0x00019450
		public override CsdlElement Element
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000C768 File Offset: 0x0000A968
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001B258 File Offset: 0x00019458
		private IEdmType ComputeDefinition()
		{
			IEdmType edmType = this.schema.FindType(this.reference.FullName);
			IEdmType edmType2;
			if ((edmType2 = edmType) == null)
			{
				edmType2 = new UnresolvedType(this.schema.ReplaceAlias(this.reference.FullName) ?? this.reference.FullName, base.Location);
			}
			return edmType2;
		}

		// Token: 0x040005EE RID: 1518
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040005EF RID: 1519
		private readonly CsdlNamedTypeReference reference;

		// Token: 0x040005F0 RID: 1520
		private readonly Cache<CsdlSemanticsNamedTypeReference, IEdmType> definitionCache = new Cache<CsdlSemanticsNamedTypeReference, IEdmType>();

		// Token: 0x040005F1 RID: 1521
		private static readonly Func<CsdlSemanticsNamedTypeReference, IEdmType> ComputeDefinitionFunc = (CsdlSemanticsNamedTypeReference me) => me.ComputeDefinition();
	}
}
