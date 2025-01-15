using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB9 RID: 11449
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotTableStyle : OpenXmlLeafElement
	{
		// Token: 0x170084BE RID: 33982
		// (get) Token: 0x060187CB RID: 100299 RVA: 0x00341DB3 File Offset: 0x0033FFB3
		public override string LocalName
		{
			get
			{
				return "pivotTableStyleInfo";
			}
		}

		// Token: 0x170084BF RID: 33983
		// (get) Token: 0x060187CC RID: 100300 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084C0 RID: 33984
		// (get) Token: 0x060187CD RID: 100301 RVA: 0x00341DBA File Offset: 0x0033FFBA
		internal override int ElementTypeId
		{
			get
			{
				return 11429;
			}
		}

		// Token: 0x060187CE RID: 100302 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084C1 RID: 33985
		// (get) Token: 0x060187CF RID: 100303 RVA: 0x00341DC1 File Offset: 0x0033FFC1
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotTableStyle.attributeTagNames;
			}
		}

		// Token: 0x170084C2 RID: 33986
		// (get) Token: 0x060187D0 RID: 100304 RVA: 0x00341DC8 File Offset: 0x0033FFC8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotTableStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x170084C3 RID: 33987
		// (get) Token: 0x060187D1 RID: 100305 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060187D2 RID: 100306 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170084C4 RID: 33988
		// (get) Token: 0x060187D3 RID: 100307 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060187D4 RID: 100308 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showRowHeaders")]
		public BooleanValue ShowRowHeaders
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170084C5 RID: 33989
		// (get) Token: 0x060187D5 RID: 100309 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060187D6 RID: 100310 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showColHeaders")]
		public BooleanValue ShowColumnHeaders
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170084C6 RID: 33990
		// (get) Token: 0x060187D7 RID: 100311 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060187D8 RID: 100312 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showRowStripes")]
		public BooleanValue ShowRowStripes
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170084C7 RID: 33991
		// (get) Token: 0x060187D9 RID: 100313 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060187DA RID: 100314 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showColStripes")]
		public BooleanValue ShowColumnStripes
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170084C8 RID: 33992
		// (get) Token: 0x060187DB RID: 100315 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060187DC RID: 100316 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "showLastColumn")]
		public BooleanValue ShowLastColumn
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060187DE RID: 100318 RVA: 0x00341DD0 File Offset: 0x0033FFD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showRowHeaders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showColHeaders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showRowStripes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showColStripes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showLastColumn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060187DF RID: 100319 RVA: 0x00341E69 File Offset: 0x00340069
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableStyle>(deep);
		}

		// Token: 0x060187E0 RID: 100320 RVA: 0x00341E74 File Offset: 0x00340074
		// Note: this type is marked as 'beforefieldinit'.
		static PivotTableStyle()
		{
			byte[] array = new byte[6];
			PivotTableStyle.attributeNamespaceIds = array;
		}

		// Token: 0x0400A082 RID: 41090
		private const string tagName = "pivotTableStyleInfo";

		// Token: 0x0400A083 RID: 41091
		private const byte tagNsId = 22;

		// Token: 0x0400A084 RID: 41092
		internal const int ElementTypeIdConst = 11429;

		// Token: 0x0400A085 RID: 41093
		private static string[] attributeTagNames = new string[] { "name", "showRowHeaders", "showColHeaders", "showRowStripes", "showColStripes", "showLastColumn" };

		// Token: 0x0400A086 RID: 41094
		private static byte[] attributeNamespaceIds;
	}
}
