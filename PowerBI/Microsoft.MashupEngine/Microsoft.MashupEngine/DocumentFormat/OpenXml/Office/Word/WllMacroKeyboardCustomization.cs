using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002462 RID: 9314
	[GeneratedCode("DomGen", "2.0")]
	internal class WllMacroKeyboardCustomization : MacroWllType
	{
		// Token: 0x170050BA RID: 20666
		// (get) Token: 0x0601129A RID: 70298 RVA: 0x002EB2E7 File Offset: 0x002E94E7
		public override string LocalName
		{
			get
			{
				return "wll";
			}
		}

		// Token: 0x170050BB RID: 20667
		// (get) Token: 0x0601129B RID: 70299 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050BC RID: 20668
		// (get) Token: 0x0601129C RID: 70300 RVA: 0x002EB2EE File Offset: 0x002E94EE
		internal override int ElementTypeId
		{
			get
			{
				return 12544;
			}
		}

		// Token: 0x0601129D RID: 70301 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601129F RID: 70303 RVA: 0x002EB2F5 File Offset: 0x002E94F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WllMacroKeyboardCustomization>(deep);
		}

		// Token: 0x04007871 RID: 30833
		private const string tagName = "wll";

		// Token: 0x04007872 RID: 30834
		private const byte tagNsId = 33;

		// Token: 0x04007873 RID: 30835
		internal const int ElementTypeIdConst = 12544;
	}
}
