using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7C RID: 11900
	[GeneratedCode("DomGen", "2.0")]
	internal class FootnoteReference : FootnoteEndnoteReferenceType
	{
		// Token: 0x17008AD3 RID: 35539
		// (get) Token: 0x06019493 RID: 103571 RVA: 0x00348401 File Offset: 0x00346601
		public override string LocalName
		{
			get
			{
				return "footnoteReference";
			}
		}

		// Token: 0x17008AD4 RID: 35540
		// (get) Token: 0x06019494 RID: 103572 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AD5 RID: 35541
		// (get) Token: 0x06019495 RID: 103573 RVA: 0x00348408 File Offset: 0x00346608
		internal override int ElementTypeId
		{
			get
			{
				return 11569;
			}
		}

		// Token: 0x06019496 RID: 103574 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019498 RID: 103576 RVA: 0x00348417 File Offset: 0x00346617
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteReference>(deep);
		}

		// Token: 0x0400A817 RID: 43031
		private const string tagName = "footnoteReference";

		// Token: 0x0400A818 RID: 43032
		private const byte tagNsId = 23;

		// Token: 0x0400A819 RID: 43033
		internal const int ElementTypeIdConst = 11569;
	}
}
