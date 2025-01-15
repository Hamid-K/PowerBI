using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000044 RID: 68
	internal abstract class ODataAtomMetadataDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00009F44 File Offset: 0x00008144
		internal ODataAtomMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009F86 File Offset: 0x00008186
		protected bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009F98 File Offset: 0x00008198
		protected AtomPersonMetadata ReadAtomPersonConstruct()
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
							goto IL_00E3;
						}
					}
					else
					{
						string localName;
						if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !this.ReadAtomMetadata || (localName = base.XmlReader.LocalName) == null)
						{
							goto IL_00E3;
						}
						if (!(localName == "name"))
						{
							if (!(localName == "uri"))
							{
								if (!(localName == "email"))
								{
									goto IL_00E3;
								}
								atomPersonMetadata.Email = this.ReadElementStringValue();
							}
							else
							{
								Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
								string text = this.ReadElementStringValue();
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
					IL_00EE:
					if (base.XmlReader.NodeType == 15)
					{
						break;
					}
					continue;
					IL_00E3:
					base.XmlReader.Skip();
					goto IL_00EE;
				}
			}
			base.XmlReader.Read();
			return atomPersonMetadata;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A0B4 File Offset: 0x000082B4
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000A16C File Offset: 0x0000836C
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

		// Token: 0x06000295 RID: 661 RVA: 0x0000A267 File Offset: 0x00008467
		protected string ReadElementStringValue()
		{
			return base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A274 File Offset: 0x00008474
		protected AtomTextConstruct ReadTitleElement()
		{
			return this.ReadAtomTextConstruct();
		}

		// Token: 0x0400016B RID: 363
		private readonly string EmptyNamespace;

		// Token: 0x0400016C RID: 364
		private readonly string AtomNamespace;
	}
}
