using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Client.Providers;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x02000026 RID: 38
	internal sealed class ClientEdmModel : EdmElement, IEdmModel, IEdmElement
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00006F7C File Offset: 0x0000517C
		internal ClientEdmModel(ODataProtocolVersion maxProtocolVersion)
		{
			this.maxProtocolVersion = maxProtocolVersion;
			this.EdmStructuredSchemaElements = new ConcurrentDictionary<string, IEdmSchemaElement>();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006FE0 File Offset: 0x000051E0
		public IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006FE7 File Offset: 0x000051E7
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return this.coreModel;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006FF6 File Offset: 0x000051F6
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.directValueAnnotationsManager;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006FFE File Offset: 0x000051FE
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007006 File Offset: 0x00005206
		internal ConcurrentDictionary<string, IEdmSchemaElement> EdmStructuredSchemaElements { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000700F File Offset: 0x0000520F
		internal ODataProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.maxProtocolVersion;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmOperationImport> FindOperationImportsByNameNonBindingParameterType(string operationImportName, IEnumerable<string> parameterNames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007018 File Offset: 0x00005218
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			ClientTypeAnnotation clientTypeAnnotation = null;
			if (this.typeNameToClientTypeAnnotationCache.TryGetValue(qualifiedName, out clientTypeAnnotation))
			{
				return (IEdmSchemaType)clientTypeAnnotation.EdmType;
			}
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003487 File Offset: 0x00001687
		public IEdmTerm FindDeclaredTerm(string qualifiedName)
		{
			return null;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006FEF File Offset: 0x000051EF
		public IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006FE0 File Offset: 0x000051E0
		public IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007044 File Offset: 0x00005244
		internal IEdmType GetOrCreateEdmType(Type type)
		{
			Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> dictionary = this.clrToEdmTypeCache;
			ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue;
			lock (dictionary)
			{
				this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue);
			}
			if (edmTypeCacheValue == null)
			{
				if (PrimitiveType.IsKnownNullableType(type))
				{
					edmTypeCacheValue = this.GetOrCreateEdmTypeInternal(null, type, ClientTypeUtil.EmptyPropertyInfoArray, false, new bool?(false));
				}
				else
				{
					PropertyInfo[] array;
					bool flag2;
					Type[] typeHierarchy = ClientEdmModel.GetTypeHierarchy(type, out array, out flag2);
					bool flag3 = array != null;
					array = array ?? ClientTypeUtil.EmptyPropertyInfoArray;
					foreach (Type type2 in typeHierarchy)
					{
						IEdmStructuredType edmStructuredType = ((edmTypeCacheValue == null) ? null : (edmTypeCacheValue.EdmType as IEdmStructuredType));
						edmTypeCacheValue = this.GetOrCreateEdmTypeInternal(edmStructuredType, type2, array, flag3, (type2 == type) ? new bool?(flag2) : null);
						array = ClientTypeUtil.EmptyPropertyInfoArray;
					}
				}
			}
			this.ValidateComplexType(type, edmTypeCacheValue);
			return edmTypeCacheValue.EdmType;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007140 File Offset: 0x00005340
		internal ClientTypeAnnotation GetClientTypeAnnotation(string edmTypeName)
		{
			IEdmType edmType = this.clrToEdmTypeCache.Values.First((ClientEdmModel.EdmTypeCacheValue e) => e.EdmType.FullName() == edmTypeName).EdmType;
			return this.GetClientTypeAnnotation(edmType);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007184 File Offset: 0x00005384
		private static Type[] GetTypeHierarchy(Type type, out PropertyInfo[] keyProperties, out bool hasProperties)
		{
			keyProperties = ClientTypeUtil.GetKeyPropertiesOnType(type, out hasProperties);
			List<Type> list = new List<Type>();
			if (keyProperties != null)
			{
				Type type2;
				if (keyProperties.Length != 0)
				{
					type2 = keyProperties[0].DeclaringType;
				}
				else
				{
					type2 = type;
					while (!type2.GetCustomAttributes(false).OfType<EntityTypeAttribute>().Any<EntityTypeAttribute>() && type2.GetBaseType() != null)
					{
						type2 = type2.GetBaseType();
					}
				}
				do
				{
					list.Insert(0, type);
					if (!(type != type2))
					{
						break;
					}
				}
				while ((type = type.GetBaseType()) != null);
			}
			else
			{
				do
				{
					list.Insert(0, type);
				}
				while ((type = type.GetBaseType()) != null && ClientTypeUtil.GetPropertiesOnType(type, false).Any<PropertyInfo>());
			}
			return list.ToArray();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007234 File Offset: 0x00005434
		private void ValidateComplexType(Type type, ClientEdmModel.EdmTypeCacheValue cachedEdmType)
		{
			if (cachedEdmType.EdmType.TypeKind == EdmTypeKind.Complex)
			{
				bool? hasProperties = cachedEdmType.HasProperties;
				if (hasProperties == null)
				{
					hasProperties = new bool?(ClientTypeUtil.GetPropertiesOnType(type, false).Any<PropertyInfo>());
					Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> dictionary = this.clrToEdmTypeCache;
					lock (dictionary)
					{
						ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue = this.clrToEdmTypeCache[type];
						edmTypeCacheValue.HasProperties = hasProperties;
					}
				}
				if (hasProperties == false && (type == typeof(object) || type.IsGenericType()))
				{
					throw Error.InvalidOperation(Strings.ClientType_NoSettableFields(type.ToString()));
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007300 File Offset: 0x00005500
		private void SetMimeTypeForProperties(IEdmStructuredType edmStructuredType)
		{
			MimeTypePropertyAttribute attribute = (MimeTypePropertyAttribute)this.GetClientTypeAnnotation(edmStructuredType).ElementType.GetCustomAttributes(typeof(MimeTypePropertyAttribute), true).SingleOrDefault<object>();
			if (attribute != null)
			{
				IEdmProperty edmProperty = edmStructuredType.Properties().SingleOrDefault((IEdmProperty p) => p.Name == attribute.DataPropertyName);
				if (edmProperty == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMimeTypeDataProperty(this.GetClientTypeAnnotation(edmStructuredType).ElementTypeName, attribute.DataPropertyName));
				}
				IEdmProperty edmProperty2 = edmStructuredType.Properties().SingleOrDefault((IEdmProperty p) => p.Name == attribute.MimeTypePropertyName);
				if (edmProperty2 == null)
				{
					throw Error.InvalidOperation(Strings.ClientType_MissingMimeTypeProperty(this.GetClientTypeAnnotation(edmStructuredType).ElementTypeName, attribute.MimeTypePropertyName));
				}
				this.GetClientPropertyAnnotation(edmProperty).MimeTypeProperty = this.GetClientPropertyAnnotation(edmProperty2);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000073D8 File Offset: 0x000055D8
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "cyclomatic complexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:MethodCoupledWithTooManyTypesFromDifferentNamespaces", Justification = "should refactor the method in the future.")]
		private ClientEdmModel.EdmTypeCacheValue GetOrCreateEdmTypeInternal(IEdmStructuredType edmBaseType, Type type, PropertyInfo[] keyProperties, bool isEntity, bool? hasProperties)
		{
			Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> dictionary = this.clrToEdmTypeCache;
			ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue;
			lock (dictionary)
			{
				this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue);
			}
			if (edmTypeCacheValue == null)
			{
				bool flag2 = false;
				IEdmSchemaElement edmSchemaElement = null;
				if (this.EdmStructuredSchemaElements.TryGetValue(ClientTypeUtil.GetServerDefinedTypeName(type), out edmSchemaElement))
				{
					IEdmStructuredType edmStructuredType = edmSchemaElement as IEdmStructuredType;
					if (edmStructuredType != null)
					{
						flag2 = edmStructuredType.IsOpen;
					}
				}
				Type implementationType;
				if (PrimitiveType.IsKnownNullableType(type))
				{
					PrimitiveType primitiveType;
					PrimitiveType.TryGetPrimitiveType(type, out primitiveType);
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(primitiveType.CreateEdmPrimitiveType(), hasProperties);
				}
				else if ((implementationType = ClientTypeUtil.GetImplementationType(type, typeof(ICollection<>))) != null && ClientTypeUtil.GetImplementationType(type, typeof(IDictionary<, >)) == null)
				{
					Type type2 = implementationType.GetGenericArguments()[0];
					IEdmType orCreateEdmType = this.GetOrCreateEdmType(type2);
					if (orCreateEdmType.TypeKind == EdmTypeKind.Collection)
					{
						throw new ODataException(Strings.ClientType_CollectionOfCollectionNotSupported);
					}
					edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmCollectionType(orCreateEdmType.ToEdmTypeReference(ClientTypeUtil.CanAssignNull(type2))), hasProperties);
				}
				else
				{
					Type enumTypeTmp = null;
					if (isEntity)
					{
						Action<EdmEntityTypeWithDelayLoadedProperties> action = delegate(EdmEntityTypeWithDelayLoadedProperties entityType)
						{
							List<IEdmProperty> list = new List<IEdmProperty>();
							List<IEdmStructuralProperty> list2 = new List<IEdmStructuralProperty>();
							using (IEnumerator<PropertyInfo> enumerator = (from p in ClientTypeUtil.GetPropertiesOnType(type, edmBaseType != null)
								orderby p.Name
								select p).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									PropertyInfo property = enumerator.Current;
									IEdmProperty edmProperty = this.CreateEdmProperty(entityType, property);
									list.Add(edmProperty);
									if (edmBaseType == null && keyProperties.Any((PropertyInfo k) => k.DeclaringType == type && k.Name == property.Name))
									{
										list2.Add((IEdmStructuralProperty)edmProperty);
									}
								}
							}
							foreach (IEdmProperty edmProperty2 in list)
							{
								entityType.AddProperty(edmProperty2);
							}
							entityType.AddKeys(list2);
						};
						bool hasStreamValue = ClientEdmModel.GetHasStreamValue((IEdmEntityType)edmBaseType, type);
						edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmEntityTypeWithDelayLoadedProperties(CommonUtil.GetModelTypeNamespace(type), CommonUtil.GetModelTypeName(type), (IEdmEntityType)edmBaseType, type.IsAbstract(), flag2, hasStreamValue, action), hasProperties);
					}
					else if ((enumTypeTmp = Nullable.GetUnderlyingType(type) ?? type) != null && enumTypeTmp.IsEnum())
					{
						Action<EdmEnumTypeWithDelayLoadedMembers> action2 = delegate(EdmEnumTypeWithDelayLoadedMembers enumType)
						{
							foreach (FieldInfo fieldInfo in enumTypeTmp.GetFields(BindingFlags.Static | BindingFlags.Public))
							{
								object obj = Enum.Parse(enumTypeTmp, fieldInfo.Name, false);
								enumType.AddMember(new EdmEnumMember(enumType, fieldInfo.Name, new EdmEnumMemberValue((long)Convert.ChangeType(obj, typeof(long), CultureInfo.InvariantCulture.NumberFormat))));
							}
						};
						Type underlyingType = Enum.GetUnderlyingType(enumTypeTmp);
						IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)EdmCoreModel.Instance.FindDeclaredType("Edm." + underlyingType.Name);
						bool flag3 = enumTypeTmp.GetCustomAttributes(false).Any((object s) => s is FlagsAttribute);
						edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmEnumTypeWithDelayLoadedMembers(CommonUtil.GetModelTypeNamespace(enumTypeTmp), CommonUtil.GetModelTypeName(enumTypeTmp), edmPrimitiveType, flag3, action2), null);
					}
					else
					{
						Action<EdmComplexTypeWithDelayLoadedProperties> action3 = delegate(EdmComplexTypeWithDelayLoadedProperties complexType)
						{
							List<IEdmProperty> list3 = new List<IEdmProperty>();
							foreach (PropertyInfo propertyInfo in from p in ClientTypeUtil.GetPropertiesOnType(type, edmBaseType != null)
								orderby p.Name
								select p)
							{
								IEdmProperty edmProperty3 = this.CreateEdmProperty(complexType, propertyInfo);
								list3.Add(edmProperty3);
							}
							foreach (IEdmProperty edmProperty4 in list3)
							{
								complexType.AddProperty(edmProperty4);
							}
						};
						edmTypeCacheValue = new ClientEdmModel.EdmTypeCacheValue(new EdmComplexTypeWithDelayLoadedProperties(CommonUtil.GetModelTypeNamespace(type), CommonUtil.GetModelTypeName(type), (IEdmComplexType)edmBaseType, type.IsAbstract(), flag2, action3), hasProperties);
					}
				}
				IEdmType edmType = edmTypeCacheValue.EdmType;
				ClientTypeAnnotation orCreateClientTypeAnnotation = this.GetOrCreateClientTypeAnnotation(edmType, type);
				this.SetClientTypeAnnotation(edmType, orCreateClientTypeAnnotation);
				if (edmType.TypeKind == EdmTypeKind.Entity || edmType.TypeKind == EdmTypeKind.Complex)
				{
					IEdmStructuredType edmStructuredType2 = edmType as IEdmStructuredType;
					this.SetMimeTypeForProperties(edmStructuredType2);
				}
				Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> dictionary2 = this.clrToEdmTypeCache;
				lock (dictionary2)
				{
					ClientEdmModel.EdmTypeCacheValue edmTypeCacheValue2;
					if (this.clrToEdmTypeCache.TryGetValue(type, out edmTypeCacheValue2))
					{
						edmTypeCacheValue = edmTypeCacheValue2;
					}
					else
					{
						this.clrToEdmTypeCache.Add(type, edmTypeCacheValue);
					}
				}
			}
			return edmTypeCacheValue;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000077E8 File Offset: 0x000059E8
		private IEdmProperty CreateEdmProperty(IEdmStructuredType declaringType, PropertyInfo propertyInfo)
		{
			IEdmType orCreateEdmType = this.GetOrCreateEdmType(propertyInfo.PropertyType);
			bool flag = ClientTypeUtil.CanAssignNull(propertyInfo.PropertyType);
			IEdmProperty edmProperty;
			if (orCreateEdmType.TypeKind == EdmTypeKind.Entity || (orCreateEdmType.TypeKind == EdmTypeKind.Collection && ((IEdmCollectionType)orCreateEdmType).ElementType.TypeKind() == EdmTypeKind.Entity))
			{
				IEdmEntityType edmEntityType = declaringType as IEdmEntityType;
				if (edmEntityType == null)
				{
					throw Error.InvalidOperation(Strings.ClientTypeCache_NonEntityTypeCannotContainEntityProperties(propertyInfo.Name, propertyInfo.DeclaringType.ToString()));
				}
				edmProperty = EdmNavigationProperty.CreateNavigationPropertyWithPartner(ClientTypeUtil.GetServerDefinedName(propertyInfo), orCreateEdmType.ToEdmTypeReference(flag), null, null, false, EdmOnDeleteAction.None, "Partner", edmEntityType.ToEdmTypeReference(true), null, null, false, EdmOnDeleteAction.None);
			}
			else
			{
				edmProperty = new EdmStructuralProperty(declaringType, ClientTypeUtil.GetServerDefinedName(propertyInfo), orCreateEdmType.ToEdmTypeReference(flag));
			}
			edmProperty.SetClientPropertyAnnotation(new ClientPropertyAnnotation(edmProperty, propertyInfo, this));
			return edmProperty;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000078A8 File Offset: 0x00005AA8
		private ClientTypeAnnotation GetOrCreateClientTypeAnnotation(IEdmType edmType, Type type)
		{
			string text = type.ToString();
			ClientTypeAnnotation clientTypeAnnotation;
			if (edmType.TypeKind == EdmTypeKind.Enum || edmType.TypeKind == EdmTypeKind.Complex || edmType.TypeKind == EdmTypeKind.Entity)
			{
				Dictionary<string, ClientTypeAnnotation> dictionary = this.typeNameToClientTypeAnnotationCache;
				lock (dictionary)
				{
					if (this.typeNameToClientTypeAnnotationCache.TryGetValue(text, out clientTypeAnnotation) && clientTypeAnnotation.ElementType != type)
					{
						text = type.AssemblyQualifiedName;
						if (this.typeNameToClientTypeAnnotationCache.TryGetValue(text, out clientTypeAnnotation) && clientTypeAnnotation.ElementType != type)
						{
							throw Error.InvalidOperation(Strings.ClientType_MultipleTypesWithSameName(text));
						}
					}
					if (clientTypeAnnotation == null)
					{
						clientTypeAnnotation = new ClientTypeAnnotation(edmType, type, text, this);
						this.typeNameToClientTypeAnnotationCache.Add(text, clientTypeAnnotation);
					}
					return clientTypeAnnotation;
				}
			}
			clientTypeAnnotation = new ClientTypeAnnotation(edmType, type, text, this);
			return clientTypeAnnotation;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000797C File Offset: 0x00005B7C
		private static bool GetHasStreamValue(IEdmEntityType edmBaseType, Type type)
		{
			MediaEntryAttribute mediaEntryAttribute = (MediaEntryAttribute)type.GetCustomAttributes(typeof(MediaEntryAttribute), true).SingleOrDefault<object>();
			return mediaEntryAttribute != null || type.GetCustomAttributes(typeof(HasStreamAttribute), true).Any<object>();
		}

		// Token: 0x04000065 RID: 101
		private readonly Dictionary<Type, ClientEdmModel.EdmTypeCacheValue> clrToEdmTypeCache = new Dictionary<Type, ClientEdmModel.EdmTypeCacheValue>(EqualityComparer<Type>.Default);

		// Token: 0x04000066 RID: 102
		private readonly Dictionary<string, ClientTypeAnnotation> typeNameToClientTypeAnnotationCache = new Dictionary<string, ClientTypeAnnotation>(StringComparer.Ordinal);

		// Token: 0x04000067 RID: 103
		private readonly EdmDirectValueAnnotationsManager directValueAnnotationsManager = new EdmDirectValueAnnotationsManager();

		// Token: 0x04000068 RID: 104
		private readonly ODataProtocolVersion maxProtocolVersion;

		// Token: 0x04000069 RID: 105
		private readonly IEnumerable<IEdmModel> coreModel = new IEdmModel[] { EdmCoreModel.Instance };

		// Token: 0x02000158 RID: 344
		private sealed class EdmTypeCacheValue
		{
			// Token: 0x06000D35 RID: 3381 RVA: 0x0002E513 File Offset: 0x0002C713
			public EdmTypeCacheValue(IEdmType edmType, bool? hasProperties)
			{
				this.edmType = edmType;
				this.hasProperties = hasProperties;
			}

			// Token: 0x17000347 RID: 839
			// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0002E529 File Offset: 0x0002C729
			public IEdmType EdmType
			{
				get
				{
					return this.edmType;
				}
			}

			// Token: 0x17000348 RID: 840
			// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0002E531 File Offset: 0x0002C731
			// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0002E539 File Offset: 0x0002C739
			public bool? HasProperties
			{
				get
				{
					return this.hasProperties;
				}
				set
				{
					this.hasProperties = value;
				}
			}

			// Token: 0x040006F1 RID: 1777
			private readonly IEdmType edmType;

			// Token: 0x040006F2 RID: 1778
			private bool? hasProperties;
		}
	}
}
