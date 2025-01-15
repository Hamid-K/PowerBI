using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002383 RID: 9091
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SortMapItemType : OpenXmlLeafElement
	{
		// Token: 0x17004B5E RID: 19294
		// (get) Token: 0x06010697 RID: 67223 RVA: 0x002E3507 File Offset: 0x002E1707
		internal override string[] AttributeTagNames
		{
			get
			{
				return SortMapItemType.attributeTagNames;
			}
		}

		// Token: 0x17004B5F RID: 19295
		// (get) Token: 0x06010698 RID: 67224 RVA: 0x002E350E File Offset: 0x002E170E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SortMapItemType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B60 RID: 19296
		// (get) Token: 0x06010699 RID: 67225 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601069A RID: 67226 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "newVal")]
		public UInt32Value NewVal
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004B61 RID: 19297
		// (get) Token: 0x0601069B RID: 67227 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601069C RID: 67228 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "oldVal")]
		public UInt32Value OldVal
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601069D RID: 67229 RVA: 0x002E3515 File Offset: 0x002E1715
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "newVal" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "oldVal" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601069F RID: 67231 RVA: 0x002E354C File Offset: 0x002E174C
		// Note: this type is marked as 'beforefieldinit'.
		static SortMapItemType()
		{
			byte[] array = new byte[2];
			SortMapItemType.attributeNamespaceIds = array;
		}

		// Token: 0x0400747E RID: 29822
		private static string[] attributeTagNames = new string[] { "newVal", "oldVal" };

		// Token: 0x0400747F RID: 29823
		private static byte[] attributeNamespaceIds;
	}
}
