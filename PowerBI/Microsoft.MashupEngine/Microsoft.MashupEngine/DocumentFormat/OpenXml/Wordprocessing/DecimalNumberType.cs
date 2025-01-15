using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2F RID: 11823
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class DecimalNumberType : OpenXmlLeafElement
	{
		// Token: 0x17008979 RID: 35193
		// (get) Token: 0x060191BA RID: 102842 RVA: 0x00346784 File Offset: 0x00344984
		internal override string[] AttributeTagNames
		{
			get
			{
				return DecimalNumberType.attributeTagNames;
			}
		}

		// Token: 0x1700897A RID: 35194
		// (get) Token: 0x060191BB RID: 102843 RVA: 0x0034678B File Offset: 0x0034498B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DecimalNumberType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700897B RID: 35195
		// (get) Token: 0x060191BC RID: 102844 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060191BD RID: 102845 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060191BE RID: 102846 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A70C RID: 42764
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A70D RID: 42765
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
