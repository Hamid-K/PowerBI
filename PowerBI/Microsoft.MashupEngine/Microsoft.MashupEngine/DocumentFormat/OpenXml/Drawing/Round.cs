using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002729 RID: 10025
	[GeneratedCode("DomGen", "2.0")]
	internal class Round : OpenXmlLeafElement
	{
		// Token: 0x17005FE3 RID: 24547
		// (get) Token: 0x0601343B RID: 78907 RVA: 0x002ECFB6 File Offset: 0x002EB1B6
		public override string LocalName
		{
			get
			{
				return "round";
			}
		}

		// Token: 0x17005FE4 RID: 24548
		// (get) Token: 0x0601343C RID: 78908 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FE5 RID: 24549
		// (get) Token: 0x0601343D RID: 78909 RVA: 0x00305946 File Offset: 0x00303B46
		internal override int ElementTypeId
		{
			get
			{
				return 10088;
			}
		}

		// Token: 0x0601343E RID: 78910 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013440 RID: 78912 RVA: 0x0030594D File Offset: 0x00303B4D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Round>(deep);
		}

		// Token: 0x0400855C RID: 34140
		private const string tagName = "round";

		// Token: 0x0400855D RID: 34141
		private const byte tagNsId = 10;

		// Token: 0x0400855E RID: 34142
		internal const int ElementTypeIdConst = 10088;
	}
}
