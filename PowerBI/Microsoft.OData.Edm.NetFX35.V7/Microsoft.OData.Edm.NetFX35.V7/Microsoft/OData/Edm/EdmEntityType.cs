using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000059 RID: 89
	public class EdmEntityType : EdmStructuredType, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000AB33 File Offset: 0x00008D33
		public EdmEntityType(string namespaceName, string name)
			: this(namespaceName, name, null, false, false)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000AB40 File Offset: 0x00008D40
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000AB4D File Offset: 0x00008D4D
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
			: this(namespaceName, name, baseType, isAbstract, isOpen, false)
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000AB5D File Offset: 0x00008D5D
		public EdmEntityType(string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.hasStream = hasStream;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000AB98 File Offset: 0x00008D98
		public virtual IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return this.declaredKey;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		public bool HasStream
		{
			get
			{
				return this.hasStream || (base.BaseType != null && this.BaseEntityType().HasStream);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public void AddKeys(params IEdmStructuralProperty[] keyProperties)
		{
			this.AddKeys(keyProperties);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000ABDC File Offset: 0x00008DDC
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

		// Token: 0x06000358 RID: 856 RVA: 0x0000AC48 File Offset: 0x00008E48
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

		// Token: 0x06000359 RID: 857 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public void SetNavigationPropertyPartner(EdmNavigationProperty navigationProperty, IEdmPathExpression navigationPropertyPath, EdmNavigationProperty partnerNavigationProperty, IEdmPathExpression partnerNavigationPropertyPath)
		{
			navigationProperty.SetPartner(partnerNavigationProperty, partnerNavigationPropertyPath);
			if (partnerNavigationProperty.DeclaringType is IEdmEntityType)
			{
				partnerNavigationProperty.SetPartner(navigationProperty, navigationPropertyPath);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000ACE4 File Offset: 0x00008EE4
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

		// Token: 0x040000B6 RID: 182
		private readonly string namespaceName;

		// Token: 0x040000B7 RID: 183
		private readonly string name;

		// Token: 0x040000B8 RID: 184
		private readonly bool hasStream;

		// Token: 0x040000B9 RID: 185
		private List<IEdmStructuralProperty> declaredKey;
	}
}
