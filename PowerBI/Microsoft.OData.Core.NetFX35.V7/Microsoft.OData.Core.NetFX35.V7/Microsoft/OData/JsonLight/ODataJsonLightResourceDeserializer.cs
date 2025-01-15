using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200020A RID: 522
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to keep the logic together for better readability.")]
	internal sealed class ODataJsonLightResourceDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x0003C017 File Offset: 0x0003A217
		internal ODataJsonLightResourceDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003C50A File Offset: 0x0003A70A
		internal void ReadResourceSetContentStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0003C540 File Offset: 0x0003A740
		internal void ReadResourceSetContentEnd()
		{
			base.JsonReader.ReadEndArray();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0003C550 File Offset: 0x0003A750
		internal void ReadResourceTypeName(IODataJsonLightReaderResourceState resourceState)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string propertyName = base.JsonReader.GetPropertyName();
				if (string.CompareOrdinal("@odata.type", propertyName) == 0 || base.CompareSimplifiedODataAnnotation("@type", propertyName))
				{
					base.JsonReader.Read();
					resourceState.Resource.TypeName = base.ReadODataTypeAnnotationValue();
				}
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0003C5B0 File Offset: 0x0003A7B0
		internal ODataJsonLightReaderNestedResourceInfo ReadResourceContent(IODataJsonLightReaderResourceState resourceState)
		{
			ODataJsonLightReaderNestedResourceInfo readerNestedResourceInfo = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ReadPropertyCustomAnnotationValue = new Func<PropertyAndAnnotationCollector, string, object>(base.ReadCustomInstanceAnnotationValue);
				base.ProcessProperty(resourceState.PropertyAndAnnotationCollector, new Func<string, object>(this.ReadEntryPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						resourceState.AnyPropertyFound = true;
						readerNestedResourceInfo = this.ReadPropertyWithValue(resourceState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						resourceState.AnyPropertyFound = true;
						readerNestedResourceInfo = this.ReadPropertyWithoutValue(resourceState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						object obj = this.ReadODataOrCustomInstanceAnnotationValue(resourceState, propertyParsingResult, propertyName);
						this.ApplyEntryInstanceAnnotation(resourceState, propertyName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						this.ReadMetadataReferencePropertyValue(resourceState, propertyName);
						break;
					default:
						return;
					}
				});
				if (readerNestedResourceInfo != null)
				{
					break;
				}
			}
			return readerNestedResourceInfo;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0003C634 File Offset: 0x0003A834
		internal object ReadODataOrCustomInstanceAnnotationValue(IODataJsonLightReaderResourceState resourceState, ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string annotationName)
		{
			object obj = this.ReadEntryInstanceAnnotation(annotationName, resourceState.AnyPropertyFound, true, resourceState.PropertyAndAnnotationCollector);
			if (propertyParsingResult == ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation)
			{
				resourceState.PropertyAndAnnotationCollector.AddODataScopeAnnotation(annotationName, obj);
			}
			else
			{
				resourceState.PropertyAndAnnotationCollector.AddCustomScopeAnnotation(annotationName, obj);
			}
			return obj;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0003C678 File Offset: 0x0003A878
		internal void ValidateMediaEntity(IODataJsonLightReaderResourceState resourceState)
		{
			ODataResource resource = resourceState.Resource;
			if (resource != null)
			{
				IEdmEntityType edmEntityType = resourceState.ResourceType as IEdmEntityType;
				if (edmEntityType != null)
				{
					if (!base.ReadingResponse && edmEntityType.HasStream && resource.MediaResource == null)
					{
						ODataStreamReferenceValue mediaResource = resource.MediaResource;
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						this.SetEntryMediaResource(resourceState, mediaResource);
					}
					this.ReaderValidator.ValidateMediaResource(resource, edmEntityType);
				}
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		internal void ReadTopLevelResourceSetAnnotations(ODataResourceSetBase resourceSet, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool forResourceSetStart, bool readAllResourceSetProperties)
		{
			bool buffering = false;
			try
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					bool foundValueProperty = false;
					if (!forResourceSetStart & readAllResourceSetProperties)
					{
						propertyAndAnnotationCollector = new PropertyAndAnnotationCollector(false);
					}
					base.ProcessProperty(propertyAndAnnotationCollector, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
					{
						switch (propertyParseResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
							if (string.CompareOrdinal("value", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidPropertyInTopLevelResourceSet(propertyName, "value"));
							}
							if (readAllResourceSetProperties)
							{
								this.JsonReader.StartBuffering();
								buffering = true;
								this.JsonReader.SkipValue();
								return;
							}
							foundValueProperty = true;
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_InvalidPropertyAnnotationInTopLevelResourceSet(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							this.ReadODataOrCustomInstanceAnnotationValue(resourceSet, propertyAndAnnotationCollector, forResourceSetStart, readAllResourceSetProperties, propertyParseResult, propertyName);
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							if (!(resourceSet is ODataResourceSet))
							{
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
							}
							this.ReadMetadataReferencePropertyValue((ODataResourceSet)resourceSet, propertyName);
							return;
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightResourceDeserializer_ReadTopLevelResourceSetAnnotations));
						}
					});
					if (foundValueProperty)
					{
						return;
					}
				}
			}
			finally
			{
				if (buffering)
				{
					base.JsonReader.StopBuffering();
				}
			}
			if (forResourceSetStart && !readAllResourceSetProperties)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_ExpectedResourceSetPropertyNotFound("value"));
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		internal void ReadODataOrCustomInstanceAnnotationValue(ODataResourceSetBase resourceSet, PropertyAndAnnotationCollector propertyAndAnnotationCollector, bool forResourceSetStart, bool readAllResourceSetProperties, ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string annotationName)
		{
			if (propertyParseResult == ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation)
			{
				propertyAndAnnotationCollector.AddODataScopeAnnotation(annotationName, base.JsonReader.Value);
			}
			if (forResourceSetStart || !readAllResourceSetProperties)
			{
				this.ReadAndApplyResourceSetInstanceAnnotationValue(annotationName, resourceSet, propertyAndAnnotationCollector);
				return;
			}
			base.JsonReader.SkipValue();
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0003C820 File Offset: 0x0003AA20
		internal object ReadEntryPropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (base.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(propertyAnnotationName);
			if (num <= 1467962784U)
			{
				if (num <= 278645481U)
				{
					if (num != 270005791U)
					{
						if (num != 278645481U)
						{
							goto IL_022C;
						}
						if (!(propertyAnnotationName == "odata.mediaContentType"))
						{
							goto IL_022C;
						}
						goto IL_019A;
					}
					else
					{
						if (!(propertyAnnotationName == "odata.count"))
						{
							goto IL_022C;
						}
						return base.ReadAndValidateAnnotationAsLongForIeee754Compatible(propertyAnnotationName);
					}
				}
				else if (num != 1088217959U)
				{
					if (num != 1208232771U)
					{
						if (num != 1467962784U)
						{
							goto IL_022C;
						}
						if (!(propertyAnnotationName == "odata.mediaEditLink"))
						{
							goto IL_022C;
						}
					}
					else if (!(propertyAnnotationName == "odata.context"))
					{
						goto IL_022C;
					}
				}
				else
				{
					if (!(propertyAnnotationName == "odata.bind"))
					{
						goto IL_022C;
					}
					if (base.JsonReader.NodeType != JsonNodeType.StartArray)
					{
						return new ODataEntityReferenceLink
						{
							Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
						};
					}
					LinkedList<ODataEntityReferenceLink> linkedList = new LinkedList<ODataEntityReferenceLink>();
					base.JsonReader.Read();
					while (base.JsonReader.NodeType != JsonNodeType.EndArray)
					{
						linkedList.AddLast(new ODataEntityReferenceLink
						{
							Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
						});
					}
					base.JsonReader.Read();
					if (linkedList.Count == 0)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_EmptyBindArray("odata.bind"));
					}
					return linkedList;
				}
			}
			else if (num <= 1895014474U)
			{
				if (num != 1661917678U)
				{
					if (num != 1688811224U)
					{
						if (num != 1895014474U)
						{
							goto IL_022C;
						}
						if (!(propertyAnnotationName == "odata.navigationLink"))
						{
							goto IL_022C;
						}
					}
					else if (!(propertyAnnotationName == "odata.mediaReadLink"))
					{
						goto IL_022C;
					}
				}
				else
				{
					if (!(propertyAnnotationName == "odata.deltaLink"))
					{
						goto IL_022C;
					}
					goto IL_022C;
				}
			}
			else if (num != 3438618421U)
			{
				if (num != 3528111475U)
				{
					if (num != 4263018177U)
					{
						goto IL_022C;
					}
					if (!(propertyAnnotationName == "odata.mediaEtag"))
					{
						goto IL_022C;
					}
					goto IL_019A;
				}
				else if (!(propertyAnnotationName == "odata.associationLink"))
				{
					goto IL_022C;
				}
			}
			else if (!(propertyAnnotationName == "odata.nextLink"))
			{
				goto IL_022C;
			}
			return base.ReadAndValidateAnnotationStringValueAsUri(propertyAnnotationName);
			IL_019A:
			return base.ReadAndValidateAnnotationStringValue(propertyAnnotationName);
			IL_022C:
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0003CA64 File Offset: 0x0003AC64
		internal object ReadEntryInstanceAnnotation(string annotationName, bool anyPropertyFound, bool typeAnnotationFound, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(annotationName);
			if (num <= 1239564525U)
			{
				if (num <= 332581925U)
				{
					if (num != 278645481U)
					{
						if (num != 332581925U)
						{
							goto IL_0188;
						}
						if (!(annotationName == "odata.etag"))
						{
							goto IL_0188;
						}
						if (anyPropertyFound)
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty(annotationName));
						}
						return base.ReadAndValidateAnnotationStringValue(annotationName);
					}
					else
					{
						if (!(annotationName == "odata.mediaContentType"))
						{
							goto IL_0188;
						}
						goto IL_0180;
					}
				}
				else if (num != 839961940U)
				{
					if (num != 1239564525U)
					{
						goto IL_0188;
					}
					if (!(annotationName == "odata.id"))
					{
						goto IL_0188;
					}
					if (anyPropertyFound)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_ResourceInstanceAnnotationPrecededByProperty(annotationName));
					}
					return base.ReadAnnotationStringValueAsUri(annotationName);
				}
				else if (!(annotationName == "odata.readLink"))
				{
					goto IL_0188;
				}
			}
			else if (num <= 1688811224U)
			{
				if (num != 1467962784U)
				{
					if (num != 1688811224U)
					{
						goto IL_0188;
					}
					if (!(annotationName == "odata.mediaReadLink"))
					{
						goto IL_0188;
					}
				}
				else if (!(annotationName == "odata.mediaEditLink"))
				{
					goto IL_0188;
				}
			}
			else if (num != 3579993412U)
			{
				if (num != 4075776568U)
				{
					if (num != 4263018177U)
					{
						goto IL_0188;
					}
					if (!(annotationName == "odata.mediaEtag"))
					{
						goto IL_0188;
					}
					goto IL_0180;
				}
				else
				{
					if (!(annotationName == "odata.type"))
					{
						goto IL_0188;
					}
					if (!typeAnnotationFound)
					{
						return base.ReadODataTypeAnnotationValue();
					}
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_ResourceTypeAnnotationNotFirst);
				}
			}
			else if (!(annotationName == "odata.editLink"))
			{
				goto IL_0188;
			}
			return base.ReadAndValidateAnnotationStringValueAsUri(annotationName);
			IL_0180:
			return base.ReadAndValidateAnnotationStringValue(annotationName);
			IL_0188:
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			return base.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, annotationName);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0003CC08 File Offset: 0x0003AE08
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "The casts aren't actually being done multiple times, since they occur in different cases of the switch statement.")]
		internal void ApplyEntryInstanceAnnotation(IODataJsonLightReaderResourceState resourceState, string annotationName, object annotationValue)
		{
			ODataResource resource = resourceState.Resource;
			ODataStreamReferenceValue mediaResource = resource.MediaResource;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(annotationName);
			if (num <= 1239564525U)
			{
				if (num <= 332581925U)
				{
					if (num != 278645481U)
					{
						if (num == 332581925U)
						{
							if (annotationName == "odata.etag")
							{
								resource.ETag = (string)annotationValue;
								goto IL_0231;
							}
						}
					}
					else if (annotationName == "odata.mediaContentType")
					{
						ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
						mediaResource.ContentType = (string)annotationValue;
						goto IL_0231;
					}
				}
				else if (num != 839961940U)
				{
					if (num == 1239564525U)
					{
						if (annotationName == "odata.id")
						{
							if (annotationValue == null)
							{
								resource.IsTransient = true;
								goto IL_0231;
							}
							resource.Id = (Uri)annotationValue;
							goto IL_0231;
						}
					}
				}
				else if (annotationName == "odata.readLink")
				{
					resource.ReadLink = (Uri)annotationValue;
					goto IL_0231;
				}
			}
			else if (num <= 1688811224U)
			{
				if (num != 1467962784U)
				{
					if (num == 1688811224U)
					{
						if (annotationName == "odata.mediaReadLink")
						{
							ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
							mediaResource.ReadLink = (Uri)annotationValue;
							goto IL_0231;
						}
					}
				}
				else if (annotationName == "odata.mediaEditLink")
				{
					ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
					mediaResource.EditLink = (Uri)annotationValue;
					goto IL_0231;
				}
			}
			else if (num != 3579993412U)
			{
				if (num != 4075776568U)
				{
					if (num == 4263018177U)
					{
						if (annotationName == "odata.mediaEtag")
						{
							ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
							mediaResource.ETag = (string)annotationValue;
							goto IL_0231;
						}
					}
				}
				else if (annotationName == "odata.type")
				{
					resource.TypeName = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName((string)annotationValue));
					goto IL_0231;
				}
			}
			else if (annotationName == "odata.editLink")
			{
				resource.EditLink = (Uri)annotationValue;
				goto IL_0231;
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			resource.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, annotationValue.ToODataValue()));
			IL_0231:
			if (mediaResource != null && resource.MediaResource == null)
			{
				this.SetEntryMediaResource(resourceState, mediaResource);
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0003CE5C File Offset: 0x0003B05C
		internal void ReadAndApplyResourceSetInstanceAnnotationValue(string annotationName, ODataResourceSetBase resourceSet, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			if (annotationName == "odata.count")
			{
				resourceSet.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
				return;
			}
			if (annotationName == "odata.nextLink")
			{
				resourceSet.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
				return;
			}
			if (annotationName == "odata.deltaLink")
			{
				resourceSet.DeltaLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.deltaLink");
				return;
			}
			if (!(annotationName == "odata.type"))
			{
				ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
				object obj = base.ReadCustomInstanceAnnotationValue(propertyAndAnnotationCollector, annotationName);
				resourceSet.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, obj.ToODataValue()));
				return;
			}
			base.ReadAndValidateAnnotationStringValue("odata.type");
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0003CF10 File Offset: 0x0003B110
		internal ODataJsonLightReaderNestedResourceInfo ReadPropertyWithoutValue(IODataJsonLightReaderResourceState resourceState, string propertyName)
		{
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = null;
			IEdmStructuredType resourceType = resourceState.ResourceType;
			IEdmProperty edmProperty = this.ReaderValidator.ValidatePropertyDefined(propertyName, resourceType);
			if (edmProperty != null && !edmProperty.Type.IsUntyped())
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					if (base.ReadingResponse)
					{
						odataJsonLightReaderNestedResourceInfo = ODataJsonLightResourceDeserializer.ReadDeferredNestedResourceInfo(resourceState, propertyName, edmNavigationProperty);
					}
					else
					{
						odataJsonLightReaderNestedResourceInfo = (edmNavigationProperty.Type.IsCollection() ? ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, false) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, false));
						if (!odataJsonLightReaderNestedResourceInfo.HasEntityReferenceLink)
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(propertyName, "odata.bind"));
						}
					}
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (!type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_PropertyWithoutValueWithWrongType(propertyName, type.FullName()));
					}
					ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(resourceState, propertyName);
					ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, edmProperty.Name, odataStreamReferenceValue);
				}
			}
			else
			{
				odataJsonLightReaderNestedResourceInfo = this.ReadUndeclaredProperty(resourceState, propertyName, false);
			}
			return odataJsonLightReaderNestedResourceInfo;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0003D000 File Offset: 0x0003B200
		internal void ReadNextLinkAnnotationAtResourceSetEnd(ODataResourceSetBase resourceSet, ODataJsonLightReaderNestedResourceInfo expandedNestedResourceInfo, PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			if (expandedNestedResourceInfo != null)
			{
				this.ReadExpandedResourceSetAnnotationsAtResourceSetEnd(resourceSet, expandedNestedResourceInfo);
				return;
			}
			bool flag = base.JsonReader is ReorderingJsonReader;
			this.ReadTopLevelResourceSetAnnotations(resourceSet, propertyAndAnnotationCollector, false, flag);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003D034 File Offset: 0x0003B234
		private static ODataJsonLightReaderNestedResourceInfo ReadDeferredNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, string navigationPropertyName, IEdmNavigationProperty navigationProperty)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = navigationPropertyName,
				IsCollection = ((navigationProperty == null) ? default(bool?) : new bool?(navigationProperty.Type.IsCollection()))
			};
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.navigationLink"))
				{
					if (!(key == "odata.associationLink"))
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key));
					}
					odataNestedResourceInfo.AssociationLinkUrl = (Uri)keyValuePair.Value;
				}
				else
				{
					odataNestedResourceInfo.Url = (Uri)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateDeferredLinkInfo(odataNestedResourceInfo, navigationProperty);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003D124 File Offset: 0x0003B324
		private void ReadExpandedResourceSetAnnotationsAtResourceSetEnd(ODataResourceSetBase resourceSet, ODataJsonLightReaderNestedResourceInfo expandedNestedResourceInfo)
		{
			string text;
			string text2;
			while (base.JsonReader.NodeType == JsonNodeType.Property && ODataJsonLightDeserializer.TryParsePropertyAnnotation(base.JsonReader.GetPropertyName(), out text, out text2) && string.CompareOrdinal(text, expandedNestedResourceInfo.NestedResourceInfo.Name) == 0)
			{
				if (!base.ReadingResponse)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(text, text2));
				}
				base.JsonReader.Read();
				string text3 = base.CompleteSimplifiedODataAnnotation(text2);
				if (!(text3 == "odata.nextLink"))
				{
					if (!(text3 == "odata.count"))
					{
						if (!(text3 == "odata.deltaLink"))
						{
						}
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedPropertyAnnotationAfterExpandedResourceSet(text2, expandedNestedResourceInfo.NestedResourceInfo.Name));
					}
					if (resourceSet.Count != null)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation("odata.count", expandedNestedResourceInfo.NestedResourceInfo.Name));
					}
					resourceSet.Count = new long?(base.ReadAndValidateAnnotationAsLongForIeee754Compatible("odata.count"));
				}
				else
				{
					if (resourceSet.NextPageLink != null)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_DuplicateNestedResourceSetAnnotation("odata.nextLink", expandedNestedResourceInfo.NestedResourceInfo.Name));
					}
					resourceSet.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
				}
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0003D25C File Offset: 0x0003B45C
		private void SetEntryMediaResource(IODataJsonLightReaderResourceState resourceState, ODataStreamReferenceValue mediaResource)
		{
			ODataResource resource = resourceState.Resource;
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment);
			mediaResource.SetMetadataBuilder(resourceMetadataBuilderForReader, null);
			resource.MediaResource = mediaResource;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0003D29C File Offset: 0x0003B49C
		private ODataJsonLightReaderNestedResourceInfo ReadPropertyWithValue(IODataJsonLightReaderResourceState resourceState, string propertyName)
		{
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = null;
			IEdmStructuredType resourceType = resourceState.ResourceType;
			IEdmProperty edmProperty = this.ReaderValidator.ValidatePropertyDefined(propertyName, resourceType);
			if (edmProperty != null && !edmProperty.Type.IsUntyped())
			{
				IEdmStructuralProperty edmStructuralProperty = edmProperty as IEdmStructuralProperty;
				IEdmStructuredType edmStructuredType = ((edmStructuralProperty == null) ? null : edmStructuralProperty.Type.ToStructuredType());
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmStructuredType != null)
				{
					bool flag = edmStructuralProperty.Type.IsCollection();
					ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag), propertyName);
					if (flag)
					{
						odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceSetNestedResourceInfo(resourceState, edmStructuralProperty, edmStructuredType, edmStructuralProperty.Name);
					}
					else
					{
						odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceNestedResourceInfo(resourceState, edmStructuralProperty, edmStructuredType, edmStructuralProperty.Name);
					}
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
				}
				else if (edmNavigationProperty != null)
				{
					bool flag2 = edmNavigationProperty.Type.IsCollection();
					ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag2), propertyName);
					if (flag2)
					{
						odataJsonLightReaderNestedResourceInfo = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceSetNestedResourceInfo(resourceState, edmNavigationProperty, edmNavigationProperty.Type.ToStructuredType(), propertyName) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, true));
					}
					else
					{
						odataJsonLightReaderNestedResourceInfo = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceNestedResourceInfo(resourceState, edmNavigationProperty, propertyName, edmNavigationProperty.Type.ToStructuredType(), base.MessageReaderSettings) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, true));
					}
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_StreamPropertyWithValue(propertyName));
					}
					this.ReadEntryDataProperty(resourceState, edmProperty, ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(resourceState.PropertyAndAnnotationCollector, propertyName));
				}
			}
			else
			{
				odataJsonLightReaderNestedResourceInfo = this.ReadUndeclaredProperty(resourceState, propertyName, true);
			}
			return odataJsonLightReaderNestedResourceInfo;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0003D434 File Offset: 0x0003B634
		private void ReadEntryDataProperty(IODataJsonLightReaderResourceState resourceState, IEdmProperty edmProperty, string propertyTypeName)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			object obj = base.ReadNonEntityValue(propertyTypeName, edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, edmProperty.Name, default(bool?));
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, edmProperty.Name, obj);
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0003D494 File Offset: 0x0003B694
		private ODataJsonLightReaderNestedResourceInfo InnerReadUndeclaredProperty(IODataJsonLightReaderResourceState resourceState, IEdmStructuredType owningStructuredType, string propertyName, bool propertyWithValue)
		{
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_OpenPropertyWithoutValue(propertyName));
			}
			bool flag = false;
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(resourceState.PropertyAndAnnotationCollector, propertyName);
			string text2 = base.TryReadOrPeekPayloadType(resourceState.PropertyAndAnnotationCollector, propertyName, flag);
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(base.Model, null, text2, EdmTypeKind.Complex, base.MessageReaderSettings.ClientCustomTypeResolver, out edmTypeKind);
			IEdmTypeReference edmTypeReference = null;
			if (!string.IsNullOrEmpty(text2) && edmType != null)
			{
				EdmTypeKind edmTypeKind2;
				ODataTypeAnnotation odataTypeAnnotation;
				edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, default(bool?), null, null, text2, base.Model, new Func<EdmTypeKind>(base.GetNonEntityValueKind), out edmTypeKind2, out odataTypeAnnotation);
			}
			edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			IEdmStructuredType edmStructuredType = edmTypeReference.ToStructuredType();
			if (edmStructuredType != null)
			{
				bool flag2 = edmTypeReference.IsCollection();
				ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag2), propertyName);
				ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
				if (flag2)
				{
					odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceSetNestedResourceInfo(resourceState, null, edmStructuredType, propertyName);
				}
				else
				{
					odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceNestedResourceInfo(resourceState, null, edmStructuredType, propertyName);
				}
				return odataJsonLightReaderNestedResourceInfo;
			}
			object obj;
			if (!(edmTypeReference is IEdmUntypedTypeReference))
			{
				obj = base.ReadNonEntityValue(text, edmTypeReference, null, null, true, false, false, propertyName, new bool?(true));
			}
			else
			{
				obj = base.JsonReader.ReadAsUntypedOrNullValue();
			}
			ValidationUtils.ValidateOpenPropertyValue(propertyName, obj);
			ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, obj);
			return null;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0003D5FC File Offset: 0x0003B7FC
		private ODataJsonLightReaderNestedResourceInfo ReadUndeclaredProperty(IODataJsonLightReaderResourceState resourceState, string propertyName, bool propertyWithValue)
		{
			IDictionary<string, object> odataPropertyAnnotations = resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName);
			object obj;
			if (odataPropertyAnnotations.TryGetValue("odata.navigationLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.associationLink", ref obj))
			{
				ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = ODataJsonLightResourceDeserializer.ReadDeferredNestedResourceInfo(resourceState, propertyName, null);
				resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
				if (propertyWithValue)
				{
					ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, default(bool?), propertyName);
					base.JsonReader.SkipValue();
				}
				return odataJsonLightReaderNestedResourceInfo;
			}
			if (odataPropertyAnnotations.TryGetValue("odata.mediaEditLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaReadLink", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaContentType", ref obj) || odataPropertyAnnotations.TryGetValue("odata.mediaEtag", ref obj))
			{
				if (propertyWithValue)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_StreamPropertyWithValue(propertyName));
				}
				ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(resourceState, propertyName);
				ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, odataStreamReferenceValue);
				return null;
			}
			else
			{
				if (resourceState.ResourceType.IsOpen)
				{
					return this.InnerReadUndeclaredProperty(resourceState, resourceState.ResourceType, propertyName, propertyWithValue);
				}
				if (!propertyWithValue)
				{
					throw new ODataException(Strings.ODataJsonLightResourceDeserializer_PropertyWithoutValueWithUnknownType(propertyName));
				}
				ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(resourceState.PropertyAndAnnotationCollector, propertyName);
				if (!base.MessageReaderSettings.ThrowOnUndeclaredPropertyForNonOpenType)
				{
					bool flag = false;
					return base.InnerReadUndeclaredProperty(resourceState, propertyName, flag);
				}
				return null;
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0003D734 File Offset: 0x0003B934
		private ODataStreamReferenceValue ReadStreamPropertyValue(IODataJsonLightReaderResourceState resourceState, string streamPropertyName)
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_StreamPropertyInRequest);
			}
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(streamPropertyName))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.mediaEditLink"))
				{
					if (!(key == "odata.mediaReadLink"))
					{
						if (!(key == "odata.mediaEtag"))
						{
							if (!(key == "odata.mediaContentType"))
							{
								throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation(streamPropertyName, keyValuePair.Key));
							}
							odataStreamReferenceValue.ContentType = (string)keyValuePair.Value;
						}
						else
						{
							odataStreamReferenceValue.ETag = (string)keyValuePair.Value;
						}
					}
					else
					{
						odataStreamReferenceValue.ReadLink = (Uri)keyValuePair.Value;
					}
				}
				else
				{
					odataStreamReferenceValue.EditLink = (Uri)keyValuePair.Value;
				}
			}
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment);
			odataStreamReferenceValue.SetMetadataBuilder(resourceMetadataBuilderForReader, streamPropertyName);
			return odataStreamReferenceValue;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003D86C File Offset: 0x0003BA6C
		private void ReadSingleOperationValue(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderResourceState resourceState, string metadataReferencePropertyName, bool insideArray)
		{
			if (readerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, readerContext.JsonReader.NodeType));
			}
			readerContext.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToEntry(readerContext, metadataReferencePropertyName);
			if (odataOperation == null)
			{
				while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
				{
					readerContext.JsonReader.ReadPropertyName();
					readerContext.JsonReader.SkipValue();
				}
				readerContext.JsonReader.ReadEndObject();
				return;
			}
			while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = ODataAnnotationNames.RemoveAnnotationPrefix(readerContext.JsonReader.ReadPropertyName());
				if (!(text == "title"))
				{
					if (!(text == "target"))
					{
						readerContext.JsonReader.SkipValue();
					}
					else
					{
						if (odataOperation.Target != null)
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
						}
						string text2 = readerContext.JsonReader.ReadStringValue("target");
						ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text2, text, metadataReferencePropertyName);
						odataOperation.Target = readerContext.ProcessUriFromPayload(text2);
					}
				}
				else
				{
					if (odataOperation.Title != null)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
					}
					string text3 = readerContext.JsonReader.ReadStringValue("title");
					ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text3, text, metadataReferencePropertyName);
					odataOperation.Title = text3;
				}
			}
			if (odataOperation.Target == null && insideArray)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_OperationMissingTargetProperty(metadataReferencePropertyName));
			}
			readerContext.JsonReader.ReadEndObject();
			this.SetMetadataBuilder(resourceState, odataOperation);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		private void ReadSingleOperationValue(ODataResourceSet resourceSet, string metadataReferencePropertyName, bool insideArray)
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToResourceSet(resourceSet, metadataReferencePropertyName);
			if (odataOperation == null)
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					base.JsonReader.ReadPropertyName();
					base.JsonReader.SkipValue();
				}
				base.JsonReader.ReadEndObject();
				return;
			}
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = ODataAnnotationNames.RemoveAnnotationPrefix(base.JsonReader.ReadPropertyName());
				if (!(text == "title"))
				{
					if (!(text == "target"))
					{
						base.JsonReader.SkipValue();
					}
					else
					{
						if (odataOperation.Target != null)
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
						}
						string text2 = base.JsonReader.ReadStringValue("target");
						ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text2, text, metadataReferencePropertyName);
						odataOperation.Target = base.ProcessUriFromPayload(text2);
					}
				}
				else
				{
					if (odataOperation.Title != null)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
					}
					string text3 = base.JsonReader.ReadStringValue("title");
					ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text3, text, metadataReferencePropertyName);
					odataOperation.Title = text3;
				}
			}
			if (odataOperation.Target == null && insideArray)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_OperationMissingTargetProperty(metadataReferencePropertyName));
			}
			base.JsonReader.ReadEndObject();
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0003DB54 File Offset: 0x0003BD54
		private void SetMetadataBuilder(IODataJsonLightReaderResourceState resourceState, ODataOperation operation)
		{
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment);
			operation.SetMetadataBuilder(resourceMetadataBuilderForReader, base.ContextUriParseResult.MetadataDocumentUri);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0003DB90 File Offset: 0x0003BD90
		private ODataOperation CreateODataOperationAndAddToEntry(IODataJsonOperationsDeserializerContext readerContext, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = Enumerable.FirstOrDefault<IEdmOperation>(base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName));
			if (edmOperation == null)
			{
				return null;
			}
			bool flag;
			ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmOperation, out flag);
			if (flag)
			{
				readerContext.AddActionToResource((ODataAction)odataOperation);
			}
			else
			{
				readerContext.AddFunctionToResource((ODataFunction)odataOperation);
			}
			return odataOperation;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0003DC00 File Offset: 0x0003BE00
		private ODataOperation CreateODataOperationAndAddToResourceSet(ODataResourceSet resourceSet, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = Enumerable.FirstOrDefault<IEdmOperation>(base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName));
			if (edmOperation == null)
			{
				return null;
			}
			bool flag;
			ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmOperation, out flag);
			if (flag)
			{
				resourceSet.AddAction((ODataAction)odataOperation);
			}
			else
			{
				resourceSet.AddFunction((ODataFunction)odataOperation);
			}
			return odataOperation;
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0003DC70 File Offset: 0x0003BE70
		private void ReadMetadataReferencePropertyValue(IODataJsonLightReaderResourceState resourceState, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IODataJsonOperationsDeserializerContext iodataJsonOperationsDeserializerContext = new ODataJsonLightResourceDeserializer.OperationsDeserializerContext(resourceState.Resource, this);
			bool flag = false;
			if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadStartArray();
				flag = true;
			}
			do
			{
				this.ReadSingleOperationValue(iodataJsonOperationsDeserializerContext, resourceState, metadataReferencePropertyName, flag);
			}
			while (flag && iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.EndArray);
			if (flag)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadEndArray();
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		private void ReadMetadataReferencePropertyValue(ODataResourceSet resourceSet, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			bool flag = false;
			if (base.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				base.JsonReader.ReadStartArray();
				flag = true;
			}
			do
			{
				this.ReadSingleOperationValue(resourceSet, metadataReferencePropertyName, flag);
			}
			while (flag && base.JsonReader.NodeType != JsonNodeType.EndArray);
			if (flag)
			{
				base.JsonReader.ReadEndArray();
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0003DD51 File Offset: 0x0003BF51
		private void ValidateCanReadMetadataReferenceProperty()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest);
			}
		}

		// Token: 0x0200033F RID: 831
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06001ABA RID: 6842 RVA: 0x0004BCA1 File Offset: 0x00049EA1
			public OperationsDeserializerContext(ODataResource resource, ODataJsonLightResourceDeserializer jsonLightResourceDeserializer)
			{
				this.resource = resource;
				this.jsonLightResourceDeserializer = jsonLightResourceDeserializer;
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0004BCB7 File Offset: 0x00049EB7
			public IJsonReader JsonReader
			{
				get
				{
					return this.jsonLightResourceDeserializer.JsonReader;
				}
			}

			// Token: 0x06001ABC RID: 6844 RVA: 0x0004BCC4 File Offset: 0x00049EC4
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.jsonLightResourceDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x06001ABD RID: 6845 RVA: 0x0004BCD2 File Offset: 0x00049ED2
			public void AddActionToResource(ODataAction action)
			{
				this.resource.AddAction(action);
			}

			// Token: 0x06001ABE RID: 6846 RVA: 0x0004BCE0 File Offset: 0x00049EE0
			public void AddFunctionToResource(ODataFunction function)
			{
				this.resource.AddFunction(function);
			}

			// Token: 0x04000D55 RID: 3413
			private ODataResource resource;

			// Token: 0x04000D56 RID: 3414
			private ODataJsonLightResourceDeserializer jsonLightResourceDeserializer;
		}
	}
}
