using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000187 RID: 391
	internal class CsdlSemanticsNamedTypeReference : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001D314 File Offset: 0x0001B514
		public CsdlSemanticsNamedTypeReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.reference = reference;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001D336 File Offset: 0x0001B536
		public IEdmType Definition
		{
			get
			{
				return this.definitionCache.GetValue(this, CsdlSemanticsNamedTypeReference.ComputeDefinitionFunc, null);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001D34A File Offset: 0x0001B54A
		public bool IsNullable
		{
			get
			{
				return this.reference.IsNullable;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0001D357 File Offset: 0x0001B557
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001D364 File Offset: 0x0001B564
		public override CsdlElement Element
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000C065 File Offset: 0x0000A265
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001D36C File Offset: 0x0001B56C
		private IEdmType ComputeDefinition()
		{
			IEdmType edmType = this.schema.FindType(this.reference.FullName);
			return edmType ?? new UnresolvedType(this.schema.ReplaceAlias(this.reference.FullName), base.Location);
		}

		// Token: 0x0400066A RID: 1642
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x0400066B RID: 1643
		private readonly CsdlNamedTypeReference reference;

		// Token: 0x0400066C RID: 1644
		private readonly Cache<CsdlSemanticsNamedTypeReference, IEdmType> definitionCache = new Cache<CsdlSemanticsNamedTypeReference, IEdmType>();

		// Token: 0x0400066D RID: 1645
		private static readonly Func<CsdlSemanticsNamedTypeReference, IEdmType> ComputeDefinitionFunc = (CsdlSemanticsNamedTypeReference me) => me.ComputeDefinition();
	}
}
