using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002327 RID: 8999
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualDrawingShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700488A RID: 18570
		// (get) Token: 0x0601006A RID: 65642 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x1700488B RID: 18571
		// (get) Token: 0x0601006B RID: 65643 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x1700488C RID: 18572
		// (get) Token: 0x0601006C RID: 65644 RVA: 0x002DEB01 File Offset: 0x002DCD01
		internal override int ElementTypeId
		{
			get
			{
				return 13022;
			}
		}

		// Token: 0x0601006D RID: 65645 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700488D RID: 18573
		// (get) Token: 0x0601006E RID: 65646 RVA: 0x002DEB08 File Offset: 0x002DCD08
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x1700488E RID: 18574
		// (get) Token: 0x0601006F RID: 65647 RVA: 0x002DEB0F File Offset: 0x002DCD0F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700488F RID: 18575
		// (get) Token: 0x06010070 RID: 65648 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010071 RID: 65649 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010072 RID: 65650 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingShapeProperties()
		{
		}

		// Token: 0x06010073 RID: 65651 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010074 RID: 65652 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010075 RID: 65653 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010076 RID: 65654 RVA: 0x002DEB16 File Offset: 0x002DCD16
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

		// Token: 0x17004890 RID: 18576
		// (get) Token: 0x06010077 RID: 65655 RVA: 0x002DEB49 File Offset: 0x002DCD49
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17004891 RID: 18577
		// (get) Token: 0x06010078 RID: 65656 RVA: 0x002DEB50 File Offset: 0x002DCD50
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004892 RID: 18578
		// (get) Token: 0x06010079 RID: 65657 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004893 RID: 18579
		// (get) Token: 0x0601007A RID: 65658 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x0601007B RID: 65659 RVA: 0x002DEB60 File Offset: 0x002DCD60
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

		// Token: 0x17004894 RID: 18580
		// (get) Token: 0x0601007C RID: 65660 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x0601007D RID: 65661 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x0601007E RID: 65662 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601007F RID: 65663 RVA: 0x002DEB9D File Offset: 0x002DCD9D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingShapeProperties>(deep);
		}

		// Token: 0x06010080 RID: 65664 RVA: 0x002DEBA8 File Offset: 0x002DCDA8
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingShapeProperties()
		{
			byte[] array = new byte[1];
			NonVisualDrawingShapeProperties.attributeNamespaceIds = array;
			NonVisualDrawingShapeProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualDrawingShapeProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040072C1 RID: 29377
		private const string tagName = "cNvSpPr";

		// Token: 0x040072C2 RID: 29378
		private const byte tagNsId = 56;

		// Token: 0x040072C3 RID: 29379
		internal const int ElementTypeIdConst = 13022;

		// Token: 0x040072C4 RID: 29380
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x040072C5 RID: 29381
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072C6 RID: 29382
		private static readonly string[] eleTagNames;

		// Token: 0x040072C7 RID: 29383
		private static readonly byte[] eleNamespaceIds;
	}
}
