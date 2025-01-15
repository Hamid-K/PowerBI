using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262F RID: 9775
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeLocks))]
	internal class NonVisualShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A47 RID: 23111
		// (get) Token: 0x060127CC RID: 75724 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x17005A48 RID: 23112
		// (get) Token: 0x060127CD RID: 75725 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A49 RID: 23113
		// (get) Token: 0x060127CE RID: 75726 RVA: 0x002FBCA6 File Offset: 0x002F9EA6
		internal override int ElementTypeId
		{
			get
			{
				return 10594;
			}
		}

		// Token: 0x060127CF RID: 75727 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A4A RID: 23114
		// (get) Token: 0x060127D0 RID: 75728 RVA: 0x002FBCAD File Offset: 0x002F9EAD
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17005A4B RID: 23115
		// (get) Token: 0x060127D1 RID: 75729 RVA: 0x002FBCB4 File Offset: 0x002F9EB4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A4C RID: 23116
		// (get) Token: 0x060127D2 RID: 75730 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060127D3 RID: 75731 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060127D4 RID: 75732 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeDrawingProperties()
		{
		}

		// Token: 0x060127D5 RID: 75733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127D6 RID: 75734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127D7 RID: 75735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060127D8 RID: 75736 RVA: 0x002DEB16 File Offset: 0x002DCD16
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

		// Token: 0x17005A4D RID: 23117
		// (get) Token: 0x060127D9 RID: 75737 RVA: 0x002FBCBB File Offset: 0x002F9EBB
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005A4E RID: 23118
		// (get) Token: 0x060127DA RID: 75738 RVA: 0x002FBCC2 File Offset: 0x002F9EC2
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A4F RID: 23119
		// (get) Token: 0x060127DB RID: 75739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A50 RID: 23120
		// (get) Token: 0x060127DC RID: 75740 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x060127DD RID: 75741 RVA: 0x002DEB60 File Offset: 0x002DCD60
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

		// Token: 0x17005A51 RID: 23121
		// (get) Token: 0x060127DE RID: 75742 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060127DF RID: 75743 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060127E0 RID: 75744 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060127E1 RID: 75745 RVA: 0x002FBCC9 File Offset: 0x002F9EC9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeDrawingProperties>(deep);
		}

		// Token: 0x060127E2 RID: 75746 RVA: 0x002FBCD4 File Offset: 0x002F9ED4
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualShapeDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualShapeDrawingProperties.attributeNamespaceIds = array;
			NonVisualShapeDrawingProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualShapeDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008057 RID: 32855
		private const string tagName = "cNvSpPr";

		// Token: 0x04008058 RID: 32856
		private const byte tagNsId = 12;

		// Token: 0x04008059 RID: 32857
		internal const int ElementTypeIdConst = 10594;

		// Token: 0x0400805A RID: 32858
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x0400805B RID: 32859
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400805C RID: 32860
		private static readonly string[] eleTagNames;

		// Token: 0x0400805D RID: 32861
		private static readonly byte[] eleNamespaceIds;
	}
}
