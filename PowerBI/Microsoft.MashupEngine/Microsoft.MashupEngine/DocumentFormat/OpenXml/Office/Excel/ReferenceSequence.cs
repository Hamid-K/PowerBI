using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x0200237F RID: 9087
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ReferenceSequence : OpenXmlLeafTextElement
	{
		// Token: 0x17004B4A RID: 19274
		// (get) Token: 0x06010663 RID: 67171 RVA: 0x002E3371 File Offset: 0x002E1571
		public override string LocalName
		{
			get
			{
				return "sqref";
			}
		}

		// Token: 0x17004B4B RID: 19275
		// (get) Token: 0x06010664 RID: 67172 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B4C RID: 19276
		// (get) Token: 0x06010665 RID: 67173 RVA: 0x002E3378 File Offset: 0x002E1578
		internal override int ElementTypeId
		{
			get
			{
				return 12531;
			}
		}

		// Token: 0x06010666 RID: 67174 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010667 RID: 67175 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ReferenceSequence()
		{
		}

		// Token: 0x06010668 RID: 67176 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ReferenceSequence(string text)
			: base(text)
		{
		}

		// Token: 0x06010669 RID: 67177 RVA: 0x002E3380 File Offset: 0x002E1580
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new ListValue<StringValue>
			{
				InnerText = text
			};
		}

		// Token: 0x0601066A RID: 67178 RVA: 0x002E339B File Offset: 0x002E159B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ReferenceSequence>(deep);
		}

		// Token: 0x0400746E RID: 29806
		private const string tagName = "sqref";

		// Token: 0x0400746F RID: 29807
		private const byte tagNsId = 32;

		// Token: 0x04007470 RID: 29808
		internal const int ElementTypeIdConst = 12531;
	}
}
