using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000243 RID: 579
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to keep the logic together for better readability.")]
	internal sealed class ODataJsonLightResourceDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x00048443 File Offset: 0x00046643
		internal ODataJsonLightResourceDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000489C6 File Offset: 0x00046BC6
		internal void ReadResourceSetContentStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_CannotReadResourceSetContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000489FC File Offset: 0x00046BFC
		internal void ReadResourceSetContentEnd()
		{
			base.JsonReader.ReadEndArray();
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00048A0C File Offset: 0x00046C0C
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

		// Token: 0x0600192A RID: 6442 RVA: 0x00048A6C File Offset: 0x00046C6C
		internal ODataDeletedResource IsDeletedResource()
		{
			ODataDeletedResource odataDeletedResource = null;
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.GetPropertyName();
				if (string.CompareOrdinal("@odata.removed", text) == 0 || base.CompareSimplifiedODataAnnotation("@removed", text))
				{
					DeltaDeletedEntryReason deltaDeletedEntryReason = DeltaDeletedEntryReason.Changed;
					Uri uri = null;
					base.JsonReader.Read();
					if (base.JsonReader.NodeType != JsonNodeType.PrimitiveValue)
					{
						while (base.JsonReader.NodeType != JsonNodeType.EndObject)
						{
							if (!base.JsonReader.Read())
							{
								break;
							}
							if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("reason", base.JsonReader.GetPropertyName()) == 0)
							{
								base.JsonReader.Read();
								if (string.CompareOrdinal("deleted", base.JsonReader.ReadStringValue()) == 0)
								{
									deltaDeletedEntryReason = DeltaDeletedEntryReason.Deleted;
								}
							}
						}
					}
					else if (base.JsonReader.Value != null)
					{
						throw new ODataException(Strings.ODataJsonLightResourceDeserializer_DeltaRemovedAnnotationMustBeObject(base.JsonReader.Value));
					}
					base.JsonReader.Read();
					if (base.JsonReader.NodeType != JsonNodeType.Property)
					{
						throw new ODataException(Strings.ODataWriterCore_DeltaResourceWithoutIdOrKeyProperties);
					}
					text = base.JsonReader.GetPropertyName();
					if (string.CompareOrdinal("@odata.id", text) == 0 || base.CompareSimplifiedODataAnnotation("@id", text))
					{
						base.JsonReader.Read();
						uri = UriUtils.StringToUri(base.JsonReader.ReadStringValue());
					}
					odataDeletedResource = ReaderUtils.CreateDeletedResource(uri, deltaDeletedEntryReason);
				}
			}
			return odataDeletedResource;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00048BD4 File Offset: 0x00046DD4
		internal ODataDeletedResource ReadDeletedEntry()
		{
			Uri uri = null;
			DeltaDeletedEntryReason deltaDeletedEntryReason = DeltaDeletedEntryReason.Changed;
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("id", base.JsonReader.GetPropertyName()) == 0)
			{
				base.JsonReader.Read();
				uri = base.JsonReader.ReadUriValue();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("reason", base.JsonReader.GetPropertyName()) == 0)
			{
				base.JsonReader.Read();
				if (string.CompareOrdinal("deleted", base.JsonReader.ReadStringValue()) == 0)
				{
					deltaDeletedEntryReason = DeltaDeletedEntryReason.Deleted;
				}
			}
			while (base.JsonReader.NodeType != JsonNodeType.EndObject && base.JsonReader.Read())
			{
				if (base.JsonReader.NodeType == JsonNodeType.StartObject || base.JsonReader.NodeType == JsonNodeType.StartArray)
				{
					throw new ODataException(Strings.ODataWriterCore_NestedContentNotAllowedIn40DeletedEntry);
				}
			}
			return ReaderUtils.CreateDeletedResource(uri, deltaDeletedEntryReason);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00048CB8 File Offset: 0x00046EB8
		internal void ReadDeltaLinkSource(ODataDeltaLinkBase link)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("source", base.JsonReader.GetPropertyName()) == 0)
			{
				base.JsonReader.Read();
				link.Source = base.JsonReader.ReadUriValue();
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00048D08 File Offset: 0x00046F08
		internal void ReadDeltaLinkRelationship(ODataDeltaLinkBase link)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("relationship", base.JsonReader.GetPropertyName()) == 0)
			{
				base.JsonReader.Read();
				link.Relationship = base.JsonReader.ReadStringValue();
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00048D58 File Offset: 0x00046F58
		internal void ReadDeltaLinkTarget(ODataDeltaLinkBase link)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("target", base.JsonReader.GetPropertyName()) == 0)
			{
				base.JsonReader.Read();
				link.Target = base.JsonReader.ReadUriValue();
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00048DA8 File Offset: 0x00046FA8
		internal ODataJsonLightReaderNestedInfo ReadResourceContent(IODataJsonLightReaderResourceState resourceState)
		{
			ODataJsonLightReaderNestedInfo readerNestedResourceInfo = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ReadPropertyCustomAnnotationValue = new Func<PropertyAndAnnotationCollector, string, object>(base.ReadCustomInstanceAnnotationValue);
				base.ProcessProperty(resourceState.PropertyAndAnnotationCollector, new Func<string, object>(this.ReadEntryPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						this.ReadOverPropertyName();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						resourceState.AnyPropertyFound = true;
						readerNestedResourceInfo = this.ReadPropertyWithValue(resourceState, propertyName, false);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						resourceState.AnyPropertyFound = true;
						readerNestedResourceInfo = this.ReadPropertyWithoutValue(resourceState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						this.ReadOverPropertyName();
						object obj = this.ReadODataOrCustomInstanceAnnotationValue(resourceState, propertyParsingResult, propertyName);
						this.ApplyEntryInstanceAnnotation(resourceState, propertyName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						this.ReadOverPropertyName();
						this.ReadMetadataReferencePropertyValue(resourceState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.NestedDeltaResourceSet:
						resourceState.AnyPropertyFound = true;
						readerNestedResourceInfo = this.ReadPropertyWithValue(resourceState, propertyName, true);
						return;
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

		// Token: 0x06001930 RID: 6448 RVA: 0x00048E2C File Offset: 0x0004702C
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

		// Token: 0x06001931 RID: 6449 RVA: 0x00048E70 File Offset: 0x00047070
		internal void ValidateMediaEntity(IODataJsonLightReaderResourceState resourceState)
		{
			ODataResourceBase resource = resourceState.Resource;
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

		// Token: 0x06001932 RID: 6450 RVA: 0x00048ED4 File Offset: 0x000470D4
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
						this.ReadOverPropertyName();
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

		// Token: 0x06001933 RID: 6451 RVA: 0x00048FE0 File Offset: 0x000471E0
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

		// Token: 0x06001934 RID: 6452 RVA: 0x00049018 File Offset: 0x00047218
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

		// Token: 0x06001935 RID: 6453 RVA: 0x0004925C File Offset: 0x0004745C
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

		// Token: 0x06001936 RID: 6454 RVA: 0x00049400 File Offset: 0x00047600
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "The casts aren't actually being done multiple times, since they occur in different cases of the switch statement.")]
		internal void ApplyEntryInstanceAnnotation(IODataJsonLightReaderResourceState resourceState, string annotationName, object annotationValue)
		{
			ODataResourceBase resource = resourceState.Resource;
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

		// Token: 0x06001937 RID: 6455 RVA: 0x00049654 File Offset: 0x00047854
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

		// Token: 0x06001938 RID: 6456 RVA: 0x00049708 File Offset: 0x00047908
		internal ODataJsonLightReaderNestedInfo ReadPropertyWithoutValue(IODataJsonLightReaderResourceState resourceState, string propertyName)
		{
			ODataJsonLightReaderNestedInfo odataJsonLightReaderNestedInfo = null;
			IEdmStructuredType resourceType = resourceState.ResourceType;
			IEdmProperty edmProperty = this.ReaderValidator.ValidatePropertyDefined(propertyName, resourceType);
			if (edmProperty != null && !edmProperty.Type.IsUntyped())
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
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
					odataJsonLightReaderNestedInfo = odataJsonLightReaderNestedResourceInfo;
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
				odataJsonLightReaderNestedInfo = this.ReadUndeclaredProperty(resourceState, propertyName, false);
			}
			return odataJsonLightReaderNestedInfo;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00049800 File Offset: 0x00047A00
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

		// Token: 0x0600193A RID: 6458 RVA: 0x00049834 File Offset: 0x00047A34
		private static ODataJsonLightReaderNestedResourceInfo ReadDeferredNestedResourceInfo(IODataJsonLightReaderResourceState resourceState, string navigationPropertyName, IEdmNavigationProperty navigationProperty)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = navigationPropertyName,
				IsCollection = ((navigationProperty == null) ? null : new bool?(navigationProperty.Type.IsCollection()))
			};
			foreach (KeyValuePair<string, object> keyValuePair in resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(odataNestedResourceInfo.Name))
			{
				string key = keyValuePair.Key;
				if (!(key == "odata.navigationLink"))
				{
					if (!(key == "odata.associationLink"))
					{
						if (!(key == "odata.type"))
						{
							throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedDeferredLinkPropertyAnnotation(odataNestedResourceInfo.Name, keyValuePair.Key));
						}
						odataNestedResourceInfo.TypeAnnotation = new ODataTypeAnnotation((string)keyValuePair.Value);
					}
					else
					{
						odataNestedResourceInfo.AssociationLinkUrl = (Uri)keyValuePair.Value;
					}
				}
				else
				{
					odataNestedResourceInfo.Url = (Uri)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNestedResourceInfo.CreateDeferredLinkInfo(odataNestedResourceInfo, navigationProperty);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00049954 File Offset: 0x00047B54
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

		// Token: 0x0600193C RID: 6460 RVA: 0x00049A8C File Offset: 0x00047C8C
		private void SetEntryMediaResource(IODataJsonLightReaderResourceState resourceState, ODataStreamReferenceValue mediaResource)
		{
			ODataResourceBase resource = resourceState.Resource;
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, false);
			mediaResource.SetMetadataBuilder(resourceMetadataBuilderForReader, null);
			resource.MediaResource = mediaResource;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00049AD0 File Offset: 0x00047CD0
		private ODataJsonLightReaderNestedInfo ReadPropertyWithValue(IODataJsonLightReaderResourceState resourceState, string propertyName, bool isDeltaResourceSet)
		{
			ODataJsonLightReaderNestedInfo odataJsonLightReaderNestedInfo = null;
			IEdmStructuredType resourceType = resourceState.ResourceType;
			IEdmProperty edmProperty = this.ReaderValidator.ValidatePropertyDefined(propertyName, resourceType);
			bool flag = edmProperty != null && edmProperty.Type.IsCollection();
			IEdmStructuralProperty edmStructuralProperty = edmProperty as IEdmStructuralProperty;
			if (edmStructuralProperty != null)
			{
				ODataJsonLightReaderNestedInfo odataJsonLightReaderNestedInfo2 = this.TryReadAsStream(resourceState, edmStructuralProperty, edmStructuralProperty.Type, edmStructuralProperty.Name);
				if (odataJsonLightReaderNestedInfo2 != null)
				{
					return odataJsonLightReaderNestedInfo2;
				}
			}
			if (edmProperty != null && !edmProperty.Type.IsUntyped())
			{
				this.ReadOverPropertyName();
				IEdmStructuredType edmStructuredType = ((edmStructuralProperty == null) ? null : edmStructuralProperty.Type.ToStructuredType());
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmStructuredType != null)
				{
					ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag), propertyName);
					ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo;
					if (flag)
					{
						odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceSetNestedResourceInfo(resourceState, edmStructuralProperty, edmStructuredType, edmStructuralProperty.Name);
					}
					else
					{
						odataJsonLightReaderNestedResourceInfo = ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceNestedResourceInfo(resourceState, edmStructuralProperty, edmStructuredType, edmStructuralProperty.Name);
					}
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
					odataJsonLightReaderNestedInfo = odataJsonLightReaderNestedResourceInfo;
				}
				else if (edmNavigationProperty != null)
				{
					ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag), propertyName);
					ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo2;
					if (flag)
					{
						odataJsonLightReaderNestedResourceInfo2 = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceSetNestedResourceInfo(resourceState, edmNavigationProperty, edmNavigationProperty.Type.ToStructuredType(), propertyName, isDeltaResourceSet) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, true));
					}
					else
					{
						odataJsonLightReaderNestedResourceInfo2 = (base.ReadingResponse ? ODataJsonLightPropertyAndValueDeserializer.ReadExpandedResourceNestedResourceInfo(resourceState, edmNavigationProperty, propertyName, edmNavigationProperty.Type.ToStructuredType(), base.MessageReaderSettings) : ODataJsonLightPropertyAndValueDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(resourceState, edmNavigationProperty, propertyName, true));
					}
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo2.NestedResourceInfo);
					odataJsonLightReaderNestedInfo = odataJsonLightReaderNestedResourceInfo2;
				}
				else
				{
					IEnumerable<string> derivedTypeConstraints = base.JsonLightInputContext.Model.GetDerivedTypeConstraints(edmProperty);
					if (derivedTypeConstraints != null)
					{
						resourceState.PropertyAndAnnotationCollector.SetDerivedTypeValidator(propertyName, new DerivedTypeValidator(edmProperty.Type.Definition, derivedTypeConstraints, "property", propertyName));
					}
					this.ReadEntryDataProperty(resourceState, edmProperty, ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(resourceState.PropertyAndAnnotationCollector, propertyName));
				}
			}
			else
			{
				odataJsonLightReaderNestedInfo = this.ReadUndeclaredProperty(resourceState, propertyName, true);
			}
			return odataJsonLightReaderNestedInfo;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00049CBC File Offset: 0x00047EBC
		private ODataJsonLightReaderNestedInfo TryReadAsStream(IODataJsonLightReaderResourceState resourceState, IEdmStructuralProperty property, IEdmTypeReference propertyType, string propertyName)
		{
			IEdmPrimitiveType edmPrimitiveType = null;
			bool flag;
			if (propertyType != null)
			{
				edmPrimitiveType = propertyType.Definition.AsElementType() as IEdmPrimitiveType;
				flag = propertyType.IsCollection();
			}
			else
			{
				flag = base.JsonReader.NodeType != JsonNodeType.PrimitiveValue;
			}
			Func<IEdmPrimitiveType, bool, string, IEdmProperty, bool> readAsStreamFunc = base.MessageReaderSettings.ReadAsStreamFunc;
			if ((edmPrimitiveType == null || (!edmPrimitiveType.IsStream() && (readAsStreamFunc == null || (property != null && property.IsKey()) || (!edmPrimitiveType.IsBinary() && !edmPrimitiveType.IsString() && !flag) || !readAsStreamFunc(edmPrimitiveType, flag, propertyName, property)))) && ((propertyType != null && !propertyType.Definition.AsElementType().IsUntyped()) || (!flag && !base.JsonReader.CanStream()) || readAsStreamFunc == null || !readAsStreamFunc(null, flag, propertyName, property)))
			{
				return null;
			}
			if (flag)
			{
				this.ReadOverPropertyName();
				IEdmType edmType;
				if (propertyType != null)
				{
					edmType = propertyType.Definition.AsElementType();
				}
				else
				{
					IEdmType untypedType = EdmCoreModel.Instance.GetUntypedType();
					edmType = untypedType;
				}
				IEdmType edmType2 = edmType;
				return ODataJsonLightPropertyAndValueDeserializer.ReadStreamCollectionNestedResourceInfo(resourceState, property, propertyName, edmType2);
			}
			ODataPropertyInfo odataPropertyInfo;
			if (edmPrimitiveType != null && edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Stream)
			{
				ODataStreamPropertyInfo odataStreamPropertyInfo = this.ReadStreamPropertyInfo(resourceState, propertyName);
				if (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					bool flag2 = false;
					if (odataStreamPropertyInfo.ContentType != null)
					{
						if (odataStreamPropertyInfo.ContentType.Contains("application/json"))
						{
							flag2 = true;
						}
					}
					else if (property != null)
					{
						IEdmVocabularyAnnotation edmVocabularyAnnotation = property.VocabularyAnnotations(base.Model).FirstOrDefault((IEdmVocabularyAnnotation a) => a.Term == CoreVocabularyModel.MediaTypeTerm);
						if (edmVocabularyAnnotation != null)
						{
							IEdmStringConstantExpression edmStringConstantExpression = edmVocabularyAnnotation.Value as IEdmStringConstantExpression;
							if (edmStringConstantExpression != null && edmStringConstantExpression.Value.Contains("application/json"))
							{
								flag2 = true;
							}
						}
					}
					if (!flag2)
					{
						this.ReadOverPropertyName();
					}
				}
				ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(resourceState, propertyName);
				ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, odataStreamReferenceValue);
				odataPropertyInfo = odataStreamPropertyInfo;
			}
			else
			{
				this.ReadOverPropertyName();
				odataPropertyInfo = new ODataPropertyInfo
				{
					PrimitiveTypeKind = ((edmPrimitiveType == null) ? EdmPrimitiveTypeKind.None : edmPrimitiveType.PrimitiveKind),
					Name = propertyName
				};
			}
			return new ODataJsonLightReaderNestedPropertyInfo(odataPropertyInfo, property);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00049EC2 File Offset: 0x000480C2
		private void ReadOverPropertyName()
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.JsonReader.Read();
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00049EDE File Offset: 0x000480DE
		private static bool IsJsonStream(ODataStreamPropertyInfo streamPropertyInfo)
		{
			return streamPropertyInfo.ContentType != null && streamPropertyInfo.ContentType.Contains("application/json");
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00049EFC File Offset: 0x000480FC
		private void ReadEntryDataProperty(IODataJsonLightReaderResourceState resourceState, IEdmProperty edmProperty, string propertyTypeName)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			object obj = base.ReadNonEntityValue(propertyTypeName, edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, edmProperty.Name, null);
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00049F5C File Offset: 0x0004815C
		private ODataJsonLightReaderNestedInfo InnerReadUndeclaredProperty(IODataJsonLightReaderResourceState resourceState, IEdmStructuredType owningStructuredType, string propertyName, bool propertyWithValue)
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
				edmTypeReference = this.ReaderValidator.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, null, text2, base.Model, new Func<EdmTypeKind>(base.GetNonEntityValueKind), out edmTypeKind2, out odataTypeAnnotation);
			}
			ODataJsonLightReaderNestedInfo odataJsonLightReaderNestedInfo = this.TryReadAsStream(resourceState, null, edmTypeReference, propertyName);
			if (odataJsonLightReaderNestedInfo != null)
			{
				return odataJsonLightReaderNestedInfo;
			}
			edmTypeReference = ODataJsonLightPropertyAndValueDeserializer.ResolveUntypedType(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference, base.MessageReaderSettings.PrimitiveTypeResolver, base.MessageReaderSettings.ReadUntypedAsString, !base.MessageReaderSettings.ThrowIfTypeConflictsWithMetadata);
			bool flag2 = edmTypeReference.IsCollection();
			IEdmStructuredType edmStructuredType = edmTypeReference.ToStructuredType();
			if (edmStructuredType == null)
			{
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
			ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, new bool?(flag2), propertyName);
			if (flag2)
			{
				return ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceSetNestedResourceInfo(resourceState, null, edmStructuredType, propertyName);
			}
			return ODataJsonLightPropertyAndValueDeserializer.ReadNonExpandedResourceNestedResourceInfo(resourceState, null, edmStructuredType, propertyName);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0004A0D0 File Offset: 0x000482D0
		private ODataJsonLightReaderNestedInfo ReadUndeclaredProperty(IODataJsonLightReaderResourceState resourceState, string propertyName, bool propertyWithValue)
		{
			IDictionary<string, object> odataPropertyAnnotations = resourceState.PropertyAndAnnotationCollector.GetODataPropertyAnnotations(propertyName);
			object obj;
			if (odataPropertyAnnotations.TryGetValue("odata.mediaEditLink", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaReadLink", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaContentType", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaEtag", out obj))
			{
				ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(resourceState, propertyName);
				ODataJsonLightPropertyAndValueDeserializer.AddResourceProperty(resourceState, propertyName, odataStreamReferenceValue);
				if (propertyWithValue)
				{
					ODataStreamPropertyInfo odataStreamPropertyInfo = this.ReadStreamPropertyInfo(resourceState, propertyName);
					if (!ODataJsonLightResourceDeserializer.IsJsonStream(odataStreamPropertyInfo))
					{
						base.JsonReader.Read();
					}
					return new ODataJsonLightReaderNestedPropertyInfo(odataStreamPropertyInfo, null);
				}
				return null;
			}
			else
			{
				if (propertyWithValue)
				{
					base.JsonReader.Read();
				}
				if (odataPropertyAnnotations.TryGetValue("odata.navigationLink", out obj) || odataPropertyAnnotations.TryGetValue("odata.associationLink", out obj))
				{
					ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = ODataJsonLightResourceDeserializer.ReadDeferredNestedResourceInfo(resourceState, propertyName, null);
					resourceState.PropertyAndAnnotationCollector.ValidatePropertyUniquenessOnNestedResourceInfoStart(odataJsonLightReaderNestedResourceInfo.NestedResourceInfo);
					if (propertyWithValue)
					{
						ODataJsonLightPropertyAndValueDeserializer.ValidateExpandedNestedResourceInfoPropertyValue(base.JsonReader, null, propertyName);
						base.JsonReader.SkipValue();
					}
					return odataJsonLightReaderNestedResourceInfo;
				}
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

		// Token: 0x06001944 RID: 6468 RVA: 0x0004A230 File Offset: 0x00048430
		private ODataStreamReferenceValue ReadStreamPropertyValue(IODataJsonLightReaderResourceState resourceState, string streamPropertyName)
		{
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			this.ReadStreamInfo(odataStreamReferenceValue, resourceState, streamPropertyName);
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, false);
			odataStreamReferenceValue.SetMetadataBuilder(resourceMetadataBuilderForReader, streamPropertyName);
			return odataStreamReferenceValue;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004A274 File Offset: 0x00048474
		private ODataStreamPropertyInfo ReadStreamPropertyInfo(IODataJsonLightReaderResourceState resourceState, string streamPropertyName)
		{
			ODataStreamPropertyInfo odataStreamPropertyInfo = new ODataStreamPropertyInfo
			{
				Name = streamPropertyName
			};
			this.ReadStreamInfo(odataStreamPropertyInfo, resourceState, streamPropertyName);
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, false);
			odataStreamPropertyInfo.SetMetadataBuilder(resourceMetadataBuilderForReader, streamPropertyName);
			return odataStreamPropertyInfo;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0004A2C0 File Offset: 0x000484C0
		private void ReadStreamInfo(IODataStreamReferenceInfo streamInfo, IODataJsonLightReaderResourceState resourceState, string streamPropertyName)
		{
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
								if (!(key == "odata.type"))
								{
									throw new ODataException(Strings.ODataJsonLightResourceDeserializer_UnexpectedStreamPropertyAnnotation(streamPropertyName, keyValuePair.Key));
								}
							}
							else
							{
								streamInfo.ContentType = (string)keyValuePair.Value;
							}
						}
						else
						{
							streamInfo.ETag = (string)keyValuePair.Value;
						}
					}
					else
					{
						streamInfo.ReadLink = (Uri)keyValuePair.Value;
					}
				}
				else
				{
					streamInfo.EditLink = (Uri)keyValuePair.Value;
				}
			}
			if (!base.ReadingResponse && (streamInfo.ETag != null || streamInfo.EditLink != null || streamInfo.ReadLink != null))
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_StreamPropertyInRequest(streamPropertyName));
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004A3F8 File Offset: 0x000485F8
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

		// Token: 0x06001948 RID: 6472 RVA: 0x0004A570 File Offset: 0x00048770
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

		// Token: 0x06001949 RID: 6473 RVA: 0x0004A6E0 File Offset: 0x000488E0
		private void SetMetadataBuilder(IODataJsonLightReaderResourceState resourceState, ODataOperation operation)
		{
			ODataResourceMetadataBuilder resourceMetadataBuilderForReader = base.MetadataContext.GetResourceMetadataBuilderForReader(resourceState, base.JsonLightInputContext.ODataSimplifiedOptions.EnableReadingKeyAsSegment, false);
			operation.SetMetadataBuilder(resourceMetadataBuilderForReader, base.ContextUriParseResult.MetadataDocumentUri);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0004A720 File Offset: 0x00048920
		private ODataOperation CreateODataOperationAndAddToEntry(IODataJsonOperationsDeserializerContext readerContext, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName).FirstOrDefault<IEdmOperation>();
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

		// Token: 0x0600194B RID: 6475 RVA: 0x0004A790 File Offset: 0x00048990
		private ODataOperation CreateODataOperationAndAddToResourceSet(ODataResourceSet resourceSet, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.ContextUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEdmOperation edmOperation = base.JsonLightInputContext.Model.ResolveOperations(uriFragmentFromMetadataReferencePropertyName).FirstOrDefault<IEdmOperation>();
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

		// Token: 0x0600194C RID: 6476 RVA: 0x0004A800 File Offset: 0x00048A00
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

		// Token: 0x0600194D RID: 6477 RVA: 0x0004A878 File Offset: 0x00048A78
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

		// Token: 0x0600194E RID: 6478 RVA: 0x0004A8E1 File Offset: 0x00048AE1
		private void ValidateCanReadMetadataReferenceProperty()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightResourceDeserializer_MetadataReferencePropertyInRequest);
			}
		}

		// Token: 0x020003FC RID: 1020
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x060020FC RID: 8444 RVA: 0x0005CA85 File Offset: 0x0005AC85
			public OperationsDeserializerContext(ODataResourceBase resource, ODataJsonLightResourceDeserializer jsonLightResourceDeserializer)
			{
				this.resource = resource;
				this.jsonLightResourceDeserializer = jsonLightResourceDeserializer;
			}

			// Token: 0x1700064A RID: 1610
			// (get) Token: 0x060020FD RID: 8445 RVA: 0x0005CA9B File Offset: 0x0005AC9B
			public IJsonReader JsonReader
			{
				get
				{
					return this.jsonLightResourceDeserializer.JsonReader;
				}
			}

			// Token: 0x060020FE RID: 8446 RVA: 0x0005CAA8 File Offset: 0x0005ACA8
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.jsonLightResourceDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x060020FF RID: 8447 RVA: 0x0005CAB6 File Offset: 0x0005ACB6
			public void AddActionToResource(ODataAction action)
			{
				this.resource.AddAction(action);
			}

			// Token: 0x06002100 RID: 8448 RVA: 0x0005CAC4 File Offset: 0x0005ACC4
			public void AddFunctionToResource(ODataFunction function)
			{
				this.resource.AddFunction(function);
			}

			// Token: 0x04000FB7 RID: 4023
			private ODataResourceBase resource;

			// Token: 0x04000FB8 RID: 4024
			private ODataJsonLightResourceDeserializer jsonLightResourceDeserializer;
		}
	}
}
