using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002328 RID: 9000
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingShapeProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class ShapeNonVisualProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004895 RID: 18581
		// (get) Token: 0x06010081 RID: 65665 RVA: 0x002DEC0B File Offset: 0x002DCE0B
		public override string LocalName
		{
			get
			{
				return "nvSpPr";
			}
		}

		// Token: 0x17004896 RID: 18582
		// (get) Token: 0x06010082 RID: 65666 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x17004897 RID: 18583
		// (get) Token: 0x06010083 RID: 65667 RVA: 0x002DEC12 File Offset: 0x002DCE12
		internal override int ElementTypeId
		{
			get
			{
				return 13023;
			}
		}

		// Token: 0x06010084 RID: 65668 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06010085 RID: 65669 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeNonVisualProperties()
		{
		}

		// Token: 0x06010086 RID: 65670 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeNonVisualProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010087 RID: 65671 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeNonVisualProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010088 RID: 65672 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeNonVisualProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010089 RID: 65673 RVA: 0x002DEC19 File Offset: 0x002DCE19
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (56 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualDrawingShapeProperties();
			}
			return null;
		}

		// Token: 0x17004898 RID: 18584
		// (get) Token: 0x0601008A RID: 65674 RVA: 0x002DEC4C File Offset: 0x002DCE4C
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeNonVisualProperties.eleTagNames;
			}
		}

		// Token: 0x17004899 RID: 18585
		// (get) Token: 0x0601008B RID: 65675 RVA: 0x002DEC53 File Offset: 0x002DCE53
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeNonVisualProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700489A RID: 18586
		// (get) Token: 0x0601008C RID: 65676 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700489B RID: 18587
		// (get) Token: 0x0601008D RID: 65677 RVA: 0x002DEC5A File Offset: 0x002DCE5A
		// (set) Token: 0x0601008E RID: 65678 RVA: 0x002DEC63 File Offset: 0x002DCE63
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x1700489C RID: 18588
		// (get) Token: 0x0601008F RID: 65679 RVA: 0x002DEC6D File Offset: 0x002DCE6D
		// (set) Token: 0x06010090 RID: 65680 RVA: 0x002DEC76 File Offset: 0x002DCE76
		public NonVisualDrawingShapeProperties NonVisualDrawingShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingShapeProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualDrawingShapeProperties>(1, value);
			}
		}

		// Token: 0x06010091 RID: 65681 RVA: 0x002DEC80 File Offset: 0x002DCE80
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeNonVisualProperties>(deep);
		}

		// Token: 0x040072C8 RID: 29384
		private const string tagName = "nvSpPr";

		// Token: 0x040072C9 RID: 29385
		private const byte tagNsId = 56;

		// Token: 0x040072CA RID: 29386
		internal const int ElementTypeIdConst = 13023;

		// Token: 0x040072CB RID: 29387
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvSpPr" };

		// Token: 0x040072CC RID: 29388
		private static readonly byte[] eleNamespaceIds = new byte[] { 56, 56 };
	}
}
