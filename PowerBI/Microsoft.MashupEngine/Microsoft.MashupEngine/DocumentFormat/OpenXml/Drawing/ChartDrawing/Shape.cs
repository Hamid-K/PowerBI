using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002629 RID: 9769
	[ChildElementInfo(typeof(Style))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualShapeProperties))]
	[ChildElementInfo(typeof(TextBody))]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x170059F7 RID: 23031
		// (get) Token: 0x06012725 RID: 75557 RVA: 0x002DF64E File Offset: 0x002DD84E
		public override string LocalName
		{
			get
			{
				return "sp";
			}
		}

		// Token: 0x170059F8 RID: 23032
		// (get) Token: 0x06012726 RID: 75558 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170059F9 RID: 23033
		// (get) Token: 0x06012727 RID: 75559 RVA: 0x002FB400 File Offset: 0x002F9600
		internal override int ElementTypeId
		{
			get
			{
				return 10588;
			}
		}

		// Token: 0x06012728 RID: 75560 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170059FA RID: 23034
		// (get) Token: 0x06012729 RID: 75561 RVA: 0x002FB407 File Offset: 0x002F9607
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x170059FB RID: 23035
		// (get) Token: 0x0601272A RID: 75562 RVA: 0x002FB40E File Offset: 0x002F960E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x170059FC RID: 23036
		// (get) Token: 0x0601272B RID: 75563 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601272C RID: 75564 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170059FD RID: 23037
		// (get) Token: 0x0601272D RID: 75565 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601272E RID: 75566 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "textlink")]
		public StringValue TextLink
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170059FE RID: 23038
		// (get) Token: 0x0601272F RID: 75567 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06012730 RID: 75568 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fLocksText")]
		public BooleanValue LockText
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170059FF RID: 23039
		// (get) Token: 0x06012731 RID: 75569 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06012732 RID: 75570 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06012733 RID: 75571 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x06012734 RID: 75572 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012735 RID: 75573 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012736 RID: 75574 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012737 RID: 75575 RVA: 0x002FB418 File Offset: 0x002F9618
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "nvSpPr" == name)
			{
				return new NonVisualShapeProperties();
			}
			if (12 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (12 == namespaceId && "style" == name)
			{
				return new Style();
			}
			if (12 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			return null;
		}

		// Token: 0x17005A00 RID: 23040
		// (get) Token: 0x06012738 RID: 75576 RVA: 0x002FB486 File Offset: 0x002F9686
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x17005A01 RID: 23041
		// (get) Token: 0x06012739 RID: 75577 RVA: 0x002FB48D File Offset: 0x002F968D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x17005A02 RID: 23042
		// (get) Token: 0x0601273A RID: 75578 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A03 RID: 23043
		// (get) Token: 0x0601273B RID: 75579 RVA: 0x002FB494 File Offset: 0x002F9694
		// (set) Token: 0x0601273C RID: 75580 RVA: 0x002FB49D File Offset: 0x002F969D
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

		// Token: 0x17005A04 RID: 23044
		// (get) Token: 0x0601273D RID: 75581 RVA: 0x002FB4A7 File Offset: 0x002F96A7
		// (set) Token: 0x0601273E RID: 75582 RVA: 0x002FB4B0 File Offset: 0x002F96B0
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

		// Token: 0x17005A05 RID: 23045
		// (get) Token: 0x0601273F RID: 75583 RVA: 0x002FB4BA File Offset: 0x002F96BA
		// (set) Token: 0x06012740 RID: 75584 RVA: 0x002FB4C3 File Offset: 0x002F96C3
		public Style Style
		{
			get
			{
				return base.GetElement<Style>(2);
			}
			set
			{
				base.SetElement<Style>(2, value);
			}
		}

		// Token: 0x17005A06 RID: 23046
		// (get) Token: 0x06012741 RID: 75585 RVA: 0x002FB4CD File Offset: 0x002F96CD
		// (set) Token: 0x06012742 RID: 75586 RVA: 0x002FB4D6 File Offset: 0x002F96D6
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

		// Token: 0x06012743 RID: 75587 RVA: 0x002FB4E0 File Offset: 0x002F96E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "textlink" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fLocksText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012744 RID: 75588 RVA: 0x002FB54D File Offset: 0x002F974D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x06012745 RID: 75589 RVA: 0x002FB558 File Offset: 0x002F9758
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[4];
			Shape.attributeNamespaceIds = array;
			Shape.eleTagNames = new string[] { "nvSpPr", "spPr", "style", "txBody" };
			Shape.eleNamespaceIds = new byte[] { 12, 12, 12, 12 };
		}

		// Token: 0x0400802F RID: 32815
		private const string tagName = "sp";

		// Token: 0x04008030 RID: 32816
		private const byte tagNsId = 12;

		// Token: 0x04008031 RID: 32817
		internal const int ElementTypeIdConst = 10588;

		// Token: 0x04008032 RID: 32818
		private static string[] attributeTagNames = new string[] { "macro", "textlink", "fLocksText", "fPublished" };

		// Token: 0x04008033 RID: 32819
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008034 RID: 32820
		private static readonly string[] eleTagNames;

		// Token: 0x04008035 RID: 32821
		private static readonly byte[] eleNamespaceIds;
	}
}
