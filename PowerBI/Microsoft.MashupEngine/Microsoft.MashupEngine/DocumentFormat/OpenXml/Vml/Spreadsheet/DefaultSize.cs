using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BE RID: 8638
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultSize : OpenXmlLeafTextElement
	{
		// Token: 0x17003747 RID: 14151
		// (get) Token: 0x0600DBA2 RID: 56226 RVA: 0x002BC740 File Offset: 0x002BA940
		public override string LocalName
		{
			get
			{
				return "DefaultSize";
			}
		}

		// Token: 0x17003748 RID: 14152
		// (get) Token: 0x0600DBA3 RID: 56227 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003749 RID: 14153
		// (get) Token: 0x0600DBA4 RID: 56228 RVA: 0x002BC747 File Offset: 0x002BA947
		internal override int ElementTypeId
		{
			get
			{
				return 12441;
			}
		}

		// Token: 0x0600DBA5 RID: 56229 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBA6 RID: 56230 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DefaultSize()
		{
		}

		// Token: 0x0600DBA7 RID: 56231 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DefaultSize(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBA8 RID: 56232 RVA: 0x002BC750 File Offset: 0x002BA950
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBA9 RID: 56233 RVA: 0x002BC76B File Offset: 0x002BA96B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultSize>(deep);
		}

		// Token: 0x04006C6E RID: 27758
		private const string tagName = "DefaultSize";

		// Token: 0x04006C6F RID: 27759
		private const byte tagNsId = 29;

		// Token: 0x04006C70 RID: 27760
		internal const int ElementTypeIdConst = 12441;
	}
}
