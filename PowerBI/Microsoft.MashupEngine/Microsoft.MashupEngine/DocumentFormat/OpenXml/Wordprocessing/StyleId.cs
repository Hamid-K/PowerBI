using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D62 RID: 11618
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleId : StringType
	{
		// Token: 0x170086C2 RID: 34498
		// (get) Token: 0x06018C43 RID: 101443 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x170086C3 RID: 34499
		// (get) Token: 0x06018C44 RID: 101444 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086C4 RID: 34500
		// (get) Token: 0x06018C45 RID: 101445 RVA: 0x003448DF File Offset: 0x00342ADF
		internal override int ElementTypeId
		{
			get
			{
				return 11949;
			}
		}

		// Token: 0x06018C46 RID: 101446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C48 RID: 101448 RVA: 0x003448E6 File Offset: 0x00342AE6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleId>(deep);
		}

		// Token: 0x0400A492 RID: 42130
		private const string tagName = "style";

		// Token: 0x0400A493 RID: 42131
		private const byte tagNsId = 23;

		// Token: 0x0400A494 RID: 42132
		internal const int ElementTypeIdConst = 11949;
	}
}
