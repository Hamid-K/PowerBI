using System;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000048 RID: 72
	internal sealed class ODataAtomErrorDeserializer : ODataAtomDeserializer
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000AF18 File Offset: 0x00009118
		internal ODataAtomErrorDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AF24 File Offset: 0x00009124
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
							goto IL_00C9;
						}
					}
					else
					{
						string localName;
						if (!xmlReader.NamespaceEquals(xmlReader.ODataMetadataNamespace) || (localName = xmlReader.LocalName) == null)
						{
							goto IL_00C9;
						}
						if (!(localName == "code"))
						{
							if (!(localName == "message"))
							{
								if (!(localName == "innererror"))
								{
									goto IL_00C9;
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
					IL_00CF:
					if (xmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00C9:
					xmlReader.Skip();
					goto IL_00CF;
				}
			}
			return odataError;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B010 File Offset: 0x00009210
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B0D0 File Offset: 0x000092D0
		private static void VerifyErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B0EB File Offset: 0x000092EB
		private static void VerifyInnerErrorElementNotFound(ref ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementsFoundBitField, ODataAtomErrorDeserializer.DuplicateInnerErrorElementPropertyBitMask elementFoundBitMask, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomErrorDeserializer_MultipleInnerErrorElementsWithSameName(elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B108 File Offset: 0x00009308
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

		// Token: 0x02000049 RID: 73
		[Flags]
		private enum DuplicateErrorElementPropertyBitMask
		{
			// Token: 0x04000172 RID: 370
			None = 0,
			// Token: 0x04000173 RID: 371
			Code = 1,
			// Token: 0x04000174 RID: 372
			Message = 2,
			// Token: 0x04000175 RID: 373
			InnerError = 4
		}

		// Token: 0x0200004A RID: 74
		[Flags]
		private enum DuplicateInnerErrorElementPropertyBitMask
		{
			// Token: 0x04000177 RID: 375
			None = 0,
			// Token: 0x04000178 RID: 376
			Message = 1,
			// Token: 0x04000179 RID: 377
			TypeName = 2,
			// Token: 0x0400017A RID: 378
			StackTrace = 4,
			// Token: 0x0400017B RID: 379
			InternalException = 8
		}
	}
}
