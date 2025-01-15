using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F0 RID: 10480
	[GeneratedCode("DomGen", "2.0")]
	internal class Artist : NameType
	{
		// Token: 0x17006979 RID: 27001
		// (get) Token: 0x06014A79 RID: 84601 RVA: 0x003153C8 File Offset: 0x003135C8
		public override string LocalName
		{
			get
			{
				return "Artist";
			}
		}

		// Token: 0x1700697A RID: 27002
		// (get) Token: 0x06014A7A RID: 84602 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700697B RID: 27003
		// (get) Token: 0x06014A7B RID: 84603 RVA: 0x003153CF File Offset: 0x003135CF
		internal override int ElementTypeId
		{
			get
			{
				return 10765;
			}
		}

		// Token: 0x06014A7C RID: 84604 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A7D RID: 84605 RVA: 0x003153D6 File Offset: 0x003135D6
		public Artist()
		{
		}

		// Token: 0x06014A7E RID: 84606 RVA: 0x003153DE File Offset: 0x003135DE
		public Artist(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A7F RID: 84607 RVA: 0x003153E7 File Offset: 0x003135E7
		public Artist(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A80 RID: 84608 RVA: 0x003153F0 File Offset: 0x003135F0
		public Artist(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014A81 RID: 84609 RVA: 0x003153F9 File Offset: 0x003135F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Artist>(deep);
		}

		// Token: 0x04008F5B RID: 36699
		private const string tagName = "Artist";

		// Token: 0x04008F5C RID: 36700
		private const byte tagNsId = 9;

		// Token: 0x04008F5D RID: 36701
		internal const int ElementTypeIdConst = 10765;
	}
}
