using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC6 RID: 10950
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TimeListSubShapeIdType : OpenXmlLeafElement
	{
		// Token: 0x1700753A RID: 30010
		// (get) Token: 0x06016509 RID: 91401 RVA: 0x00328EB6 File Offset: 0x003270B6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeListSubShapeIdType.attributeTagNames;
			}
		}

		// Token: 0x1700753B RID: 30011
		// (get) Token: 0x0601650A RID: 91402 RVA: 0x00328EBD File Offset: 0x003270BD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeListSubShapeIdType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700753C RID: 30012
		// (get) Token: 0x0601650B RID: 91403 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601650C RID: 91404 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
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

		// Token: 0x0601650D RID: 91405 RVA: 0x002E015B File Offset: 0x002DE35B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601650F RID: 91407 RVA: 0x00328EC4 File Offset: 0x003270C4
		// Note: this type is marked as 'beforefieldinit'.
		static TimeListSubShapeIdType()
		{
			byte[] array = new byte[1];
			TimeListSubShapeIdType.attributeNamespaceIds = array;
		}

		// Token: 0x04009728 RID: 38696
		private static string[] attributeTagNames = new string[] { "spid" };

		// Token: 0x04009729 RID: 38697
		private static byte[] attributeNamespaceIds;
	}
}
