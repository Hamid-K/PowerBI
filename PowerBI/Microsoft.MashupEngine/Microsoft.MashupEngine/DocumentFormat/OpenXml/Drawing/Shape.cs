using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002838 RID: 10296
	[ChildElementInfo(typeof(NonVisualShapeProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(TextShape))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x17006631 RID: 26161
		// (get) Token: 0x060142FA RID: 82682 RVA: 0x002DF64E File Offset: 0x002DD84E
		public override string LocalName
		{
			get
			{
				return "sp";
			}
		}

		// Token: 0x17006632 RID: 26162
		// (get) Token: 0x060142FB RID: 82683 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006633 RID: 26163
		// (get) Token: 0x060142FC RID: 82684 RVA: 0x0031022E File Offset: 0x0030E42E
		internal override int ElementTypeId
		{
			get
			{
				return 10332;
			}
		}

		// Token: 0x060142FD RID: 82685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060142FE RID: 82686 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x060142FF RID: 82687 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014300 RID: 82688 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014301 RID: 82689 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014302 RID: 82690 RVA: 0x00310238 File Offset: 0x0030E438
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvSpPr" == name)
			{
				return new NonVisualShapeProperties();
			}
			if (10 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (10 == namespaceId && "txSp" == name)
			{
				return new TextShape();
			}
			if (10 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006634 RID: 26164
		// (get) Token: 0x06014303 RID: 82691 RVA: 0x003102BE File Offset: 0x0030E4BE
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x17006635 RID: 26165
		// (get) Token: 0x06014304 RID: 82692 RVA: 0x003102C5 File Offset: 0x0030E4C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x17006636 RID: 26166
		// (get) Token: 0x06014305 RID: 82693 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006637 RID: 26167
		// (get) Token: 0x06014306 RID: 82694 RVA: 0x003102CC File Offset: 0x0030E4CC
		// (set) Token: 0x06014307 RID: 82695 RVA: 0x003102D5 File Offset: 0x0030E4D5
		public NonVisualShapeProperties NonVisualShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualShapeProperties>(0, value);
			}
		}

		// Token: 0x17006638 RID: 26168
		// (get) Token: 0x06014308 RID: 82696 RVA: 0x003102DF File Offset: 0x0030E4DF
		// (set) Token: 0x06014309 RID: 82697 RVA: 0x003102E8 File Offset: 0x0030E4E8
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x17006639 RID: 26169
		// (get) Token: 0x0601430A RID: 82698 RVA: 0x003102F2 File Offset: 0x0030E4F2
		// (set) Token: 0x0601430B RID: 82699 RVA: 0x003102FB File Offset: 0x0030E4FB
		public TextShape TextShape
		{
			get
			{
				return base.GetElement<TextShape>(2);
			}
			set
			{
				base.SetElement<TextShape>(2, value);
			}
		}

		// Token: 0x1700663A RID: 26170
		// (get) Token: 0x0601430C RID: 82700 RVA: 0x0030CDF5 File Offset: 0x0030AFF5
		// (set) Token: 0x0601430D RID: 82701 RVA: 0x0030CDFE File Offset: 0x0030AFFE
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(3);
			}
			set
			{
				base.SetElement<ShapeStyle>(3, value);
			}
		}

		// Token: 0x1700663B RID: 26171
		// (get) Token: 0x0601430E RID: 82702 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x0601430F RID: 82703 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06014310 RID: 82704 RVA: 0x00310305 File Offset: 0x0030E505
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x04008970 RID: 35184
		private const string tagName = "sp";

		// Token: 0x04008971 RID: 35185
		private const byte tagNsId = 10;

		// Token: 0x04008972 RID: 35186
		internal const int ElementTypeIdConst = 10332;

		// Token: 0x04008973 RID: 35187
		private static readonly string[] eleTagNames = new string[] { "nvSpPr", "spPr", "txSp", "style", "extLst" };

		// Token: 0x04008974 RID: 35188
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
	}
}
