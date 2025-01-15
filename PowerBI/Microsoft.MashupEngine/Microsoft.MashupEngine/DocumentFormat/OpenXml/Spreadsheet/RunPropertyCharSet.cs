using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAA RID: 11178
	[GeneratedCode("DomGen", "2.0")]
	internal class RunPropertyCharSet : InternationalPropertyType
	{
		// Token: 0x17007B6A RID: 31594
		// (get) Token: 0x060172D4 RID: 94932 RVA: 0x0033377A File Offset: 0x0033197A
		public override string LocalName
		{
			get
			{
				return "charset";
			}
		}

		// Token: 0x17007B6B RID: 31595
		// (get) Token: 0x060172D5 RID: 94933 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B6C RID: 31596
		// (get) Token: 0x060172D6 RID: 94934 RVA: 0x00333781 File Offset: 0x00331981
		internal override int ElementTypeId
		{
			get
			{
				return 11148;
			}
		}

		// Token: 0x060172D7 RID: 94935 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172D9 RID: 94937 RVA: 0x00333788 File Offset: 0x00331988
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunPropertyCharSet>(deep);
		}

		// Token: 0x04009B73 RID: 39795
		private const string tagName = "charset";

		// Token: 0x04009B74 RID: 39796
		private const byte tagNsId = 22;

		// Token: 0x04009B75 RID: 39797
		internal const int ElementTypeIdConst = 11148;
	}
}
