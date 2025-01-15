using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DC RID: 8668
	[GeneratedCode("DomGen", "2.0")]
	internal class Anchor : OpenXmlLeafTextElement
	{
		// Token: 0x170037A1 RID: 14241
		// (get) Token: 0x0600DC92 RID: 56466 RVA: 0x002BCD58 File Offset: 0x002BAF58
		public override string LocalName
		{
			get
			{
				return "Anchor";
			}
		}

		// Token: 0x170037A2 RID: 14242
		// (get) Token: 0x0600DC93 RID: 56467 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037A3 RID: 14243
		// (get) Token: 0x0600DC94 RID: 56468 RVA: 0x002BCD5F File Offset: 0x002BAF5F
		internal override int ElementTypeId
		{
			get
			{
				return 12439;
			}
		}

		// Token: 0x0600DC95 RID: 56469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC96 RID: 56470 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Anchor()
		{
		}

		// Token: 0x0600DC97 RID: 56471 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Anchor(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC98 RID: 56472 RVA: 0x002BCD68 File Offset: 0x002BAF68
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC99 RID: 56473 RVA: 0x002BCD83 File Offset: 0x002BAF83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Anchor>(deep);
		}

		// Token: 0x04006CC8 RID: 27848
		private const string tagName = "Anchor";

		// Token: 0x04006CC9 RID: 27849
		private const byte tagNsId = 29;

		// Token: 0x04006CCA RID: 27850
		internal const int ElementTypeIdConst = 12439;
	}
}
