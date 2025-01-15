using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B34 RID: 11060
	[GeneratedCode("DomGen", "2.0")]
	internal class Author : XstringType
	{
		// Token: 0x17007783 RID: 30595
		// (get) Token: 0x06016A43 RID: 92739 RVA: 0x0032D81F File Offset: 0x0032BA1F
		public override string LocalName
		{
			get
			{
				return "author";
			}
		}

		// Token: 0x17007784 RID: 30596
		// (get) Token: 0x06016A44 RID: 92740 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007785 RID: 30597
		// (get) Token: 0x06016A45 RID: 92741 RVA: 0x0032D826 File Offset: 0x0032BA26
		internal override int ElementTypeId
		{
			get
			{
				return 11057;
			}
		}

		// Token: 0x06016A46 RID: 92742 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A47 RID: 92743 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public Author()
		{
		}

		// Token: 0x06016A48 RID: 92744 RVA: 0x0032D835 File Offset: 0x0032BA35
		public Author(string text)
			: base(text)
		{
		}

		// Token: 0x06016A49 RID: 92745 RVA: 0x0032D840 File Offset: 0x0032BA40
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A4A RID: 92746 RVA: 0x0032D85B File Offset: 0x0032BA5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Author>(deep);
		}

		// Token: 0x0400995C RID: 39260
		private const string tagName = "author";

		// Token: 0x0400995D RID: 39261
		private const byte tagNsId = 22;

		// Token: 0x0400995E RID: 39262
		internal const int ElementTypeIdConst = 11057;
	}
}
