using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B3E RID: 11070
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstFooter : XstringType
	{
		// Token: 0x170077A1 RID: 30625
		// (get) Token: 0x06016A93 RID: 92819 RVA: 0x002F353C File Offset: 0x002F173C
		public override string LocalName
		{
			get
			{
				return "firstFooter";
			}
		}

		// Token: 0x170077A2 RID: 30626
		// (get) Token: 0x06016A94 RID: 92820 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077A3 RID: 30627
		// (get) Token: 0x06016A95 RID: 92821 RVA: 0x0032DA00 File Offset: 0x0032BC00
		internal override int ElementTypeId
		{
			get
			{
				return 11228;
			}
		}

		// Token: 0x06016A96 RID: 92822 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A97 RID: 92823 RVA: 0x0032D82D File Offset: 0x0032BA2D
		public FirstFooter()
		{
		}

		// Token: 0x06016A98 RID: 92824 RVA: 0x0032D835 File Offset: 0x0032BA35
		public FirstFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06016A99 RID: 92825 RVA: 0x0032DA08 File Offset: 0x0032BC08
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06016A9A RID: 92826 RVA: 0x0032DA23 File Offset: 0x0032BC23
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstFooter>(deep);
		}

		// Token: 0x0400997A RID: 39290
		private const string tagName = "firstFooter";

		// Token: 0x0400997B RID: 39291
		private const byte tagNsId = 22;

		// Token: 0x0400997C RID: 39292
		internal const int ElementTypeIdConst = 11228;
	}
}
