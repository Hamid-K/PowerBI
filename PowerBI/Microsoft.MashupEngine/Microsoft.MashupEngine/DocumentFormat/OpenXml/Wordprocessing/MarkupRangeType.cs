using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D41 RID: 11585
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MarkupRangeType : OpenXmlLeafElement
	{
		// Token: 0x17008658 RID: 34392
		// (get) Token: 0x06018B6B RID: 101227 RVA: 0x003443B8 File Offset: 0x003425B8
		internal override string[] AttributeTagNames
		{
			get
			{
				return MarkupRangeType.attributeTagNames;
			}
		}

		// Token: 0x17008659 RID: 34393
		// (get) Token: 0x06018B6C RID: 101228 RVA: 0x003443BF File Offset: 0x003425BF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MarkupRangeType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700865A RID: 34394
		// (get) Token: 0x06018B6D RID: 101229 RVA: 0x003443C6 File Offset: 0x003425C6
		// (set) Token: 0x06018B6E RID: 101230 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "displacedByCustomXml")]
		public EnumValue<DisplacedByCustomXmlValues> DisplacedByCustomXml
		{
			get
			{
				return (EnumValue<DisplacedByCustomXmlValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700865B RID: 34395
		// (get) Token: 0x06018B6F RID: 101231 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06018B70 RID: 101232 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x06018B71 RID: 101233 RVA: 0x003443D5 File Offset: 0x003425D5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "displacedByCustomXml" == name)
			{
				return new EnumValue<DisplacedByCustomXmlValues>();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A433 RID: 42035
		private static string[] attributeTagNames = new string[] { "displacedByCustomXml", "id" };

		// Token: 0x0400A434 RID: 42036
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
