using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D35 RID: 11573
	[GeneratedCode("DomGen", "2.0")]
	internal class CellInsertion : TrackChangeType
	{
		// Token: 0x17008626 RID: 34342
		// (get) Token: 0x06018B07 RID: 101127 RVA: 0x00344088 File Offset: 0x00342288
		public override string LocalName
		{
			get
			{
				return "cellIns";
			}
		}

		// Token: 0x17008627 RID: 34343
		// (get) Token: 0x06018B08 RID: 101128 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008628 RID: 34344
		// (get) Token: 0x06018B09 RID: 101129 RVA: 0x0034408F File Offset: 0x0034228F
		internal override int ElementTypeId
		{
			get
			{
				return 11473;
			}
		}

		// Token: 0x06018B0A RID: 101130 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B0C RID: 101132 RVA: 0x0034409E File Offset: 0x0034229E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellInsertion>(deep);
		}

		// Token: 0x0400A40B RID: 41995
		private const string tagName = "cellIns";

		// Token: 0x0400A40C RID: 41996
		private const byte tagNsId = 23;

		// Token: 0x0400A40D RID: 41997
		internal const int ElementTypeIdConst = 11473;
	}
}
