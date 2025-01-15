using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000BE RID: 190
	internal class ODataJsonLightValueSerializer : ODataJsonLightSerializer, IODataJsonLightValueSerializer
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x000186BD File Offset: 0x000168BD
		internal ODataJsonLightValueSerializer(ODataJsonLightPropertySerializer propertySerializer, bool initContextUriBuilder = false)
			: base(propertySerializer.JsonLightOutputContext, initContextUriBuilder)
		{
			this.propertySerializer = propertySerializer;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000186D3 File Offset: 0x000168D3
		internal ODataJsonLightValueSerializer(ODataJsonLightOutputContext outputContext, bool initContextUriBuilder = false)
			: base(outputContext, initContextUriBuilder)
		{
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000186DD File Offset: 0x000168DD
		IJsonWriter IODataJsonLightValueSerializer.JsonWriter
		{
			get
			{
				return base.JsonWriter;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000186E5 File Offset: 0x000168E5
		IEdmModel IODataJsonLightValueSerializer.Model
		{
			get
			{
				return base.Model;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000186ED File Offset: 0x000168ED
		ODataMessageWriterSettings IODataJsonLightValueSerializer.Settings
		{
			get
			{
				return base.JsonLightOutputContext.MessageWriterSettings;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000186FA File Offset: 0x000168FA
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

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001871C File Offset: 0x0001691C
		public void WriteNullValue()
		{
			base.JsonWriter.WriteValue(null);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001872C File Offset: 0x0001692C
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
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
			IEdmComplexTypeReference edmComplexTypeReference = (IEdmComplexTypeReference)TypeNameOracle.ResolveAndValidateTypeForComplexValue(base.Model, metadataTypeReference, complexValue, isOpenPropertyType, this.WriterValidator);
			text = base.JsonLightOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, metadataTypeReference, edmComplexTypeReference, isOpenPropertyType);
			if (text != null)
			{
				base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(text);
			}
			base.InstanceAnnotationWriter.WriteInstanceAnnotations(complexValue.InstanceAnnotations, null);
			this.PropertySerializer.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), complexValue.Properties, true, duplicatePropertyNamesChecker, null);
			if (!isTopLevel)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00018812 File Offset: 0x00016A12
		public void WriteEnumValue(ODataEnumValue value, IEdmTypeReference expectedTypeReference)
		{
			if (value.Value == null)
			{
				this.WriteNullValue();
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value.Value);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00018834 File Offset: 0x00016A34
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		public void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference metadataTypeReference, IEdmTypeReference valueTypeReference, bool isTopLevelProperty, bool isInUri, bool isOpenPropertyType)
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
					base.ODataAnnotationWriter.WriteODataTypeInstanceAnnotation(text);
					base.JsonWriter.WriteValuePropertyName();
				}
			}
			base.JsonWriter.StartArrayScope();
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				IEdmTypeReference edmTypeReference = ((valueTypeReference == null) ? null : ((IEdmCollectionTypeReference)valueTypeReference).ElementType());
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = null;
				foreach (object obj in items)
				{
					this.WriterValidator.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
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

		// Token: 0x060006DC RID: 1756 RVA: 0x00018A04 File Offset: 0x00016C04
		public void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference)
		{
			value = base.Model.ConvertToUnderlyingTypeIfUIntValue(value, expectedTypeReference);
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			ODataPayloadValueConverter payloadValueConverter = base.JsonLightOutputContext.PayloadValueConverter;
			if (expectedTypeReference != null && payloadValueConverter.GetType() == typeof(ODataPayloadValueConverter))
			{
				this.WriterValidator.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			value = payloadValueConverter.ConvertToPayloadValue(value, expectedTypeReference);
			if (primitiveTypeReference != null && primitiveTypeReference.IsSpatial())
			{
				PrimitiveConverter.Instance.WriteJsonLight(value, base.JsonWriter);
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00018A8E File Offset: 0x00016C8E
		public void WriteUntypedValue(ODataUntypedValue value)
		{
			if (string.IsNullOrEmpty(value.RawValue))
			{
				throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingRawValueOnUntyped);
			}
			base.JsonWriter.WriteRawValue(value.RawValue);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00018AB9 File Offset: 0x00016CB9
		DuplicatePropertyNamesChecker IODataJsonLightValueSerializer.CreateDuplicatePropertyNamesChecker()
		{
			return base.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00018AC1 File Offset: 0x00016CC1
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		[Conditional("DEBUG")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00018AC3 File Offset: 0x00016CC3
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00018AE0 File Offset: 0x00016CE0
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400032A RID: 810
		private int recursionDepth;

		// Token: 0x0400032B RID: 811
		private ODataJsonLightPropertySerializer propertySerializer;
	}
}
