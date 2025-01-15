using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B69 RID: 11113
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TupleSet))]
	internal class Sets : OpenXmlCompositeElement
	{
		// Token: 0x1700791D RID: 31005
		// (get) Token: 0x06016DE7 RID: 93671 RVA: 0x0032FFD3 File Offset: 0x0032E1D3
		public override string LocalName
		{
			get
			{
				return "sets";
			}
		}

		// Token: 0x1700791E RID: 31006
		// (get) Token: 0x06016DE8 RID: 93672 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700791F RID: 31007
		// (get) Token: 0x06016DE9 RID: 93673 RVA: 0x0032FFDA File Offset: 0x0032E1DA
		internal override int ElementTypeId
		{
			get
			{
				return 11092;
			}
		}

		// Token: 0x06016DEA RID: 93674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007920 RID: 31008
		// (get) Token: 0x06016DEB RID: 93675 RVA: 0x0032FFE1 File Offset: 0x0032E1E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Sets.attributeTagNames;
			}
		}

		// Token: 0x17007921 RID: 31009
		// (get) Token: 0x06016DEC RID: 93676 RVA: 0x0032FFE8 File Offset: 0x0032E1E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Sets.attributeNamespaceIds;
			}
		}

		// Token: 0x17007922 RID: 31010
		// (get) Token: 0x06016DED RID: 93677 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016DEE RID: 93678 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06016DEF RID: 93679 RVA: 0x00293ECF File Offset: 0x002920CF
		public Sets()
		{
		}

		// Token: 0x06016DF0 RID: 93680 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Sets(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DF1 RID: 93681 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Sets(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016DF2 RID: 93682 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Sets(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016DF3 RID: 93683 RVA: 0x0032FFEF File Offset: 0x0032E1EF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "set" == name)
			{
				return new TupleSet();
			}
			return null;
		}

		// Token: 0x06016DF4 RID: 93684 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016DF5 RID: 93685 RVA: 0x0033000A File Offset: 0x0032E20A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sets>(deep);
		}

		// Token: 0x06016DF6 RID: 93686 RVA: 0x00330014 File Offset: 0x0032E214
		// Note: this type is marked as 'beforefieldinit'.
		static Sets()
		{
			byte[] array = new byte[1];
			Sets.attributeNamespaceIds = array;
		}

		// Token: 0x04009A41 RID: 39489
		private const string tagName = "sets";

		// Token: 0x04009A42 RID: 39490
		private const byte tagNsId = 22;

		// Token: 0x04009A43 RID: 39491
		internal const int ElementTypeIdConst = 11092;

		// Token: 0x04009A44 RID: 39492
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A45 RID: 39493
		private static byte[] attributeNamespaceIds;
	}
}
