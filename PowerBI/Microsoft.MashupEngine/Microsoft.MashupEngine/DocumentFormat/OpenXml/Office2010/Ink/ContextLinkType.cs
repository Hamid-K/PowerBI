using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Ink
{
	// Token: 0x02002268 RID: 8808
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ContextLinkType : OpenXmlLeafElement
	{
		// Token: 0x17003CD2 RID: 15570
		// (get) Token: 0x0600E795 RID: 59285 RVA: 0x002C8687 File Offset: 0x002C6887
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContextLinkType.attributeTagNames;
			}
		}

		// Token: 0x17003CD3 RID: 15571
		// (get) Token: 0x0600E796 RID: 59286 RVA: 0x002C868E File Offset: 0x002C688E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContextLinkType.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CD4 RID: 15572
		// (get) Token: 0x0600E797 RID: 59287 RVA: 0x002C8695 File Offset: 0x002C6895
		// (set) Token: 0x0600E798 RID: 59288 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "direction")]
		public EnumValue<LinkDirectionValues> Direction
		{
			get
			{
				return (EnumValue<LinkDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003CD5 RID: 15573
		// (get) Token: 0x0600E799 RID: 59289 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E79A RID: 59290 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x0600E79B RID: 59291 RVA: 0x002C86A4 File Offset: 0x002C68A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "direction" == name)
			{
				return new EnumValue<LinkDirectionValues>();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E79D RID: 59293 RVA: 0x002C86DC File Offset: 0x002C68DC
		// Note: this type is marked as 'beforefieldinit'.
		static ContextLinkType()
		{
			byte[] array = new byte[2];
			ContextLinkType.attributeNamespaceIds = array;
		}

		// Token: 0x04006F55 RID: 28501
		private static string[] attributeTagNames = new string[] { "direction", "ref" };

		// Token: 0x04006F56 RID: 28502
		private static byte[] attributeNamespaceIds;
	}
}
