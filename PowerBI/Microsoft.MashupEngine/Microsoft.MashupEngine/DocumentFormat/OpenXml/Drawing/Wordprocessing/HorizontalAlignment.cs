using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028B0 RID: 10416
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalAlignment : OpenXmlLeafTextElement
	{
		// Token: 0x170068C2 RID: 26818
		// (get) Token: 0x0601488B RID: 84107 RVA: 0x00314659 File Offset: 0x00312859
		public override string LocalName
		{
			get
			{
				return "align";
			}
		}

		// Token: 0x170068C3 RID: 26819
		// (get) Token: 0x0601488C RID: 84108 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170068C4 RID: 26820
		// (get) Token: 0x0601488D RID: 84109 RVA: 0x003146C0 File Offset: 0x003128C0
		internal override int ElementTypeId
		{
			get
			{
				return 10713;
			}
		}

		// Token: 0x0601488E RID: 84110 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601488F RID: 84111 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HorizontalAlignment()
		{
		}

		// Token: 0x06014890 RID: 84112 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HorizontalAlignment(string text)
			: base(text)
		{
		}

		// Token: 0x06014891 RID: 84113 RVA: 0x003146C8 File Offset: 0x003128C8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<HorizontalAlignmentValues>
			{
				InnerText = text
			};
		}

		// Token: 0x06014892 RID: 84114 RVA: 0x003146E3 File Offset: 0x003128E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalAlignment>(deep);
		}

		// Token: 0x04008E86 RID: 36486
		private const string tagName = "align";

		// Token: 0x04008E87 RID: 36487
		private const byte tagNsId = 16;

		// Token: 0x04008E88 RID: 36488
		internal const int ElementTypeIdConst = 10713;
	}
}
