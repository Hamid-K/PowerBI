using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFE RID: 12030
	[ChildElementInfo(typeof(BottomMargin))]
	[ChildElementInfo(typeof(RightMargin))]
	[ChildElementInfo(typeof(StartMargin), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EndMargin), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LeftMargin))]
	[ChildElementInfo(typeof(TopMargin))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellMargin : OpenXmlCompositeElement
	{
		// Token: 0x17008DAE RID: 36270
		// (get) Token: 0x06019AAC RID: 105132 RVA: 0x00353A2D File Offset: 0x00351C2D
		public override string LocalName
		{
			get
			{
				return "tcMar";
			}
		}

		// Token: 0x17008DAF RID: 36271
		// (get) Token: 0x06019AAD RID: 105133 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DB0 RID: 36272
		// (get) Token: 0x06019AAE RID: 105134 RVA: 0x00353A34 File Offset: 0x00351C34
		internal override int ElementTypeId
		{
			get
			{
				return 11656;
			}
		}

		// Token: 0x06019AAF RID: 105135 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019AB0 RID: 105136 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellMargin()
		{
		}

		// Token: 0x06019AB1 RID: 105137 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellMargin(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019AB2 RID: 105138 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellMargin(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019AB3 RID: 105139 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellMargin(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019AB4 RID: 105140 RVA: 0x00353A3C File Offset: 0x00351C3C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopMargin();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new LeftMargin();
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
				return new RightMargin();
			}
			if (23 == namespaceId && "end" == name)
			{
				return new EndMargin();
			}
			return null;
		}

		// Token: 0x17008DB1 RID: 36273
		// (get) Token: 0x06019AB5 RID: 105141 RVA: 0x00353ADA File Offset: 0x00351CDA
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellMargin.eleTagNames;
			}
		}

		// Token: 0x17008DB2 RID: 36274
		// (get) Token: 0x06019AB6 RID: 105142 RVA: 0x00353AE1 File Offset: 0x00351CE1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellMargin.eleNamespaceIds;
			}
		}

		// Token: 0x17008DB3 RID: 36275
		// (get) Token: 0x06019AB7 RID: 105143 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008DB4 RID: 36276
		// (get) Token: 0x06019AB8 RID: 105144 RVA: 0x00353AE8 File Offset: 0x00351CE8
		// (set) Token: 0x06019AB9 RID: 105145 RVA: 0x00353AF1 File Offset: 0x00351CF1
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

		// Token: 0x17008DB5 RID: 36277
		// (get) Token: 0x06019ABA RID: 105146 RVA: 0x00353AFB File Offset: 0x00351CFB
		// (set) Token: 0x06019ABB RID: 105147 RVA: 0x00353B04 File Offset: 0x00351D04
		public LeftMargin LeftMargin
		{
			get
			{
				return base.GetElement<LeftMargin>(1);
			}
			set
			{
				base.SetElement<LeftMargin>(1, value);
			}
		}

		// Token: 0x17008DB6 RID: 36278
		// (get) Token: 0x06019ABC RID: 105148 RVA: 0x00353B0E File Offset: 0x00351D0E
		// (set) Token: 0x06019ABD RID: 105149 RVA: 0x00353B17 File Offset: 0x00351D17
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

		// Token: 0x17008DB7 RID: 36279
		// (get) Token: 0x06019ABE RID: 105150 RVA: 0x00353B21 File Offset: 0x00351D21
		// (set) Token: 0x06019ABF RID: 105151 RVA: 0x00353B2A File Offset: 0x00351D2A
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

		// Token: 0x17008DB8 RID: 36280
		// (get) Token: 0x06019AC0 RID: 105152 RVA: 0x00353B34 File Offset: 0x00351D34
		// (set) Token: 0x06019AC1 RID: 105153 RVA: 0x00353B3D File Offset: 0x00351D3D
		public RightMargin RightMargin
		{
			get
			{
				return base.GetElement<RightMargin>(4);
			}
			set
			{
				base.SetElement<RightMargin>(4, value);
			}
		}

		// Token: 0x17008DB9 RID: 36281
		// (get) Token: 0x06019AC2 RID: 105154 RVA: 0x00353B47 File Offset: 0x00351D47
		// (set) Token: 0x06019AC3 RID: 105155 RVA: 0x00353B50 File Offset: 0x00351D50
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

		// Token: 0x06019AC4 RID: 105156 RVA: 0x00353B5A File Offset: 0x00351D5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellMargin>(deep);
		}

		// Token: 0x0400AA07 RID: 43527
		private const string tagName = "tcMar";

		// Token: 0x0400AA08 RID: 43528
		private const byte tagNsId = 23;

		// Token: 0x0400AA09 RID: 43529
		internal const int ElementTypeIdConst = 11656;

		// Token: 0x0400AA0A RID: 43530
		private static readonly string[] eleTagNames = new string[] { "top", "left", "start", "bottom", "right", "end" };

		// Token: 0x0400AA0B RID: 43531
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
