using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FC RID: 9468
	[ChildElementInfo(typeof(ShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualDrawingShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170053B7 RID: 21431
		// (get) Token: 0x06011955 RID: 72021 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x170053B8 RID: 21432
		// (get) Token: 0x06011956 RID: 72022 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053B9 RID: 21433
		// (get) Token: 0x06011957 RID: 72023 RVA: 0x002F01DA File Offset: 0x002EE3DA
		internal override int ElementTypeId
		{
			get
			{
				return 13134;
			}
		}

		// Token: 0x06011958 RID: 72024 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053BA RID: 21434
		// (get) Token: 0x06011959 RID: 72025 RVA: 0x002F01E1 File Offset: 0x002EE3E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingShapeProperties.attributeTagNames;
			}
		}

		// Token: 0x170053BB RID: 21435
		// (get) Token: 0x0601195A RID: 72026 RVA: 0x002F01E8 File Offset: 0x002EE3E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingShapeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170053BC RID: 21436
		// (get) Token: 0x0601195B RID: 72027 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601195C RID: 72028 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601195D RID: 72029 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingShapeProperties()
		{
		}

		// Token: 0x0601195E RID: 72030 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601195F RID: 72031 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011960 RID: 72032 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011961 RID: 72033 RVA: 0x002DEB16 File Offset: 0x002DCD16
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

		// Token: 0x170053BD RID: 21437
		// (get) Token: 0x06011962 RID: 72034 RVA: 0x002F01EF File Offset: 0x002EE3EF
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingShapeProperties.eleTagNames;
			}
		}

		// Token: 0x170053BE RID: 21438
		// (get) Token: 0x06011963 RID: 72035 RVA: 0x002F01F6 File Offset: 0x002EE3F6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170053BF RID: 21439
		// (get) Token: 0x06011964 RID: 72036 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053C0 RID: 21440
		// (get) Token: 0x06011965 RID: 72037 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x06011966 RID: 72038 RVA: 0x002DEB60 File Offset: 0x002DCD60
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

		// Token: 0x170053C1 RID: 21441
		// (get) Token: 0x06011967 RID: 72039 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06011968 RID: 72040 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06011969 RID: 72041 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601196A RID: 72042 RVA: 0x002F01FD File Offset: 0x002EE3FD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingShapeProperties>(deep);
		}

		// Token: 0x0601196B RID: 72043 RVA: 0x002F0208 File Offset: 0x002EE408
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingShapeProperties()
		{
			byte[] array = new byte[1];
			NonVisualDrawingShapeProperties.attributeNamespaceIds = array;
			NonVisualDrawingShapeProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualDrawingShapeProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04007B6A RID: 31594
		private const string tagName = "cNvSpPr";

		// Token: 0x04007B6B RID: 31595
		private const byte tagNsId = 61;

		// Token: 0x04007B6C RID: 31596
		internal const int ElementTypeIdConst = 13134;

		// Token: 0x04007B6D RID: 31597
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x04007B6E RID: 31598
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B6F RID: 31599
		private static readonly string[] eleTagNames;

		// Token: 0x04007B70 RID: 31600
		private static readonly byte[] eleNamespaceIds;
	}
}
