using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023EA RID: 9194
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Id : OpenXmlLeafTextElement
	{
		// Token: 0x17004DC9 RID: 19913
		// (get) Token: 0x06010BEA RID: 68586 RVA: 0x002E6A3B File Offset: 0x002E4C3B
		public override string LocalName
		{
			get
			{
				return "id";
			}
		}

		// Token: 0x17004DCA RID: 19914
		// (get) Token: 0x06010BEB RID: 68587 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DCB RID: 19915
		// (get) Token: 0x06010BEC RID: 68588 RVA: 0x002E6A42 File Offset: 0x002E4C42
		internal override int ElementTypeId
		{
			get
			{
				return 12920;
			}
		}

		// Token: 0x06010BED RID: 68589 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010BEE RID: 68590 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Id()
		{
		}

		// Token: 0x06010BEF RID: 68591 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Id(string text)
			: base(text)
		{
		}

		// Token: 0x06010BF0 RID: 68592 RVA: 0x002E6A4C File Offset: 0x002E4C4C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06010BF1 RID: 68593 RVA: 0x002E6A67 File Offset: 0x002E4C67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Id>(deep);
		}

		// Token: 0x0400762D RID: 30253
		private const string tagName = "id";

		// Token: 0x0400762E RID: 30254
		private const byte tagNsId = 53;

		// Token: 0x0400762F RID: 30255
		internal const int ElementTypeIdConst = 12920;
	}
}
