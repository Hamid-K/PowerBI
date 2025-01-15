using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B4F RID: 11087
	[ChildElementInfo(typeof(Page))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Pages : OpenXmlCompositeElement
	{
		// Token: 0x1700783E RID: 30782
		// (get) Token: 0x06016BEA RID: 93162 RVA: 0x0032E9F4 File Offset: 0x0032CBF4
		public override string LocalName
		{
			get
			{
				return "pages";
			}
		}

		// Token: 0x1700783F RID: 30783
		// (get) Token: 0x06016BEB RID: 93163 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007840 RID: 30784
		// (get) Token: 0x06016BEC RID: 93164 RVA: 0x0032E9FB File Offset: 0x0032CBFB
		internal override int ElementTypeId
		{
			get
			{
				return 11070;
			}
		}

		// Token: 0x06016BED RID: 93165 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007841 RID: 30785
		// (get) Token: 0x06016BEE RID: 93166 RVA: 0x0032EA02 File Offset: 0x0032CC02
		internal override string[] AttributeTagNames
		{
			get
			{
				return Pages.attributeTagNames;
			}
		}

		// Token: 0x17007842 RID: 30786
		// (get) Token: 0x06016BEF RID: 93167 RVA: 0x0032EA09 File Offset: 0x0032CC09
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Pages.attributeNamespaceIds;
			}
		}

		// Token: 0x17007843 RID: 30787
		// (get) Token: 0x06016BF0 RID: 93168 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016BF1 RID: 93169 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016BF2 RID: 93170 RVA: 0x00293ECF File Offset: 0x002920CF
		public Pages()
		{
		}

		// Token: 0x06016BF3 RID: 93171 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Pages(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BF4 RID: 93172 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Pages(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016BF5 RID: 93173 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Pages(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016BF6 RID: 93174 RVA: 0x0032EA10 File Offset: 0x0032CC10
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "page" == name)
			{
				return new Page();
			}
			return null;
		}

		// Token: 0x06016BF7 RID: 93175 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016BF8 RID: 93176 RVA: 0x0032EA2B File Offset: 0x0032CC2B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pages>(deep);
		}

		// Token: 0x06016BF9 RID: 93177 RVA: 0x0032EA34 File Offset: 0x0032CC34
		// Note: this type is marked as 'beforefieldinit'.
		static Pages()
		{
			byte[] array = new byte[1];
			Pages.attributeNamespaceIds = array;
		}

		// Token: 0x040099C9 RID: 39369
		private const string tagName = "pages";

		// Token: 0x040099CA RID: 39370
		private const byte tagNsId = 22;

		// Token: 0x040099CB RID: 39371
		internal const int ElementTypeIdConst = 11070;

		// Token: 0x040099CC RID: 39372
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040099CD RID: 39373
		private static byte[] attributeNamespaceIds;
	}
}
