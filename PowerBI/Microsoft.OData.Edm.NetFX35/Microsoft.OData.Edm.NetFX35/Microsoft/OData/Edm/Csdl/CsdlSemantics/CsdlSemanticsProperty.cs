using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001DB RID: 475
	internal class CsdlSemanticsProperty : CsdlSemanticsElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00019C1B File Offset: 0x00017E1B
		public CsdlSemanticsProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlProperty property)
			: base(property)
		{
			this.property = property;
			this.declaringType = declaringType;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00019C3D File Offset: 0x00017E3D
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00019C4A File Offset: 0x00017E4A
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00019C52 File Offset: 0x00017E52
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00019C66 File Offset: 0x00017E66
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00019C73 File Offset: 0x00017E73
		public string DefaultValueString
		{
			get
			{
				return this.property.DefaultValue;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00019C80 File Offset: 0x00017E80
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				if (!this.property.IsFixedConcurrency)
				{
					return EdmConcurrencyMode.None;
				}
				return EdmConcurrencyMode.Fixed;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00019C92 File Offset: 0x00017E92
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00019C95 File Offset: 0x00017E95
		public override CsdlElement Element
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00019C9D File Offset: 0x00017E9D
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00019CB6 File Offset: 0x00017EB6
		private IEdmTypeReference ComputeType()
		{
			return CsdlSemanticsModel.WrapTypeReference(this.declaringType.Context, this.property.Type);
		}

		// Token: 0x040004CB RID: 1227
		protected CsdlProperty property;

		// Token: 0x040004CC RID: 1228
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x040004CD RID: 1229
		private readonly Cache<CsdlSemanticsProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsProperty, IEdmTypeReference>();

		// Token: 0x040004CE RID: 1230
		private static readonly Func<CsdlSemanticsProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsProperty me) => me.ComputeType();
	}
}
