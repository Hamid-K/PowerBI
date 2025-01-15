using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258B RID: 9611
	[GeneratedCode("DomGen", "2.0")]
	internal class HeightMode : LayoutModeType
	{
		// Token: 0x17005653 RID: 22099
		// (get) Token: 0x06011F23 RID: 73507 RVA: 0x002F3EA0 File Offset: 0x002F20A0
		public override string LocalName
		{
			get
			{
				return "hMode";
			}
		}

		// Token: 0x17005654 RID: 22100
		// (get) Token: 0x06011F24 RID: 73508 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005655 RID: 22101
		// (get) Token: 0x06011F25 RID: 73509 RVA: 0x002F3EA7 File Offset: 0x002F20A7
		internal override int ElementTypeId
		{
			get
			{
				return 10410;
			}
		}

		// Token: 0x06011F26 RID: 73510 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011F28 RID: 73512 RVA: 0x002F3EAE File Offset: 0x002F20AE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeightMode>(deep);
		}

		// Token: 0x04007D63 RID: 32099
		private const string tagName = "hMode";

		// Token: 0x04007D64 RID: 32100
		private const byte tagNsId = 11;

		// Token: 0x04007D65 RID: 32101
		internal const int ElementTypeIdConst = 10410;
	}
}
