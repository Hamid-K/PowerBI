using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024D2 RID: 9426
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class UncheckedState : CheckBoxSymbolType
	{
		// Token: 0x17005311 RID: 21265
		// (get) Token: 0x060117D7 RID: 71639 RVA: 0x002EEF6C File Offset: 0x002ED16C
		public override string LocalName
		{
			get
			{
				return "uncheckedState";
			}
		}

		// Token: 0x17005312 RID: 21266
		// (get) Token: 0x060117D8 RID: 71640 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005313 RID: 21267
		// (get) Token: 0x060117D9 RID: 71641 RVA: 0x002EEF73 File Offset: 0x002ED173
		internal override int ElementTypeId
		{
			get
			{
				return 12896;
			}
		}

		// Token: 0x060117DA RID: 71642 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117DC RID: 71644 RVA: 0x002EEF7A File Offset: 0x002ED17A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UncheckedState>(deep);
		}

		// Token: 0x04007A15 RID: 31253
		private const string tagName = "uncheckedState";

		// Token: 0x04007A16 RID: 31254
		private const byte tagNsId = 52;

		// Token: 0x04007A17 RID: 31255
		internal const int ElementTypeIdConst = 12896;
	}
}
