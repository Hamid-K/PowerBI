using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3C RID: 11068
	[GeneratedCode("DomGen", "2.0")]
	internal class EvenFooter : XstringType
	{
		// Token: 0x1700779B RID: 30619
		// (get) Token: 0x06016A83 RID: 92803 RVA: 0x002F34D4 File Offset: 0x002F16D4
		public override string LocalName
		{
			get
			{
				return "evenFooter";
			}
		}

		// Token: 0x1700779C RID: 30620
		// (get) Token: 0x06016A84 RID: 92804 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700779D RID: 30621
		// (get) Token: 0x06016A85 RID: 92805 RVA: 0x0032D9A8 File Offset: 0x0032BBA8
		internal override int ElementTypeId
		{
			get
			{
				return 11226;
			}
		}

		// Token: 0x06016A86 RID: 92806 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A87 RID: 92807 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public EvenFooter()
		{
		}

		// Token: 0x06016A88 RID: 92808 RVA: 0x0032D835 File Offset: 0x0032BA35
		public EvenFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06016A89 RID: 92809 RVA: 0x0032D9B0 File Offset: 0x0032BBB0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A8A RID: 92810 RVA: 0x0032D9CB File Offset: 0x0032BBCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EvenFooter>(deep);
		}

		// Token: 0x04009974 RID: 39284
		private const string tagName = "evenFooter";

		// Token: 0x04009975 RID: 39285
		private const byte tagNsId = 22;

		// Token: 0x04009976 RID: 39286
		internal const int ElementTypeIdConst = 11226;
	}
}
