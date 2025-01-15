using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200002C RID: 44
	internal class ODataAtomPropertyAndValueDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000193 RID: 403 RVA: 0x0000530C File Offset: 0x0000350C
		internal ODataAtomPropertyAndValueDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.ODataNullAttributeName = nameTable.Add("null");
			this.ODataCollectionItemElementName = nameTable.Add("element");
			this.AtomTypeAttributeName = nameTable.Add("type");
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005370 File Offset: 0x00003570
		internal ODataProperty ReadTopLevelProperty(IEdmStructuralProperty expectedProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			base.ReadPayloadStart();
			if (!base.UseServerFormatBehavior && !base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
			{
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace(base.XmlReader.NamespaceURI, base.XmlReader.ODataMetadataNamespace));
			}
			string expectedPropertyName = ReaderUtils.GetExpectedPropertyName(expectedProperty);
			ODataProperty odataProperty = this.ReadProperty(true, expectedPropertyName, expectedPropertyTypeReference, ODataNullValueBehaviorKind.Default);
			base.ReadPayloadEnd();
			return odataProperty;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000053E0 File Offset: 0x000035E0
		internal object ReadNonEntityValue(IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue)
		{
			return this.ReadNonEntityValueImplementation(expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, null);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000053FC File Offset: 0x000035FC
		protected EdmTypeKind GetNonEntityValueKind()
		{
			if (base.XmlReader.IsEmptyElement)
			{
				return EdmTypeKind.Primitive;
			}
			base.XmlReader.StartBuffering();
			EdmTypeKind edmTypeKind;
			try
			{
				base.XmlReader.Read();
				bool flag = false;
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else
					{
						if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
						{
							if (base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
							{
								flag = true;
							}
						}
						else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
						{
							break;
						}
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == 15)
					{
						goto Block_9;
					}
				}
				return EdmTypeKind.Complex;
				Block_9:
				edmTypeKind = (flag ? EdmTypeKind.Collection : EdmTypeKind.Primitive);
			}
			finally
			{
				base.XmlReader.StopBuffering();
			}
			return edmTypeKind;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000054E4 File Offset: 0x000036E4
		protected void ReadNonEntityValueAttributes(out string typeName, out bool isNull)
		{
			typeName = null;
			isNull = false;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
					{
						typeName = ReaderUtils.AddEdmPrefixOfTypeName(ReaderUtils.RemovePrefixOfTypeName(base.XmlReader.Value));
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataNullAttributeName))
					{
						isNull = ODataAtomReaderUtils.ReadMetadataNullAttributeValue(base.XmlReader.Value);
						break;
					}
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000557D File Offset: 0x0000377D
		protected void ReadProperties(IEdmStructuredType structuredType, ReadOnlyEnumerable<ODataProperty> properties, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.ReadPropertiesImplementation(structuredType, properties, duplicatePropertyNamesChecker);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005588 File Offset: 0x00003788
		private object ReadNonEntityValueImplementation(IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, string propertyName)
		{
			string itemTypeNameFromCollection;
			bool flag;
			this.ReadNonEntityValueAttributes(out itemTypeNameFromCollection, out flag);
			if (!flag)
			{
				bool flag2 = false;
				if (collectionValidator != null && itemTypeNameFromCollection == null)
				{
					itemTypeNameFromCollection = collectionValidator.ItemTypeNameFromCollection;
					EdmTypeKind itemTypeKindFromCollection = collectionValidator.ItemTypeKindFromCollection;
					flag2 = itemTypeKindFromCollection != EdmTypeKind.None;
				}
				EdmTypeKind edmTypeKind;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, ODataAtomPropertyAndValueDeserializer.edmStringType, expectedTypeReference, itemTypeNameFromCollection, base.Model, base.MessageReaderSettings, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out serializationTypeNameAnnotation);
				if (flag2)
				{
					serializationTypeNameAnnotation = new SerializationTypeNameAnnotation
					{
						TypeName = null
					};
				}
				if (collectionValidator != null)
				{
					collectionValidator.ValidateCollectionItem(itemTypeNameFromCollection, edmTypeKind);
				}
				switch (edmTypeKind)
				{
				case EdmTypeKind.Primitive:
					return this.ReadPrimitiveValue(edmTypeReference.AsPrimitive());
				case EdmTypeKind.Complex:
					return this.ReadComplexValue((edmTypeReference == null) ? null : edmTypeReference.AsComplex(), itemTypeNameFromCollection, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					return this.ReadCollectionValue(edmCollectionTypeReference, itemTypeNameFromCollection, serializationTypeNameAnnotation);
				}
				case EdmTypeKind.Enum:
					return this.ReadEnumValue(edmTypeReference.AsEnum());
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomPropertyAndValueDeserializer_ReadNonEntityValue));
			}
			return this.ReadNullValue(expectedTypeReference, validateNullValue, propertyName);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000056B0 File Offset: 0x000038B0
		private object ReadNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName)
		{
			base.XmlReader.SkipElementContent();
			ReaderValidationUtils.ValidateNullValue(base.Model, expectedTypeReference, base.MessageReaderSettings, validateNullValue, propertyName, default(bool?));
			return null;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000056E8 File Offset: 0x000038E8
		private void ReadPropertiesImplementation(IEdmStructuredType structuredType, ReadOnlyEnumerable<ODataProperty> properties, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				IEdmProperty edmProperty;
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
					{
						edmProperty = null;
						bool flag = false;
						bool flag2 = false;
						if (structuredType != null)
						{
							edmProperty = ReaderValidationUtils.ValidateValuePropertyDefined(base.XmlReader.LocalName, structuredType, base.MessageReaderSettings, out flag2);
							if (edmProperty != null && edmProperty.PropertyKind == EdmPropertyKind.Navigation)
							{
								break;
							}
							flag = edmProperty == null;
						}
						if (flag2)
						{
							base.XmlReader.Skip();
						}
						else
						{
							ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((base.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
							ODataProperty odataProperty = this.ReadProperty(false, (edmProperty == null) ? null : edmProperty.Name, (edmProperty == null) ? null : edmProperty.Type, odataNullValueBehaviorKind);
							if (odataProperty != null)
							{
								if (flag)
								{
									ValidationUtils.ValidateOpenPropertyValue(odataProperty.Name, odataProperty.Value);
								}
								duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
								properties.AddToSourceList(odataProperty);
							}
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == 15)
					{
						return;
					}
				}
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(edmProperty.Name, structuredType));
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005838 File Offset: 0x00003A38
		private ODataProperty ReadProperty(bool isTop, string expectedPropertyName, IEdmTypeReference expectedPropertyTypeReference, ODataNullValueBehaviorKind nullValueReadBehaviorKind)
		{
			ODataProperty odataProperty = new ODataProperty();
			string text = null;
			if (!isTop)
			{
				text = base.XmlReader.LocalName;
				ValidationUtils.ValidatePropertyName(text);
				ReaderValidationUtils.ValidateExpectedPropertyName(expectedPropertyName, text);
			}
			odataProperty.Name = text;
			object obj = this.ReadNonEntityValueImplementation(expectedPropertyTypeReference, null, null, nullValueReadBehaviorKind == ODataNullValueBehaviorKind.Default, text);
			if (nullValueReadBehaviorKind == ODataNullValueBehaviorKind.IgnoreValue && obj == null)
			{
				odataProperty = null;
			}
			else
			{
				odataProperty.Value = obj;
			}
			base.XmlReader.Read();
			return odataProperty;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000058A0 File Offset: 0x00003AA0
		private ODataEnumValue ReadEnumValue(IEdmEnumTypeReference actualValueTypeReference)
		{
			return AtomValueUtils.ReadEnumValue(base.XmlReader, actualValueTypeReference);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000058BC File Offset: 0x00003ABC
		private object ReadPrimitiveValue(IEdmPrimitiveTypeReference actualValueTypeReference)
		{
			return AtomValueUtils.ReadPrimitiveValue(base.XmlReader, actualValueTypeReference);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000058D8 File Offset: 0x00003AD8
		private ODataComplexValue ReadComplexValue(IEdmComplexTypeReference complexTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			IEdmComplexType edmComplexType = ((complexTypeReference == null) ? null : ((IEdmComplexType)complexTypeReference.Definition));
			odataComplexValue.TypeName = ((edmComplexType == null) ? payloadTypeName : edmComplexType.FullTypeName());
			if (serializationTypeNameAnnotation != null)
			{
				odataComplexValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			base.XmlReader.MoveToElement();
			if (duplicatePropertyNamesChecker == null)
			{
				duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			}
			else
			{
				duplicatePropertyNamesChecker.Clear();
			}
			ReadOnlyEnumerable<ODataProperty> readOnlyEnumerable = new ReadOnlyEnumerable<ODataProperty>();
			this.ReadPropertiesImplementation(edmComplexType, readOnlyEnumerable, duplicatePropertyNamesChecker);
			odataComplexValue.Properties = readOnlyEnumerable;
			this.DecreaseRecursionDepth();
			return odataComplexValue;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005960 File Offset: 0x00003B60
		private ODataCollectionValue ReadCollectionValue(IEdmCollectionTypeReference collectionTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			this.IncreaseRecursionDepth();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionTypeReference == null) ? payloadTypeName : collectionTypeReference.FullName());
			if (serializationTypeNameAnnotation != null)
			{
				odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			base.XmlReader.MoveToElement();
			List<object> list = new List<object>();
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				IEdmTypeReference edmTypeReference = ((collectionTypeReference == null) ? null : collectionTypeReference.ElementType());
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
				CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
				if (collectionTypeReference == null)
				{
					string text = ((payloadTypeName == null) ? null : EdmLibraryExtensions.GetCollectionItemTypeName(payloadTypeName));
					collectionWithoutExpectedTypeValidator = new CollectionWithoutExpectedTypeValidator(text);
				}
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
					{
						if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
						{
							base.XmlReader.Skip();
						}
						else
						{
							object obj = this.ReadNonEntityValueImplementation(edmTypeReference, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, true, null);
							base.XmlReader.Read();
							ValidationUtils.ValidateCollectionItem(obj, edmTypeReference.IsNullable());
							list.Add(obj);
						}
					}
					else
					{
						if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
						{
							break;
						}
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == 15)
					{
						goto IL_0175;
					}
				}
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(base.XmlReader.LocalName, base.XmlReader.ODataMetadataNamespace));
			}
			IL_0175:
			odataCollectionValue.Items = new ReadOnlyEnumerable(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005AF5 File Offset: 0x00003CF5
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005B12 File Offset: 0x00003D12
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005B22 File Offset: 0x00003D22
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this is needed in DEBUG build.")]
		[Conditional("DEBUG")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x04000110 RID: 272
		protected readonly string EmptyNamespace;

		// Token: 0x04000111 RID: 273
		protected readonly string ODataNullAttributeName;

		// Token: 0x04000112 RID: 274
		protected readonly string ODataCollectionItemElementName;

		// Token: 0x04000113 RID: 275
		protected readonly string AtomTypeAttributeName;

		// Token: 0x04000114 RID: 276
		private static readonly IEdmType edmStringType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String);

		// Token: 0x04000115 RID: 277
		private int recursionDepth;
	}
}
