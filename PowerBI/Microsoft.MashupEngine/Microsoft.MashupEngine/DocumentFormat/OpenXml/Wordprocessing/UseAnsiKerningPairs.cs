using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E20 RID: 11808
	[GeneratedCode("DomGen", "2.0")]
	internal class UseAnsiKerningPairs : OnOffType
	{
		// Token: 0x170088FC RID: 35068
		// (get) Token: 0x060190B8 RID: 102584 RVA: 0x003459ED File Offset: 0x00343BED
		public override string LocalName
		{
			get
			{
				return "useAnsiKerningPairs";
			}
		}

		// Token: 0x170088FD RID: 35069
		// (get) Token: 0x060190B9 RID: 102585 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088FE RID: 35070
		// (get) Token: 0x060190BA RID: 102586 RVA: 0x003459F4 File Offset: 0x00343BF4
		internal override int ElementTypeId
		{
			get
			{
				return 12118;
			}
		}

		// Token: 0x060190BB RID: 102587 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190BD RID: 102589 RVA: 0x003459FB File Offset: 0x00343BFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseAnsiKerningPairs>(deep);
		}

		// Token: 0x0400A6CB RID: 42699
		private const string tagName = "useAnsiKerningPairs";

		// Token: 0x0400A6CC RID: 42700
		private const byte tagNsId = 23;

		// Token: 0x0400A6CD RID: 42701
		internal const int ElementTypeIdConst = 12118;
	}
}
