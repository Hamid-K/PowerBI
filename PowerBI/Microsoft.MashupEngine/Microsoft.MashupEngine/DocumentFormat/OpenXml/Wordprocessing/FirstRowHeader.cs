using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9E RID: 11678
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstRowHeader : OnOffType
	{
		// Token: 0x17008776 RID: 34678
		// (get) Token: 0x06018DAC RID: 101804 RVA: 0x00344E3F File Offset: 0x0034303F
		public override string LocalName
		{
			get
			{
				return "fHdr";
			}
		}

		// Token: 0x17008777 RID: 34679
		// (get) Token: 0x06018DAD RID: 101805 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008778 RID: 34680
		// (get) Token: 0x06018DAE RID: 101806 RVA: 0x00344E46 File Offset: 0x00343046
		internal override int ElementTypeId
		{
			get
			{
				return 11809;
			}
		}

		// Token: 0x06018DAF RID: 101807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DB1 RID: 101809 RVA: 0x00344E4D File Offset: 0x0034304D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstRowHeader>(deep);
		}

		// Token: 0x0400A545 RID: 42309
		private const string tagName = "fHdr";

		// Token: 0x0400A546 RID: 42310
		private const byte tagNsId = 23;

		// Token: 0x0400A547 RID: 42311
		internal const int ElementTypeIdConst = 11809;
	}
}
