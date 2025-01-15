using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F8 RID: 10232
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftBorder : ThemeableLineStyleType
	{
		// Token: 0x17006501 RID: 25857
		// (get) Token: 0x06013FE3 RID: 81891 RVA: 0x002BF360 File Offset: 0x002BD560
		public override string LocalName
		{
			get
			{
				return "left";
			}
		}

		// Token: 0x17006502 RID: 25858
		// (get) Token: 0x06013FE4 RID: 81892 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006503 RID: 25859
		// (get) Token: 0x06013FE5 RID: 81893 RVA: 0x0030E3A1 File Offset: 0x0030C5A1
		internal override int ElementTypeId
		{
			get
			{
				return 10268;
			}
		}

		// Token: 0x06013FE6 RID: 81894 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013FE7 RID: 81895 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public LeftBorder()
		{
		}

		// Token: 0x06013FE8 RID: 81896 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public LeftBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FE9 RID: 81897 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public LeftBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FEA RID: 81898 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public LeftBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FEB RID: 81899 RVA: 0x0030E3CB File Offset: 0x0030C5CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftBorder>(deep);
		}

		// Token: 0x0400888C RID: 34956
		private const string tagName = "left";

		// Token: 0x0400888D RID: 34957
		private const byte tagNsId = 10;

		// Token: 0x0400888E RID: 34958
		internal const int ElementTypeIdConst = 10268;
	}
}
