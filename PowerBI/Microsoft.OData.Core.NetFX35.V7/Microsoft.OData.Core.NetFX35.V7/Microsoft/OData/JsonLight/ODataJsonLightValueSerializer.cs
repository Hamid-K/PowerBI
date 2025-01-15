using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200021D RID: 541
	internal class ODataJsonLightValueSerializer : ODataJsonLightSerializer
	{
		// Token: 0x060015EF RID: 5615 RVA: 0x0004328C File Offset: 0x0004148C
		internal ODataJsonLightValueSerializer(ODataJsonLightPropertySerializer propertySerializer, bool initContextUriBuilder = false)
			: base(propertySerializer.JsonLightOutputContext, initContextUriBuilder)
		{
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0004329B File Offset: 0x0004149B
		internal ODataJsonLightValueSerializer(ODataJsonLightOutputContext outputContext, bool initContextUriBuilder = false)
			: base(outputContext, initContextUriBuilder)
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000432A5 File Offset: 0x000414A5
		public virtual void WriteNullValue()
		{
			base.JsonWriter.WriteValue(null);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000432B3 File Offset: 0x000414B3
		public virtual void WriteEnumValue(ODataEnumValue value, IEdmTypeReference expectedTypeReference)
		{
			if (value.Value == null)
			{
				this.WriteNullValue();
				return;
			}
			base.JsonWriter.WritePrimitiveValue(value.Value);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000432D8 File Offset: 0x000414D8
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
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
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
			base.JsonWriter.EndArrayScope();
			if (flag)
			{
				base.JsonWriter.EndObjectScope();
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0004346C File Offset: 0x0004166C
		public virtual void WritePrimitiveValue(object value, IEdmTypeReference expectedTypeReference)
		{
			this.WritePrimitiveValue(value, null, expectedTypeReference);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00043478 File Offset: 0x00041678
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

		// Token: 0x060015F6 RID: 5622 RVA: 0x0004350B File Offset: 0x0004170B
		public virtual void WriteUntypedValue(ODataUntypedValue value)
		{
			if (string.IsNullOrEmpty(value.RawValue))
			{
				throw new ODataException(Strings.ODataJsonLightValueSerializer_MissingRawValueOnUntyped);
			}
			base.JsonWriter.WriteRawValue(value.RawValue);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00043536 File Offset: 0x00041736
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00043553 File Offset: 0x00041753
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000A44 RID: 2628
		private int recursionDepth;
	}
}
