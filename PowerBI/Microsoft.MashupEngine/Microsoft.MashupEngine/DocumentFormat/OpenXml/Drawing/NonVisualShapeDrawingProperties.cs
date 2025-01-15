using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A5 RID: 10149
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeLocks))]
	internal class NonVisualShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700629F RID: 25247
		// (get) Token: 0x06013A8F RID: 80527 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x170062A0 RID: 25248
		// (get) Token: 0x06013A90 RID: 80528 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062A1 RID: 25249
		// (get) Token: 0x06013A91 RID: 80529 RVA: 0x0030A656 File Offset: 0x00308856
		internal override int ElementTypeId
		{
			get
			{
				return 10182;
			}
		}

		// Token: 0x06013A92 RID: 80530 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170062A2 RID: 25250
		// (get) Token: 0x06013A93 RID: 80531 RVA: 0x0030A65D File Offset: 0x0030885D
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170062A3 RID: 25251
		// (get) Token: 0x06013A94 RID: 80532 RVA: 0x0030A664 File Offset: 0x00308864
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170062A4 RID: 25252
		// (get) Token: 0x06013A95 RID: 80533 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013A96 RID: 80534 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "txBox")]
		public BooleanValue TextBox
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

		// Token: 0x06013A97 RID: 80535 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeDrawingProperties()
		{
		}

		// Token: 0x06013A98 RID: 80536 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A99 RID: 80537 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A9A RID: 80538 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013A9B RID: 80539 RVA: 0x002DEB16 File Offset: 0x002DCD16
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "spLocks" == name)
			{
				return new ShapeLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170062A5 RID: 25253
		// (get) Token: 0x06013A9C RID: 80540 RVA: 0x0030A66B File Offset: 0x0030886B
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170062A6 RID: 25254
		// (get) Token: 0x06013A9D RID: 80541 RVA: 0x0030A672 File Offset: 0x00308872
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062A7 RID: 25255
		// (get) Token: 0x06013A9E RID: 80542 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062A8 RID: 25256
		// (get) Token: 0x06013A9F RID: 80543 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x06013AA0 RID: 80544 RVA: 0x002DEB60 File Offset: 0x002DCD60
		public ShapeLocks ShapeLocks
		{
			get
			{
				return base.GetElement<ShapeLocks>(0);
			}
			set
			{
				base.SetElement<ShapeLocks>(0, value);
			}
		}

		// Token: 0x170062A9 RID: 25257
		// (get) Token: 0x06013AA1 RID: 80545 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06013AA2 RID: 80546 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06013AA3 RID: 80547 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013AA4 RID: 80548 RVA: 0x0030A679 File Offset: 0x00308879
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeDrawingProperties>(deep);
		}

		// Token: 0x06013AA5 RID: 80549 RVA: 0x0030A684 File Offset: 0x00308884
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualShapeDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualShapeDrawingProperties.attributeNamespaceIds = array;
			NonVisualShapeDrawingProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualShapeDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400872D RID: 34605
		private const string tagName = "cNvSpPr";

		// Token: 0x0400872E RID: 34606
		private const byte tagNsId = 10;

		// Token: 0x0400872F RID: 34607
		internal const int ElementTypeIdConst = 10182;

		// Token: 0x04008730 RID: 34608
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x04008731 RID: 34609
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008732 RID: 34610
		private static readonly string[] eleTagNames;

		// Token: 0x04008733 RID: 34611
		private static readonly byte[] eleNamespaceIds;
	}
}
