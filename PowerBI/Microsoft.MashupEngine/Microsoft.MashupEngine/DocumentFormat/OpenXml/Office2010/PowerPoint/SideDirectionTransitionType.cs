using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002395 RID: 9109
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class SideDirectionTransitionType : OpenXmlLeafElement
	{
		// Token: 0x17004BE2 RID: 19426
		// (get) Token: 0x060107C0 RID: 67520 RVA: 0x002E4058 File Offset: 0x002E2258
		internal override string[] AttributeTagNames
		{
			get
			{
				return SideDirectionTransitionType.attributeTagNames;
			}
		}

		// Token: 0x17004BE3 RID: 19427
		// (get) Token: 0x060107C1 RID: 67521 RVA: 0x002E405F File Offset: 0x002E225F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SideDirectionTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BE4 RID: 19428
		// (get) Token: 0x060107C2 RID: 67522 RVA: 0x002E4066 File Offset: 0x002E2266
		// (set) Token: 0x060107C3 RID: 67523 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060107C4 RID: 67524 RVA: 0x002E4075 File Offset: 0x002E2275
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionSlideDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060107C6 RID: 67526 RVA: 0x002E4098 File Offset: 0x002E2298
		// Note: this type is marked as 'beforefieldinit'.
		static SideDirectionTransitionType()
		{
			byte[] array = new byte[1];
			SideDirectionTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x040074CD RID: 29901
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x040074CE RID: 29902
		private static byte[] attributeNamespaceIds;
	}
}
