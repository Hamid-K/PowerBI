using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D1 RID: 9681
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(PolynomialOrder))]
	[ChildElementInfo(typeof(Period))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TrendlineName))]
	[ChildElementInfo(typeof(TrendlineType))]
	[ChildElementInfo(typeof(Forward))]
	[ChildElementInfo(typeof(Backward))]
	[ChildElementInfo(typeof(Intercept))]
	[ChildElementInfo(typeof(DisplayRSquaredValue))]
	[ChildElementInfo(typeof(DisplayEquation))]
	[ChildElementInfo(typeof(TrendlineLabel))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Trendline : OpenXmlCompositeElement
	{
		// Token: 0x17005803 RID: 22531
		// (get) Token: 0x060122D6 RID: 74454 RVA: 0x002F6B44 File Offset: 0x002F4D44
		public override string LocalName
		{
			get
			{
				return "trendline";
			}
		}

		// Token: 0x17005804 RID: 22532
		// (get) Token: 0x060122D7 RID: 74455 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005805 RID: 22533
		// (get) Token: 0x060122D8 RID: 74456 RVA: 0x002F6B4B File Offset: 0x002F4D4B
		internal override int ElementTypeId
		{
			get
			{
				return 10522;
			}
		}

		// Token: 0x060122D9 RID: 74457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060122DA RID: 74458 RVA: 0x00293ECF File Offset: 0x002920CF
		public Trendline()
		{
		}

		// Token: 0x060122DB RID: 74459 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Trendline(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060122DC RID: 74460 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Trendline(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060122DD RID: 74461 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Trendline(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060122DE RID: 74462 RVA: 0x002F6B54 File Offset: 0x002F4D54
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "name" == name)
			{
				return new TrendlineName();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "trendlineType" == name)
			{
				return new TrendlineType();
			}
			if (11 == namespaceId && "order" == name)
			{
				return new PolynomialOrder();
			}
			if (11 == namespaceId && "period" == name)
			{
				return new Period();
			}
			if (11 == namespaceId && "forward" == name)
			{
				return new Forward();
			}
			if (11 == namespaceId && "backward" == name)
			{
				return new Backward();
			}
			if (11 == namespaceId && "intercept" == name)
			{
				return new Intercept();
			}
			if (11 == namespaceId && "dispRSqr" == name)
			{
				return new DisplayRSquaredValue();
			}
			if (11 == namespaceId && "dispEq" == name)
			{
				return new DisplayEquation();
			}
			if (11 == namespaceId && "trendlineLbl" == name)
			{
				return new TrendlineLabel();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005806 RID: 22534
		// (get) Token: 0x060122DF RID: 74463 RVA: 0x002F6C82 File Offset: 0x002F4E82
		internal override string[] ElementTagNames
		{
			get
			{
				return Trendline.eleTagNames;
			}
		}

		// Token: 0x17005807 RID: 22535
		// (get) Token: 0x060122E0 RID: 74464 RVA: 0x002F6C89 File Offset: 0x002F4E89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Trendline.eleNamespaceIds;
			}
		}

		// Token: 0x17005808 RID: 22536
		// (get) Token: 0x060122E1 RID: 74465 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005809 RID: 22537
		// (get) Token: 0x060122E2 RID: 74466 RVA: 0x002F6C90 File Offset: 0x002F4E90
		// (set) Token: 0x060122E3 RID: 74467 RVA: 0x002F6C99 File Offset: 0x002F4E99
		public TrendlineName TrendlineName
		{
			get
			{
				return base.GetElement<TrendlineName>(0);
			}
			set
			{
				base.SetElement<TrendlineName>(0, value);
			}
		}

		// Token: 0x1700580A RID: 22538
		// (get) Token: 0x060122E4 RID: 74468 RVA: 0x002F529E File Offset: 0x002F349E
		// (set) Token: 0x060122E5 RID: 74469 RVA: 0x002F52A7 File Offset: 0x002F34A7
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(1);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(1, value);
			}
		}

		// Token: 0x1700580B RID: 22539
		// (get) Token: 0x060122E6 RID: 74470 RVA: 0x002F6CA3 File Offset: 0x002F4EA3
		// (set) Token: 0x060122E7 RID: 74471 RVA: 0x002F6CAC File Offset: 0x002F4EAC
		public TrendlineType TrendlineType
		{
			get
			{
				return base.GetElement<TrendlineType>(2);
			}
			set
			{
				base.SetElement<TrendlineType>(2, value);
			}
		}

		// Token: 0x1700580C RID: 22540
		// (get) Token: 0x060122E8 RID: 74472 RVA: 0x002F6CB6 File Offset: 0x002F4EB6
		// (set) Token: 0x060122E9 RID: 74473 RVA: 0x002F6CBF File Offset: 0x002F4EBF
		public PolynomialOrder PolynomialOrder
		{
			get
			{
				return base.GetElement<PolynomialOrder>(3);
			}
			set
			{
				base.SetElement<PolynomialOrder>(3, value);
			}
		}

		// Token: 0x1700580D RID: 22541
		// (get) Token: 0x060122EA RID: 74474 RVA: 0x002F6CC9 File Offset: 0x002F4EC9
		// (set) Token: 0x060122EB RID: 74475 RVA: 0x002F6CD2 File Offset: 0x002F4ED2
		public Period Period
		{
			get
			{
				return base.GetElement<Period>(4);
			}
			set
			{
				base.SetElement<Period>(4, value);
			}
		}

		// Token: 0x1700580E RID: 22542
		// (get) Token: 0x060122EC RID: 74476 RVA: 0x002F6CDC File Offset: 0x002F4EDC
		// (set) Token: 0x060122ED RID: 74477 RVA: 0x002F6CE5 File Offset: 0x002F4EE5
		public Forward Forward
		{
			get
			{
				return base.GetElement<Forward>(5);
			}
			set
			{
				base.SetElement<Forward>(5, value);
			}
		}

		// Token: 0x1700580F RID: 22543
		// (get) Token: 0x060122EE RID: 74478 RVA: 0x002F6CEF File Offset: 0x002F4EEF
		// (set) Token: 0x060122EF RID: 74479 RVA: 0x002F6CF8 File Offset: 0x002F4EF8
		public Backward Backward
		{
			get
			{
				return base.GetElement<Backward>(6);
			}
			set
			{
				base.SetElement<Backward>(6, value);
			}
		}

		// Token: 0x17005810 RID: 22544
		// (get) Token: 0x060122F0 RID: 74480 RVA: 0x002F6D02 File Offset: 0x002F4F02
		// (set) Token: 0x060122F1 RID: 74481 RVA: 0x002F6D0B File Offset: 0x002F4F0B
		public Intercept Intercept
		{
			get
			{
				return base.GetElement<Intercept>(7);
			}
			set
			{
				base.SetElement<Intercept>(7, value);
			}
		}

		// Token: 0x17005811 RID: 22545
		// (get) Token: 0x060122F2 RID: 74482 RVA: 0x002F6D15 File Offset: 0x002F4F15
		// (set) Token: 0x060122F3 RID: 74483 RVA: 0x002F6D1E File Offset: 0x002F4F1E
		public DisplayRSquaredValue DisplayRSquaredValue
		{
			get
			{
				return base.GetElement<DisplayRSquaredValue>(8);
			}
			set
			{
				base.SetElement<DisplayRSquaredValue>(8, value);
			}
		}

		// Token: 0x17005812 RID: 22546
		// (get) Token: 0x060122F4 RID: 74484 RVA: 0x002F6D28 File Offset: 0x002F4F28
		// (set) Token: 0x060122F5 RID: 74485 RVA: 0x002F6D32 File Offset: 0x002F4F32
		public DisplayEquation DisplayEquation
		{
			get
			{
				return base.GetElement<DisplayEquation>(9);
			}
			set
			{
				base.SetElement<DisplayEquation>(9, value);
			}
		}

		// Token: 0x17005813 RID: 22547
		// (get) Token: 0x060122F6 RID: 74486 RVA: 0x002F6D3D File Offset: 0x002F4F3D
		// (set) Token: 0x060122F7 RID: 74487 RVA: 0x002F6D47 File Offset: 0x002F4F47
		public TrendlineLabel TrendlineLabel
		{
			get
			{
				return base.GetElement<TrendlineLabel>(10);
			}
			set
			{
				base.SetElement<TrendlineLabel>(10, value);
			}
		}

		// Token: 0x17005814 RID: 22548
		// (get) Token: 0x060122F8 RID: 74488 RVA: 0x002F6D52 File Offset: 0x002F4F52
		// (set) Token: 0x060122F9 RID: 74489 RVA: 0x002F6D5C File Offset: 0x002F4F5C
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(11);
			}
			set
			{
				base.SetElement<ExtensionList>(11, value);
			}
		}

		// Token: 0x060122FA RID: 74490 RVA: 0x002F6D67 File Offset: 0x002F4F67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Trendline>(deep);
		}

		// Token: 0x04007E89 RID: 32393
		private const string tagName = "trendline";

		// Token: 0x04007E8A RID: 32394
		private const byte tagNsId = 11;

		// Token: 0x04007E8B RID: 32395
		internal const int ElementTypeIdConst = 10522;

		// Token: 0x04007E8C RID: 32396
		private static readonly string[] eleTagNames = new string[]
		{
			"name", "spPr", "trendlineType", "order", "period", "forward", "backward", "intercept", "dispRSqr", "dispEq",
			"trendlineLbl", "extLst"
		};

		// Token: 0x04007E8D RID: 32397
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11
		};
	}
}
