using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D7F RID: 11647
	[GeneratedCode("DomGen", "2.0")]
	internal class GutterOnRight : OnOffType
	{
		// Token: 0x17008719 RID: 34585
		// (get) Token: 0x06018CF2 RID: 101618 RVA: 0x00344BBC File Offset: 0x00342DBC
		public override string LocalName
		{
			get
			{
				return "rtlGutter";
			}
		}

		// Token: 0x1700871A RID: 34586
		// (get) Token: 0x06018CF3 RID: 101619 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700871B RID: 34587
		// (get) Token: 0x06018CF4 RID: 101620 RVA: 0x00344BC3 File Offset: 0x00342DC3
		internal override int ElementTypeId
		{
			get
			{
				return 11540;
			}
		}

		// Token: 0x06018CF5 RID: 101621 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CF7 RID: 101623 RVA: 0x00344BCA File Offset: 0x00342DCA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GutterOnRight>(deep);
		}

		// Token: 0x0400A4E8 RID: 42216
		private const string tagName = "rtlGutter";

		// Token: 0x0400A4E9 RID: 42217
		private const byte tagNsId = 23;

		// Token: 0x0400A4EA RID: 42218
		internal const int ElementTypeIdConst = 11540;
	}
}
