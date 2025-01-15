using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002630 RID: 9776
	[ChildElementInfo(typeof(NonVisualShapeDrawingProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A52 RID: 23122
		// (get) Token: 0x060127E3 RID: 75747 RVA: 0x002DEC0B File Offset: 0x002DCE0B
		public override string LocalName
		{
			get
			{
				return "nvSpPr";
			}
		}

		// Token: 0x17005A53 RID: 23123
		// (get) Token: 0x060127E4 RID: 75748 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A54 RID: 23124
		// (get) Token: 0x060127E5 RID: 75749 RVA: 0x002FBD37 File Offset: 0x002F9F37
		internal override int ElementTypeId
		{
			get
			{
				return 10595;
			}
		}

		// Token: 0x060127E6 RID: 75750 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060127E7 RID: 75751 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeProperties()
		{
		}

		// Token: 0x060127E8 RID: 75752 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127E9 RID: 75753 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127EA RID: 75754 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060127EB RID: 75755 RVA: 0x002FBD3E File Offset: 0x002F9F3E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (12 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x17005A55 RID: 23125
		// (get) Token: 0x060127EC RID: 75756 RVA: 0x002FBD71 File Offset: 0x002F9F71
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005A56 RID: 23126
		// (get) Token: 0x060127ED RID: 75757 RVA: 0x002FBD78 File Offset: 0x002F9F78
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A57 RID: 23127
		// (get) Token: 0x060127EE RID: 75758 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A58 RID: 23128
		// (get) Token: 0x060127EF RID: 75759 RVA: 0x002FBD7F File Offset: 0x002F9F7F
		// (set) Token: 0x060127F0 RID: 75760 RVA: 0x002FBD88 File Offset: 0x002F9F88
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

		// Token: 0x17005A59 RID: 23129
		// (get) Token: 0x060127F1 RID: 75761 RVA: 0x002FBD92 File Offset: 0x002F9F92
		// (set) Token: 0x060127F2 RID: 75762 RVA: 0x002FBD9B File Offset: 0x002F9F9B
		public NonVisualShapeDrawingProperties NonVisualShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x060127F3 RID: 75763 RVA: 0x002FBDA5 File Offset: 0x002F9FA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeProperties>(deep);
		}

		// Token: 0x0400805E RID: 32862
		private const string tagName = "nvSpPr";

		// Token: 0x0400805F RID: 32863
		private const byte tagNsId = 12;

		// Token: 0x04008060 RID: 32864
		internal const int ElementTypeIdConst = 10595;

		// Token: 0x04008061 RID: 32865
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvSpPr" };

		// Token: 0x04008062 RID: 32866
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
