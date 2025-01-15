using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000199 RID: 409
	internal class CsdlSemanticsProperty : CsdlSemanticsElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0001E857 File Offset: 0x0001CA57
		public CsdlSemanticsProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlProperty property)
			: base(property)
		{
			this.property = property;
			this.declaringType = declaringType;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001E879 File Offset: 0x0001CA79
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0001E886 File Offset: 0x0001CA86
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0001E88E File Offset: 0x0001CA8E
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001E8A2 File Offset: 0x0001CAA2
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		public string DefaultValueString
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001E8BC File Offset: 0x0001CABC
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001E8DD File Offset: 0x0001CADD
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringType.Context, this.property.Type);
		}

		// Token: 0x0400066A RID: 1642
		protected CsdlProperty property;

		// Token: 0x0400066B RID: 1643
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x0400066C RID: 1644
		private readonly Cache<CsdlSemanticsProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsProperty, IEdmTypeReference>();

		// Token: 0x0400066D RID: 1645
		private static readonly Func<CsdlSemanticsProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsProperty me) => me.ComputeType();
	}
}
