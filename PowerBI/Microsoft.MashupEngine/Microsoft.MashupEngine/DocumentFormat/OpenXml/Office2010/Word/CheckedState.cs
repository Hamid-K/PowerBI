using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024D1 RID: 9425
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CheckedState : CheckBoxSymbolType
	{
		// Token: 0x1700530E RID: 21262
		// (get) Token: 0x060117D1 RID: 71633 RVA: 0x002EEF4D File Offset: 0x002ED14D
		public override string LocalName
		{
			get
			{
				return "checkedState";
			}
		}

		// Token: 0x1700530F RID: 21263
		// (get) Token: 0x060117D2 RID: 71634 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005310 RID: 21264
		// (get) Token: 0x060117D3 RID: 71635 RVA: 0x002EEF54 File Offset: 0x002ED154
		internal override int ElementTypeId
		{
			get
			{
				return 12895;
			}
		}

		// Token: 0x060117D4 RID: 71636 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117D6 RID: 71638 RVA: 0x002EEF63 File Offset: 0x002ED163
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckedState>(deep);
		}

		// Token: 0x04007A12 RID: 31250
		private const string tagName = "checkedState";

		// Token: 0x04007A13 RID: 31251
		private const byte tagNsId = 52;

		// Token: 0x04007A14 RID: 31252
		internal const int ElementTypeIdConst = 12895;
	}
}
