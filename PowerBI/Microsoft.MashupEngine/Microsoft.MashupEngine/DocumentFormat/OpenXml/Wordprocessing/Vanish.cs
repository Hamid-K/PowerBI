using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D8D RID: 11661
	[GeneratedCode("DomGen", "2.0")]
	internal class Vanish : OnOffType
	{
		// Token: 0x17008743 RID: 34627
		// (get) Token: 0x06018D46 RID: 101702 RVA: 0x00344CDB File Offset: 0x00342EDB
		public override string LocalName
		{
			get
			{
				return "vanish";
			}
		}

		// Token: 0x17008744 RID: 34628
		// (get) Token: 0x06018D47 RID: 101703 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008745 RID: 34629
		// (get) Token: 0x06018D48 RID: 101704 RVA: 0x00344CE2 File Offset: 0x00342EE2
		internal override int ElementTypeId
		{
			get
			{
				return 11590;
			}
		}

		// Token: 0x06018D49 RID: 101705 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D4B RID: 101707 RVA: 0x00344CE9 File Offset: 0x00342EE9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Vanish>(deep);
		}

		// Token: 0x0400A512 RID: 42258
		private const string tagName = "vanish";

		// Token: 0x0400A513 RID: 42259
		private const byte tagNsId = 23;

		// Token: 0x0400A514 RID: 42260
		internal const int ElementTypeIdConst = 11590;
	}
}
