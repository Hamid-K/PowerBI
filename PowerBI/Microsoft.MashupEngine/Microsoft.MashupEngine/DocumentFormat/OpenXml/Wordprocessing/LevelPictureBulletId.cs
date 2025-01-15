using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E38 RID: 11832
	[GeneratedCode("DomGen", "2.0")]
	internal class LevelPictureBulletId : DecimalNumberType
	{
		// Token: 0x17008994 RID: 35220
		// (get) Token: 0x060191F1 RID: 102897 RVA: 0x003468A8 File Offset: 0x00344AA8
		public override string LocalName
		{
			get
			{
				return "lvlPicBulletId";
			}
		}

		// Token: 0x17008995 RID: 35221
		// (get) Token: 0x060191F2 RID: 102898 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008996 RID: 35222
		// (get) Token: 0x060191F3 RID: 102899 RVA: 0x003468AF File Offset: 0x00344AAF
		internal override int ElementTypeId
		{
			get
			{
				return 11869;
			}
		}

		// Token: 0x060191F4 RID: 102900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060191F6 RID: 102902 RVA: 0x003468B6 File Offset: 0x00344AB6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LevelPictureBulletId>(deep);
		}

		// Token: 0x0400A726 RID: 42790
		private const string tagName = "lvlPicBulletId";

		// Token: 0x0400A727 RID: 42791
		private const byte tagNsId = 23;

		// Token: 0x0400A728 RID: 42792
		internal const int ElementTypeIdConst = 11869;
	}
}
