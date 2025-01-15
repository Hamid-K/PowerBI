using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E0 RID: 224
	internal class ODataJsonLightPropertySerializer : ODataJsonLightSerializer
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001E521 File Offset: 0x0001C721
		internal ODataJsonLightPropertySerializer(ODataJsonLightOutputContext jsonLightOutputContext, bool initContextUriBuilder = false)
			: base(jsonLightOutputContext, initContextUriBuilder)
		{
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this, initContextUriBuilder);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001E538 File Offset: 0x0001C738
		internal ODataJsonLightValueSerializer JsonLightValueSerializer
		{
			get
			{
				return this.jsonLightValueSerializer;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E620 File Offset: 0x0001C820
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				ODataPayloadKind odataPayloadKind = (this.JsonLightOutputContext.MessageWriterSettings.IsIndividualProperty ? ODataPayloadKind.IndividualProperty : ODataPayloadKind.Property);
				ODataContextUrlInfo contextInfo = ODataContextUrlInfo.Create(property.ODataValue, this.JsonLightOutputContext.MessageWriterSettings.ODataUri, this.Model);
				this.WriteContextUriProperty(odataPayloadKind, () => contextInfo, null, null);
				this.WriteProperty(property, null, true, false, this.CreateDuplicatePropertyNamesChecker(), null);
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001E654 File Offset: 0x0001C854
		internal void WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> properties, bool isComplexValue, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			if (properties == null)
			{
				return;
			}
			foreach (ODataProperty odataProperty in properties)
			{
				this.WriteProperty(odataProperty, owningType, false, !isComplexValue, duplicatePropertyNamesChecker, projectedProperties);
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		private bool IsOpenProperty(ODataProperty property, IEdmStructuredType owningType, IEdmProperty edmProperty)
		{
			bool flag;
			if (property.SerializationInfo != null)
			{
				flag = property.SerializationInfo.PropertyKind == ODataPropertyKind.Open;
			}
			else
			{
				flag = (!base.WritingResponse && owningType == null) || (owningType != null && owningType.IsOpen && edmProperty == null);
			}
			if (flag)
			{
				this.WriterValidator.ValidateOpenPropertyValue(property.Name, property.ODataValue);
			}
			return flag;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E710 File Offset: 0x0001C910
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Splitting the code would make the logic harder to understand; class coupling is only slightly above threshold.")]
		private void WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, bool allowStreamProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			this.WriterValidator.ValidatePropertyNotNull(property);
			string name = property.Name;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			this.WriterValidator.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(property);
			this.WriteInstanceAnnotation(property, isTopLevel);
			bool flag = base.JsonLightOutputContext.MessageWriterSettings.EnableFullValidation && !base.WritingResponse;
			IEdmProperty edmProperty = this.WriterValidator.ValidatePropertyDefined(name, owningType, flag);
			IEdmTypeReference edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			ODataValue odataValue = property.ODataValue;
			ODataStreamReferenceValue odataStreamReferenceValue = odataValue as ODataStreamReferenceValue;
			if (odataStreamReferenceValue != null)
			{
				if (!allowStreamProperty)
				{
					throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(name));
				}
				this.WriterValidator.ValidateStreamReferenceProperty(property, edmProperty, base.WritingResponse);
				this.WriteStreamReferenceProperty(name, odataStreamReferenceValue);
				return;
			}
			else
			{
				if (odataValue is ODataNullValue || odataValue == null)
				{
					this.WriteNullProperty(property, edmTypeReference, isTopLevel);
					return;
				}
				bool flag2 = this.IsOpenProperty(property, owningType, edmProperty);
				ODataPrimitiveValue odataPrimitiveValue = odataValue as ODataPrimitiveValue;
				if (odataPrimitiveValue != null)
				{
					this.WritePrimitiveProperty(property, odataPrimitiveValue, edmTypeReference, isTopLevel, flag2);
					return;
				}
				ODataComplexValue odataComplexValue = odataValue as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.WriteComplexProperty(property, odataComplexValue, edmTypeReference, isTopLevel, flag2);
					return;
				}
				ODataEnumValue odataEnumValue = odataValue as ODataEnumValue;
				if (odataEnumValue != null)
				{
					this.WriteEnumProperty(property, odataEnumValue, edmTypeReference, isTopLevel, flag2);
					return;
				}
				ODataCollectionValue odataCollectionValue = odataValue as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					this.WriteCollectionProperty(property, odataCollectionValue, edmTypeReference, isTopLevel, flag2);
					return;
				}
				ODataUntypedValue odataUntypedValue = odataValue as ODataUntypedValue;
				this.WriteUntypedProperty(property, odataUntypedValue, isTopLevel);
				return;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E875 File Offset: 0x0001CA75
		private void WriteInstanceAnnotation(ODataProperty property, bool isTopLevel)
		{
			if (Enumerable.Any<ODataInstanceAnnotation>(property.InstanceAnnotations))
			{
				if (isTopLevel)
				{
					base.InstanceAnnotationWriter.WriteInstanceAnnotations(property.InstanceAnnotations, null);
					return;
				}
				base.InstanceAnnotationWriter.WriteInstanceAnnotations(property.InstanceAnnotations, property.Name);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
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

		// Token: 0x0600085D RID: 2141 RVA: 0x0001E978 File Offset: 0x0001CB78
		private void WriteNullProperty(ODataProperty property, IEdmTypeReference propertyTypeReference, bool isTopLevel)
		{
			this.WriterValidator.ValidateNullPropertyValue(propertyTypeReference, property.Name, base.MessageWriterSettings.WriterBehavior, base.Model);
			if (isTopLevel)
			{
				base.ODataAnnotationWriter.WriteInstanceAnnotationName("odata.null");
				base.JsonWriter.WriteValue(true);
				return;
			}
			base.JsonWriter.WriteName(property.Name);
			this.JsonLightValueSerializer.WriteNullValue();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		private void WriteComplexProperty(ODataProperty property, ODataComplexValue complexValue, IEdmTypeReference propertyTypeReference, bool isTopLevel, bool isOpenPropertyType)
		{
			if (!isTopLevel)
			{
				base.JsonWriter.WriteName(property.Name);
			}
			this.JsonLightValueSerializer.WriteComplexValue(complexValue, propertyTypeReference, isTopLevel, isOpenPropertyType, base.CreateDuplicatePropertyNamesChecker());
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001EA14 File Offset: 0x0001CC14
		private void WriteEnumProperty(ODataProperty property, ODataEnumValue enumValue, IEdmTypeReference propertyTypeReference, bool isTopLevel, bool isOpenPropertyType)
		{
			string wirePropertyName = ODataJsonLightPropertySerializer.GetWirePropertyName(isTopLevel, property.Name);
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForEnumValue(base.Model, enumValue, isOpenPropertyType);
			bool flag = string.Equals(base.JsonLightOutputContext.Model.GetType().Name, "ClientEdmModel", 5);
			string valueTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(enumValue, propertyTypeReference, edmTypeReference, flag || isOpenPropertyType);
			this.WritePropertyTypeName(wirePropertyName, valueTypeNameForWriting, isTopLevel);
			base.JsonWriter.WriteName(wirePropertyName);
			this.JsonLightValueSerializer.WriteEnumValue(enumValue, propertyTypeReference);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		private void WriteCollectionProperty(ODataProperty property, ODataCollectionValue collectionValue, IEdmTypeReference propertyTypeReference, bool isTopLevel, bool isOpenPropertyType)
		{
			string wirePropertyName = ODataJsonLightPropertySerializer.GetWirePropertyName(isTopLevel, property.Name);
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForCollectionValue(base.Model, propertyTypeReference, collectionValue, isOpenPropertyType, this.WriterValidator);
			string valueTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, propertyTypeReference, edmTypeReference, isOpenPropertyType);
			this.WritePropertyTypeName(wirePropertyName, valueTypeNameForWriting, isTopLevel);
			base.JsonWriter.WriteName(wirePropertyName);
			this.JsonLightValueSerializer.WriteCollectionValue(collectionValue, propertyTypeReference, edmTypeReference, isTopLevel, false, isOpenPropertyType);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001EB10 File Offset: 0x0001CD10
		private void WriteUntypedProperty(ODataProperty property, ODataUntypedValue untypedValue, bool isTopLevel)
		{
			base.JsonWriter.WriteName(ODataJsonLightPropertySerializer.GetWirePropertyName(isTopLevel, property.Name));
			this.JsonLightValueSerializer.WriteUntypedValue(untypedValue);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001EB38 File Offset: 0x0001CD38
		private void WritePrimitiveProperty(ODataProperty property, ODataPrimitiveValue primitiveValue, IEdmTypeReference propertyTypeReference, bool isTopLevel, bool isOpenPropertyType)
		{
			string wirePropertyName = ODataJsonLightPropertySerializer.GetWirePropertyName(isTopLevel, property.Name);
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveAndValidateTypeForPrimitiveValue(primitiveValue);
			string valueTypeNameForWriting = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(primitiveValue, propertyTypeReference, edmTypeReference, isOpenPropertyType);
			this.WritePropertyTypeName(wirePropertyName, valueTypeNameForWriting, isTopLevel);
			base.JsonWriter.WriteName(wirePropertyName);
			this.JsonLightValueSerializer.WritePrimitiveValue(primitiveValue.Value, propertyTypeReference);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001EB98 File Offset: 0x0001CD98
		private void WritePropertyTypeName(string propertyName, string typeNameToWrite, bool isTopLevel)
		{
			if (typeNameToWrite != null)
			{
				if (isTopLevel)
				{
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(typeNameToWrite);
					return;
				}
				base.ODataAnnotationWriter.WriteODataTypePropertyAnnotation(propertyName, typeNameToWrite);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001EBBA File Offset: 0x0001CDBA
		private static string GetWirePropertyName(bool isTopLevel, string propertyName)
		{
			if (!isTopLevel)
			{
				return propertyName;
			}
			return "value";
		}

		// Token: 0x04000386 RID: 902
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
