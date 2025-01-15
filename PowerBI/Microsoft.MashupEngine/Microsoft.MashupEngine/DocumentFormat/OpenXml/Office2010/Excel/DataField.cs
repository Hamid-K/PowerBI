using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DF RID: 9183
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DataField : OpenXmlLeafElement
	{
		// Token: 0x17004D64 RID: 19812
		// (get) Token: 0x06010B15 RID: 68373 RVA: 0x002E60BE File Offset: 0x002E42BE
		public override string LocalName
		{
			get
			{
				return "dataField";
			}
		}

		// Token: 0x17004D65 RID: 19813
		// (get) Token: 0x06010B16 RID: 68374 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D66 RID: 19814
		// (get) Token: 0x06010B17 RID: 68375 RVA: 0x002E60C5 File Offset: 0x002E42C5
		internal override int ElementTypeId
		{
			get
			{
				return 12909;
			}
		}

		// Token: 0x06010B18 RID: 68376 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D67 RID: 19815
		// (get) Token: 0x06010B19 RID: 68377 RVA: 0x002E60CC File Offset: 0x002E42CC
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataField.attributeTagNames;
			}
		}

		// Token: 0x17004D68 RID: 19816
		// (get) Token: 0x06010B1A RID: 68378 RVA: 0x002E60D3 File Offset: 0x002E42D3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataField.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D69 RID: 19817
		// (get) Token: 0x06010B1B RID: 68379 RVA: 0x002E60DA File Offset: 0x002E42DA
		// (set) Token: 0x06010B1C RID: 68380 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pivotShowAs")]
		public EnumValue<PivotShowAsValues> PivotShowAs
		{
			get
			{
				return (EnumValue<PivotShowAsValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004D6A RID: 19818
		// (get) Token: 0x06010B1D RID: 68381 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010B1E RID: 68382 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceField")]
		public UInt32Value SourceField
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

		// Token: 0x17004D6B RID: 19819
		// (get) Token: 0x06010B1F RID: 68383 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010B20 RID: 68384 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06010B22 RID: 68386 RVA: 0x002E60EC File Offset: 0x002E42EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pivotShowAs" == name)
			{
				return new EnumValue<PivotShowAsValues>();
			}
			if (namespaceId == 0 && "sourceField" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B23 RID: 68387 RVA: 0x002E6143 File Offset: 0x002E4343
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataField>(deep);
		}

		// Token: 0x06010B24 RID: 68388 RVA: 0x002E614C File Offset: 0x002E434C
		// Note: this type is marked as 'beforefieldinit'.
		static DataField()
		{
			byte[] array = new byte[3];
			DataField.attributeNamespaceIds = array;
		}

		// Token: 0x040075F0 RID: 30192
		private const string tagName = "dataField";

		// Token: 0x040075F1 RID: 30193
		private const byte tagNsId = 53;

		// Token: 0x040075F2 RID: 30194
		internal const int ElementTypeIdConst = 12909;

		// Token: 0x040075F3 RID: 30195
		private static string[] attributeTagNames = new string[] { "pivotShowAs", "sourceField", "uniqueName" };

		// Token: 0x040075F4 RID: 30196
		private static byte[] attributeNamespaceIds;
	}
}
