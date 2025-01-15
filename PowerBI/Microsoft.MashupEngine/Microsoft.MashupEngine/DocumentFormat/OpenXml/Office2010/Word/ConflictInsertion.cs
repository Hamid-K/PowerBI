using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002486 RID: 9350
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConflictInsertion : TrackChangeType
	{
		// Token: 0x17005164 RID: 20836
		// (get) Token: 0x06011431 RID: 70705 RVA: 0x002EC7B0 File Offset: 0x002EA9B0
		public override string LocalName
		{
			get
			{
				return "conflictIns";
			}
		}

		// Token: 0x17005165 RID: 20837
		// (get) Token: 0x06011432 RID: 70706 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005166 RID: 20838
		// (get) Token: 0x06011433 RID: 70707 RVA: 0x002EC8BC File Offset: 0x002EAABC
		internal override int ElementTypeId
		{
			get
			{
				return 12830;
			}
		}

		// Token: 0x06011434 RID: 70708 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011436 RID: 70710 RVA: 0x002EC8CB File Offset: 0x002EAACB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConflictInsertion>(deep);
		}

		// Token: 0x040078EB RID: 30955
		private const string tagName = "conflictIns";

		// Token: 0x040078EC RID: 30956
		private const byte tagNsId = 52;

		// Token: 0x040078ED RID: 30957
		internal const int ElementTypeIdConst = 12830;
	}
}
