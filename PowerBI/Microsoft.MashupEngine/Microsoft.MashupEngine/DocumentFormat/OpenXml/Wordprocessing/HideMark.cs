using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EEC RID: 12012
	[GeneratedCode("DomGen", "2.0")]
	internal class HideMark : OnOffOnlyType
	{
		// Token: 0x17008D78 RID: 36216
		// (get) Token: 0x06019A40 RID: 105024 RVA: 0x00353896 File Offset: 0x00351A96
		public override string LocalName
		{
			get
			{
				return "hideMark";
			}
		}

		// Token: 0x17008D79 RID: 36217
		// (get) Token: 0x06019A41 RID: 105025 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D7A RID: 36218
		// (get) Token: 0x06019A42 RID: 105026 RVA: 0x0035389D File Offset: 0x00351A9D
		internal override int ElementTypeId
		{
			get
			{
				return 11659;
			}
		}

		// Token: 0x06019A43 RID: 105027 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A45 RID: 105029 RVA: 0x003538A4 File Offset: 0x00351AA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideMark>(deep);
		}

		// Token: 0x0400A9D1 RID: 43473
		private const string tagName = "hideMark";

		// Token: 0x0400A9D2 RID: 43474
		private const byte tagNsId = 23;

		// Token: 0x0400A9D3 RID: 43475
		internal const int ElementTypeIdConst = 11659;
	}
}
