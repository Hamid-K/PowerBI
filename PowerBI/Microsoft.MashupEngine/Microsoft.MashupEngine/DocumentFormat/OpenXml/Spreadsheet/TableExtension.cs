using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3E RID: 11326
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Table), FileFormatVersions.Office2010)]
	internal class TableExtension : OpenXmlCompositeElement
	{
		// Token: 0x17008189 RID: 33161
		// (get) Token: 0x06017FF4 RID: 98292 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x1700818A RID: 33162
		// (get) Token: 0x06017FF5 RID: 98293 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700818B RID: 33163
		// (get) Token: 0x06017FF6 RID: 98294 RVA: 0x0033D857 File Offset: 0x0033BA57
		internal override int ElementTypeId
		{
			get
			{
				return 11308;
			}
		}

		// Token: 0x06017FF7 RID: 98295 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700818C RID: 33164
		// (get) Token: 0x06017FF8 RID: 98296 RVA: 0x0033D85E File Offset: 0x0033BA5E
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableExtension.attributeTagNames;
			}
		}

		// Token: 0x1700818D RID: 33165
		// (get) Token: 0x06017FF9 RID: 98297 RVA: 0x0033D865 File Offset: 0x0033BA65
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x1700818E RID: 33166
		// (get) Token: 0x06017FFA RID: 98298 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017FFB RID: 98299 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017FFC RID: 98300 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableExtension()
		{
		}

		// Token: 0x06017FFD RID: 98301 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FFE RID: 98302 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FFF RID: 98303 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018000 RID: 98304 RVA: 0x0033D86C File Offset: 0x0033BA6C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "table" == name)
			{
				return new Table();
			}
			return null;
		}

		// Token: 0x06018001 RID: 98305 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018002 RID: 98306 RVA: 0x0033D887 File Offset: 0x0033BA87
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableExtension>(deep);
		}

		// Token: 0x06018003 RID: 98307 RVA: 0x0033D890 File Offset: 0x0033BA90
		// Note: this type is marked as 'beforefieldinit'.
		static TableExtension()
		{
			byte[] array = new byte[1];
			TableExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E6C RID: 40556
		private const string tagName = "ext";

		// Token: 0x04009E6D RID: 40557
		private const byte tagNsId = 22;

		// Token: 0x04009E6E RID: 40558
		internal const int ElementTypeIdConst = 11308;

		// Token: 0x04009E6F RID: 40559
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E70 RID: 40560
		private static byte[] attributeNamespaceIds;
	}
}
