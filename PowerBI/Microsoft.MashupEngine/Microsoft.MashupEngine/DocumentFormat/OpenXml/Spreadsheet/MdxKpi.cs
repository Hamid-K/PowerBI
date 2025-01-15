using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C02 RID: 11266
	[GeneratedCode("DomGen", "2.0")]
	internal class MdxKpi : OpenXmlLeafElement
	{
		// Token: 0x17007F4C RID: 32588
		// (get) Token: 0x06017B1C RID: 97052 RVA: 0x0033A033 File Offset: 0x00338233
		public override string LocalName
		{
			get
			{
				return "k";
			}
		}

		// Token: 0x17007F4D RID: 32589
		// (get) Token: 0x06017B1D RID: 97053 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F4E RID: 32590
		// (get) Token: 0x06017B1E RID: 97054 RVA: 0x0033A03A File Offset: 0x0033823A
		internal override int ElementTypeId
		{
			get
			{
				return 11245;
			}
		}

		// Token: 0x06017B1F RID: 97055 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F4F RID: 32591
		// (get) Token: 0x06017B20 RID: 97056 RVA: 0x0033A041 File Offset: 0x00338241
		internal override string[] AttributeTagNames
		{
			get
			{
				return MdxKpi.attributeTagNames;
			}
		}

		// Token: 0x17007F50 RID: 32592
		// (get) Token: 0x06017B21 RID: 97057 RVA: 0x0033A048 File Offset: 0x00338248
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MdxKpi.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F51 RID: 32593
		// (get) Token: 0x06017B22 RID: 97058 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017B23 RID: 97059 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public UInt32Value NameIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007F52 RID: 32594
		// (get) Token: 0x06017B24 RID: 97060 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017B25 RID: 97061 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "np")]
		public UInt32Value KpiIndex
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

		// Token: 0x17007F53 RID: 32595
		// (get) Token: 0x06017B26 RID: 97062 RVA: 0x0033A04F File Offset: 0x0033824F
		// (set) Token: 0x06017B27 RID: 97063 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "p")]
		public EnumValue<MdxKPIPropertyValues> KpiProperty
		{
			get
			{
				return (EnumValue<MdxKPIPropertyValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017B29 RID: 97065 RVA: 0x0033A060 File Offset: 0x00338260
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "np" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "p" == name)
			{
				return new EnumValue<MdxKPIPropertyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B2A RID: 97066 RVA: 0x0033A0B7 File Offset: 0x003382B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MdxKpi>(deep);
		}

		// Token: 0x06017B2B RID: 97067 RVA: 0x0033A0C0 File Offset: 0x003382C0
		// Note: this type is marked as 'beforefieldinit'.
		static MdxKpi()
		{
			byte[] array = new byte[3];
			MdxKpi.attributeNamespaceIds = array;
		}

		// Token: 0x04009D31 RID: 40241
		private const string tagName = "k";

		// Token: 0x04009D32 RID: 40242
		private const byte tagNsId = 22;

		// Token: 0x04009D33 RID: 40243
		internal const int ElementTypeIdConst = 11245;

		// Token: 0x04009D34 RID: 40244
		private static string[] attributeTagNames = new string[] { "n", "np", "p" };

		// Token: 0x04009D35 RID: 40245
		private static byte[] attributeNamespaceIds;
	}
}
