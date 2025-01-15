using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C5 RID: 10181
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class AdjustPoint2DType : OpenXmlLeafElement
	{
		// Token: 0x1700637F RID: 25471
		// (get) Token: 0x06013C70 RID: 81008 RVA: 0x0030B9CB File Offset: 0x00309BCB
		internal override string[] AttributeTagNames
		{
			get
			{
				return AdjustPoint2DType.attributeTagNames;
			}
		}

		// Token: 0x17006380 RID: 25472
		// (get) Token: 0x06013C71 RID: 81009 RVA: 0x0030B9D2 File Offset: 0x00309BD2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AdjustPoint2DType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006381 RID: 25473
		// (get) Token: 0x06013C72 RID: 81010 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013C73 RID: 81011 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public StringValue X
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

		// Token: 0x17006382 RID: 25474
		// (get) Token: 0x06013C74 RID: 81012 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013C75 RID: 81013 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public StringValue Y
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

		// Token: 0x06013C76 RID: 81014 RVA: 0x0030B9D9 File Offset: 0x00309BD9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013C78 RID: 81016 RVA: 0x0030BA10 File Offset: 0x00309C10
		// Note: this type is marked as 'beforefieldinit'.
		static AdjustPoint2DType()
		{
			byte[] array = new byte[2];
			AdjustPoint2DType.attributeNamespaceIds = array;
		}

		// Token: 0x040087BC RID: 34748
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x040087BD RID: 34749
		private static byte[] attributeNamespaceIds;
	}
}
