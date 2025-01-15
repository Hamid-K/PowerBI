using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB1 RID: 11441
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Field))]
	internal class ColumnFields : OpenXmlCompositeElement
	{
		// Token: 0x1700848E RID: 33934
		// (get) Token: 0x0601874B RID: 100171 RVA: 0x00341A73 File Offset: 0x0033FC73
		public override string LocalName
		{
			get
			{
				return "colFields";
			}
		}

		// Token: 0x1700848F RID: 33935
		// (get) Token: 0x0601874C RID: 100172 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008490 RID: 33936
		// (get) Token: 0x0601874D RID: 100173 RVA: 0x00341A7A File Offset: 0x0033FC7A
		internal override int ElementTypeId
		{
			get
			{
				return 11421;
			}
		}

		// Token: 0x0601874E RID: 100174 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008491 RID: 33937
		// (get) Token: 0x0601874F RID: 100175 RVA: 0x00341A81 File Offset: 0x0033FC81
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColumnFields.attributeTagNames;
			}
		}

		// Token: 0x17008492 RID: 33938
		// (get) Token: 0x06018750 RID: 100176 RVA: 0x00341A88 File Offset: 0x0033FC88
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColumnFields.attributeNamespaceIds;
			}
		}

		// Token: 0x17008493 RID: 33939
		// (get) Token: 0x06018751 RID: 100177 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018752 RID: 100178 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018753 RID: 100179 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColumnFields()
		{
		}

		// Token: 0x06018754 RID: 100180 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColumnFields(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018755 RID: 100181 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColumnFields(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018756 RID: 100182 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColumnFields(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018757 RID: 100183 RVA: 0x003419AF File Offset: 0x0033FBAF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "field" == name)
			{
				return new Field();
			}
			return null;
		}

		// Token: 0x06018758 RID: 100184 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018759 RID: 100185 RVA: 0x00341A8F File Offset: 0x0033FC8F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnFields>(deep);
		}

		// Token: 0x0601875A RID: 100186 RVA: 0x00341A98 File Offset: 0x0033FC98
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnFields()
		{
			byte[] array = new byte[1];
			ColumnFields.attributeNamespaceIds = array;
		}

		// Token: 0x0400A05A RID: 41050
		private const string tagName = "colFields";

		// Token: 0x0400A05B RID: 41051
		private const byte tagNsId = 22;

		// Token: 0x0400A05C RID: 41052
		internal const int ElementTypeIdConst = 11421;

		// Token: 0x0400A05D RID: 41053
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A05E RID: 41054
		private static byte[] attributeNamespaceIds;
	}
}
