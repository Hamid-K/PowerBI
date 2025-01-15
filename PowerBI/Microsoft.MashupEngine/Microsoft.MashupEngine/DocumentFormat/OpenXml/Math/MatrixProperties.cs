using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BE RID: 10686
	[ChildElementInfo(typeof(BaseJustification))]
	[ChildElementInfo(typeof(RowSpacing))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HidePlaceholder))]
	[ChildElementInfo(typeof(RowSpacingRule))]
	[ChildElementInfo(typeof(ColumnGapRule))]
	[ChildElementInfo(typeof(ColumnSpacing))]
	[ChildElementInfo(typeof(ColumnGap))]
	[ChildElementInfo(typeof(MatrixColumns))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class MatrixProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DB2 RID: 28082
		// (get) Token: 0x06015438 RID: 87096 RVA: 0x0031D576 File Offset: 0x0031B776
		public override string LocalName
		{
			get
			{
				return "mPr";
			}
		}

		// Token: 0x17006DB3 RID: 28083
		// (get) Token: 0x06015439 RID: 87097 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DB4 RID: 28084
		// (get) Token: 0x0601543A RID: 87098 RVA: 0x0031D57D File Offset: 0x0031B77D
		internal override int ElementTypeId
		{
			get
			{
				return 10921;
			}
		}

		// Token: 0x0601543B RID: 87099 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601543C RID: 87100 RVA: 0x00293ECF File Offset: 0x002920CF
		public MatrixProperties()
		{
		}

		// Token: 0x0601543D RID: 87101 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MatrixProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601543E RID: 87102 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MatrixProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601543F RID: 87103 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MatrixProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015440 RID: 87104 RVA: 0x0031D584 File Offset: 0x0031B784
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "baseJc" == name)
			{
				return new BaseJustification();
			}
			if (21 == namespaceId && "plcHide" == name)
			{
				return new HidePlaceholder();
			}
			if (21 == namespaceId && "rSpRule" == name)
			{
				return new RowSpacingRule();
			}
			if (21 == namespaceId && "cGpRule" == name)
			{
				return new ColumnGapRule();
			}
			if (21 == namespaceId && "rSp" == name)
			{
				return new RowSpacing();
			}
			if (21 == namespaceId && "cSp" == name)
			{
				return new ColumnSpacing();
			}
			if (21 == namespaceId && "cGp" == name)
			{
				return new ColumnGap();
			}
			if (21 == namespaceId && "mcs" == name)
			{
				return new MatrixColumns();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DB5 RID: 28085
		// (get) Token: 0x06015441 RID: 87105 RVA: 0x0031D66A File Offset: 0x0031B86A
		internal override string[] ElementTagNames
		{
			get
			{
				return MatrixProperties.eleTagNames;
			}
		}

		// Token: 0x17006DB6 RID: 28086
		// (get) Token: 0x06015442 RID: 87106 RVA: 0x0031D671 File Offset: 0x0031B871
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MatrixProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DB7 RID: 28087
		// (get) Token: 0x06015443 RID: 87107 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DB8 RID: 28088
		// (get) Token: 0x06015444 RID: 87108 RVA: 0x0031CD98 File Offset: 0x0031AF98
		// (set) Token: 0x06015445 RID: 87109 RVA: 0x0031CDA1 File Offset: 0x0031AFA1
		public BaseJustification BaseJustification
		{
			get
			{
				return base.GetElement<BaseJustification>(0);
			}
			set
			{
				base.SetElement<BaseJustification>(0, value);
			}
		}

		// Token: 0x17006DB9 RID: 28089
		// (get) Token: 0x06015446 RID: 87110 RVA: 0x0031D678 File Offset: 0x0031B878
		// (set) Token: 0x06015447 RID: 87111 RVA: 0x0031D681 File Offset: 0x0031B881
		public HidePlaceholder HidePlaceholder
		{
			get
			{
				return base.GetElement<HidePlaceholder>(1);
			}
			set
			{
				base.SetElement<HidePlaceholder>(1, value);
			}
		}

		// Token: 0x17006DBA RID: 28090
		// (get) Token: 0x06015448 RID: 87112 RVA: 0x0031D68B File Offset: 0x0031B88B
		// (set) Token: 0x06015449 RID: 87113 RVA: 0x0031D694 File Offset: 0x0031B894
		public RowSpacingRule RowSpacingRule
		{
			get
			{
				return base.GetElement<RowSpacingRule>(2);
			}
			set
			{
				base.SetElement<RowSpacingRule>(2, value);
			}
		}

		// Token: 0x17006DBB RID: 28091
		// (get) Token: 0x0601544A RID: 87114 RVA: 0x0031D69E File Offset: 0x0031B89E
		// (set) Token: 0x0601544B RID: 87115 RVA: 0x0031D6A7 File Offset: 0x0031B8A7
		public ColumnGapRule ColumnGapRule
		{
			get
			{
				return base.GetElement<ColumnGapRule>(3);
			}
			set
			{
				base.SetElement<ColumnGapRule>(3, value);
			}
		}

		// Token: 0x17006DBC RID: 28092
		// (get) Token: 0x0601544C RID: 87116 RVA: 0x0031CDE4 File Offset: 0x0031AFE4
		// (set) Token: 0x0601544D RID: 87117 RVA: 0x0031CDED File Offset: 0x0031AFED
		public RowSpacing RowSpacing
		{
			get
			{
				return base.GetElement<RowSpacing>(4);
			}
			set
			{
				base.SetElement<RowSpacing>(4, value);
			}
		}

		// Token: 0x17006DBD RID: 28093
		// (get) Token: 0x0601544E RID: 87118 RVA: 0x0031D6B1 File Offset: 0x0031B8B1
		// (set) Token: 0x0601544F RID: 87119 RVA: 0x0031D6BA File Offset: 0x0031B8BA
		public ColumnSpacing ColumnSpacing
		{
			get
			{
				return base.GetElement<ColumnSpacing>(5);
			}
			set
			{
				base.SetElement<ColumnSpacing>(5, value);
			}
		}

		// Token: 0x17006DBE RID: 28094
		// (get) Token: 0x06015450 RID: 87120 RVA: 0x0031D6C4 File Offset: 0x0031B8C4
		// (set) Token: 0x06015451 RID: 87121 RVA: 0x0031D6CD File Offset: 0x0031B8CD
		public ColumnGap ColumnGap
		{
			get
			{
				return base.GetElement<ColumnGap>(6);
			}
			set
			{
				base.SetElement<ColumnGap>(6, value);
			}
		}

		// Token: 0x17006DBF RID: 28095
		// (get) Token: 0x06015452 RID: 87122 RVA: 0x0031D6D7 File Offset: 0x0031B8D7
		// (set) Token: 0x06015453 RID: 87123 RVA: 0x0031D6E0 File Offset: 0x0031B8E0
		public MatrixColumns MatrixColumns
		{
			get
			{
				return base.GetElement<MatrixColumns>(7);
			}
			set
			{
				base.SetElement<MatrixColumns>(7, value);
			}
		}

		// Token: 0x17006DC0 RID: 28096
		// (get) Token: 0x06015454 RID: 87124 RVA: 0x0031C878 File Offset: 0x0031AA78
		// (set) Token: 0x06015455 RID: 87125 RVA: 0x0031C881 File Offset: 0x0031AA81
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(8);
			}
			set
			{
				base.SetElement<ControlProperties>(8, value);
			}
		}

		// Token: 0x06015456 RID: 87126 RVA: 0x0031D6EA File Offset: 0x0031B8EA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixProperties>(deep);
		}

		// Token: 0x0400926A RID: 37482
		private const string tagName = "mPr";

		// Token: 0x0400926B RID: 37483
		private const byte tagNsId = 21;

		// Token: 0x0400926C RID: 37484
		internal const int ElementTypeIdConst = 10921;

		// Token: 0x0400926D RID: 37485
		private static readonly string[] eleTagNames = new string[] { "baseJc", "plcHide", "rSpRule", "cGpRule", "rSp", "cSp", "cGp", "mcs", "ctrlPr" };

		// Token: 0x0400926E RID: 37486
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21, 21, 21, 21 };
	}
}
