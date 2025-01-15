using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001E1 RID: 481
	internal abstract class ODataAtomMetadataDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000E0B RID: 3595 RVA: 0x0003209C File Offset: 0x0003029C
		internal ODataAtomMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000320DE File Offset: 0x000302DE
		protected bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000320F0 File Offset: 0x000302F0
		protected AtomPersonMetadata ReadAtomPersonConstruct(EpmTargetPathSegment epmTargetPathSegment)
		{
			AtomPersonMetadata atomPersonMetadata = new AtomPersonMetadata();
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != 1)
					{
						if (nodeType != 15)
						{
							goto IL_00FE;
						}
					}
					else
					{
						EpmTargetPathSegment epmTargetPathSegment2;
						string localName;
						if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !this.ShouldReadElement(epmTargetPathSegment, base.XmlReader.LocalName, out epmTargetPathSegment2) || (localName = base.XmlReader.LocalName) == null)
						{
							goto IL_00FE;
						}
						if (!(localName == "name"))
						{
							if (!(localName == "uri"))
							{
								if (!(localName == "email"))
								{
									goto IL_00FE;
								}
								atomPersonMetadata.Email = this.ReadElementStringValue();
							}
							else
							{
								Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
								string text = this.ReadElementStringValue();
								if (epmTargetPathSegment2 != null)
								{
									atomPersonMetadata.UriFromEpm = text;
								}
								if (this.ReadAtomMetadata)
								{
									atomPersonMetadata.Uri = base.ProcessUriFromPayload(text, xmlBaseUri);
								}
							}
						}
						else
						{
							atomPersonMetadata.Name = this.ReadElementStringValue();
						}
					}
					IL_0109:
					if (base.XmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00FE:
					base.XmlReader.Skip();
					goto IL_0109;
				}
			}
			base.XmlReader.Read();
			return atomPersonMetadata;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00032228 File Offset: 0x00030428
		protected DateTimeOffset? ReadAtomDateConstruct()
		{
			string text = this.ReadElementStringValue();
			text = text.Trim();
			if (text.Length >= 20)
			{
				if (text.get_Chars(19) == '.')
				{
					int num = 20;
					while (text.Length > num && char.IsDigit(text.get_Chars(num)))
					{
						num++;
					}
					text = text.Substring(0, 19) + text.Substring(num);
				}
				DateTimeOffset dateTimeOffset;
				if (DateTimeOffset.TryParseExact(text, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture.DateTimeFormat, 0, ref dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
				if (DateTimeOffset.TryParseExact(text, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture.DateTimeFormat, 80, ref dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
			}
			return new DateTimeOffset?(PlatformHelper.ConvertStringToDateTimeOffset(text));
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000322E0 File Offset: 0x000304E0
		protected string ReadAtomDateConstructAsString()
		{
			return this.ReadElementStringValue();
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000322F8 File Offset: 0x000304F8
		protected AtomTextConstruct ReadAtomTextConstruct()
		{
			AtomTextConstruct atomTextConstruct = new AtomTextConstruct();
			string text = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && string.CompareOrdinal(base.XmlReader.LocalName, "type") == 0)
				{
					text = base.XmlReader.Value;
				}
			}
			base.XmlReader.MoveToElement();
			if (text != null)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "text")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Text;
						goto IL_00C5;
					}
					if (text2 == "html")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Html;
						goto IL_00C5;
					}
					if (text2 == "xhtml")
					{
						atomTextConstruct.Kind = AtomTextConstructKind.Xhtml;
						goto IL_00C5;
					}
				}
				throw new ODataException(Strings.ODataAtomEntryMetadataDeserializer_InvalidTextConstructKind(text, base.XmlReader.LocalName));
			}
			atomTextConstruct.Kind = AtomTextConstructKind.Text;
			IL_00C5:
			if (atomTextConstruct.Kind == AtomTextConstructKind.Xhtml)
			{
				atomTextConstruct.Text = base.XmlReader.ReadInnerXml();
			}
			else
			{
				atomTextConstruct.Text = this.ReadElementStringValue();
			}
			return atomTextConstruct;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000323F3 File Offset: 0x000305F3
		protected string ReadElementStringValue()
		{
			if (base.UseClientFormatBehavior)
			{
				return base.XmlReader.ReadFirstTextNodeValue();
			}
			return base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00032414 File Offset: 0x00030614
		protected AtomTextConstruct ReadTitleElement()
		{
			return this.ReadAtomTextConstruct();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003243C File Offset: 0x0003063C
		protected bool ShouldReadElement(EpmTargetPathSegment parentSegment, string segmentName, out EpmTargetPathSegment subSegment)
		{
			subSegment = null;
			if (parentSegment != null)
			{
				subSegment = Enumerable.FirstOrDefault<EpmTargetPathSegment>(parentSegment.SubSegments, (EpmTargetPathSegment segment) => string.CompareOrdinal(segment.SegmentName, segmentName) == 0);
				if (subSegment != null && subSegment.EpmInfo != null && subSegment.EpmInfo.Attribute.KeepInContent)
				{
					return this.ReadAtomMetadata;
				}
			}
			return subSegment != null || this.ReadAtomMetadata;
		}

		// Token: 0x04000520 RID: 1312
		private readonly string EmptyNamespace;

		// Token: 0x04000521 RID: 1313
		private readonly string AtomNamespace;
	}
}
