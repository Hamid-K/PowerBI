using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6D RID: 12141
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnDelimiter : UnsignedDecimalNumberType
	{
		// Token: 0x170090D7 RID: 37079
		// (get) Token: 0x0601A1CB RID: 106955 RVA: 0x0035D94C File Offset: 0x0035BB4C
		public override string LocalName
		{
			get
			{
				return "colDelim";
			}
		}

		// Token: 0x170090D8 RID: 37080
		// (get) Token: 0x0601A1CC RID: 106956 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090D9 RID: 37081
		// (get) Token: 0x0601A1CD RID: 106957 RVA: 0x0035D953 File Offset: 0x0035BB53
		internal override int ElementTypeId
		{
			get
			{
				return 11807;
			}
		}

		// Token: 0x0601A1CE RID: 106958 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A1D0 RID: 106960 RVA: 0x0035D95A File Offset: 0x0035BB5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnDelimiter>(deep);
		}

		// Token: 0x0400ABD8 RID: 43992
		private const string tagName = "colDelim";

		// Token: 0x0400ABD9 RID: 43993
		private const byte tagNsId = 23;

		// Token: 0x0400ABDA RID: 43994
		internal const int ElementTypeIdConst = 11807;
	}
}
