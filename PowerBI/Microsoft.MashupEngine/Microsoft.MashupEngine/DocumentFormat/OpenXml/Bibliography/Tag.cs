using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E4 RID: 10468
	[GeneratedCode("DomGen", "2.0")]
	internal class Tag : OpenXmlLeafTextElement
	{
		// Token: 0x17006954 RID: 26964
		// (get) Token: 0x06014A14 RID: 84500 RVA: 0x00315124 File Offset: 0x00313324
		public override string LocalName
		{
			get
			{
				return "Tag";
			}
		}

		// Token: 0x17006955 RID: 26965
		// (get) Token: 0x06014A15 RID: 84501 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006956 RID: 26966
		// (get) Token: 0x06014A16 RID: 84502 RVA: 0x0031512B File Offset: 0x0031332B
		internal override int ElementTypeId
		{
			get
			{
				return 10823;
			}
		}

		// Token: 0x06014A17 RID: 84503 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A18 RID: 84504 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Tag()
		{
		}

		// Token: 0x06014A19 RID: 84505 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Tag(string text)
			: base(text)
		{
		}

		// Token: 0x06014A1A RID: 84506 RVA: 0x00315134 File Offset: 0x00313334
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A1B RID: 84507 RVA: 0x0031514F File Offset: 0x0031334F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tag>(deep);
		}

		// Token: 0x04008F38 RID: 36664
		private const string tagName = "Tag";

		// Token: 0x04008F39 RID: 36665
		private const byte tagNsId = 9;

		// Token: 0x04008F3A RID: 36666
		internal const int ElementTypeIdConst = 10823;
	}
}
