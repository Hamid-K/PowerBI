using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001C1 RID: 449
	public class ODataResourceDeserializer : ODataEdmTypeDeserializer
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x0003C323 File Offset: 0x0003A523
		public ODataResourceDeserializer(ODataDeserializerProvider deserializerProvider)
			: base(ODataPayloadKind.Resource, deserializerProvider)
		{
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003C330 File Offset: 0x0003A530
		public override object Read(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
		{
			if (messageReader == null)
			{
				throw Error.ArgumentNull("messageReader");
			}
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			IEdmTypeReference edmType = readContext.GetEdmType(type);
			if (!edmType.IsStructured())
			{
				throw Error.Argument("type", SRResources.ArgumentMustBeOfType, new object[] { "Structured" });
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = edmType.AsStructured();
			IEdmNavigationSource edmNavigationSource = null;
			if (edmStructuredTypeReference.IsEntity())
			{
				if (readContext.Path == null)
				{
					throw Error.Argument("readContext", SRResources.ODataPathMissing, new object[0]);
				}
				edmNavigationSource = readContext.Path.NavigationSource;
				if (edmNavigationSource == null)
				{
					throw new SerializationException(SRResources.NavigationSourceMissingDuringDeserialization);
				}
			}
			ODataResourceWrapper odataResourceWrapper = messageReader.CreateODataResourceReader(edmNavigationSource, edmStructuredTypeReference.StructuredDefinition()).ReadResourceOrResourceSet() as ODataResourceWrapper;
			return this.ReadInline(odataResourceWrapper, edmStructuredTypeReference, readContext);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
		public sealed override object ReadInline(object item, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			if (edmType == null)
			{
				throw Error.ArgumentNull("edmType");
			}
			if (edmType.IsComplex() && item == null)
			{
				return null;
			}
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			if (!edmType.IsStructured())
			{
				throw Error.Argument("edmType", SRResources.ArgumentMustBeOfType, new object[] { "Entity or Complex" });
			}
			ODataResourceWrapper odataResourceWrapper = item as ODataResourceWrapper;
			if (odataResourceWrapper == null)
			{
				throw Error.Argument("item", SRResources.ArgumentMustBeOfType, new object[] { typeof(ODataResource).Name });
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			return this.ReadResource(odataResourceWrapper, edmType.AsStructured(), readContext);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003C498 File Offset: 0x0003A698
		public virtual object ReadResource(ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (resourceWrapper == null)
			{
				throw Error.ArgumentNull("resourceWrapper");
			}
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			if (string.IsNullOrEmpty(resourceWrapper.Resource.TypeName) || !(structuredType.FullName() != resourceWrapper.Resource.TypeName))
			{
				object obj = this.CreateResourceInstance(structuredType, readContext);
				this.ApplyResourceProperties(obj, resourceWrapper, structuredType, readContext);
				return obj;
			}
			IEdmModel model = readContext.Model;
			if (model == null)
			{
				throw Error.Argument("readContext", SRResources.ModelMissingFromReadContext, new object[0]);
			}
			IEdmStructuredType edmStructuredType = model.FindType(resourceWrapper.Resource.TypeName) as IEdmStructuredType;
			if (edmStructuredType == null)
			{
				throw new ODataException(Error.Format(SRResources.ResourceTypeNotInModel, new object[] { resourceWrapper.Resource.TypeName }));
			}
			if (edmStructuredType.IsAbstract)
			{
				throw new ODataException(Error.Format(SRResources.CannotInstantiateAbstractResourceType, new object[] { resourceWrapper.Resource.TypeName }));
			}
			IEdmEntityType edmEntityType = edmStructuredType as IEdmEntityType;
			IEdmTypeReference edmTypeReference;
			if (edmEntityType != null)
			{
				edmTypeReference = new EdmEntityTypeReference(edmEntityType, false);
			}
			else
			{
				edmTypeReference = new EdmComplexTypeReference(edmStructuredType as IEdmComplexType, false);
			}
			ODataEdmTypeDeserializer edmTypeDeserializer = base.DeserializerProvider.GetEdmTypeDeserializer(edmTypeReference);
			if (edmTypeDeserializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { edmEntityType.FullName() }));
			}
			object obj2 = edmTypeDeserializer.ReadInline(resourceWrapper, edmTypeReference, readContext);
			EdmStructuredObject edmStructuredObject = obj2 as EdmStructuredObject;
			if (edmStructuredObject != null)
			{
				edmStructuredObject.ExpectedEdmType = structuredType.StructuredDefinition();
			}
			return obj2;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003C608 File Offset: 0x0003A808
		public virtual object CreateResourceInstance(IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (readContext == null)
			{
				throw Error.ArgumentNull("readContext");
			}
			if (structuredType == null)
			{
				throw Error.ArgumentNull("structuredType");
			}
			IEdmModel model = readContext.Model;
			if (model == null)
			{
				throw Error.Argument("readContext", SRResources.ModelMissingFromReadContext, new object[0]);
			}
			if (readContext.IsUntyped)
			{
				if (structuredType.IsEntity())
				{
					return new EdmEntityObject(structuredType.AsEntity());
				}
				return new EdmComplexObject(structuredType.AsComplex());
			}
			else
			{
				Type clrType = EdmLibHelpers.GetClrType(structuredType, model);
				if (clrType == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { structuredType.FullName() }));
				}
				if (!readContext.IsDeltaOfT)
				{
					return Activator.CreateInstance(clrType);
				}
				IEnumerable<string> enumerable = from edmProperty in structuredType.StructuralProperties()
					select EdmLibHelpers.GetClrPropertyName(edmProperty, model);
				if (structuredType.IsOpen())
				{
					PropertyInfo dynamicPropertyDictionary = EdmLibHelpers.GetDynamicPropertyDictionary(structuredType.StructuredDefinition(), model);
					return Activator.CreateInstance(readContext.ResourceType, new object[] { clrType, enumerable, dynamicPropertyDictionary });
				}
				return Activator.CreateInstance(readContext.ResourceType, new object[] { clrType, enumerable });
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003C738 File Offset: 0x0003A938
		public virtual void ApplyNestedProperties(object resource, ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (resourceWrapper == null)
			{
				throw Error.ArgumentNull("resourceWrapper");
			}
			foreach (ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper in resourceWrapper.NestedResourceInfos)
			{
				this.ApplyNestedProperty(resource, odataNestedResourceInfoWrapper, structuredType, readContext);
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003C798 File Offset: 0x0003A998
		public virtual void ApplyNestedProperty(object resource, ODataNestedResourceInfoWrapper resourceInfoWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (resource == null)
			{
				throw Error.ArgumentNull("resource");
			}
			if (resourceInfoWrapper == null)
			{
				throw Error.ArgumentNull("resourceInfoWrapper");
			}
			IEdmProperty edmProperty = structuredType.FindProperty(resourceInfoWrapper.NestedResourceInfo.Name);
			if (edmProperty == null && !structuredType.IsOpen())
			{
				throw new ODataException(Error.Format(SRResources.NestedPropertyNotfound, new object[]
				{
					resourceInfoWrapper.NestedResourceInfo.Name,
					structuredType.FullName()
				}));
			}
			foreach (ODataItemBase odataItemBase in resourceInfoWrapper.NestedItems)
			{
				if (odataItemBase == null)
				{
					if (edmProperty == null)
					{
						this.ApplyDynamicResourceInNestedProperty(resourceInfoWrapper.NestedResourceInfo.Name, resource, structuredType, null, readContext);
					}
					else
					{
						this.ApplyResourceInNestedProperty(edmProperty, resource, null, readContext);
					}
				}
				if (!(odataItemBase is ODataEntityReferenceLinkBase))
				{
					ODataResourceSetWrapper odataResourceSetWrapper = odataItemBase as ODataResourceSetWrapper;
					if (odataResourceSetWrapper != null)
					{
						if (edmProperty == null)
						{
							this.ApplyDynamicResourceSetInNestedProperty(resourceInfoWrapper.NestedResourceInfo.Name, resource, structuredType, odataResourceSetWrapper, readContext);
						}
						else
						{
							this.ApplyResourceSetInNestedProperty(edmProperty, resource, odataResourceSetWrapper, readContext);
						}
					}
					else
					{
						ODataResourceWrapper odataResourceWrapper = (ODataResourceWrapper)odataItemBase;
						if (odataResourceWrapper != null)
						{
							if (edmProperty == null)
							{
								this.ApplyDynamicResourceInNestedProperty(resourceInfoWrapper.NestedResourceInfo.Name, resource, structuredType, odataResourceWrapper, readContext);
							}
							else
							{
								this.ApplyResourceInNestedProperty(edmProperty, resource, odataResourceWrapper, readContext);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003C8E4 File Offset: 0x0003AAE4
		public virtual void ApplyStructuralProperties(object resource, ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (resourceWrapper == null)
			{
				throw Error.ArgumentNull("resourceWrapper");
			}
			foreach (ODataProperty odataProperty in resourceWrapper.Resource.Properties)
			{
				this.ApplyStructuralProperty(resource, odataProperty, structuredType, readContext);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003C948 File Offset: 0x0003AB48
		public virtual void ApplyStructuralProperty(object resource, ODataProperty structuralProperty, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			if (resource == null)
			{
				throw Error.ArgumentNull("resource");
			}
			if (structuralProperty == null)
			{
				throw Error.ArgumentNull("structuralProperty");
			}
			DeserializationHelpers.ApplyProperty(structuralProperty, structuredType, resource, base.DeserializerProvider, readContext);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003C976 File Offset: 0x0003AB76
		private void ApplyResourceProperties(object resource, ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
		{
			this.ApplyStructuralProperties(resource, resourceWrapper, structuredType, readContext);
			this.ApplyNestedProperties(resource, resourceWrapper, structuredType, readContext);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003C990 File Offset: 0x0003AB90
		private void ApplyResourceInNestedProperty(IEdmProperty nestedProperty, object resource, ODataResourceWrapper resourceWrapper, ODataDeserializerContext readContext)
		{
			if (readContext.IsDeltaOfT)
			{
				IEdmNavigationProperty edmNavigationProperty = nestedProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					throw new ODataException(Error.Format(SRResources.CannotPatchNavigationProperties, new object[]
					{
						edmNavigationProperty.Name,
						edmNavigationProperty.DeclaringEntityType().FullName()
					}));
				}
			}
			object obj = this.ReadNestedResourceInline(resourceWrapper, nestedProperty.Type, readContext);
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(nestedProperty, readContext.Model);
			DeserializationHelpers.SetProperty(resource, clrPropertyName, obj);
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003CA04 File Offset: 0x0003AC04
		private void ApplyDynamicResourceInNestedProperty(string propertyName, object resource, IEdmStructuredTypeReference resourceStructuredType, ODataResourceWrapper resourceWrapper, ODataDeserializerContext readContext)
		{
			object obj = null;
			if (resourceWrapper != null)
			{
				IEdmTypeReference edmTypeReference = readContext.Model.FindDeclaredType(resourceWrapper.Resource.TypeName).ToEdmTypeReference(true);
				obj = this.ReadNestedResourceInline(resourceWrapper, edmTypeReference, readContext);
			}
			DeserializationHelpers.SetDynamicProperty(resource, propertyName, obj, resourceStructuredType.StructuredDefinition(), readContext.Model);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003CA58 File Offset: 0x0003AC58
		private object ReadNestedResourceInline(ODataResourceWrapper resourceWrapper, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			if (resourceWrapper == null)
			{
				return null;
			}
			ODataEdmTypeDeserializer edmTypeDeserializer = base.DeserializerProvider.GetEdmTypeDeserializer(edmType);
			if (edmTypeDeserializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { edmType.FullName() }));
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = edmType.AsStructured();
			ODataDeserializerContext odataDeserializerContext = new ODataDeserializerContext
			{
				Path = readContext.Path,
				Model = readContext.Model
			};
			Type type;
			if (readContext.IsUntyped)
			{
				type = (edmStructuredTypeReference.IsEntity() ? typeof(EdmEntityObject) : typeof(EdmComplexObject));
			}
			else
			{
				type = EdmLibHelpers.GetClrType(edmStructuredTypeReference, readContext.Model);
				if (type == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { edmStructuredTypeReference.FullName() }));
				}
			}
			odataDeserializerContext.ResourceType = (readContext.IsDeltaOfT ? typeof(Delta<>).MakeGenericType(new Type[] { type }) : type);
			return edmTypeDeserializer.ReadInline(resourceWrapper, edmType, odataDeserializerContext);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003CB54 File Offset: 0x0003AD54
		private void ApplyResourceSetInNestedProperty(IEdmProperty nestedProperty, object resource, ODataResourceSetWrapper resourceSetWrapper, ODataDeserializerContext readContext)
		{
			if (readContext.IsDeltaOfT)
			{
				IEdmNavigationProperty edmNavigationProperty = nestedProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					throw new ODataException(Error.Format(SRResources.CannotPatchNavigationProperties, new object[]
					{
						edmNavigationProperty.Name,
						edmNavigationProperty.DeclaringEntityType().FullName()
					}));
				}
			}
			object obj = this.ReadNestedResourceSetInline(resourceSetWrapper, nestedProperty.Type, readContext);
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(nestedProperty, readContext.Model);
			DeserializationHelpers.SetCollectionProperty(resource, nestedProperty, obj, clrPropertyName);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003CBC8 File Offset: 0x0003ADC8
		private void ApplyDynamicResourceSetInNestedProperty(string propertyName, object resource, IEdmStructuredTypeReference structuredType, ODataResourceSetWrapper resourceSetWrapper, ODataDeserializerContext readContext)
		{
			if (string.IsNullOrEmpty(resourceSetWrapper.ResourceSet.TypeName))
			{
				throw new ODataException(Error.Format(SRResources.DynamicResourceSetTypeNameIsRequired, new object[] { propertyName }));
			}
			string collectionElementTypeName = DeserializationHelpers.GetCollectionElementTypeName(resourceSetWrapper.ResourceSet.TypeName, false);
			EdmCollectionTypeReference edmCollectionTypeReference = new EdmCollectionTypeReference(new EdmCollectionType(readContext.Model.FindDeclaredType(collectionElementTypeName).ToEdmTypeReference(true)));
			if (base.DeserializerProvider.GetEdmTypeDeserializer(edmCollectionTypeReference) == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { edmCollectionTypeReference.FullName() }));
			}
			IEnumerable enumerable = this.ReadNestedResourceSetInline(resourceSetWrapper, edmCollectionTypeReference, readContext) as IEnumerable;
			object obj = enumerable;
			if (enumerable != null && readContext.IsUntyped)
			{
				obj = enumerable.ConvertToEdmObject(edmCollectionTypeReference);
			}
			DeserializationHelpers.SetDynamicProperty(resource, structuredType, EdmTypeKind.Collection, propertyName, obj, edmCollectionTypeReference, readContext.Model);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		private object ReadNestedResourceSetInline(ODataResourceSetWrapper resourceSetWrapper, IEdmTypeReference edmType, ODataDeserializerContext readContext)
		{
			ODataEdmTypeDeserializer edmTypeDeserializer = base.DeserializerProvider.GetEdmTypeDeserializer(edmType);
			if (edmTypeDeserializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeDeserialized, new object[] { edmType.FullName() }));
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = edmType.AsCollection().ElementType().AsStructured();
			ODataDeserializerContext odataDeserializerContext = new ODataDeserializerContext
			{
				Path = readContext.Path,
				Model = readContext.Model
			};
			if (readContext.IsUntyped)
			{
				if (edmStructuredTypeReference.IsEntity())
				{
					odataDeserializerContext.ResourceType = typeof(EdmEntityObjectCollection);
				}
				else
				{
					odataDeserializerContext.ResourceType = typeof(EdmComplexObjectCollection);
				}
			}
			else
			{
				Type clrType = EdmLibHelpers.GetClrType(edmStructuredTypeReference, readContext.Model);
				if (clrType == null)
				{
					throw new ODataException(Error.Format(SRResources.MappingDoesNotContainResourceType, new object[] { edmStructuredTypeReference.FullName() }));
				}
				odataDeserializerContext.ResourceType = typeof(List<>).MakeGenericType(new Type[] { clrType });
			}
			return edmTypeDeserializer.ReadInline(resourceSetWrapper, edmType, odataDeserializerContext);
		}
	}
}
