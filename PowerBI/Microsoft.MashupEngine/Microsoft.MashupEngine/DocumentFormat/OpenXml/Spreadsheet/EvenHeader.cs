using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3B RID: 11067
	[GeneratedCode("DomGen", "2.0")]
	internal class EvenHeader : XstringType
	{
		// Token: 0x17007798 RID: 30616
		// (get) Token: 0x06016A7B RID: 92795 RVA: 0x002F34A0 File Offset: 0x002F16A0
		public override string LocalName
		{
			get
			{
				return "evenHeader";
			}
		}

		// Token: 0x17007799 RID: 30617
		// (get) Token: 0x06016A7C RID: 92796 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700779A RID: 30618
		// (get) Token: 0x06016A7D RID: 92797 RVA: 0x0032D97C File Offset: 0x0032BB7C
		internal override int ElementTypeId
		{
			get
			{
				return 11225;
			}
		}

		// Token: 0x06016A7E RID: 92798 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A7F RID: 92799 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public EvenHeader()
		{
		}

		// Token: 0x06016A80 RID: 92800 RVA: 0x0032D835 File Offset: 0x0032BA35
		public EvenHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06016A81 RID: 92801 RVA: 0x0032D984 File Offset: 0x0032BB84
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A82 RID: 92802 RVA: 0x0032D99F File Offset: 0x0032BB9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EvenHeader>(deep);
		}

		// Token: 0x04009971 RID: 39281
		private const string tagName = "evenHeader";

		// Token: 0x04009972 RID: 39282
		private const byte tagNsId = 22;

		// Token: 0x04009973 RID: 39283
		internal const int ElementTypeIdConst = 11225;
	}
}
