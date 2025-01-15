using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000256 RID: 598
	internal class ODataJsonLightValueSerializer : ODataJsonLightSerializer
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x000517EC File Offset: 0x0004F9EC
		internal ODataJsonLightValueSerializer(ODataJsonLightPropertySerializer propertySerializer, bool initContextUriBuilder = false)
			: base(propertySerializer.JsonLightOutputContext, initContextUriBuilder)
		{
			this.propertySerializer = propertySerializer;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00051802 File Offset: 0x0004FA02
		internal ODataJsonLightValueSerializer(ODataJsonLightOutputContext outputContext, bool initContextUriBuilder = false)
			: base(outputContext, initContextUriBuilder)
		{
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0005180C File Offset: 0x0004FA0C
		private ODataJsonLightPropertySerializer PropertySerializer
		{
			get
			{
				if (this.propertySerializer == null)
				{
					this.propertySerializer = new ODataJsonLightPropertySerializer(base.JsonLightOutputContext, false);
				}
				return this.propertySerializer;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0005182E File Offset: 0x0004FA2E
		public virtual void WriteNullValue()
		{
			base.JsonWriter.WriteValue(null);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0005183C File Offset: 0x0004FA3C
		public virtual void WriteEnumValue(ODataEnumValue value, IEdmTypeReference expectedTypeReference)
		{
			if (value.Value == null)
			{
				this.WriteNullValue();
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value.Value);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00051860 File Offset: 0x0004FA60
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		public virtual void WriteResourceValue(ODataResourceValue resourceValue, IEdmTypeReference metadataTypeReference, bool isOpenPropertyType, IDuplicatePropertyNameChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			base.JsonWriter.StartObjectScope();
			string text = resourceValue.TypeName;
			if (metadataTypeReference == null && !base.WritingResponse && text == null && base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueSerializer_NoExpectedTypeOrTypeNameSpecifiedForResourceValueRequest);
			}
			IEdmStructuredTypeReference edmStructuredTypeReference = (IEdmStructuredTypeReference)TypeNameOracle.ResolveAndValidateTypeForResourceValue(base.Model, metadataTypeReference, resourceValue, isOpenPropertyType, this.WriterValidator);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(resourceValue, metadataTypeReference, edmStructuredTypeReference, isOpenPropertyType);
			if (text != null)
			{
				base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(text, false);
			}
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(resourceValue.InstanceAnnotations, null, false);
			this.PropertySerializer.WriteProperties((edmStructuredTypeReference == null) ? null : edmStructuredTypeReference.StructuredDefinition(), resourceValue.Properties, true, duplicatePropertyNamesChecker, null);
			base.JsonWriter.EndObjectScope();
			this.DecreaseRecursionDepth();
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00051930 File Offset: 0x0004FB30
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		public virtual void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, IEdmTypeReference valueTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType)
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
			if (valueTypeReference == null)
			{
				valueTypeReference = TypeNameOracle.ResolveAndValidateTypeForCollectionValue(base.Model, metadataTypeReference, collectionValue, isOpenPropertyType, this.WriterValidator);
			}
			bool flag = false;
			if (isInUri)
			{
				text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, metadataTypeReference, valueTypeReference, isOpenPropertyType);
				if (!string.IsNullOrEmpty(text))
				{
					flag = true;
					base.JsonWriter.StartObjectScope();
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(text, false);
					base.JsonWriter.WriteValuePropertyName();
				}
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				IEdmTypeReference edmTypeReference = ((valueTypeReference == null) ? null : ((IEdmCollectionTypeReference)valueTypeReference).ElementType());
				IDuplicatePropertyNameChecker duplicatePropertyNameChecker = null;
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
					ODataResourceValue odataResourceValue = obj as ODataResourceValue;
					if (odataResourceValue != null)
					{
						if (duplicatePropertyNameChecker == null)
						{
							duplicatePropertyNameChecker = base.CreateDuplicatePropertyNameChecker();
						}
						this.WriteResourceValue(odataResourceValue, edmTypeReference, false, duplicatePropertyNameChecker);
						duplicatePropertyNameChecker.Reset();
					}
					else
					{
						ODataEnumValue odataEnumValue = obj as ODataEnumValue;
						if (odataEnumValue != null)
						{
							this.WriteEnumValue(odataEnumValue, edmTypeReference);
						}
						else
						{
							ODataUntypedValue odataUntypedValue = obj as ODataUntypedValue;
							if (odataUntypedValue != null)
							{
								this.WriteUntypedValue(odataUntypedValue);
							}
							else if (obj != null)
							{
								this.WritePrimitiveValue(obj, edmTypeReference);
							}
							else
							{
								this.WriteNullValue();
							}
						}
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

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00051AFC File Offset: 0x0004FCFC
		public virtual void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference)
		{
			this.WritePrimitiveValue(value, null, expectedTypeReference);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00051B08 File Offset: 0x0004FD08
		public virtual void WritePrimitiveValue(object value, IEdmTypeReference actualTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (actualTypeReference == null)
			{
				value = base.Model.ConvertToUnderlyingTypeIfUIntValue(value, expectedTypeReference);
				actualTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			}
			ODataPayloadValueConverter payloadValueConverter = base.JsonLightOutputContext.PayloadValueConverter;
			if (expectedTypeReference != null && payloadValueConverter.GetType() == typeof(ODataPayloadValueConverter))
			{
				this.WriterValidator.ValidateIsExpectedPrimitiveType(value, (IEdmPrimitiveTypeReference)actualTypeReference, expectedTypeReference);
			}
			value = payloadValueConverter.ConvertToPayloadValue(value, expectedTypeReference);
			if (actualTypeReference != null && actualTypeReference.IsSpatial())
			{
				PrimitiveConverter.Instance.WriteJsonLight(value, base.JsonWriter);
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00051B9B File Offset: 0x0004FD9B
		public virtual void WriteUntypedValue(ODataUntypedValue value)
		{
			if (string.IsNullOrEmpty(value.RawValue))
			{
				throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingRawValueOnUntyped);
			}
			base.JsonWriter.WriteRawValue(value.RawValue);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00051BC8 File Offset: 0x0004FDC8
		public virtual void WriteStreamValue(ODataBinaryStreamValue streamValue)
		{
			IJsonStreamWriter jsonStreamWriter = base.JsonWriter as IJsonStreamWriter;
			if (jsonStreamWriter == null)
			{
				base.JsonWriter.WritePrimitiveValue(new StreamReader(streamValue.Stream).ReadToEnd());
				return;
			}
			Stream stream = jsonStreamWriter.StartStreamValueScope();
			streamValue.Stream.CopyTo(stream);
			stream.Flush();
			stream.Dispose();
			jsonStreamWriter.EndStreamValueScope();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00051C25 File Offset: 0x0004FE25
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00051C42 File Offset: 0x0004FE42
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000B60 RID: 2912
		private int recursionDepth;

		// Token: 0x04000B61 RID: 2913
		private ODataJsonLightPropertySerializer propertySerializer;
	}
}
