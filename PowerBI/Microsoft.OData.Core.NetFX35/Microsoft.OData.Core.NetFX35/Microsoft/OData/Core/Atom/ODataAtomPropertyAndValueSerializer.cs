using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000035 RID: 53
	internal class ODataAtomPropertyAndValueSerializer : ODataAtomSerializer
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00006482 File Offset: 0x00004682
		internal ODataAtomPropertyAndValueSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000648B File Offset: 0x0000468B
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WritePayloadStart();
			this.WriteProperty(property, null, true, false, null, base.CreateDuplicatePropertyNamesChecker(), null);
			base.WritePayloadEnd();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000064AC File Offset: 0x000046AC
		internal void WriteInstanceAnnotations(IEnumerable<AtomInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (AtomInstanceAnnotation atomInstanceAnnotation in instanceAnnotations)
			{
				if (!hashSet.Add(atomInstanceAnnotation.TermName))
				{
					throw new ODataException(Strings.JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(atomInstanceAnnotation.TermName));
				}
				if (!tracker.IsAnnotationWritten(atomInstanceAnnotation.TermName))
				{
					this.WriteInstanceAnnotation(atomInstanceAnnotation);
					tracker.MarkAnnotationWritten(atomInstanceAnnotation.TermName);
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000653C File Offset: 0x0000473C
		internal void WriteInstanceAnnotation(AtomInstanceAnnotation instanceAnnotation)
		{
			if (base.MessageWriterSettings.ShouldSkipAnnotation(instanceAnnotation.TermName))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(instanceAnnotation.TermName, base.Model);
			this.WriteInstanceAnnotationStart(instanceAnnotation);
			ODataPrimitiveValue odataPrimitiveValue = instanceAnnotation.Value as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				this.WritePrimitiveInstanceAnnotationValue(odataPrimitiveValue, edmTypeReference);
			}
			else
			{
				ODataComplexValue odataComplexValue = instanceAnnotation.Value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.WriteComplexValue(odataComplexValue, edmTypeReference, false, false, null, null, base.CreateDuplicatePropertyNamesChecker(), null, null);
				}
				else
				{
					ODataCollectionValue odataCollectionValue = instanceAnnotation.Value as ODataCollectionValue;
					if (odataCollectionValue != null)
					{
						this.WriteCollectionValue(odataCollectionValue, edmTypeReference, false, false);
					}
					else
					{
						if (edmTypeReference != null && !edmTypeReference.IsNullable)
						{
							throw new ODataException(Strings.ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(instanceAnnotation.TermName, edmTypeReference.FullName()));
						}
						this.WriteNullAttribute();
					}
				}
			}
			this.WriteInstanceAnnotationEnd();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006600 File Offset: 0x00004800
		internal bool WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> cachedProperties, bool isWritingCollection, Action beforePropertiesAction, Action afterPropertiesAction, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			if (cachedProperties == null)
			{
				return false;
			}
			bool flag = false;
			foreach (ODataProperty odataProperty in cachedProperties)
			{
				flag |= this.WriteProperty(odataProperty, owningType, false, isWritingCollection, flag ? null : beforePropertiesAction, duplicatePropertyNamesChecker, projectedProperties);
			}
			if (afterPropertiesAction != null && flag)
			{
				afterPropertiesAction.Invoke();
			}
			return flag;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006670 File Offset: 0x00004870
		internal void WritePrimitiveValue(object value, CollectionWithoutExpectedTypeValidator collectionValidator, IEdmTypeReference expectedTypeReference, SerializationTypeNameAnnotation typeNameAnnotation)
		{
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			if (primitiveTypeReference == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
			}
			if (collectionValidator != null)
			{
				collectionValidator.ValidateCollectionItem(primitiveTypeReference.FullName(), EdmTypeKind.Primitive);
			}
			if (expectedTypeReference != null)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			string text;
			string valueTypeNameForWriting = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(value, primitiveTypeReference, typeNameAnnotation, collectionValidator, out text);
			if (valueTypeNameForWriting != null && valueTypeNameForWriting != "Edm.String")
			{
				this.WritePropertyTypeAttribute(valueTypeNameForWriting);
			}
			AtomValueUtils.WritePrimitiveValue(base.XmlWriter, value);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000066F8 File Offset: 0x000048F8
		internal void WriteEnumValue(ODataEnumValue value, CollectionWithoutExpectedTypeValidator collectionValidator, IEdmTypeReference expectedTypeReference, SerializationTypeNameAnnotation typeNameAnnotation)
		{
			string text;
			string valueTypeNameForWriting = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(value, expectedTypeReference, typeNameAnnotation, collectionValidator, out text);
			if (valueTypeNameForWriting != null)
			{
				this.WritePropertyTypeAttribute(valueTypeNameForWriting);
			}
			AtomValueUtils.WritePrimitiveValue(base.XmlWriter, value.Value);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006760 File Offset: 0x00004960
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Need to keep the logic together for better readability.")]
		internal bool WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isOpenPropertyType, bool isWritingCollection, Action beforeValueAction, Action afterValueAction, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, ProjectedPropertiesAnnotation projectedProperties)
		{
			string typeName = complexValue.TypeName;
			if (collectionValidator != null)
			{
				collectionValidator.ValidateCollectionItem(typeName, EdmTypeKind.Complex);
			}
			this.IncreaseRecursionDepth();
			IEdmComplexTypeReference edmComplexTypeReference = TypeNameOracle.ResolveAndValidateTypeForComplexValue(base.Model, metadataTypeReference, complexValue, isOpenPropertyType, this.WriterValidator).AsComplexOrNull();
			string text;
			typeName = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, edmComplexTypeReference, complexValue.GetAnnotation<SerializationTypeNameAnnotation>(), collectionValidator, out text);
			Action action = beforeValueAction;
			if (typeName != null)
			{
				if (beforeValueAction != null)
				{
					action = delegate
					{
						beforeValueAction.Invoke();
						this.WritePropertyTypeAttribute(typeName);
					};
				}
				else
				{
					this.WritePropertyTypeAttribute(typeName);
				}
			}
			bool flag = this.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), complexValue.Properties, isWritingCollection, action, afterValueAction, duplicatePropertyNamesChecker, projectedProperties);
			this.DecreaseRecursionDepth();
			return flag;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006853 File Offset: 0x00004A53
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006858 File Offset: 0x00004A58
		private void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType, bool isWritingCollection)
		{
			this.IncreaseRecursionDepth();
			IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeForCollectionValue(base.Model, propertyTypeReference, collectionValue, isOpenPropertyType, this.WriterValidator);
			string text;
			string valueTypeNameForWriting = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, edmCollectionTypeReference, collectionValue.GetAnnotation<SerializationTypeNameAnnotation>(), null, out text);
			if (valueTypeNameForWriting != null)
			{
				this.WritePropertyTypeAttribute(valueTypeNameForWriting);
			}
			IEdmTypeReference edmTypeReference = ((edmCollectionTypeReference == null) ? null : edmCollectionTypeReference.ElementType());
			CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = new CollectionWithoutExpectedTypeValidator(text);
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = null;
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
					base.XmlWriter.WriteStartElement("m", "element", "http://docs.oasis-open.org/odata/ns/metadata");
					ODataComplexValue odataComplexValue = obj as ODataComplexValue;
					if (odataComplexValue != null)
					{
						if (duplicatePropertyNamesChecker == null)
						{
							duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
						}
						this.WriteComplexValue(odataComplexValue, edmTypeReference, false, isWritingCollection, null, null, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, null);
						duplicatePropertyNamesChecker.Clear();
					}
					else
					{
						ODataEnumValue odataEnumValue = obj as ODataEnumValue;
						if (odataEnumValue != null)
						{
							this.WriteEnumValue(odataEnumValue, collectionWithoutExpectedTypeValidator, edmTypeReference, null);
						}
						else if (obj != null)
						{
							this.WritePrimitiveValue(obj, collectionWithoutExpectedTypeValidator, edmTypeReference, null);
						}
						else
						{
							this.WriteNullCollectionElementValue(edmTypeReference);
						}
					}
					base.XmlWriter.WriteEndElement();
				}
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000069C0 File Offset: 0x00004BC0
		private void WritePrimitiveInstanceAnnotationValue(ODataPrimitiveValue primitiveValue, IEdmTypeReference expectedTypeReference)
		{
			object value = primitiveValue.Value;
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			string text = AtomInstanceAnnotation.LookupAttributeValueNotationNameByEdmTypeKind(primitiveTypeReference.PrimitiveKind());
			if (text != null)
			{
				if (expectedTypeReference != null)
				{
					ValidationUtils.ValidateIsExpectedPrimitiveType(primitiveValue.Value, primitiveTypeReference, expectedTypeReference);
				}
				base.XmlWriter.WriteAttributeString(text, AtomValueUtils.ConvertPrimitiveToString(value));
				return;
			}
			this.WritePrimitiveValue(value, null, expectedTypeReference, primitiveValue.GetAnnotation<SerializationTypeNameAnnotation>());
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006A24 File Offset: 0x00004C24
		private bool WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, bool isWritingCollection, Action beforePropertyAction, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
			object value = property.Value;
			string name = property.Name;
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = null;
			if (!ODataAtomPropertyAndValueSerializer.ShouldWritePropertyInContent(projectedProperties, name))
			{
				return false;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(property);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(name, owningType, true);
			IEdmTypeReference edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(name));
			}
			if (value == null)
			{
				this.WriteNullPropertyValue(edmTypeReference, name, isTopLevel, isWritingCollection, beforePropertyAction);
				return true;
			}
			bool flag = owningType != null && owningType.IsOpen && edmTypeReference == null;
			if (flag)
			{
				ValidationUtils.ValidateOpenPropertyValue(name, value);
			}
			if (odataComplexValue != null)
			{
				return this.WriteComplexValueProperty(odataComplexValue, name, isTopLevel, isWritingCollection, beforePropertyAction, edmTypeReference, flag, projectedPropertiesAnnotation);
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				this.WriteCollectionValueProperty(odataCollectionValue, name, isTopLevel, isWritingCollection, beforePropertyAction, edmTypeReference, flag);
				return true;
			}
			this.WritePropertyStart(beforePropertyAction, property, isWritingCollection, isTopLevel);
			SerializationTypeNameAnnotation annotation = property.ODataValue.GetAnnotation<SerializationTypeNameAnnotation>();
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				this.WriteEnumValue(odataEnumValue, null, edmTypeReference, annotation);
			}
			else
			{
				this.WritePrimitiveValue(value, null, edmTypeReference, annotation);
			}
			this.WritePropertyEnd();
			return true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006B74 File Offset: 0x00004D74
		private bool WriteComplexValueProperty(ODataComplexValue complexValue, string propertyName, bool isTopLevel, bool isWritingCollection, Action beforeValueAction, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType, ProjectedPropertiesAnnotation complexValueProjectedProperties)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			if (isTopLevel)
			{
				this.WritePropertyStart(beforeValueAction, propertyName, complexValue, isWritingCollection, true);
				this.WriteComplexValue(complexValue, propertyTypeReference, isOpenPropertyType, isWritingCollection, null, null, duplicatePropertyNamesChecker, null, null);
				this.WritePropertyEnd();
				return true;
			}
			return this.WriteComplexValue(complexValue, propertyTypeReference, isOpenPropertyType, isWritingCollection, delegate
			{
				this.WritePropertyStart(beforeValueAction, propertyName, complexValue, isWritingCollection, false);
			}, new Action(this.WritePropertyEnd), duplicatePropertyNamesChecker, null, complexValueProjectedProperties);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006C2B File Offset: 0x00004E2B
		private void WriteCollectionValueProperty(ODataCollectionValue collectionValue, string propertyName, bool isTopLevel, bool isWritingTopLevelCollection, Action beforePropertyAction, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType)
		{
			this.WritePropertyStart(beforePropertyAction, propertyName, collectionValue, isWritingTopLevelCollection, isTopLevel);
			this.WriteCollectionValue(collectionValue, propertyTypeReference, isOpenPropertyType, isWritingTopLevelCollection);
			this.WritePropertyEnd();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006C50 File Offset: 0x00004E50
		private static bool ShouldWritePropertyInContent(ProjectedPropertiesAnnotation projectedProperties, string propertyName)
		{
			return !projectedProperties.ShouldSkipProperty(propertyName);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006C6C File Offset: 0x00004E6C
		private void WriteNullPropertyValue(IEdmTypeReference propertyTypeReference, string propertyName, bool isTopLevel, bool isWritingCollection, Action beforePropertyAction)
		{
			WriterValidationUtils.ValidateNullPropertyValue(propertyTypeReference, propertyName, base.MessageWriterSettings.WriterBehavior, base.Model);
			this.WritePropertyStart(beforePropertyAction, propertyName, new ODataNullValue(), isWritingCollection, isTopLevel);
			if (propertyTypeReference != null && !base.UseDefaultFormatBehavior)
			{
				string text = propertyTypeReference.FullName();
				if (text != "Edm.String" && (propertyTypeReference.IsODataPrimitiveTypeKind() || base.UseServerFormatBehavior))
				{
					this.WritePropertyTypeAttribute(text);
				}
			}
			this.WriteNullAttribute();
			this.WritePropertyEnd();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006CE4 File Offset: 0x00004EE4
		private void WriteNullCollectionElementValue(IEdmTypeReference propertyTypeReference)
		{
			ValidationUtils.ValidateNullCollectionItem(propertyTypeReference, base.AtomOutputContext.MessageWriterSettings.WriterBehavior);
			base.AtomOutputContext.XmlWriter.WriteAttributeString("null", "http://docs.oasis-open.org/odata/ns/metadata", "true");
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006D1C File Offset: 0x00004F1C
		private void WritePropertyStart(Action beforePropertyCallback, string propertyName, ODataValue value, bool isWritingCollection, bool isTopLevel)
		{
			if (beforePropertyCallback != null)
			{
				beforePropertyCallback.Invoke();
			}
			if (!isTopLevel)
			{
				base.XmlWriter.WriteStartElement(isWritingCollection ? string.Empty : "d", propertyName, "http://docs.oasis-open.org/odata/ns/data");
				return;
			}
			base.XmlWriter.WriteStartElement("m", "value", "http://docs.oasis-open.org/odata/ns/metadata");
			ODataAtomSerializer.DefaultNamespaceFlags defaultNamespaceFlags = ODataAtomSerializer.DefaultNamespaceFlags.GeoRss | ODataAtomSerializer.DefaultNamespaceFlags.Gml;
			if (!base.MessageWriterSettings.AlwaysUseDefaultXmlNamespaceForRootElement)
			{
				defaultNamespaceFlags |= ODataAtomSerializer.DefaultNamespaceFlags.OData;
			}
			base.WriteDefaultNamespaceAttributes(defaultNamespaceFlags);
			ODataContextUriBuilder odataContextUriBuilder = base.AtomOutputContext.CreateContextUriBuilder();
			ODataPayloadKind odataPayloadKind = (base.AtomOutputContext.MessageWriterSettings.IsIndividualProperty ? ODataPayloadKind.IndividualProperty : ODataPayloadKind.Property);
			ODataContextUrlInfo odataContextUrlInfo = ODataContextUrlInfo.Create(value, base.AtomOutputContext.MessageWriterSettings.ODataUri, null);
			base.WriteContextUriProperty(odataContextUriBuilder.BuildContextUri(odataPayloadKind, odataContextUrlInfo));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006DD7 File Offset: 0x00004FD7
		private void WritePropertyStart(Action beforePropertyCallback, ODataProperty property, bool isWritingCollection, bool isTopLevel)
		{
			this.WritePropertyStart(beforePropertyCallback, property.Name, property.ODataValue, isWritingCollection, isTopLevel);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006DEF File Offset: 0x00004FEF
		private void WritePropertyEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006DFC File Offset: 0x00004FFC
		private void WriteInstanceAnnotationStart(AtomInstanceAnnotation instanceAnnotation)
		{
			base.XmlWriter.WriteStartElement("annotation", "http://docs.oasis-open.org/odata/ns/metadata");
			base.XmlWriter.WriteAttributeString("term", string.Empty, instanceAnnotation.TermName);
			if (!string.IsNullOrEmpty(instanceAnnotation.Target))
			{
				base.XmlWriter.WriteAttributeString("target", string.Empty, instanceAnnotation.Target);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006E61 File Offset: 0x00005061
		private void WriteInstanceAnnotationEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006E6E File Offset: 0x0000506E
		private void WritePropertyTypeAttribute(string typeName)
		{
			base.XmlWriter.WriteAttributeString("m", "type", "http://docs.oasis-open.org/odata/ns/metadata", ODataAtomWriterUtils.PrefixTypeName(WriterUtils.RemoveEdmPrefixFromTypeName(typeName)));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006E95 File Offset: 0x00005095
		private void WriteNullAttribute()
		{
			base.XmlWriter.WriteAttributeString("m", "null", "http://docs.oasis-open.org/odata/ns/metadata", "true");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006EB6 File Offset: 0x000050B6
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006ED3 File Offset: 0x000050D3
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400012C RID: 300
		private int recursionDepth;
	}
}
