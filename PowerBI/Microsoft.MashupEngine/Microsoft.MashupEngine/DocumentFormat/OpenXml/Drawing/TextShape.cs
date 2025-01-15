using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A8 RID: 10152
	[ChildElementInfo(typeof(Transform2D))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TextBody))]
	[ChildElementInfo(typeof(UseShapeRectangle))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class TextShape : OpenXmlCompositeElement
	{
		// Token: 0x170062BC RID: 25276
		// (get) Token: 0x06013ACD RID: 80589 RVA: 0x0030AA1A File Offset: 0x00308C1A
		public override string LocalName
		{
			get
			{
				return "txSp";
			}
		}

		// Token: 0x170062BD RID: 25277
		// (get) Token: 0x06013ACE RID: 80590 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062BE RID: 25278
		// (get) Token: 0x06013ACF RID: 80591 RVA: 0x0030AA21 File Offset: 0x00308C21
		internal override int ElementTypeId
		{
			get
			{
				return 10185;
			}
		}

		// Token: 0x06013AD0 RID: 80592 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013AD1 RID: 80593 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextShape()
		{
		}

		// Token: 0x06013AD2 RID: 80594 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AD3 RID: 80595 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AD4 RID: 80596 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013AD5 RID: 80597 RVA: 0x0030AA28 File Offset: 0x00308C28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			if (10 == namespaceId && "useSpRect" == name)
			{
				return new UseShapeRectangle();
			}
			if (10 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170062BF RID: 25279
		// (get) Token: 0x06013AD6 RID: 80598 RVA: 0x0030AA96 File Offset: 0x00308C96
		internal override string[] ElementTagNames
		{
			get
			{
				return TextShape.eleTagNames;
			}
		}

		// Token: 0x170062C0 RID: 25280
		// (get) Token: 0x06013AD7 RID: 80599 RVA: 0x0030AA9D File Offset: 0x00308C9D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextShape.eleNamespaceIds;
			}
		}

		// Token: 0x170062C1 RID: 25281
		// (get) Token: 0x06013AD8 RID: 80600 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062C2 RID: 25282
		// (get) Token: 0x06013AD9 RID: 80601 RVA: 0x0030AAA4 File Offset: 0x00308CA4
		// (set) Token: 0x06013ADA RID: 80602 RVA: 0x0030AAAD File Offset: 0x00308CAD
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(0);
			}
			set
			{
				base.SetElement<TextBody>(0, value);
			}
		}

		// Token: 0x06013ADB RID: 80603 RVA: 0x0030AAB7 File Offset: 0x00308CB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextShape>(deep);
		}

		// Token: 0x04008740 RID: 34624
		private const string tagName = "txSp";

		// Token: 0x04008741 RID: 34625
		private const byte tagNsId = 10;

		// Token: 0x04008742 RID: 34626
		internal const int ElementTypeIdConst = 10185;

		// Token: 0x04008743 RID: 34627
		private static readonly string[] eleTagNames = new string[] { "txBody", "useSpRect", "xfrm", "extLst" };

		// Token: 0x04008744 RID: 34628
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
