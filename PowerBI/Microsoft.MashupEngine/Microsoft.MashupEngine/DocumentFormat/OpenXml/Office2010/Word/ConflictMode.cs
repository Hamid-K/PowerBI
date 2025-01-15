using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B0 RID: 9392
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ConflictMode : OnOffType
	{
		// Token: 0x1700524E RID: 21070
		// (get) Token: 0x06011625 RID: 71205 RVA: 0x002EDF67 File Offset: 0x002EC167
		public override string LocalName
		{
			get
			{
				return "conflictMode";
			}
		}

		// Token: 0x1700524F RID: 21071
		// (get) Token: 0x06011626 RID: 71206 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005250 RID: 21072
		// (get) Token: 0x06011627 RID: 71207 RVA: 0x002EDF6E File Offset: 0x002EC16E
		internal override int ElementTypeId
		{
			get
			{
				return 12867;
			}
		}

		// Token: 0x06011628 RID: 71208 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601162A RID: 71210 RVA: 0x002EDF75 File Offset: 0x002EC175
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConflictMode>(deep);
		}

		// Token: 0x04007991 RID: 31121
		private const string tagName = "conflictMode";

		// Token: 0x04007992 RID: 31122
		private const byte tagNsId = 52;

		// Token: 0x04007993 RID: 31123
		internal const int ElementTypeIdConst = 12867;
	}
}
