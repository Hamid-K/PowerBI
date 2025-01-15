using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024EC RID: 9452
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PercentageHeight : OpenXmlLeafTextElement
	{
		// Token: 0x17005346 RID: 21318
		// (get) Token: 0x06011855 RID: 71765 RVA: 0x002EF530 File Offset: 0x002ED730
		public override string LocalName
		{
			get
			{
				return "pctHeight";
			}
		}

		// Token: 0x17005347 RID: 21319
		// (get) Token: 0x06011856 RID: 71766 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x17005348 RID: 21320
		// (get) Token: 0x06011857 RID: 71767 RVA: 0x002EF537 File Offset: 0x002ED737
		internal override int ElementTypeId
		{
			get
			{
				return 12827;
			}
		}

		// Token: 0x06011858 RID: 71768 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011859 RID: 71769 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PercentageHeight()
		{
		}

		// Token: 0x0601185A RID: 71770 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PercentageHeight(string text)
			: base(text)
		{
		}

		// Token: 0x0601185B RID: 71771 RVA: 0x002EF540 File Offset: 0x002ED740
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x0601185C RID: 71772 RVA: 0x002EF55B File Offset: 0x002ED75B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PercentageHeight>(deep);
		}

		// Token: 0x04007B19 RID: 31513
		private const string tagName = "pctHeight";

		// Token: 0x04007B1A RID: 31514
		private const byte tagNsId = 51;

		// Token: 0x04007B1B RID: 31515
		internal const int ElementTypeIdConst = 12827;
	}
}
