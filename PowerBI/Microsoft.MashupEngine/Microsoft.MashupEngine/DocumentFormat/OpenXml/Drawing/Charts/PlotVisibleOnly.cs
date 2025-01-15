using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002527 RID: 9511
	[GeneratedCode("DomGen", "2.0")]
	internal class PlotVisibleOnly : BooleanType
	{
		// Token: 0x1700548E RID: 21646
		// (get) Token: 0x06011B17 RID: 72471 RVA: 0x002F14AA File Offset: 0x002EF6AA
		public override string LocalName
		{
			get
			{
				return "plotVisOnly";
			}
		}

		// Token: 0x1700548F RID: 21647
		// (get) Token: 0x06011B18 RID: 72472 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005490 RID: 21648
		// (get) Token: 0x06011B19 RID: 72473 RVA: 0x002F14B1 File Offset: 0x002EF6B1
		internal override int ElementTypeId
		{
			get
			{
				return 10502;
			}
		}

		// Token: 0x06011B1A RID: 72474 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B1C RID: 72476 RVA: 0x002F14B8 File Offset: 0x002EF6B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PlotVisibleOnly>(deep);
		}

		// Token: 0x04007C09 RID: 31753
		private const string tagName = "plotVisOnly";

		// Token: 0x04007C0A RID: 31754
		private const byte tagNsId = 11;

		// Token: 0x04007C0B RID: 31755
		internal const int ElementTypeIdConst = 10502;
	}
}
