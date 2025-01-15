using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002438 RID: 9272
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OlapSlicerCache), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TabularSlicerCache), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerCacheData : OpenXmlCompositeElement
	{
		// Token: 0x1700502A RID: 20522
		// (get) Token: 0x06011141 RID: 69953 RVA: 0x002958E1 File Offset: 0x00293AE1
		public override string LocalName
		{
			get
			{
				return "data";
			}
		}

		// Token: 0x1700502B RID: 20523
		// (get) Token: 0x06011142 RID: 69954 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700502C RID: 20524
		// (get) Token: 0x06011143 RID: 69955 RVA: 0x002EA5D7 File Offset: 0x002E87D7
		internal override int ElementTypeId
		{
			get
			{
				return 12996;
			}
		}

		// Token: 0x06011144 RID: 69956 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011145 RID: 69957 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerCacheData()
		{
		}

		// Token: 0x06011146 RID: 69958 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerCacheData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011147 RID: 69959 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerCacheData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011148 RID: 69960 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerCacheData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011149 RID: 69961 RVA: 0x002EA5DE File Offset: 0x002E87DE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "olap" == name)
			{
				return new OlapSlicerCache();
			}
			if (53 == namespaceId && "tabular" == name)
			{
				return new TabularSlicerCache();
			}
			return null;
		}

		// Token: 0x1700502D RID: 20525
		// (get) Token: 0x0601114A RID: 69962 RVA: 0x002EA611 File Offset: 0x002E8811
		internal override string[] ElementTagNames
		{
			get
			{
				return SlicerCacheData.eleTagNames;
			}
		}

		// Token: 0x1700502E RID: 20526
		// (get) Token: 0x0601114B RID: 69963 RVA: 0x002EA618 File Offset: 0x002E8818
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlicerCacheData.eleNamespaceIds;
			}
		}

		// Token: 0x1700502F RID: 20527
		// (get) Token: 0x0601114C RID: 69964 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005030 RID: 20528
		// (get) Token: 0x0601114D RID: 69965 RVA: 0x002EA61F File Offset: 0x002E881F
		// (set) Token: 0x0601114E RID: 69966 RVA: 0x002EA628 File Offset: 0x002E8828
		public OlapSlicerCache OlapSlicerCache
		{
			get
			{
				return base.GetElement<OlapSlicerCache>(0);
			}
			set
			{
				base.SetElement<OlapSlicerCache>(0, value);
			}
		}

		// Token: 0x17005031 RID: 20529
		// (get) Token: 0x0601114F RID: 69967 RVA: 0x002EA632 File Offset: 0x002E8832
		// (set) Token: 0x06011150 RID: 69968 RVA: 0x002EA63B File Offset: 0x002E883B
		public TabularSlicerCache TabularSlicerCache
		{
			get
			{
				return base.GetElement<TabularSlicerCache>(1);
			}
			set
			{
				base.SetElement<TabularSlicerCache>(1, value);
			}
		}

		// Token: 0x06011151 RID: 69969 RVA: 0x002EA645 File Offset: 0x002E8845
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCacheData>(deep);
		}

		// Token: 0x0400778D RID: 30605
		private const string tagName = "data";

		// Token: 0x0400778E RID: 30606
		private const byte tagNsId = 53;

		// Token: 0x0400778F RID: 30607
		internal const int ElementTypeIdConst = 12996;

		// Token: 0x04007790 RID: 30608
		private static readonly string[] eleTagNames = new string[] { "olap", "tabular" };

		// Token: 0x04007791 RID: 30609
		private static readonly byte[] eleNamespaceIds = new byte[] { 53, 53 };
	}
}
