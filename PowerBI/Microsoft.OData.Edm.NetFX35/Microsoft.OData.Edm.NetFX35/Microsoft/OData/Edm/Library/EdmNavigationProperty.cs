using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000203 RID: 515
	public sealed class EdmNavigationProperty : EdmProperty, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x00022484 File Offset: 0x00020684
		private EdmNavigationProperty(IEdmEntityType declaringType, string name, IEdmTypeReference type, IEnumerable<IEdmStructuralProperty> dependentProperties, IEnumerable<IEdmStructuralProperty> principalProperties, bool containsTarget, EdmOnDeleteAction onDelete)
			: base(declaringType, name, type)
		{
			this.containsTarget = containsTarget;
			this.onDelete = onDelete;
			if (dependentProperties != null)
			{
				this.referentialConstraint = EdmReferentialConstraint.Create(dependentProperties, principalProperties);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x000224B2 File Offset: 0x000206B2
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000224B5 File Offset: 0x000206B5
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000224BD File Offset: 0x000206BD
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraint;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x000224C5 File Offset: 0x000206C5
		public IEdmEntityType DeclaringEntityType
		{
			get
			{
				return (IEdmEntityType)base.DeclaringType;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x000224D2 File Offset: 0x000206D2
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x000224DA File Offset: 0x000206DA
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partner;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000224E4 File Offset: 0x000206E4
		public static EdmNavigationProperty CreateNavigationProperty(IEdmEntityType declaringType, EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<string>(propertyInfo.Name, "propertyInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			return new EdmNavigationProperty(declaringType, propertyInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(propertyInfo.Target, propertyInfo.TargetMultiplicity, "propertyInfo.TargetMultiplicity"), propertyInfo.DependentProperties, propertyInfo.PrincipalProperties, propertyInfo.ContainsTarget, propertyInfo.OnDelete);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002255C File Offset: 0x0002075C
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
			edmNavigationProperty.partner = edmNavigationProperty2;
			edmNavigationProperty2.partner = edmNavigationProperty;
			return edmNavigationProperty;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00022654 File Offset: 0x00020854
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
			edmNavigationProperty.partner = edmNavigationProperty2;
			edmNavigationProperty2.partner = edmNavigationProperty;
			return edmNavigationProperty;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000226FC File Offset: 0x000208FC
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

		// Token: 0x06000C2B RID: 3115 RVA: 0x00022750 File Offset: 0x00020950
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

		// Token: 0x0400058A RID: 1418
		private readonly IEdmReferentialConstraint referentialConstraint;

		// Token: 0x0400058B RID: 1419
		private readonly bool containsTarget;

		// Token: 0x0400058C RID: 1420
		private readonly EdmOnDeleteAction onDelete;

		// Token: 0x0400058D RID: 1421
		private EdmNavigationProperty partner;
	}
}
