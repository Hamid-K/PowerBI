using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0C RID: 11276
	[GeneratedCode("DomGen", "2.0")]
	internal class Protection : OpenXmlLeafElement
	{
		// Token: 0x17007FB6 RID: 32694
		// (get) Token: 0x06017BF9 RID: 97273 RVA: 0x002FAA24 File Offset: 0x002F8C24
		public override string LocalName
		{
			get
			{
				return "protection";
			}
		}

		// Token: 0x17007FB7 RID: 32695
		// (get) Token: 0x06017BFA RID: 97274 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FB8 RID: 32696
		// (get) Token: 0x06017BFB RID: 97275 RVA: 0x0033AA66 File Offset: 0x00338C66
		internal override int ElementTypeId
		{
			get
			{
				return 11257;
			}
		}

		// Token: 0x06017BFC RID: 97276 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007FB9 RID: 32697
		// (get) Token: 0x06017BFD RID: 97277 RVA: 0x0033AA6D File Offset: 0x00338C6D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Protection.attributeTagNames;
			}
		}

		// Token: 0x17007FBA RID: 32698
		// (get) Token: 0x06017BFE RID: 97278 RVA: 0x0033AA74 File Offset: 0x00338C74
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Protection.attributeNamespaceIds;
			}
		}

		// Token: 0x17007FBB RID: 32699
		// (get) Token: 0x06017BFF RID: 97279 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017C00 RID: 97280 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "locked")]
		public BooleanValue Locked
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

		// Token: 0x17007FBC RID: 32700
		// (get) Token: 0x06017C01 RID: 97281 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017C02 RID: 97282 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x06017C04 RID: 97284 RVA: 0x0033AA7B File Offset: 0x00338C7B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "locked" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017C05 RID: 97285 RVA: 0x0033AAB1 File Offset: 0x00338CB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Protection>(deep);
		}

		// Token: 0x06017C06 RID: 97286 RVA: 0x0033AABC File Offset: 0x00338CBC
		// Note: this type is marked as 'beforefieldinit'.
		static Protection()
		{
			byte[] array = new byte[2];
			Protection.attributeNamespaceIds = array;
		}

		// Token: 0x04009D6D RID: 40301
		private const string tagName = "protection";

		// Token: 0x04009D6E RID: 40302
		private const byte tagNsId = 22;

		// Token: 0x04009D6F RID: 40303
		internal const int ElementTypeIdConst = 11257;

		// Token: 0x04009D70 RID: 40304
		private static string[] attributeTagNames = new string[] { "locked", "hidden" };

		// Token: 0x04009D71 RID: 40305
		private static byte[] attributeNamespaceIds;
	}
}
