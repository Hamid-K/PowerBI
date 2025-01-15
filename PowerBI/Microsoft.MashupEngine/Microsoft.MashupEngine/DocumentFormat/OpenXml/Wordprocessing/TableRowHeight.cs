using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F01 RID: 12033
	[GeneratedCode("DomGen", "2.0")]
	internal class TableRowHeight : OpenXmlLeafElement
	{
		// Token: 0x17008DC6 RID: 36294
		// (get) Token: 0x06019ADE RID: 105182 RVA: 0x00353CA4 File Offset: 0x00351EA4
		public override string LocalName
		{
			get
			{
				return "trHeight";
			}
		}

		// Token: 0x17008DC7 RID: 36295
		// (get) Token: 0x06019ADF RID: 105183 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DC8 RID: 36296
		// (get) Token: 0x06019AE0 RID: 105184 RVA: 0x00353CAB File Offset: 0x00351EAB
		internal override int ElementTypeId
		{
			get
			{
				return 11665;
			}
		}

		// Token: 0x06019AE1 RID: 105185 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DC9 RID: 36297
		// (get) Token: 0x06019AE2 RID: 105186 RVA: 0x00353CB2 File Offset: 0x00351EB2
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableRowHeight.attributeTagNames;
			}
		}

		// Token: 0x17008DCA RID: 36298
		// (get) Token: 0x06019AE3 RID: 105187 RVA: 0x00353CB9 File Offset: 0x00351EB9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableRowHeight.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DCB RID: 36299
		// (get) Token: 0x06019AE4 RID: 105188 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06019AE5 RID: 105189 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt32Value Val
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

		// Token: 0x17008DCC RID: 36300
		// (get) Token: 0x06019AE6 RID: 105190 RVA: 0x00353CC0 File Offset: 0x00351EC0
		// (set) Token: 0x06019AE7 RID: 105191 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "hRule")]
		public EnumValue<HeightRuleValues> HeightType
		{
			get
			{
				return (EnumValue<HeightRuleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06019AE9 RID: 105193 RVA: 0x00353CCF File Offset: 0x00351ECF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "hRule" == name)
			{
				return new EnumValue<HeightRuleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019AEA RID: 105194 RVA: 0x00353D09 File Offset: 0x00351F09
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableRowHeight>(deep);
		}

		// Token: 0x0400AA16 RID: 43542
		private const string tagName = "trHeight";

		// Token: 0x0400AA17 RID: 43543
		private const byte tagNsId = 23;

		// Token: 0x0400AA18 RID: 43544
		internal const int ElementTypeIdConst = 11665;

		// Token: 0x0400AA19 RID: 43545
		private static string[] attributeTagNames = new string[] { "val", "hRule" };

		// Token: 0x0400AA1A RID: 43546
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
