using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000144 RID: 324
	internal sealed class JsonLightInstanceAnnotationWriter
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x0001B6E7 File Offset: 0x000198E7
		internal JsonLightInstanceAnnotationWriter(IODataJsonLightValueSerializer valueSerializer, JsonLightTypeNameOracle typeNameOracle)
		{
			this.valueSerializer = valueSerializer;
			this.typeNameOracle = typeNameOracle;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001B6FD File Offset: 0x000198FD
		private IJsonWriter JsonWriter
		{
			get
			{
				return this.valueSerializer.JsonWriter;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001B70C File Offset: 0x0001990C
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
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
					this.WriteInstanceAnnotation(odataInstanceAnnotation);
					tracker.MarkAnnotationWritten(odataInstanceAnnotation.Name);
				}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001B79C File Offset: 0x0001999C
		internal void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations)
		{
			this.WriteInstanceAnnotations(instanceAnnotations, new InstanceAnnotationWriteTracker());
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001B7AC File Offset: 0x000199AC
		internal void WriteInstanceAnnotation(ODataInstanceAnnotation instanceAnnotation)
		{
			string name = instanceAnnotation.Name;
			ODataValue value = instanceAnnotation.Value;
			if (this.valueSerializer.Settings.ShouldSkipAnnotation(name))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(name, this.valueSerializer.Model);
			if (value is ODataNullValue)
			{
				if (edmTypeReference != null && !edmTypeReference.IsNullable)
				{
					throw new ODataException(Strings.ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(instanceAnnotation.Name, edmTypeReference.ODataFullName()));
				}
				this.JsonWriter.WriteName(name);
				this.valueSerializer.WriteNullValue();
				return;
			}
			else
			{
				bool flag = edmTypeReference == null;
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.JsonWriter.WriteName(name);
					this.valueSerializer.WriteComplexValue(odataComplexValue, edmTypeReference, false, flag, this.valueSerializer.CreateDuplicatePropertyNamesChecker());
					return;
				}
				IEdmTypeReference edmTypeReference2 = TypeNameOracle.ResolveAndValidateTypeNameForValue(this.valueSerializer.Model, edmTypeReference, value, flag);
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					string valueTypeNameForWriting = this.typeNameOracle.GetValueTypeNameForWriting(odataCollectionValue, edmTypeReference, edmTypeReference2, flag);
					if (valueTypeNameForWriting != null)
					{
						ODataJsonLightWriterUtils.WriteODataTypePropertyAnnotation(this.JsonWriter, name, valueTypeNameForWriting);
					}
					this.JsonWriter.WriteName(name);
					this.valueSerializer.WriteCollectionValue(odataCollectionValue, edmTypeReference, false, false, flag);
					return;
				}
				ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
				string valueTypeNameForWriting2 = this.typeNameOracle.GetValueTypeNameForWriting(odataPrimitiveValue, edmTypeReference, edmTypeReference2, flag);
				if (valueTypeNameForWriting2 != null)
				{
					ODataJsonLightWriterUtils.WriteODataTypePropertyAnnotation(this.JsonWriter, name, valueTypeNameForWriting2);
				}
				this.JsonWriter.WriteName(name);
				this.valueSerializer.WritePrimitiveValue(odataPrimitiveValue.Value, edmTypeReference);
				return;
			}
		}

		// Token: 0x0400034D RID: 845
		private readonly IODataJsonLightValueSerializer valueSerializer;

		// Token: 0x0400034E RID: 846
		private readonly JsonLightTypeNameOracle typeNameOracle;
	}
}
