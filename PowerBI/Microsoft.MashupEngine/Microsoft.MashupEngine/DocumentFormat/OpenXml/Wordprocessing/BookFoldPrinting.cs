using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCC RID: 11724
	[GeneratedCode("DomGen", "2.0")]
	internal class BookFoldPrinting : OnOffType
	{
		// Token: 0x17008800 RID: 34816
		// (get) Token: 0x06018EC0 RID: 102080 RVA: 0x00345261 File Offset: 0x00343461
		public override string LocalName
		{
			get
			{
				return "bookFoldPrinting";
			}
		}

		// Token: 0x17008801 RID: 34817
		// (get) Token: 0x06018EC1 RID: 102081 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008802 RID: 34818
		// (get) Token: 0x06018EC2 RID: 102082 RVA: 0x00345268 File Offset: 0x00343468
		internal override int ElementTypeId
		{
			get
			{
				return 12007;
			}
		}

		// Token: 0x06018EC3 RID: 102083 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EC5 RID: 102085 RVA: 0x0034526F File Offset: 0x0034346F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookFoldPrinting>(deep);
		}

		// Token: 0x0400A5CF RID: 42447
		private const string tagName = "bookFoldPrinting";

		// Token: 0x0400A5D0 RID: 42448
		private const byte tagNsId = 23;

		// Token: 0x0400A5D1 RID: 42449
		internal const int ElementTypeIdConst = 12007;
	}
}
