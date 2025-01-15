using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B6 RID: 9142
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CreationId : RandomIdType
	{
		// Token: 0x17004C79 RID: 19577
		// (get) Token: 0x06010901 RID: 67841 RVA: 0x002E4BFF File Offset: 0x002E2DFF
		public override string LocalName
		{
			get
			{
				return "creationId";
			}
		}

		// Token: 0x17004C7A RID: 19578
		// (get) Token: 0x06010902 RID: 67842 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C7B RID: 19579
		// (get) Token: 0x06010903 RID: 67843 RVA: 0x002E4C06 File Offset: 0x002E2E06
		internal override int ElementTypeId
		{
			get
			{
				return 12796;
			}
		}

		// Token: 0x06010904 RID: 67844 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010906 RID: 67846 RVA: 0x002E4C15 File Offset: 0x002E2E15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CreationId>(deep);
		}

		// Token: 0x04007545 RID: 30021
		private const string tagName = "creationId";

		// Token: 0x04007546 RID: 30022
		private const byte tagNsId = 49;

		// Token: 0x04007547 RID: 30023
		internal const int ElementTypeIdConst = 12796;
	}
}
