using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B2 RID: 9394
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Checked : OnOffType
	{
		// Token: 0x17005254 RID: 21076
		// (get) Token: 0x06011631 RID: 71217 RVA: 0x002EDF95 File Offset: 0x002EC195
		public override string LocalName
		{
			get
			{
				return "checked";
			}
		}

		// Token: 0x17005255 RID: 21077
		// (get) Token: 0x06011632 RID: 71218 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005256 RID: 21078
		// (get) Token: 0x06011633 RID: 71219 RVA: 0x002EDF9C File Offset: 0x002EC19C
		internal override int ElementTypeId
		{
			get
			{
				return 12894;
			}
		}

		// Token: 0x06011634 RID: 71220 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011636 RID: 71222 RVA: 0x002EDFA3 File Offset: 0x002EC1A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Checked>(deep);
		}

		// Token: 0x04007997 RID: 31127
		private const string tagName = "checked";

		// Token: 0x04007998 RID: 31128
		private const byte tagNsId = 52;

		// Token: 0x04007999 RID: 31129
		internal const int ElementTypeIdConst = 12894;
	}
}
