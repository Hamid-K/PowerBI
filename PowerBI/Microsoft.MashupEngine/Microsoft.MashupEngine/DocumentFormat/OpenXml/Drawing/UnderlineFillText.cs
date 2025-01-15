using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275E RID: 10078
	[GeneratedCode("DomGen", "2.0")]
	internal class UnderlineFillText : OpenXmlLeafElement
	{
		// Token: 0x170060CF RID: 24783
		// (get) Token: 0x0601366E RID: 79470 RVA: 0x00306A2E File Offset: 0x00304C2E
		public override string LocalName
		{
			get
			{
				return "uFillTx";
			}
		}

		// Token: 0x170060D0 RID: 24784
		// (get) Token: 0x0601366F RID: 79471 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060D1 RID: 24785
		// (get) Token: 0x06013670 RID: 79472 RVA: 0x00306A35 File Offset: 0x00304C35
		internal override int ElementTypeId
		{
			get
			{
				return 10115;
			}
		}

		// Token: 0x06013671 RID: 79473 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013673 RID: 79475 RVA: 0x00306A3C File Offset: 0x00304C3C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnderlineFillText>(deep);
		}

		// Token: 0x04008610 RID: 34320
		private const string tagName = "uFillTx";

		// Token: 0x04008611 RID: 34321
		private const byte tagNsId = 10;

		// Token: 0x04008612 RID: 34322
		internal const int ElementTypeIdConst = 10115;
	}
}
