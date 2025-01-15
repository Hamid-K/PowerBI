using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251D RID: 9501
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayEquation : BooleanType
	{
		// Token: 0x17005470 RID: 21616
		// (get) Token: 0x06011ADB RID: 72411 RVA: 0x002F13C4 File Offset: 0x002EF5C4
		public override string LocalName
		{
			get
			{
				return "dispEq";
			}
		}

		// Token: 0x17005471 RID: 21617
		// (get) Token: 0x06011ADC RID: 72412 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005472 RID: 21618
		// (get) Token: 0x06011ADD RID: 72413 RVA: 0x002F13CB File Offset: 0x002EF5CB
		internal override int ElementTypeId
		{
			get
			{
				return 10444;
			}
		}

		// Token: 0x06011ADE RID: 72414 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AE0 RID: 72416 RVA: 0x002F13D2 File Offset: 0x002EF5D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayEquation>(deep);
		}

		// Token: 0x04007BEB RID: 31723
		private const string tagName = "dispEq";

		// Token: 0x04007BEC RID: 31724
		private const byte tagNsId = 11;

		// Token: 0x04007BED RID: 31725
		internal const int ElementTypeIdConst = 10444;
	}
}
