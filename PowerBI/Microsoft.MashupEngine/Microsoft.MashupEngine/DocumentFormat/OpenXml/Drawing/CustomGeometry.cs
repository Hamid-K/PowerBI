using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002726 RID: 10022
	[ChildElementInfo(typeof(AdjustValueList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeGuideList))]
	[ChildElementInfo(typeof(AdjustHandleList))]
	[ChildElementInfo(typeof(ConnectionSiteList))]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(PathList))]
	internal class CustomGeometry : OpenXmlCompositeElement
	{
		// Token: 0x17005FC3 RID: 24515
		// (get) Token: 0x060133F7 RID: 78839 RVA: 0x00305620 File Offset: 0x00303820
		public override string LocalName
		{
			get
			{
				return "custGeom";
			}
		}

		// Token: 0x17005FC4 RID: 24516
		// (get) Token: 0x060133F8 RID: 78840 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FC5 RID: 24517
		// (get) Token: 0x060133F9 RID: 78841 RVA: 0x00305627 File Offset: 0x00303827
		internal override int ElementTypeId
		{
			get
			{
				return 10085;
			}
		}

		// Token: 0x060133FA RID: 78842 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060133FB RID: 78843 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomGeometry()
		{
		}

		// Token: 0x060133FC RID: 78844 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomGeometry(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060133FD RID: 78845 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomGeometry(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060133FE RID: 78846 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomGeometry(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060133FF RID: 78847 RVA: 0x00305630 File Offset: 0x00303830
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "avLst" == name)
			{
				return new AdjustValueList();
			}
			if (10 == namespaceId && "gdLst" == name)
			{
				return new ShapeGuideList();
			}
			if (10 == namespaceId && "ahLst" == name)
			{
				return new AdjustHandleList();
			}
			if (10 == namespaceId && "cxnLst" == name)
			{
				return new ConnectionSiteList();
			}
			if (10 == namespaceId && "rect" == name)
			{
				return new Rectangle();
			}
			if (10 == namespaceId && "pathLst" == name)
			{
				return new PathList();
			}
			return null;
		}

		// Token: 0x17005FC6 RID: 24518
		// (get) Token: 0x06013400 RID: 78848 RVA: 0x003056CE File Offset: 0x003038CE
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomGeometry.eleTagNames;
			}
		}

		// Token: 0x17005FC7 RID: 24519
		// (get) Token: 0x06013401 RID: 78849 RVA: 0x003056D5 File Offset: 0x003038D5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomGeometry.eleNamespaceIds;
			}
		}

		// Token: 0x17005FC8 RID: 24520
		// (get) Token: 0x06013402 RID: 78850 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005FC9 RID: 24521
		// (get) Token: 0x06013403 RID: 78851 RVA: 0x003056DC File Offset: 0x003038DC
		// (set) Token: 0x06013404 RID: 78852 RVA: 0x003056E5 File Offset: 0x003038E5
		public AdjustValueList AdjustValueList
		{
			get
			{
				return base.GetElement<AdjustValueList>(0);
			}
			set
			{
				base.SetElement<AdjustValueList>(0, value);
			}
		}

		// Token: 0x17005FCA RID: 24522
		// (get) Token: 0x06013405 RID: 78853 RVA: 0x003056EF File Offset: 0x003038EF
		// (set) Token: 0x06013406 RID: 78854 RVA: 0x003056F8 File Offset: 0x003038F8
		public ShapeGuideList ShapeGuideList
		{
			get
			{
				return base.GetElement<ShapeGuideList>(1);
			}
			set
			{
				base.SetElement<ShapeGuideList>(1, value);
			}
		}

		// Token: 0x17005FCB RID: 24523
		// (get) Token: 0x06013407 RID: 78855 RVA: 0x00305702 File Offset: 0x00303902
		// (set) Token: 0x06013408 RID: 78856 RVA: 0x0030570B File Offset: 0x0030390B
		public AdjustHandleList AdjustHandleList
		{
			get
			{
				return base.GetElement<AdjustHandleList>(2);
			}
			set
			{
				base.SetElement<AdjustHandleList>(2, value);
			}
		}

		// Token: 0x17005FCC RID: 24524
		// (get) Token: 0x06013409 RID: 78857 RVA: 0x00305715 File Offset: 0x00303915
		// (set) Token: 0x0601340A RID: 78858 RVA: 0x0030571E File Offset: 0x0030391E
		public ConnectionSiteList ConnectionSiteList
		{
			get
			{
				return base.GetElement<ConnectionSiteList>(3);
			}
			set
			{
				base.SetElement<ConnectionSiteList>(3, value);
			}
		}

		// Token: 0x17005FCD RID: 24525
		// (get) Token: 0x0601340B RID: 78859 RVA: 0x00305728 File Offset: 0x00303928
		// (set) Token: 0x0601340C RID: 78860 RVA: 0x00305731 File Offset: 0x00303931
		public Rectangle Rectangle
		{
			get
			{
				return base.GetElement<Rectangle>(4);
			}
			set
			{
				base.SetElement<Rectangle>(4, value);
			}
		}

		// Token: 0x17005FCE RID: 24526
		// (get) Token: 0x0601340D RID: 78861 RVA: 0x0030573B File Offset: 0x0030393B
		// (set) Token: 0x0601340E RID: 78862 RVA: 0x00305744 File Offset: 0x00303944
		public PathList PathList
		{
			get
			{
				return base.GetElement<PathList>(5);
			}
			set
			{
				base.SetElement<PathList>(5, value);
			}
		}

		// Token: 0x0601340F RID: 78863 RVA: 0x0030574E File Offset: 0x0030394E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomGeometry>(deep);
		}

		// Token: 0x04008549 RID: 34121
		private const string tagName = "custGeom";

		// Token: 0x0400854A RID: 34122
		private const byte tagNsId = 10;

		// Token: 0x0400854B RID: 34123
		internal const int ElementTypeIdConst = 10085;

		// Token: 0x0400854C RID: 34124
		private static readonly string[] eleTagNames = new string[] { "avLst", "gdLst", "ahLst", "cxnLst", "rect", "pathLst" };

		// Token: 0x0400854D RID: 34125
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
