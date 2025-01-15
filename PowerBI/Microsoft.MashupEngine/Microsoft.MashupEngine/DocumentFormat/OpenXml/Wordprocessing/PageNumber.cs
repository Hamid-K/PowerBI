using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6A RID: 11882
	[GeneratedCode("DomGen", "2.0")]
	internal class PageNumber : EmptyType
	{
		// Token: 0x17008A7F RID: 35455
		// (get) Token: 0x060193DF RID: 103391 RVA: 0x00347B38 File Offset: 0x00345D38
		public override string LocalName
		{
			get
			{
				return "pgNum";
			}
		}

		// Token: 0x17008A80 RID: 35456
		// (get) Token: 0x060193E0 RID: 103392 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A81 RID: 35457
		// (get) Token: 0x060193E1 RID: 103393 RVA: 0x00347B3F File Offset: 0x00345D3F
		internal override int ElementTypeId
		{
			get
			{
				return 11562;
			}
		}

		// Token: 0x060193E2 RID: 103394 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193E4 RID: 103396 RVA: 0x00347B46 File Offset: 0x00345D46
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageNumber>(deep);
		}

		// Token: 0x0400A7D6 RID: 42966
		private const string tagName = "pgNum";

		// Token: 0x0400A7D7 RID: 42967
		private const byte tagNsId = 23;

		// Token: 0x0400A7D8 RID: 42968
		internal const int ElementTypeIdConst = 11562;
	}
}
