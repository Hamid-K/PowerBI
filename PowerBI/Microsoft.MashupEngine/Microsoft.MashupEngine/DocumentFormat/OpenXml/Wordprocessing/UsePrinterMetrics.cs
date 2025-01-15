using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DFC RID: 11772
	[GeneratedCode("DomGen", "2.0")]
	internal class UsePrinterMetrics : OnOffType
	{
		// Token: 0x17008890 RID: 34960
		// (get) Token: 0x06018FE0 RID: 102368 RVA: 0x003456B1 File Offset: 0x003438B1
		public override string LocalName
		{
			get
			{
				return "usePrinterMetrics";
			}
		}

		// Token: 0x17008891 RID: 34961
		// (get) Token: 0x06018FE1 RID: 102369 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008892 RID: 34962
		// (get) Token: 0x06018FE2 RID: 102370 RVA: 0x003456B8 File Offset: 0x003438B8
		internal override int ElementTypeId
		{
			get
			{
				return 12082;
			}
		}

		// Token: 0x06018FE3 RID: 102371 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FE5 RID: 102373 RVA: 0x003456BF File Offset: 0x003438BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UsePrinterMetrics>(deep);
		}

		// Token: 0x0400A65F RID: 42591
		private const string tagName = "usePrinterMetrics";

		// Token: 0x0400A660 RID: 42592
		private const byte tagNsId = 23;

		// Token: 0x0400A661 RID: 42593
		internal const int ElementTypeIdConst = 12082;
	}
}
