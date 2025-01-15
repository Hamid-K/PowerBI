using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A33 RID: 10803
	[GeneratedCode("DomGen", "2.0")]
	internal class RgbColor : OpenXmlLeafElement
	{
		// Token: 0x170070DD RID: 28893
		// (get) Token: 0x06015B3B RID: 88891 RVA: 0x0032229A File Offset: 0x0032049A
		public override string LocalName
		{
			get
			{
				return "rgb";
			}
		}

		// Token: 0x170070DE RID: 28894
		// (get) Token: 0x06015B3C RID: 88892 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070DF RID: 28895
		// (get) Token: 0x06015B3D RID: 88893 RVA: 0x003222A1 File Offset: 0x003204A1
		internal override int ElementTypeId
		{
			get
			{
				return 12223;
			}
		}

		// Token: 0x06015B3E RID: 88894 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070E0 RID: 28896
		// (get) Token: 0x06015B3F RID: 88895 RVA: 0x003222A8 File Offset: 0x003204A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return RgbColor.attributeTagNames;
			}
		}

		// Token: 0x170070E1 RID: 28897
		// (get) Token: 0x06015B40 RID: 88896 RVA: 0x003222AF File Offset: 0x003204AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RgbColor.attributeNamespaceIds;
			}
		}

		// Token: 0x170070E2 RID: 28898
		// (get) Token: 0x06015B41 RID: 88897 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06015B42 RID: 88898 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public Int32Value Red
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170070E3 RID: 28899
		// (get) Token: 0x06015B43 RID: 88899 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06015B44 RID: 88900 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "g")]
		public Int32Value Green
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170070E4 RID: 28900
		// (get) Token: 0x06015B45 RID: 88901 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06015B46 RID: 88902 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "b")]
		public Int32Value Blue
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06015B48 RID: 88904 RVA: 0x003222B8 File Offset: 0x003204B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "g" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015B49 RID: 88905 RVA: 0x0032230F File Offset: 0x0032050F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RgbColor>(deep);
		}

		// Token: 0x06015B4A RID: 88906 RVA: 0x00322318 File Offset: 0x00320518
		// Note: this type is marked as 'beforefieldinit'.
		static RgbColor()
		{
			byte[] array = new byte[3];
			RgbColor.attributeNamespaceIds = array;
		}

		// Token: 0x04009476 RID: 38006
		private const string tagName = "rgb";

		// Token: 0x04009477 RID: 38007
		private const byte tagNsId = 24;

		// Token: 0x04009478 RID: 38008
		internal const int ElementTypeIdConst = 12223;

		// Token: 0x04009479 RID: 38009
		private static string[] attributeTagNames = new string[] { "r", "g", "b" };

		// Token: 0x0400947A RID: 38010
		private static byte[] attributeNamespaceIds;
	}
}
