using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001B2 RID: 434
	internal class CsdlSemanticsNavigationProperty : CsdlSemanticsElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00018C78 File Offset: 0x00016E78
		public CsdlSemanticsNavigationProperty(CsdlSemanticsEntityTypeDefinition declaringType, CsdlNavigationProperty navigationProperty)
			: base(navigationProperty)
		{
			this.declaringType = declaringType;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00018CD1 File Offset: 0x00016ED1
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.declaringType.Model;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00018CDE File Offset: 0x00016EDE
		public override CsdlElement Element
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00018CE6 File Offset: 0x00016EE6
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00018CF3 File Offset: 0x00016EF3
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00018D14 File Offset: 0x00016F14
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00018D1C File Offset: 0x00016F1C
		public bool ContainsTarget
		{
			get
			{
				return this.navigationProperty.ContainsTarget;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00018D29 File Offset: 0x00016F29
		public IEdmTypeReference Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00018D3D File Offset: 0x00016F3D
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00018D43 File Offset: 0x00016F43
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partnerCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputePartnerFunc, (CsdlSemanticsNavigationProperty cycle) => null);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00018D73 File Offset: 0x00016F73
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00018D87 File Offset: 0x00016F87
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraintCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeReferentialConstraintFunc, null);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00018D9B File Offset: 0x00016F9B
		private IEdmEntityType TargetEntityType
		{
			get
			{
				return this.targetEntityTypeCache.GetValue(this, CsdlSemanticsNavigationProperty.ComputeTargetEntityTypeFunc, null);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00018DAF File Offset: 0x00016FAF
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.declaringType.Context);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00018DC8 File Offset: 0x00016FC8
		private IEdmEntityType ComputeTargetEntityType()
		{
			IEdmType edmType = this.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return (IEdmEntityType)edmType;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00018E04 File Offset: 0x00017004
		private IEdmNavigationProperty ComputePartner()
		{
			string partner = this.navigationProperty.Partner;
			IEdmEntityType targetEntityType = this.TargetEntityType;
			if (partner != null)
			{
				IEdmNavigationProperty edmNavigationProperty = targetEntityType.FindProperty(partner) as IEdmNavigationProperty;
				if (edmNavigationProperty == null)
				{
					edmNavigationProperty = new UnresolvedNavigationPropertyPath(targetEntityType, partner, base.Location);
				}
				return edmNavigationProperty;
			}
			foreach (IEdmNavigationProperty edmNavigationProperty2 in targetEntityType.NavigationProperties())
			{
				if (edmNavigationProperty2 != this && edmNavigationProperty2.Partner == this)
				{
					return edmNavigationProperty2;
				}
			}
			return null;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00018E9C File Offset: 0x0001709C
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

		// Token: 0x06000928 RID: 2344 RVA: 0x00018F64 File Offset: 0x00017164
		private IEdmReferentialConstraint ComputeReferentialConstraint()
		{
			if (Enumerable.Any<CsdlReferentialConstraint>(this.navigationProperty.ReferentialConstraints))
			{
				return new EdmReferentialConstraint(Enumerable.Select<CsdlReferentialConstraint, EdmReferentialConstraintPropertyPair>(this.navigationProperty.ReferentialConstraints, new Func<CsdlReferentialConstraint, EdmReferentialConstraintPropertyPair>(this.ComputeReferentialConstraintPropertyPair)));
			}
			return null;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00018F9C File Offset: 0x0001719C
		private EdmReferentialConstraintPropertyPair ComputeReferentialConstraintPropertyPair(CsdlReferentialConstraint csdlConstraint)
		{
			IEdmStructuralProperty edmStructuralProperty = (this.declaringType.FindProperty(csdlConstraint.PropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.declaringType, csdlConstraint.PropertyName, csdlConstraint.Location);
			IEdmStructuralProperty edmStructuralProperty2 = (this.TargetEntityType.FindProperty(csdlConstraint.ReferencedPropertyName) as IEdmStructuralProperty) ?? new UnresolvedProperty(this.ToEntityType(), csdlConstraint.ReferencedPropertyName, csdlConstraint.Location);
			return new EdmReferentialConstraintPropertyPair(edmStructuralProperty, edmStructuralProperty2);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00019014 File Offset: 0x00017214
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
			return list ?? Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000476 RID: 1142
		private readonly CsdlNavigationProperty navigationProperty;

		// Token: 0x04000477 RID: 1143
		private readonly CsdlSemanticsEntityTypeDefinition declaringType;

		// Token: 0x04000478 RID: 1144
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference> typeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmTypeReference>();

		// Token: 0x04000479 RID: 1145
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeType();

		// Token: 0x0400047A RID: 1146
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> partnerCache = new Cache<CsdlSemanticsNavigationProperty, IEdmNavigationProperty>();

		// Token: 0x0400047B RID: 1147
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmNavigationProperty> ComputePartnerFunc = (CsdlSemanticsNavigationProperty me) => me.ComputePartner();

		// Token: 0x0400047C RID: 1148
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> referentialConstraintCache = new Cache<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint>();

		// Token: 0x0400047D RID: 1149
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmReferentialConstraint> ComputeReferentialConstraintFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeReferentialConstraint();

		// Token: 0x0400047E RID: 1150
		private readonly Cache<CsdlSemanticsNavigationProperty, IEdmEntityType> targetEntityTypeCache = new Cache<CsdlSemanticsNavigationProperty, IEdmEntityType>();

		// Token: 0x0400047F RID: 1151
		private static readonly Func<CsdlSemanticsNavigationProperty, IEdmEntityType> ComputeTargetEntityTypeFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeTargetEntityType();

		// Token: 0x04000480 RID: 1152
		private readonly Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>>();

		// Token: 0x04000481 RID: 1153
		private static readonly Func<CsdlSemanticsNavigationProperty, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsNavigationProperty me) => me.ComputeErrors();
	}
}
