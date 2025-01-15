using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB7 RID: 10935
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PhotoAlbum : OpenXmlCompositeElement
	{
		// Token: 0x170074DA RID: 29914
		// (get) Token: 0x06016432 RID: 91186 RVA: 0x00328490 File Offset: 0x00326690
		public override string LocalName
		{
			get
			{
				return "photoAlbum";
			}
		}

		// Token: 0x170074DB RID: 29915
		// (get) Token: 0x06016433 RID: 91187 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074DC RID: 29916
		// (get) Token: 0x06016434 RID: 91188 RVA: 0x00328497 File Offset: 0x00326697
		internal override int ElementTypeId
		{
			get
			{
				return 12350;
			}
		}

		// Token: 0x06016435 RID: 91189 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074DD RID: 29917
		// (get) Token: 0x06016436 RID: 91190 RVA: 0x0032849E File Offset: 0x0032669E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PhotoAlbum.attributeTagNames;
			}
		}

		// Token: 0x170074DE RID: 29918
		// (get) Token: 0x06016437 RID: 91191 RVA: 0x003284A5 File Offset: 0x003266A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PhotoAlbum.attributeNamespaceIds;
			}
		}

		// Token: 0x170074DF RID: 29919
		// (get) Token: 0x06016438 RID: 91192 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016439 RID: 91193 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bw")]
		public BooleanValue BlackWhite
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

		// Token: 0x170074E0 RID: 29920
		// (get) Token: 0x0601643A RID: 91194 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601643B RID: 91195 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showCaptions")]
		public BooleanValue ShowCaptions
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170074E1 RID: 29921
		// (get) Token: 0x0601643C RID: 91196 RVA: 0x003284AC File Offset: 0x003266AC
		// (set) Token: 0x0601643D RID: 91197 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "layout")]
		public EnumValue<PhotoAlbumLayoutValues> Layout
		{
			get
			{
				return (EnumValue<PhotoAlbumLayoutValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170074E2 RID: 29922
		// (get) Token: 0x0601643E RID: 91198 RVA: 0x003284BB File Offset: 0x003266BB
		// (set) Token: 0x0601643F RID: 91199 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "frame")]
		public EnumValue<PhotoAlbumFrameShapeValues> Frame
		{
			get
			{
				return (EnumValue<PhotoAlbumFrameShapeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06016440 RID: 91200 RVA: 0x00293ECF File Offset: 0x002920CF
		public PhotoAlbum()
		{
		}

		// Token: 0x06016441 RID: 91201 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PhotoAlbum(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016442 RID: 91202 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PhotoAlbum(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016443 RID: 91203 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PhotoAlbum(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016444 RID: 91204 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170074E3 RID: 29923
		// (get) Token: 0x06016445 RID: 91205 RVA: 0x003284CA File Offset: 0x003266CA
		internal override string[] ElementTagNames
		{
			get
			{
				return PhotoAlbum.eleTagNames;
			}
		}

		// Token: 0x170074E4 RID: 29924
		// (get) Token: 0x06016446 RID: 91206 RVA: 0x003284D1 File Offset: 0x003266D1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PhotoAlbum.eleNamespaceIds;
			}
		}

		// Token: 0x170074E5 RID: 29925
		// (get) Token: 0x06016447 RID: 91207 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170074E6 RID: 29926
		// (get) Token: 0x06016448 RID: 91208 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06016449 RID: 91209 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601644A RID: 91210 RVA: 0x003284D8 File Offset: 0x003266D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bw" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showCaptions" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "layout" == name)
			{
				return new EnumValue<PhotoAlbumLayoutValues>();
			}
			if (namespaceId == 0 && "frame" == name)
			{
				return new EnumValue<PhotoAlbumFrameShapeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601644B RID: 91211 RVA: 0x00328545 File Offset: 0x00326745
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhotoAlbum>(deep);
		}

		// Token: 0x0601644C RID: 91212 RVA: 0x00328550 File Offset: 0x00326750
		// Note: this type is marked as 'beforefieldinit'.
		static PhotoAlbum()
		{
			byte[] array = new byte[4];
			PhotoAlbum.attributeNamespaceIds = array;
			PhotoAlbum.eleTagNames = new string[] { "extLst" };
			PhotoAlbum.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040096EF RID: 38639
		private const string tagName = "photoAlbum";

		// Token: 0x040096F0 RID: 38640
		private const byte tagNsId = 24;

		// Token: 0x040096F1 RID: 38641
		internal const int ElementTypeIdConst = 12350;

		// Token: 0x040096F2 RID: 38642
		private static string[] attributeTagNames = new string[] { "bw", "showCaptions", "layout", "frame" };

		// Token: 0x040096F3 RID: 38643
		private static byte[] attributeNamespaceIds;

		// Token: 0x040096F4 RID: 38644
		private static readonly string[] eleTagNames;

		// Token: 0x040096F5 RID: 38645
		private static readonly byte[] eleNamespaceIds;
	}
}
