using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F2 RID: 10482
	[GeneratedCode("DomGen", "2.0")]
	internal class Compiler : NameType
	{
		// Token: 0x1700697F RID: 27007
		// (get) Token: 0x06014A8B RID: 84619 RVA: 0x00315419 File Offset: 0x00313619
		public override string LocalName
		{
			get
			{
				return "Compiler";
			}
		}

		// Token: 0x17006980 RID: 27008
		// (get) Token: 0x06014A8C RID: 84620 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006981 RID: 27009
		// (get) Token: 0x06014A8D RID: 84621 RVA: 0x00315420 File Offset: 0x00313620
		internal override int ElementTypeId
		{
			get
			{
				return 10768;
			}
		}

		// Token: 0x06014A8E RID: 84622 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A8F RID: 84623 RVA: 0x003153D6 File Offset: 0x003135D6
		public Compiler()
		{
		}

		// Token: 0x06014A90 RID: 84624 RVA: 0x003153DE File Offset: 0x003135DE
		public Compiler(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A91 RID: 84625 RVA: 0x003153E7 File Offset: 0x003135E7
		public Compiler(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A92 RID: 84626 RVA: 0x003153F0 File Offset: 0x003135F0
		public Compiler(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014A93 RID: 84627 RVA: 0x00315427 File Offset: 0x00313627
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Compiler>(deep);
		}

		// Token: 0x04008F61 RID: 36705
		private const string tagName = "Compiler";

		// Token: 0x04008F62 RID: 36706
		private const byte tagNsId = 9;

		// Token: 0x04008F63 RID: 36707
		internal const int ElementTypeIdConst = 10768;
	}
}
