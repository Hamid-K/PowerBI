using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002430 RID: 9264
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SlicerStyleElement : OpenXmlLeafElement
	{
		// Token: 0x17004FDB RID: 20443
		// (get) Token: 0x06011098 RID: 69784 RVA: 0x002E9E9E File Offset: 0x002E809E
		public override string LocalName
		{
			get
			{
				return "slicerStyleElement";
			}
		}

		// Token: 0x17004FDC RID: 20444
		// (get) Token: 0x06011099 RID: 69785 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FDD RID: 20445
		// (get) Token: 0x0601109A RID: 69786 RVA: 0x002E9EA5 File Offset: 0x002E80A5
		internal override int ElementTypeId
		{
			get
			{
				return 12988;
			}
		}

		// Token: 0x0601109B RID: 69787 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FDE RID: 20446
		// (get) Token: 0x0601109C RID: 69788 RVA: 0x002E9EAC File Offset: 0x002E80AC
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerStyleElement.attributeTagNames;
			}
		}

		// Token: 0x17004FDF RID: 20447
		// (get) Token: 0x0601109D RID: 69789 RVA: 0x002E9EB3 File Offset: 0x002E80B3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerStyleElement.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FE0 RID: 20448
		// (get) Token: 0x0601109E RID: 69790 RVA: 0x002E9EBA File Offset: 0x002E80BA
		// (set) Token: 0x0601109F RID: 69791 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<SlicerStyleTypeValues> Type
		{
			get
			{
				return (EnumValue<SlicerStyleTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004FE1 RID: 20449
		// (get) Token: 0x060110A0 RID: 69792 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060110A1 RID: 69793 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060110A3 RID: 69795 RVA: 0x002E9EC9 File Offset: 0x002E80C9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<SlicerStyleTypeValues>();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060110A4 RID: 69796 RVA: 0x002E9EFF File Offset: 0x002E80FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerStyleElement>(deep);
		}

		// Token: 0x060110A5 RID: 69797 RVA: 0x002E9F08 File Offset: 0x002E8108
		// Note: this type is marked as 'beforefieldinit'.
		static SlicerStyleElement()
		{
			byte[] array = new byte[2];
			SlicerStyleElement.attributeNamespaceIds = array;
		}

		// Token: 0x04007763 RID: 30563
		private const string tagName = "slicerStyleElement";

		// Token: 0x04007764 RID: 30564
		private const byte tagNsId = 53;

		// Token: 0x04007765 RID: 30565
		internal const int ElementTypeIdConst = 12988;

		// Token: 0x04007766 RID: 30566
		private static string[] attributeTagNames = new string[] { "type", "dxfId" };

		// Token: 0x04007767 RID: 30567
		private static byte[] attributeNamespaceIds;
	}
}
