using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002558 RID: 9560
	[GeneratedCode("DomGen", "2.0")]
	internal class TickLabelPosition : OpenXmlLeafElement
	{
		// Token: 0x1700557E RID: 21886
		// (get) Token: 0x06011D30 RID: 73008 RVA: 0x002F2D65 File Offset: 0x002F0F65
		public override string LocalName
		{
			get
			{
				return "tickLblPos";
			}
		}

		// Token: 0x1700557F RID: 21887
		// (get) Token: 0x06011D31 RID: 73009 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005580 RID: 21888
		// (get) Token: 0x06011D32 RID: 73010 RVA: 0x002F2D6C File Offset: 0x002F0F6C
		internal override int ElementTypeId
		{
			get
			{
				return 10382;
			}
		}

		// Token: 0x06011D33 RID: 73011 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005581 RID: 21889
		// (get) Token: 0x06011D34 RID: 73012 RVA: 0x002F2D73 File Offset: 0x002F0F73
		internal override string[] AttributeTagNames
		{
			get
			{
				return TickLabelPosition.attributeTagNames;
			}
		}

		// Token: 0x17005582 RID: 21890
		// (get) Token: 0x06011D35 RID: 73013 RVA: 0x002F2D7A File Offset: 0x002F0F7A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TickLabelPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005583 RID: 21891
		// (get) Token: 0x06011D36 RID: 73014 RVA: 0x002F2D81 File Offset: 0x002F0F81
		// (set) Token: 0x06011D37 RID: 73015 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<TickLabelPositionValues> Val
		{
			get
			{
				return (EnumValue<TickLabelPositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011D39 RID: 73017 RVA: 0x002F2D90 File Offset: 0x002F0F90
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<TickLabelPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011D3A RID: 73018 RVA: 0x002F2DB0 File Offset: 0x002F0FB0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TickLabelPosition>(deep);
		}

		// Token: 0x06011D3B RID: 73019 RVA: 0x002F2DBC File Offset: 0x002F0FBC
		// Note: this type is marked as 'beforefieldinit'.
		static TickLabelPosition()
		{
			byte[] array = new byte[1];
			TickLabelPosition.attributeNamespaceIds = array;
		}

		// Token: 0x04007CB5 RID: 31925
		private const string tagName = "tickLblPos";

		// Token: 0x04007CB6 RID: 31926
		private const byte tagNsId = 11;

		// Token: 0x04007CB7 RID: 31927
		internal const int ElementTypeIdConst = 10382;

		// Token: 0x04007CB8 RID: 31928
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007CB9 RID: 31929
		private static byte[] attributeNamespaceIds;
	}
}
