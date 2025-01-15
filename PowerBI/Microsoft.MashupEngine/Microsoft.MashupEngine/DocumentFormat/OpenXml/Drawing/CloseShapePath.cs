using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CB RID: 10187
	[GeneratedCode("DomGen", "2.0")]
	internal class CloseShapePath : OpenXmlLeafElement
	{
		// Token: 0x170063B1 RID: 25521
		// (get) Token: 0x06013CD8 RID: 81112 RVA: 0x0030BDE2 File Offset: 0x00309FE2
		public override string LocalName
		{
			get
			{
				return "close";
			}
		}

		// Token: 0x170063B2 RID: 25522
		// (get) Token: 0x06013CD9 RID: 81113 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063B3 RID: 25523
		// (get) Token: 0x06013CDA RID: 81114 RVA: 0x0030BDE9 File Offset: 0x00309FE9
		internal override int ElementTypeId
		{
			get
			{
				return 10221;
			}
		}

		// Token: 0x06013CDB RID: 81115 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013CDD RID: 81117 RVA: 0x0030BDF0 File Offset: 0x00309FF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CloseShapePath>(deep);
		}

		// Token: 0x040087D9 RID: 34777
		private const string tagName = "close";

		// Token: 0x040087DA RID: 34778
		private const byte tagNsId = 10;

		// Token: 0x040087DB RID: 34779
		internal const int ElementTypeIdConst = 10221;
	}
}
