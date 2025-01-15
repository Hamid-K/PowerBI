using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C47 RID: 11335
	[ChildElementInfo(typeof(DataField), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataFieldExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081C1 RID: 33217
		// (get) Token: 0x06018082 RID: 98434 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081C2 RID: 33218
		// (get) Token: 0x06018083 RID: 98435 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081C3 RID: 33219
		// (get) Token: 0x06018084 RID: 98436 RVA: 0x0033DD5B File Offset: 0x0033BF5B
		internal override int ElementTypeId
		{
			get
			{
				return 11316;
			}
		}

		// Token: 0x06018085 RID: 98437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081C4 RID: 33220
		// (get) Token: 0x06018086 RID: 98438 RVA: 0x0033DD62 File Offset: 0x0033BF62
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataFieldExtension.attributeTagNames;
			}
		}

		// Token: 0x170081C5 RID: 33221
		// (get) Token: 0x06018087 RID: 98439 RVA: 0x0033DD69 File Offset: 0x0033BF69
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataFieldExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081C6 RID: 33222
		// (get) Token: 0x06018088 RID: 98440 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018089 RID: 98441 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x0601808A RID: 98442 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataFieldExtension()
		{
		}

		// Token: 0x0601808B RID: 98443 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataFieldExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601808C RID: 98444 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataFieldExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601808D RID: 98445 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataFieldExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601808E RID: 98446 RVA: 0x0033DD70 File Offset: 0x0033BF70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "dataField" == name)
			{
				return new DataField();
			}
			return null;
		}

		// Token: 0x0601808F RID: 98447 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018090 RID: 98448 RVA: 0x0033DD8B File Offset: 0x0033BF8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataFieldExtension>(deep);
		}

		// Token: 0x06018091 RID: 98449 RVA: 0x0033DD94 File Offset: 0x0033BF94
		// Note: this type is marked as 'beforefieldinit'.
		static DataFieldExtension()
		{
			byte[] array = new byte[1];
			DataFieldExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E94 RID: 40596
		private const string tagName = "ext";

		// Token: 0x04009E95 RID: 40597
		private const byte tagNsId = 22;

		// Token: 0x04009E96 RID: 40598
		internal const int ElementTypeIdConst = 11316;

		// Token: 0x04009E97 RID: 40599
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E98 RID: 40600
		private static byte[] attributeNamespaceIds;
	}
}
