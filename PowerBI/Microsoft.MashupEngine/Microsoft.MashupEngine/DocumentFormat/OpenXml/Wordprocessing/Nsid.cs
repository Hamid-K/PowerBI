using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F7B RID: 12155
	[GeneratedCode("DomGen", "2.0")]
	internal class Nsid : LongHexNumberType
	{
		// Token: 0x1700912F RID: 37167
		// (get) Token: 0x0601A282 RID: 107138 RVA: 0x0035E21E File Offset: 0x0035C41E
		public override string LocalName
		{
			get
			{
				return "nsid";
			}
		}

		// Token: 0x17009130 RID: 37168
		// (get) Token: 0x0601A283 RID: 107139 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009131 RID: 37169
		// (get) Token: 0x0601A284 RID: 107140 RVA: 0x0035E225 File Offset: 0x0035C425
		internal override int ElementTypeId
		{
			get
			{
				return 11874;
			}
		}

		// Token: 0x0601A285 RID: 107141 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A287 RID: 107143 RVA: 0x0035E22C File Offset: 0x0035C42C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Nsid>(deep);
		}

		// Token: 0x0400AC15 RID: 44053
		private const string tagName = "nsid";

		// Token: 0x0400AC16 RID: 44054
		private const byte tagNsId = 23;

		// Token: 0x0400AC17 RID: 44055
		internal const int ElementTypeIdConst = 11874;
	}
}
