using System;
using System.Collections;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000146 RID: 326
	internal class ODataJsonLightValueSerializer : ODataJsonLightSerializer, IODataJsonLightValueSerializer
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x0001BB5A File Offset: 0x00019D5A
		internal ODataJsonLightValueSerializer(ODataJsonLightPropertySerializer propertySerializer)
			: base(propertySerializer.JsonLightOutputContext)
		{
			this.propertySerializer = propertySerializer;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001BB6F File Offset: 0x00019D6F
		internal ODataJsonLightValueSerializer(ODataJsonLightOutputContext outputContext)
			: base(outputContext)
		{
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001BB78 File Offset: 0x00019D78
		IJsonWriter IODataJsonLightValueSerializer.JsonWriter
		{
			get
			{
				return base.JsonWriter;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001BB80 File Offset: 0x00019D80
		ODataVersion IODataJsonLightValueSerializer.Version
		{
			get
			{
				return base.Version;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001BB88 File Offset: 0x00019D88
		IEdmModel IODataJsonLightValueSerializer.Model
		{
			get
			{
				return base.Model;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001BB90 File Offset: 0x00019D90
		ODataMessageWriterSettings IODataJsonLightValueSerializer.Settings
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001BB9D File Offset: 0x00019D9D
		private ODataJsonLightPropertySerializer PropertySerializer
		{
			get
			{
				if (this.propertySerializer == null)
				{
					this.propertySerializer = new ODataJsonLightPropertySerializer(base.JsonLightOutputContext);
				}
				return this.propertySerializer;
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001BBBE File Offset: 0x00019DBE
		public void WriteNullValue()
		{
			base.JsonWriter.WriteValue(null);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001BBCC File Offset: 0x00019DCC
		public void WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isTopLevel, bool isOpenPropertyType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			if (!isTopLevel)
			{
				base.JsonWriter.StartObjectScope();
			}
			string text = complexValue.TypeName;
			if (isTopLevel)
			{
				if (text == null)
				{
					throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingTypeNameOnComplex);
				}
			}
			else if (metadataTypeReference == null && !base.WritingResponse && text == null && base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForComplexValueRequest);
			}
			IEdmComplexTypeReference edmComplexTypeReference = (IEdmComplexTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, complexValue, isOpenPropertyType);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, metadataTypeReference, edmComplexTypeReference, isOpenPropertyType);
			if (text != null)
			{
				ODataJsonLightWriterUtils.WriteODataTypeInstanceAnnotation(base.JsonWriter, text);
			}
			this.PropertySerializer.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), complexValue.Properties, true, duplicatePropertyNamesChecker, null);
			if (!isTopLevel)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001BC9C File Offset: 0x00019E9C
		public void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType)
		{
			this.IncreaseRecursionDepth();
			string text = collectionValue.TypeName;
			if (isTopLevelProperty)
			{
				if (text == null)
				{
					throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingTypeNameOnCollection);
				}
			}
			else if (metadataTypeReference == null && !base.WritingResponse && text == null && base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForCollectionValueInRequest);
			}
			IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, collectionValue, isOpenPropertyType);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, metadataTypeReference, edmCollectionTypeReference, isOpenPropertyType);
			bool flag = isInUri && !string.IsNullOrEmpty(text);
			if (flag)
			{
				base.JsonWriter.StartObjectScope();
				ODataJsonLightWriterUtils.WriteODataTypeInstanceAnnotation(base.JsonWriter, text);
				base.JsonWriter.WriteValuePropertyName();
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				IEdmTypeReference edmTypeReference = ((edmCollectionTypeReference == null) ? null : edmCollectionTypeReference.ElementType());
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
						this.WriteComplexValue(odataComplexValue, edmTypeReference, false, false, duplicatePropertyNamesChecker);
						duplicatePropertyNamesChecker.Clear();
					}
					else
					{
						this.WritePrimitiveValue(obj, edmTypeReference);
					}
				}
			}
			base.JsonWriter.EndArrayScope();
			if (flag)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001BE1C File Offset: 0x0001A01C
		public void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference)
		{
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			if (expectedTypeReference != null)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			if (primitiveTypeReference != null && primitiveTypeReference.IsSpatial())
			{
				PrimitiveConverter.Instance.WriteJsonLight(value, base.JsonWriter, base.Version);
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value, base.Version);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001BE75 File Offset: 0x0001A075
		DuplicatePropertyNamesChecker IODataJsonLightValueSerializer.CreateDuplicatePropertyNamesChecker()
		{
			return base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001BE7D File Offset: 0x0001A07D
		[Conditional("DEBUG")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001BE7F File Offset: 0x0001A07F
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001BE9C File Offset: 0x0001A09C
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000352 RID: 850
		private int recursionDepth;

		// Token: 0x04000353 RID: 851
		private ODataJsonLightPropertySerializer propertySerializer;
	}
}
