using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002398 RID: 9112
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class LeftRightDirectionTransitionType : OpenXmlLeafElement
	{
		// Token: 0x17004BEB RID: 19435
		// (get) Token: 0x060107D3 RID: 67539 RVA: 0x002E40FD File Offset: 0x002E22FD
		internal override string[] AttributeTagNames
		{
			get
			{
				return LeftRightDirectionTransitionType.attributeTagNames;
			}
		}

		// Token: 0x17004BEC RID: 19436
		// (get) Token: 0x060107D4 RID: 67540 RVA: 0x002E4104 File Offset: 0x002E2304
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LeftRightDirectionTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BED RID: 19437
		// (get) Token: 0x060107D5 RID: 67541 RVA: 0x002E410B File Offset: 0x002E230B
		// (set) Token: 0x060107D6 RID: 67542 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionLeftRightDirectionTypeValues> Direction
		{
			get
			{
				return (EnumValue<TransitionLeftRightDirectionTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060107D7 RID: 67543 RVA: 0x002E411A File Offset: 0x002E231A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionLeftRightDirectionTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060107D9 RID: 67545 RVA: 0x002E413C File Offset: 0x002E233C
		// Note: this type is marked as 'beforefieldinit'.
		static LeftRightDirectionTransitionType()
		{
			byte[] array = new byte[1];
			LeftRightDirectionTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x040074D5 RID: 29909
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x040074D6 RID: 29910
		private static byte[] attributeNamespaceIds;
	}
}
