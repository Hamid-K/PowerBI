using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E1 RID: 8673
	[GeneratedCode("DomGen", "2.0")]
	internal class MultiSelections : OpenXmlLeafTextElement
	{
		// Token: 0x170037B0 RID: 14256
		// (get) Token: 0x0600DCBA RID: 56506 RVA: 0x002BCE5C File Offset: 0x002BB05C
		public override string LocalName
		{
			get
			{
				return "MultiSel";
			}
		}

		// Token: 0x170037B1 RID: 14257
		// (get) Token: 0x0600DCBB RID: 56507 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037B2 RID: 14258
		// (get) Token: 0x0600DCBC RID: 56508 RVA: 0x002BCE63 File Offset: 0x002BB063
		internal override int ElementTypeId
		{
			get
			{
				return 12473;
			}
		}

		// Token: 0x0600DCBD RID: 56509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCBE RID: 56510 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MultiSelections()
		{
		}

		// Token: 0x0600DCBF RID: 56511 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MultiSelections(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCC0 RID: 56512 RVA: 0x002BCE6C File Offset: 0x002BB06C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCC1 RID: 56513 RVA: 0x002BCE87 File Offset: 0x002BB087
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultiSelections>(deep);
		}

		// Token: 0x04006CD7 RID: 27863
		private const string tagName = "MultiSel";

		// Token: 0x04006CD8 RID: 27864
		private const byte tagNsId = 29;

		// Token: 0x04006CD9 RID: 27865
		internal const int ElementTypeIdConst = 12473;
	}
}
