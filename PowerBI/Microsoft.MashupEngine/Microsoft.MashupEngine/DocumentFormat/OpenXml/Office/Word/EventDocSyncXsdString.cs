using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246D RID: 9325
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocSyncXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x170050FA RID: 20730
		// (get) Token: 0x06011322 RID: 70434 RVA: 0x002EB920 File Offset: 0x002E9B20
		public override string LocalName
		{
			get
			{
				return "eventDocSync";
			}
		}

		// Token: 0x170050FB RID: 20731
		// (get) Token: 0x06011323 RID: 70435 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050FC RID: 20732
		// (get) Token: 0x06011324 RID: 70436 RVA: 0x002EB927 File Offset: 0x002E9B27
		internal override int ElementTypeId
		{
			get
			{
				return 12552;
			}
		}

		// Token: 0x06011325 RID: 70437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011326 RID: 70438 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocSyncXsdString()
		{
		}

		// Token: 0x06011327 RID: 70439 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocSyncXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011328 RID: 70440 RVA: 0x002EB930 File Offset: 0x002E9B30
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011329 RID: 70441 RVA: 0x002EB94B File Offset: 0x002E9B4B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocSyncXsdString>(deep);
		}

		// Token: 0x0400789B RID: 30875
		private const string tagName = "eventDocSync";

		// Token: 0x0400789C RID: 30876
		private const byte tagNsId = 33;

		// Token: 0x0400789D RID: 30877
		internal const int ElementTypeIdConst = 12552;
	}
}
