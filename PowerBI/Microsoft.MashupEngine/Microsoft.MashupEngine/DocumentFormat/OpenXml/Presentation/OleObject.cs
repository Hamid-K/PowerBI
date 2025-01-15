using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FC RID: 10748
	[ChildElementInfo(typeof(OleObjectEmbed))]
	[ChildElementInfo(typeof(OleObjectLink))]
	[ChildElementInfo(typeof(Picture))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OleObject : OpenXmlCompositeElement
	{
		// Token: 0x17006EC2 RID: 28354
		// (get) Token: 0x0601568E RID: 87694 RVA: 0x0031EAA0 File Offset: 0x0031CCA0
		public override string LocalName
		{
			get
			{
				return "oleObj";
			}
		}

		// Token: 0x17006EC3 RID: 28355
		// (get) Token: 0x0601568F RID: 87695 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EC4 RID: 28356
		// (get) Token: 0x06015690 RID: 87696 RVA: 0x0031EAA7 File Offset: 0x0031CCA7
		internal override int ElementTypeId
		{
			get
			{
				return 12175;
			}
		}

		// Token: 0x06015691 RID: 87697 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006EC5 RID: 28357
		// (get) Token: 0x06015692 RID: 87698 RVA: 0x0031EAAE File Offset: 0x0031CCAE
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleObject.attributeTagNames;
			}
		}

		// Token: 0x17006EC6 RID: 28358
		// (get) Token: 0x06015693 RID: 87699 RVA: 0x0031EAB5 File Offset: 0x0031CCB5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17006EC7 RID: 28359
		// (get) Token: 0x06015694 RID: 87700 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015695 RID: 87701 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
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

		// Token: 0x17006EC8 RID: 28360
		// (get) Token: 0x06015696 RID: 87702 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015697 RID: 87703 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17006EC9 RID: 28361
		// (get) Token: 0x06015698 RID: 87704 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015699 RID: 87705 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showAsIcon")]
		public BooleanValue ShowAsIcon
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

		// Token: 0x17006ECA RID: 28362
		// (get) Token: 0x0601569A RID: 87706 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601569B RID: 87707 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006ECB RID: 28363
		// (get) Token: 0x0601569C RID: 87708 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0601569D RID: 87709 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "imgW")]
		public Int32Value ImageWidth
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006ECC RID: 28364
		// (get) Token: 0x0601569E RID: 87710 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x0601569F RID: 87711 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "imgH")]
		public Int32Value ImageHeight
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17006ECD RID: 28365
		// (get) Token: 0x060156A0 RID: 87712 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x060156A1 RID: 87713 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "progId")]
		public StringValue ProgId
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x060156A2 RID: 87714 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObject()
		{
		}

		// Token: 0x060156A3 RID: 87715 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060156A4 RID: 87716 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060156A5 RID: 87717 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060156A6 RID: 87718 RVA: 0x0031EABC File Offset: 0x0031CCBC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "embed" == name)
			{
				return new OleObjectEmbed();
			}
			if (24 == namespaceId && "link" == name)
			{
				return new OleObjectLink();
			}
			if (24 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			return null;
		}

		// Token: 0x060156A7 RID: 87719 RVA: 0x0031EB14 File Offset: 0x0031CD14
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showAsIcon" == name)
			{
				return new BooleanValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imgW" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "imgH" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "progId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060156A8 RID: 87720 RVA: 0x0031EBC5 File Offset: 0x0031CDC5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObject>(deep);
		}

		// Token: 0x060156A9 RID: 87721 RVA: 0x0031EBD0 File Offset: 0x0031CDD0
		// Note: this type is marked as 'beforefieldinit'.
		static OleObject()
		{
			byte[] array = new byte[7];
			array[3] = 19;
			OleObject.attributeNamespaceIds = array;
		}

		// Token: 0x04009361 RID: 37729
		private const string tagName = "oleObj";

		// Token: 0x04009362 RID: 37730
		private const byte tagNsId = 24;

		// Token: 0x04009363 RID: 37731
		internal const int ElementTypeIdConst = 12175;

		// Token: 0x04009364 RID: 37732
		private static string[] attributeTagNames = new string[] { "spid", "name", "showAsIcon", "id", "imgW", "imgH", "progId" };

		// Token: 0x04009365 RID: 37733
		private static byte[] attributeNamespaceIds;
	}
}
