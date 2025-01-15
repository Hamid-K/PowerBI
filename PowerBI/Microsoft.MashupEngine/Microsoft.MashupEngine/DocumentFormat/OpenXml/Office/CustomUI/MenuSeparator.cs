using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002273 RID: 8819
	[GeneratedCode("DomGen", "2.0")]
	internal class MenuSeparator : OpenXmlLeafElement
	{
		// Token: 0x17003D94 RID: 15764
		// (get) Token: 0x0600E91E RID: 59678 RVA: 0x002C9E0B File Offset: 0x002C800B
		public override string LocalName
		{
			get
			{
				return "menuSeparator";
			}
		}

		// Token: 0x17003D95 RID: 15765
		// (get) Token: 0x0600E91F RID: 59679 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003D96 RID: 15766
		// (get) Token: 0x0600E920 RID: 59680 RVA: 0x002C9E12 File Offset: 0x002C8012
		internal override int ElementTypeId
		{
			get
			{
				return 12578;
			}
		}

		// Token: 0x0600E921 RID: 59681 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003D97 RID: 15767
		// (get) Token: 0x0600E922 RID: 59682 RVA: 0x002C9E19 File Offset: 0x002C8019
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuSeparator.attributeTagNames;
			}
		}

		// Token: 0x17003D98 RID: 15768
		// (get) Token: 0x0600E923 RID: 59683 RVA: 0x002C9E20 File Offset: 0x002C8020
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuSeparator.attributeNamespaceIds;
			}
		}

		// Token: 0x17003D99 RID: 15769
		// (get) Token: 0x0600E924 RID: 59684 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E925 RID: 59685 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003D9A RID: 15770
		// (get) Token: 0x0600E926 RID: 59686 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E927 RID: 59687 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003D9B RID: 15771
		// (get) Token: 0x0600E928 RID: 59688 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E929 RID: 59689 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003D9C RID: 15772
		// (get) Token: 0x0600E92A RID: 59690 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E92B RID: 59691 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003D9D RID: 15773
		// (get) Token: 0x0600E92C RID: 59692 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E92D RID: 59693 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003D9E RID: 15774
		// (get) Token: 0x0600E92E RID: 59694 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E92F RID: 59695 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003D9F RID: 15775
		// (get) Token: 0x0600E930 RID: 59696 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E931 RID: 59697 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x17003DA0 RID: 15776
		// (get) Token: 0x0600E932 RID: 59698 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E933 RID: 59699 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getTitle")]
		public StringValue GetTitle
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x0600E935 RID: 59701 RVA: 0x002C9E28 File Offset: 0x002C8028
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
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getTitle" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E936 RID: 59702 RVA: 0x002C9EED File Offset: 0x002C80ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuSeparator>(deep);
		}

		// Token: 0x0600E937 RID: 59703 RVA: 0x002C9EF8 File Offset: 0x002C80F8
		// Note: this type is marked as 'beforefieldinit'.
		static MenuSeparator()
		{
			byte[] array = new byte[8];
			MenuSeparator.attributeNamespaceIds = array;
		}

		// Token: 0x04006F9E RID: 28574
		private const string tagName = "menuSeparator";

		// Token: 0x04006F9F RID: 28575
		private const byte tagNsId = 34;

		// Token: 0x04006FA0 RID: 28576
		internal const int ElementTypeIdConst = 12578;

		// Token: 0x04006FA1 RID: 28577
		private static string[] attributeTagNames = new string[] { "id", "idQ", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "title", "getTitle" };

		// Token: 0x04006FA2 RID: 28578
		private static byte[] attributeNamespaceIds;
	}
}
