using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BA7 RID: 11175
	[GeneratedCode("DomGen", "2.0")]
	internal class RunFont : OpenXmlLeafElement
	{
		// Token: 0x17007B5E RID: 31582
		// (get) Token: 0x060172BB RID: 94907 RVA: 0x003336C4 File Offset: 0x003318C4
		public override string LocalName
		{
			get
			{
				return "rFont";
			}
		}

		// Token: 0x17007B5F RID: 31583
		// (get) Token: 0x060172BC RID: 94908 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B60 RID: 31584
		// (get) Token: 0x060172BD RID: 94909 RVA: 0x003336CB File Offset: 0x003318CB
		internal override int ElementTypeId
		{
			get
			{
				return 11146;
			}
		}

		// Token: 0x060172BE RID: 94910 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B61 RID: 31585
		// (get) Token: 0x060172BF RID: 94911 RVA: 0x003336D2 File Offset: 0x003318D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return RunFont.attributeTagNames;
			}
		}

		// Token: 0x17007B62 RID: 31586
		// (get) Token: 0x060172C0 RID: 94912 RVA: 0x003336D9 File Offset: 0x003318D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RunFont.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B63 RID: 31587
		// (get) Token: 0x060172C1 RID: 94913 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060172C2 RID: 94914 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x060172C4 RID: 94916 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060172C5 RID: 94917 RVA: 0x003336E0 File Offset: 0x003318E0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunFont>(deep);
		}

		// Token: 0x060172C6 RID: 94918 RVA: 0x003336EC File Offset: 0x003318EC
		// Note: this type is marked as 'beforefieldinit'.
		static RunFont()
		{
			byte[] array = new byte[1];
			RunFont.attributeNamespaceIds = array;
		}

		// Token: 0x04009B69 RID: 39785
		private const string tagName = "rFont";

		// Token: 0x04009B6A RID: 39786
		private const byte tagNsId = 22;

		// Token: 0x04009B6B RID: 39787
		internal const int ElementTypeIdConst = 11146;

		// Token: 0x04009B6C RID: 39788
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B6D RID: 39789
		private static byte[] attributeNamespaceIds;
	}
}
