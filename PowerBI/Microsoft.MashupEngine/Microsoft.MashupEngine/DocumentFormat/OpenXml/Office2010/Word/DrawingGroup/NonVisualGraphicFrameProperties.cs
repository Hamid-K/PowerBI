using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F3 RID: 9459
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualGraphicFrameProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005365 RID: 21349
		// (get) Token: 0x0601189F RID: 71839 RVA: 0x002EF8EE File Offset: 0x002EDAEE
		public override string LocalName
		{
			get
			{
				return "cNvFrPr";
			}
		}

		// Token: 0x17005366 RID: 21350
		// (get) Token: 0x060118A0 RID: 71840 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005367 RID: 21351
		// (get) Token: 0x060118A1 RID: 71841 RVA: 0x002EF8F5 File Offset: 0x002EDAF5
		internal override int ElementTypeId
		{
			get
			{
				return 13124;
			}
		}

		// Token: 0x060118A2 RID: 71842 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060118A3 RID: 71843 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameProperties()
		{
		}

		// Token: 0x060118A4 RID: 71844 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118A5 RID: 71845 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118A6 RID: 71846 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060118A7 RID: 71847 RVA: 0x002EF8FC File Offset: 0x002EDAFC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "graphicFrameLocks" == name)
			{
				return new GraphicFrameLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005368 RID: 21352
		// (get) Token: 0x060118A8 RID: 71848 RVA: 0x002EF92F File Offset: 0x002EDB2F
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleTagNames;
			}
		}

		// Token: 0x17005369 RID: 21353
		// (get) Token: 0x060118A9 RID: 71849 RVA: 0x002EF936 File Offset: 0x002EDB36
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700536A RID: 21354
		// (get) Token: 0x060118AA RID: 71850 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700536B RID: 21355
		// (get) Token: 0x060118AB RID: 71851 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x060118AC RID: 71852 RVA: 0x002EF946 File Offset: 0x002EDB46
		public GraphicFrameLocks GraphicFrameLocks
		{
			get
			{
				return base.GetElement<GraphicFrameLocks>(0);
			}
			set
			{
				base.SetElement<GraphicFrameLocks>(0, value);
			}
		}

		// Token: 0x1700536C RID: 21356
		// (get) Token: 0x060118AD RID: 71853 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060118AE RID: 71854 RVA: 0x002DEB73 File Offset: 0x002DCD73
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x060118AF RID: 71855 RVA: 0x002EF950 File Offset: 0x002EDB50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameProperties>(deep);
		}

		// Token: 0x04007B39 RID: 31545
		private const string tagName = "cNvFrPr";

		// Token: 0x04007B3A RID: 31546
		private const byte tagNsId = 60;

		// Token: 0x04007B3B RID: 31547
		internal const int ElementTypeIdConst = 13124;

		// Token: 0x04007B3C RID: 31548
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x04007B3D RID: 31549
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
