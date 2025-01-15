using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E1 RID: 225
	internal class TypeResolver
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		internal TypeResolver(ClientEdmModel model, Func<string, Type> resolveTypeFromName, Func<Type, string> resolveNameFromType, IEdmModel serviceModel)
		{
			this.resolveTypeFromName = resolveTypeFromName;
			this.resolveNameFromType = resolveNameFromType;
			this.serviceModel = serviceModel;
			this.clientEdmModel = model;
			if (serviceModel != null && this.clientEdmModel != null)
			{
				foreach (IEdmSchemaElement edmSchemaElement in serviceModel.SchemaElements.Where((IEdmSchemaElement se) => se is IEdmStructuredType))
				{
					this.clientEdmModel.EdmStructuredSchemaElements.TryAdd(edmSchemaElement.Name, edmSchemaElement);
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001FF7C File Offset: 0x0001E17C
		internal IEdmModel ReaderModel
		{
			get
			{
				if (this.serviceModel != null)
				{
					return this.serviceModel;
				}
				return this.clientEdmModel;
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001FF93 File Offset: 0x0001E193
		internal void IsProjectionRequest()
		{
			this.skipTypeAssignabilityCheck = true;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001FF9C File Offset: 0x0001E19C
		internal ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string readerTypeName)
		{
			string collectionItemWireTypeName = WebUtil.GetCollectionItemWireTypeName(readerTypeName);
			if (collectionItemWireTypeName != null)
			{
				Type implementationType = ClientTypeUtil.GetImplementationType(expectedType, typeof(ICollection<>));
				Type type = implementationType.GetGenericArguments()[0];
				if (!PrimitiveType.IsKnownType(type))
				{
					type = this.ResolveTypeForMaterialization(type, collectionItemWireTypeName).ElementType;
				}
				Type backingTypeForCollectionProperty = WebUtil.GetBackingTypeForCollectionProperty(expectedType);
				return this.clientEdmModel.GetClientTypeAnnotation(backingTypeForCollectionProperty);
			}
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(readerTypeName, out primitiveType))
			{
				return this.clientEdmModel.GetClientTypeAnnotation(primitiveType.ClrType);
			}
			ClientTypeAnnotation clientTypeAnnotation;
			if (this.edmTypeNameMap.TryGetValue(readerTypeName, out clientTypeAnnotation))
			{
				return clientTypeAnnotation;
			}
			if (this.serviceModel != null)
			{
				Type type2 = this.ResolveTypeFromName(readerTypeName, expectedType);
				return this.clientEdmModel.GetClientTypeAnnotation(type2);
			}
			return this.clientEdmModel.GetClientTypeAnnotation(readerTypeName);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00020054 File Offset: 0x0001E254
		internal IEdmType ResolveWireTypeName(IEdmType expectedEdmType, string wireName)
		{
			if (expectedEdmType != null && expectedEdmType.TypeKind == EdmTypeKind.Primitive)
			{
				return expectedEdmType;
			}
			Type type;
			if (expectedEdmType != null)
			{
				ClientTypeAnnotation clientTypeAnnotation = this.clientEdmModel.GetClientTypeAnnotation(expectedEdmType);
				type = clientTypeAnnotation.ElementType;
			}
			else
			{
				type = typeof(object);
			}
			Type type2 = this.ResolveTypeFromName(wireName, type);
			ClientTypeAnnotation clientTypeAnnotation2 = this.clientEdmModel.GetClientTypeAnnotation(this.clientEdmModel.GetOrCreateEdmType(type2));
			IEdmType edmType = clientTypeAnnotation2.EdmType;
			EdmTypeKind typeKind = edmType.TypeKind;
			if (typeKind == EdmTypeKind.Entity || typeKind == EdmTypeKind.Complex)
			{
				string text = edmType.FullName();
				if (!this.edmTypeNameMap.ContainsKey(text))
				{
					this.edmTypeNameMap.Add(text, clientTypeAnnotation2);
				}
			}
			return edmType;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000200F8 File Offset: 0x0001E2F8
		internal string ResolveServiceEntityTypeFullName(Type clientClrType)
		{
			IEdmType edmType = this.ResolveExpectedTypeForReading(clientClrType);
			if (edmType == null)
			{
				return null;
			}
			return edmType.FullName();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00020118 File Offset: 0x0001E318
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		internal IEdmType ResolveExpectedTypeForReading(Type clientClrType)
		{
			ClientTypeAnnotation clientTypeAnnotation = this.clientEdmModel.GetClientTypeAnnotation(clientClrType);
			IEdmType edmType = clientTypeAnnotation.EdmType;
			if (this.serviceModel == null)
			{
				return edmType;
			}
			if (edmType.TypeKind == EdmTypeKind.Primitive)
			{
				return edmType;
			}
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				IEdmTypeReference elementType = ((IEdmCollectionType)edmType).ElementType;
				if (elementType.IsPrimitive())
				{
					return edmType;
				}
				Type type = clientClrType.GetGenericArguments()[0];
				IEdmType edmType2 = this.ResolveExpectedTypeForReading(type);
				if (edmType2 == null)
				{
					return null;
				}
				return new EdmCollectionType(edmType2.ToEdmTypeReference(elementType.IsNullable));
			}
			else
			{
				IEdmStructuredType edmStructuredType;
				if (!this.TryToResolveServerType(clientTypeAnnotation, out edmStructuredType))
				{
					return null;
				}
				return edmStructuredType;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000201A8 File Offset: 0x0001E3A8
		internal bool ShouldWriteClientTypeForOpenServerProperty(IEdmProperty clientProperty, string serverTypeName)
		{
			if (serverTypeName == null)
			{
				return false;
			}
			if (this.serviceModel == null)
			{
				return false;
			}
			IEdmStructuredType edmStructuredType = this.serviceModel.FindType(serverTypeName) as IEdmStructuredType;
			return edmStructuredType != null && edmStructuredType.FindProperty(clientProperty.Name) == null;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000201EC File Offset: 0x0001E3EC
		internal bool TryResolveEntitySetBaseTypeName(string entitySetName, out string serverTypeName)
		{
			serverTypeName = null;
			if (this.serviceModel == null)
			{
				return false;
			}
			IEdmEntitySet edmEntitySet = this.serviceModel.FindDeclaredEntitySet(entitySetName);
			if (edmEntitySet != null)
			{
				serverTypeName = edmEntitySet.EntityType().FullName();
				return true;
			}
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00020228 File Offset: 0x0001E428
		internal bool TryResolveNavigationTargetTypeName(string serverSourceTypeName, string navigationPropertyName, out string serverTypeName)
		{
			serverTypeName = null;
			if (this.serviceModel == null || serverSourceTypeName == null)
			{
				return false;
			}
			IEdmEntityType edmEntityType = this.serviceModel.FindType(serverSourceTypeName) as IEdmEntityType;
			if (edmEntityType == null)
			{
				return false;
			}
			IEdmNavigationProperty edmNavigationProperty = edmEntityType.FindProperty(navigationPropertyName) as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				return false;
			}
			IEdmTypeReference edmTypeReference = edmNavigationProperty.Type;
			if (edmTypeReference.IsCollection())
			{
				edmTypeReference = edmTypeReference.AsCollection().ElementType();
			}
			serverTypeName = edmTypeReference.FullName();
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020294 File Offset: 0x0001E494
		private bool TryToResolveServerType(ClientTypeAnnotation clientTypeAnnotation, out IEdmStructuredType serverType)
		{
			string text = this.resolveNameFromType(clientTypeAnnotation.ElementType);
			if (text == null)
			{
				serverType = null;
				return false;
			}
			serverType = this.serviceModel.FindType(text) as IEdmStructuredType;
			return serverType != null;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000202D4 File Offset: 0x0001E4D4
		private Type ResolveTypeFromName(string wireName, Type expectedType)
		{
			Type type;
			if (!ClientConvert.ToNamedType(wireName, out type))
			{
				type = this.resolveTypeFromName(wireName);
				if (type == null)
				{
					type = ClientTypeCache.ResolveFromName(wireName, expectedType);
				}
				if (!this.skipTypeAssignabilityCheck && type != null && !expectedType.IsAssignableFrom(type))
				{
					throw Error.InvalidOperation(Strings.Deserialize_Current(expectedType, type));
				}
			}
			return type ?? expectedType;
		}

		// Token: 0x04000362 RID: 866
		private readonly IDictionary<string, ClientTypeAnnotation> edmTypeNameMap = new Dictionary<string, ClientTypeAnnotation>(StringComparer.Ordinal);

		// Token: 0x04000363 RID: 867
		private readonly Func<string, Type> resolveTypeFromName;

		// Token: 0x04000364 RID: 868
		private readonly Func<Type, string> resolveNameFromType;

		// Token: 0x04000365 RID: 869
		private readonly ClientEdmModel clientEdmModel;

		// Token: 0x04000366 RID: 870
		private readonly IEdmModel serviceModel;

		// Token: 0x04000367 RID: 871
		private bool skipTypeAssignabilityCheck;
	}
}
