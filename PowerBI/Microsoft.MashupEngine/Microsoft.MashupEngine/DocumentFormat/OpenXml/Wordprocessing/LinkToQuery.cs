using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9F RID: 11679
	[GeneratedCode("DomGen", "2.0")]
	internal class LinkToQuery : OnOffType
	{
		// Token: 0x17008779 RID: 34681
		// (get) Token: 0x06018DB2 RID: 101810 RVA: 0x00344E56 File Offset: 0x00343056
		public override string LocalName
		{
			get
			{
				return "linkToQuery";
			}
		}

		// Token: 0x1700877A RID: 34682
		// (get) Token: 0x06018DB3 RID: 101811 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700877B RID: 34683
		// (get) Token: 0x06018DB4 RID: 101812 RVA: 0x00344E5D File Offset: 0x0034305D
		internal override int ElementTypeId
		{
			get
			{
				return 11813;
			}
		}

		// Token: 0x06018DB5 RID: 101813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DB7 RID: 101815 RVA: 0x00344E64 File Offset: 0x00343064
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkToQuery>(deep);
		}

		// Token: 0x0400A548 RID: 42312
		private const string tagName = "linkToQuery";

		// Token: 0x0400A549 RID: 42313
		private const byte tagNsId = 23;

		// Token: 0x0400A54A RID: 42314
		internal const int ElementTypeIdConst = 11813;
	}
}
