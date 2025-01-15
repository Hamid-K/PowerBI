using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AB RID: 10155
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualConnectorShapeDrawingProperties))]
	internal class NonVisualConnectionShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062D7 RID: 25303
		// (get) Token: 0x06013B09 RID: 80649 RVA: 0x002FC2F4 File Offset: 0x002FA4F4
		public override string LocalName
		{
			get
			{
				return "nvCxnSpPr";
			}
		}

		// Token: 0x170062D8 RID: 25304
		// (get) Token: 0x06013B0A RID: 80650 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062D9 RID: 25305
		// (get) Token: 0x06013B0B RID: 80651 RVA: 0x0030ACD0 File Offset: 0x00308ED0
		internal override int ElementTypeId
		{
			get
			{
				return 10188;
			}
		}

		// Token: 0x06013B0C RID: 80652 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013B0D RID: 80653 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectionShapeProperties()
		{
		}

		// Token: 0x06013B0E RID: 80654 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectionShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B0F RID: 80655 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectionShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B10 RID: 80656 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectionShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B11 RID: 80657 RVA: 0x0030ACD7 File Offset: 0x00308ED7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (10 == namespaceId && "cNvCxnSpPr" == name)
			{
				return new NonVisualConnectorShapeDrawingProperties();
			}
			return null;
		}

		// Token: 0x170062DA RID: 25306
		// (get) Token: 0x06013B12 RID: 80658 RVA: 0x0030AD0A File Offset: 0x00308F0A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170062DB RID: 25307
		// (get) Token: 0x06013B13 RID: 80659 RVA: 0x0030AD11 File Offset: 0x00308F11
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062DC RID: 25308
		// (get) Token: 0x06013B14 RID: 80660 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062DD RID: 25309
		// (get) Token: 0x06013B15 RID: 80661 RVA: 0x0030A72F File Offset: 0x0030892F
		// (set) Token: 0x06013B16 RID: 80662 RVA: 0x0030A738 File Offset: 0x00308938
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

		// Token: 0x170062DE RID: 25310
		// (get) Token: 0x06013B17 RID: 80663 RVA: 0x0030AD18 File Offset: 0x00308F18
		// (set) Token: 0x06013B18 RID: 80664 RVA: 0x0030AD21 File Offset: 0x00308F21
		public NonVisualConnectorShapeDrawingProperties NonVisualConnectorShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectorShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualConnectorShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x06013B19 RID: 80665 RVA: 0x0030AD2B File Offset: 0x00308F2B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectionShapeProperties>(deep);
		}

		// Token: 0x0400874F RID: 34639
		private const string tagName = "nvCxnSpPr";

		// Token: 0x04008750 RID: 34640
		private const byte tagNsId = 10;

		// Token: 0x04008751 RID: 34641
		internal const int ElementTypeIdConst = 10188;

		// Token: 0x04008752 RID: 34642
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvCxnSpPr" };

		// Token: 0x04008753 RID: 34643
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
