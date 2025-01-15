using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002511 RID: 9489
	[GeneratedCode("DomGen", "2.0")]
	internal class VaryColors : BooleanType
	{
		// Token: 0x1700544C RID: 21580
		// (get) Token: 0x06011A93 RID: 72339 RVA: 0x002F12B0 File Offset: 0x002EF4B0
		public override string LocalName
		{
			get
			{
				return "varyColors";
			}
		}

		// Token: 0x1700544D RID: 21581
		// (get) Token: 0x06011A94 RID: 72340 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700544E RID: 21582
		// (get) Token: 0x06011A95 RID: 72341 RVA: 0x002F12B7 File Offset: 0x002EF4B7
		internal override int ElementTypeId
		{
			get
			{
				return 10361;
			}
		}

		// Token: 0x06011A96 RID: 72342 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A98 RID: 72344 RVA: 0x002F12BE File Offset: 0x002EF4BE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VaryColors>(deep);
		}

		// Token: 0x04007BC7 RID: 31687
		private const string tagName = "varyColors";

		// Token: 0x04007BC8 RID: 31688
		private const byte tagNsId = 11;

		// Token: 0x04007BC9 RID: 31689
		internal const int ElementTypeIdConst = 10361;
	}
}
