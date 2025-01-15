using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C4 RID: 452
	internal class ODataVerboseJsonPropertyAndValueSerializer : ODataVerboseJsonSerializer
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x0002F124 File Offset: 0x0002D324
		internal ODataVerboseJsonPropertyAndValueSerializer(ODataVerboseJsonOutputContext verboseJsonOutputContext)
			: base(verboseJsonOutputContext)
		{
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002F184 File Offset: 0x0002D384
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WriteTopLevelPayload(delegate
			{
				this.JsonWriter.StartObjectScope();
				this.WriteProperty(property, null, false, this.CreateDuplicatePropertyNamesChecker(), null);
				this.JsonWriter.EndObjectScope();
			});
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002F1B8 File Offset: 0x0002D3B8
		internal void WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> properties, bool isComplexValue, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			if (properties == null)
			{
				return;
			}
			foreach (ODataProperty odataProperty in properties)
			{
				this.WriteProperty(odataProperty, owningType, !isComplexValue, duplicatePropertyNamesChecker, projectedProperties);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002F210 File Offset: 0x0002D410
		internal void WritePrimitiveValue(object value, CollectionWithoutExpectedTypeValidator collectionValidator, IEdmTypeReference expectedTypeReference)
		{
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			if (collectionValidator != null)
			{
				if (primitiveTypeReference == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
				}
				collectionValidator.ValidateCollectionItem(primitiveTypeReference.FullName(), EdmTypeKind.Primitive);
			}
			if (expectedTypeReference != null)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			if (primitiveTypeReference != null && primitiveTypeReference.IsSpatial())
			{
				string text = primitiveTypeReference.FullName();
				PrimitiveConverter.Instance.WriteVerboseJson(value, base.JsonWriter, text, base.Version);
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value, base.Version);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002F29C File Offset: 0x0002D49C
		internal void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator)
		{
			this.IncreaseRecursionDepth();
			base.JsonWriter.StartObjectScope();
			string text = complexValue.TypeName;
			if (collectionValidator != null)
			{
				collectionValidator.ValidateCollectionItem(text, EdmTypeKind.Complex);
			}
			IEdmComplexTypeReference edmComplexTypeReference = TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, propertyTypeReference, complexValue, isOpenPropertyType).AsComplexOrNull();
			string text2;
			text = base.VerboseJsonOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, edmComplexTypeReference, complexValue.GetAnnotation<SerializationTypeNameAnnotation>(), collectionValidator, out text2);
			if (text != null)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(base.JsonWriter, text);
			}
			this.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), complexValue.Properties, true, duplicatePropertyNamesChecker, null);
			base.JsonWriter.EndObjectScope();
			this.DecreaseRecursionDepth();
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002F33C File Offset: 0x0002D53C
		internal void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, bool isOpenPropertyType)
		{
			this.IncreaseRecursionDepth();
			base.JsonWriter.StartObjectScope();
			IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, collectionValue, isOpenPropertyType);
			string text;
			string valueTypeNameForWriting = base.VerboseJsonOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, edmCollectionTypeReference, collectionValue.GetAnnotation<SerializationTypeNameAnnotation>(), null, out text);
			if (valueTypeNameForWriting != null)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(base.JsonWriter, valueTypeNameForWriting);
			}
			base.JsonWriter.WriteDataArrayName();
			base.JsonWriter.StartArrayScope();
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				IEdmTypeReference edmTypeReference = ((edmCollectionTypeReference == null) ? null : edmCollectionTypeReference.ElementType());
				CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = new CollectionWithoutExpectedTypeValidator(text);
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = null;
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, false);
					ODataComplexValue odataComplexValue = obj as ODataComplexValue;
					if (odataComplexValue != null)
					{
						if (duplicatePropertyNamesChecker == null)
						{
							duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
						}
						this.WriteComplexValue(odataComplexValue, edmTypeReference, false, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator);
						duplicatePropertyNamesChecker.Clear();
					}
					else
					{
						this.WritePrimitiveValue(obj, collectionWithoutExpectedTypeValidator, edmTypeReference);
					}
				}
			}
			base.JsonWriter.EndArrayScope();
			base.JsonWriter.EndObjectScope();
			this.DecreaseRecursionDepth();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002F47C File Offset: 0x0002D67C
		internal void WriteStreamReferenceValueContent(ODataStreamReferenceValue streamReferenceValue)
		{
			Uri editLink = streamReferenceValue.EditLink;
			if (editLink != null)
			{
				base.JsonWriter.WriteName("edit_media");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(editLink));
			}
			if (streamReferenceValue.ReadLink != null)
			{
				base.JsonWriter.WriteName("media_src");
				base.JsonWriter.WriteValue(base.UriToAbsoluteUriString(streamReferenceValue.ReadLink));
			}
			if (streamReferenceValue.ContentType != null)
			{
				base.JsonWriter.WriteName("content_type");
				base.JsonWriter.WriteValue(streamReferenceValue.ContentType);
			}
			string etag = streamReferenceValue.ETag;
			if (etag != null)
			{
				this.WriteETag("media_etag", etag);
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002F52F File Offset: 0x0002D72F
		internal void WriteETag(string etagName, string etagValue)
		{
			base.JsonWriter.WriteName(etagName);
			base.JsonWriter.WriteValue(etagValue);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002F549 File Offset: 0x0002D749
		[Conditional("DEBUG")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002F54C File Offset: 0x0002D74C
		private void WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool allowStreamProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
			string name = property.Name;
			object value = property.Value;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(property);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(name, owningType, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			IEdmTypeReference edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			if ((edmTypeReference != null && edmTypeReference.IsSpatial()) || (edmTypeReference == null && value is ISpatial))
			{
				ODataVersionChecker.CheckSpatialValue(base.Version);
			}
			base.JsonWriter.WriteName(name);
			if (value == null)
			{
				WriterValidationUtils.ValidateNullPropertyValue(edmTypeReference, name, base.MessageWriterSettings.WriterBehavior, base.Model);
				base.JsonWriter.WriteValue(null);
				return;
			}
			bool flag = owningType != null && owningType.IsOpen && edmTypeReference == null;
			if (flag)
			{
				ValidationUtils.ValidateOpenPropertyValue(name, value, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			}
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.WriteComplexValue(odataComplexValue, edmTypeReference, flag, base.CreateDuplicatePropertyNamesChecker(), null);
				return;
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				ODataVersionChecker.CheckCollectionValueProperties(base.Version, name);
				this.WriteCollectionValue(odataCollectionValue, edmTypeReference, flag);
				return;
			}
			ODataStreamReferenceValue odataStreamReferenceValue = value as ODataStreamReferenceValue;
			if (odataStreamReferenceValue == null)
			{
				this.WritePrimitiveValue(value, null, edmTypeReference);
				return;
			}
			if (!allowStreamProperty)
			{
				throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(name));
			}
			WriterValidationUtils.ValidateStreamReferenceProperty(property, edmProperty, base.Version, base.WritingResponse);
			WriterValidationUtils.ValidateStreamReferenceValue(odataStreamReferenceValue, false);
			this.WriteStreamReferenceValue(odataStreamReferenceValue);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		private void WriteStreamReferenceValue(ODataStreamReferenceValue streamReferenceValue)
		{
			base.JsonWriter.StartObjectScope();
			base.JsonWriter.WriteName("__mediaresource");
			base.JsonWriter.StartObjectScope();
			this.WriteStreamReferenceValueContent(streamReferenceValue);
			base.JsonWriter.EndObjectScope();
			base.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002F700 File Offset: 0x0002D900
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002F71D File Offset: 0x0002D91D
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x040004AE RID: 1198
		private int recursionDepth;
	}
}
