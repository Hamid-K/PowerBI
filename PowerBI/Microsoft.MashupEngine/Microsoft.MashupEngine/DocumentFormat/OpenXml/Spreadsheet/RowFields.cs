using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAF RID: 11439
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Field))]
	internal class RowFields : OpenXmlCompositeElement
	{
		// Token: 0x17008482 RID: 33922
		// (get) Token: 0x0601872B RID: 100139 RVA: 0x00341993 File Offset: 0x0033FB93
		public override string LocalName
		{
			get
			{
				return "rowFields";
			}
		}

		// Token: 0x17008483 RID: 33923
		// (get) Token: 0x0601872C RID: 100140 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008484 RID: 33924
		// (get) Token: 0x0601872D RID: 100141 RVA: 0x0034199A File Offset: 0x0033FB9A
		internal override int ElementTypeId
		{
			get
			{
				return 11419;
			}
		}

		// Token: 0x0601872E RID: 100142 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008485 RID: 33925
		// (get) Token: 0x0601872F RID: 100143 RVA: 0x003419A1 File Offset: 0x0033FBA1
		internal override string[] AttributeTagNames
		{
			get
			{
				return RowFields.attributeTagNames;
			}
		}

		// Token: 0x17008486 RID: 33926
		// (get) Token: 0x06018730 RID: 100144 RVA: 0x003419A8 File Offset: 0x0033FBA8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RowFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17008487 RID: 33927
		// (get) Token: 0x06018731 RID: 100145 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018732 RID: 100146 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018733 RID: 100147 RVA: 0x00293ECF File Offset: 0x002920CF
		public RowFields()
		{
		}

		// Token: 0x06018734 RID: 100148 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RowFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018735 RID: 100149 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RowFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018736 RID: 100150 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RowFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018737 RID: 100151 RVA: 0x003419AF File Offset: 0x0033FBAF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "field" == name)
			{
				return new Field();
			}
			return null;
		}

		// Token: 0x06018738 RID: 100152 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018739 RID: 100153 RVA: 0x003419CA File Offset: 0x0033FBCA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowFields>(deep);
		}

		// Token: 0x0601873A RID: 100154 RVA: 0x003419D4 File Offset: 0x0033FBD4
		// Note: this type is marked as 'beforefieldinit'.
		static RowFields()
		{
			byte[] array = new byte[1];
			RowFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A050 RID: 41040
		private const string tagName = "rowFields";

		// Token: 0x0400A051 RID: 41041
		private const byte tagNsId = 22;

		// Token: 0x0400A052 RID: 41042
		internal const int ElementTypeIdConst = 11419;

		// Token: 0x0400A053 RID: 41043
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A054 RID: 41044
		private static byte[] attributeNamespaceIds;
	}
}
