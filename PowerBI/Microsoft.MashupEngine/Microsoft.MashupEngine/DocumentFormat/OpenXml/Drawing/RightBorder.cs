using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F9 RID: 10233
	[GeneratedCode("DomGen", "2.0")]
	internal class RightBorder : ThemeableLineStyleType
	{
		// Token: 0x17006504 RID: 25860
		// (get) Token: 0x06013FEC RID: 81900 RVA: 0x002BF396 File Offset: 0x002BD596
		public override string LocalName
		{
			get
			{
				return "right";
			}
		}

		// Token: 0x17006505 RID: 25861
		// (get) Token: 0x06013FED RID: 81901 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006506 RID: 25862
		// (get) Token: 0x06013FEE RID: 81902 RVA: 0x0030E3D4 File Offset: 0x0030C5D4
		internal override int ElementTypeId
		{
			get
			{
				return 10269;
			}
		}

		// Token: 0x06013FEF RID: 81903 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013FF0 RID: 81904 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public RightBorder()
		{
		}

		// Token: 0x06013FF1 RID: 81905 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public RightBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FF2 RID: 81906 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public RightBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FF3 RID: 81907 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public RightBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FF4 RID: 81908 RVA: 0x0030E3DB File Offset: 0x0030C5DB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightBorder>(deep);
		}

		// Token: 0x0400888F RID: 34959
		private const string tagName = "right";

		// Token: 0x04008890 RID: 34960
		private const byte tagNsId = 10;

		// Token: 0x04008891 RID: 34961
		internal const int ElementTypeIdConst = 10269;
	}
}
