using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BB RID: 10171
	[GeneratedCode("DomGen", "2.0")]
	internal class BevelBottom : BevelType
	{
		// Token: 0x1700634E RID: 25422
		// (get) Token: 0x06013C08 RID: 80904 RVA: 0x002EED67 File Offset: 0x002ECF67
		public override string LocalName
		{
			get
			{
				return "bevelB";
			}
		}

		// Token: 0x1700634F RID: 25423
		// (get) Token: 0x06013C09 RID: 80905 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006350 RID: 25424
		// (get) Token: 0x06013C0A RID: 80906 RVA: 0x0030B657 File Offset: 0x00309857
		internal override int ElementTypeId
		{
			get
			{
				return 10202;
			}
		}

		// Token: 0x06013C0B RID: 80907 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C0D RID: 80909 RVA: 0x0030B65E File Offset: 0x0030985E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BevelBottom>(deep);
		}

		// Token: 0x04008799 RID: 34713
		private const string tagName = "bevelB";

		// Token: 0x0400879A RID: 34714
		private const byte tagNsId = 10;

		// Token: 0x0400879B RID: 34715
		internal const int ElementTypeIdConst = 10202;
	}
}
