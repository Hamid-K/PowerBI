using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EDD RID: 11997
	[GeneratedCode("DomGen", "2.0")]
	internal class WidthAfterTableRow : TableWidthType
	{
		// Token: 0x17008D38 RID: 36152
		// (get) Token: 0x060199BD RID: 104893 RVA: 0x0035341B File Offset: 0x0035161B
		public override string LocalName
		{
			get
			{
				return "wAfter";
			}
		}

		// Token: 0x17008D39 RID: 36153
		// (get) Token: 0x060199BE RID: 104894 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D3A RID: 36154
		// (get) Token: 0x060199BF RID: 104895 RVA: 0x00353422 File Offset: 0x00351622
		internal override int ElementTypeId
		{
			get
			{
				return 11664;
			}
		}

		// Token: 0x060199C0 RID: 104896 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060199C2 RID: 104898 RVA: 0x00353429 File Offset: 0x00351629
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WidthAfterTableRow>(deep);
		}

		// Token: 0x0400A99F RID: 43423
		private const string tagName = "wAfter";

		// Token: 0x0400A9A0 RID: 43424
		private const byte tagNsId = 23;

		// Token: 0x0400A9A1 RID: 43425
		internal const int ElementTypeIdConst = 11664;
	}
}
