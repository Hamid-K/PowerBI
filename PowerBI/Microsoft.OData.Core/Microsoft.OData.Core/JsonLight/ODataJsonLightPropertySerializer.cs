using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024D RID: 589
	internal class ODataJsonLightPropertySerializer : ODataJsonLightSerializer
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x0004E7FE File Offset: 0x0004C9FE
		internal ODataJsonLightPropertySerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext, initContextUriBuilder)
		{
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this, initContextUriBuilder);
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0004E815 File Offset: 0x0004CA15
		internal ODataJsonLightValueSerializer JsonLightValueSerializer
		{
			get
			{
				return this.jsonLightValueSerializer;
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0004E820 File Offset: 0x0004CA20
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				ODataPayloadKind odataPayloadKind = (this.JsonLightOutputContext.MessageWriterSettings.IsIndividualProperty ? ODataPayloadKind.IndividualProperty : ODataPayloadKind.Property);
				if (!(this.JsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel))
				{
					ODataContextUrlInfo contextInfo = ODataContextUrlInfo.Create(property.ODataValue, this.MessageWriterSettings.Version ?? ODataVersion.V4, this.JsonLightOutputContext.MessageWriterSettings.ODataUri, this.Model);
					this.WriteContextUriProperty(odataPayloadKind, () => contextInfo, null, null);
				}
				this.WriteProperty(property, null, true, this.CreateDuplicatePropertyNameChecker(), null);
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0004E854 File Offset: 0x0004CA54
		internal void WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> properties, bool isComplexValue, IDuplicatePropertyNameChecker duplicatePropertyNameChecker, ODataResourceMetadataBuilder metadataBuilder)
		{
			if (properties == null)
			{
				return;
			}
			foreach (ODataProperty odataProperty in properties)
			{
				this.WriteProperty(odataProperty, owningType, false, duplicatePropertyNameChecker, metadataBuilder);
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0004E8A8 File Offset: 0x0004CAA8
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Splitting the code would make the logic harder to understand; class coupling is only slightly above threshold.")]
		internal void WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, IDuplicatePropertyNameChecker duplicatePropertyNameChecker, ODataResourceMetadataBuilder metadataBuilder)
		{
			this.WritePropertyInfo(property, owningType, isTopLevel, duplicatePropertyNameChecker, metadataBuilder);
			ODataValue odataValue = property.ODataValue;
			ODataUntypedValue odataUntypedValue = odataValue as ODataUntypedValue;
			if (odataUntypedValue != null)
			{
				this.WriteUntypedValue(odataUntypedValue);
				return;
			}
			ODataStreamReferenceValue odataStreamReferenceValue = odataValue as ODataStreamReferenceValue;
			if (odataStreamReferenceValue != null && !(base.JsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel))
			{
				this.WriteStreamValue(odataStreamReferenceValue, property.Name, metadataBuilder);
				return;
			}
			if (odataValue is ODataNullValue || odataValue == null)
			{
				this.WriteNullProperty(property);
				return;
			}
			bool flag = this.IsOpenProperty(property);
			ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				this.WritePrimitiveProperty(odataPrimitiveValue, flag);
				return;
			}
			ODataEnumValue odataEnumValue = odataValue as ODataEnumValue;
			if (odataEnumValue != null)
			{
				this.WriteEnumProperty(odataEnumValue, flag);
				return;
			}
			ODataResourceValue odataResourceValue = odataValue as ODataResourceValue;
			if (odataResourceValue != null)
			{
				if (isTopLevel)
				{
					throw new ODataException(Strings.ODataMessageWriter_NotAllowedWriteTopLevelPropertyWithResourceValue(property.Name));
				}
				this.WriteResourceProperty(property, odataResourceValue, flag);
				return;
			}
			else
			{
				ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					if (isTopLevel && odataCollectionValue.Items != null)
					{
						if (odataCollectionValue.Items.Any((object i) => i is ODataResourceValue))
						{
							throw new ODataException(Strings.ODataMessageWriter_NotAllowedWriteTopLevelPropertyWithResourceValue(property.Name));
						}
					}
					this.WriteCollectionProperty(odataCollectionValue, flag);
					return;
				}
				ODataBinaryStreamValue odataBinaryStreamValue = odataValue as ODataBinaryStreamValue;
				if (odataBinaryStreamValue != null)
				{
					this.WriteStreamProperty(odataBinaryStreamValue, flag);
					return;
				}
				return;
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0004E9F0 File Offset: 0x0004CBF0
		internal void WritePropertyInfo(ODataPropertyInfo propertyInfo, IEdmStructuredType owningType, bool isTopLevel, IDuplicatePropertyNameChecker duplicatePropertyNameChecker, ODataResourceMetadataBuilder metadataBuilder)
		{
			WriterValidationUtils.ValidatePropertyNotNull(propertyInfo);
			string name = propertyInfo.Name;
			if (base.JsonLightOutputContext.MessageWriterSettings.Validations != ValidationKinds.None)
			{
				WriterValidationUtils.ValidatePropertyName(name);
			}
			if (!base.JsonLightOutputContext.PropertyCacheHandler.InResourceSetScope())
			{
				this.currentPropertyInfo = new PropertySerializationInfo(base.JsonLightOutputContext.Model, name, owningType)
				{
					IsTopLevel = isTopLevel
				};
			}
			else
			{
				this.currentPropertyInfo = base.JsonLightOutputContext.PropertyCacheHandler.GetProperty(base.JsonLightOutputContext.Model, name, owningType);
			}
			WriterValidationUtils.ValidatePropertyDefined(this.currentPropertyInfo, base.MessageWriterSettings.ThrowOnUndeclaredPropertyForNonOpenType);
			duplicatePropertyNameChecker.ValidatePropertyUniqueness(propertyInfo);
			if (this.currentPropertyInfo.MetadataType.IsUndeclaredProperty)
			{
				this.WriteODataTypeAnnotation(propertyInfo, isTopLevel);
			}
			this.WriteInstanceAnnotation(propertyInfo, isTopLevel, this.currentPropertyInfo.MetadataType.IsUndeclaredProperty);
			ODataStreamPropertyInfo odataStreamPropertyInfo = propertyInfo as ODataStreamPropertyInfo;
			if (odataStreamPropertyInfo != null && !(base.JsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel))
			{
				this.WriteStreamValue(odataStreamPropertyInfo, propertyInfo.Name, metadataBuilder);
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0004EAF4 File Offset: 0x0004CCF4
		private bool IsOpenProperty(ODataPropertyInfo property)
		{
			bool flag;
			if (property.SerializationInfo != null)
			{
				flag = property.SerializationInfo.PropertyKind == ODataPropertyKind.Open;
			}
			else
			{
				flag = (!base.WritingResponse && this.currentPropertyInfo.MetadataType.OwningType == null) || this.currentPropertyInfo.MetadataType.IsOpenProperty;
			}
			return flag;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0004EB49 File Offset: 0x0004CD49
		private void WriteUntypedValue(ODataUntypedValue untypedValue)
		{
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.jsonLightValueSerializer.WriteUntypedValue(untypedValue);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0004EB6D File Offset: 0x0004CD6D
		private void WriteStreamValue(IODataStreamReferenceInfo streamInfo, string propertyName, ODataResourceMetadataBuilder metadataBuilder)
		{
			WriterValidationUtils.ValidateStreamPropertyInfo(streamInfo, this.currentPropertyInfo.MetadataType.EdmProperty, propertyName, base.WritingResponse);
			this.WriteStreamInfo(propertyName, streamInfo);
			if (metadataBuilder != null)
			{
				metadataBuilder.MarkStreamPropertyProcessed(propertyName);
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0004EB9E File Offset: 0x0004CD9E
		private void WriteInstanceAnnotation(ODataPropertyInfo property, bool isTopLevel, bool isUndeclaredProperty)
		{
			if (property.InstanceAnnotations.Count != 0)
			{
				if (isTopLevel)
				{
					base.InstanceAnnotationWriter.WriteInstanceAnnotations(property.InstanceAnnotations, null, false);
					return;
				}
				base.InstanceAnnotationWriter.WriteInstanceAnnotations(property.InstanceAnnotations, property.Name, isUndeclaredProperty);
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0004EBDC File Offset: 0x0004CDDC
		private void WriteODataTypeAnnotation(ODataPropertyInfo property, bool isTopLevel)
		{
			if (property.TypeAnnotation != null && property.TypeAnnotation.TypeName != null)
			{
				string typeName = property.TypeAnnotation.TypeName;
				IEdmPrimitiveType edmPrimitiveType = EdmCoreModel.Instance.FindType(typeName) as IEdmPrimitiveType;
				if (edmPrimitiveType == null || (edmPrimitiveType.PrimitiveKind != EdmPrimitiveTypeKind.String && edmPrimitiveType.PrimitiveKind != EdmPrimitiveTypeKind.Decimal && edmPrimitiveType.PrimitiveKind != EdmPrimitiveTypeKind.Boolean))
				{
					if (isTopLevel)
					{
						base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(typeName, false);
						return;
					}
					base.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(property.Name, typeName);
				}
			}
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0004EC60 File Offset: 0x0004CE60
		private void WriteStreamInfo(string propertyName, IODataStreamReferenceInfo streamInfo)
		{
			Uri editLink = streamInfo.EditLink;
			if (editLink != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaEditLink");
				base.JsonWriter.WriteValue(base.UriToString(editLink));
			}
			Uri readLink = streamInfo.ReadLink;
			if (readLink != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaReadLink");
				base.JsonWriter.WriteValue(base.UriToString(readLink));
			}
			string contentType = streamInfo.ContentType;
			if (contentType != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaContentType");
				base.JsonWriter.WriteValue(contentType);
			}
			string etag = streamInfo.ETag;
			if (etag != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaEtag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0004ED24 File Offset: 0x0004CF24
		private void WriteNullProperty(ODataPropertyInfo property)
		{
			this.WriterValidator.ValidateNullPropertyValue(this.currentPropertyInfo.MetadataType.TypeReference, property.Name, this.currentPropertyInfo.IsTopLevel, base.Model);
			if (!this.currentPropertyInfo.IsTopLevel)
			{
				base.JsonWriter.WriteName(property.Name);
				this.JsonLightValueSerializer.WriteNullValue();
				return;
			}
			if (base.JsonLightOutputContext.MessageWriterSettings.LibraryCompatibility < ODataLibraryCompatibility.Version7 && base.JsonLightOutputContext.MessageWriterSettings.Version < ODataVersion.V401)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.null");
				base.JsonWriter.WriteValue(true);
				return;
			}
			throw new ODataException(Strings.ODataMessageWriter_CannotWriteTopLevelNull);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0004EDF4 File Offset: 0x0004CFF4
		private void WriteResourceProperty(ODataProperty property, ODataResourceValue resourceValue, bool isOpenPropertyType)
		{
			base.JsonWriter.WriteName(property.Name);
			this.JsonLightValueSerializer.WriteResourceValue(resourceValue, this.currentPropertyInfo.MetadataType.TypeReference, isOpenPropertyType, base.CreateDuplicatePropertyNameChecker());
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0004EE2C File Offset: 0x0004D02C
		private void WriteEnumProperty(ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			this.ResolveEnumValueTypeName(enumValue, isOpenPropertyType);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WriteEnumValue(enumValue, this.currentPropertyInfo.MetadataType.TypeReference);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0004EE7C File Offset: 0x0004D07C
		private void ResolveEnumValueTypeName(ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			if (this.currentPropertyInfo.ValueType == null || this.currentPropertyInfo.ValueType.TypeName != enumValue.TypeName)
			{
				IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForEnumValue(base.Model, enumValue, isOpenPropertyType);
				bool flag = string.Equals(base.JsonLightOutputContext.Model.GetType().Name, "ClientEdmModel", StringComparison.OrdinalIgnoreCase);
				string valueTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(enumValue, this.currentPropertyInfo.MetadataType.TypeReference, edmTypeReference, flag || isOpenPropertyType);
				this.currentPropertyInfo.ValueType = new PropertyValueTypeInfo(enumValue.TypeName, edmTypeReference);
				this.currentPropertyInfo.TypeNameToWrite = valueTypeNameForWriting;
				return;
			}
			string text;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(enumValue, out text))
			{
				this.currentPropertyInfo.TypeNameToWrite = text;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0004EF44 File Offset: 0x0004D144
		private void WriteCollectionProperty(ODataCollectionValue collectionValue, bool isOpenPropertyType)
		{
			this.ResolveCollectionValueTypeName(collectionValue, isOpenPropertyType);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WriteCollectionValue(collectionValue, this.currentPropertyInfo.MetadataType.TypeReference, this.currentPropertyInfo.ValueType.TypeReference, this.currentPropertyInfo.IsTopLevel, false, isOpenPropertyType);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		private void ResolveCollectionValueTypeName(ODataCollectionValue collectionValue, bool isOpenPropertyType)
		{
			if (this.currentPropertyInfo.ValueType == null || this.currentPropertyInfo.ValueType.TypeName != collectionValue.TypeName)
			{
				IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForCollectionValue(base.Model, this.currentPropertyInfo.MetadataType.TypeReference, collectionValue, isOpenPropertyType, this.WriterValidator);
				this.currentPropertyInfo.ValueType = new PropertyValueTypeInfo(collectionValue.TypeName, edmTypeReference);
				this.currentPropertyInfo.TypeNameToWrite = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, this.currentPropertyInfo, isOpenPropertyType);
				return;
			}
			string text;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(collectionValue, out text))
			{
				this.currentPropertyInfo.TypeNameToWrite = text;
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0004F05C File Offset: 0x0004D25C
		private void WriteStreamProperty(ODataBinaryStreamValue streamValue, bool isOpenPropertyType)
		{
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WriteStreamValue(streamValue);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0004F080 File Offset: 0x0004D280
		private void WritePrimitiveProperty(ODataPrimitiveValue primitiveValue, bool isOpenPropertyType)
		{
			this.ResolvePrimitiveValueTypeName(primitiveValue, isOpenPropertyType);
			WriterValidationUtils.ValidatePropertyDerivedTypeConstraint(this.currentPropertyInfo);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WritePrimitiveValue(primitiveValue.Value, this.currentPropertyInfo.ValueType.TypeReference, this.currentPropertyInfo.MetadataType.TypeReference);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0004F0F0 File Offset: 0x0004D2F0
		private void ResolvePrimitiveValueTypeName(ODataPrimitiveValue primitiveValue, bool isOpenPropertyType)
		{
			string name = primitiveValue.Value.GetType().Name;
			if (this.currentPropertyInfo.ValueType == null || this.currentPropertyInfo.ValueType.TypeName != name)
			{
				IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForPrimitiveValue(primitiveValue);
				this.currentPropertyInfo.ValueType = new PropertyValueTypeInfo(name, edmTypeReference);
				this.currentPropertyInfo.TypeNameToWrite = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(primitiveValue, this.currentPropertyInfo, isOpenPropertyType);
				return;
			}
			string text;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(primitiveValue, out text))
			{
				this.currentPropertyInfo.TypeNameToWrite = text;
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0004F188 File Offset: 0x0004D388
		private void WritePropertyTypeName()
		{
			string typeNameToWrite = this.currentPropertyInfo.TypeNameToWrite;
			if (typeNameToWrite != null)
			{
				if (this.currentPropertyInfo.IsTopLevel)
				{
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(typeNameToWrite, false);
					return;
				}
				base.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(this.currentPropertyInfo.PropertyName, typeNameToWrite);
			}
		}

		// Token: 0x04000B4F RID: 2895
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;

		// Token: 0x04000B50 RID: 2896
		private PropertySerializationInfo currentPropertyInfo;
	}
}
