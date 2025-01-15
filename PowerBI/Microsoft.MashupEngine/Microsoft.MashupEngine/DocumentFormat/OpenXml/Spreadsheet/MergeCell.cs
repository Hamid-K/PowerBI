using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BED RID: 11245
	[GeneratedCode("DomGen", "2.0")]
	internal class MergeCell : OpenXmlLeafElement
	{
		// Token: 0x17007E55 RID: 32341
		// (get) Token: 0x060178FC RID: 96508 RVA: 0x003385EE File Offset: 0x003367EE
		public override string LocalName
		{
			get
			{
				return "mergeCell";
			}
		}

		// Token: 0x17007E56 RID: 32342
		// (get) Token: 0x060178FD RID: 96509 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E57 RID: 32343
		// (get) Token: 0x060178FE RID: 96510 RVA: 0x003385F5 File Offset: 0x003367F5
		internal override int ElementTypeId
		{
			get
			{
				return 11217;
			}
		}

		// Token: 0x060178FF RID: 96511 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E58 RID: 32344
		// (get) Token: 0x06017900 RID: 96512 RVA: 0x003385FC File Offset: 0x003367FC
		internal override string[] AttributeTagNames
		{
			get
			{
				return MergeCell.attributeTagNames;
			}
		}

		// Token: 0x17007E59 RID: 32345
		// (get) Token: 0x06017901 RID: 96513 RVA: 0x00338603 File Offset: 0x00336803
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MergeCell.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E5A RID: 32346
		// (get) Token: 0x06017902 RID: 96514 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017903 RID: 96515 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x06017905 RID: 96517 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017906 RID: 96518 RVA: 0x0033860A File Offset: 0x0033680A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MergeCell>(deep);
		}

		// Token: 0x06017907 RID: 96519 RVA: 0x00338614 File Offset: 0x00336814
		// Note: this type is marked as 'beforefieldinit'.
		static MergeCell()
		{
			byte[] array = new byte[1];
			MergeCell.attributeNamespaceIds = array;
		}

		// Token: 0x04009CC7 RID: 40135
		private const string tagName = "mergeCell";

		// Token: 0x04009CC8 RID: 40136
		private const byte tagNsId = 22;

		// Token: 0x04009CC9 RID: 40137
		internal const int ElementTypeIdConst = 11217;

		// Token: 0x04009CCA RID: 40138
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x04009CCB RID: 40139
		private static byte[] attributeNamespaceIds;
	}
}
