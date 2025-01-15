using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FF RID: 10239
	[GeneratedCode("DomGen", "2.0")]
	internal class TopRightToBottomLeftBorder : ThemeableLineStyleType
	{
		// Token: 0x17006516 RID: 25878
		// (get) Token: 0x06014022 RID: 81954 RVA: 0x0030E449 File Offset: 0x0030C649
		public override string LocalName
		{
			get
			{
				return "tr2bl";
			}
		}

		// Token: 0x17006517 RID: 25879
		// (get) Token: 0x06014023 RID: 81955 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006518 RID: 25880
		// (get) Token: 0x06014024 RID: 81956 RVA: 0x0030E450 File Offset: 0x0030C650
		internal override int ElementTypeId
		{
			get
			{
				return 10275;
			}
		}

		// Token: 0x06014025 RID: 81957 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014026 RID: 81958 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public TopRightToBottomLeftBorder()
		{
		}

		// Token: 0x06014027 RID: 81959 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public TopRightToBottomLeftBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014028 RID: 81960 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public TopRightToBottomLeftBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014029 RID: 81961 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public TopRightToBottomLeftBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601402A RID: 81962 RVA: 0x0030E457 File Offset: 0x0030C657
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopRightToBottomLeftBorder>(deep);
		}

		// Token: 0x040088A1 RID: 34977
		private const string tagName = "tr2bl";

		// Token: 0x040088A2 RID: 34978
		private const byte tagNsId = 10;

		// Token: 0x040088A3 RID: 34979
		internal const int ElementTypeIdConst = 10275;
	}
}
