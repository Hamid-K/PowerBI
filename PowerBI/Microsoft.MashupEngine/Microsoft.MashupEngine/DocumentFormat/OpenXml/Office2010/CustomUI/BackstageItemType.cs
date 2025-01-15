using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F7 RID: 8951
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BackstageItemType : OpenXmlLeafElement
	{
		// Token: 0x1700470E RID: 18190
		// (get) Token: 0x0600FD28 RID: 64808 RVA: 0x002DC117 File Offset: 0x002DA317
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageItemType.attributeTagNames;
			}
		}

		// Token: 0x1700470F RID: 18191
		// (get) Token: 0x0600FD29 RID: 64809 RVA: 0x002DC11E File Offset: 0x002DA31E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageItemType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004710 RID: 18192
		// (get) Token: 0x0600FD2A RID: 64810 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FD2B RID: 64811 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004711 RID: 18193
		// (get) Token: 0x0600FD2C RID: 64812 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FD2D RID: 64813 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004712 RID: 18194
		// (get) Token: 0x0600FD2E RID: 64814 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FD2F RID: 64815 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x0600FD30 RID: 64816 RVA: 0x002DC128 File Offset: 0x002DA328
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
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FD32 RID: 64818 RVA: 0x002DC180 File Offset: 0x002DA380
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageItemType()
		{
			byte[] array = new byte[3];
			BackstageItemType.attributeNamespaceIds = array;
		}

		// Token: 0x040071F5 RID: 29173
		private static string[] attributeTagNames = new string[] { "id", "label", "getLabel" };

		// Token: 0x040071F6 RID: 29174
		private static byte[] attributeNamespaceIds;
	}
}
