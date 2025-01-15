using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002899 RID: 10393
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualGroupShapeDrawingProperties))]
	internal class NonVisualGroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067F2 RID: 26610
		// (get) Token: 0x060146D7 RID: 83671 RVA: 0x002DF395 File Offset: 0x002DD595
		public override string LocalName
		{
			get
			{
				return "nvGrpSpPr";
			}
		}

		// Token: 0x170067F3 RID: 26611
		// (get) Token: 0x060146D8 RID: 83672 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067F4 RID: 26612
		// (get) Token: 0x060146D9 RID: 83673 RVA: 0x003130D5 File Offset: 0x003112D5
		internal override int ElementTypeId
		{
			get
			{
				return 10754;
			}
		}

		// Token: 0x060146DA RID: 83674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060146DB RID: 83675 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeProperties()
		{
		}

		// Token: 0x060146DC RID: 83676 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146DD RID: 83677 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146DE RID: 83678 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060146DF RID: 83679 RVA: 0x003130DC File Offset: 0x003112DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (18 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x170067F5 RID: 26613
		// (get) Token: 0x060146E0 RID: 83680 RVA: 0x0031310F File Offset: 0x0031130F
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170067F6 RID: 26614
		// (get) Token: 0x060146E1 RID: 83681 RVA: 0x00313116 File Offset: 0x00311316
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067F7 RID: 26615
		// (get) Token: 0x060146E2 RID: 83682 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067F8 RID: 26616
		// (get) Token: 0x060146E3 RID: 83683 RVA: 0x003120BF File Offset: 0x003102BF
		// (set) Token: 0x060146E4 RID: 83684 RVA: 0x003120C8 File Offset: 0x003102C8
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

		// Token: 0x170067F9 RID: 26617
		// (get) Token: 0x060146E5 RID: 83685 RVA: 0x0031311D File Offset: 0x0031131D
		// (set) Token: 0x060146E6 RID: 83686 RVA: 0x00313126 File Offset: 0x00311326
		public NonVisualGroupShapeDrawingProperties NonVisualGroupShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x060146E7 RID: 83687 RVA: 0x00313130 File Offset: 0x00311330
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeProperties>(deep);
		}

		// Token: 0x04008E0D RID: 36365
		private const string tagName = "nvGrpSpPr";

		// Token: 0x04008E0E RID: 36366
		private const byte tagNsId = 18;

		// Token: 0x04008E0F RID: 36367
		internal const int ElementTypeIdConst = 10754;

		// Token: 0x04008E10 RID: 36368
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr" };

		// Token: 0x04008E11 RID: 36369
		private static readonly byte[] eleNamespaceIds = new byte[] { 18, 18 };
	}
}
