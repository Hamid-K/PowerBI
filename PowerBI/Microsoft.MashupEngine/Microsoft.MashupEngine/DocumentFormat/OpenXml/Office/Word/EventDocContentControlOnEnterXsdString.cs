using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002473 RID: 9331
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocContentControlOnEnterXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x1700510C RID: 20748
		// (get) Token: 0x06011352 RID: 70482 RVA: 0x002EBA58 File Offset: 0x002E9C58
		public override string LocalName
		{
			get
			{
				return "eventDocContentControlOnEnter";
			}
		}

		// Token: 0x1700510D RID: 20749
		// (get) Token: 0x06011353 RID: 70483 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700510E RID: 20750
		// (get) Token: 0x06011354 RID: 70484 RVA: 0x002EBA5F File Offset: 0x002E9C5F
		internal override int ElementTypeId
		{
			get
			{
				return 12558;
			}
		}

		// Token: 0x06011355 RID: 70485 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011356 RID: 70486 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocContentControlOnEnterXsdString()
		{
		}

		// Token: 0x06011357 RID: 70487 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocContentControlOnEnterXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011358 RID: 70488 RVA: 0x002EBA68 File Offset: 0x002E9C68
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011359 RID: 70489 RVA: 0x002EBA83 File Offset: 0x002E9C83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocContentControlOnEnterXsdString>(deep);
		}

		// Token: 0x040078AD RID: 30893
		private const string tagName = "eventDocContentControlOnEnter";

		// Token: 0x040078AE RID: 30894
		private const byte tagNsId = 33;

		// Token: 0x040078AF RID: 30895
		internal const int ElementTypeIdConst = 12558;
	}
}
