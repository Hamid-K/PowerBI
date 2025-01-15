using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C23 RID: 11299
	[ChildElementInfo(typeof(Value))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Values : OpenXmlCompositeElement
	{
		// Token: 0x1700807E RID: 32894
		// (get) Token: 0x06017DB6 RID: 97718 RVA: 0x002E9B46 File Offset: 0x002E7D46
		public override string LocalName
		{
			get
			{
				return "values";
			}
		}

		// Token: 0x1700807F RID: 32895
		// (get) Token: 0x06017DB7 RID: 97719 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008080 RID: 32896
		// (get) Token: 0x06017DB8 RID: 97720 RVA: 0x0033BE72 File Offset: 0x0033A072
		internal override int ElementTypeId
		{
			get
			{
				return 11280;
			}
		}

		// Token: 0x06017DB9 RID: 97721 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008081 RID: 32897
		// (get) Token: 0x06017DBA RID: 97722 RVA: 0x0033BE79 File Offset: 0x0033A079
		internal override string[] AttributeTagNames
		{
			get
			{
				return Values.attributeTagNames;
			}
		}

		// Token: 0x17008082 RID: 32898
		// (get) Token: 0x06017DBB RID: 97723 RVA: 0x0033BE80 File Offset: 0x0033A080
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Values.attributeNamespaceIds;
			}
		}

		// Token: 0x17008083 RID: 32899
		// (get) Token: 0x06017DBC RID: 97724 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017DBD RID: 97725 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rows")]
		public UInt32Value Rows
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

		// Token: 0x17008084 RID: 32900
		// (get) Token: 0x06017DBE RID: 97726 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017DBF RID: 97727 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cols")]
		public UInt32Value Columns
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

		// Token: 0x06017DC0 RID: 97728 RVA: 0x00293ECF File Offset: 0x002920CF
		public Values()
		{
		}

		// Token: 0x06017DC1 RID: 97729 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Values(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DC2 RID: 97730 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Values(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DC3 RID: 97731 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Values(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017DC4 RID: 97732 RVA: 0x002E9B62 File Offset: 0x002E7D62
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "value" == name)
			{
				return new Value();
			}
			return null;
		}

		// Token: 0x06017DC5 RID: 97733 RVA: 0x002E9B7D File Offset: 0x002E7D7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rows" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cols" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017DC6 RID: 97734 RVA: 0x0033BE87 File Offset: 0x0033A087
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Values>(deep);
		}

		// Token: 0x06017DC7 RID: 97735 RVA: 0x0033BE90 File Offset: 0x0033A090
		// Note: this type is marked as 'beforefieldinit'.
		static Values()
		{
			byte[] array = new byte[2];
			Values.attributeNamespaceIds = array;
		}

		// Token: 0x04009DDE RID: 40414
		private const string tagName = "values";

		// Token: 0x04009DDF RID: 40415
		private const byte tagNsId = 22;

		// Token: 0x04009DE0 RID: 40416
		internal const int ElementTypeIdConst = 11280;

		// Token: 0x04009DE1 RID: 40417
		private static string[] attributeTagNames = new string[] { "rows", "cols" };

		// Token: 0x04009DE2 RID: 40418
		private static byte[] attributeNamespaceIds;
	}
}
