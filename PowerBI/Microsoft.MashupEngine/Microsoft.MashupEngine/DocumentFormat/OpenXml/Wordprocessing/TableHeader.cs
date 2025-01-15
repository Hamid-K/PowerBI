using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EEE RID: 12014
	[GeneratedCode("DomGen", "2.0")]
	internal class TableHeader : OnOffOnlyType
	{
		// Token: 0x17008D7E RID: 36222
		// (get) Token: 0x06019A4C RID: 105036 RVA: 0x003538C4 File Offset: 0x00351AC4
		public override string LocalName
		{
			get
			{
				return "tblHeader";
			}
		}

		// Token: 0x17008D7F RID: 36223
		// (get) Token: 0x06019A4D RID: 105037 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D80 RID: 36224
		// (get) Token: 0x06019A4E RID: 105038 RVA: 0x003538CB File Offset: 0x00351ACB
		internal override int ElementTypeId
		{
			get
			{
				return 11668;
			}
		}

		// Token: 0x06019A4F RID: 105039 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A51 RID: 105041 RVA: 0x003538D2 File Offset: 0x00351AD2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableHeader>(deep);
		}

		// Token: 0x0400A9D7 RID: 43479
		private const string tagName = "tblHeader";

		// Token: 0x0400A9D8 RID: 43480
		private const byte tagNsId = 23;

		// Token: 0x0400A9D9 RID: 43481
		internal const int ElementTypeIdConst = 11668;
	}
}
