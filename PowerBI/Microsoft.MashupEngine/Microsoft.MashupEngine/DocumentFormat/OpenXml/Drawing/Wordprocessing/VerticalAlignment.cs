using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AE RID: 10414
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalAlignment : OpenXmlLeafTextElement
	{
		// Token: 0x170068BC RID: 26812
		// (get) Token: 0x0601487B RID: 84091 RVA: 0x00314659 File Offset: 0x00312859
		public override string LocalName
		{
			get
			{
				return "align";
			}
		}

		// Token: 0x170068BD RID: 26813
		// (get) Token: 0x0601487C RID: 84092 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170068BE RID: 26814
		// (get) Token: 0x0601487D RID: 84093 RVA: 0x00314660 File Offset: 0x00312860
		internal override int ElementTypeId
		{
			get
			{
				return 10711;
			}
		}

		// Token: 0x0601487E RID: 84094 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601487F RID: 84095 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VerticalAlignment()
		{
		}

		// Token: 0x06014880 RID: 84096 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VerticalAlignment(string text)
			: base(text)
		{
		}

		// Token: 0x06014881 RID: 84097 RVA: 0x00314668 File Offset: 0x00312868
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<VerticalAlignmentValues>
			{
				InnerText = text
			};
		}

		// Token: 0x06014882 RID: 84098 RVA: 0x00314683 File Offset: 0x00312883
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalAlignment>(deep);
		}

		// Token: 0x04008E80 RID: 36480
		private const string tagName = "align";

		// Token: 0x04008E81 RID: 36481
		private const byte tagNsId = 16;

		// Token: 0x04008E82 RID: 36482
		internal const int ElementTypeIdConst = 10711;
	}
}
