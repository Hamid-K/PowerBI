using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A0 RID: 9120
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class HoneycombTransition : EmptyType
	{
		// Token: 0x17004C03 RID: 19459
		// (get) Token: 0x06010805 RID: 67589 RVA: 0x002E4253 File Offset: 0x002E2453
		public override string LocalName
		{
			get
			{
				return "honeycomb";
			}
		}

		// Token: 0x17004C04 RID: 19460
		// (get) Token: 0x06010806 RID: 67590 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C05 RID: 19461
		// (get) Token: 0x06010807 RID: 67591 RVA: 0x002E425A File Offset: 0x002E245A
		internal override int ElementTypeId
		{
			get
			{
				return 12772;
			}
		}

		// Token: 0x06010808 RID: 67592 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601080A RID: 67594 RVA: 0x002E4269 File Offset: 0x002E2469
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HoneycombTransition>(deep);
		}

		// Token: 0x040074EB RID: 29931
		private const string tagName = "honeycomb";

		// Token: 0x040074EC RID: 29932
		private const byte tagNsId = 49;

		// Token: 0x040074ED RID: 29933
		internal const int ElementTypeIdConst = 12772;
	}
}
