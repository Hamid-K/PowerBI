using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD0 RID: 11728
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintTwoOnOne : OnOffType
	{
		// Token: 0x1700880C RID: 34828
		// (get) Token: 0x06018ED8 RID: 102104 RVA: 0x003452BD File Offset: 0x003434BD
		public override string LocalName
		{
			get
			{
				return "printTwoOnOne";
			}
		}

		// Token: 0x1700880D RID: 34829
		// (get) Token: 0x06018ED9 RID: 102105 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700880E RID: 34830
		// (get) Token: 0x06018EDA RID: 102106 RVA: 0x003452C4 File Offset: 0x003434C4
		internal override int ElementTypeId
		{
			get
			{
				return 12019;
			}
		}

		// Token: 0x06018EDB RID: 102107 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EDD RID: 102109 RVA: 0x003452CB File Offset: 0x003434CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintTwoOnOne>(deep);
		}

		// Token: 0x0400A5DB RID: 42459
		private const string tagName = "printTwoOnOne";

		// Token: 0x0400A5DC RID: 42460
		private const byte tagNsId = 23;

		// Token: 0x0400A5DD RID: 42461
		internal const int ElementTypeIdConst = 12019;
	}
}
