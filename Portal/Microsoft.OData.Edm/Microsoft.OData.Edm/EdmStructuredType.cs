using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C5 RID: 197
	public abstract class EdmStructuredType : EdmType, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0000BE33 File Offset: 0x0000A033
		protected EdmStructuredType(bool isAbstract, bool isOpen, IEdmStructuredType baseStructuredType)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.baseStructuredType = baseStructuredType;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000BE66 File Offset: 0x0000A066
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000BE6E File Offset: 0x0000A06E
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000BE76 File Offset: 0x0000A076
		public virtual IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredProperties;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000BE7E File Offset: 0x0000A07E
		public IEdmStructuredType BaseType
		{
			get
			{
				return this.baseStructuredType;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000BE86 File Offset: 0x0000A086
		protected IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionary.GetValue(this, EdmStructuredType.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000BE9A File Offset: 0x0000A09A
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

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, true));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000BF08 File Offset: 0x0000A108
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type, bool isNullable)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, isNullable));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000BF34 File Offset: 0x0000A134
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000BF54 File Offset: 0x0000A154
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type, string defaultValue)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type, defaultValue);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000BF74 File Offset: 0x0000A174
		public EdmNavigationProperty AddUnidirectionalNavigation(EdmNavigationPropertyInfo propertyInfo)
		{
			EdmUtil.CheckArgumentNull<EdmNavigationPropertyInfo>(propertyInfo, "propertyInfo");
			EdmNavigationProperty edmNavigationProperty = EdmNavigationProperty.CreateNavigationProperty(this, propertyInfo);
			this.AddProperty(edmNavigationProperty);
			return edmNavigationProperty;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			if (!this.PropertiesDictionary.TryGetValue(name, out edmProperty))
			{
				return null;
			}
			return edmProperty;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04000174 RID: 372
		private readonly IEdmStructuredType baseStructuredType;

		// Token: 0x04000175 RID: 373
		private readonly List<IEdmProperty> declaredProperties = new List<IEdmProperty>();

		// Token: 0x04000176 RID: 374
		private readonly bool isAbstract;

		// Token: 0x04000177 RID: 375
		private readonly bool isOpen;

		// Token: 0x04000178 RID: 376
		private readonly Cache<EdmStructuredType, IDictionary<string, IEdmProperty>> propertiesDictionary = new Cache<EdmStructuredType, IDictionary<string, IEdmProperty>>();

		// Token: 0x04000179 RID: 377
		private static readonly Func<EdmStructuredType, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (EdmStructuredType me) => me.ComputePropertiesDictionary();
	}
}
