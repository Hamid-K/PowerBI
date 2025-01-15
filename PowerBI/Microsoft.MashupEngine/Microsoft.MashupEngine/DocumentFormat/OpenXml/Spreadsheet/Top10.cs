using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDE RID: 11486
	[GeneratedCode("DomGen", "2.0")]
	internal class Top10 : OpenXmlLeafElement
	{
		// Token: 0x170085FA RID: 34298
		// (get) Token: 0x06018AAA RID: 101034 RVA: 0x00343C53 File Offset: 0x00341E53
		public override string LocalName
		{
			get
			{
				return "top10";
			}
		}

		// Token: 0x170085FB RID: 34299
		// (get) Token: 0x06018AAB RID: 101035 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085FC RID: 34300
		// (get) Token: 0x06018AAC RID: 101036 RVA: 0x00343C5A File Offset: 0x00341E5A
		internal override int ElementTypeId
		{
			get
			{
				return 11468;
			}
		}

		// Token: 0x06018AAD RID: 101037 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085FD RID: 34301
		// (get) Token: 0x06018AAE RID: 101038 RVA: 0x00343C61 File Offset: 0x00341E61
		internal override string[] AttributeTagNames
		{
			get
			{
				return Top10.attributeTagNames;
			}
		}

		// Token: 0x170085FE RID: 34302
		// (get) Token: 0x06018AAF RID: 101039 RVA: 0x00343C68 File Offset: 0x00341E68
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Top10.attributeNamespaceIds;
			}
		}

		// Token: 0x170085FF RID: 34303
		// (get) Token: 0x06018AB0 RID: 101040 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018AB1 RID: 101041 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "top")]
		public BooleanValue Top
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008600 RID: 34304
		// (get) Token: 0x06018AB2 RID: 101042 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018AB3 RID: 101043 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "percent")]
		public BooleanValue Percent
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008601 RID: 34305
		// (get) Token: 0x06018AB4 RID: 101044 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06018AB5 RID: 101045 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008602 RID: 34306
		// (get) Token: 0x06018AB6 RID: 101046 RVA: 0x002F66C2 File Offset: 0x002F48C2
		// (set) Token: 0x06018AB7 RID: 101047 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "filterVal")]
		public DoubleValue FilterValue
		{
			get
			{
				return (DoubleValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06018AB9 RID: 101049 RVA: 0x00343C70 File Offset: 0x00341E70
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "top" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "percent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "filterVal" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018ABA RID: 101050 RVA: 0x00343CDD File Offset: 0x00341EDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Top10>(deep);
		}

		// Token: 0x06018ABB RID: 101051 RVA: 0x00343CE8 File Offset: 0x00341EE8
		// Note: this type is marked as 'beforefieldinit'.
		static Top10()
		{
			byte[] array = new byte[4];
			Top10.attributeNamespaceIds = array;
		}

		// Token: 0x0400A137 RID: 41271
		private const string tagName = "top10";

		// Token: 0x0400A138 RID: 41272
		private const byte tagNsId = 22;

		// Token: 0x0400A139 RID: 41273
		internal const int ElementTypeIdConst = 11468;

		// Token: 0x0400A13A RID: 41274
		private static string[] attributeTagNames = new string[] { "top", "percent", "val", "filterVal" };

		// Token: 0x0400A13B RID: 41275
		private static byte[] attributeNamespaceIds;
	}
}
