using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000192 RID: 402
	internal class CsdlSemanticsNavigationProperty : CsdlSemanticsElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmCheckable
	{
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		public CsdlSemanticsNavigationProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlNavigationProperty navigationProperty)
			: base(navigationProperty)
		{
			this.declaringType = declaringType;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001E10D File Offset: 0x0001C30D
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0001E11A File Offset: 0x0001C31A
		public override CsdlElement Element
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001E122 File Offset: 0x0001C322
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0001E12F File Offset: 0x0001C32F
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				if (this.navigationProperty.OnDelete == null)
				{
					return EdmOnDeleteAction.None;
				}
				return this.navigationProperty.OnDelete.Action;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001E150 File Offset: 0x0001C350
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001E158 File Offset: 0x0001C358
		public bool ContainsTarget
		{
			get
			{
				return this.navigationProperty.ContainsTarget;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001E165 File Offset: 0x0001C365
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001E179 File Offset: 0x0001C379
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partnerCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputePartnerFunc, (CsdlSemanticsNavigationProperty cycle) => null);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001E1AB File Offset: 0x0001C3AB
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0001E1BF File Offset: 0x0001C3BF
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraintCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeReferentialConstraintFunc, null);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001E1D3 File Offset: 0x0001C3D3
		private IEdmEntityType TargetEntityType
		{
			get
			{
				return this.targetEntityTypeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTargetEntityTypeFunc, null);
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
		internal static IEdmNavigationProperty ResolvePartnerPath(IEdmEntityType type, IEdmPathExpression path, IEdmModel model)
		{
			IEdmStructuredType edmStructuredType = type;
			IEdmProperty edmProperty = null;
			foreach (string text in path.PathSegments)
			{
				if (edmStructuredType == null)
				{
					return null;
				}
				if (text.IndexOf('.') < 0)
				{
					edmProperty = edmStructuredType.FindProperty(text);
					if (edmProperty == null)
					{
						return null;
					}
					edmStructuredType = edmProperty.Type.Definition.AsElementType() as IEdmStructuredType;
				}
				else
				{
					IEdmSchemaType edmSchemaType = model.FindDeclaredType(text);
					if (edmSchemaType == null || !edmSchemaType.IsOrInheritsFrom(edmStructuredType))
					{
						return null;
					}
					edmStructuredType = edmSchemaType as IEdmStructuredType;
					edmProperty = null;
				}
			}
			if (edmProperty == null)
			{
				return null;
			}
			return edmProperty as IEdmNavigationProperty;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
		private IEdmEntityType ComputeTargetEntityType()
		{
			IEdmType edmType = this.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return (IEdmEntityType)edmType;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		private IEdmNavigationProperty ComputePartner()
		{
			IEdmPathExpression partnerPath = this.navigationProperty.PartnerPath;
			IEdmEntityType targetEntityType = this.TargetEntityType;
			if (partnerPath != null)
			{
				return CsdlSemanticsNavigationProperty.ResolvePartnerPath(targetEntityType, partnerPath, this.Model) ?? new UnresolvedNavigationPropertyPath(targetEntityType, partnerPath.Path, base.Location);
			}
			foreach (IEdmNavigationProperty edmNavigationProperty in targetEntityType.NavigationProperties())
			{
				if (edmNavigationProperty != this && edmNavigationProperty.Partner == this)
				{
					return edmNavigationProperty;
				}
			}
			return null;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001E394 File Offset: 0x0001C594
		private IEdmTypeReference ComputeType()
		{
			string text = this.navigationProperty.Type;
			bool flag;
			if (text.StartsWith("Collection(", 4) && text.EndsWith(")", 4))
			{
				flag = true;
				text = text.Substring("Collection(".Length, text.Length - "Collection(".Length - 1);
			}
			else
			{
				flag = false;
			}
			IEdmEntityType edmEntityType = this.declaringType.Context.FindType(text) as IEdmEntityType;
			if (edmEntityType == null)
			{
				edmEntityType = new UnresolvedEntityType(text, base.Location);
			}
			bool flag2 = !flag && (this.navigationProperty.Nullable ?? true);
			IEdmEntityTypeReference edmEntityTypeReference = new EdmEntityTypeReference(edmEntityType, flag2);
			if (flag)
			{
				return new EdmCollectionTypeReference(new EdmCollectionType(edmEntityTypeReference));
			}
			return edmEntityTypeReference;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001E45C File Offset: 0x0001C65C
		private IEdmReferentialConstraint ComputeReferentialConstraint()
		{
			if (Enumerable.Any<CsdlReferentialConstraint>(this.navigationProperty.ReferentialConstraints))
			{
				return new EdmReferentialConstraint(Enumerable.Select<CsdlReferentialConstraint, EdmReferentialConstraintPropertyPair>(this.navigationProperty.ReferentialConstraints, new Func<CsdlReferentialConstraint, EdmReferentialConstraintPropertyPair>(this.ComputeReferentialConstraintPropertyPair)));
			}
			return null;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001E494 File Offset: 0x0001C694
		private EdmReferentialConstraintPropertyPair ComputeReferentialConstraintPropertyPair(CsdlReferentialConstraint csdlConstraint)
		{
			IEdmStructuralProperty edmStructuralProperty = (this.declaringType.FindProperty(csdlConstraint.PropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.declaringType, csdlConstraint.PropertyName, csdlConstraint.Location);
			IEdmStructuralProperty edmStructuralProperty2 = (this.TargetEntityType.FindProperty(csdlConstraint.ReferencedPropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.ToEntityType(), csdlConstraint.ReferencedPropertyName, csdlConstraint.Location);
			return new EdmReferentialConstraintPropertyPair(edmStructuralProperty, edmStructuralProperty2);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001E50C File Offset: 0x0001C70C
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = null;
			if (this.Type.IsCollection() && this.navigationProperty.Nullable != null)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, new EdmError(base.Location, EdmErrorCode.NavigationPropertyWithCollectionTypeCannotHaveNullableAttribute, Strings.CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType));
			}
			BadEntityType badEntityType = this.TargetEntityType as BadEntityType;
			if (badEntityType != null)
			{
				list = CsdlSemanticsElement.AllocateAndAdd<EdmError>(list, badEntityType.Errors);
			}
			IEnumerable<EdmError> enumerable = list;
			return enumerable ?? Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000650 RID: 1616
		private readonly CsdlNavigationProperty navigationProperty;

		// Token: 0x04000651 RID: 1617
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x04000652 RID: 1618
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference>();

		// Token: 0x04000653 RID: 1619
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeType();

		// Token: 0x04000654 RID: 1620
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> partnerCache = new Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty>();

		// Token: 0x04000655 RID: 1621
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> ComputePartnerFunc = (CsdlSemanticsNavigationProperty me) => me.ComputePartner();

		// Token: 0x04000656 RID: 1622
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> referentialConstraintCache = new Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint>();

		// Token: 0x04000657 RID: 1623
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> ComputeReferentialConstraintFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeReferentialConstraint();

		// Token: 0x04000658 RID: 1624
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmEntityType> targetEntityTypeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmEntityType>();

		// Token: 0x04000659 RID: 1625
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmEntityType> ComputeTargetEntityTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeTargetEntityType();

		// Token: 0x0400065A RID: 1626
		private readonly Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>>();

		// Token: 0x0400065B RID: 1627
		private static readonly Func<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeErrors();
	}
}
