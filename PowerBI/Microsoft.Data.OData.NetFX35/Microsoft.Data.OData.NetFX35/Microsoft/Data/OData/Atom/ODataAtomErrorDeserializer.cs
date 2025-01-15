using System;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001F3 RID: 499
	internal sealed class ODataAtomErrorDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000E7F RID: 3711 RVA: 0x00034102 File Offset: 0x00032302
		internal ODataAtomErrorDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003410C File Offset: 0x0003230C
		internal static ODataError ReadErrorElement(BufferingXmlReader xmlReader, int maxInnerErrorDepth)
		{
			ODataError odataError = new ODataError();
			ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask duplicateErrorElementPropertyBitMask = ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.None;
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = xmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							goto IL_00E4;
						}
					}
					else
					{
						string localName;
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace) || (localName = xmlReader.LocalName) == null)
						{
							goto IL_00E4;
						}
						if (!(localName == "code"))
						{
							if (!(localName == "message"))
							{
								if (!(localName == "innererror"))
								{
									goto IL_00E4;
								}
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.InnerError, "innererror");
								odataError.InnerError = ODataAtomErrorDeserializer.ReadInnerErrorElement(xmlReader, 0, maxInnerErrorDepth);
							}
							else
							{
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Message, "message");
								odataError.MessageLanguage = xmlReader.GetAttribute(xmlReader.XmlLangAttributeName, xmlReader.XmlNamespace);
								odataError.Message = xmlReader.ReadElementValue();
							}
						}
						else
						{
							ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Code, "code");
							odataError.ErrorCode = xmlReader.ReadElementValue();
						}
					}
					IL_00EA:
					if (xmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00E4:
					xmlReader.Skip();
					goto IL_00EA;
				}
			}
			return odataError;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034214 File Offset: 0x00032414
		internal ODataError ReadTopLevelError()
		{
			ODataError odataError2;
			try
			{
				base.XmlReader.DisableInStreamErrorDetection = true;
				base.ReadPayloadStart();
				if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) || !base.XmlReader.LocalNameEquals(base.XmlReader.ODataErrorElementName))
				{
					throw new ODataErrorException(Strings.ODataAtomErrorDeserializer_InvalidRootElement(base.XmlReader.Name, base.XmlReader.NamespaceURI));
				}
				ODataError odataError = ODataAtomErrorDeserializer.ReadErrorElement(base.XmlReader, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
				base.XmlReader.Read();
				base.ReadPayloadEnd();
				odataError2 = odataError;
			}
			finally
			{
				base.XmlReader.DisableInStreamErrorDetection = false;
			}
			return odataError2;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000342D4 File Offset: 0x000324D4
		private static void VerifyErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000342EF File Offset: 0x000324EF
		private static void VerifyInnerErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003430C File Offset: 0x0003250C
		private static ODataInnerError ReadInnerErrorElement(BufferingXmlReader xmlReader, int recursionDepth, int maxInnerErrorDepth)
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref recursionDepth, maxInnerErrorDepth);
			ODataInnerError odataInnerError = new ODataInnerError();
			ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask duplicateInnerErrorElementPropertyBitMask = ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.None;
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = xmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							goto IL_00FC;
						}
					}
					else
					{
						string localName;
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace) || (localName = xmlReader.LocalName) == null)
						{
							goto IL_00FC;
						}
						if (!(localName == "message"))
						{
							if (!(localName == "type"))
							{
								if (!(localName == "stacktrace"))
								{
									if (!(localName == "internalexception"))
									{
										goto IL_00FC;
									}
									ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.InternalException, "internalexception");
									odataInnerError.InnerError = ODataAtomErrorDeserializer.ReadInnerErrorElement(xmlReader, recursionDepth, maxInnerErrorDepth);
								}
								else
								{
									ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.StackTrace, "stacktrace");
									odataInnerError.StackTrace = xmlReader.ReadElementValue();
								}
							}
							else
							{
								ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.TypeName, "type");
								odataInnerError.TypeName = xmlReader.ReadElementValue();
							}
						}
						else
						{
							ODataAtomErrorDeserializer.VerifyInnerErrorElementNotFound(ref duplicateInnerErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask.Message, "message");
							odataInnerError.Message = xmlReader.ReadElementValue();
						}
					}
					IL_0102:
					if (xmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00FC:
					xmlReader.Skip();
					goto IL_0102;
				}
			}
			xmlReader.Read();
			return odataInnerError;
		}

		// Token: 0x020001F4 RID: 500
		[Flags]
		private enum DuplicateErrorElementPropertyBitMask
		{
			// Token: 0x04000566 RID: 1382
			None = 0,
			// Token: 0x04000567 RID: 1383
			Code = 1,
			// Token: 0x04000568 RID: 1384
			Message = 2,
			// Token: 0x04000569 RID: 1385
			InnerError = 4
		}

		// Token: 0x020001F5 RID: 501
		[Flags]
		private enum DuplicateInnerErrorElementPropertyBitMask
		{
			// Token: 0x0400056B RID: 1387
			None = 0,
			// Token: 0x0400056C RID: 1388
			Message = 1,
			// Token: 0x0400056D RID: 1389
			TypeName = 2,
			// Token: 0x0400056E RID: 1390
			StackTrace = 4,
			// Token: 0x0400056F RID: 1391
			InternalException = 8
		}
	}
}
