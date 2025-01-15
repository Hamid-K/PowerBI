using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C1 RID: 10177
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceRectangle : RelativeRectangleType
	{
		// Token: 0x17006363 RID: 25443
		// (get) Token: 0x06013C33 RID: 80947 RVA: 0x0030B785 File Offset: 0x00309985
		public override string LocalName
		{
			get
			{
				return "srcRect";
			}
		}

		// Token: 0x17006364 RID: 25444
		// (get) Token: 0x06013C34 RID: 80948 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006365 RID: 25445
		// (get) Token: 0x06013C35 RID: 80949 RVA: 0x0030B78C File Offset: 0x0030998C
		internal override int ElementTypeId
		{
			get
			{
				return 10212;
			}
		}

		// Token: 0x06013C36 RID: 80950 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C38 RID: 80952 RVA: 0x0030B793 File Offset: 0x00309993
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceRectangle>(deep);
		}

		// Token: 0x040087AA RID: 34730
		private const string tagName = "srcRect";

		// Token: 0x040087AB RID: 34731
		private const byte tagNsId = 10;

		// Token: 0x040087AC RID: 34732
		internal const int ElementTypeIdConst = 10212;
	}
}
