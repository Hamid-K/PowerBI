using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002526 RID: 9510
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoTitleDeleted : BooleanType
	{
		// Token: 0x1700548B RID: 21643
		// (get) Token: 0x06011B11 RID: 72465 RVA: 0x002F1493 File Offset: 0x002EF693
		public override string LocalName
		{
			get
			{
				return "autoTitleDeleted";
			}
		}

		// Token: 0x1700548C RID: 21644
		// (get) Token: 0x06011B12 RID: 72466 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700548D RID: 21645
		// (get) Token: 0x06011B13 RID: 72467 RVA: 0x002F149A File Offset: 0x002EF69A
		internal override int ElementTypeId
		{
			get
			{
				return 10494;
			}
		}

		// Token: 0x06011B14 RID: 72468 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B16 RID: 72470 RVA: 0x002F14A1 File Offset: 0x002EF6A1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoTitleDeleted>(deep);
		}

		// Token: 0x04007C06 RID: 31750
		private const string tagName = "autoTitleDeleted";

		// Token: 0x04007C07 RID: 31751
		private const byte tagNsId = 11;

		// Token: 0x04007C08 RID: 31752
		internal const int ElementTypeIdConst = 10494;
	}
}
