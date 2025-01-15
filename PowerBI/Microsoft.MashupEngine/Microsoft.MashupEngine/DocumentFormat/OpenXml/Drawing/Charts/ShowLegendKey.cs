using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250A RID: 9482
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowLegendKey : BooleanType
	{
		// Token: 0x17005437 RID: 21559
		// (get) Token: 0x06011A69 RID: 72297 RVA: 0x002F1207 File Offset: 0x002EF407
		public override string LocalName
		{
			get
			{
				return "showLegendKey";
			}
		}

		// Token: 0x17005438 RID: 21560
		// (get) Token: 0x06011A6A RID: 72298 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005439 RID: 21561
		// (get) Token: 0x06011A6B RID: 72299 RVA: 0x002F120E File Offset: 0x002EF40E
		internal override int ElementTypeId
		{
			get
			{
				return 10346;
			}
		}

		// Token: 0x06011A6C RID: 72300 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A6E RID: 72302 RVA: 0x002F121D File Offset: 0x002EF41D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowLegendKey>(deep);
		}

		// Token: 0x04007BB2 RID: 31666
		private const string tagName = "showLegendKey";

		// Token: 0x04007BB3 RID: 31667
		private const byte tagNsId = 11;

		// Token: 0x04007BB4 RID: 31668
		internal const int ElementTypeIdConst = 10346;
	}
}
