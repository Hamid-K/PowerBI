using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BC RID: 10172
	[GeneratedCode("DomGen", "2.0")]
	internal class Bevel : BevelType
	{
		// Token: 0x17006351 RID: 25425
		// (get) Token: 0x06013C0E RID: 80910 RVA: 0x002ECFCD File Offset: 0x002EB1CD
		public override string LocalName
		{
			get
			{
				return "bevel";
			}
		}

		// Token: 0x17006352 RID: 25426
		// (get) Token: 0x06013C0F RID: 80911 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006353 RID: 25427
		// (get) Token: 0x06013C10 RID: 80912 RVA: 0x0030B667 File Offset: 0x00309867
		internal override int ElementTypeId
		{
			get
			{
				return 10267;
			}
		}

		// Token: 0x06013C11 RID: 80913 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C13 RID: 80915 RVA: 0x0030B66E File Offset: 0x0030986E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bevel>(deep);
		}

		// Token: 0x0400879C RID: 34716
		private const string tagName = "bevel";

		// Token: 0x0400879D RID: 34717
		private const byte tagNsId = 10;

		// Token: 0x0400879E RID: 34718
		internal const int ElementTypeIdConst = 10267;
	}
}
