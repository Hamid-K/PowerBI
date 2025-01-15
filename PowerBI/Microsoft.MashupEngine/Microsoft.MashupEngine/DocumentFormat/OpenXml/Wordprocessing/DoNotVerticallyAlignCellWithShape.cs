using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1D RID: 11805
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotVerticallyAlignCellWithShape : OnOffType
	{
		// Token: 0x170088F3 RID: 35059
		// (get) Token: 0x060190A6 RID: 102566 RVA: 0x003459A8 File Offset: 0x00343BA8
		public override string LocalName
		{
			get
			{
				return "doNotVertAlignCellWithSp";
			}
		}

		// Token: 0x170088F4 RID: 35060
		// (get) Token: 0x060190A7 RID: 102567 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088F5 RID: 35061
		// (get) Token: 0x060190A8 RID: 102568 RVA: 0x003459AF File Offset: 0x00343BAF
		internal override int ElementTypeId
		{
			get
			{
				return 12115;
			}
		}

		// Token: 0x060190A9 RID: 102569 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190AB RID: 102571 RVA: 0x003459B6 File Offset: 0x00343BB6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotVerticallyAlignCellWithShape>(deep);
		}

		// Token: 0x0400A6C2 RID: 42690
		private const string tagName = "doNotVertAlignCellWithSp";

		// Token: 0x0400A6C3 RID: 42691
		private const byte tagNsId = 23;

		// Token: 0x0400A6C4 RID: 42692
		internal const int ElementTypeIdConst = 12115;
	}
}
