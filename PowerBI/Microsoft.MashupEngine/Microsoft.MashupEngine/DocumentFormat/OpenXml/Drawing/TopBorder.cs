using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FA RID: 10234
	[GeneratedCode("DomGen", "2.0")]
	internal class TopBorder : ThemeableLineStyleType
	{
		// Token: 0x17006507 RID: 25863
		// (get) Token: 0x06013FF5 RID: 81909 RVA: 0x002BF37F File Offset: 0x002BD57F
		public override string LocalName
		{
			get
			{
				return "top";
			}
		}

		// Token: 0x17006508 RID: 25864
		// (get) Token: 0x06013FF6 RID: 81910 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006509 RID: 25865
		// (get) Token: 0x06013FF7 RID: 81911 RVA: 0x0030E3E4 File Offset: 0x0030C5E4
		internal override int ElementTypeId
		{
			get
			{
				return 10270;
			}
		}

		// Token: 0x06013FF8 RID: 81912 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013FF9 RID: 81913 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public TopBorder()
		{
		}

		// Token: 0x06013FFA RID: 81914 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public TopBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FFB RID: 81915 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public TopBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FFC RID: 81916 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public TopBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FFD RID: 81917 RVA: 0x0030E3EB File Offset: 0x0030C5EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopBorder>(deep);
		}

		// Token: 0x04008892 RID: 34962
		private const string tagName = "top";

		// Token: 0x04008893 RID: 34963
		private const byte tagNsId = 10;

		// Token: 0x04008894 RID: 34964
		internal const int ElementTypeIdConst = 10270;
	}
}
