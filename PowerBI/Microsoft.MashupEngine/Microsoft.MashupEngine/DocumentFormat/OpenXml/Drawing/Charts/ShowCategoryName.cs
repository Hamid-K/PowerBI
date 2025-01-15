using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250C RID: 9484
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowCategoryName : BooleanType
	{
		// Token: 0x1700543D RID: 21565
		// (get) Token: 0x06011A75 RID: 72309 RVA: 0x002F123D File Offset: 0x002EF43D
		public override string LocalName
		{
			get
			{
				return "showCatName";
			}
		}

		// Token: 0x1700543E RID: 21566
		// (get) Token: 0x06011A76 RID: 72310 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700543F RID: 21567
		// (get) Token: 0x06011A77 RID: 72311 RVA: 0x002F1244 File Offset: 0x002EF444
		internal override int ElementTypeId
		{
			get
			{
				return 10348;
			}
		}

		// Token: 0x06011A78 RID: 72312 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A7A RID: 72314 RVA: 0x002F124B File Offset: 0x002EF44B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowCategoryName>(deep);
		}

		// Token: 0x04007BB8 RID: 31672
		private const string tagName = "showCatName";

		// Token: 0x04007BB9 RID: 31673
		private const byte tagNsId = 11;

		// Token: 0x04007BBA RID: 31674
		internal const int ElementTypeIdConst = 10348;
	}
}
