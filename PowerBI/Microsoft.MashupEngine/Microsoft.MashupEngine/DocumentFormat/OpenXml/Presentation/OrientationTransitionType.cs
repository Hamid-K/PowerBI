using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACC RID: 10956
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class OrientationTransitionType : OpenXmlLeafElement
	{
		// Token: 0x1700755A RID: 30042
		// (get) Token: 0x0601654E RID: 91470 RVA: 0x0032914D File Offset: 0x0032734D
		internal override string[] AttributeTagNames
		{
			get
			{
				return OrientationTransitionType.attributeTagNames;
			}
		}

		// Token: 0x1700755B RID: 30043
		// (get) Token: 0x0601654F RID: 91471 RVA: 0x00329154 File Offset: 0x00327354
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OrientationTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700755C RID: 30044
		// (get) Token: 0x06016550 RID: 91472 RVA: 0x002E4355 File Offset: 0x002E2555
		// (set) Token: 0x06016551 RID: 91473 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016552 RID: 91474 RVA: 0x002E4364 File Offset: 0x002E2564
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016554 RID: 91476 RVA: 0x0032915C File Offset: 0x0032735C
		// Note: this type is marked as 'beforefieldinit'.
		static OrientationTransitionType()
		{
			byte[] array = new byte[1];
			OrientationTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x0400973F RID: 38719
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x04009740 RID: 38720
		private static byte[] attributeNamespaceIds;
	}
}
