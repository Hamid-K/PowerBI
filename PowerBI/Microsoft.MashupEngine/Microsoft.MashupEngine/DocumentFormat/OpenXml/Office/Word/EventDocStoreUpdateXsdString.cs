using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002474 RID: 9332
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocStoreUpdateXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x1700510F RID: 20751
		// (get) Token: 0x0601135A RID: 70490 RVA: 0x002EBA8C File Offset: 0x002E9C8C
		public override string LocalName
		{
			get
			{
				return "eventDocStoreUpdate";
			}
		}

		// Token: 0x17005110 RID: 20752
		// (get) Token: 0x0601135B RID: 70491 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005111 RID: 20753
		// (get) Token: 0x0601135C RID: 70492 RVA: 0x002EBA93 File Offset: 0x002E9C93
		internal override int ElementTypeId
		{
			get
			{
				return 12559;
			}
		}

		// Token: 0x0601135D RID: 70493 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601135E RID: 70494 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocStoreUpdateXsdString()
		{
		}

		// Token: 0x0601135F RID: 70495 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocStoreUpdateXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011360 RID: 70496 RVA: 0x002EBA9C File Offset: 0x002E9C9C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011361 RID: 70497 RVA: 0x002EBAB7 File Offset: 0x002E9CB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocStoreUpdateXsdString>(deep);
		}

		// Token: 0x040078B0 RID: 30896
		private const string tagName = "eventDocStoreUpdate";

		// Token: 0x040078B1 RID: 30897
		private const byte tagNsId = 33;

		// Token: 0x040078B2 RID: 30898
		internal const int ElementTypeIdConst = 12559;
	}
}
