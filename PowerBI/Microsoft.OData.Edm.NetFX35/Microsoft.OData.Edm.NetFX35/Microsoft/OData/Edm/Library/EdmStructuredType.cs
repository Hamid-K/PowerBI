using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001F9 RID: 505
	public abstract class EdmStructuredType : EdmType, IEdmStructuredType, IEdmType, IEdmElement
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002187B File Offset: 0x0001FA7B
		protected EdmStructuredType(bool isAbstract, bool isOpen, IEdmStructuredType baseStructuredType)
		{
			this.isAbstract = isAbstract;
			this.isOpen = isOpen;
			this.baseStructuredType = baseStructuredType;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x000218AE File Offset: 0x0001FAAE
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000218B6 File Offset: 0x0001FAB6
		public bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000218BE File Offset: 0x0001FABE
		public virtual IEnumerable<IEdmProperty> DeclaredProperties
		{
			get
			{
				return this.declaredProperties;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000218C6 File Offset: 0x0001FAC6
		public IEdmStructuredType BaseType
		{
			get
			{
				return this.baseStructuredType;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000218CE File Offset: 0x0001FACE
		protected IDictionary<string, IEdmProperty> PropertiesDictionary
		{
			get
			{
				return this.propertiesDictionary.GetValue(this, EdmStructuredType.ComputePropertiesDictionaryFunc, null);
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000218E4 File Offset: 0x0001FAE4
		public void AddProperty(IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			if (!object.ReferenceEquals(this, property.DeclaringType))
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(property.Name));
			}
			this.declaredProperties.Add(property);
			this.propertiesDictionary.Clear(null);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00021934 File Offset: 0x0001FB34
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, true));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00021960 File Offset: 0x0001FB60
		public EdmStructuralProperty AddStructuralProperty(string name, EdmPrimitiveTypeKind type, bool isNullable)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, EdmCoreModel.Instance.GetPrimitive(type, isNullable));
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002198C File Offset: 0x0001FB8C
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000219AC File Offset: 0x0001FBAC
		public EdmStructuralProperty AddStructuralProperty(string name, IEdmTypeReference type, string defaultValue, EdmConcurrencyMode concurrencyMode)
		{
			EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this, name, type, defaultValue, concurrencyMode);
			this.AddProperty(edmStructuralProperty);
			return edmStructuralProperty;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000219D0 File Offset: 0x0001FBD0
		public IEdmProperty FindProperty(string name)
		{
			IEdmProperty edmProperty;
			if (!this.PropertiesDictionary.TryGetValue(name, ref edmProperty))
			{
				return null;
			}
			return edmProperty;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000219F0 File Offset: 0x0001FBF0
		private IDictionary<string, IEdmProperty> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
			foreach (IEdmProperty edmProperty in this.Properties())
			{
				RegistrationHelper.RegisterProperty(edmProperty, edmProperty.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x04000568 RID: 1384
		private readonly IEdmStructuredType baseStructuredType;

		// Token: 0x04000569 RID: 1385
		private readonly List<IEdmProperty> declaredProperties = new List<IEdmProperty>();

		// Token: 0x0400056A RID: 1386
		private readonly bool isAbstract;

		// Token: 0x0400056B RID: 1387
		private readonly bool isOpen;

		// Token: 0x0400056C RID: 1388
		private readonly Cache<EdmStructuredType, IDictionary<string, IEdmProperty>> propertiesDictionary = new Cache<EdmStructuredType, IDictionary<string, IEdmProperty>>();

		// Token: 0x0400056D RID: 1389
		private static readonly Func<EdmStructuredType, IDictionary<string, IEdmProperty>> ComputePropertiesDictionaryFunc = (EdmStructuredType me) => me.ComputePropertiesDictionary();
	}
}
