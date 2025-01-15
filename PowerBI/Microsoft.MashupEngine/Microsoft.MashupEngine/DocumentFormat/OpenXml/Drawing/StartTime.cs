using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276E RID: 10094
	[GeneratedCode("DomGen", "2.0")]
	internal class StartTime : AudioCDTimeType
	{
		// Token: 0x1700613E RID: 24894
		// (get) Token: 0x06013771 RID: 79729 RVA: 0x003075FB File Offset: 0x003057FB
		public override string LocalName
		{
			get
			{
				return "st";
			}
		}

		// Token: 0x1700613F RID: 24895
		// (get) Token: 0x06013772 RID: 79730 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006140 RID: 24896
		// (get) Token: 0x06013773 RID: 79731 RVA: 0x00307602 File Offset: 0x00305802
		internal override int ElementTypeId
		{
			get
			{
				return 10128;
			}
		}

		// Token: 0x06013774 RID: 79732 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013776 RID: 79734 RVA: 0x00307611 File Offset: 0x00305811
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartTime>(deep);
		}

		// Token: 0x04008654 RID: 34388
		private const string tagName = "st";

		// Token: 0x04008655 RID: 34389
		private const byte tagNsId = 10;

		// Token: 0x04008656 RID: 34390
		internal const int ElementTypeIdConst = 10128;
	}
}
