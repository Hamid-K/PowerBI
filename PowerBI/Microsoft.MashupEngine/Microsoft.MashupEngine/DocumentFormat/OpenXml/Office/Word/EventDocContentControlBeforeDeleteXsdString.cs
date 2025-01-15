using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002471 RID: 9329
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocContentControlBeforeDeleteXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005106 RID: 20742
		// (get) Token: 0x06011342 RID: 70466 RVA: 0x002EB9F0 File Offset: 0x002E9BF0
		public override string LocalName
		{
			get
			{
				return "eventDocContentControlBeforeDelete";
			}
		}

		// Token: 0x17005107 RID: 20743
		// (get) Token: 0x06011343 RID: 70467 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005108 RID: 20744
		// (get) Token: 0x06011344 RID: 70468 RVA: 0x002EB9F7 File Offset: 0x002E9BF7
		internal override int ElementTypeId
		{
			get
			{
				return 12556;
			}
		}

		// Token: 0x06011345 RID: 70469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011346 RID: 70470 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocContentControlBeforeDeleteXsdString()
		{
		}

		// Token: 0x06011347 RID: 70471 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocContentControlBeforeDeleteXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011348 RID: 70472 RVA: 0x002EBA00 File Offset: 0x002E9C00
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011349 RID: 70473 RVA: 0x002EBA1B File Offset: 0x002E9C1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocContentControlBeforeDeleteXsdString>(deep);
		}

		// Token: 0x040078A7 RID: 30887
		private const string tagName = "eventDocContentControlBeforeDelete";

		// Token: 0x040078A8 RID: 30888
		private const byte tagNsId = 33;

		// Token: 0x040078A9 RID: 30889
		internal const int ElementTypeIdConst = 12556;
	}
}
