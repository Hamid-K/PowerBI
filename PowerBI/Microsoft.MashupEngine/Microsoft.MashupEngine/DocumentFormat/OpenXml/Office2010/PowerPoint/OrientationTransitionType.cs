using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023A3 RID: 9123
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OrientationTransitionType : OpenXmlLeafElement
	{
		// Token: 0x17004C11 RID: 19473
		// (get) Token: 0x06010821 RID: 67617 RVA: 0x002E4347 File Offset: 0x002E2547
		internal override string[] AttributeTagNames
		{
			get
			{
				return OrientationTransitionType.attributeTagNames;
			}
		}

		// Token: 0x17004C12 RID: 19474
		// (get) Token: 0x06010822 RID: 67618 RVA: 0x002E434E File Offset: 0x002E254E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OrientationTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C13 RID: 19475
		// (get) Token: 0x06010823 RID: 67619 RVA: 0x002E4355 File Offset: 0x002E2555
		// (set) Token: 0x06010824 RID: 67620 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<DirectionValues> Direction
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010825 RID: 67621 RVA: 0x002E4364 File Offset: 0x002E2564
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010827 RID: 67623 RVA: 0x002E4384 File Offset: 0x002E2584
		// Note: this type is marked as 'beforefieldinit'.
		static OrientationTransitionType()
		{
			byte[] array = new byte[1];
			OrientationTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x040074F6 RID: 29942
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x040074F7 RID: 29943
		private static byte[] attributeNamespaceIds;
	}
}
