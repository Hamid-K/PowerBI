using System;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ODataAtomErrorDeserializer
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x00021DBC File Offset: 0x0001FFBC
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
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
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
					if (xmlReader.NodeType == XmlNodeType.EndElement)
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

		// Token: 0x06000E5C RID: 3676 RVA: 0x00021EA3 File Offset: 0x000200A3
		private static void VerifyErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00021EBE File Offset: 0x000200BE
		private static void VerifyInnerErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00021EDC File Offset: 0x000200DC
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
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
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
					if (xmlReader.NodeType == XmlNodeType.EndElement)
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

		// Token: 0x02000361 RID: 865
		[Flags]
		private enum DuplicateErrorElementPropertyBitMask
		{
			// Token: 0x04000E19 RID: 3609
			None = 0,
			// Token: 0x04000E1A RID: 3610
			Code = 1,
			// Token: 0x04000E1B RID: 3611
			Message = 2,
			// Token: 0x04000E1C RID: 3612
			InnerError = 4
		}

		// Token: 0x02000362 RID: 866
		[Flags]
		private enum DuplicateInnerErrorElementPropertyBitMask
		{
			// Token: 0x04000E1E RID: 3614
			None = 0,
			// Token: 0x04000E1F RID: 3615
			Message = 1,
			// Token: 0x04000E20 RID: 3616
			TypeName = 2,
			// Token: 0x04000E21 RID: 3617
			StackTrace = 4,
			// Token: 0x04000E22 RID: 3618
			InternalException = 8
		}
	}
}
