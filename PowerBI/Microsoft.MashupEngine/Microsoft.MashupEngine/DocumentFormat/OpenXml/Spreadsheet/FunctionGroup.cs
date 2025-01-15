using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3C RID: 11324
	[GeneratedCode("DomGen", "2.0")]
	internal class FunctionGroup : OpenXmlLeafElement
	{
		// Token: 0x1700817D RID: 33149
		// (get) Token: 0x06017FD8 RID: 98264 RVA: 0x0033D747 File Offset: 0x0033B947
		public override string LocalName
		{
			get
			{
				return "functionGroup";
			}
		}

		// Token: 0x1700817E RID: 33150
		// (get) Token: 0x06017FD9 RID: 98265 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700817F RID: 33151
		// (get) Token: 0x06017FDA RID: 98266 RVA: 0x0033D74E File Offset: 0x0033B94E
		internal override int ElementTypeId
		{
			get
			{
				return 11306;
			}
		}

		// Token: 0x06017FDB RID: 98267 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008180 RID: 33152
		// (get) Token: 0x06017FDC RID: 98268 RVA: 0x0033D755 File Offset: 0x0033B955
		internal override string[] AttributeTagNames
		{
			get
			{
				return FunctionGroup.attributeTagNames;
			}
		}

		// Token: 0x17008181 RID: 33153
		// (get) Token: 0x06017FDD RID: 98269 RVA: 0x0033D75C File Offset: 0x0033B95C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FunctionGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17008182 RID: 33154
		// (get) Token: 0x06017FDE RID: 98270 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017FDF RID: 98271 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017FE1 RID: 98273 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017FE2 RID: 98274 RVA: 0x0033D763 File Offset: 0x0033B963
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FunctionGroup>(deep);
		}

		// Token: 0x06017FE3 RID: 98275 RVA: 0x0033D76C File Offset: 0x0033B96C
		// Note: this type is marked as 'beforefieldinit'.
		static FunctionGroup()
		{
			byte[] array = new byte[1];
			FunctionGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04009E62 RID: 40546
		private const string tagName = "functionGroup";

		// Token: 0x04009E63 RID: 40547
		private const byte tagNsId = 22;

		// Token: 0x04009E64 RID: 40548
		internal const int ElementTypeIdConst = 11306;

		// Token: 0x04009E65 RID: 40549
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04009E66 RID: 40550
		private static byte[] attributeNamespaceIds;
	}
}
