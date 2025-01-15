using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DB1 RID: 11697
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintPostScriptOverText : OnOffType
	{
		// Token: 0x170087AF RID: 34735
		// (get) Token: 0x06018E1E RID: 101918 RVA: 0x00344FF4 File Offset: 0x003431F4
		public override string LocalName
		{
			get
			{
				return "printPostScriptOverText";
			}
		}

		// Token: 0x170087B0 RID: 34736
		// (get) Token: 0x06018E1F RID: 101919 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087B1 RID: 34737
		// (get) Token: 0x06018E20 RID: 101920 RVA: 0x00344FFB File Offset: 0x003431FB
		internal override int ElementTypeId
		{
			get
			{
				return 11965;
			}
		}

		// Token: 0x06018E21 RID: 101921 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E23 RID: 101923 RVA: 0x00345002 File Offset: 0x00343202
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintPostScriptOverText>(deep);
		}

		// Token: 0x0400A57E RID: 42366
		private const string tagName = "printPostScriptOverText";

		// Token: 0x0400A57F RID: 42367
		private const byte tagNsId = 23;

		// Token: 0x0400A580 RID: 42368
		internal const int ElementTypeIdConst = 11965;
	}
}
