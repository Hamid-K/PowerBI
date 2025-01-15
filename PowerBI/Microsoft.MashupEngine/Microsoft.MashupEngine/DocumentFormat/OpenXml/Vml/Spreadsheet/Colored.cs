using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D2 RID: 8658
	[GeneratedCode("DomGen", "2.0")]
	internal class Colored : OpenXmlLeafTextElement
	{
		// Token: 0x17003783 RID: 14211
		// (get) Token: 0x0600DC42 RID: 56386 RVA: 0x002BCB50 File Offset: 0x002BAD50
		public override string LocalName
		{
			get
			{
				return "Colored";
			}
		}

		// Token: 0x17003784 RID: 14212
		// (get) Token: 0x0600DC43 RID: 56387 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003785 RID: 14213
		// (get) Token: 0x0600DC44 RID: 56388 RVA: 0x002BCB57 File Offset: 0x002BAD57
		internal override int ElementTypeId
		{
			get
			{
				return 12477;
			}
		}

		// Token: 0x0600DC45 RID: 56389 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC46 RID: 56390 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Colored()
		{
		}

		// Token: 0x0600DC47 RID: 56391 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Colored(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC48 RID: 56392 RVA: 0x002BCB60 File Offset: 0x002BAD60
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC49 RID: 56393 RVA: 0x002BCB7B File Offset: 0x002BAD7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Colored>(deep);
		}

		// Token: 0x04006CAA RID: 27818
		private const string tagName = "Colored";

		// Token: 0x04006CAB RID: 27819
		private const byte tagNsId = 29;

		// Token: 0x04006CAC RID: 27820
		internal const int ElementTypeIdConst = 12477;
	}
}
