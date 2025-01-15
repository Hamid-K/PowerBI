using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002483 RID: 9347
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RunConflictInsertion : RunTrackChangeType
	{
		// Token: 0x17005159 RID: 20825
		// (get) Token: 0x06011414 RID: 70676 RVA: 0x002EC7B0 File Offset: 0x002EA9B0
		public override string LocalName
		{
			get
			{
				return "conflictIns";
			}
		}

		// Token: 0x1700515A RID: 20826
		// (get) Token: 0x06011415 RID: 70677 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700515B RID: 20827
		// (get) Token: 0x06011416 RID: 70678 RVA: 0x002EC7BB File Offset: 0x002EA9BB
		internal override int ElementTypeId
		{
			get
			{
				return 12828;
			}
		}

		// Token: 0x06011417 RID: 70679 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011418 RID: 70680 RVA: 0x002EC7C2 File Offset: 0x002EA9C2
		public RunConflictInsertion()
		{
		}

		// Token: 0x06011419 RID: 70681 RVA: 0x002EC7CA File Offset: 0x002EA9CA
		public RunConflictInsertion(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601141A RID: 70682 RVA: 0x002EC7D3 File Offset: 0x002EA9D3
		public RunConflictInsertion(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601141B RID: 70683 RVA: 0x002EC7DC File Offset: 0x002EA9DC
		public RunConflictInsertion(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601141C RID: 70684 RVA: 0x002EC7E5 File Offset: 0x002EA9E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunConflictInsertion>(deep);
		}

		// Token: 0x040078E3 RID: 30947
		private const string tagName = "conflictIns";

		// Token: 0x040078E4 RID: 30948
		private const byte tagNsId = 52;

		// Token: 0x040078E5 RID: 30949
		internal const int ElementTypeIdConst = 12828;
	}
}
