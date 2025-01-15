using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028EC RID: 10476
	[GeneratedCode("DomGen", "2.0")]
	internal class Year : OpenXmlLeafTextElement
	{
		// Token: 0x1700696C RID: 26988
		// (get) Token: 0x06014A54 RID: 84564 RVA: 0x003152BC File Offset: 0x003134BC
		public override string LocalName
		{
			get
			{
				return "Year";
			}
		}

		// Token: 0x1700696D RID: 26989
		// (get) Token: 0x06014A55 RID: 84565 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700696E RID: 26990
		// (get) Token: 0x06014A56 RID: 84566 RVA: 0x003152C3 File Offset: 0x003134C3
		internal override int ElementTypeId
		{
			get
			{
				return 10831;
			}
		}

		// Token: 0x06014A57 RID: 84567 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A58 RID: 84568 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Year()
		{
		}

		// Token: 0x06014A59 RID: 84569 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Year(string text)
			: base(text)
		{
		}

		// Token: 0x06014A5A RID: 84570 RVA: 0x003152CC File Offset: 0x003134CC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A5B RID: 84571 RVA: 0x003152E7 File Offset: 0x003134E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Year>(deep);
		}

		// Token: 0x04008F50 RID: 36688
		private const string tagName = "Year";

		// Token: 0x04008F51 RID: 36689
		private const byte tagNsId = 9;

		// Token: 0x04008F52 RID: 36690
		internal const int ElementTypeIdConst = 10831;
	}
}
