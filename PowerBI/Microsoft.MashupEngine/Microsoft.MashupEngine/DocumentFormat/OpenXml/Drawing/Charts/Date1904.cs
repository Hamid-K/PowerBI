using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002530 RID: 9520
	[GeneratedCode("DomGen", "2.0")]
	internal class Date1904 : BooleanType
	{
		// Token: 0x170054A9 RID: 21673
		// (get) Token: 0x06011B4D RID: 72525 RVA: 0x002F156B File Offset: 0x002EF76B
		public override string LocalName
		{
			get
			{
				return "date1904";
			}
		}

		// Token: 0x170054AA RID: 21674
		// (get) Token: 0x06011B4E RID: 72526 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054AB RID: 21675
		// (get) Token: 0x06011B4F RID: 72527 RVA: 0x002F1572 File Offset: 0x002EF772
		internal override int ElementTypeId
		{
			get
			{
				return 10571;
			}
		}

		// Token: 0x06011B50 RID: 72528 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B52 RID: 72530 RVA: 0x002F1579 File Offset: 0x002EF779
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Date1904>(deep);
		}

		// Token: 0x04007C24 RID: 31780
		private const string tagName = "date1904";

		// Token: 0x04007C25 RID: 31781
		private const byte tagNsId = 11;

		// Token: 0x04007C26 RID: 31782
		internal const int ElementTypeIdConst = 10571;
	}
}
