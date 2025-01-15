using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002404 RID: 9220
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class LowMarkerColor : ColorType
	{
		// Token: 0x17004EB4 RID: 20148
		// (get) Token: 0x06010DF1 RID: 69105 RVA: 0x002E8468 File Offset: 0x002E6668
		public override string LocalName
		{
			get
			{
				return "colorLow";
			}
		}

		// Token: 0x17004EB5 RID: 20149
		// (get) Token: 0x06010DF2 RID: 69106 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EB6 RID: 20150
		// (get) Token: 0x06010DF3 RID: 69107 RVA: 0x002E846F File Offset: 0x002E666F
		internal override int ElementTypeId
		{
			get
			{
				return 12944;
			}
		}

		// Token: 0x06010DF4 RID: 69108 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DF6 RID: 69110 RVA: 0x002E8476 File Offset: 0x002E6676
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LowMarkerColor>(deep);
		}

		// Token: 0x0400769B RID: 30363
		private const string tagName = "colorLow";

		// Token: 0x0400769C RID: 30364
		private const byte tagNsId = 53;

		// Token: 0x0400769D RID: 30365
		internal const int ElementTypeIdConst = 12944;
	}
}
