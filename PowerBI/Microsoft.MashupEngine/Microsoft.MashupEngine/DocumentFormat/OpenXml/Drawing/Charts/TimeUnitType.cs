using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025F7 RID: 9719
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TimeUnitType : OpenXmlLeafElement
	{
		// Token: 0x17005964 RID: 22884
		// (get) Token: 0x060125D8 RID: 75224 RVA: 0x002FA3B3 File Offset: 0x002F85B3
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeUnitType.attributeTagNames;
			}
		}

		// Token: 0x17005965 RID: 22885
		// (get) Token: 0x060125D9 RID: 75225 RVA: 0x002FA3BA File Offset: 0x002F85BA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeUnitType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005966 RID: 22886
		// (get) Token: 0x060125DA RID: 75226 RVA: 0x002FA3C1 File Offset: 0x002F85C1
		// (set) Token: 0x060125DB RID: 75227 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<TimeUnitValues> Val
		{
			get
			{
				return (EnumValue<TimeUnitValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060125DC RID: 75228 RVA: 0x002FA3D0 File Offset: 0x002F85D0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<TimeUnitValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060125DE RID: 75230 RVA: 0x002FA3F0 File Offset: 0x002F85F0
		// Note: this type is marked as 'beforefieldinit'.
		static TimeUnitType()
		{
			byte[] array = new byte[1];
			TimeUnitType.attributeNamespaceIds = array;
		}

		// Token: 0x04007F40 RID: 32576
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007F41 RID: 32577
		private static byte[] attributeNamespaceIds;
	}
}
