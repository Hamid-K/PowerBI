using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FD RID: 10493
	[GeneratedCode("DomGen", "2.0")]
	internal class Writer : NameType
	{
		// Token: 0x170069A0 RID: 27040
		// (get) Token: 0x06014AEE RID: 84718 RVA: 0x00315516 File Offset: 0x00313716
		public override string LocalName
		{
			get
			{
				return "Writer";
			}
		}

		// Token: 0x170069A1 RID: 27041
		// (get) Token: 0x06014AEF RID: 84719 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069A2 RID: 27042
		// (get) Token: 0x06014AF0 RID: 84720 RVA: 0x0031551D File Offset: 0x0031371D
		internal override int ElementTypeId
		{
			get
			{
				return 10780;
			}
		}

		// Token: 0x06014AF1 RID: 84721 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AF2 RID: 84722 RVA: 0x003153D6 File Offset: 0x003135D6
		public Writer()
		{
		}

		// Token: 0x06014AF3 RID: 84723 RVA: 0x003153DE File Offset: 0x003135DE
		public Writer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AF4 RID: 84724 RVA: 0x003153E7 File Offset: 0x003135E7
		public Writer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AF5 RID: 84725 RVA: 0x003153F0 File Offset: 0x003135F0
		public Writer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AF6 RID: 84726 RVA: 0x00315524 File Offset: 0x00313724
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Writer>(deep);
		}

		// Token: 0x04008F82 RID: 36738
		private const string tagName = "Writer";

		// Token: 0x04008F83 RID: 36739
		private const byte tagNsId = 9;

		// Token: 0x04008F84 RID: 36740
		internal const int ElementTypeIdConst = 10780;
	}
}
