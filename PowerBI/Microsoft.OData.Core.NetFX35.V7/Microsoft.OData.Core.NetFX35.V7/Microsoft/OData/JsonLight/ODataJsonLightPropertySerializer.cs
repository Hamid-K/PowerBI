using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000214 RID: 532
	internal class ODataJsonLightPropertySerializer : ODataJsonLightSerializer
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00040DDC File Offset: 0x0003EFDC
		internal ODataJsonLightPropertySerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext, initContextUriBuilder)
		{
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this, initContextUriBuilder);
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00040DF3 File Offset: 0x0003EFF3
		internal ODataJsonLightValueSerializer JsonLightValueSerializer
		{
			get
			{
				return this.jsonLightValueSerializer;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00040DFC File Offset: 0x0003EFFC
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			if (property.ODataValue == null || property.ODataValue.IsNullValue)
			{
				throw new ODataException("A null top-level property is not allowed to be serialized.");
			}
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				ODataPayloadKind odataPayloadKind = (this.JsonLightOutputContext.MessageWriterSettings.IsIndividualProperty ? ODataPayloadKind.IndividualProperty : ODataPayloadKind.Property);
				ODataContextUrlInfo contextInfo = ODataContextUrlInfo.Create(property.ODataValue, this.JsonLightOutputContext.MessageWriterSettings.ODataUri, this.Model);
				this.WriteContextUriProperty(odataPayloadKind, () => contextInfo, null, null);
				this.WriteProperty(property, null, true, false, this.CreateDuplicatePropertyNameChecker());
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00040E5C File Offset: 0x0003F05C
		internal void WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> properties, bool isComplexValue, IDuplicatePropertyNameChecker duplicatePropertyNameChecker)
		{
			if (properties == null)
			{
				return;
			}
			foreach (ODataProperty odataProperty in properties)
			{
				this.WriteProperty(odataProperty, owningType, false, !isComplexValue, duplicatePropertyNameChecker);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00040EB0 File Offset: 0x0003F0B0
		private bool IsOpenProperty(ODataProperty property)
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
			if (flag)
			{
				this.WriterValidator.ValidateOpenPropertyValue(property.Name, property.ODataValue);
			}
			return flag;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00040F20 File Offset: 0x0003F120
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Splitting the code would make the logic harder to understand; class coupling is only slightly above threshold.")]
		private void WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, bool allowStreamProperty, IDuplicatePropertyNameChecker duplicatePropertyNameChecker)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
			string name = property.Name;
			if (!base.JsonLightOutputContext.PropertyCacheHandler.InResourceSetScope())
			{
				WriterValidationUtils.ValidatePropertyName(name);
				this.currentPropertyInfo = new PropertySerializationInfo(name, owningType)
				{
					IsTopLevel = isTopLevel
				};
			}
			else
			{
				this.currentPropertyInfo = base.JsonLightOutputContext.PropertyCacheHandler.GetProperty(name, owningType);
			}
			WriterValidationUtils.ValidatePropertyDefined(this.currentPropertyInfo, base.MessageWriterSettings.ThrowOnUndeclaredPropertyForNonOpenType);
			duplicatePropertyNameChecker.ValidatePropertyUniqueness(property);
			if (this.currentPropertyInfo.MetadataType.IsUndeclaredProperty)
			{
				this.WriteODataTypeAnnotation(property, isTopLevel);
			}
			this.WriteInstanceAnnotation(property, isTopLevel, this.currentPropertyInfo.MetadataType.IsUndeclaredProperty);
			ODataValue odataValue = property.ODataValue;
			ODataUntypedValue odataUntypedValue = odataValue as ODataUntypedValue;
			if (odataUntypedValue != null)
			{
				this.WriteUntypedValue(odataUntypedValue);
				return;
			}
			ODataStreamReferenceValue odataStreamReferenceValue = odataValue as ODataStreamReferenceValue;
			if (odataStreamReferenceValue != null)
			{
				if (!allowStreamProperty)
				{
					throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataResource(name));
				}
				WriterValidationUtils.ValidateStreamReferenceProperty(property, this.currentPropertyInfo.MetadataType.EdmProperty, base.WritingResponse);
				this.WriteStreamReferenceProperty(name, odataStreamReferenceValue);
				return;
			}
			else
			{
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
				ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					this.WriteCollectionProperty(odataCollectionValue, flag);
					return;
				}
				return;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0004108C File Offset: 0x0003F28C
		private void WriteUntypedValue(ODataUntypedValue untypedValue)
		{
			if (!base.MessageWriterSettings.ThrowOnUndeclaredPropertyForNonOpenType)
			{
				base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
				this.jsonLightValueSerializer.WriteUntypedValue(untypedValue);
				return;
			}
			throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(this.currentPropertyInfo.PropertyName, this.currentPropertyInfo.MetadataType.OwningType.FullTypeName()));
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000410F3 File Offset: 0x0003F2F3
		private void WriteInstanceAnnotation(ODataProperty property, bool isTopLevel, bool isUndeclaredProperty)
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

		// Token: 0x0600158B RID: 5515 RVA: 0x00041134 File Offset: 0x0003F334
		private void WriteODataTypeAnnotation(ODataProperty property, bool isTopLevel)
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

		// Token: 0x0600158C RID: 5516 RVA: 0x000411B8 File Offset: 0x0003F3B8
		private void WriteStreamReferenceProperty(string propertyName, ODataStreamReferenceValue streamReferenceValue)
		{
			Uri editLink = streamReferenceValue.EditLink;
			if (editLink != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaEditLink");
				base.JsonWriter.WriteValue(base.UriToString(editLink));
			}
			Uri readLink = streamReferenceValue.ReadLink;
			if (readLink != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaReadLink");
				base.JsonWriter.WriteValue(base.UriToString(readLink));
			}
			string contentType = streamReferenceValue.ContentType;
			if (contentType != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaContentType");
				base.JsonWriter.WriteValue(contentType);
			}
			string etag = streamReferenceValue.ETag;
			if (etag != null)
			{
				base.ODataAnnotationWriter.WritePropertyAnnotationName(propertyName, "odata.mediaEtag");
				base.JsonWriter.WriteValue(etag);
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004127C File Offset: 0x0003F47C
		private void WriteNullProperty(ODataProperty property)
		{
			this.WriterValidator.ValidateNullPropertyValue(this.currentPropertyInfo.MetadataType.TypeReference, property.Name, base.Model);
			if (this.currentPropertyInfo.IsTopLevel)
			{
				throw new ODataException("A null top-level property is not allowed to be serialized.");
			}
			base.JsonWriter.WriteName(property.Name);
			this.JsonLightValueSerializer.WriteNullValue();
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000412E4 File Offset: 0x0003F4E4
		private void WriteEnumProperty(ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			this.ResolveEnumValueTypeName(enumValue, isOpenPropertyType);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WriteEnumValue(enumValue, this.currentPropertyInfo.MetadataType.TypeReference);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00041334 File Offset: 0x0003F534
		private void ResolveEnumValueTypeName(ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			if (this.currentPropertyInfo.ValueType == null || this.currentPropertyInfo.ValueType.TypeName != enumValue.TypeName)
			{
				IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForEnumValue(base.Model, enumValue, isOpenPropertyType);
				bool flag = string.Equals(base.JsonLightOutputContext.Model.GetType().Name, "ClientEdmModel", 5);
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

		// Token: 0x06001590 RID: 5520 RVA: 0x000413FC File Offset: 0x0003F5FC
		private void WriteCollectionProperty(ODataCollectionValue collectionValue, bool isOpenPropertyType)
		{
			this.ResolveCollectionValueTypeName(collectionValue, isOpenPropertyType);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WriteCollectionValue(collectionValue, this.currentPropertyInfo.MetadataType.TypeReference, this.currentPropertyInfo.ValueType.TypeReference, this.currentPropertyInfo.IsTopLevel, false, isOpenPropertyType);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00041468 File Offset: 0x0003F668
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

		// Token: 0x06001592 RID: 5522 RVA: 0x00041514 File Offset: 0x0003F714
		private void WritePrimitiveProperty(ODataPrimitiveValue primitiveValue, bool isOpenPropertyType)
		{
			this.ResolvePrimitiveValueTypeName(primitiveValue, isOpenPropertyType);
			this.WritePropertyTypeName();
			base.JsonWriter.WriteName(this.currentPropertyInfo.WireName);
			this.JsonLightValueSerializer.WritePrimitiveValue(primitiveValue.Value, this.currentPropertyInfo.ValueType.TypeReference, this.currentPropertyInfo.MetadataType.TypeReference);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00041578 File Offset: 0x0003F778
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

		// Token: 0x06001594 RID: 5524 RVA: 0x00041610 File Offset: 0x0003F810
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

		// Token: 0x04000A32 RID: 2610
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;

		// Token: 0x04000A33 RID: 2611
		private PropertySerializationInfo currentPropertyInfo;
	}
}
