using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DDB RID: 11739
	[GeneratedCode("DomGen", "2.0")]
	internal class AlwaysMergeEmptyNamespace : OnOffType
	{
		// Token: 0x1700882D RID: 34861
		// (get) Token: 0x06018F1A RID: 102170 RVA: 0x003453BA File Offset: 0x003435BA
		public override string LocalName
		{
			get
			{
				return "alwaysMergeEmptyNamespace";
			}
		}

		// Token: 0x1700882E RID: 34862
		// (get) Token: 0x06018F1B RID: 102171 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700882F RID: 34863
		// (get) Token: 0x06018F1C RID: 102172 RVA: 0x003453C1 File Offset: 0x003435C1
		internal override int ElementTypeId
		{
			get
			{
				return 12033;
			}
		}

		// Token: 0x06018F1D RID: 102173 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F1F RID: 102175 RVA: 0x003453C8 File Offset: 0x003435C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlwaysMergeEmptyNamespace>(deep);
		}

		// Token: 0x0400A5FC RID: 42492
		private const string tagName = "alwaysMergeEmptyNamespace";

		// Token: 0x0400A5FD RID: 42493
		private const byte tagNsId = 23;

		// Token: 0x0400A5FE RID: 42494
		internal const int ElementTypeIdConst = 12033;
	}
}
