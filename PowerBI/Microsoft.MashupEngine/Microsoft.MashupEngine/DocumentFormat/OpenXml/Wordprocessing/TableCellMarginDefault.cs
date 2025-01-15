using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0B RID: 12043
	[ChildElementInfo(typeof(TopMargin))]
	[ChildElementInfo(typeof(StartMargin), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EndMargin), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TableCellLeftMargin))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BottomMargin))]
	[ChildElementInfo(typeof(TableCellRightMargin))]
	internal class TableCellMarginDefault : OpenXmlCompositeElement
	{
		// Token: 0x17008E0C RID: 36364
		// (get) Token: 0x06019B6D RID: 105325 RVA: 0x00354370 File Offset: 0x00352570
		public override string LocalName
		{
			get
			{
				return "tblCellMar";
			}
		}

		// Token: 0x17008E0D RID: 36365
		// (get) Token: 0x06019B6E RID: 105326 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E0E RID: 36366
		// (get) Token: 0x06019B6F RID: 105327 RVA: 0x00354377 File Offset: 0x00352577
		internal override int ElementTypeId
		{
			get
			{
				return 11681;
			}
		}

		// Token: 0x06019B70 RID: 105328 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019B71 RID: 105329 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellMarginDefault()
		{
		}

		// Token: 0x06019B72 RID: 105330 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellMarginDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019B73 RID: 105331 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellMarginDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019B74 RID: 105332 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellMarginDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019B75 RID: 105333 RVA: 0x00354380 File Offset: 0x00352580
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopMargin();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new TableCellLeftMargin();
			}
			if (23 == namespaceId && "start" == name)
			{
				return new StartMargin();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new BottomMargin();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new TableCellRightMargin();
			}
			if (23 == namespaceId && "end" == name)
			{
				return new EndMargin();
			}
			return null;
		}

		// Token: 0x17008E0F RID: 36367
		// (get) Token: 0x06019B76 RID: 105334 RVA: 0x0035441E File Offset: 0x0035261E
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellMarginDefault.eleTagNames;
			}
		}

		// Token: 0x17008E10 RID: 36368
		// (get) Token: 0x06019B77 RID: 105335 RVA: 0x00354425 File Offset: 0x00352625
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellMarginDefault.eleNamespaceIds;
			}
		}

		// Token: 0x17008E11 RID: 36369
		// (get) Token: 0x06019B78 RID: 105336 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E12 RID: 36370
		// (get) Token: 0x06019B79 RID: 105337 RVA: 0x00353AE8 File Offset: 0x00351CE8
		// (set) Token: 0x06019B7A RID: 105338 RVA: 0x00353AF1 File Offset: 0x00351CF1
		public TopMargin TopMargin
		{
			get
			{
				return base.GetElement<TopMargin>(0);
			}
			set
			{
				base.SetElement<TopMargin>(0, value);
			}
		}

		// Token: 0x17008E13 RID: 36371
		// (get) Token: 0x06019B7B RID: 105339 RVA: 0x0035442C File Offset: 0x0035262C
		// (set) Token: 0x06019B7C RID: 105340 RVA: 0x00354435 File Offset: 0x00352635
		public TableCellLeftMargin TableCellLeftMargin
		{
			get
			{
				return base.GetElement<TableCellLeftMargin>(1);
			}
			set
			{
				base.SetElement<TableCellLeftMargin>(1, value);
			}
		}

		// Token: 0x17008E14 RID: 36372
		// (get) Token: 0x06019B7D RID: 105341 RVA: 0x00353B0E File Offset: 0x00351D0E
		// (set) Token: 0x06019B7E RID: 105342 RVA: 0x00353B17 File Offset: 0x00351D17
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StartMargin StartMargin
		{
			get
			{
				return base.GetElement<StartMargin>(2);
			}
			set
			{
				base.SetElement<StartMargin>(2, value);
			}
		}

		// Token: 0x17008E15 RID: 36373
		// (get) Token: 0x06019B7F RID: 105343 RVA: 0x00353B21 File Offset: 0x00351D21
		// (set) Token: 0x06019B80 RID: 105344 RVA: 0x00353B2A File Offset: 0x00351D2A
		public BottomMargin BottomMargin
		{
			get
			{
				return base.GetElement<BottomMargin>(3);
			}
			set
			{
				base.SetElement<BottomMargin>(3, value);
			}
		}

		// Token: 0x17008E16 RID: 36374
		// (get) Token: 0x06019B81 RID: 105345 RVA: 0x0035443F File Offset: 0x0035263F
		// (set) Token: 0x06019B82 RID: 105346 RVA: 0x00354448 File Offset: 0x00352648
		public TableCellRightMargin TableCellRightMargin
		{
			get
			{
				return base.GetElement<TableCellRightMargin>(4);
			}
			set
			{
				base.SetElement<TableCellRightMargin>(4, value);
			}
		}

		// Token: 0x17008E17 RID: 36375
		// (get) Token: 0x06019B83 RID: 105347 RVA: 0x00353B47 File Offset: 0x00351D47
		// (set) Token: 0x06019B84 RID: 105348 RVA: 0x00353B50 File Offset: 0x00351D50
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public EndMargin EndMargin
		{
			get
			{
				return base.GetElement<EndMargin>(5);
			}
			set
			{
				base.SetElement<EndMargin>(5, value);
			}
		}

		// Token: 0x06019B85 RID: 105349 RVA: 0x00354452 File Offset: 0x00352652
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellMarginDefault>(deep);
		}

		// Token: 0x0400AA41 RID: 43585
		private const string tagName = "tblCellMar";

		// Token: 0x0400AA42 RID: 43586
		private const byte tagNsId = 23;

		// Token: 0x0400AA43 RID: 43587
		internal const int ElementTypeIdConst = 11681;

		// Token: 0x0400AA44 RID: 43588
		private static readonly string[] eleTagNames = new string[] { "top", "left", "start", "bottom", "right", "end" };

		// Token: 0x0400AA45 RID: 43589
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
