using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002489 RID: 9353
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomXmlConflictDeletionRangeStart : TrackChangeType
	{
		// Token: 0x1700516D RID: 20845
		// (get) Token: 0x06011443 RID: 70723 RVA: 0x002EC8FB File Offset: 0x002EAAFB
		public override string LocalName
		{
			get
			{
				return "customXmlConflictDelRangeStart";
			}
		}

		// Token: 0x1700516E RID: 20846
		// (get) Token: 0x06011444 RID: 70724 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700516F RID: 20847
		// (get) Token: 0x06011445 RID: 70725 RVA: 0x002EC902 File Offset: 0x002EAB02
		internal override int ElementTypeId
		{
			get
			{
				return 12870;
			}
		}

		// Token: 0x06011446 RID: 70726 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011448 RID: 70728 RVA: 0x002EC909 File Offset: 0x002EAB09
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlConflictDeletionRangeStart>(deep);
		}

		// Token: 0x040078F4 RID: 30964
		private const string tagName = "customXmlConflictDelRangeStart";

		// Token: 0x040078F5 RID: 30965
		private const byte tagNsId = 52;

		// Token: 0x040078F6 RID: 30966
		internal const int ElementTypeIdConst = 12870;
	}
}
