using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9D RID: 11933
	[GeneratedCode("DomGen", "2.0")]
	internal class Border : BorderType
	{
		// Token: 0x17008B72 RID: 35698
		// (get) Token: 0x060195D6 RID: 103894 RVA: 0x00349012 File Offset: 0x00347212
		public override string LocalName
		{
			get
			{
				return "bdr";
			}
		}

		// Token: 0x17008B73 RID: 35699
		// (get) Token: 0x060195D7 RID: 103895 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B74 RID: 35700
		// (get) Token: 0x060195D8 RID: 103896 RVA: 0x00349019 File Offset: 0x00347219
		internal override int ElementTypeId
		{
			get
			{
				return 11602;
			}
		}

		// Token: 0x060195D9 RID: 103897 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195DB RID: 103899 RVA: 0x00349028 File Offset: 0x00347228
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Border>(deep);
		}

		// Token: 0x0400A88D RID: 43149
		private const string tagName = "bdr";

		// Token: 0x0400A88E RID: 43150
		private const byte tagNsId = 23;

		// Token: 0x0400A88F RID: 43151
		internal const int ElementTypeIdConst = 11602;
	}
}
