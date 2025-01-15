using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4F RID: 11343
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Connection), FileFormatVersions.Office2010)]
	internal class ConnectionExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081F1 RID: 33265
		// (get) Token: 0x06018102 RID: 98562 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081F2 RID: 33266
		// (get) Token: 0x06018103 RID: 98563 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081F3 RID: 33267
		// (get) Token: 0x06018104 RID: 98564 RVA: 0x0033E09B File Offset: 0x0033C29B
		internal override int ElementTypeId
		{
			get
			{
				return 11324;
			}
		}

		// Token: 0x06018105 RID: 98565 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081F4 RID: 33268
		// (get) Token: 0x06018106 RID: 98566 RVA: 0x0033E0A2 File Offset: 0x0033C2A2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionExtension.attributeTagNames;
			}
		}

		// Token: 0x170081F5 RID: 33269
		// (get) Token: 0x06018107 RID: 98567 RVA: 0x0033E0A9 File Offset: 0x0033C2A9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081F6 RID: 33270
		// (get) Token: 0x06018108 RID: 98568 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018109 RID: 98569 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601810A RID: 98570 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionExtension()
		{
		}

		// Token: 0x0601810B RID: 98571 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601810C RID: 98572 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601810D RID: 98573 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601810E RID: 98574 RVA: 0x0033E0B0 File Offset: 0x0033C2B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "connection" == name)
			{
				return new Connection();
			}
			return null;
		}

		// Token: 0x0601810F RID: 98575 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018110 RID: 98576 RVA: 0x0033E0CB File Offset: 0x0033C2CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionExtension>(deep);
		}

		// Token: 0x06018111 RID: 98577 RVA: 0x0033E0D4 File Offset: 0x0033C2D4
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionExtension()
		{
			byte[] array = new byte[1];
			ConnectionExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EBC RID: 40636
		private const string tagName = "ext";

		// Token: 0x04009EBD RID: 40637
		private const byte tagNsId = 22;

		// Token: 0x04009EBE RID: 40638
		internal const int ElementTypeIdConst = 11324;

		// Token: 0x04009EBF RID: 40639
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EC0 RID: 40640
		private static byte[] attributeNamespaceIds;
	}
}
