using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C5 RID: 8901
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class MenuSeparatorNoTitle : OpenXmlLeafElement
	{
		// Token: 0x170042E4 RID: 17124
		// (get) Token: 0x0600F469 RID: 62569 RVA: 0x002C9E0B File Offset: 0x002C800B
		public override string LocalName
		{
			get
			{
				return "menuSeparator";
			}
		}

		// Token: 0x170042E5 RID: 17125
		// (get) Token: 0x0600F46A RID: 62570 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170042E6 RID: 17126
		// (get) Token: 0x0600F46B RID: 62571 RVA: 0x002D418E File Offset: 0x002D238E
		internal override int ElementTypeId
		{
			get
			{
				return 13046;
			}
		}

		// Token: 0x0600F46C RID: 62572 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170042E7 RID: 17127
		// (get) Token: 0x0600F46D RID: 62573 RVA: 0x002D4195 File Offset: 0x002D2395
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuSeparatorNoTitle.attributeTagNames;
			}
		}

		// Token: 0x170042E8 RID: 17128
		// (get) Token: 0x0600F46E RID: 62574 RVA: 0x002D419C File Offset: 0x002D239C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuSeparatorNoTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x170042E9 RID: 17129
		// (get) Token: 0x0600F46F RID: 62575 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F470 RID: 62576 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170042EA RID: 17130
		// (get) Token: 0x0600F471 RID: 62577 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F472 RID: 62578 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170042EB RID: 17131
		// (get) Token: 0x0600F473 RID: 62579 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F474 RID: 62580 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170042EC RID: 17132
		// (get) Token: 0x0600F475 RID: 62581 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F476 RID: 62582 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170042ED RID: 17133
		// (get) Token: 0x0600F477 RID: 62583 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F478 RID: 62584 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170042EE RID: 17134
		// (get) Token: 0x0600F479 RID: 62585 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F47A RID: 62586 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170042EF RID: 17135
		// (get) Token: 0x0600F47B RID: 62587 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F47C RID: 62588 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x0600F47E RID: 62590 RVA: 0x002D41A4 File Offset: 0x002D23A4
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
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F47F RID: 62591 RVA: 0x002D4253 File Offset: 0x002D2453
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuSeparatorNoTitle>(deep);
		}

		// Token: 0x0600F480 RID: 62592 RVA: 0x002D425C File Offset: 0x002D245C
		// Note: this type is marked as 'beforefieldinit'.
		static MenuSeparatorNoTitle()
		{
			byte[] array = new byte[7];
			MenuSeparatorNoTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04007106 RID: 28934
		private const string tagName = "menuSeparator";

		// Token: 0x04007107 RID: 28935
		private const byte tagNsId = 57;

		// Token: 0x04007108 RID: 28936
		internal const int ElementTypeIdConst = 13046;

		// Token: 0x04007109 RID: 28937
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ" };

		// Token: 0x0400710A RID: 28938
		private static byte[] attributeNamespaceIds;
	}
}
