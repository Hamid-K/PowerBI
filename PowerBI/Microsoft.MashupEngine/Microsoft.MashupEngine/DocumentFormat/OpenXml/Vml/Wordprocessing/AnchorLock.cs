using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x0200223C RID: 8764
	[GeneratedCode("DomGen", "2.0")]
	internal class AnchorLock : OpenXmlLeafElement
	{
		// Token: 0x17003978 RID: 14712
		// (get) Token: 0x0600E0A0 RID: 57504 RVA: 0x002BFF9F File Offset: 0x002BE19F
		public override string LocalName
		{
			get
			{
				return "anchorlock";
			}
		}

		// Token: 0x17003979 RID: 14713
		// (get) Token: 0x0600E0A1 RID: 57505 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x1700397A RID: 14714
		// (get) Token: 0x0600E0A2 RID: 57506 RVA: 0x002BFFA6 File Offset: 0x002BE1A6
		internal override int ElementTypeId
		{
			get
			{
				return 12435;
			}
		}

		// Token: 0x0600E0A3 RID: 57507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E0A5 RID: 57509 RVA: 0x002BFFAD File Offset: 0x002BE1AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnchorLock>(deep);
		}

		// Token: 0x04006E5E RID: 28254
		private const string tagName = "anchorlock";

		// Token: 0x04006E5F RID: 28255
		private const byte tagNsId = 28;

		// Token: 0x04006E60 RID: 28256
		internal const int ElementTypeIdConst = 12435;
	}
}
