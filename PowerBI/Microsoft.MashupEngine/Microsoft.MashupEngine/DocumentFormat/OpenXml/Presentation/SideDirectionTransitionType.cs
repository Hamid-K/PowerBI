using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD7 RID: 10967
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SideDirectionTransitionType : OpenXmlLeafElement
	{
		// Token: 0x1700757B RID: 30075
		// (get) Token: 0x06016593 RID: 91539 RVA: 0x003292F2 File Offset: 0x003274F2
		internal override string[] AttributeTagNames
		{
			get
			{
				return SideDirectionTransitionType.attributeTagNames;
			}
		}

		// Token: 0x1700757C RID: 30076
		// (get) Token: 0x06016594 RID: 91540 RVA: 0x003292F9 File Offset: 0x003274F9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SideDirectionTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700757D RID: 30077
		// (get) Token: 0x06016595 RID: 91541 RVA: 0x002E4066 File Offset: 0x002E2266
		// (set) Token: 0x06016596 RID: 91542 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionSlideDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionSlideDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016597 RID: 91543 RVA: 0x002E4075 File Offset: 0x002E2275
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionSlideDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016599 RID: 91545 RVA: 0x00329300 File Offset: 0x00327500
		// Note: this type is marked as 'beforefieldinit'.
		static SideDirectionTransitionType()
		{
			byte[] array = new byte[1];
			SideDirectionTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x0400975D RID: 38749
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x0400975E RID: 38750
		private static byte[] attributeNamespaceIds;
	}
}
