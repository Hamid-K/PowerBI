using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3A RID: 11066
	[GeneratedCode("DomGen", "2.0")]
	internal class OddFooter : XstringType
	{
		// Token: 0x17007795 RID: 30613
		// (get) Token: 0x06016A73 RID: 92787 RVA: 0x002F346C File Offset: 0x002F166C
		public override string LocalName
		{
			get
			{
				return "oddFooter";
			}
		}

		// Token: 0x17007796 RID: 30614
		// (get) Token: 0x06016A74 RID: 92788 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007797 RID: 30615
		// (get) Token: 0x06016A75 RID: 92789 RVA: 0x0032D950 File Offset: 0x0032BB50
		internal override int ElementTypeId
		{
			get
			{
				return 11224;
			}
		}

		// Token: 0x06016A76 RID: 92790 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A77 RID: 92791 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public OddFooter()
		{
		}

		// Token: 0x06016A78 RID: 92792 RVA: 0x0032D835 File Offset: 0x0032BA35
		public OddFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06016A79 RID: 92793 RVA: 0x0032D958 File Offset: 0x0032BB58
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A7A RID: 92794 RVA: 0x0032D973 File Offset: 0x0032BB73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OddFooter>(deep);
		}

		// Token: 0x0400996E RID: 39278
		private const string tagName = "oddFooter";

		// Token: 0x0400996F RID: 39279
		private const byte tagNsId = 22;

		// Token: 0x04009970 RID: 39280
		internal const int ElementTypeIdConst = 11224;
	}
}
