using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AC RID: 10156
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureLocks))]
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtensionList))]
	internal class NonVisualPictureDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062DF RID: 25311
		// (get) Token: 0x06013B1B RID: 80667 RVA: 0x002FC3A1 File Offset: 0x002FA5A1
		public override string LocalName
		{
			get
			{
				return "cNvPicPr";
			}
		}

		// Token: 0x170062E0 RID: 25312
		// (get) Token: 0x06013B1C RID: 80668 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062E1 RID: 25313
		// (get) Token: 0x06013B1D RID: 80669 RVA: 0x0030AD75 File Offset: 0x00308F75
		internal override int ElementTypeId
		{
			get
			{
				return 10189;
			}
		}

		// Token: 0x06013B1E RID: 80670 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170062E2 RID: 25314
		// (get) Token: 0x06013B1F RID: 80671 RVA: 0x0030AD7C File Offset: 0x00308F7C
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170062E3 RID: 25315
		// (get) Token: 0x06013B20 RID: 80672 RVA: 0x0030AD83 File Offset: 0x00308F83
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170062E4 RID: 25316
		// (get) Token: 0x06013B21 RID: 80673 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013B22 RID: 80674 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "preferRelativeResize")]
		public BooleanValue PreferRelativeResize
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

		// Token: 0x06013B23 RID: 80675 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureDrawingProperties()
		{
		}

		// Token: 0x06013B24 RID: 80676 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B25 RID: 80677 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B26 RID: 80678 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B27 RID: 80679 RVA: 0x002FC3BD File Offset: 0x002FA5BD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "picLocks" == name)
			{
				return new PictureLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new NonVisualPicturePropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x170062E5 RID: 25317
		// (get) Token: 0x06013B28 RID: 80680 RVA: 0x0030AD8A File Offset: 0x00308F8A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170062E6 RID: 25318
		// (get) Token: 0x06013B29 RID: 80681 RVA: 0x0030AD91 File Offset: 0x00308F91
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062E7 RID: 25319
		// (get) Token: 0x06013B2A RID: 80682 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062E8 RID: 25320
		// (get) Token: 0x06013B2B RID: 80683 RVA: 0x002FC3FE File Offset: 0x002FA5FE
		// (set) Token: 0x06013B2C RID: 80684 RVA: 0x002FC407 File Offset: 0x002FA607
		public PictureLocks PictureLocks
		{
			get
			{
				return base.GetElement<PictureLocks>(0);
			}
			set
			{
				base.SetElement<PictureLocks>(0, value);
			}
		}

		// Token: 0x170062E9 RID: 25321
		// (get) Token: 0x06013B2D RID: 80685 RVA: 0x002FC411 File Offset: 0x002FA611
		// (set) Token: 0x06013B2E RID: 80686 RVA: 0x002FC41A File Offset: 0x002FA61A
		public NonVisualPicturePropertiesExtensionList NonVisualPicturePropertiesExtensionList
		{
			get
			{
				return base.GetElement<NonVisualPicturePropertiesExtensionList>(1);
			}
			set
			{
				base.SetElement<NonVisualPicturePropertiesExtensionList>(1, value);
			}
		}

		// Token: 0x06013B2F RID: 80687 RVA: 0x002FC424 File Offset: 0x002FA624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preferRelativeResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013B30 RID: 80688 RVA: 0x0030AD98 File Offset: 0x00308F98
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureDrawingProperties>(deep);
		}

		// Token: 0x06013B31 RID: 80689 RVA: 0x0030ADA4 File Offset: 0x00308FA4
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPictureDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualPictureDrawingProperties.attributeNamespaceIds = array;
			NonVisualPictureDrawingProperties.eleTagNames = new string[] { "picLocks", "extLst" };
			NonVisualPictureDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008754 RID: 34644
		private const string tagName = "cNvPicPr";

		// Token: 0x04008755 RID: 34645
		private const byte tagNsId = 10;

		// Token: 0x04008756 RID: 34646
		internal const int ElementTypeIdConst = 10189;

		// Token: 0x04008757 RID: 34647
		private static string[] attributeTagNames = new string[] { "preferRelativeResize" };

		// Token: 0x04008758 RID: 34648
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008759 RID: 34649
		private static readonly string[] eleTagNames;

		// Token: 0x0400875A RID: 34650
		private static readonly byte[] eleNamespaceIds;
	}
}
