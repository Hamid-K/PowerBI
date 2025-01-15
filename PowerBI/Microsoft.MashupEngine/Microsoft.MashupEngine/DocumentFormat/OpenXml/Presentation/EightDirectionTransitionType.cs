using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD1 RID: 10961
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class EightDirectionTransitionType : OpenXmlLeafElement
	{
		// Token: 0x17007569 RID: 30057
		// (get) Token: 0x0601656D RID: 91501 RVA: 0x003291EF File Offset: 0x003273EF
		internal override string[] AttributeTagNames
		{
			get
			{
				return EightDirectionTransitionType.attributeTagNames;
			}
		}

		// Token: 0x1700756A RID: 30058
		// (get) Token: 0x0601656E RID: 91502 RVA: 0x003291F6 File Offset: 0x003273F6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EightDirectionTransitionType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700756B RID: 30059
		// (get) Token: 0x0601656F RID: 91503 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016570 RID: 91504 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public StringValue Direction
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

		// Token: 0x06016571 RID: 91505 RVA: 0x002E41FB File Offset: 0x002E23FB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016573 RID: 91507 RVA: 0x00329200 File Offset: 0x00327400
		// Note: this type is marked as 'beforefieldinit'.
		static EightDirectionTransitionType()
		{
			byte[] array = new byte[1];
			EightDirectionTransitionType.attributeNamespaceIds = array;
		}

		// Token: 0x0400974D RID: 38733
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x0400974E RID: 38734
		private static byte[] attributeNamespaceIds;
	}
}
