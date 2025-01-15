using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8B RID: 11915
	[GeneratedCode("DomGen", "2.0")]
	internal class ClickAndTypeStyle : String253Type
	{
		// Token: 0x17008B0A RID: 35594
		// (get) Token: 0x06019504 RID: 103684 RVA: 0x00348702 File Offset: 0x00346902
		public override string LocalName
		{
			get
			{
				return "clickAndTypeStyle";
			}
		}

		// Token: 0x17008B0B RID: 35595
		// (get) Token: 0x06019505 RID: 103685 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B0C RID: 35596
		// (get) Token: 0x06019506 RID: 103686 RVA: 0x00348709 File Offset: 0x00346909
		internal override int ElementTypeId
		{
			get
			{
				return 12003;
			}
		}

		// Token: 0x06019507 RID: 103687 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019509 RID: 103689 RVA: 0x00348710 File Offset: 0x00346910
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ClickAndTypeStyle>(deep);
		}

		// Token: 0x0400A847 RID: 43079
		private const string tagName = "clickAndTypeStyle";

		// Token: 0x0400A848 RID: 43080
		private const byte tagNsId = 23;

		// Token: 0x0400A849 RID: 43081
		internal const int ElementTypeIdConst = 12003;
	}
}
