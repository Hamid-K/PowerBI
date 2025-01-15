using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D0 RID: 9680
	[ChildElementInfo(typeof(Index))]
	[ChildElementInfo(typeof(InvertIfNegative))]
	[ChildElementInfo(typeof(Marker))]
	[ChildElementInfo(typeof(Bubble3D))]
	[ChildElementInfo(typeof(Explosion))]
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PictureOptions))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataPoint : OpenXmlCompositeElement
	{
		// Token: 0x170057F5 RID: 22517
		// (get) Token: 0x060122B8 RID: 74424 RVA: 0x002F696D File Offset: 0x002F4B6D
		public override string LocalName
		{
			get
			{
				return "dPt";
			}
		}

		// Token: 0x170057F6 RID: 22518
		// (get) Token: 0x060122B9 RID: 74425 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057F7 RID: 22519
		// (get) Token: 0x060122BA RID: 74426 RVA: 0x002F6974 File Offset: 0x002F4B74
		internal override int ElementTypeId
		{
			get
			{
				return 10521;
			}
		}

		// Token: 0x060122BB RID: 74427 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060122BC RID: 74428 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataPoint()
		{
		}

		// Token: 0x060122BD RID: 74429 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataPoint(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060122BE RID: 74430 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataPoint(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060122BF RID: 74431 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataPoint(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060122C0 RID: 74432 RVA: 0x002F697C File Offset: 0x002F4B7C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "invertIfNegative" == name)
			{
				return new InvertIfNegative();
			}
			if (11 == namespaceId && "marker" == name)
			{
				return new Marker();
			}
			if (11 == namespaceId && "bubble3D" == name)
			{
				return new Bubble3D();
			}
			if (11 == namespaceId && "explosion" == name)
			{
				return new Explosion();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "pictureOptions" == name)
			{
				return new PictureOptions();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170057F8 RID: 22520
		// (get) Token: 0x060122C1 RID: 74433 RVA: 0x002F6A4A File Offset: 0x002F4C4A
		internal override string[] ElementTagNames
		{
			get
			{
				return DataPoint.eleTagNames;
			}
		}

		// Token: 0x170057F9 RID: 22521
		// (get) Token: 0x060122C2 RID: 74434 RVA: 0x002F6A51 File Offset: 0x002F4C51
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataPoint.eleNamespaceIds;
			}
		}

		// Token: 0x170057FA RID: 22522
		// (get) Token: 0x060122C3 RID: 74435 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057FB RID: 22523
		// (get) Token: 0x060122C4 RID: 74436 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x060122C5 RID: 74437 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x170057FC RID: 22524
		// (get) Token: 0x060122C6 RID: 74438 RVA: 0x002F6A58 File Offset: 0x002F4C58
		// (set) Token: 0x060122C7 RID: 74439 RVA: 0x002F6A61 File Offset: 0x002F4C61
		public InvertIfNegative InvertIfNegative
		{
			get
			{
				return base.GetElement<InvertIfNegative>(1);
			}
			set
			{
				base.SetElement<InvertIfNegative>(1, value);
			}
		}

		// Token: 0x170057FD RID: 22525
		// (get) Token: 0x060122C8 RID: 74440 RVA: 0x002F59D7 File Offset: 0x002F3BD7
		// (set) Token: 0x060122C9 RID: 74441 RVA: 0x002F59E0 File Offset: 0x002F3BE0
		public Marker Marker
		{
			get
			{
				return base.GetElement<Marker>(2);
			}
			set
			{
				base.SetElement<Marker>(2, value);
			}
		}

		// Token: 0x170057FE RID: 22526
		// (get) Token: 0x060122CA RID: 74442 RVA: 0x002F6A6B File Offset: 0x002F4C6B
		// (set) Token: 0x060122CB RID: 74443 RVA: 0x002F6A74 File Offset: 0x002F4C74
		public Bubble3D Bubble3D
		{
			get
			{
				return base.GetElement<Bubble3D>(3);
			}
			set
			{
				base.SetElement<Bubble3D>(3, value);
			}
		}

		// Token: 0x170057FF RID: 22527
		// (get) Token: 0x060122CC RID: 74444 RVA: 0x002F6A7E File Offset: 0x002F4C7E
		// (set) Token: 0x060122CD RID: 74445 RVA: 0x002F6A87 File Offset: 0x002F4C87
		public Explosion Explosion
		{
			get
			{
				return base.GetElement<Explosion>(4);
			}
			set
			{
				base.SetElement<Explosion>(4, value);
			}
		}

		// Token: 0x17005800 RID: 22528
		// (get) Token: 0x060122CE RID: 74446 RVA: 0x002F6A91 File Offset: 0x002F4C91
		// (set) Token: 0x060122CF RID: 74447 RVA: 0x002F6A9A File Offset: 0x002F4C9A
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(5);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(5, value);
			}
		}

		// Token: 0x17005801 RID: 22529
		// (get) Token: 0x060122D0 RID: 74448 RVA: 0x002F6AA4 File Offset: 0x002F4CA4
		// (set) Token: 0x060122D1 RID: 74449 RVA: 0x002F6AAD File Offset: 0x002F4CAD
		public PictureOptions PictureOptions
		{
			get
			{
				return base.GetElement<PictureOptions>(6);
			}
			set
			{
				base.SetElement<PictureOptions>(6, value);
			}
		}

		// Token: 0x17005802 RID: 22530
		// (get) Token: 0x060122D2 RID: 74450 RVA: 0x002F6AB7 File Offset: 0x002F4CB7
		// (set) Token: 0x060122D3 RID: 74451 RVA: 0x002F6AC0 File Offset: 0x002F4CC0
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(7);
			}
			set
			{
				base.SetElement<ExtensionList>(7, value);
			}
		}

		// Token: 0x060122D4 RID: 74452 RVA: 0x002F6ACA File Offset: 0x002F4CCA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataPoint>(deep);
		}

		// Token: 0x04007E84 RID: 32388
		private const string tagName = "dPt";

		// Token: 0x04007E85 RID: 32389
		private const byte tagNsId = 11;

		// Token: 0x04007E86 RID: 32390
		internal const int ElementTypeIdConst = 10521;

		// Token: 0x04007E87 RID: 32391
		private static readonly string[] eleTagNames = new string[] { "idx", "invertIfNegative", "marker", "bubble3D", "explosion", "spPr", "pictureOptions", "extLst" };

		// Token: 0x04007E88 RID: 32392
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
