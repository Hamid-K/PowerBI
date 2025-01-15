using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002317 RID: 8983
	[ChildElementInfo(typeof(DropZoneFilter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropZoneCategories), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropZoneData), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropZoneSeries), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DropZonesVisible), FileFormatVersions.Office2010)]
	internal class PivotOptions : OpenXmlCompositeElement
	{
		// Token: 0x17004826 RID: 18470
		// (get) Token: 0x0600FF94 RID: 65428 RVA: 0x002DE0BD File Offset: 0x002DC2BD
		public override string LocalName
		{
			get
			{
				return "pivotOptions";
			}
		}

		// Token: 0x17004827 RID: 18471
		// (get) Token: 0x0600FF95 RID: 65429 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004828 RID: 18472
		// (get) Token: 0x0600FF96 RID: 65430 RVA: 0x002DE0C8 File Offset: 0x002DC2C8
		internal override int ElementTypeId
		{
			get
			{
				return 12691;
			}
		}

		// Token: 0x0600FF97 RID: 65431 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FF98 RID: 65432 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotOptions()
		{
		}

		// Token: 0x0600FF99 RID: 65433 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotOptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF9A RID: 65434 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotOptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF9B RID: 65435 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotOptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF9C RID: 65436 RVA: 0x002DE0D0 File Offset: 0x002DC2D0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "dropZoneFilter" == name)
			{
				return new DropZoneFilter();
			}
			if (46 == namespaceId && "dropZoneCategories" == name)
			{
				return new DropZoneCategories();
			}
			if (46 == namespaceId && "dropZoneData" == name)
			{
				return new DropZoneData();
			}
			if (46 == namespaceId && "dropZoneSeries" == name)
			{
				return new DropZoneSeries();
			}
			if (46 == namespaceId && "dropZonesVisible" == name)
			{
				return new DropZonesVisible();
			}
			return null;
		}

		// Token: 0x17004829 RID: 18473
		// (get) Token: 0x0600FF9D RID: 65437 RVA: 0x002DE156 File Offset: 0x002DC356
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotOptions.eleTagNames;
			}
		}

		// Token: 0x1700482A RID: 18474
		// (get) Token: 0x0600FF9E RID: 65438 RVA: 0x002DE15D File Offset: 0x002DC35D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotOptions.eleNamespaceIds;
			}
		}

		// Token: 0x1700482B RID: 18475
		// (get) Token: 0x0600FF9F RID: 65439 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700482C RID: 18476
		// (get) Token: 0x0600FFA0 RID: 65440 RVA: 0x002DE164 File Offset: 0x002DC364
		// (set) Token: 0x0600FFA1 RID: 65441 RVA: 0x002DE16D File Offset: 0x002DC36D
		public DropZoneFilter DropZoneFilter
		{
			get
			{
				return base.GetElement<DropZoneFilter>(0);
			}
			set
			{
				base.SetElement<DropZoneFilter>(0, value);
			}
		}

		// Token: 0x1700482D RID: 18477
		// (get) Token: 0x0600FFA2 RID: 65442 RVA: 0x002DE177 File Offset: 0x002DC377
		// (set) Token: 0x0600FFA3 RID: 65443 RVA: 0x002DE180 File Offset: 0x002DC380
		public DropZoneCategories DropZoneCategories
		{
			get
			{
				return base.GetElement<DropZoneCategories>(1);
			}
			set
			{
				base.SetElement<DropZoneCategories>(1, value);
			}
		}

		// Token: 0x1700482E RID: 18478
		// (get) Token: 0x0600FFA4 RID: 65444 RVA: 0x002DE18A File Offset: 0x002DC38A
		// (set) Token: 0x0600FFA5 RID: 65445 RVA: 0x002DE193 File Offset: 0x002DC393
		public DropZoneData DropZoneData
		{
			get
			{
				return base.GetElement<DropZoneData>(2);
			}
			set
			{
				base.SetElement<DropZoneData>(2, value);
			}
		}

		// Token: 0x1700482F RID: 18479
		// (get) Token: 0x0600FFA6 RID: 65446 RVA: 0x002DE19D File Offset: 0x002DC39D
		// (set) Token: 0x0600FFA7 RID: 65447 RVA: 0x002DE1A6 File Offset: 0x002DC3A6
		public DropZoneSeries DropZoneSeries
		{
			get
			{
				return base.GetElement<DropZoneSeries>(3);
			}
			set
			{
				base.SetElement<DropZoneSeries>(3, value);
			}
		}

		// Token: 0x17004830 RID: 18480
		// (get) Token: 0x0600FFA8 RID: 65448 RVA: 0x002DE1B0 File Offset: 0x002DC3B0
		// (set) Token: 0x0600FFA9 RID: 65449 RVA: 0x002DE1B9 File Offset: 0x002DC3B9
		public DropZonesVisible DropZonesVisible
		{
			get
			{
				return base.GetElement<DropZonesVisible>(4);
			}
			set
			{
				base.SetElement<DropZonesVisible>(4, value);
			}
		}

		// Token: 0x0600FFAA RID: 65450 RVA: 0x002DE1C3 File Offset: 0x002DC3C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotOptions>(deep);
		}

		// Token: 0x0400727C RID: 29308
		private const string tagName = "pivotOptions";

		// Token: 0x0400727D RID: 29309
		private const byte tagNsId = 46;

		// Token: 0x0400727E RID: 29310
		internal const int ElementTypeIdConst = 12691;

		// Token: 0x0400727F RID: 29311
		private static readonly string[] eleTagNames = new string[] { "dropZoneFilter", "dropZoneCategories", "dropZoneData", "dropZoneSeries", "dropZonesVisible" };

		// Token: 0x04007280 RID: 29312
		private static readonly byte[] eleNamespaceIds = new byte[] { 46, 46, 46, 46, 46 };
	}
}
