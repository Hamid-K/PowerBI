using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FF RID: 511
	public class EdmEntityType : EdmStructuredType, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x00021EB7 File Offset: 0x000200B7
		public EdmEntityType(string namespaceName, string name)
			: this(namespaceName, name, null, false, false)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00021EC4 File Offset: 0x000200C4
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00021ED1 File Offset: 0x000200D1
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
			: this(namespaceName, name, baseType, isAbstract, isOpen, false)
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00021EE1 File Offset: 0x000200E1
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.hasStream = hasStream;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00021F1C File Offset: 0x0002011C
		public virtual IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKey;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00021F24 File Offset: 0x00020124
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00021F27 File Offset: 0x00020127
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00021F2F File Offset: 0x0002012F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00021F37 File Offset: 0x00020137
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00021F3A File Offset: 0x0002013A
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00021F3D File Offset: 0x0002013D
		public bool HasStream
		{
			get
			{
				return this.hasStream || (base.BaseType != null && this.BaseEntityType().HasStream);
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00021F5E File Offset: 0x0002015E
		public void AddKeys(params IEdmStructuralProperty[] keyProperties)
		{
			this.AddKeys((IEnumerable<IEdmStructuralProperty>)keyProperties);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00021F6C File Offset: 0x0002016C
		public void AddKeys(IEnumerable<IEdmStructuralProperty> keyProperties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(keyProperties, "keyProperties");
			foreach (IEdmStructuralProperty edmStructuralProperty in keyProperties)
			{
				if (this.declaredKey == null)
				{
					this.declaredKey = new List<IEdmStructuralProperty>();
				}
				this.declaredKey.Add(edmStructuralProperty);
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00021FD8 File Offset: 0x000201D8
		public EdmNavigationProperty AddUnidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationProperty(this, propertyInfo);
			base.AddProperty(edmNavigationProperty);
			return edmNavigationProperty;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00022004 File Offset: 0x00020204
		public EdmNavigationProperty AddBidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(propertyInfo.Target, "propertyInfo.Target");
			EdmEntityType edmEntityType = propertyInfo.Target as EdmEntityType;
			if (edmEntityType == null)
			{
				throw new ArgumentException("propertyInfo.Target", Strings.Constructable_TargetMustBeStock(typeof(EdmEntityType).FullName));
			}
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationPropertyWithPartner(propertyInfo, this.FixUpDefaultPartnerInfo(propertyInfo, partnerInfo));
			base.AddProperty(edmNavigationProperty);
			edmEntityType.AddProperty(edmNavigationProperty.Partner);
			return edmNavigationProperty;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00022080 File Offset: 0x00020280
		private EdmNavigationPropertyInfo FixUpDefaultPartnerInfo(EdmNavigationPropertyInfo propertyInfo, EdmNavigationPropertyInfo partnerInfo)
		{
			EdmNavigationPropertyInfo edmNavigationPropertyInfo = null;
			if (partnerInfo == null)
			{
				edmNavigationPropertyInfo = (partnerInfo = new EdmNavigationPropertyInfo());
			}
			if (partnerInfo.Name == null)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.Name = (propertyInfo.Name ?? string.Empty) + "Partner";
			}
			if (partnerInfo.Target == null)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.Target = this;
			}
			if (partnerInfo.TargetMultiplicity == EdmMultiplicity.Unknown)
			{
				if (edmNavigationPropertyInfo == null)
				{
					edmNavigationPropertyInfo = partnerInfo.Clone();
				}
				edmNavigationPropertyInfo.TargetMultiplicity = EdmMultiplicity.ZeroOrOne;
			}
			return edmNavigationPropertyInfo ?? partnerInfo;
		}

		// Token: 0x0400057D RID: 1405
		private readonly string namespaceName;

		// Token: 0x0400057E RID: 1406
		private readonly string name;

		// Token: 0x0400057F RID: 1407
		private readonly bool hasStream;

		// Token: 0x04000580 RID: 1408
		private List<IEdmStructuralProperty> declaredKey;
	}
}
