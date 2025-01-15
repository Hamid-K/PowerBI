using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288C RID: 10380
	[GeneratedCode("DomGen", "2.0")]
	internal class RowId : OpenXmlLeafTextElement
	{
		// Token: 0x1700678F RID: 26511
		// (get) Token: 0x060145FD RID: 83453 RVA: 0x002E3583 File Offset: 0x002E1783
		public override string LocalName
		{
			get
			{
				return "row";
			}
		}

		// Token: 0x17006790 RID: 26512
		// (get) Token: 0x060145FE RID: 83454 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006791 RID: 26513
		// (get) Token: 0x060145FF RID: 83455 RVA: 0x003129FC File Offset: 0x00310BFC
		internal override int ElementTypeId
		{
			get
			{
				return 10741;
			}
		}

		// Token: 0x06014600 RID: 83456 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014601 RID: 83457 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public RowId()
		{
		}

		// Token: 0x06014602 RID: 83458 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public RowId(string text)
			: base(text)
		{
		}

		// Token: 0x06014603 RID: 83459 RVA: 0x00312A04 File Offset: 0x00310C04
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014604 RID: 83460 RVA: 0x00312A1F File Offset: 0x00310C1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowId>(deep);
		}

		// Token: 0x04008DCF RID: 36303
		private const string tagName = "row";

		// Token: 0x04008DD0 RID: 36304
		private const byte tagNsId = 18;

		// Token: 0x04008DD1 RID: 36305
		internal const int ElementTypeIdConst = 10741;
	}
}
