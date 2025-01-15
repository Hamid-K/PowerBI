using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002488 RID: 9352
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomXmlConflictInsertionRangeStart : TrackChangeType
	{
		// Token: 0x1700516A RID: 20842
		// (get) Token: 0x0601143D RID: 70717 RVA: 0x002EC8E4 File Offset: 0x002EAAE4
		public override string LocalName
		{
			get
			{
				return "customXmlConflictInsRangeStart";
			}
		}

		// Token: 0x1700516B RID: 20843
		// (get) Token: 0x0601143E RID: 70718 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700516C RID: 20844
		// (get) Token: 0x0601143F RID: 70719 RVA: 0x002EC8EB File Offset: 0x002EAAEB
		internal override int ElementTypeId
		{
			get
			{
				return 12868;
			}
		}

		// Token: 0x06011440 RID: 70720 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011442 RID: 70722 RVA: 0x002EC8F2 File Offset: 0x002EAAF2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlConflictInsertionRangeStart>(deep);
		}

		// Token: 0x040078F1 RID: 30961
		private const string tagName = "customXmlConflictInsRangeStart";

		// Token: 0x040078F2 RID: 30962
		private const byte tagNsId = 52;

		// Token: 0x040078F3 RID: 30963
		internal const int ElementTypeIdConst = 12868;
	}
}
