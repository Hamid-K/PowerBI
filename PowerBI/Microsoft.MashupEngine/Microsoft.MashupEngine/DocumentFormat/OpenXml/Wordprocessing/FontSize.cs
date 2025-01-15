using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E94 RID: 11924
	[GeneratedCode("DomGen", "2.0")]
	internal class FontSize : HpsMeasureType
	{
		// Token: 0x17008B42 RID: 35650
		// (get) Token: 0x06019575 RID: 103797 RVA: 0x0033352F File Offset: 0x0033172F
		public override string LocalName
		{
			get
			{
				return "sz";
			}
		}

		// Token: 0x17008B43 RID: 35651
		// (get) Token: 0x06019576 RID: 103798 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B44 RID: 35652
		// (get) Token: 0x06019577 RID: 103799 RVA: 0x00348BE4 File Offset: 0x00346DE4
		internal override int ElementTypeId
		{
			get
			{
				return 11597;
			}
		}

		// Token: 0x06019578 RID: 103800 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601957A RID: 103802 RVA: 0x00348BF3 File Offset: 0x00346DF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontSize>(deep);
		}

		// Token: 0x0400A86D RID: 43117
		private const string tagName = "sz";

		// Token: 0x0400A86E RID: 43118
		private const byte tagNsId = 23;

		// Token: 0x0400A86F RID: 43119
		internal const int ElementTypeIdConst = 11597;
	}
}
