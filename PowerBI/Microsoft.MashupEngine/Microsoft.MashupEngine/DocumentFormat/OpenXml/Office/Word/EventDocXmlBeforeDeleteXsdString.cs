using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200246F RID: 9327
	[GeneratedCode("DomGen", "2.0")]
	internal class EventDocXmlBeforeDeleteXsdString : OpenXmlLeafTextElement
	{
		// Token: 0x17005100 RID: 20736
		// (get) Token: 0x06011332 RID: 70450 RVA: 0x002EB988 File Offset: 0x002E9B88
		public override string LocalName
		{
			get
			{
				return "eventDocXmlBeforeDelete";
			}
		}

		// Token: 0x17005101 RID: 20737
		// (get) Token: 0x06011333 RID: 70451 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005102 RID: 20738
		// (get) Token: 0x06011334 RID: 70452 RVA: 0x002EB98F File Offset: 0x002E9B8F
		internal override int ElementTypeId
		{
			get
			{
				return 12554;
			}
		}

		// Token: 0x06011335 RID: 70453 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011336 RID: 70454 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EventDocXmlBeforeDeleteXsdString()
		{
		}

		// Token: 0x06011337 RID: 70455 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EventDocXmlBeforeDeleteXsdString(string text)
			: base(text)
		{
		}

		// Token: 0x06011338 RID: 70456 RVA: 0x002EB998 File Offset: 0x002E9B98
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011339 RID: 70457 RVA: 0x002EB9B3 File Offset: 0x002E9BB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EventDocXmlBeforeDeleteXsdString>(deep);
		}

		// Token: 0x040078A1 RID: 30881
		private const string tagName = "eventDocXmlBeforeDelete";

		// Token: 0x040078A2 RID: 30882
		private const byte tagNsId = 33;

		// Token: 0x040078A3 RID: 30883
		internal const int ElementTypeIdConst = 12554;
	}
}
