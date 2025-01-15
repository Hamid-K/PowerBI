using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CA RID: 9418
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BevelTop : BevelType
	{
		// Token: 0x170052F2 RID: 21234
		// (get) Token: 0x0601178F RID: 71567 RVA: 0x002EED48 File Offset: 0x002ECF48
		public override string LocalName
		{
			get
			{
				return "bevelT";
			}
		}

		// Token: 0x170052F3 RID: 21235
		// (get) Token: 0x06011790 RID: 71568 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052F4 RID: 21236
		// (get) Token: 0x06011791 RID: 71569 RVA: 0x002EED4F File Offset: 0x002ECF4F
		internal override int ElementTypeId
		{
			get
			{
				return 12889;
			}
		}

		// Token: 0x06011792 RID: 71570 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011794 RID: 71572 RVA: 0x002EED5E File Offset: 0x002ECF5E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BevelTop>(deep);
		}

		// Token: 0x040079FD RID: 31229
		private const string tagName = "bevelT";

		// Token: 0x040079FE RID: 31230
		private const byte tagNsId = 52;

		// Token: 0x040079FF RID: 31231
		internal const int ElementTypeIdConst = 12889;
	}
}
