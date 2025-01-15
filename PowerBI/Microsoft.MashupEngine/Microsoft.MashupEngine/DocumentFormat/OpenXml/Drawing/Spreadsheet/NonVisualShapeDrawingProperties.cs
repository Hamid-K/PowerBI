using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002894 RID: 10388
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067C2 RID: 26562
		// (get) Token: 0x0601466F RID: 83567 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x170067C3 RID: 26563
		// (get) Token: 0x06014670 RID: 83568 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067C4 RID: 26564
		// (get) Token: 0x06014671 RID: 83569 RVA: 0x00312E13 File Offset: 0x00311013
		internal override int ElementTypeId
		{
			get
			{
				return 10749;
			}
		}

		// Token: 0x06014672 RID: 83570 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067C5 RID: 26565
		// (get) Token: 0x06014673 RID: 83571 RVA: 0x00312E1A File Offset: 0x0031101A
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170067C6 RID: 26566
		// (get) Token: 0x06014674 RID: 83572 RVA: 0x00312E21 File Offset: 0x00311021
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170067C7 RID: 26567
		// (get) Token: 0x06014675 RID: 83573 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06014676 RID: 83574 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06014677 RID: 83575 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeDrawingProperties()
		{
		}

		// Token: 0x06014678 RID: 83576 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014679 RID: 83577 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601467A RID: 83578 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601467B RID: 83579 RVA: 0x002DEB16 File Offset: 0x002DCD16
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

		// Token: 0x170067C8 RID: 26568
		// (get) Token: 0x0601467C RID: 83580 RVA: 0x00312E28 File Offset: 0x00311028
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067C9 RID: 26569
		// (get) Token: 0x0601467D RID: 83581 RVA: 0x00312E2F File Offset: 0x0031102F
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067CA RID: 26570
		// (get) Token: 0x0601467E RID: 83582 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067CB RID: 26571
		// (get) Token: 0x0601467F RID: 83583 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x06014680 RID: 83584 RVA: 0x002DEB60 File Offset: 0x002DCD60
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

		// Token: 0x170067CC RID: 26572
		// (get) Token: 0x06014681 RID: 83585 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06014682 RID: 83586 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06014683 RID: 83587 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014684 RID: 83588 RVA: 0x00312E36 File Offset: 0x00311036
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeDrawingProperties>(deep);
		}

		// Token: 0x06014685 RID: 83589 RVA: 0x00312E40 File Offset: 0x00311040
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualShapeDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualShapeDrawingProperties.attributeNamespaceIds = array;
			NonVisualShapeDrawingProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualShapeDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008DF0 RID: 36336
		private const string tagName = "cNvSpPr";

		// Token: 0x04008DF1 RID: 36337
		private const byte tagNsId = 18;

		// Token: 0x04008DF2 RID: 36338
		internal const int ElementTypeIdConst = 10749;

		// Token: 0x04008DF3 RID: 36339
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x04008DF4 RID: 36340
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008DF5 RID: 36341
		private static readonly string[] eleTagNames;

		// Token: 0x04008DF6 RID: 36342
		private static readonly byte[] eleNamespaceIds;
	}
}
