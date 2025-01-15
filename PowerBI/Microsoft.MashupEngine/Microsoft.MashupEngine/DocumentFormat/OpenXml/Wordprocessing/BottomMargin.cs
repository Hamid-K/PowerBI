using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE2 RID: 12002
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomMargin : TableWidthType
	{
		// Token: 0x17008D47 RID: 36167
		// (get) Token: 0x060199DB RID: 104923 RVA: 0x002BF3AD File Offset: 0x002BD5AD
		public override string LocalName
		{
			get
			{
				return "bottom";
			}
		}

		// Token: 0x17008D48 RID: 36168
		// (get) Token: 0x060199DC RID: 104924 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D49 RID: 36169
		// (get) Token: 0x060199DD RID: 104925 RVA: 0x00353480 File Offset: 0x00351680
		internal override int ElementTypeId
		{
			get
			{
				return 12128;
			}
		}

		// Token: 0x060199DE RID: 104926 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199E0 RID: 104928 RVA: 0x00353487 File Offset: 0x00351687
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomMargin>(deep);
		}

		// Token: 0x0400A9AE RID: 43438
		private const string tagName = "bottom";

		// Token: 0x0400A9AF RID: 43439
		private const byte tagNsId = 23;

		// Token: 0x0400A9B0 RID: 43440
		internal const int ElementTypeIdConst = 12128;
	}
}
