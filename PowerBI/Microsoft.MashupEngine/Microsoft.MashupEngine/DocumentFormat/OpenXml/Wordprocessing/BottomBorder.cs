using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA0 RID: 11936
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomBorder : BorderType
	{
		// Token: 0x17008B7B RID: 35707
		// (get) Token: 0x060195E8 RID: 103912 RVA: 0x002BF3AD File Offset: 0x002BD5AD
		public override string LocalName
		{
			get
			{
				return "bottom";
			}
		}

		// Token: 0x17008B7C RID: 35708
		// (get) Token: 0x060195E9 RID: 103913 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B7D RID: 35709
		// (get) Token: 0x060195EA RID: 103914 RVA: 0x00349051 File Offset: 0x00347251
		internal override int ElementTypeId
		{
			get
			{
				return 11717;
			}
		}

		// Token: 0x060195EB RID: 103915 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195ED RID: 103917 RVA: 0x00349058 File Offset: 0x00347258
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomBorder>(deep);
		}

		// Token: 0x0400A896 RID: 43158
		private const string tagName = "bottom";

		// Token: 0x0400A897 RID: 43159
		private const byte tagNsId = 23;

		// Token: 0x0400A898 RID: 43160
		internal const int ElementTypeIdConst = 11717;
	}
}
