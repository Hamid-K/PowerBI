using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002889 RID: 10377
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnId : OpenXmlLeafTextElement
	{
		// Token: 0x17006786 RID: 26502
		// (get) Token: 0x060145E5 RID: 83429 RVA: 0x002E35A2 File Offset: 0x002E17A2
		public override string LocalName
		{
			get
			{
				return "col";
			}
		}

		// Token: 0x17006787 RID: 26503
		// (get) Token: 0x060145E6 RID: 83430 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006788 RID: 26504
		// (get) Token: 0x060145E7 RID: 83431 RVA: 0x00312967 File Offset: 0x00310B67
		internal override int ElementTypeId
		{
			get
			{
				return 10739;
			}
		}

		// Token: 0x060145E8 RID: 83432 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060145E9 RID: 83433 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ColumnId()
		{
		}

		// Token: 0x060145EA RID: 83434 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ColumnId(string text)
			: base(text)
		{
		}

		// Token: 0x060145EB RID: 83435 RVA: 0x00312970 File Offset: 0x00310B70
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x060145EC RID: 83436 RVA: 0x0031298B File Offset: 0x00310B8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnId>(deep);
		}

		// Token: 0x04008DC6 RID: 36294
		private const string tagName = "col";

		// Token: 0x04008DC7 RID: 36295
		private const byte tagNsId = 18;

		// Token: 0x04008DC8 RID: 36296
		internal const int ElementTypeIdConst = 10739;
	}
}
