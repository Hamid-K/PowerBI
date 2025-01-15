using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A2 RID: 418
	internal class CsdlSemanticsNavigationProperty : CsdlSemanticsElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmCheckable
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x000207C4 File Offset: 0x0001E9C4
		public CsdlSemanticsNavigationProperty(CsdlSemanticsStructuredTypeDefinition declaringType, CsdlNavigationProperty navigationProperty)
			: base(navigationProperty)
		{
			this.declaringType = declaringType;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002081D File Offset: 0x0001EA1D
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0002082A File Offset: 0x0001EA2A
		public override CsdlElement Element
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00020832 File Offset: 0x0001EA32
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002083F File Offset: 0x0001EA3F
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00020860 File Offset: 0x0001EA60
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00020868 File Offset: 0x0001EA68
		public bool ContainsTarget
		{
			get
			{
				return this.navigationProperty.ContainsTarget;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00020875 File Offset: 0x0001EA75
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00002732 File Offset: 0x00000932
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00020889 File Offset: 0x0001EA89
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partnerCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputePartnerFunc, (CsdlSemanticsNavigationProperty cycle) => null);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000208BB File Offset: 0x0001EABB
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000208CF File Offset: 0x0001EACF
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraintCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeReferentialConstraintFunc, null);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x000208E3 File Offset: 0x0001EAE3
		private IEdmEntityType TargetEntityType
		{
			get
			{
				return this.targetEntityTypeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTargetEntityTypeFunc, null);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000208F8 File Offset: 0x0001EAF8
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

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000209B4 File Offset: 0x0001EBB4
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000209D0 File Offset: 0x0001EBD0
		private IEdmEntityType ComputeTargetEntityType()
		{
			IEdmType edmType = this.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return (IEdmEntityType)edmType;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00020A0C File Offset: 0x0001EC0C
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

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		private IEdmTypeReference ComputeType()
		{
			string text = this.navigationProperty.Type;
			bool flag;
			if (text.StartsWith("Collection(", StringComparison.Ordinal) && text.EndsWith(")", StringComparison.Ordinal))
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

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00020B6C File Offset: 0x0001ED6C
		private IEdmReferentialConstraint ComputeReferentialConstraint()
		{
			if (this.navigationProperty.ReferentialConstraints.Any<CsdlReferentialConstraint>())
			{
				return new EdmReferentialConstraint(this.navigationProperty.ReferentialConstraints.Select(new Func<CsdlReferentialConstraint, EdmReferentialConstraintPropertyPair>(this.ComputeReferentialConstraintPropertyPair)));
			}
			return null;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00020BA4 File Offset: 0x0001EDA4
		private EdmReferentialConstraintPropertyPair ComputeReferentialConstraintPropertyPair(CsdlReferentialConstraint csdlConstraint)
		{
			IEdmStructuralProperty edmStructuralProperty = (this.declaringType.FindProperty(csdlConstraint.PropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.declaringType, csdlConstraint.PropertyName, csdlConstraint.Location);
			IEdmStructuralProperty edmStructuralProperty2 = (this.TargetEntityType.FindProperty(csdlConstraint.ReferencedPropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.ToEntityType(), csdlConstraint.ReferencedPropertyName, csdlConstraint.Location);
			return new EdmReferentialConstraintPropertyPair(edmStructuralProperty, edmStructuralProperty2);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00020C1C File Offset: 0x0001EE1C
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

		// Token: 0x040006D7 RID: 1751
		private readonly CsdlNavigationProperty navigationProperty;

		// Token: 0x040006D8 RID: 1752
		private readonly CsdlSemanticsStructuredTypeDefinition declaringType;

		// Token: 0x040006D9 RID: 1753
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference>();

		// Token: 0x040006DA RID: 1754
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeType();

		// Token: 0x040006DB RID: 1755
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> partnerCache = new Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty>();

		// Token: 0x040006DC RID: 1756
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> ComputePartnerFunc = (CsdlSemanticsNavigationProperty me) => me.ComputePartner();

		// Token: 0x040006DD RID: 1757
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> referentialConstraintCache = new Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint>();

		// Token: 0x040006DE RID: 1758
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> ComputeReferentialConstraintFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeReferentialConstraint();

		// Token: 0x040006DF RID: 1759
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmEntityType> targetEntityTypeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmEntityType>();

		// Token: 0x040006E0 RID: 1760
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmEntityType> ComputeTargetEntityTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeTargetEntityType();

		// Token: 0x040006E1 RID: 1761
		private readonly Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>>();

		// Token: 0x040006E2 RID: 1762
		private static readonly Func<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeErrors();
	}
}
