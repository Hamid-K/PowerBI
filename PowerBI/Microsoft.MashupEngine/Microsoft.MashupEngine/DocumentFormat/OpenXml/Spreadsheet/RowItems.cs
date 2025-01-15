using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB0 RID: 11440
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RowItem))]
	internal class RowItems : OpenXmlCompositeElement
	{
		// Token: 0x17008488 RID: 33928
		// (get) Token: 0x0601873B RID: 100155 RVA: 0x00341A03 File Offset: 0x0033FC03
		public override string LocalName
		{
			get
			{
				return "rowItems";
			}
		}

		// Token: 0x17008489 RID: 33929
		// (get) Token: 0x0601873C RID: 100156 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700848A RID: 33930
		// (get) Token: 0x0601873D RID: 100157 RVA: 0x00341A0A File Offset: 0x0033FC0A
		internal override int ElementTypeId
		{
			get
			{
				return 11420;
			}
		}

		// Token: 0x0601873E RID: 100158 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700848B RID: 33931
		// (get) Token: 0x0601873F RID: 100159 RVA: 0x00341A11 File Offset: 0x0033FC11
		internal override string[] AttributeTagNames
		{
			get
			{
				return RowItems.attributeTagNames;
			}
		}

		// Token: 0x1700848C RID: 33932
		// (get) Token: 0x06018740 RID: 100160 RVA: 0x00341A18 File Offset: 0x0033FC18
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RowItems.attributeNamespaceIds;
			}
		}

		// Token: 0x1700848D RID: 33933
		// (get) Token: 0x06018741 RID: 100161 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018742 RID: 100162 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018743 RID: 100163 RVA: 0x00293ECF File Offset: 0x002920CF
		public RowItems()
		{
		}

		// Token: 0x06018744 RID: 100164 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RowItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018745 RID: 100165 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RowItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018746 RID: 100166 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RowItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018747 RID: 100167 RVA: 0x00341A1F File Offset: 0x0033FC1F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "i" == name)
			{
				return new RowItem();
			}
			return null;
		}

		// Token: 0x06018748 RID: 100168 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018749 RID: 100169 RVA: 0x00341A3A File Offset: 0x0033FC3A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowItems>(deep);
		}

		// Token: 0x0601874A RID: 100170 RVA: 0x00341A44 File Offset: 0x0033FC44
		// Note: this type is marked as 'beforefieldinit'.
		static RowItems()
		{
			byte[] array = new byte[1];
			RowItems.attributeNamespaceIds = array;
		}

		// Token: 0x0400A055 RID: 41045
		private const string tagName = "rowItems";

		// Token: 0x0400A056 RID: 41046
		private const byte tagNsId = 22;

		// Token: 0x0400A057 RID: 41047
		internal const int ElementTypeIdConst = 11420;

		// Token: 0x0400A058 RID: 41048
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A059 RID: 41049
		private static byte[] attributeNamespaceIds;
	}
}
