using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD4 RID: 10964
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OptionalBlackTransitionType : OpenXmlLeafElement
	{
		// Token: 0x17007572 RID: 30066
		// (get) Token: 0x06016580 RID: 91520 RVA: 0x00329265 File Offset: 0x00327465
		internal override string[] AttributeTagNames
		{
			get
			{
				return OptionalBlackTransitionType.attributeTagNames;
			}
		}

		// Token: 0x17007573 RID: 30067
		// (get) Token: 0x06016581 RID: 91521 RVA: 0x0032926C File Offset: 0x0032746C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OptionalBlackTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007574 RID: 30068
		// (get) Token: 0x06016582 RID: 91522 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016583 RID: 91523 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "thruBlk")]
		public BooleanValue ThroughBlack
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016584 RID: 91524 RVA: 0x00329273 File Offset: 0x00327473
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "thruBlk" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016586 RID: 91526 RVA: 0x00329294 File Offset: 0x00327494
		// Note: this type is marked as 'beforefieldinit'.
		static OptionalBlackTransitionType()
		{
			byte[] array = new byte[1];
			OptionalBlackTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x04009755 RID: 38741
		private static string[] attributeTagNames = new string[] { "thruBlk" };

		// Token: 0x04009756 RID: 38742
		private static byte[] attributeNamespaceIds;
	}
}
