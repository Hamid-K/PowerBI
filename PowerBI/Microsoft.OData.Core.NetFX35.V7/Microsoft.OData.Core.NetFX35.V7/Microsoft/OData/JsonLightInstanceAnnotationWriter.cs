using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200001E RID: 30
	internal sealed class JsonLightInstanceAnnotationWriter
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003C54 File Offset: 0x00001E54
		internal JsonLightInstanceAnnotationWriter(ODataJsonLightValueSerializer valueSerializer, JsonLightTypeNameOracle typeNameOracle)
		{
			this.valueSerializer = valueSerializer;
			this.typeNameOracle = typeNameOracle;
			this.jsonWriter = this.valueSerializer.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, valueSerializer.JsonLightOutputContext.ODataSimplifiedOptions.EnableWritingODataAnnotationWithoutPrefix);
			this.writerValidator = this.valueSerializer.MessageWriterSettings.Validator;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker, bool ignoreFilter = false, string propertyName = null)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (ODataInstanceAnnotation odataInstanceAnnotation in instanceAnnotations)
			{
				if (!hashSet.Add(odataInstanceAnnotation.Name))
				{
					throw new ODataException(Strings.JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(odataInstanceAnnotation.Name));
				}
				if (!tracker.IsAnnotationWritten(odataInstanceAnnotation.Name) && (!ODataAnnotationNames.IsODataAnnotationName(odataInstanceAnnotation.Name) || ODataAnnotationNames.IsUnknownODataAnnotationName(odataInstanceAnnotation.Name)))
				{
					this.WriteInstanceAnnotation(odataInstanceAnnotation, ignoreFilter, propertyName);
					tracker.MarkAnnotationWritten(odataInstanceAnnotation.Name);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D6C File Offset: 0x00001F6C
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, string propertyName = null, bool isUndeclaredProperty = false)
		{
			if (isUndeclaredProperty)
			{
				using (IEnumerator<ODataInstanceAnnotation> enumerator = instanceAnnotations.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ODataInstanceAnnotation odataInstanceAnnotation = enumerator.Current;
						this.WriteInstanceAnnotation(odataInstanceAnnotation, true, propertyName);
					}
					return;
				}
			}
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker(), false, propertyName);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal void WriteInstanceAnnotationsForError(IEnumerable<ODataInstanceAnnotation> instanceAnnotations)
		{
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker(), true, null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003DD8 File Offset: 0x00001FD8
		internal void WriteInstanceAnnotation(ODataInstanceAnnotation instanceAnnotation, bool ignoreFilter = false, string propertyName = null)
		{
			string name = instanceAnnotation.Name;
			ODataValue value = instanceAnnotation.Value;
			if (!ignoreFilter && this.valueSerializer.MessageWriterSettings.ShouldSkipAnnotation(name))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfTerm(name, this.valueSerializer.Model);
			if (value is ODataNullValue)
			{
				if (edmTypeReference != null && !edmTypeReference.IsNullable)
				{
					throw new ODataException(Strings.JsonLightInstanceAnnotationWriter_NullValueNotAllowedForInstanceAnnotation(instanceAnnotation.Name, edmTypeReference.FullName()));
				}
				this.WriteInstanceAnnotationName(propertyName, name);
				this.valueSerializer.WriteNullValue();
				return;
			}
			else
			{
				bool flag = edmTypeReference == null;
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					IEdmTypeReference edmTypeReference2 = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeForCollectionValue(this.valueSerializer.Model, edmTypeReference, odataCollectionValue, flag, this.writerValidator);
					string valueTypeNameForWriting = this.typeNameOracle.GetValueTypeNameForWriting(odataCollectionValue, edmTypeReference, edmTypeReference2, flag);
					if (valueTypeNameForWriting != null)
					{
						this.odataAnnotationWriter.WriteODataTypePropertyAnnotation(name, valueTypeNameForWriting);
					}
					this.WriteInstanceAnnotationName(propertyName, name);
					this.valueSerializer.WriteCollectionValue(odataCollectionValue, edmTypeReference, edmTypeReference2, false, false, flag);
					return;
				}
				ODataUntypedValue odataUntypedValue = value as ODataUntypedValue;
				if (odataUntypedValue != null)
				{
					this.WriteInstanceAnnotationName(propertyName, name);
					this.valueSerializer.WriteUntypedValue(odataUntypedValue);
					return;
				}
				ODataEnumValue odataEnumValue = value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					this.WriteInstanceAnnotationName(propertyName, name);
					this.valueSerializer.WriteEnumValue(odataEnumValue, edmTypeReference);
					return;
				}
				ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
				IEdmTypeReference edmTypeReference3 = TypeNameOracle.ResolveAndValidateTypeForPrimitiveValue(odataPrimitiveValue);
				string valueTypeNameForWriting2 = this.typeNameOracle.GetValueTypeNameForWriting(odataPrimitiveValue, edmTypeReference, edmTypeReference3, flag);
				if (valueTypeNameForWriting2 != null)
				{
					this.odataAnnotationWriter.WriteODataTypePropertyAnnotation(name, valueTypeNameForWriting2);
				}
				this.WriteInstanceAnnotationName(propertyName, name);
				this.valueSerializer.WritePrimitiveValue(odataPrimitiveValue.Value, edmTypeReference3, edmTypeReference);
				return;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F65 File Offset: 0x00002165
		private void WriteInstanceAnnotationName(string propertyName, string annotationName)
		{
			if (propertyName != null)
			{
				this.jsonWriter.WritePropertyAnnotationName(propertyName, annotationName);
				return;
			}
			this.jsonWriter.WriteInstanceAnnotationName(annotationName);
		}

		// Token: 0x0400007E RID: 126
		private readonly ODataJsonLightValueSerializer valueSerializer;

		// Token: 0x0400007F RID: 127
		private readonly JsonLightTypeNameOracle typeNameOracle;

		// Token: 0x04000080 RID: 128
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000081 RID: 129
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x04000082 RID: 130
		private readonly IWriterValidator writerValidator;
	}
}
