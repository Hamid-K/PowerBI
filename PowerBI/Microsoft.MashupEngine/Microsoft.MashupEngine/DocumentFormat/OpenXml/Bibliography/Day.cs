using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C8 RID: 10440
	[GeneratedCode("DomGen", "2.0")]
	internal class Day : OpenXmlLeafTextElement
	{
		// Token: 0x17006900 RID: 26880
		// (get) Token: 0x06014934 RID: 84276 RVA: 0x00314B74 File Offset: 0x00312D74
		public override string LocalName
		{
			get
			{
				return "Day";
			}
		}

		// Token: 0x17006901 RID: 26881
		// (get) Token: 0x06014935 RID: 84277 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006902 RID: 26882
		// (get) Token: 0x06014936 RID: 84278 RVA: 0x00314B7B File Offset: 0x00312D7B
		internal override int ElementTypeId
		{
			get
			{
				return 10794;
			}
		}

		// Token: 0x06014937 RID: 84279 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014938 RID: 84280 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Day()
		{
		}

		// Token: 0x06014939 RID: 84281 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Day(string text)
			: base(text)
		{
		}

		// Token: 0x0601493A RID: 84282 RVA: 0x00314B84 File Offset: 0x00312D84
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601493B RID: 84283 RVA: 0x00314B9F File Offset: 0x00312D9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Day>(deep);
		}

		// Token: 0x04008EE4 RID: 36580
		private const string tagName = "Day";

		// Token: 0x04008EE5 RID: 36581
		private const byte tagNsId = 9;

		// Token: 0x04008EE6 RID: 36582
		internal const int ElementTypeIdConst = 10794;
	}
}
