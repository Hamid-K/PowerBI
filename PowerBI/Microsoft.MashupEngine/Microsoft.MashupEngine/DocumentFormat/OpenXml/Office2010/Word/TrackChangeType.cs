using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002485 RID: 9349
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TrackChangeType : OpenXmlLeafElement
	{
		// Token: 0x1700515F RID: 20831
		// (get) Token: 0x06011426 RID: 70694 RVA: 0x002EC805 File Offset: 0x002EAA05
		internal override string[] AttributeTagNames
		{
			get
			{
				return TrackChangeType.attributeTagNames;
			}
		}

		// Token: 0x17005160 RID: 20832
		// (get) Token: 0x06011427 RID: 70695 RVA: 0x002EC80C File Offset: 0x002EAA0C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TrackChangeType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005161 RID: 20833
		// (get) Token: 0x06011428 RID: 70696 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011429 RID: 70697 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x17005162 RID: 20834
		// (get) Token: 0x0601142A RID: 70698 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601142B RID: 70699 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005163 RID: 20835
		// (get) Token: 0x0601142C RID: 70700 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601142D RID: 70701 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601142E RID: 70702 RVA: 0x002EC814 File Offset: 0x002EAA14
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040078E9 RID: 30953
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x040078EA RID: 30954
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
