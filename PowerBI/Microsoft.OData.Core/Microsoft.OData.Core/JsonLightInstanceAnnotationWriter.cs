using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000047 RID: 71
	internal sealed class JsonLightInstanceAnnotationWriter
	{
		// Token: 0x06000235 RID: 565 RVA: 0x000061D0 File Offset: 0x000043D0
		internal JsonLightInstanceAnnotationWriter(ODataJsonLightValueSerializer valueSerializer, JsonLightTypeNameOracle typeNameOracle)
		{
			this.valueSerializer = valueSerializer;
			this.typeNameOracle = typeNameOracle;
			this.jsonWriter = this.valueSerializer.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, valueSerializer.JsonLightOutputContext.OmitODataPrefix, this.valueSerializer.MessageWriterSettings.Version);
			this.writerValidator = this.valueSerializer.MessageWriterSettings.Validator;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006244 File Offset: 0x00004444
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

		// Token: 0x06000237 RID: 567 RVA: 0x000062F0 File Offset: 0x000044F0
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

		// Token: 0x06000238 RID: 568 RVA: 0x0000634C File Offset: 0x0000454C
		internal void WriteInstanceAnnotationsForError(IEnumerable<ODataInstanceAnnotation> instanceAnnotations)
		{
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker(), true, null);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000635C File Offset: 0x0000455C
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
				ODataResourceValue odataResourceValue = value as ODataResourceValue;
				if (odataResourceValue != null)
				{
					this.WriteInstanceAnnotationName(propertyName, name);
					this.valueSerializer.WriteResourceValue(odataResourceValue, edmTypeReference, flag, this.valueSerializer.CreateDuplicatePropertyNameChecker());
					return;
				}
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

		// Token: 0x0600023A RID: 570 RVA: 0x00006518 File Offset: 0x00004718
		private void WriteInstanceAnnotationName(string propertyName, string annotationName)
		{
			if (propertyName != null)
			{
				this.jsonWriter.WritePropertyAnnotationName(propertyName, annotationName);
				return;
			}
			this.jsonWriter.WriteInstanceAnnotationName(annotationName);
		}

		// Token: 0x040000EE RID: 238
		private readonly ODataJsonLightValueSerializer valueSerializer;

		// Token: 0x040000EF RID: 239
		private readonly JsonLightTypeNameOracle typeNameOracle;

		// Token: 0x040000F0 RID: 240
		private readonly IJsonWriter jsonWriter;

		// Token: 0x040000F1 RID: 241
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x040000F2 RID: 242
		private readonly IWriterValidator writerValidator;
	}
}
