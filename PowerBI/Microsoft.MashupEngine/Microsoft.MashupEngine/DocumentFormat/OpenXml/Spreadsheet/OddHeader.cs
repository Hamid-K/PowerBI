using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B39 RID: 11065
	[GeneratedCode("DomGen", "2.0")]
	internal class OddHeader : XstringType
	{
		// Token: 0x17007792 RID: 30610
		// (get) Token: 0x06016A6B RID: 92779 RVA: 0x002F3438 File Offset: 0x002F1638
		public override string LocalName
		{
			get
			{
				return "oddHeader";
			}
		}

		// Token: 0x17007793 RID: 30611
		// (get) Token: 0x06016A6C RID: 92780 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007794 RID: 30612
		// (get) Token: 0x06016A6D RID: 92781 RVA: 0x0032D924 File Offset: 0x0032BB24
		internal override int ElementTypeId
		{
			get
			{
				return 11223;
			}
		}

		// Token: 0x06016A6E RID: 92782 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A6F RID: 92783 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public OddHeader()
		{
		}

		// Token: 0x06016A70 RID: 92784 RVA: 0x0032D835 File Offset: 0x0032BA35
		public OddHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06016A71 RID: 92785 RVA: 0x0032D92C File Offset: 0x0032BB2C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A72 RID: 92786 RVA: 0x0032D947 File Offset: 0x0032BB47
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OddHeader>(deep);
		}

		// Token: 0x0400996B RID: 39275
		private const string tagName = "oddHeader";

		// Token: 0x0400996C RID: 39276
		private const byte tagNsId = 22;

		// Token: 0x0400996D RID: 39277
		internal const int ElementTypeIdConst = 11223;
	}
}
