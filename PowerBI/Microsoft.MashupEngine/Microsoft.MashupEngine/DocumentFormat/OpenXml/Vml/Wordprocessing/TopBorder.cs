using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x02002237 RID: 8759
	[GeneratedCode("DomGen", "2.0")]
	internal class TopBorder : BorderType
	{
		// Token: 0x17003963 RID: 14691
		// (get) Token: 0x0600E076 RID: 57462 RVA: 0x002BFE1F File Offset: 0x002BE01F
		public override string LocalName
		{
			get
			{
				return "bordertop";
			}
		}

		// Token: 0x17003964 RID: 14692
		// (get) Token: 0x0600E077 RID: 57463 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x17003965 RID: 14693
		// (get) Token: 0x0600E078 RID: 57464 RVA: 0x002BFE2A File Offset: 0x002BE02A
		internal override int ElementTypeId
		{
			get
			{
				return 12430;
			}
		}

		// Token: 0x0600E079 RID: 57465 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E07B RID: 57467 RVA: 0x002BFE39 File Offset: 0x002BE039
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopBorder>(deep);
		}

		// Token: 0x04006E4D RID: 28237
		private const string tagName = "bordertop";

		// Token: 0x04006E4E RID: 28238
		private const byte tagNsId = 28;

		// Token: 0x04006E4F RID: 28239
		internal const int ElementTypeIdConst = 12430;
	}
}
