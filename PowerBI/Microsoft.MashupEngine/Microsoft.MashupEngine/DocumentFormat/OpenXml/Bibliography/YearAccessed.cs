using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028ED RID: 10477
	[GeneratedCode("DomGen", "2.0")]
	internal class YearAccessed : OpenXmlLeafTextElement
	{
		// Token: 0x1700696F RID: 26991
		// (get) Token: 0x06014A5C RID: 84572 RVA: 0x003152F0 File Offset: 0x003134F0
		public override string LocalName
		{
			get
			{
				return "YearAccessed";
			}
		}

		// Token: 0x17006970 RID: 26992
		// (get) Token: 0x06014A5D RID: 84573 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006971 RID: 26993
		// (get) Token: 0x06014A5E RID: 84574 RVA: 0x003152F7 File Offset: 0x003134F7
		internal override int ElementTypeId
		{
			get
			{
				return 10832;
			}
		}

		// Token: 0x06014A5F RID: 84575 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A60 RID: 84576 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public YearAccessed()
		{
		}

		// Token: 0x06014A61 RID: 84577 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public YearAccessed(string text)
			: base(text)
		{
		}

		// Token: 0x06014A62 RID: 84578 RVA: 0x00315300 File Offset: 0x00313500
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A63 RID: 84579 RVA: 0x0031531B File Offset: 0x0031351B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<YearAccessed>(deep);
		}

		// Token: 0x04008F53 RID: 36691
		private const string tagName = "YearAccessed";

		// Token: 0x04008F54 RID: 36692
		private const byte tagNsId = 9;

		// Token: 0x04008F55 RID: 36693
		internal const int ElementTypeIdConst = 10832;
	}
}
