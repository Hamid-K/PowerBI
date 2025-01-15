using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DB RID: 9179
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SlicerCache), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerCaches : OpenXmlCompositeElement
	{
		// Token: 0x17004D3E RID: 19774
		// (get) Token: 0x06010AC3 RID: 68291 RVA: 0x002AECF3 File Offset: 0x002ACEF3
		public override string LocalName
		{
			get
			{
				return "slicerCaches";
			}
		}

		// Token: 0x17004D3F RID: 19775
		// (get) Token: 0x06010AC4 RID: 68292 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D40 RID: 19776
		// (get) Token: 0x06010AC5 RID: 68293 RVA: 0x002E5D3B File Offset: 0x002E3F3B
		internal override int ElementTypeId
		{
			get
			{
				return 12905;
			}
		}

		// Token: 0x06010AC6 RID: 68294 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010AC7 RID: 68295 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerCaches()
		{
		}

		// Token: 0x06010AC8 RID: 68296 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerCaches(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AC9 RID: 68297 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerCaches(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010ACA RID: 68298 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerCaches(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010ACB RID: 68299 RVA: 0x002E5D42 File Offset: 0x002E3F42
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicerCache" == name)
			{
				return new SlicerCache();
			}
			return null;
		}

		// Token: 0x06010ACC RID: 68300 RVA: 0x002E5D5D File Offset: 0x002E3F5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCaches>(deep);
		}

		// Token: 0x040075DA RID: 30170
		private const string tagName = "slicerCaches";

		// Token: 0x040075DB RID: 30171
		private const byte tagNsId = 53;

		// Token: 0x040075DC RID: 30172
		internal const int ElementTypeIdConst = 12905;
	}
}
