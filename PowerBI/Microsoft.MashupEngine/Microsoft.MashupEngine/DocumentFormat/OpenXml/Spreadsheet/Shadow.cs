using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9E RID: 11166
	[GeneratedCode("DomGen", "2.0")]
	internal class Shadow : BooleanPropertyType
	{
		// Token: 0x17007B36 RID: 31542
		// (get) Token: 0x0601726A RID: 94826 RVA: 0x002C0C98 File Offset: 0x002BEE98
		public override string LocalName
		{
			get
			{
				return "shadow";
			}
		}

		// Token: 0x17007B37 RID: 31543
		// (get) Token: 0x0601726B RID: 94827 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B38 RID: 31544
		// (get) Token: 0x0601726C RID: 94828 RVA: 0x00333417 File Offset: 0x00331617
		internal override int ElementTypeId
		{
			get
			{
				return 11141;
			}
		}

		// Token: 0x0601726D RID: 94829 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601726F RID: 94831 RVA: 0x0033341E File Offset: 0x0033161E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shadow>(deep);
		}

		// Token: 0x04009B49 RID: 39753
		private const string tagName = "shadow";

		// Token: 0x04009B4A RID: 39754
		private const byte tagNsId = 22;

		// Token: 0x04009B4B RID: 39755
		internal const int ElementTypeIdConst = 11141;
	}
}
