using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABB RID: 10939
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TimeListType : OpenXmlLeafElement
	{
		// Token: 0x17007507 RID: 29959
		// (get) Token: 0x06016491 RID: 91281 RVA: 0x0032894F File Offset: 0x00326B4F
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeListType.attributeTagNames;
			}
		}

		// Token: 0x17007508 RID: 29960
		// (get) Token: 0x06016492 RID: 91282 RVA: 0x00328956 File Offset: 0x00326B56
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeListType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007509 RID: 29961
		// (get) Token: 0x06016493 RID: 91283 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016494 RID: 91284 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int32Value X
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

		// Token: 0x1700750A RID: 29962
		// (get) Token: 0x06016495 RID: 91285 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06016496 RID: 91286 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public Int32Value Y
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06016497 RID: 91287 RVA: 0x0032895D File Offset: 0x00326B5D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016499 RID: 91289 RVA: 0x00328994 File Offset: 0x00326B94
		// Note: this type is marked as 'beforefieldinit'.
		static TimeListType()
		{
			byte[] array = new byte[2];
			TimeListType.attributeNamespaceIds = array;
		}

		// Token: 0x04009703 RID: 38659
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x04009704 RID: 38660
		private static byte[] attributeNamespaceIds;
	}
}
