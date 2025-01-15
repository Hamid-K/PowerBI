using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002635 RID: 9781
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualConnectionShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualConnectorShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A80 RID: 23168
		// (get) Token: 0x06012848 RID: 75848 RVA: 0x002FC2F4 File Offset: 0x002FA4F4
		public override string LocalName
		{
			get
			{
				return "nvCxnSpPr";
			}
		}

		// Token: 0x17005A81 RID: 23169
		// (get) Token: 0x06012849 RID: 75849 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A82 RID: 23170
		// (get) Token: 0x0601284A RID: 75850 RVA: 0x002FC2FB File Offset: 0x002FA4FB
		internal override int ElementTypeId
		{
			get
			{
				return 10600;
			}
		}

		// Token: 0x0601284B RID: 75851 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601284C RID: 75852 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectorShapeDrawingProperties()
		{
		}

		// Token: 0x0601284D RID: 75853 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectorShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601284E RID: 75854 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectorShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601284F RID: 75855 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectorShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012850 RID: 75856 RVA: 0x002FC302 File Offset: 0x002FA502
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (12 == namespaceId && "cNvCxnSpPr" == name)
			{
				return new NonVisualConnectionShapeProperties();
			}
			return null;
		}

		// Token: 0x17005A83 RID: 23171
		// (get) Token: 0x06012851 RID: 75857 RVA: 0x002FC335 File Offset: 0x002FA535
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005A84 RID: 23172
		// (get) Token: 0x06012852 RID: 75858 RVA: 0x002FC33C File Offset: 0x002FA53C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A85 RID: 23173
		// (get) Token: 0x06012853 RID: 75859 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A86 RID: 23174
		// (get) Token: 0x06012854 RID: 75860 RVA: 0x002FBD7F File Offset: 0x002F9F7F
		// (set) Token: 0x06012855 RID: 75861 RVA: 0x002FBD88 File Offset: 0x002F9F88
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

		// Token: 0x17005A87 RID: 23175
		// (get) Token: 0x06012856 RID: 75862 RVA: 0x002FC343 File Offset: 0x002FA543
		// (set) Token: 0x06012857 RID: 75863 RVA: 0x002FC34C File Offset: 0x002FA54C
		public NonVisualConnectionShapeProperties NonVisualConnectionShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectionShapeProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualConnectionShapeProperties>(1, value);
			}
		}

		// Token: 0x06012858 RID: 75864 RVA: 0x002FC356 File Offset: 0x002FA556
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectorShapeDrawingProperties>(deep);
		}

		// Token: 0x04008079 RID: 32889
		private const string tagName = "nvCxnSpPr";

		// Token: 0x0400807A RID: 32890
		private const byte tagNsId = 12;

		// Token: 0x0400807B RID: 32891
		internal const int ElementTypeIdConst = 10600;

		// Token: 0x0400807C RID: 32892
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvCxnSpPr" };

		// Token: 0x0400807D RID: 32893
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12 };
	}
}
