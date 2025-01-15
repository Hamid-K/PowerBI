using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002523 RID: 9507
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplyToEnd : BooleanType
	{
		// Token: 0x17005482 RID: 21634
		// (get) Token: 0x06011AFF RID: 72447 RVA: 0x002F144E File Offset: 0x002EF64E
		public override string LocalName
		{
			get
			{
				return "applyToEnd";
			}
		}

		// Token: 0x17005483 RID: 21635
		// (get) Token: 0x06011B00 RID: 72448 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005484 RID: 21636
		// (get) Token: 0x06011B01 RID: 72449 RVA: 0x002F1455 File Offset: 0x002EF655
		internal override int ElementTypeId
		{
			get
			{
				return 10471;
			}
		}

		// Token: 0x06011B02 RID: 72450 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B04 RID: 72452 RVA: 0x002F145C File Offset: 0x002EF65C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplyToEnd>(deep);
		}

		// Token: 0x04007BFD RID: 31741
		private const string tagName = "applyToEnd";

		// Token: 0x04007BFE RID: 31742
		private const byte tagNsId = 11;

		// Token: 0x04007BFF RID: 31743
		internal const int ElementTypeIdConst = 10471;
	}
}
