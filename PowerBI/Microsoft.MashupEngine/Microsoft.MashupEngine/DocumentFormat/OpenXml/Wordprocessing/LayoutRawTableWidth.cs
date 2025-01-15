using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E07 RID: 11783
	[GeneratedCode("DomGen", "2.0")]
	internal class LayoutRawTableWidth : OnOffType
	{
		// Token: 0x170088B1 RID: 34993
		// (get) Token: 0x06019022 RID: 102434 RVA: 0x003457AE File Offset: 0x003439AE
		public override string LocalName
		{
			get
			{
				return "layoutRawTableWidth";
			}
		}

		// Token: 0x170088B2 RID: 34994
		// (get) Token: 0x06019023 RID: 102435 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088B3 RID: 34995
		// (get) Token: 0x06019024 RID: 102436 RVA: 0x003457B5 File Offset: 0x003439B5
		internal override int ElementTypeId
		{
			get
			{
				return 12093;
			}
		}

		// Token: 0x06019025 RID: 102437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019027 RID: 102439 RVA: 0x003457BC File Offset: 0x003439BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutRawTableWidth>(deep);
		}

		// Token: 0x0400A680 RID: 42624
		private const string tagName = "layoutRawTableWidth";

		// Token: 0x0400A681 RID: 42625
		private const byte tagNsId = 23;

		// Token: 0x0400A682 RID: 42626
		internal const int ElementTypeIdConst = 12093;
	}
}
