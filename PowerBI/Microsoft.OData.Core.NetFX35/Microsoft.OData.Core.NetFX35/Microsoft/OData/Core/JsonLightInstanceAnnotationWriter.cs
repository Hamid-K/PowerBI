using System;
using System.Collections.Generic;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000113 RID: 275
	internal sealed class JsonLightInstanceAnnotationWriter
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x00026018 File Offset: 0x00024218
		internal JsonLightInstanceAnnotationWriter(IODataJsonLightValueSerializer valueSerializer, JsonLightTypeNameOracle typeNameOracle)
		{
			this.valueSerializer = valueSerializer;
			this.typeNameOracle = typeNameOracle;
			this.jsonWriter = this.valueSerializer.JsonWriter;
			this.odataAnnotationWriter = new JsonLightODataAnnotationWriter(this.jsonWriter, valueSerializer.Settings.ODataSimplified);
			this.writerValidator = ValidatorFactory.CreateWriterValidator(this.valueSerializer.Settings.EnableFullValidation);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00026084 File Offset: 0x00024284
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker, bool ignoreFilter = false, string propertyName = null)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (ODataInstanceAnnotation odataInstanceAnnotation in instanceAnnotations)
			{
				if (!hashSet.Add(odataInstanceAnnotation.Name))
				{
					throw new ODataException(Strings.JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(odataInstanceAnnotation.Name));
				}
				if (!tracker.IsAnnotationWritten(odataInstanceAnnotation.Name))
				{
					this.WriteInstanceAnnotation(odataInstanceAnnotation, ignoreFilter, propertyName);
					tracker.MarkAnnotationWritten(odataInstanceAnnotation.Name);
				}
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00026114 File Offset: 0x00024314
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, string propertyName = null)
		{
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker(), false, propertyName);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00026124 File Offset: 0x00024324
		internal void WriteInstanceAnnotationsForError(IEnumerable<ODataInstanceAnnotation> instanceAnnotations)
		{
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker(), true, null);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00026134 File Offset: 0x00024334
		internal void WriteInstanceAnnotation(ODataInstanceAnnotation instanceAnnotation, bool ignoreFilter = false, string propertyName = null)
		{
			string name = instanceAnnotation.Name;
			ODataValue value = instanceAnnotation.Value;
			if (!ignoreFilter && this.valueSerializer.Settings.ShouldSkipAnnotation(name))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(name, this.valueSerializer.Model);
			if (value is ODataNullValue)
			{
				if (edmTypeReference != null && !edmTypeReference.IsNullable)
				{
					throw new ODataException(Strings.ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(instanceAnnotation.Name, edmTypeReference.FullName()));
				}
				this.WriteInstanceAnnotationName(propertyName, name);
				this.valueSerializer.WriteNullValue();
				return;
			}
			else
			{
				bool flag = edmTypeReference == null;
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.WriteInstanceAnnotationName(propertyName, name);
					this.valueSerializer.WriteComplexValue(odataComplexValue, edmTypeReference, false, flag, this.valueSerializer.CreateDuplicatePropertyNamesChecker());
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
				ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
				IEdmTypeReference edmTypeReference3 = TypeNameOracle.ResolveAndValidateTypeForPrimitiveValue(odataPrimitiveValue);
				string valueTypeNameForWriting2 = this.typeNameOracle.GetValueTypeNameForWriting(odataPrimitiveValue, edmTypeReference, edmTypeReference3, flag);
				if (valueTypeNameForWriting2 != null)
				{
					this.odataAnnotationWriter.WriteODataTypePropertyAnnotation(name, valueTypeNameForWriting2);
				}
				this.WriteInstanceAnnotationName(propertyName, name);
				this.valueSerializer.WritePrimitiveValue(odataPrimitiveValue.Value, edmTypeReference);
				return;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000262AA File Offset: 0x000244AA
		private void WriteInstanceAnnotationName(string propertyName, string annotationName)
		{
			if (propertyName != null)
			{
				this.jsonWriter.WritePropertyAnnotationName(propertyName, annotationName);
				return;
			}
			this.jsonWriter.WriteInstanceAnnotationName(annotationName);
		}

		// Token: 0x04000432 RID: 1074
		private readonly IODataJsonLightValueSerializer valueSerializer;

		// Token: 0x04000433 RID: 1075
		private readonly JsonLightTypeNameOracle typeNameOracle;

		// Token: 0x04000434 RID: 1076
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000435 RID: 1077
		private readonly JsonLightODataAnnotationWriter odataAnnotationWriter;

		// Token: 0x04000436 RID: 1078
		private readonly IWriterValidator writerValidator;
	}
}
