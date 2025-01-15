using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x0200221B RID: 8731
	[GeneratedCode("DomGen", "2.0")]
	internal class LinkType : OpenXmlLeafTextElement
	{
		// Token: 0x17003932 RID: 14642
		// (get) Token: 0x0600E00D RID: 57357 RVA: 0x002BF9A0 File Offset: 0x002BDBA0
		public override string LocalName
		{
			get
			{
				return "LinkType";
			}
		}

		// Token: 0x17003933 RID: 14643
		// (get) Token: 0x0600E00E RID: 57358 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003934 RID: 14644
		// (get) Token: 0x0600E00F RID: 57359 RVA: 0x002BF9A7 File Offset: 0x002BDBA7
		internal override int ElementTypeId
		{
			get
			{
				return 12424;
			}
		}

		// Token: 0x0600E010 RID: 57360 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E011 RID: 57361 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LinkType()
		{
		}

		// Token: 0x0600E012 RID: 57362 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LinkType(string text)
			: base(text)
		{
		}

		// Token: 0x0600E013 RID: 57363 RVA: 0x002BF9B0 File Offset: 0x002BDBB0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<OleLinkValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600E014 RID: 57364 RVA: 0x002BF9CB File Offset: 0x002BDBCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkType>(deep);
		}

		// Token: 0x04006DCB RID: 28107
		private const string tagName = "LinkType";

		// Token: 0x04006DCC RID: 28108
		private const byte tagNsId = 27;

		// Token: 0x04006DCD RID: 28109
		internal const int ElementTypeIdConst = 12424;
	}
}
