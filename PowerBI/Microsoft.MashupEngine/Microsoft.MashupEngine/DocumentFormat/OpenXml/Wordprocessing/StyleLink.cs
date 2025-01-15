using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E85 RID: 11909
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleLink : String253Type
	{
		// Token: 0x17008AF8 RID: 35576
		// (get) Token: 0x060194E0 RID: 103648 RVA: 0x0034867F File Offset: 0x0034687F
		public override string LocalName
		{
			get
			{
				return "styleLink";
			}
		}

		// Token: 0x17008AF9 RID: 35577
		// (get) Token: 0x060194E1 RID: 103649 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AFA RID: 35578
		// (get) Token: 0x060194E2 RID: 103650 RVA: 0x00348686 File Offset: 0x00346886
		internal override int ElementTypeId
		{
			get
			{
				return 11878;
			}
		}

		// Token: 0x060194E3 RID: 103651 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194E5 RID: 103653 RVA: 0x0034868D File Offset: 0x0034688D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleLink>(deep);
		}

		// Token: 0x0400A835 RID: 43061
		private const string tagName = "styleLink";

		// Token: 0x0400A836 RID: 43062
		private const byte tagNsId = 23;

		// Token: 0x0400A837 RID: 43063
		internal const int ElementTypeIdConst = 11878;
	}
}
