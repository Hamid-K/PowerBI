using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5E RID: 10846
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700723E RID: 29246
		// (get) Token: 0x06015E3C RID: 89660 RVA: 0x002DEAFA File Offset: 0x002DCCFA
		public override string LocalName
		{
			get
			{
				return "cNvSpPr";
			}
		}

		// Token: 0x1700723F RID: 29247
		// (get) Token: 0x06015E3D RID: 89661 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007240 RID: 29248
		// (get) Token: 0x06015E3E RID: 89662 RVA: 0x003241E2 File Offset: 0x003223E2
		internal override int ElementTypeId
		{
			get
			{
				return 12264;
			}
		}

		// Token: 0x06015E3F RID: 89663 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007241 RID: 29249
		// (get) Token: 0x06015E40 RID: 89664 RVA: 0x003241E9 File Offset: 0x003223E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17007242 RID: 29250
		// (get) Token: 0x06015E41 RID: 89665 RVA: 0x003241F0 File Offset: 0x003223F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007243 RID: 29251
		// (get) Token: 0x06015E42 RID: 89666 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015E43 RID: 89667 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015E44 RID: 89668 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualShapeDrawingProperties()
		{
		}

		// Token: 0x06015E45 RID: 89669 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E46 RID: 89670 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E47 RID: 89671 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E48 RID: 89672 RVA: 0x002DEB16 File Offset: 0x002DCD16
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

		// Token: 0x17007244 RID: 29252
		// (get) Token: 0x06015E49 RID: 89673 RVA: 0x003241F7 File Offset: 0x003223F7
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17007245 RID: 29253
		// (get) Token: 0x06015E4A RID: 89674 RVA: 0x003241FE File Offset: 0x003223FE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007246 RID: 29254
		// (get) Token: 0x06015E4B RID: 89675 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007247 RID: 29255
		// (get) Token: 0x06015E4C RID: 89676 RVA: 0x002DEB57 File Offset: 0x002DCD57
		// (set) Token: 0x06015E4D RID: 89677 RVA: 0x002DEB60 File Offset: 0x002DCD60
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

		// Token: 0x17007248 RID: 29256
		// (get) Token: 0x06015E4E RID: 89678 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06015E4F RID: 89679 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06015E50 RID: 89680 RVA: 0x002DEB7D File Offset: 0x002DCD7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "txBox" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015E51 RID: 89681 RVA: 0x00324205 File Offset: 0x00322405
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualShapeDrawingProperties>(deep);
		}

		// Token: 0x06015E52 RID: 89682 RVA: 0x00324210 File Offset: 0x00322410
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualShapeDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualShapeDrawingProperties.attributeNamespaceIds = array;
			NonVisualShapeDrawingProperties.eleTagNames = new string[] { "spLocks", "extLst" };
			NonVisualShapeDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04009548 RID: 38216
		private const string tagName = "cNvSpPr";

		// Token: 0x04009549 RID: 38217
		private const byte tagNsId = 24;

		// Token: 0x0400954A RID: 38218
		internal const int ElementTypeIdConst = 12264;

		// Token: 0x0400954B RID: 38219
		private static string[] attributeTagNames = new string[] { "txBox" };

		// Token: 0x0400954C RID: 38220
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400954D RID: 38221
		private static readonly string[] eleTagNames;

		// Token: 0x0400954E RID: 38222
		private static readonly byte[] eleNamespaceIds;
	}
}
