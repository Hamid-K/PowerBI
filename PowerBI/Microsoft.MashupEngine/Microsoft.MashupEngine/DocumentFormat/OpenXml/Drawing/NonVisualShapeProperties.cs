using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A6 RID: 10150
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualShapeDrawingProperties))]
	internal class NonVisualShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062AA RID: 25258
		// (get) Token: 0x06013AA6 RID: 80550 RVA: 0x002DEC0B File Offset: 0x002DCE0B
		public override string LocalName
		{
			get
			{
				return "nvSpPr";
			}
		}

		// Token: 0x170062AB RID: 25259
		// (get) Token: 0x06013AA7 RID: 80551 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062AC RID: 25260
		// (get) Token: 0x06013AA8 RID: 80552 RVA: 0x0030A6E7 File Offset: 0x003088E7
		internal override int ElementTypeId
		{
			get
			{
				return 10183;
			}
		}

		// Token: 0x06013AA9 RID: 80553 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013AAA RID: 80554 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeProperties()
		{
		}

		// Token: 0x06013AAB RID: 80555 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AAC RID: 80556 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AAD RID: 80557 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013AAE RID: 80558 RVA: 0x0030A6EE File Offset: 0x003088EE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (10 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x170062AD RID: 25261
		// (get) Token: 0x06013AAF RID: 80559 RVA: 0x0030A721 File Offset: 0x00308921
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170062AE RID: 25262
		// (get) Token: 0x06013AB0 RID: 80560 RVA: 0x0030A728 File Offset: 0x00308928
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062AF RID: 25263
		// (get) Token: 0x06013AB1 RID: 80561 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062B0 RID: 25264
		// (get) Token: 0x06013AB2 RID: 80562 RVA: 0x0030A72F File Offset: 0x0030892F
		// (set) Token: 0x06013AB3 RID: 80563 RVA: 0x0030A738 File Offset: 0x00308938
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

		// Token: 0x170062B1 RID: 25265
		// (get) Token: 0x06013AB4 RID: 80564 RVA: 0x0030A742 File Offset: 0x00308942
		// (set) Token: 0x06013AB5 RID: 80565 RVA: 0x0030A74B File Offset: 0x0030894B
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

		// Token: 0x06013AB6 RID: 80566 RVA: 0x0030A755 File Offset: 0x00308955
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeProperties>(deep);
		}

		// Token: 0x04008734 RID: 34612
		private const string tagName = "nvSpPr";

		// Token: 0x04008735 RID: 34613
		private const byte tagNsId = 10;

		// Token: 0x04008736 RID: 34614
		internal const int ElementTypeIdConst = 10183;

		// Token: 0x04008737 RID: 34615
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvSpPr" };

		// Token: 0x04008738 RID: 34616
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
