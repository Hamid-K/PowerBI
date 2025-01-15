using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A1 RID: 9377
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PresetLineDashProperties : OpenXmlLeafElement
	{
		// Token: 0x170051CF RID: 20943
		// (get) Token: 0x06011517 RID: 70935 RVA: 0x002ED194 File Offset: 0x002EB394
		public override string LocalName
		{
			get
			{
				return "prstDash";
			}
		}

		// Token: 0x170051D0 RID: 20944
		// (get) Token: 0x06011518 RID: 70936 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051D1 RID: 20945
		// (get) Token: 0x06011519 RID: 70937 RVA: 0x002ED19B File Offset: 0x002EB39B
		internal override int ElementTypeId
		{
			get
			{
				return 12849;
			}
		}

		// Token: 0x0601151A RID: 70938 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051D2 RID: 20946
		// (get) Token: 0x0601151B RID: 70939 RVA: 0x002ED1A2 File Offset: 0x002EB3A2
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetLineDashProperties.attributeTagNames;
			}
		}

		// Token: 0x170051D3 RID: 20947
		// (get) Token: 0x0601151C RID: 70940 RVA: 0x002ED1A9 File Offset: 0x002EB3A9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetLineDashProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170051D4 RID: 20948
		// (get) Token: 0x0601151D RID: 70941 RVA: 0x002ED1B0 File Offset: 0x002EB3B0
		// (set) Token: 0x0601151E RID: 70942 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
		public EnumValue<PresetLineDashValues> Val
		{
			get
			{
				return (EnumValue<PresetLineDashValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011520 RID: 70944 RVA: 0x002ED1BF File Offset: 0x002EB3BF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<PresetLineDashValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011521 RID: 70945 RVA: 0x002ED1E1 File Offset: 0x002EB3E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetLineDashProperties>(deep);
		}

		// Token: 0x04007947 RID: 31047
		private const string tagName = "prstDash";

		// Token: 0x04007948 RID: 31048
		private const byte tagNsId = 52;

		// Token: 0x04007949 RID: 31049
		internal const int ElementTypeIdConst = 12849;

		// Token: 0x0400794A RID: 31050
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400794B RID: 31051
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
