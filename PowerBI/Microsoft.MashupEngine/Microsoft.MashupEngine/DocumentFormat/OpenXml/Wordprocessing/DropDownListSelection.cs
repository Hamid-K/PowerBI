using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E34 RID: 11828
	[GeneratedCode("DomGen", "2.0")]
	internal class DropDownListSelection : DecimalNumberType
	{
		// Token: 0x17008988 RID: 35208
		// (get) Token: 0x060191D9 RID: 102873 RVA: 0x0034684C File Offset: 0x00344A4C
		public override string LocalName
		{
			get
			{
				return "result";
			}
		}

		// Token: 0x17008989 RID: 35209
		// (get) Token: 0x060191DA RID: 102874 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700898A RID: 35210
		// (get) Token: 0x060191DB RID: 102875 RVA: 0x00346853 File Offset: 0x00344A53
		internal override int ElementTypeId
		{
			get
			{
				return 11740;
			}
		}

		// Token: 0x060191DC RID: 102876 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191DE RID: 102878 RVA: 0x0034685A File Offset: 0x00344A5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropDownListSelection>(deep);
		}

		// Token: 0x0400A71A RID: 42778
		private const string tagName = "result";

		// Token: 0x0400A71B RID: 42779
		private const byte tagNsId = 23;

		// Token: 0x0400A71C RID: 42780
		internal const int ElementTypeIdConst = 11740;
	}
}
