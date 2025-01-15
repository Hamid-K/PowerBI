using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002529 RID: 9513
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartObject : BooleanType
	{
		// Token: 0x17005494 RID: 21652
		// (get) Token: 0x06011B23 RID: 72483 RVA: 0x002F14D8 File Offset: 0x002EF6D8
		public override string LocalName
		{
			get
			{
				return "chartObject";
			}
		}

		// Token: 0x17005495 RID: 21653
		// (get) Token: 0x06011B24 RID: 72484 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005496 RID: 21654
		// (get) Token: 0x06011B25 RID: 72485 RVA: 0x002F14DF File Offset: 0x002EF6DF
		internal override int ElementTypeId
		{
			get
			{
				return 10505;
			}
		}

		// Token: 0x06011B26 RID: 72486 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B28 RID: 72488 RVA: 0x002F14E6 File Offset: 0x002EF6E6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartObject>(deep);
		}

		// Token: 0x04007C0F RID: 31759
		private const string tagName = "chartObject";

		// Token: 0x04007C10 RID: 31760
		private const byte tagNsId = 11;

		// Token: 0x04007C11 RID: 31761
		internal const int ElementTypeIdConst = 10505;
	}
}
