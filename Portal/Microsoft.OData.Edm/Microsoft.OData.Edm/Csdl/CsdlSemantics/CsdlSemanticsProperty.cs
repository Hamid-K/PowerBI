using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AA RID: 426
	internal class CsdlSemanticsProperty : CsdlSemanticsElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x00020FEF File Offset: 0x0001F1EF
		public CsdlSemanticsProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlProperty property)
			: base(property)
		{
			this.property = property;
			this.declaringType = declaringType;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00021011 File Offset: 0x0001F211
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002101E File Offset: 0x0001F21E
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00021026 File Offset: 0x0001F226
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002103A File Offset: 0x0001F23A
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00021047 File Offset: 0x0001F247
		public string DefaultValueString
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00021054 File Offset: 0x0001F254
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002105C File Offset: 0x0001F25C
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00021075 File Offset: 0x0001F275
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringType.Context, this.property.Type);
		}

		// Token: 0x040006F5 RID: 1781
		protected CsdlProperty property;

		// Token: 0x040006F6 RID: 1782
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x040006F7 RID: 1783
		private readonly Cache<CsdlSemanticsProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsProperty, IEdmTypeReference>();

		// Token: 0x040006F8 RID: 1784
		private static readonly Func<CsdlSemanticsProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsProperty me) => me.ComputeType();
	}
}
