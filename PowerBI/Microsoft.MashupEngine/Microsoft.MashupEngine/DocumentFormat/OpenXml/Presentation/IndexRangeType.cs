using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029EE RID: 10734
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class IndexRangeType : OpenXmlLeafElement
	{
		// Token: 0x17006E63 RID: 28259
		// (get) Token: 0x060155B5 RID: 87477 RVA: 0x0031E271 File Offset: 0x0031C471
		internal override string[] AttributeTagNames
		{
			get
			{
				return IndexRangeType.attributeTagNames;
			}
		}

		// Token: 0x17006E64 RID: 28260
		// (get) Token: 0x060155B6 RID: 87478 RVA: 0x0031E278 File Offset: 0x0031C478
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IndexRangeType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E65 RID: 28261
		// (get) Token: 0x060155B7 RID: 87479 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060155B8 RID: 87480 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "st")]
		public UInt32Value Start
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

		// Token: 0x17006E66 RID: 28262
		// (get) Token: 0x060155B9 RID: 87481 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060155BA RID: 87482 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "end")]
		public UInt32Value End
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

		// Token: 0x060155BB RID: 87483 RVA: 0x0031E27F File Offset: 0x0031C47F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "st" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "end" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060155BD RID: 87485 RVA: 0x0031E2B8 File Offset: 0x0031C4B8
		// Note: this type is marked as 'beforefieldinit'.
		static IndexRangeType()
		{
			byte[] array = new byte[2];
			IndexRangeType.attributeNamespaceIds = array;
		}

		// Token: 0x04009324 RID: 37668
		private static string[] attributeTagNames = new string[] { "st", "end" };

		// Token: 0x04009325 RID: 37669
		private static byte[] attributeNamespaceIds;
	}
}
