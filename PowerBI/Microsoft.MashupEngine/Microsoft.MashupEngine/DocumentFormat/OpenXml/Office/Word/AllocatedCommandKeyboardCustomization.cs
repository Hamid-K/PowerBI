using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002464 RID: 9316
	[GeneratedCode("DomGen", "2.0")]
	internal class AllocatedCommandKeyboardCustomization : AcceleratorKeymapType
	{
		// Token: 0x170050C0 RID: 20672
		// (get) Token: 0x060112A7 RID: 70311 RVA: 0x002EB364 File Offset: 0x002E9564
		public override string LocalName
		{
			get
			{
				return "acd";
			}
		}

		// Token: 0x170050C1 RID: 20673
		// (get) Token: 0x060112A8 RID: 70312 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050C2 RID: 20674
		// (get) Token: 0x060112A9 RID: 70313 RVA: 0x002EB36B File Offset: 0x002E956B
		internal override int ElementTypeId
		{
			get
			{
				return 12543;
			}
		}

		// Token: 0x060112AA RID: 70314 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060112AC RID: 70316 RVA: 0x002EB37A File Offset: 0x002E957A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllocatedCommandKeyboardCustomization>(deep);
		}

		// Token: 0x04007876 RID: 30838
		private const string tagName = "acd";

		// Token: 0x04007877 RID: 30839
		private const byte tagNsId = 33;

		// Token: 0x04007878 RID: 30840
		internal const int ElementTypeIdConst = 12543;
	}
}
