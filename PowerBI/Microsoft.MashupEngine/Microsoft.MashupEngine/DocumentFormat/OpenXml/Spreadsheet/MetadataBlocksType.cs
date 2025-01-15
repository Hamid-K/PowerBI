using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF7 RID: 11255
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MetadataBlock))]
	internal abstract class MetadataBlocksType : OpenXmlCompositeElement
	{
		// Token: 0x17007EE5 RID: 32485
		// (get) Token: 0x06017A34 RID: 96820 RVA: 0x003395FF File Offset: 0x003377FF
		internal override string[] AttributeTagNames
		{
			get
			{
				return MetadataBlocksType.attributeTagNames;
			}
		}

		// Token: 0x17007EE6 RID: 32486
		// (get) Token: 0x06017A35 RID: 96821 RVA: 0x00339606 File Offset: 0x00337806
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MetadataBlocksType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007EE7 RID: 32487
		// (get) Token: 0x06017A36 RID: 96822 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017A37 RID: 96823 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06017A38 RID: 96824 RVA: 0x0033960D File Offset: 0x0033780D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "bk" == name)
			{
				return new MetadataBlock();
			}
			return null;
		}

		// Token: 0x06017A39 RID: 96825 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A3A RID: 96826 RVA: 0x00293ECF File Offset: 0x002920CF
		protected MetadataBlocksType()
		{
		}

		// Token: 0x06017A3B RID: 96827 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected MetadataBlocksType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A3C RID: 96828 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected MetadataBlocksType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A3D RID: 96829 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected MetadataBlocksType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A3E RID: 96830 RVA: 0x00339628 File Offset: 0x00337828
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataBlocksType()
		{
			byte[] array = new byte[1];
			MetadataBlocksType.attributeNamespaceIds = array;
		}

		// Token: 0x04009D01 RID: 40193
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009D02 RID: 40194
		private static byte[] attributeNamespaceIds;
	}
}
