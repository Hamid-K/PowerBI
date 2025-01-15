using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002484 RID: 9348
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class RunConflictDeletion : RunTrackChangeType
	{
		// Token: 0x1700515C RID: 20828
		// (get) Token: 0x0601141D RID: 70685 RVA: 0x002EC7EE File Offset: 0x002EA9EE
		public override string LocalName
		{
			get
			{
				return "conflictDel";
			}
		}

		// Token: 0x1700515D RID: 20829
		// (get) Token: 0x0601141E RID: 70686 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700515E RID: 20830
		// (get) Token: 0x0601141F RID: 70687 RVA: 0x002EC7F5 File Offset: 0x002EA9F5
		internal override int ElementTypeId
		{
			get
			{
				return 12829;
			}
		}

		// Token: 0x06011420 RID: 70688 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011421 RID: 70689 RVA: 0x002EC7C2 File Offset: 0x002EA9C2
		public RunConflictDeletion()
		{
		}

		// Token: 0x06011422 RID: 70690 RVA: 0x002EC7CA File Offset: 0x002EA9CA
		public RunConflictDeletion(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011423 RID: 70691 RVA: 0x002EC7D3 File Offset: 0x002EA9D3
		public RunConflictDeletion(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011424 RID: 70692 RVA: 0x002EC7DC File Offset: 0x002EA9DC
		public RunConflictDeletion(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011425 RID: 70693 RVA: 0x002EC7FC File Offset: 0x002EA9FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunConflictDeletion>(deep);
		}

		// Token: 0x040078E6 RID: 30950
		private const string tagName = "conflictDel";

		// Token: 0x040078E7 RID: 30951
		private const byte tagNsId = 52;

		// Token: 0x040078E8 RID: 30952
		internal const int ElementTypeIdConst = 12829;
	}
}
