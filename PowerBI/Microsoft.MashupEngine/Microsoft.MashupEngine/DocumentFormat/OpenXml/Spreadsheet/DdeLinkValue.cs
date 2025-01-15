using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B41 RID: 11073
	[GeneratedCode("DomGen", "2.0")]
	internal class DdeLinkValue : XstringType
	{
		// Token: 0x170077AA RID: 30634
		// (get) Token: 0x06016AAB RID: 92843 RVA: 0x002F2F88 File Offset: 0x002F1188
		public override string LocalName
		{
			get
			{
				return "val";
			}
		}

		// Token: 0x170077AB RID: 30635
		// (get) Token: 0x06016AAC RID: 92844 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077AC RID: 30636
		// (get) Token: 0x06016AAD RID: 92845 RVA: 0x0032DA84 File Offset: 0x0032BC84
		internal override int ElementTypeId
		{
			get
			{
				return 11288;
			}
		}

		// Token: 0x06016AAE RID: 92846 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016AAF RID: 92847 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public DdeLinkValue()
		{
		}

		// Token: 0x06016AB0 RID: 92848 RVA: 0x0032D835 File Offset: 0x0032BA35
		public DdeLinkValue(string text)
			: base(text)
		{
		}

		// Token: 0x06016AB1 RID: 92849 RVA: 0x0032DA8C File Offset: 0x0032BC8C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016AB2 RID: 92850 RVA: 0x0032DAA7 File Offset: 0x0032BCA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeLinkValue>(deep);
		}

		// Token: 0x04009983 RID: 39299
		private const string tagName = "val";

		// Token: 0x04009984 RID: 39300
		private const byte tagNsId = 22;

		// Token: 0x04009985 RID: 39301
		internal const int ElementTypeIdConst = 11288;
	}
}
