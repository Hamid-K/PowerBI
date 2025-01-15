using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x02002902 RID: 10498
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceType : OpenXmlLeafTextElement
	{
		// Token: 0x170069C4 RID: 27076
		// (get) Token: 0x06014B44 RID: 84804 RVA: 0x003159BD File Offset: 0x00313BBD
		public override string LocalName
		{
			get
			{
				return "SourceType";
			}
		}

		// Token: 0x170069C5 RID: 27077
		// (get) Token: 0x06014B45 RID: 84805 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069C6 RID: 27078
		// (get) Token: 0x06014B46 RID: 84806 RVA: 0x003159C4 File Offset: 0x00313BC4
		internal override int ElementTypeId
		{
			get
			{
				return 10818;
			}
		}

		// Token: 0x06014B47 RID: 84807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014B48 RID: 84808 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public SourceType()
		{
		}

		// Token: 0x06014B49 RID: 84809 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public SourceType(string text)
			: base(text)
		{
		}

		// Token: 0x06014B4A RID: 84810 RVA: 0x003159CC File Offset: 0x00313BCC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<DataSourceValues>
			{
				InnerText = text
			};
		}

		// Token: 0x06014B4B RID: 84811 RVA: 0x003159E7 File Offset: 0x00313BE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceType>(deep);
		}

		// Token: 0x04008F92 RID: 36754
		private const string tagName = "SourceType";

		// Token: 0x04008F93 RID: 36755
		private const byte tagNsId = 9;

		// Token: 0x04008F94 RID: 36756
		internal const int ElementTypeIdConst = 10818;
	}
}
