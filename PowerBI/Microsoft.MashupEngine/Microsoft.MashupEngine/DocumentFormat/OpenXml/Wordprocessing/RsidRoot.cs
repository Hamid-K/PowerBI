using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F79 RID: 12153
	[GeneratedCode("DomGen", "2.0")]
	internal class RsidRoot : LongHexNumberType
	{
		// Token: 0x17009129 RID: 37161
		// (get) Token: 0x0601A276 RID: 107126 RVA: 0x0035E1E8 File Offset: 0x0035C3E8
		public override string LocalName
		{
			get
			{
				return "rsidRoot";
			}
		}

		// Token: 0x1700912A RID: 37162
		// (get) Token: 0x0601A277 RID: 107127 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700912B RID: 37163
		// (get) Token: 0x0601A278 RID: 107128 RVA: 0x0035E1EF File Offset: 0x0035C3EF
		internal override int ElementTypeId
		{
			get
			{
				return 11829;
			}
		}

		// Token: 0x0601A279 RID: 107129 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A27B RID: 107131 RVA: 0x0035E1FE File Offset: 0x0035C3FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RsidRoot>(deep);
		}

		// Token: 0x0400AC0F RID: 44047
		private const string tagName = "rsidRoot";

		// Token: 0x0400AC10 RID: 44048
		private const byte tagNsId = 23;

		// Token: 0x0400AC11 RID: 44049
		internal const int ElementTypeIdConst = 11829;
	}
}
