using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002947 RID: 10567
	[GeneratedCode("DomGen", "2.0")]
	internal class LinksUpToDate : OpenXmlLeafTextElement
	{
		// Token: 0x17006B50 RID: 27472
		// (get) Token: 0x06014EEA RID: 85738 RVA: 0x00318DB0 File Offset: 0x00316FB0
		public override string LocalName
		{
			get
			{
				return "LinksUpToDate";
			}
		}

		// Token: 0x17006B51 RID: 27473
		// (get) Token: 0x06014EEB RID: 85739 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B52 RID: 27474
		// (get) Token: 0x06014EEC RID: 85740 RVA: 0x00318DB7 File Offset: 0x00316FB7
		internal override int ElementTypeId
		{
			get
			{
				return 11016;
			}
		}

		// Token: 0x06014EED RID: 85741 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EEE RID: 85742 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public LinksUpToDate()
		{
		}

		// Token: 0x06014EEF RID: 85743 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public LinksUpToDate(string text)
			: base(text)
		{
		}

		// Token: 0x06014EF0 RID: 85744 RVA: 0x00318DC0 File Offset: 0x00316FC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014EF1 RID: 85745 RVA: 0x00318DDB File Offset: 0x00316FDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinksUpToDate>(deep);
		}

		// Token: 0x040090AF RID: 37039
		private const string tagName = "LinksUpToDate";

		// Token: 0x040090B0 RID: 37040
		private const byte tagNsId = 3;

		// Token: 0x040090B1 RID: 37041
		internal const int ElementTypeIdConst = 11016;
	}
}
