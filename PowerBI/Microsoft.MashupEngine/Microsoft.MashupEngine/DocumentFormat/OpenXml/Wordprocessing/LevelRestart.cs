using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E37 RID: 11831
	[GeneratedCode("DomGen", "2.0")]
	internal class LevelRestart : DecimalNumberType
	{
		// Token: 0x17008991 RID: 35217
		// (get) Token: 0x060191EB RID: 102891 RVA: 0x00346891 File Offset: 0x00344A91
		public override string LocalName
		{
			get
			{
				return "lvlRestart";
			}
		}

		// Token: 0x17008992 RID: 35218
		// (get) Token: 0x060191EC RID: 102892 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008993 RID: 35219
		// (get) Token: 0x060191ED RID: 102893 RVA: 0x00346898 File Offset: 0x00344A98
		internal override int ElementTypeId
		{
			get
			{
				return 11864;
			}
		}

		// Token: 0x060191EE RID: 102894 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191F0 RID: 102896 RVA: 0x0034689F File Offset: 0x00344A9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelRestart>(deep);
		}

		// Token: 0x0400A723 RID: 42787
		private const string tagName = "lvlRestart";

		// Token: 0x0400A724 RID: 42788
		private const byte tagNsId = 23;

		// Token: 0x0400A725 RID: 42789
		internal const int ElementTypeIdConst = 11864;
	}
}
