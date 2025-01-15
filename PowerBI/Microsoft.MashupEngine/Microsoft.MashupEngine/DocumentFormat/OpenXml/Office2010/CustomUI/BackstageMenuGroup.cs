using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FC RID: 8956
	[ChildElementInfo(typeof(BackstageSubMenu), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageMenuButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageMenuToggleButton), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackstageMenuCheckBox), FileFormatVersions.Office2010)]
	internal class BackstageMenuGroup : OpenXmlCompositeElement
	{
		// Token: 0x17004749 RID: 18249
		// (get) Token: 0x0600FDA3 RID: 64931 RVA: 0x002DC736 File Offset: 0x002DA936
		public override string LocalName
		{
			get
			{
				return "menuGroup";
			}
		}

		// Token: 0x1700474A RID: 18250
		// (get) Token: 0x0600FDA4 RID: 64932 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700474B RID: 18251
		// (get) Token: 0x0600FDA5 RID: 64933 RVA: 0x002DC73D File Offset: 0x002DA93D
		internal override int ElementTypeId
		{
			get
			{
				return 13099;
			}
		}

		// Token: 0x0600FDA6 RID: 64934 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700474C RID: 18252
		// (get) Token: 0x0600FDA7 RID: 64935 RVA: 0x002DC744 File Offset: 0x002DA944
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageMenuGroup.attributeTagNames;
			}
		}

		// Token: 0x1700474D RID: 18253
		// (get) Token: 0x0600FDA8 RID: 64936 RVA: 0x002DC74B File Offset: 0x002DA94B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageMenuGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x1700474E RID: 18254
		// (get) Token: 0x0600FDA9 RID: 64937 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FDAA RID: 64938 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700474F RID: 18255
		// (get) Token: 0x0600FDAB RID: 64939 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FDAC RID: 64940 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004750 RID: 18256
		// (get) Token: 0x0600FDAD RID: 64941 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FDAE RID: 64942 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004751 RID: 18257
		// (get) Token: 0x0600FDAF RID: 64943 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FDB0 RID: 64944 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004752 RID: 18258
		// (get) Token: 0x0600FDB1 RID: 64945 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FDB2 RID: 64946 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004753 RID: 18259
		// (get) Token: 0x0600FDB3 RID: 64947 RVA: 0x002DC752 File Offset: 0x002DA952
		// (set) Token: 0x0600FDB4 RID: 64948 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "itemSize")]
		public EnumValue<ItemSizeValues> ItemSize
		{
			get
			{
				return (EnumValue<ItemSizeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0600FDB5 RID: 64949 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageMenuGroup()
		{
		}

		// Token: 0x0600FDB6 RID: 64950 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageMenuGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDB7 RID: 64951 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageMenuGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDB8 RID: 64952 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageMenuGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FDB9 RID: 64953 RVA: 0x002DC764 File Offset: 0x002DA964
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "button" == name)
			{
				return new BackstageMenuButton();
			}
			if (57 == namespaceId && "checkBox" == name)
			{
				return new BackstageMenuCheckBox();
			}
			if (57 == namespaceId && "menu" == name)
			{
				return new BackstageSubMenu();
			}
			if (57 == namespaceId && "toggleButton" == name)
			{
				return new BackstageMenuToggleButton();
			}
			return null;
		}

		// Token: 0x0600FDBA RID: 64954 RVA: 0x002DC7D4 File Offset: 0x002DA9D4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "itemSize" == name)
			{
				return new EnumValue<ItemSizeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FDBB RID: 64955 RVA: 0x002DC86D File Offset: 0x002DAA6D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageMenuGroup>(deep);
		}

		// Token: 0x0600FDBC RID: 64956 RVA: 0x002DC878 File Offset: 0x002DAA78
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageMenuGroup()
		{
			byte[] array = new byte[6];
			BackstageMenuGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04007207 RID: 29191
		private const string tagName = "menuGroup";

		// Token: 0x04007208 RID: 29192
		private const byte tagNsId = 57;

		// Token: 0x04007209 RID: 29193
		internal const int ElementTypeIdConst = 13099;

		// Token: 0x0400720A RID: 29194
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "label", "getLabel", "itemSize" };

		// Token: 0x0400720B RID: 29195
		private static byte[] attributeNamespaceIds;
	}
}
