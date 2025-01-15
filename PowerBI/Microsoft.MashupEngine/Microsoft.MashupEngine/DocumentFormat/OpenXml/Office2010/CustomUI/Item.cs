using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E6 RID: 8934
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Item : OpenXmlLeafElement
	{
		// Token: 0x1700462E RID: 17966
		// (get) Token: 0x0600FB41 RID: 64321 RVA: 0x002AD56D File Offset: 0x002AB76D
		public override string LocalName
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x1700462F RID: 17967
		// (get) Token: 0x0600FB42 RID: 64322 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004630 RID: 17968
		// (get) Token: 0x0600FB43 RID: 64323 RVA: 0x002DA70A File Offset: 0x002D890A
		internal override int ElementTypeId
		{
			get
			{
				return 13079;
			}
		}

		// Token: 0x0600FB44 RID: 64324 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004631 RID: 17969
		// (get) Token: 0x0600FB45 RID: 64325 RVA: 0x002DA711 File Offset: 0x002D8911
		internal override string[] AttributeTagNames
		{
			get
			{
				return Item.attributeTagNames;
			}
		}

		// Token: 0x17004632 RID: 17970
		// (get) Token: 0x0600FB46 RID: 64326 RVA: 0x002DA718 File Offset: 0x002D8918
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Item.attributeNamespaceIds;
			}
		}

		// Token: 0x17004633 RID: 17971
		// (get) Token: 0x0600FB47 RID: 64327 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FB48 RID: 64328 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004634 RID: 17972
		// (get) Token: 0x0600FB49 RID: 64329 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FB4A RID: 64330 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004635 RID: 17973
		// (get) Token: 0x0600FB4B RID: 64331 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FB4C RID: 64332 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004636 RID: 17974
		// (get) Token: 0x0600FB4D RID: 64333 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FB4E RID: 64334 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004637 RID: 17975
		// (get) Token: 0x0600FB4F RID: 64335 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FB50 RID: 64336 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004638 RID: 17976
		// (get) Token: 0x0600FB51 RID: 64337 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FB52 RID: 64338 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x0600FB54 RID: 64340 RVA: 0x002DA720 File Offset: 0x002D8920
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

		// Token: 0x0600FB55 RID: 64341 RVA: 0x002DA7B9 File Offset: 0x002D89B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Item>(deep);
		}

		// Token: 0x0600FB56 RID: 64342 RVA: 0x002DA7C4 File Offset: 0x002D89C4
		// Note: this type is marked as 'beforefieldinit'.
		static Item()
		{
			byte[] array = new byte[6];
			Item.attributeNamespaceIds = array;
		}

		// Token: 0x040071AD RID: 29101
		private const string tagName = "item";

		// Token: 0x040071AE RID: 29102
		private const byte tagNsId = 57;

		// Token: 0x040071AF RID: 29103
		internal const int ElementTypeIdConst = 13079;

		// Token: 0x040071B0 RID: 29104
		private static string[] attributeTagNames = new string[] { "id", "label", "image", "imageMso", "screentip", "supertip" };

		// Token: 0x040071B1 RID: 29105
		private static byte[] attributeNamespaceIds;
	}
}
