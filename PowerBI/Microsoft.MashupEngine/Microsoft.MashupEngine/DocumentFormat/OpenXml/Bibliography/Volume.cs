using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028EB RID: 10475
	[GeneratedCode("DomGen", "2.0")]
	internal class Volume : OpenXmlLeafTextElement
	{
		// Token: 0x17006969 RID: 26985
		// (get) Token: 0x06014A4C RID: 84556 RVA: 0x00315288 File Offset: 0x00313488
		public override string LocalName
		{
			get
			{
				return "Volume";
			}
		}

		// Token: 0x1700696A RID: 26986
		// (get) Token: 0x06014A4D RID: 84557 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700696B RID: 26987
		// (get) Token: 0x06014A4E RID: 84558 RVA: 0x0031528F File Offset: 0x0031348F
		internal override int ElementTypeId
		{
			get
			{
				return 10830;
			}
		}

		// Token: 0x06014A4F RID: 84559 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A50 RID: 84560 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Volume()
		{
		}

		// Token: 0x06014A51 RID: 84561 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Volume(string text)
			: base(text)
		{
		}

		// Token: 0x06014A52 RID: 84562 RVA: 0x00315298 File Offset: 0x00313498
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A53 RID: 84563 RVA: 0x003152B3 File Offset: 0x003134B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Volume>(deep);
		}

		// Token: 0x04008F4D RID: 36685
		private const string tagName = "Volume";

		// Token: 0x04008F4E RID: 36686
		private const byte tagNsId = 9;

		// Token: 0x04008F4F RID: 36687
		internal const int ElementTypeIdConst = 10830;
	}
}
