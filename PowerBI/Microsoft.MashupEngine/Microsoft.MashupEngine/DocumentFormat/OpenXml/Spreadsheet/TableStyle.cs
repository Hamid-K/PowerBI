using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C12 RID: 11282
	[ChildElementInfo(typeof(TableStyleElement))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableStyle : OpenXmlCompositeElement
	{
		// Token: 0x17007FF4 RID: 32756
		// (get) Token: 0x06017C82 RID: 97410 RVA: 0x0030DEB2 File Offset: 0x0030C0B2
		public override string LocalName
		{
			get
			{
				return "tableStyle";
			}
		}

		// Token: 0x17007FF5 RID: 32757
		// (get) Token: 0x06017C83 RID: 97411 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FF6 RID: 32758
		// (get) Token: 0x06017C84 RID: 97412 RVA: 0x0033B231 File Offset: 0x00339431
		internal override int ElementTypeId
		{
			get
			{
				return 11263;
			}
		}

		// Token: 0x06017C85 RID: 97413 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007FF7 RID: 32759
		// (get) Token: 0x06017C86 RID: 97414 RVA: 0x0033B238 File Offset: 0x00339438
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyle.attributeTagNames;
			}
		}

		// Token: 0x17007FF8 RID: 32760
		// (get) Token: 0x06017C87 RID: 97415 RVA: 0x0033B23F File Offset: 0x0033943F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17007FF9 RID: 32761
		// (get) Token: 0x06017C88 RID: 97416 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017C89 RID: 97417 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007FFA RID: 32762
		// (get) Token: 0x06017C8A RID: 97418 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017C8B RID: 97419 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pivot")]
		public BooleanValue Pivot
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

		// Token: 0x17007FFB RID: 32763
		// (get) Token: 0x06017C8C RID: 97420 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017C8D RID: 97421 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "table")]
		public BooleanValue Table
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

		// Token: 0x17007FFC RID: 32764
		// (get) Token: 0x06017C8E RID: 97422 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017C8F RID: 97423 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06017C90 RID: 97424 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyle()
		{
		}

		// Token: 0x06017C91 RID: 97425 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C92 RID: 97426 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C93 RID: 97427 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C94 RID: 97428 RVA: 0x0033B246 File Offset: 0x00339446
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tableStyleElement" == name)
			{
				return new TableStyleElement();
			}
			return null;
		}

		// Token: 0x06017C95 RID: 97429 RVA: 0x0033B264 File Offset: 0x00339464
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pivot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "table" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017C96 RID: 97430 RVA: 0x0033B2D1 File Offset: 0x003394D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyle>(deep);
		}

		// Token: 0x06017C97 RID: 97431 RVA: 0x0033B2DC File Offset: 0x003394DC
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyle()
		{
			byte[] array = new byte[4];
			TableStyle.attributeNamespaceIds = array;
		}

		// Token: 0x04009D89 RID: 40329
		private const string tagName = "tableStyle";

		// Token: 0x04009D8A RID: 40330
		private const byte tagNsId = 22;

		// Token: 0x04009D8B RID: 40331
		internal const int ElementTypeIdConst = 11263;

		// Token: 0x04009D8C RID: 40332
		private static string[] attributeTagNames = new string[] { "name", "pivot", "table", "count" };

		// Token: 0x04009D8D RID: 40333
		private static byte[] attributeNamespaceIds;
	}
}
