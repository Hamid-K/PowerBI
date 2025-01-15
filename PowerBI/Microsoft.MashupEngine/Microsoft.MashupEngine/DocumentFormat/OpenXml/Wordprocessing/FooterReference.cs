using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3F RID: 11839
	[GeneratedCode("DomGen", "2.0")]
	internal class FooterReference : HeaderFooterReferenceType
	{
		// Token: 0x170089B3 RID: 35251
		// (get) Token: 0x06019231 RID: 102961 RVA: 0x00346AE8 File Offset: 0x00344CE8
		public override string LocalName
		{
			get
			{
				return "footerReference";
			}
		}

		// Token: 0x170089B4 RID: 35252
		// (get) Token: 0x06019232 RID: 102962 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089B5 RID: 35253
		// (get) Token: 0x06019233 RID: 102963 RVA: 0x00346AEF File Offset: 0x00344CEF
		internal override int ElementTypeId
		{
			get
			{
				return 11525;
			}
		}

		// Token: 0x06019234 RID: 102964 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019236 RID: 102966 RVA: 0x00346AF6 File Offset: 0x00344CF6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FooterReference>(deep);
		}

		// Token: 0x0400A73E RID: 42814
		private const string tagName = "footerReference";

		// Token: 0x0400A73F RID: 42815
		private const byte tagNsId = 23;

		// Token: 0x0400A740 RID: 42816
		internal const int ElementTypeIdConst = 11525;
	}
}
