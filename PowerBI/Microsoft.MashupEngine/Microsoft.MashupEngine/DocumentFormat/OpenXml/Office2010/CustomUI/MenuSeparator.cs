using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BF RID: 8895
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class MenuSeparator : OpenXmlLeafElement
	{
		// Token: 0x17004242 RID: 16962
		// (get) Token: 0x0600F315 RID: 62229 RVA: 0x002C9E0B File Offset: 0x002C800B
		public override string LocalName
		{
			get
			{
				return "menuSeparator";
			}
		}

		// Token: 0x17004243 RID: 16963
		// (get) Token: 0x0600F316 RID: 62230 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004244 RID: 16964
		// (get) Token: 0x0600F317 RID: 62231 RVA: 0x002D2CF7 File Offset: 0x002D0EF7
		internal override int ElementTypeId
		{
			get
			{
				return 13040;
			}
		}

		// Token: 0x0600F318 RID: 62232 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004245 RID: 16965
		// (get) Token: 0x0600F319 RID: 62233 RVA: 0x002D2CFE File Offset: 0x002D0EFE
		internal override string[] AttributeTagNames
		{
			get
			{
				return MenuSeparator.attributeTagNames;
			}
		}

		// Token: 0x17004246 RID: 16966
		// (get) Token: 0x0600F31A RID: 62234 RVA: 0x002D2D05 File Offset: 0x002D0F05
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MenuSeparator.attributeNamespaceIds;
			}
		}

		// Token: 0x17004247 RID: 16967
		// (get) Token: 0x0600F31B RID: 62235 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F31C RID: 62236 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004248 RID: 16968
		// (get) Token: 0x0600F31D RID: 62237 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F31E RID: 62238 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004249 RID: 16969
		// (get) Token: 0x0600F31F RID: 62239 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F320 RID: 62240 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700424A RID: 16970
		// (get) Token: 0x0600F321 RID: 62241 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F322 RID: 62242 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700424B RID: 16971
		// (get) Token: 0x0600F323 RID: 62243 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F324 RID: 62244 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700424C RID: 16972
		// (get) Token: 0x0600F325 RID: 62245 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F326 RID: 62246 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700424D RID: 16973
		// (get) Token: 0x0600F327 RID: 62247 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F328 RID: 62248 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700424E RID: 16974
		// (get) Token: 0x0600F329 RID: 62249 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F32A RID: 62250 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x1700424F RID: 16975
		// (get) Token: 0x0600F32B RID: 62251 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F32C RID: 62252 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getTitle")]
		public StringValue GetTitle
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0600F32E RID: 62254 RVA: 0x002D2D0C File Offset: 0x002D0F0C
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

		// Token: 0x0600F32F RID: 62255 RVA: 0x002D2DE7 File Offset: 0x002D0FE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MenuSeparator>(deep);
		}

		// Token: 0x0600F330 RID: 62256 RVA: 0x002D2DF0 File Offset: 0x002D0FF0
		// Note: this type is marked as 'beforefieldinit'.
		static MenuSeparator()
		{
			byte[] array = new byte[9];
			MenuSeparator.attributeNamespaceIds = array;
		}

		// Token: 0x040070E8 RID: 28904
		private const string tagName = "menuSeparator";

		// Token: 0x040070E9 RID: 28905
		private const byte tagNsId = 57;

		// Token: 0x040070EA RID: 28906
		internal const int ElementTypeIdConst = 13040;

		// Token: 0x040070EB RID: 28907
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "title", "getTitle" };

		// Token: 0x040070EC RID: 28908
		private static byte[] attributeNamespaceIds;
	}
}
