using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FF RID: 9215
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class AxisColor : ColorType
	{
		// Token: 0x17004EA5 RID: 20133
		// (get) Token: 0x06010DD3 RID: 69075 RVA: 0x002E83F5 File Offset: 0x002E65F5
		public override string LocalName
		{
			get
			{
				return "colorAxis";
			}
		}

		// Token: 0x17004EA6 RID: 20134
		// (get) Token: 0x06010DD4 RID: 69076 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EA7 RID: 20135
		// (get) Token: 0x06010DD5 RID: 69077 RVA: 0x002E83FC File Offset: 0x002E65FC
		internal override int ElementTypeId
		{
			get
			{
				return 12939;
			}
		}

		// Token: 0x06010DD6 RID: 69078 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DD8 RID: 69080 RVA: 0x002E8403 File Offset: 0x002E6603
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AxisColor>(deep);
		}

		// Token: 0x0400768C RID: 30348
		private const string tagName = "colorAxis";

		// Token: 0x0400768D RID: 30349
		private const byte tagNsId = 53;

		// Token: 0x0400768E RID: 30350
		internal const int ElementTypeIdConst = 12939;
	}
}
