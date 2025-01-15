using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E9 RID: 9193
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CacheField : OpenXmlLeafElement
	{
		// Token: 0x17004DC3 RID: 19907
		// (get) Token: 0x06010BDE RID: 68574 RVA: 0x002E69E7 File Offset: 0x002E4BE7
		public override string LocalName
		{
			get
			{
				return "cacheField";
			}
		}

		// Token: 0x17004DC4 RID: 19908
		// (get) Token: 0x06010BDF RID: 68575 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DC5 RID: 19909
		// (get) Token: 0x06010BE0 RID: 68576 RVA: 0x002E69EE File Offset: 0x002E4BEE
		internal override int ElementTypeId
		{
			get
			{
				return 12919;
			}
		}

		// Token: 0x06010BE1 RID: 68577 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DC6 RID: 19910
		// (get) Token: 0x06010BE2 RID: 68578 RVA: 0x002E69F5 File Offset: 0x002E4BF5
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheField.attributeTagNames;
			}
		}

		// Token: 0x17004DC7 RID: 19911
		// (get) Token: 0x06010BE3 RID: 68579 RVA: 0x002E69FC File Offset: 0x002E4BFC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheField.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DC8 RID: 19912
		// (get) Token: 0x06010BE4 RID: 68580 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010BE5 RID: 68581 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ignore")]
		public BooleanValue Ignore
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

		// Token: 0x06010BE7 RID: 68583 RVA: 0x002E698E File Offset: 0x002E4B8E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ignore" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010BE8 RID: 68584 RVA: 0x002E6A03 File Offset: 0x002E4C03
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheField>(deep);
		}

		// Token: 0x06010BE9 RID: 68585 RVA: 0x002E6A0C File Offset: 0x002E4C0C
		// Note: this type is marked as 'beforefieldinit'.
		static CacheField()
		{
			byte[] array = new byte[1];
			CacheField.attributeNamespaceIds = array;
		}

		// Token: 0x04007628 RID: 30248
		private const string tagName = "cacheField";

		// Token: 0x04007629 RID: 30249
		private const byte tagNsId = 53;

		// Token: 0x0400762A RID: 30250
		internal const int ElementTypeIdConst = 12919;

		// Token: 0x0400762B RID: 30251
		private static string[] attributeTagNames = new string[] { "ignore" };

		// Token: 0x0400762C RID: 30252
		private static byte[] attributeNamespaceIds;
	}
}
