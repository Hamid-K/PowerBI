using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3E RID: 11838
	[GeneratedCode("DomGen", "2.0")]
	internal class HeaderReference : HeaderFooterReferenceType
	{
		// Token: 0x170089B0 RID: 35248
		// (get) Token: 0x0601922B RID: 102955 RVA: 0x00346AC9 File Offset: 0x00344CC9
		public override string LocalName
		{
			get
			{
				return "headerReference";
			}
		}

		// Token: 0x170089B1 RID: 35249
		// (get) Token: 0x0601922C RID: 102956 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089B2 RID: 35250
		// (get) Token: 0x0601922D RID: 102957 RVA: 0x00346AD0 File Offset: 0x00344CD0
		internal override int ElementTypeId
		{
			get
			{
				return 11524;
			}
		}

		// Token: 0x0601922E RID: 102958 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019230 RID: 102960 RVA: 0x00346ADF File Offset: 0x00344CDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderReference>(deep);
		}

		// Token: 0x0400A73B RID: 42811
		private const string tagName = "headerReference";

		// Token: 0x0400A73C RID: 42812
		private const byte tagNsId = 23;

		// Token: 0x0400A73D RID: 42813
		internal const int ElementTypeIdConst = 11524;
	}
}
