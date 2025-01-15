using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000066 RID: 102
	public sealed class EdmNavigationProperty : EdmProperty, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000B457 File Offset: 0x00009657
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Navigation;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B485 File Offset: 0x00009685
		public bool ContainsTarget
		{
			get
			{
				return this.containsTarget;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000B48D File Offset: 0x0000968D
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return this.referentialConstraint;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000B495 File Offset: 0x00009695
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return this.onDelete;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000B49D File Offset: 0x0000969D
		public IEdmNavigationProperty Partner
		{
			get
			{
				return this.partner;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B4A5 File Offset: 0x000096A5
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000B4AD File Offset: 0x000096AD
		internal IEdmPathExpression PartnerPath { get; private set; }

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public static EdmNavigationProperty CreateNavigationProperty(IEdmStructuredType declaringType, EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<string>(propertyInfo.Name, "propertyInfo.Name");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			return new EdmNavigationProperty(declaringType, propertyInfo.Name, EdmNavigationProperty.CreateNavigationPropertyType(propertyInfo.Target, propertyInfo.TargetMultiplicity, "propertyInfo.TargetMultiplicity"), propertyInfo.DependentProperties, propertyInfo.PrincipalProperties, propertyInfo.ContainsTarget, propertyInfo.OnDelete);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B530 File Offset: 0x00009730
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

		// Token: 0x060003AA RID: 938 RVA: 0x0000B640 File Offset: 0x00009840
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

		// Token: 0x060003AB RID: 939 RVA: 0x0000B6FB File Offset: 0x000098FB
		internal void SetPartner(IEdmNavigationProperty navigationProperty, IEdmPathExpression navigationPropertyPath)
		{
			this.partner = navigationProperty;
			this.PartnerPath = navigationPropertyPath;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B70C File Offset: 0x0000990C
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

		// Token: 0x060003AD RID: 941 RVA: 0x0000B760 File Offset: 0x00009960
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

		// Token: 0x040000D6 RID: 214
		private readonly IEdmReferentialConstraint referentialConstraint;

		// Token: 0x040000D7 RID: 215
		private readonly bool containsTarget;

		// Token: 0x040000D8 RID: 216
		private readonly EdmOnDeleteAction onDelete;

		// Token: 0x040000D9 RID: 217
		private IEdmNavigationProperty partner;
	}
}
