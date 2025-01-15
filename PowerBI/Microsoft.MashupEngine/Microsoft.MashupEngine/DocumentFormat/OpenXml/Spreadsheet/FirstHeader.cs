using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3D RID: 11069
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstHeader : XstringType
	{
		// Token: 0x1700779E RID: 30622
		// (get) Token: 0x06016A8B RID: 92811 RVA: 0x002F3508 File Offset: 0x002F1708
		public override string LocalName
		{
			get
			{
				return "firstHeader";
			}
		}

		// Token: 0x1700779F RID: 30623
		// (get) Token: 0x06016A8C RID: 92812 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077A0 RID: 30624
		// (get) Token: 0x06016A8D RID: 92813 RVA: 0x0032D9D4 File Offset: 0x0032BBD4
		internal override int ElementTypeId
		{
			get
			{
				return 11227;
			}
		}

		// Token: 0x06016A8E RID: 92814 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A8F RID: 92815 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public FirstHeader()
		{
		}

		// Token: 0x06016A90 RID: 92816 RVA: 0x0032D835 File Offset: 0x0032BA35
		public FirstHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06016A91 RID: 92817 RVA: 0x0032D9DC File Offset: 0x0032BBDC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A92 RID: 92818 RVA: 0x0032D9F7 File Offset: 0x0032BBF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstHeader>(deep);
		}

		// Token: 0x04009977 RID: 39287
		private const string tagName = "firstHeader";

		// Token: 0x04009978 RID: 39288
		private const byte tagNsId = 22;

		// Token: 0x04009979 RID: 39289
		internal const int ElementTypeIdConst = 11227;
	}
}
