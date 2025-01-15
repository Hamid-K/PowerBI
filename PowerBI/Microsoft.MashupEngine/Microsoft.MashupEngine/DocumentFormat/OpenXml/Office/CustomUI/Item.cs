using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002288 RID: 8840
	[GeneratedCode("DomGen", "2.0")]
	internal class Item : OpenXmlLeafElement
	{
		// Token: 0x17004001 RID: 16385
		// (get) Token: 0x0600EE2C RID: 60972 RVA: 0x002AD56D File Offset: 0x002AB76D
		public override string LocalName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x17004002 RID: 16386
		// (get) Token: 0x0600EE2D RID: 60973 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17004003 RID: 16387
		// (get) Token: 0x0600EE2E RID: 60974 RVA: 0x002CEC77 File Offset: 0x002CCE77
		internal override int ElementTypeId
		{
			get
			{
				return 12599;
			}
		}

		// Token: 0x0600EE2F RID: 60975 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004004 RID: 16388
		// (get) Token: 0x0600EE30 RID: 60976 RVA: 0x002CEC7E File Offset: 0x002CCE7E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Item.attributeTagNames;
			}
		}

		// Token: 0x17004005 RID: 16389
		// (get) Token: 0x0600EE31 RID: 60977 RVA: 0x002CEC85 File Offset: 0x002CCE85
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Item.attributeNamespaceIds;
			}
		}

		// Token: 0x17004006 RID: 16390
		// (get) Token: 0x0600EE32 RID: 60978 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EE33 RID: 60979 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004007 RID: 16391
		// (get) Token: 0x0600EE34 RID: 60980 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EE35 RID: 60981 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004008 RID: 16392
		// (get) Token: 0x0600EE36 RID: 60982 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EE37 RID: 60983 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004009 RID: 16393
		// (get) Token: 0x0600EE38 RID: 60984 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EE39 RID: 60985 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700400A RID: 16394
		// (get) Token: 0x0600EE3A RID: 60986 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EE3B RID: 60987 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x1700400B RID: 16395
		// (get) Token: 0x0600EE3C RID: 60988 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EE3D RID: 60989 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0600EE3F RID: 60991 RVA: 0x002CEC8C File Offset: 0x002CCE8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EE40 RID: 60992 RVA: 0x002CED25 File Offset: 0x002CCF25
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Item>(deep);
		}

		// Token: 0x0600EE41 RID: 60993 RVA: 0x002CED30 File Offset: 0x002CCF30
		// Note: this type is marked as 'beforefieldinit'.
		static Item()
		{
			byte[] array = new byte[6];
			Item.attributeNamespaceIds = array;
		}

		// Token: 0x04007009 RID: 28681
		private const string tagName = "item";

		// Token: 0x0400700A RID: 28682
		private const byte tagNsId = 34;

		// Token: 0x0400700B RID: 28683
		internal const int ElementTypeIdConst = 12599;

		// Token: 0x0400700C RID: 28684
		private static string[] attributeTagNames = new string[] { "id", "label", "image", "imageMso", "screentip", "supertip" };

		// Token: 0x0400700D RID: 28685
		private static byte[] attributeNamespaceIds;
	}
}
