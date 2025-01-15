using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C5 RID: 10437
	[GeneratedCode("DomGen", "2.0")]
	internal class ConferenceName : OpenXmlLeafTextElement
	{
		// Token: 0x170068F7 RID: 26871
		// (get) Token: 0x0601491C RID: 84252 RVA: 0x00314AD8 File Offset: 0x00312CD8
		public override string LocalName
		{
			get
			{
				return "ConferenceName";
			}
		}

		// Token: 0x170068F8 RID: 26872
		// (get) Token: 0x0601491D RID: 84253 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068F9 RID: 26873
		// (get) Token: 0x0601491E RID: 84254 RVA: 0x00314ADF File Offset: 0x00312CDF
		internal override int ElementTypeId
		{
			get
			{
				return 10791;
			}
		}

		// Token: 0x0601491F RID: 84255 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014920 RID: 84256 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ConferenceName()
		{
		}

		// Token: 0x06014921 RID: 84257 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ConferenceName(string text)
			: base(text)
		{
		}

		// Token: 0x06014922 RID: 84258 RVA: 0x00314AE8 File Offset: 0x00312CE8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014923 RID: 84259 RVA: 0x00314B03 File Offset: 0x00312D03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConferenceName>(deep);
		}

		// Token: 0x04008EDB RID: 36571
		private const string tagName = "ConferenceName";

		// Token: 0x04008EDC RID: 36572
		private const byte tagNsId = 9;

		// Token: 0x04008EDD RID: 36573
		internal const int ElementTypeIdConst = 10791;
	}
}
