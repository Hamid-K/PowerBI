using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B6 RID: 182
	public class EdmEntityType : EdmStructuredType, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public EdmEntityType(string namespaceName, string name)
			: this(namespaceName, name, null, false, false)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000AFAD File Offset: 0x000091AD
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000AFBA File Offset: 0x000091BA
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
			: this(namespaceName, name, baseType, isAbstract, isOpen, false)
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000AFCC File Offset: 0x000091CC
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.hasStream = hasStream;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.Name);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000B029 File Offset: 0x00009229
		public virtual IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKey;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B031 File Offset: 0x00009231
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000B039 File Offset: 0x00009239
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B041 File Offset: 0x00009241
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B049 File Offset: 0x00009249
		public bool HasStream
		{
			get
			{
				return this.hasStream || (base.BaseType != null && this.BaseEntityType().HasStream);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B06A File Offset: 0x0000926A
		public void AddKeys(params IEdmStructuralProperty[] keyProperties)
		{
			this.AddKeys(keyProperties);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B074 File Offset: 0x00009274
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

		// Token: 0x06000446 RID: 1094 RVA: 0x0000B0E0 File Offset: 0x000092E0
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

		// Token: 0x06000447 RID: 1095 RVA: 0x0000B15B File Offset: 0x0000935B
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public void SetNavigationPropertyPartner(EdmNavigationProperty navigationProperty, IEdmPathExpression navigationPropertyPath, EdmNavigationProperty partnerNavigationProperty, IEdmPathExpression partnerNavigationPropertyPath)
		{
			navigationProperty.SetPartner(partnerNavigationProperty, partnerNavigationPropertyPath);
			if (partnerNavigationProperty.DeclaringType is IEdmEntityType)
			{
				partnerNavigationProperty.SetPartner(navigationProperty, navigationPropertyPath);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B17C File Offset: 0x0000937C
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

		// Token: 0x0400014D RID: 333
		private readonly string namespaceName;

		// Token: 0x0400014E RID: 334
		private readonly string name;

		// Token: 0x0400014F RID: 335
		private readonly string fullName;

		// Token: 0x04000150 RID: 336
		private readonly bool hasStream;

		// Token: 0x04000151 RID: 337
		private List<IEdmStructuralProperty> declaredKey;
	}
}
