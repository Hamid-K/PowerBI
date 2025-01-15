using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BE RID: 190
	public sealed class EdmNavigationProperty : EdmProperty, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0000B9AC File Offset: 0x00009BAC
		private EdmNavigationProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type, IEnumerable<IEdmStructuralProperty> dependentProperties, IEnumerable<IEdmStructuralProperty> principalProperties, bool containsTarget, EdmOnDeleteAction onDelete)
			: base(declaringType, name, type)
		{
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
			if (dependentProperties != null)
			{
				this.referentialConstraint = EdmReferentialConstraint.Create(dependentProperties, principalProperties);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000B9DA File Offset: 0x00009BDA
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraint;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000B9EA File Offset: 0x00009BEA
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000B9F2 File Offset: 0x00009BF2
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partner;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000B9FA File Offset: 0x00009BFA
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000BA02 File Offset: 0x00009C02
		internal IEdmPathExpression PartnerPath { get; private set; }

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public static EdmNavigationProperty CreateNavigationProperty(IEdmStructuredType declaringType, EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<string>(propertyInfo.Name, "propertyInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			return new EdmNavigationProperty(declaringType, propertyInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(propertyInfo.Target, propertyInfo.TargetMultiplicity, "propertyInfo.TargetMultiplicity"), propertyInfo.DependentProperties, propertyInfo.PrincipalProperties, propertyInfo.ContainsTarget, propertyInfo.OnDelete);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BA84 File Offset: 0x00009C84
		public static EdmNavigationProperty CreateNavigationPropertyWithPartner(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<string>(propertyInfo.Name, "propertyInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(partnerInfo, "partnerInfo");
			EdmUtil.CheckArgumentNull<string>(partnerInfo.Name, "partnerInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(partnerInfo.Target, "partnerInfo.Target");
			EdmNavigationProperty edmNavigationProperty = new EdmNavigationProperty(partnerInfo.Target, propertyInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(propertyInfo.Target, propertyInfo.TargetMultiplicity, "propertyInfo.TargetMultiplicity"), propertyInfo.DependentProperties, propertyInfo.PrincipalProperties, propertyInfo.ContainsTarget, propertyInfo.OnDelete);
			EdmNavigationProperty edmNavigationProperty2 = new EdmNavigationProperty(propertyInfo.Target, partnerInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(partnerInfo.Target, partnerInfo.TargetMultiplicity, "partnerInfo.TargetMultiplicity"), partnerInfo.DependentProperties, partnerInfo.PrincipalProperties, partnerInfo.ContainsTarget, partnerInfo.OnDelete);
			edmNavigationProperty.SetPartner(edmNavigationProperty2, new EdmPathExpression(edmNavigationProperty2.Name));
			edmNavigationProperty2.SetPartner(edmNavigationProperty, new EdmPathExpression(edmNavigationProperty.Name));
			return edmNavigationProperty;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BB94 File Offset: 0x00009D94
		public static EdmNavigationProperty CreateNavigationPropertyWithPartner(string propertyName, IEdmTypeReference propertyType, IEnumerable<IEdmStructuralProperty> dependentProperties, IEnumerable<IEdmStructuralProperty> principalProperties, bool containsTarget, EdmOnDeleteAction onDelete, string partnerPropertyName, IEdmTypeReference partnerPropertyType, IEnumerable<IEdmStructuralProperty> partnerDependentProperties, IEnumerable<IEdmStructuralProperty> partnerPrincipalProperties, bool partnerContainsTarget, EdmOnDeleteAction partnerOnDelete)
		{
			EdmUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(propertyType, "propertyType");
			EdmUtil.CheckArgumentNull<string>(partnerPropertyName, "partnerPropertyName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(partnerPropertyType, "partnerPropertyType");
			IEdmEntityType entityType = EdmNavigationProperty.GetEntityType(partnerPropertyType);
			if (entityType == null)
			{
				throw new ArgumentException(Strings.Constructable_EntityTypeOrCollectionOfEntityTypeExpected, "partnerPropertyType");
			}
			IEdmEntityType entityType2 = EdmNavigationProperty.GetEntityType(propertyType);
			if (entityType2 == null)
			{
				throw new ArgumentException(Strings.Constructable_EntityTypeOrCollectionOfEntityTypeExpected, "propertyType");
			}
			EdmNavigationProperty edmNavigationProperty = new EdmNavigationProperty(entityType, propertyName, propertyType, dependentProperties, principalProperties, containsTarget, onDelete);
			EdmNavigationProperty edmNavigationProperty2 = new EdmNavigationProperty(entityType2, partnerPropertyName, partnerPropertyType, partnerDependentProperties, partnerPrincipalProperties, partnerContainsTarget, partnerOnDelete);
			edmNavigationProperty.SetPartner(edmNavigationProperty2, new EdmPathExpression(edmNavigationProperty2.Name));
			edmNavigationProperty2.SetPartner(edmNavigationProperty, new EdmPathExpression(edmNavigationProperty.Name));
			return edmNavigationProperty;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000BC4F File Offset: 0x00009E4F
		internal void SetPartner(IEdmNavigationProperty navigationProperty, IEdmPathExpression navigationPropertyPath)
		{
			this.partner = navigationProperty;
			this.PartnerPath = navigationPropertyPath;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000BC60 File Offset: 0x00009E60
		private static IEdmEntityType GetEntityType(IEdmTypeReference type)
		{
			IEdmEntityType edmEntityType = null;
			if (type.IsEntity())
			{
				edmEntityType = (IEdmEntityType)type.Definition;
			}
			else if (type.IsCollection())
			{
				type = ((IEdmCollectionType)type.Definition).ElementType;
				if (type.IsEntity())
				{
					edmEntityType = (IEdmEntityType)type.Definition;
				}
			}
			return edmEntityType;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		private static IEdmTypeReference CreateNavigationPropertyType(IEdmEntityType entityType, EdmMultiplicity multiplicity, string multiplicityParameterName)
		{
			switch (multiplicity)
			{
			case EdmMultiplicity.ZeroOrOne:
				return new EdmEntityTypeReference(entityType, true);
			case EdmMultiplicity.One:
				return new EdmEntityTypeReference(entityType, false);
			case EdmMultiplicity.Many:
				return EdmCoreModel.GetCollection(new EdmEntityTypeReference(entityType, false));
			default:
				throw new ArgumentOutOfRangeException(multiplicityParameterName, Strings.UnknownEnumVal_Multiplicity(multiplicity));
			}
		}

		// Token: 0x0400016A RID: 362
		private readonly IEdmReferentialConstraint referentialConstraint;

		// Token: 0x0400016B RID: 363
		private readonly bool containsTarget;

		// Token: 0x0400016C RID: 364
		private readonly EdmOnDeleteAction onDelete;

		// Token: 0x0400016D RID: 365
		private IEdmNavigationProperty partner;
	}
}
