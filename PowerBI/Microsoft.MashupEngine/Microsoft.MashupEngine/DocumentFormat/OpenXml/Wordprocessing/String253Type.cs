using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E80 RID: 11904
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class String253Type : OpenXmlLeafElement
	{
		// Token: 0x17008AE9 RID: 35561
		// (get) Token: 0x060194C1 RID: 103617 RVA: 0x003485EC File Offset: 0x003467EC
		internal override string[] AttributeTagNames
		{
			get
			{
				return String253Type.attributeTagNames;
			}
		}

		// Token: 0x17008AEA RID: 35562
		// (get) Token: 0x060194C2 RID: 103618 RVA: 0x003485F3 File Offset: 0x003467F3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return String253Type.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AEB RID: 35563
		// (get) Token: 0x060194C3 RID: 103619 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060194C4 RID: 103620 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x060194C5 RID: 103621 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A827 RID: 43047
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A828 RID: 43048
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
