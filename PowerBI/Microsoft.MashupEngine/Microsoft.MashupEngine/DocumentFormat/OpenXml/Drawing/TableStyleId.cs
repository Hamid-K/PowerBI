using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F2 RID: 10226
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyleId : OpenXmlLeafTextElement
	{
		// Token: 0x170064DA RID: 25818
		// (get) Token: 0x06013F83 RID: 81795 RVA: 0x0030DF03 File Offset: 0x0030C103
		public override string LocalName
		{
			get
			{
				return "tableStyleId";
			}
		}

		// Token: 0x170064DB RID: 25819
		// (get) Token: 0x06013F84 RID: 81796 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064DC RID: 25820
		// (get) Token: 0x06013F85 RID: 81797 RVA: 0x0030DF0A File Offset: 0x0030C10A
		internal override int ElementTypeId
		{
			get
			{
				return 10262;
			}
		}

		// Token: 0x06013F86 RID: 81798 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013F87 RID: 81799 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public TableStyleId()
		{
		}

		// Token: 0x06013F88 RID: 81800 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public TableStyleId(string text)
			: base(text)
		{
		}

		// Token: 0x06013F89 RID: 81801 RVA: 0x0030DF14 File Offset: 0x0030C114
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06013F8A RID: 81802 RVA: 0x0030DF2F File Offset: 0x0030C12F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleId>(deep);
		}

		// Token: 0x04008873 RID: 34931
		private const string tagName = "tableStyleId";

		// Token: 0x04008874 RID: 34932
		private const byte tagNsId = 10;

		// Token: 0x04008875 RID: 34933
		internal const int ElementTypeIdConst = 10262;
	}
}
