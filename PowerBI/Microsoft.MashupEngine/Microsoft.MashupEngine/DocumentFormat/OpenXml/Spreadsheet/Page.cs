using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B51 RID: 11089
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PageItem))]
	internal class Page : OpenXmlCompositeElement
	{
		// Token: 0x1700784A RID: 30794
		// (get) Token: 0x06016C0A RID: 93194 RVA: 0x0032EAD3 File Offset: 0x0032CCD3
		public override string LocalName
		{
			get
			{
				return "page";
			}
		}

		// Token: 0x1700784B RID: 30795
		// (get) Token: 0x06016C0B RID: 93195 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700784C RID: 30796
		// (get) Token: 0x06016C0C RID: 93196 RVA: 0x0032EADA File Offset: 0x0032CCDA
		internal override int ElementTypeId
		{
			get
			{
				return 11072;
			}
		}

		// Token: 0x06016C0D RID: 93197 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700784D RID: 30797
		// (get) Token: 0x06016C0E RID: 93198 RVA: 0x0032EAE1 File Offset: 0x0032CCE1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Page.attributeTagNames;
			}
		}

		// Token: 0x1700784E RID: 30798
		// (get) Token: 0x06016C0F RID: 93199 RVA: 0x0032EAE8 File Offset: 0x0032CCE8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Page.attributeNamespaceIds;
			}
		}

		// Token: 0x1700784F RID: 30799
		// (get) Token: 0x06016C10 RID: 93200 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016C11 RID: 93201 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016C12 RID: 93202 RVA: 0x00293ECF File Offset: 0x002920CF
		public Page()
		{
		}

		// Token: 0x06016C13 RID: 93203 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Page(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C14 RID: 93204 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Page(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016C15 RID: 93205 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Page(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016C16 RID: 93206 RVA: 0x0032EAEF File Offset: 0x0032CCEF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pageItem" == name)
			{
				return new PageItem();
			}
			return null;
		}

		// Token: 0x06016C17 RID: 93207 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016C18 RID: 93208 RVA: 0x0032EB0A File Offset: 0x0032CD0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Page>(deep);
		}

		// Token: 0x06016C19 RID: 93209 RVA: 0x0032EB14 File Offset: 0x0032CD14
		// Note: this type is marked as 'beforefieldinit'.
		static Page()
		{
			byte[] array = new byte[1];
			Page.attributeNamespaceIds = array;
		}

		// Token: 0x040099D3 RID: 39379
		private const string tagName = "page";

		// Token: 0x040099D4 RID: 39380
		private const byte tagNsId = 22;

		// Token: 0x040099D5 RID: 39381
		internal const int ElementTypeIdConst = 11072;

		// Token: 0x040099D6 RID: 39382
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040099D7 RID: 39383
		private static byte[] attributeNamespaceIds;
	}
}
