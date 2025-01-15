using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003006 RID: 12294
	[GeneratedCode("DomGen", "2.0")]
	internal class DataBinding : OpenXmlLeafElement
	{
		// Token: 0x17009637 RID: 38455
		// (get) Token: 0x0601AD34 RID: 109876 RVA: 0x00368268 File Offset: 0x00366468
		public override string LocalName
		{
			get
			{
				return "dataBinding";
			}
		}

		// Token: 0x17009638 RID: 38456
		// (get) Token: 0x0601AD35 RID: 109877 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009639 RID: 38457
		// (get) Token: 0x0601AD36 RID: 109878 RVA: 0x0036826F File Offset: 0x0036646F
		internal override int ElementTypeId
		{
			get
			{
				return 12143;
			}
		}

		// Token: 0x0601AD37 RID: 109879 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700963A RID: 38458
		// (get) Token: 0x0601AD38 RID: 109880 RVA: 0x00368276 File Offset: 0x00366476
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataBinding.attributeTagNames;
			}
		}

		// Token: 0x1700963B RID: 38459
		// (get) Token: 0x0601AD39 RID: 109881 RVA: 0x0036827D File Offset: 0x0036647D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataBinding.attributeNamespaceIds;
			}
		}

		// Token: 0x1700963C RID: 38460
		// (get) Token: 0x0601AD3A RID: 109882 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AD3B RID: 109883 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "prefixMappings")]
		public StringValue PrefixMappings
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

		// Token: 0x1700963D RID: 38461
		// (get) Token: 0x0601AD3C RID: 109884 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AD3D RID: 109885 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "xpath")]
		public StringValue XPath
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

		// Token: 0x1700963E RID: 38462
		// (get) Token: 0x0601AD3E RID: 109886 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AD3F RID: 109887 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "storeItemID")]
		public StringValue StoreItemId
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

		// Token: 0x0601AD41 RID: 109889 RVA: 0x00368284 File Offset: 0x00366484
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "prefixMappings" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "xpath" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "storeItemID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AD42 RID: 109890 RVA: 0x003682E1 File Offset: 0x003664E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataBinding>(deep);
		}

		// Token: 0x0400AE6B RID: 44651
		private const string tagName = "dataBinding";

		// Token: 0x0400AE6C RID: 44652
		private const byte tagNsId = 23;

		// Token: 0x0400AE6D RID: 44653
		internal const int ElementTypeIdConst = 12143;

		// Token: 0x0400AE6E RID: 44654
		private static string[] attributeTagNames = new string[] { "prefixMappings", "xpath", "storeItemID" };

		// Token: 0x0400AE6F RID: 44655
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
