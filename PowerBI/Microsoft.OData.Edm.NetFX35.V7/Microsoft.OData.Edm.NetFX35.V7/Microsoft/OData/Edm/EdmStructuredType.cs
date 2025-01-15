using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000077 RID: 119
	public abstract class EdmStructuredType : EdmType, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		protected EdmStructuredType(bool isAbstract, bool isOpen, IEdmStructuredType baseStructuredType)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.baseStructuredType = baseStructuredType;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000C2EF File Offset: 0x0000A4EF
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000C2F7 File Offset: 0x0000A4F7
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000C2FF File Offset: 0x0000A4FF
		public virtual IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredProperties;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000C307 File Offset: 0x0000A507
		public IEdmStructuredType BaseType
		{
			get
			{
				return this.baseStructuredType;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000C30F File Offset: 0x0000A50F
		protected IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionary.GetValue(this, EdmStructuredType.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C323 File Offset: 0x0000A523
		public void AddProperty(IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			if (this != property.DeclaringType)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(property.Name));
			}
			this.declaredProperties.Add(property);
			this.propertiesDictionary.Clear(null);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000C364 File Offset: 0x0000A564
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, true));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000C390 File Offset: 0x0000A590
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type, bool isNullable)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, isNullable));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type, string defaultValue)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type, defaultValue);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		public EdmNavigationProperty AddUnidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationProperty(this, propertyInfo);
			this.AddProperty(edmNavigationProperty);
			return edmNavigationProperty;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C428 File Offset: 0x0000A628
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			if (!this.PropertiesDictionary.TryGetValue(name, ref edmProperty))
			{
				return null;
			}
			return edmProperty;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C448 File Offset: 0x0000A648
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04000105 RID: 261
		private readonly IEdmStructuredType baseStructuredType;

		// Token: 0x04000106 RID: 262
		private readonly List<IEdmProperty> declaredProperties = new List<IEdmProperty>();

		// Token: 0x04000107 RID: 263
		private readonly bool isAbstract;

		// Token: 0x04000108 RID: 264
		private readonly bool isOpen;

		// Token: 0x04000109 RID: 265
		private readonly Cache<EdmStructuredType, IDictionary<string, IEdmProperty>> propertiesDictionary = new Cache<EdmStructuredType, IDictionary<string, IEdmProperty>>();

		// Token: 0x0400010A RID: 266
		private static readonly Func<EdmStructuredType, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (EdmStructuredType me) => me.ComputePropertiesDictionary();
	}
}
