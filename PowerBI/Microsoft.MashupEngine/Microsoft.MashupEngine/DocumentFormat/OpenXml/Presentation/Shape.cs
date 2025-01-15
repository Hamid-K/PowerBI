using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA4 RID: 10916
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualShapeProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(TextBody))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x1700744F RID: 29775
		// (get) Token: 0x060162ED RID: 90861 RVA: 0x002DF64E File Offset: 0x002DD84E
		public override string LocalName
		{
			get
			{
				return "sp";
			}
		}

		// Token: 0x17007450 RID: 29776
		// (get) Token: 0x060162EE RID: 90862 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007451 RID: 29777
		// (get) Token: 0x060162EF RID: 90863 RVA: 0x0032765E File Offset: 0x0032585E
		internal override int ElementTypeId
		{
			get
			{
				return 12329;
			}
		}

		// Token: 0x060162F0 RID: 90864 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007452 RID: 29778
		// (get) Token: 0x060162F1 RID: 90865 RVA: 0x00327665 File Offset: 0x00325865
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x17007453 RID: 29779
		// (get) Token: 0x060162F2 RID: 90866 RVA: 0x0032766C File Offset: 0x0032586C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x17007454 RID: 29780
		// (get) Token: 0x060162F3 RID: 90867 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060162F4 RID: 90868 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "useBgFill")]
		public BooleanValue UseBackgroundFill
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060162F5 RID: 90869 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x060162F6 RID: 90870 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162F7 RID: 90871 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162F8 RID: 90872 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162F9 RID: 90873 RVA: 0x00327674 File Offset: 0x00325874
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "nvSpPr" == name)
			{
				return new NonVisualShapeProperties();
			}
			if (24 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (24 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (24 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17007455 RID: 29781
		// (get) Token: 0x060162FA RID: 90874 RVA: 0x003276FA File Offset: 0x003258FA
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x17007456 RID: 29782
		// (get) Token: 0x060162FB RID: 90875 RVA: 0x00327701 File Offset: 0x00325901
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x17007457 RID: 29783
		// (get) Token: 0x060162FC RID: 90876 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007458 RID: 29784
		// (get) Token: 0x060162FD RID: 90877 RVA: 0x00327708 File Offset: 0x00325908
		// (set) Token: 0x060162FE RID: 90878 RVA: 0x00327711 File Offset: 0x00325911
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

		// Token: 0x17007459 RID: 29785
		// (get) Token: 0x060162FF RID: 90879 RVA: 0x0032771B File Offset: 0x0032591B
		// (set) Token: 0x06016300 RID: 90880 RVA: 0x00327724 File Offset: 0x00325924
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

		// Token: 0x1700745A RID: 29786
		// (get) Token: 0x06016301 RID: 90881 RVA: 0x0032772E File Offset: 0x0032592E
		// (set) Token: 0x06016302 RID: 90882 RVA: 0x00327737 File Offset: 0x00325937
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x1700745B RID: 29787
		// (get) Token: 0x06016303 RID: 90883 RVA: 0x00327741 File Offset: 0x00325941
		// (set) Token: 0x06016304 RID: 90884 RVA: 0x0032774A File Offset: 0x0032594A
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(3);
			}
			set
			{
				base.SetElement<TextBody>(3, value);
			}
		}

		// Token: 0x1700745C RID: 29788
		// (get) Token: 0x06016305 RID: 90885 RVA: 0x0031FB9F File Offset: 0x0031DD9F
		// (set) Token: 0x06016306 RID: 90886 RVA: 0x0031FBA8 File Offset: 0x0031DDA8
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(4);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(4, value);
			}
		}

		// Token: 0x06016307 RID: 90887 RVA: 0x00327754 File Offset: 0x00325954
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "useBgFill" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016308 RID: 90888 RVA: 0x00327774 File Offset: 0x00325974
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x06016309 RID: 90889 RVA: 0x00327780 File Offset: 0x00325980
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[1];
			Shape.attributeNamespaceIds = array;
			Shape.eleTagNames = new string[] { "nvSpPr", "spPr", "style", "txBody", "extLst" };
			Shape.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
		}

		// Token: 0x04009698 RID: 38552
		private const string tagName = "sp";

		// Token: 0x04009699 RID: 38553
		private const byte tagNsId = 24;

		// Token: 0x0400969A RID: 38554
		internal const int ElementTypeIdConst = 12329;

		// Token: 0x0400969B RID: 38555
		private static string[] attributeTagNames = new string[] { "useBgFill" };

		// Token: 0x0400969C RID: 38556
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400969D RID: 38557
		private static readonly string[] eleTagNames;

		// Token: 0x0400969E RID: 38558
		private static readonly byte[] eleNamespaceIds;
	}
}
