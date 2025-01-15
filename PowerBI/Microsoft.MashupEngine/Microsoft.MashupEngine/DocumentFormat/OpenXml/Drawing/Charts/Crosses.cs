using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002559 RID: 9561
	[GeneratedCode("DomGen", "2.0")]
	internal class Crosses : OpenXmlLeafElement
	{
		// Token: 0x17005584 RID: 21892
		// (get) Token: 0x06011D3C RID: 73020 RVA: 0x002F2DEB File Offset: 0x002F0FEB
		public override string LocalName
		{
			get
			{
				return "crosses";
			}
		}

		// Token: 0x17005585 RID: 21893
		// (get) Token: 0x06011D3D RID: 73021 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005586 RID: 21894
		// (get) Token: 0x06011D3E RID: 73022 RVA: 0x002F2DF2 File Offset: 0x002F0FF2
		internal override int ElementTypeId
		{
			get
			{
				return 10384;
			}
		}

		// Token: 0x06011D3F RID: 73023 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005587 RID: 21895
		// (get) Token: 0x06011D40 RID: 73024 RVA: 0x002F2DF9 File Offset: 0x002F0FF9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Crosses.attributeTagNames;
			}
		}

		// Token: 0x17005588 RID: 21896
		// (get) Token: 0x06011D41 RID: 73025 RVA: 0x002F2E00 File Offset: 0x002F1000
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Crosses.attributeNamespaceIds;
			}
		}

		// Token: 0x17005589 RID: 21897
		// (get) Token: 0x06011D42 RID: 73026 RVA: 0x002F2E07 File Offset: 0x002F1007
		// (set) Token: 0x06011D43 RID: 73027 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<CrossesValues> Val
		{
			get
			{
				return (EnumValue<CrossesValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011D45 RID: 73029 RVA: 0x002F2E16 File Offset: 0x002F1016
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<CrossesValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011D46 RID: 73030 RVA: 0x002F2E36 File Offset: 0x002F1036
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Crosses>(deep);
		}

		// Token: 0x06011D47 RID: 73031 RVA: 0x002F2E40 File Offset: 0x002F1040
		// Note: this type is marked as 'beforefieldinit'.
		static Crosses()
		{
			byte[] array = new byte[1];
			Crosses.attributeNamespaceIds = array;
		}

		// Token: 0x04007CBA RID: 31930
		private const string tagName = "crosses";

		// Token: 0x04007CBB RID: 31931
		private const byte tagNsId = 11;

		// Token: 0x04007CBC RID: 31932
		internal const int ElementTypeIdConst = 10384;

		// Token: 0x04007CBD RID: 31933
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007CBE RID: 31934
		private static byte[] attributeNamespaceIds;
	}
}
