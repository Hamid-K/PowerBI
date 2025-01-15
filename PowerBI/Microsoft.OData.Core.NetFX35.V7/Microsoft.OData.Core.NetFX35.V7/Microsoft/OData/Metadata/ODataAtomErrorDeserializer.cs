using System;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D9 RID: 473
	internal sealed class ODataAtomErrorDeserializer
	{
		// Token: 0x0600129F RID: 4767 RVA: 0x00035630 File Offset: 0x00033830
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
							goto IL_00C6;
						}
					}
					else
					{
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace))
						{
							goto IL_00C6;
						}
						string localName = xmlReader.LocalName;
						if (!(localName == "code"))
						{
							if (!(localName == "message"))
							{
								if (!(localName == "innererror"))
								{
									goto IL_00C6;
								}
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.InnerError, "innererror");
								odataError.InnerError = ODataAtomErrorDeserializer.ReadInnerErrorElement(xmlReader, 0, maxInnerErrorDepth);
							}
							else
							{
								ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Message, "message");
								odataError.Message = xmlReader.ReadElementValue();
							}
						}
						else
						{
							ODataAtomErrorDeserializer.VerifyErrorElementNotFound(ref duplicateErrorElementPropertyBitMask, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask.Code, "code");
							odataError.ErrorCode = xmlReader.ReadElementValue();
						}
					}
					IL_00CC:
					if (xmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00C6:
					xmlReader.Skip();
					goto IL_00CC;
				}
			}
			return odataError;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00035717 File Offset: 0x00033917
		private static void VerifyErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00035732 File Offset: 0x00033932
		private static void VerifyInnerErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00035750 File Offset: 0x00033950
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
							goto IL_00F6;
						}
					}
					else
					{
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace))
						{
							goto IL_00F6;
						}
						string localName = xmlReader.LocalName;
						if (!(localName == "message"))
						{
							if (!(localName == "type"))
							{
								if (!(localName == "stacktrace"))
								{
									if (!(localName == "internalexception"))
									{
										goto IL_00F6;
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
					IL_00FC:
					if (xmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00F6:
					xmlReader.Skip();
					goto IL_00FC;
				}
			}
			xmlReader.Read();
			return odataInnerError;
		}

		// Token: 0x02000311 RID: 785
		[Flags]
		private enum DuplicateErrorElementPropertyBitMask
		{
			// Token: 0x04000CCD RID: 3277
			None = 0,
			// Token: 0x04000CCE RID: 3278
			Code = 1,
			// Token: 0x04000CCF RID: 3279
			Message = 2,
			// Token: 0x04000CD0 RID: 3280
			InnerError = 4
		}

		// Token: 0x02000312 RID: 786
		[Flags]
		private enum DuplicateInnerErrorElementPropertyBitMask
		{
			// Token: 0x04000CD2 RID: 3282
			None = 0,
			// Token: 0x04000CD3 RID: 3283
			Message = 1,
			// Token: 0x04000CD4 RID: 3284
			TypeName = 2,
			// Token: 0x04000CD5 RID: 3285
			StackTrace = 4,
			// Token: 0x04000CD6 RID: 3286
			InternalException = 8
		}
	}
}
