using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200007E RID: 126
	internal class CsdlSemanticsNamedTypeReference : CsdlSemanticsElement, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00005A49 File Offset: 0x00003C49
		public CsdlSemanticsNamedTypeReference(CsdlSemanticsSchema schema, CsdlNamedTypeReference reference)
			: base(reference)
		{
			this.schema = schema;
			this.reference = reference;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005A6B File Offset: 0x00003C6B
		public IEdmType Definition
		{
			get
			{
				return this.definitionCache.GetValue(this, CsdlSemanticsNamedTypeReference.ComputeDefinitionFunc, null);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00005A7F File Offset: 0x00003C7F
		public bool IsNullable
		{
			get
			{
				return this.reference.IsNullable;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005A8C File Offset: 0x00003C8C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.schema.Model;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005A99 File Offset: 0x00003C99
		public override CsdlElement Element
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005AA1 File Offset: 0x00003CA1
		public override string ToString()
		{
			return this.ToTraceString();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005AAC File Offset: 0x00003CAC
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

		// Token: 0x040000E6 RID: 230
		private readonly CsdlSemanticsSchema schema;

		// Token: 0x040000E7 RID: 231
		private readonly CsdlNamedTypeReference reference;

		// Token: 0x040000E8 RID: 232
		private readonly Cache<CsdlSemanticsNamedTypeReference, IEdmType> definitionCache = new Cache<CsdlSemanticsNamedTypeReference, IEdmType>();

		// Token: 0x040000E9 RID: 233
		private static readonly Func<CsdlSemanticsNamedTypeReference, IEdmType> ComputeDefinitionFunc = (CsdlSemanticsNamedTypeReference me) => me.ComputeDefinition();
	}
}
