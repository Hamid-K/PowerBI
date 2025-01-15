using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B35 RID: 11061
	[GeneratedCode("DomGen", "2.0")]
	internal class Text : XstringType
	{
		// Token: 0x17007786 RID: 30598
		// (get) Token: 0x06016A4B RID: 92747 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x17007787 RID: 30599
		// (get) Token: 0x06016A4C RID: 92748 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007788 RID: 30600
		// (get) Token: 0x06016A4D RID: 92749 RVA: 0x0032D864 File Offset: 0x0032BA64
		internal override int ElementTypeId
		{
			get
			{
				return 11150;
			}
		}

		// Token: 0x06016A4E RID: 92750 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A4F RID: 92751 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Text()
		{
		}

		// Token: 0x06016A50 RID: 92752 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Text(string text)
			: base(text)
		{
		}

		// Token: 0x06016A51 RID: 92753 RVA: 0x0032D86C File Offset: 0x0032BA6C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A52 RID: 92754 RVA: 0x0032D887 File Offset: 0x0032BA87
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Text>(deep);
		}

		// Token: 0x0400995F RID: 39263
		private const string tagName = "t";

		// Token: 0x04009960 RID: 39264
		private const byte tagNsId = 22;

		// Token: 0x04009961 RID: 39265
		internal const int ElementTypeIdConst = 11150;
	}
}
