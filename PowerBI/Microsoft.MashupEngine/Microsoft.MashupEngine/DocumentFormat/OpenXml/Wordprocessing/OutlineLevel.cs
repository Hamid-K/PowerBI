using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E30 RID: 11824
	[GeneratedCode("DomGen", "2.0")]
	internal class OutlineLevel : DecimalNumberType
	{
		// Token: 0x1700897C RID: 35196
		// (get) Token: 0x060191C1 RID: 102849 RVA: 0x003467E8 File Offset: 0x003449E8
		public override string LocalName
		{
			get
			{
				return "outlineLvl";
			}
		}

		// Token: 0x1700897D RID: 35197
		// (get) Token: 0x060191C2 RID: 102850 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700897E RID: 35198
		// (get) Token: 0x060191C3 RID: 102851 RVA: 0x003467EF File Offset: 0x003449EF
		internal override int ElementTypeId
		{
			get
			{
				return 11522;
			}
		}

		// Token: 0x060191C4 RID: 102852 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191C6 RID: 102854 RVA: 0x003467FE File Offset: 0x003449FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OutlineLevel>(deep);
		}

		// Token: 0x0400A70E RID: 42766
		private const string tagName = "outlineLvl";

		// Token: 0x0400A70F RID: 42767
		private const byte tagNsId = 23;

		// Token: 0x0400A710 RID: 42768
		internal const int ElementTypeIdConst = 11522;
	}
}
