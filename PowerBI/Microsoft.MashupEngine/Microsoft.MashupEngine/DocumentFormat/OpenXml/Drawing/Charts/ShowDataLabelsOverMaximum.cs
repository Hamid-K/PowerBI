using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002528 RID: 9512
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowDataLabelsOverMaximum : BooleanType
	{
		// Token: 0x17005491 RID: 21649
		// (get) Token: 0x06011B1D RID: 72477 RVA: 0x002F14C1 File Offset: 0x002EF6C1
		public override string LocalName
		{
			get
			{
				return "showDLblsOverMax";
			}
		}

		// Token: 0x17005492 RID: 21650
		// (get) Token: 0x06011B1E RID: 72478 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005493 RID: 21651
		// (get) Token: 0x06011B1F RID: 72479 RVA: 0x002F14C8 File Offset: 0x002EF6C8
		internal override int ElementTypeId
		{
			get
			{
				return 10504;
			}
		}

		// Token: 0x06011B20 RID: 72480 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B22 RID: 72482 RVA: 0x002F14CF File Offset: 0x002EF6CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowDataLabelsOverMaximum>(deep);
		}

		// Token: 0x04007C0C RID: 31756
		private const string tagName = "showDLblsOverMax";

		// Token: 0x04007C0D RID: 31757
		private const byte tagNsId = 11;

		// Token: 0x04007C0E RID: 31758
		internal const int ElementTypeIdConst = 10504;
	}
}
