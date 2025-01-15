using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000AB RID: 171
	internal class CsdlSemanticsNamedTypeReference : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00006E99 File Offset: 0x00005099
		public CsdlSemanticsNamedTypeReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.reference = reference;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00006EBB File Offset: 0x000050BB
		public IEdmType Definition
		{
			get
			{
				return this.definitionCache.GetValue(this, CsdlSemanticsNamedTypeReference.ComputeDefinitionFunc, null);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00006ECF File Offset: 0x000050CF
		public bool IsNullable
		{
			get
			{
				return this.reference.IsNullable;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00006EDC File Offset: 0x000050DC
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00006EE9 File Offset: 0x000050E9
		public override CsdlElement Element
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00006EF1 File Offset: 0x000050F1
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006EFC File Offset: 0x000050FC
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

		// Token: 0x04000133 RID: 307
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x04000134 RID: 308
		private readonly CsdlNamedTypeReference reference;

		// Token: 0x04000135 RID: 309
		private readonly Cache<CsdlSemanticsNamedTypeReference, IEdmType> definitionCache = new Cache<CsdlSemanticsNamedTypeReference, IEdmType>();

		// Token: 0x04000136 RID: 310
		private static readonly Func<CsdlSemanticsNamedTypeReference, IEdmType> ComputeDefinitionFunc = (CsdlSemanticsNamedTypeReference me) => me.ComputeDefinition();
	}
}
