using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7D RID: 11901
	[GeneratedCode("DomGen", "2.0")]
	internal class EndnoteReference : FootnoteEndnoteReferenceType
	{
		// Token: 0x17008AD6 RID: 35542
		// (get) Token: 0x06019499 RID: 103577 RVA: 0x00348420 File Offset: 0x00346620
		public override string LocalName
		{
			get
			{
				return "endnoteReference";
			}
		}

		// Token: 0x17008AD7 RID: 35543
		// (get) Token: 0x0601949A RID: 103578 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AD8 RID: 35544
		// (get) Token: 0x0601949B RID: 103579 RVA: 0x00348427 File Offset: 0x00346627
		internal override int ElementTypeId
		{
			get
			{
				return 11570;
			}
		}

		// Token: 0x0601949C RID: 103580 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601949E RID: 103582 RVA: 0x0034842E File Offset: 0x0034662E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnoteReference>(deep);
		}

		// Token: 0x0400A81A RID: 43034
		private const string tagName = "endnoteReference";

		// Token: 0x0400A81B RID: 43035
		private const byte tagNsId = 23;

		// Token: 0x0400A81C RID: 43036
		internal const int ElementTypeIdConst = 11570;
	}
}
