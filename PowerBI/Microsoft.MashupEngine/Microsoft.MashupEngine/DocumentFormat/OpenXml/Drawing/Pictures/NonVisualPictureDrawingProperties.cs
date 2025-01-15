using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002872 RID: 10354
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureLocks))]
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtensionList))]
	internal class NonVisualPictureDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700669B RID: 26267
		// (get) Token: 0x060143E8 RID: 82920 RVA: 0x002FC3A1 File Offset: 0x002FA5A1
		public override string LocalName
		{
			get
			{
				return "cNvPicPr";
			}
		}

		// Token: 0x1700669C RID: 26268
		// (get) Token: 0x060143E9 RID: 82921 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x1700669D RID: 26269
		// (get) Token: 0x060143EA RID: 82922 RVA: 0x00310DB2 File Offset: 0x0030EFB2
		internal override int ElementTypeId
		{
			get
			{
				return 10716;
			}
		}

		// Token: 0x060143EB RID: 82923 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700669E RID: 26270
		// (get) Token: 0x060143EC RID: 82924 RVA: 0x00310DB9 File Offset: 0x0030EFB9
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700669F RID: 26271
		// (get) Token: 0x060143ED RID: 82925 RVA: 0x00310DC0 File Offset: 0x0030EFC0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170066A0 RID: 26272
		// (get) Token: 0x060143EE RID: 82926 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060143EF RID: 82927 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060143F0 RID: 82928 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPictureDrawingProperties()
		{
		}

		// Token: 0x060143F1 RID: 82929 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPictureDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143F2 RID: 82930 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPictureDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143F3 RID: 82931 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPictureDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060143F4 RID: 82932 RVA: 0x002FC3BD File Offset: 0x002FA5BD
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

		// Token: 0x170066A1 RID: 26273
		// (get) Token: 0x060143F5 RID: 82933 RVA: 0x00310DC7 File Offset: 0x0030EFC7
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170066A2 RID: 26274
		// (get) Token: 0x060143F6 RID: 82934 RVA: 0x00310DCE File Offset: 0x0030EFCE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualPictureDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170066A3 RID: 26275
		// (get) Token: 0x060143F7 RID: 82935 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066A4 RID: 26276
		// (get) Token: 0x060143F8 RID: 82936 RVA: 0x002FC3FE File Offset: 0x002FA5FE
		// (set) Token: 0x060143F9 RID: 82937 RVA: 0x002FC407 File Offset: 0x002FA607
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

		// Token: 0x170066A5 RID: 26277
		// (get) Token: 0x060143FA RID: 82938 RVA: 0x002FC411 File Offset: 0x002FA611
		// (set) Token: 0x060143FB RID: 82939 RVA: 0x002FC41A File Offset: 0x002FA61A
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

		// Token: 0x060143FC RID: 82940 RVA: 0x002FC424 File Offset: 0x002FA624
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preferRelativeResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060143FD RID: 82941 RVA: 0x00310DD5 File Offset: 0x0030EFD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPictureDrawingProperties>(deep);
		}

		// Token: 0x060143FE RID: 82942 RVA: 0x00310DE0 File Offset: 0x0030EFE0
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualPictureDrawingProperties()
		{
			byte[] array = new byte[1];
			NonVisualPictureDrawingProperties.attributeNamespaceIds = array;
			NonVisualPictureDrawingProperties.eleTagNames = new string[] { "picLocks", "extLst" };
			NonVisualPictureDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008D3D RID: 36157
		private const string tagName = "cNvPicPr";

		// Token: 0x04008D3E RID: 36158
		private const byte tagNsId = 17;

		// Token: 0x04008D3F RID: 36159
		internal const int ElementTypeIdConst = 10716;

		// Token: 0x04008D40 RID: 36160
		private static string[] attributeTagNames = new string[] { "preferRelativeResize" };

		// Token: 0x04008D41 RID: 36161
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D42 RID: 36162
		private static readonly string[] eleTagNames;

		// Token: 0x04008D43 RID: 36163
		private static readonly byte[] eleNamespaceIds;
	}
}
