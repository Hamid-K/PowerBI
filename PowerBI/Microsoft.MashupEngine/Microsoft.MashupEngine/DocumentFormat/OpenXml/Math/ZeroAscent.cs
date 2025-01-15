using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297F RID: 10623
	[GeneratedCode("DomGen", "2.0")]
	internal class ZeroAscent : OnOffType
	{
		// Token: 0x17006C88 RID: 27784
		// (get) Token: 0x0601519E RID: 86430 RVA: 0x0031B5CE File Offset: 0x003197CE
		public override string LocalName
		{
			get
			{
				return "zeroAsc";
			}
		}

		// Token: 0x17006C89 RID: 27785
		// (get) Token: 0x0601519F RID: 86431 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C8A RID: 27786
		// (get) Token: 0x060151A0 RID: 86432 RVA: 0x0031B5D5 File Offset: 0x003197D5
		internal override int ElementTypeId
		{
			get
			{
				return 10931;
			}
		}

		// Token: 0x060151A1 RID: 86433 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151A3 RID: 86435 RVA: 0x0031B5DC File Offset: 0x003197DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ZeroAscent>(deep);
		}

		// Token: 0x04009189 RID: 37257
		private const string tagName = "zeroAsc";

		// Token: 0x0400918A RID: 37258
		private const byte tagNsId = 21;

		// Token: 0x0400918B RID: 37259
		internal const int ElementTypeIdConst = 10931;
	}
}
