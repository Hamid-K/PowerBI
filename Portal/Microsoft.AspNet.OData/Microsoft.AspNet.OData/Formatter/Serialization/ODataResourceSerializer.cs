using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x020001AA RID: 426
	public class ODataResourceSerializer : ODataEdmTypeSerializer
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00038325 File Offset: 0x00036525
		public ODataResourceSerializer(ODataSerializerProvider serializerProvider)
			: base(ODataPayloadKind.Resource, serializerProvider)
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00038330 File Offset: 0x00036530
		public override void WriteObject(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
		{
			if (messageWriter == null)
			{
				throw Error.ArgumentNull("messageWriter");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, type);
			IEdmNavigationSource navigationSource = writeContext.NavigationSource;
			ODataWriter odataWriter = messageWriter.CreateODataResourceWriter(navigationSource, edmType.ToStructuredType());
			this.WriteObjectInline(graph, edmType, odataWriter, writeContext);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00038388 File Offset: 0x00036588
		public override void WriteObjectInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (graph == null || graph is NullEdmComplexObject)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "Resource" }));
			}
			this.WriteResource(graph, writer, writeContext, expectedType);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000383E8 File Offset: 0x000365E8
		public virtual void WriteDeltaObjectInline(object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			if (writeContext == null)
			{
				throw Error.ArgumentNull("writeContext");
			}
			if (graph == null)
			{
				throw new SerializationException(Error.Format(SRResources.CannotSerializerNull, new object[] { "Resource" }));
			}
			this.WriteDeltaResource(graph, writer, writeContext);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00038440 File Offset: 0x00036640
		private void WriteDeltaResource(object graph, ODataWriter writer, ODataSerializerContext writeContext)
		{
			IEdmStructuredTypeReference resourceType = this.GetResourceType(graph, writeContext);
			ResourceContext resourceContext = new ResourceContext(writeContext, resourceType, graph);
			EdmDeltaEntityObject edmDeltaEntityObject = graph as EdmDeltaEntityObject;
			if (edmDeltaEntityObject != null && edmDeltaEntityObject.NavigationSource != null)
			{
				resourceContext.NavigationSource = edmDeltaEntityObject.NavigationSource;
			}
			SelectExpandNode selectExpandNode = this.CreateSelectExpandNode(resourceContext);
			if (selectExpandNode != null)
			{
				ODataResource odataResource = this.CreateResource(selectExpandNode, resourceContext);
				if (odataResource != null)
				{
					writer.WriteStart(odataResource);
					this.WriteDeltaComplexProperties(selectExpandNode, resourceContext, writer);
					writer.WriteEnd();
				}
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000384B0 File Offset: 0x000366B0
		private void WriteDeltaComplexProperties(SelectExpandNode selectExpandNode, ResourceContext resourceContext, ODataWriter writer)
		{
			if (selectExpandNode.SelectedComplexTypeProperties == null)
			{
				return;
			}
			IEnumerable<IEdmStructuralProperty> enumerable = selectExpandNode.SelectedComplexTypeProperties.Keys;
			if (resourceContext.EdmObject != null && resourceContext.EdmObject.IsDeltaResource())
			{
				IDelta delta = resourceContext.EdmObject as IDelta;
				IEnumerable<string> changedProperties = delta.GetChangedPropertyNames();
				enumerable = enumerable.Where((IEdmStructuralProperty p) => changedProperties.Contains(p.Name));
			}
			foreach (IEdmStructuralProperty edmStructuralProperty in enumerable)
			{
				ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
				{
					IsCollection = new bool?(edmStructuralProperty.Type.IsCollection()),
					Name = edmStructuralProperty.Name
				};
				writer.WriteStart(odataNestedResourceInfo);
				this.WriteDeltaComplexAndExpandedNavigationProperty(edmStructuralProperty, null, resourceContext, writer);
				writer.WriteEnd();
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00038594 File Offset: 0x00036794
		private void WriteDeltaComplexAndExpandedNavigationProperty(IEdmProperty edmProperty, SelectExpandClause selectExpandClause, ResourceContext resourceContext, ODataWriter writer)
		{
			object propertyValue = resourceContext.GetPropertyValue(edmProperty.Name);
			if (propertyValue == null || propertyValue is NullEdmComplexObject)
			{
				if (edmProperty.Type.IsCollection())
				{
					writer.WriteStart(new ODataResourceSet
					{
						TypeName = edmProperty.Type.FullName()
					});
				}
				else
				{
					writer.WriteStart(null);
				}
				writer.WriteEnd();
				return;
			}
			ODataSerializerContext odataSerializerContext = new ODataSerializerContext(resourceContext, selectExpandClause, edmProperty);
			if (edmProperty.Type.IsCollection())
			{
				new ODataDeltaFeedSerializer(base.SerializerProvider).WriteDeltaFeedInline(propertyValue, edmProperty.Type, writer, odataSerializerContext);
				return;
			}
			new ODataResourceSerializer(base.SerializerProvider).WriteDeltaObjectInline(propertyValue, edmProperty.Type, writer, odataSerializerContext);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00038640 File Offset: 0x00036840
		private static IEnumerable<ODataProperty> CreateODataPropertiesFromDynamicType(EdmEntityType entityType, object graph, Dictionary<IEdmProperty, object> dynamicTypeProperties)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			DynamicTypeWrapper dynamicTypeWrapper = graph as DynamicTypeWrapper;
			if (dynamicTypeWrapper == null)
			{
				IEnumerable<DynamicTypeWrapper> enumerable = graph as IEnumerable<DynamicTypeWrapper>;
				if (enumerable != null)
				{
					dynamicTypeWrapper = enumerable.SingleOrDefault<DynamicTypeWrapper>();
				}
			}
			if (dynamicTypeWrapper != null)
			{
				using (Dictionary<string, object>.Enumerator enumerator = dynamicTypeWrapper.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> prop = enumerator.Current;
						if (prop.Value != null && (prop.Value is DynamicTypeWrapper || prop.Value is IEnumerable<DynamicTypeWrapper>))
						{
							IEdmProperty edmProperty = entityType.Properties().FirstOrDefault((IEdmProperty p) => p.Name.Equals(prop.Key));
							if (edmProperty != null)
							{
								dynamicTypeProperties.Add(edmProperty, prop.Value);
							}
						}
						else
						{
							ODataProperty odataProperty = new ODataProperty
							{
								Name = prop.Key,
								Value = prop.Value
							};
							list.Add(odataProperty);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00038764 File Offset: 0x00036964
		private void WriteDynamicTypeResource(object graph, ODataWriter writer, IEdmTypeReference expectedType, ODataSerializerContext writeContext)
		{
			Dictionary<IEdmProperty, object> dictionary = new Dictionary<IEdmProperty, object>();
			EdmEntityType edmEntityType = expectedType.Definition as EdmEntityType;
			ODataResource odataResource = new ODataResource
			{
				TypeName = expectedType.FullName(),
				Properties = ODataResourceSerializer.CreateODataPropertiesFromDynamicType(edmEntityType, graph, dictionary)
			};
			odataResource.IsTransient = true;
			writer.WriteStart(odataResource);
			using (Dictionary<IEdmProperty, object>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmProperty property = enumerator.Current;
					ResourceContext resourceContext = new ResourceContext(writeContext, expectedType.AsEntity(), graph);
					if (edmEntityType.NavigationProperties().Any((IEdmNavigationProperty p) => p.Type.Equals(property.Type)) && !(property.Type is EdmCollectionTypeReference))
					{
						IEdmNavigationProperty edmNavigationProperty = edmEntityType.NavigationProperties().FirstOrDefault((IEdmNavigationProperty p) => p.Type.Equals(property.Type));
						ODataNestedResourceInfo odataNestedResourceInfo = this.CreateNavigationLink(edmNavigationProperty, resourceContext);
						if (odataNestedResourceInfo != null)
						{
							writer.WriteStart(odataNestedResourceInfo);
							this.WriteDynamicTypeResource(dictionary[property], writer, property.Type, writeContext);
							writer.WriteEnd();
						}
					}
					else
					{
						ODataNestedResourceInfo odataNestedResourceInfo2 = new ODataNestedResourceInfo
						{
							IsCollection = new bool?(property.Type.IsCollection()),
							Name = property.Name
						};
						writer.WriteStart(odataNestedResourceInfo2);
						this.WriteDynamicComplexProperty(dictionary[property], property.Type, resourceContext, writer);
						writer.WriteEnd();
					}
				}
			}
			writer.WriteEnd();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00038918 File Offset: 0x00036B18
		private void WriteResource(object graph, ODataWriter writer, ODataSerializerContext writeContext, IEdmTypeReference expectedType)
		{
			if (EdmLibHelpers.IsDynamicTypeWrapper(graph.GetType()))
			{
				this.WriteDynamicTypeResource(graph, writer, expectedType, writeContext);
				return;
			}
			IEdmStructuredTypeReference resourceType = this.GetResourceType(graph, writeContext);
			ResourceContext resourceContext = new ResourceContext(writeContext, resourceType, graph);
			SelectExpandNode selectExpandNode = this.CreateSelectExpandNode(resourceContext);
			if (selectExpandNode != null)
			{
				ODataResource odataResource = this.CreateResource(selectExpandNode, resourceContext);
				if (odataResource != null)
				{
					if (resourceContext.SerializerContext.ExpandReference)
					{
						writer.WriteEntityReferenceLink(new ODataEntityReferenceLink
						{
							Url = odataResource.Id
						});
						return;
					}
					writer.WriteStart(odataResource);
					this.WriteComplexProperties(selectExpandNode, resourceContext, writer);
					this.WriteDynamicComplexProperties(resourceContext, writer);
					this.WriteNavigationLinks(selectExpandNode, resourceContext, writer);
					this.WriteExpandedNavigationProperties(selectExpandNode, resourceContext, writer);
					this.WriteReferencedNavigationProperties(selectExpandNode, resourceContext, writer);
					writer.WriteEnd();
				}
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000389C8 File Offset: 0x00036BC8
		public virtual SelectExpandNode CreateSelectExpandNode(ResourceContext resourceContext)
		{
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			ODataSerializerContext serializerContext = resourceContext.SerializerContext;
			IEdmStructuredType structuredType = resourceContext.StructuredType;
			Tuple<SelectExpandClause, IEdmStructuredType> tuple = Tuple.Create<SelectExpandClause, IEdmStructuredType>(serializerContext.SelectExpandClause, structuredType);
			object obj;
			if (!serializerContext.Items.TryGetValue(tuple, out obj))
			{
				obj = new SelectExpandNode(structuredType, serializerContext);
				serializerContext.Items[tuple] = obj;
			}
			return obj as SelectExpandNode;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00038A2C File Offset: 0x00036C2C
		public virtual ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
		{
			if (selectExpandNode == null)
			{
				throw Error.ArgumentNull("selectExpandNode");
			}
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			if (resourceContext.SerializerContext.ExpandReference)
			{
				return new ODataResource
				{
					Id = resourceContext.GenerateSelfLink(false)
				};
			}
			string text = resourceContext.StructuredType.FullTypeName();
			ODataResource odataResource = new ODataResource
			{
				TypeName = text,
				Properties = this.CreateStructuralPropertyBag(selectExpandNode, resourceContext)
			};
			if (resourceContext.EdmObject is EdmDeltaEntityObject && resourceContext.NavigationSource != null)
			{
				ODataResourceSerializationInfo odataResourceSerializationInfo = new ODataResourceSerializationInfo();
				odataResourceSerializationInfo.NavigationSourceName = resourceContext.NavigationSource.Name;
				odataResourceSerializationInfo.NavigationSourceKind = resourceContext.NavigationSource.NavigationSourceKind();
				IEdmEntityType edmEntityType = resourceContext.NavigationSource.EntityType();
				if (edmEntityType != null)
				{
					odataResourceSerializationInfo.NavigationSourceEntityTypeName = edmEntityType.Name;
				}
				odataResource.SetSerializationInfo(odataResourceSerializationInfo);
			}
			this.AppendDynamicProperties(odataResource, selectExpandNode, resourceContext);
			if (selectExpandNode.SelectedActions != null)
			{
				foreach (ODataAction odataAction in this.CreateODataActions(selectExpandNode.SelectedActions, resourceContext))
				{
					odataResource.AddAction(odataAction);
				}
			}
			if (selectExpandNode.SelectedFunctions != null)
			{
				foreach (ODataFunction odataFunction in this.CreateODataFunctions(selectExpandNode.SelectedFunctions, resourceContext))
				{
					odataResource.AddFunction(odataFunction);
				}
			}
			IEdmStructuredType odataPathType = ODataResourceSerializer.GetODataPathType(resourceContext.SerializerContext);
			if (resourceContext.StructuredType.TypeKind == EdmTypeKind.Complex)
			{
				ODataResourceSerializer.AddTypeNameAnnotationAsNeededForComplex(odataResource, resourceContext.SerializerContext.MetadataLevel);
			}
			else
			{
				ODataResourceSerializer.AddTypeNameAnnotationAsNeeded(odataResource, odataPathType, resourceContext.SerializerContext.MetadataLevel);
			}
			if (resourceContext.StructuredType.TypeKind == EdmTypeKind.Entity && resourceContext.NavigationSource != null)
			{
				if (!(resourceContext.NavigationSource is IEdmContainedEntitySet))
				{
					EntitySelfLinks entitySelfLinks = resourceContext.SerializerContext.Model.GetNavigationSourceLinkBuilder(resourceContext.NavigationSource).BuildEntitySelfLinks(resourceContext, resourceContext.SerializerContext.MetadataLevel);
					if (entitySelfLinks.IdLink != null)
					{
						odataResource.Id = entitySelfLinks.IdLink;
					}
					if (entitySelfLinks.ReadLink != null)
					{
						odataResource.ReadLink = entitySelfLinks.ReadLink;
					}
					if (entitySelfLinks.EditLink != null)
					{
						odataResource.EditLink = entitySelfLinks.EditLink;
					}
				}
				string text2 = this.CreateETag(resourceContext);
				if (text2 != null)
				{
					odataResource.ETag = text2;
				}
			}
			return odataResource;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00038CAC File Offset: 0x00036EAC
		public virtual void AppendDynamicProperties(ODataResource resource, SelectExpandNode selectExpandNode, ResourceContext resourceContext)
		{
			if (!resourceContext.StructuredType.IsOpen || (!selectExpandNode.SelectAllDynamicProperties && selectExpandNode.SelectedDynamicProperties == null))
			{
				return;
			}
			bool flag = false;
			if (resourceContext.EdmObject is EdmDeltaComplexObject || resourceContext.EdmObject is EdmDeltaEntityObject)
			{
				flag = true;
			}
			else if (resourceContext.InternalRequest != null)
			{
				flag = resourceContext.InternalRequest.Options.NullDynamicPropertyIsEnabled;
			}
			IEdmStructuredType structuredType = resourceContext.StructuredType;
			IEdmStructuredObject edmObject = resourceContext.EdmObject;
			object obj;
			if (!(edmObject is IDelta))
			{
				PropertyInfo dynamicPropertyDictionary = EdmLibHelpers.GetDynamicPropertyDictionary(structuredType, resourceContext.EdmModel);
				if (dynamicPropertyDictionary == null || edmObject == null || !edmObject.TryGetPropertyValue(dynamicPropertyDictionary.Name, out obj) || obj == null)
				{
					return;
				}
			}
			else
			{
				obj = ((EdmStructuredObject)edmObject).TryGetDynamicProperties();
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = (IDictionary<string, object>)obj;
			HashSet<string> hashSet = new HashSet<string>(resource.Properties.Select((ODataProperty p) => p.Name));
			List<ODataProperty> list = new List<ODataProperty>();
			foreach (KeyValuePair<string, object> keyValuePair in enumerable.Where((KeyValuePair<string, object> x) => selectExpandNode.SelectedDynamicProperties == null || selectExpandNode.SelectedDynamicProperties.Contains(x.Key)))
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key))
				{
					if (keyValuePair.Value == null)
					{
						if (flag)
						{
							list.Add(new ODataProperty
							{
								Name = keyValuePair.Key,
								Value = new ODataNullValue()
							});
						}
					}
					else
					{
						if (hashSet.Contains(keyValuePair.Key))
						{
							throw Error.InvalidOperation(SRResources.DynamicPropertyNameAlreadyUsedAsDeclaredPropertyName, new object[]
							{
								keyValuePair.Key,
								structuredType.FullTypeName()
							});
						}
						IEdmTypeReference edmType = resourceContext.SerializerContext.GetEdmType(keyValuePair.Value, keyValuePair.Value.GetType());
						if (edmType == null)
						{
							throw Error.NotSupported(SRResources.TypeOfDynamicPropertyNotSupported, new object[]
							{
								keyValuePair.Value.GetType().FullName,
								keyValuePair.Key
							});
						}
						if (edmType.IsStructured() || (edmType.IsCollection() && edmType.AsCollection().ElementType().IsStructured()))
						{
							if (resourceContext.DynamicComplexProperties == null)
							{
								resourceContext.DynamicComplexProperties = new ConcurrentDictionary<string, object>();
							}
							resourceContext.DynamicComplexProperties.Add(keyValuePair);
						}
						else
						{
							ODataEdmTypeSerializer edmTypeSerializer = base.SerializerProvider.GetEdmTypeSerializer(edmType);
							if (edmTypeSerializer == null)
							{
								throw Error.NotSupported(SRResources.DynamicPropertyCannotBeSerialized, new object[]
								{
									keyValuePair.Key,
									edmType.FullName()
								});
							}
							list.Add(edmTypeSerializer.CreateProperty(keyValuePair.Value, edmType, keyValuePair.Key, resourceContext.SerializerContext));
						}
					}
				}
			}
			if (list.Any<ODataProperty>())
			{
				resource.Properties = resource.Properties.Concat(list);
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00038FA8 File Offset: 0x000371A8
		public virtual string CreateETag(ResourceContext resourceContext)
		{
			if (resourceContext.InternalRequest != null)
			{
				IEdmModel edmModel = resourceContext.EdmModel;
				IEdmNavigationSource navigationSource = resourceContext.NavigationSource;
				IEnumerable<IEdmStructuralProperty> enumerable;
				if (edmModel != null && navigationSource != null)
				{
					enumerable = from c in edmModel.GetConcurrencyProperties(navigationSource)
						orderby c.Name
						select c;
				}
				else
				{
					enumerable = Enumerable.Empty<IEdmStructuralProperty>();
				}
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (IEdmStructuralProperty edmStructuralProperty in enumerable)
				{
					dictionary.Add(edmStructuralProperty.Name, resourceContext.GetPropertyValue(edmStructuralProperty.Name));
				}
				return resourceContext.InternalRequest.CreateETag(dictionary);
			}
			return null;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00039070 File Offset: 0x00037270
		private void WriteNavigationLinks(SelectExpandNode selectExpandNode, ResourceContext resourceContext, ODataWriter writer)
		{
			if (selectExpandNode.SelectedNavigationProperties == null)
			{
				return;
			}
			foreach (ODataNestedResourceInfo odataNestedResourceInfo in this.CreateNavigationLinks(selectExpandNode.SelectedNavigationProperties, resourceContext))
			{
				writer.WriteStart(odataNestedResourceInfo);
				writer.WriteEnd();
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000390D4 File Offset: 0x000372D4
		private void WriteDynamicComplexProperties(ResourceContext resourceContext, ODataWriter writer)
		{
			if (resourceContext.DynamicComplexProperties == null)
			{
				return;
			}
			foreach (KeyValuePair<string, object> keyValuePair in resourceContext.DynamicComplexProperties)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key) && keyValuePair.Value != null)
				{
					IEdmTypeReference edmType = resourceContext.SerializerContext.GetEdmType(keyValuePair.Value, keyValuePair.Value.GetType());
					if (edmType.IsStructured() || (edmType.IsCollection() && edmType.AsCollection().ElementType().IsStructured()))
					{
						ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
						{
							IsCollection = new bool?(edmType.IsCollection()),
							Name = keyValuePair.Key
						};
						writer.WriteStart(odataNestedResourceInfo);
						this.WriteDynamicComplexProperty(keyValuePair.Value, edmType, resourceContext, writer);
						writer.WriteEnd();
					}
				}
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000391C8 File Offset: 0x000373C8
		private void WriteDynamicComplexProperty(object propertyValue, IEdmTypeReference edmType, ResourceContext resourceContext, ODataWriter writer)
		{
			ODataSerializerContext odataSerializerContext = new ODataSerializerContext(resourceContext, null, null);
			ODataEdmTypeSerializer edmTypeSerializer = base.SerializerProvider.GetEdmTypeSerializer(edmType);
			if (edmTypeSerializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { edmType.ToTraceString() }));
			}
			edmTypeSerializer.WriteObjectInline(propertyValue, edmType, writer, odataSerializerContext);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00039218 File Offset: 0x00037418
		private void WriteComplexProperties(SelectExpandNode selectExpandNode, ResourceContext resourceContext, ODataWriter writer)
		{
			IDictionary<IEdmStructuralProperty, PathSelectItem> dictionary = selectExpandNode.SelectedComplexTypeProperties;
			if (dictionary == null)
			{
				return;
			}
			if (resourceContext.EdmObject != null && resourceContext.EdmObject.IsDeltaResource())
			{
				IDelta delta = resourceContext.EdmObject as IDelta;
				IEnumerable<string> changedProperties = delta.GetChangedPropertyNames();
				dictionary = dictionary.Where((KeyValuePair<IEdmStructuralProperty, PathSelectItem> p) => changedProperties.Contains(p.Key.Name)).ToDictionary((KeyValuePair<IEdmStructuralProperty, PathSelectItem> a) => a.Key, (KeyValuePair<IEdmStructuralProperty, PathSelectItem> a) => a.Value);
			}
			foreach (KeyValuePair<IEdmStructuralProperty, PathSelectItem> keyValuePair in dictionary)
			{
				IEdmStructuralProperty key = keyValuePair.Key;
				ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
				{
					IsCollection = new bool?(key.Type.IsCollection()),
					Name = key.Name
				};
				writer.WriteStart(odataNestedResourceInfo);
				this.WriteComplexAndExpandedNavigationProperty(key, keyValuePair.Value, resourceContext, writer);
				writer.WriteEnd();
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00039344 File Offset: 0x00037544
		private void WriteExpandedNavigationProperties(SelectExpandNode selectExpandNode, ResourceContext resourceContext, ODataWriter writer)
		{
			IDictionary<IEdmNavigationProperty, ExpandedNavigationSelectItem> expandedProperties = selectExpandNode.ExpandedProperties;
			if (expandedProperties == null)
			{
				return;
			}
			foreach (KeyValuePair<IEdmNavigationProperty, ExpandedNavigationSelectItem> keyValuePair in expandedProperties)
			{
				IEdmNavigationProperty key = keyValuePair.Key;
				ODataNestedResourceInfo odataNestedResourceInfo = this.CreateNavigationLink(key, resourceContext);
				if (odataNestedResourceInfo != null)
				{
					writer.WriteStart(odataNestedResourceInfo);
					this.WriteComplexAndExpandedNavigationProperty(key, keyValuePair.Value, resourceContext, writer);
					writer.WriteEnd();
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000393C4 File Offset: 0x000375C4
		private void WriteReferencedNavigationProperties(SelectExpandNode selectExpandNode, ResourceContext resourceContext, ODataWriter writer)
		{
			IDictionary<IEdmNavigationProperty, ExpandedReferenceSelectItem> referencedProperties = selectExpandNode.ReferencedProperties;
			if (referencedProperties == null)
			{
				return;
			}
			foreach (KeyValuePair<IEdmNavigationProperty, ExpandedReferenceSelectItem> keyValuePair in referencedProperties)
			{
				IEdmNavigationProperty key = keyValuePair.Key;
				ODataNestedResourceInfo odataNestedResourceInfo = this.CreateNavigationLink(key, resourceContext);
				if (odataNestedResourceInfo != null)
				{
					writer.WriteStart(odataNestedResourceInfo);
					this.WriteComplexAndExpandedNavigationProperty(key, keyValuePair.Value, resourceContext, writer);
					writer.WriteEnd();
				}
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00039444 File Offset: 0x00037644
		private void WriteComplexAndExpandedNavigationProperty(IEdmProperty edmProperty, SelectItem selectItem, ResourceContext resourceContext, ODataWriter writer)
		{
			object propertyValue = resourceContext.GetPropertyValue(edmProperty.Name);
			if (propertyValue == null || propertyValue is NullEdmComplexObject)
			{
				if (edmProperty.Type.IsCollection())
				{
					writer.WriteStart(new ODataResourceSet
					{
						TypeName = edmProperty.Type.FullName()
					});
				}
				else
				{
					writer.WriteStart(null);
				}
				writer.WriteEnd();
				return;
			}
			ODataSerializerContext odataSerializerContext = new ODataSerializerContext(resourceContext, edmProperty, resourceContext.SerializerContext.QueryContext, selectItem);
			ODataEdmTypeSerializer edmTypeSerializer = base.SerializerProvider.GetEdmTypeSerializer(edmProperty.Type);
			if (edmTypeSerializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { edmProperty.Type.ToTraceString() }));
			}
			edmTypeSerializer.WriteObjectInline(propertyValue, edmProperty.Type, writer, odataSerializerContext);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00039502 File Offset: 0x00037702
		private IEnumerable<ODataNestedResourceInfo> CreateNavigationLinks(IEnumerable<IEdmNavigationProperty> navigationProperties, ResourceContext resourceContext)
		{
			foreach (IEdmNavigationProperty edmNavigationProperty in navigationProperties)
			{
				ODataNestedResourceInfo odataNestedResourceInfo = this.CreateNavigationLink(edmNavigationProperty, resourceContext);
				if (odataNestedResourceInfo != null)
				{
					yield return odataNestedResourceInfo;
				}
			}
			IEnumerator<IEdmNavigationProperty> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00039520 File Offset: 0x00037720
		public virtual ODataNestedResourceInfo CreateNavigationLink(IEdmNavigationProperty navigationProperty, ResourceContext resourceContext)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			ODataSerializerContext serializerContext = resourceContext.SerializerContext;
			IEdmNavigationSource navigationSource = serializerContext.NavigationSource;
			ODataNestedResourceInfo odataNestedResourceInfo = null;
			if (navigationSource != null)
			{
				IEdmTypeReference type = navigationProperty.Type;
				Uri uri = serializerContext.Model.GetNavigationSourceLinkBuilder(navigationSource).BuildNavigationLink(resourceContext, navigationProperty, serializerContext.MetadataLevel);
				odataNestedResourceInfo = new ODataNestedResourceInfo
				{
					IsCollection = new bool?(type.IsCollection()),
					Name = navigationProperty.Name
				};
				if (uri != null)
				{
					odataNestedResourceInfo.Url = uri;
				}
			}
			return odataNestedResourceInfo;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000395B4 File Offset: 0x000377B4
		private IEnumerable<ODataProperty> CreateStructuralPropertyBag(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
		{
			List<ODataProperty> list = new List<ODataProperty>();
			if (selectExpandNode.SelectedStructuralProperties != null)
			{
				IEnumerable<IEdmStructuralProperty> enumerable = selectExpandNode.SelectedStructuralProperties;
				if (resourceContext.EdmObject != null && resourceContext.EdmObject.IsDeltaResource())
				{
					IDelta delta = resourceContext.EdmObject as IDelta;
					IEnumerable<string> changedProperties = delta.GetChangedPropertyNames();
					enumerable = enumerable.Where((IEdmStructuralProperty p) => changedProperties.Contains(p.Name));
				}
				foreach (IEdmStructuralProperty edmStructuralProperty in enumerable)
				{
					ODataProperty odataProperty = this.CreateStructuralProperty(edmStructuralProperty, resourceContext);
					if (odataProperty != null)
					{
						list.Add(odataProperty);
					}
				}
			}
			return list;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00039674 File Offset: 0x00037874
		public virtual ODataProperty CreateStructuralProperty(IEdmStructuralProperty structuralProperty, ResourceContext resourceContext)
		{
			if (structuralProperty == null)
			{
				throw Error.ArgumentNull("structuralProperty");
			}
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			ODataSerializerContext serializerContext = resourceContext.SerializerContext;
			ODataEdmTypeSerializer edmTypeSerializer = base.SerializerProvider.GetEdmTypeSerializer(structuralProperty.Type);
			if (edmTypeSerializer == null)
			{
				throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { structuralProperty.Type.FullName() }));
			}
			object propertyValue = resourceContext.GetPropertyValue(structuralProperty.Name);
			IEdmTypeReference edmTypeReference = structuralProperty.Type;
			if (propertyValue != null && !edmTypeReference.IsPrimitive() && !edmTypeReference.IsEnum())
			{
				IEdmTypeReference edmType = serializerContext.GetEdmType(propertyValue, propertyValue.GetType());
				if (edmTypeReference != null && edmTypeReference != edmType)
				{
					edmTypeReference = edmType;
				}
			}
			return edmTypeSerializer.CreateProperty(propertyValue, edmTypeReference, structuralProperty.Name, serializerContext);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003972D File Offset: 0x0003792D
		private IEnumerable<ODataAction> CreateODataActions(IEnumerable<IEdmAction> actions, ResourceContext resourceContext)
		{
			foreach (IEdmAction edmAction in actions)
			{
				ODataAction odataAction = this.CreateODataAction(edmAction, resourceContext);
				if (odataAction != null)
				{
					yield return odataAction;
				}
			}
			IEnumerator<IEdmAction> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003974B File Offset: 0x0003794B
		private IEnumerable<ODataFunction> CreateODataFunctions(IEnumerable<IEdmFunction> functions, ResourceContext resourceContext)
		{
			foreach (IEdmFunction edmFunction in functions)
			{
				ODataFunction odataFunction = this.CreateODataFunction(edmFunction, resourceContext);
				if (odataFunction != null)
				{
					yield return odataFunction;
				}
			}
			IEnumerator<IEdmFunction> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003976C File Offset: 0x0003796C
		public virtual ODataAction CreateODataAction(IEdmAction action, ResourceContext resourceContext)
		{
			if (action == null)
			{
				throw Error.ArgumentNull("action");
			}
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			OperationLinkBuilder operationLinkBuilder = resourceContext.EdmModel.GetOperationLinkBuilder(action);
			if (operationLinkBuilder == null)
			{
				return null;
			}
			return ODataResourceSerializer.CreateODataOperation(action, operationLinkBuilder, resourceContext) as ODataAction;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000397B4 File Offset: 0x000379B4
		public virtual ODataFunction CreateODataFunction(IEdmFunction function, ResourceContext resourceContext)
		{
			if (function == null)
			{
				throw Error.ArgumentNull("function");
			}
			if (resourceContext == null)
			{
				throw Error.ArgumentNull("resourceContext");
			}
			OperationLinkBuilder operationLinkBuilder = resourceContext.EdmModel.GetOperationLinkBuilder(function);
			if (operationLinkBuilder == null)
			{
				return null;
			}
			return ODataResourceSerializer.CreateODataOperation(function, operationLinkBuilder, resourceContext) as ODataFunction;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000397FC File Offset: 0x000379FC
		private static ODataOperation CreateODataOperation(IEdmOperation operation, OperationLinkBuilder builder, ResourceContext resourceContext)
		{
			ODataMetadataLevel metadataLevel = resourceContext.SerializerContext.MetadataLevel;
			IEdmModel edmModel = resourceContext.EdmModel;
			if (ODataResourceSerializer.ShouldOmitOperation(operation, builder, metadataLevel))
			{
				return null;
			}
			Uri uri = builder.BuildLink(resourceContext);
			if (uri == null)
			{
				return null;
			}
			Uri uri2 = new Uri(new Uri(resourceContext.InternalUrlHelper.CreateODataLink(new ODataPathSegment[] { MetadataSegment.Instance })), "#" + ODataResourceSerializer.CreateMetadataFragment(operation));
			ODataOperation odataOperation;
			if (operation is IEdmAction)
			{
				odataOperation = new ODataAction();
			}
			else
			{
				odataOperation = new ODataFunction();
			}
			odataOperation.Metadata = uri2;
			if (metadataLevel == ODataMetadataLevel.FullMetadata)
			{
				ODataResourceSerializer.EmitTitle(edmModel, operation, odataOperation);
			}
			if (!builder.FollowsConventions || metadataLevel == ODataMetadataLevel.FullMetadata)
			{
				odataOperation.Target = uri;
			}
			return odataOperation;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000398B4 File Offset: 0x00037AB4
		internal static void EmitTitle(IEdmModel model, IEdmOperation operation, ODataOperation odataOperation)
		{
			OperationTitleAnnotation operationTitleAnnotation = model.GetOperationTitleAnnotation(operation);
			if (operationTitleAnnotation != null)
			{
				odataOperation.Title = operationTitleAnnotation.Title;
				return;
			}
			odataOperation.Title = operation.Name;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000398E8 File Offset: 0x00037AE8
		internal static string CreateMetadataFragment(IEdmOperation operation)
		{
			string name = operation.Name;
			return operation.Namespace + "." + name;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00039910 File Offset: 0x00037B10
		private static IEdmStructuredType GetODataPathType(ODataSerializerContext serializerContext)
		{
			if (serializerContext.EdmProperty != null)
			{
				if (serializerContext.EdmProperty.Type.IsCollection())
				{
					return serializerContext.EdmProperty.Type.AsCollection().ElementType().ToStructuredType();
				}
				return serializerContext.EdmProperty.Type.AsStructured().StructuredDefinition();
			}
			else
			{
				if (serializerContext.ExpandedResource != null)
				{
					return null;
				}
				IEdmType edmType = null;
				if (serializerContext.NavigationSource != null)
				{
					edmType = serializerContext.NavigationSource.EntityType();
					if (edmType.TypeKind == EdmTypeKind.Collection)
					{
						edmType = (edmType as IEdmCollectionType).ElementType.Definition;
					}
				}
				if (serializerContext.Path != null && (serializerContext.NavigationSource == null || serializerContext.NavigationSource == serializerContext.Path.NavigationSource))
				{
					edmType = serializerContext.Path.EdmType;
					if (edmType != null && edmType.TypeKind == EdmTypeKind.Collection)
					{
						edmType = (edmType as IEdmCollectionType).ElementType.Definition;
					}
				}
				return edmType as IEdmStructuredType;
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000399F4 File Offset: 0x00037BF4
		internal static void AddTypeNameAnnotationAsNeeded(ODataResource resource, IEdmStructuredType odataPathType, ODataMetadataLevel metadataLevel)
		{
			string text = null;
			if (!ODataResourceSerializer.ShouldSuppressTypeNameSerialization(resource, odataPathType, metadataLevel))
			{
				text = resource.TypeName;
			}
			resource.TypeAnnotation = new ODataTypeAnnotation(text);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00039A20 File Offset: 0x00037C20
		internal static void AddTypeNameAnnotationAsNeededForComplex(ODataResource resource, ODataMetadataLevel metadataLevel)
		{
			if (ODataResourceSerializer.ShouldAddTypeNameAnnotationForComplex(metadataLevel))
			{
				string text;
				if (ODataResourceSerializer.ShouldSuppressTypeNameSerializationForComplex(metadataLevel))
				{
					text = null;
				}
				else
				{
					text = resource.TypeName;
				}
				resource.TypeAnnotation = new ODataTypeAnnotation(text);
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003702C File Offset: 0x0003522C
		internal static bool ShouldAddTypeNameAnnotationForComplex(ODataMetadataLevel metadataLevel)
		{
			if (metadataLevel != ODataMetadataLevel.MinimalMetadata)
			{
				if (metadataLevel - ODataMetadataLevel.FullMetadata > 1)
				{
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003703C File Offset: 0x0003523C
		internal static bool ShouldSuppressTypeNameSerializationForComplex(ODataMetadataLevel metadataLevel)
		{
			return metadataLevel != ODataMetadataLevel.FullMetadata && metadataLevel == ODataMetadataLevel.NoMetadata;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00039A54 File Offset: 0x00037C54
		internal static bool ShouldOmitOperation(IEdmOperation operation, OperationLinkBuilder builder, ODataMetadataLevel metadataLevel)
		{
			switch (metadataLevel)
			{
			case ODataMetadataLevel.MinimalMetadata:
			case ODataMetadataLevel.NoMetadata:
				return operation.IsBound && builder.FollowsConventions;
			}
			return false;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00039A7C File Offset: 0x00037C7C
		internal static bool ShouldSuppressTypeNameSerialization(ODataResource resource, IEdmStructuredType edmType, ODataMetadataLevel metadataLevel)
		{
			switch (metadataLevel)
			{
			case ODataMetadataLevel.FullMetadata:
				return false;
			case ODataMetadataLevel.NoMetadata:
				return true;
			}
			string text = null;
			if (edmType != null)
			{
				text = edmType.FullTypeName();
			}
			return string.Equals(resource.TypeName, text, StringComparison.Ordinal);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00039ABC File Offset: 0x00037CBC
		private IEdmStructuredTypeReference GetResourceType(object graph, ODataSerializerContext writeContext)
		{
			IEdmTypeReference edmType = writeContext.GetEdmType(graph, graph.GetType());
			if (!edmType.IsStructured())
			{
				throw new SerializationException(Error.Format(SRResources.CannotWriteType, new object[]
				{
					base.GetType().Name,
					edmType.FullName()
				}));
			}
			return edmType.AsStructured();
		}

		// Token: 0x040003FE RID: 1022
		private const string Resource = "Resource";
	}
}
