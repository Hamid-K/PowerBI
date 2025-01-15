using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF5 RID: 11253
	[ChildElementInfo(typeof(Mdx))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MdxMetadata : OpenXmlCompositeElement
	{
		// Token: 0x17007ED8 RID: 32472
		// (get) Token: 0x06017A12 RID: 96786 RVA: 0x003394C7 File Offset: 0x003376C7
		public override string LocalName
		{
			get
			{
				return "mdxMetadata";
			}
		}

		// Token: 0x17007ED9 RID: 32473
		// (get) Token: 0x06017A13 RID: 96787 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EDA RID: 32474
		// (get) Token: 0x06017A14 RID: 96788 RVA: 0x003394CE File Offset: 0x003376CE
		internal override int ElementTypeId
		{
			get
			{
				return 11233;
			}
		}

		// Token: 0x06017A15 RID: 96789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007EDB RID: 32475
		// (get) Token: 0x06017A16 RID: 96790 RVA: 0x003394D5 File Offset: 0x003376D5
		internal override string[] AttributeTagNames
		{
			get
			{
				return MdxMetadata.attributeTagNames;
			}
		}

		// Token: 0x17007EDC RID: 32476
		// (get) Token: 0x06017A17 RID: 96791 RVA: 0x003394DC File Offset: 0x003376DC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MdxMetadata.attributeNamespaceIds;
			}
		}

		// Token: 0x17007EDD RID: 32477
		// (get) Token: 0x06017A18 RID: 96792 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017A19 RID: 96793 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017A1A RID: 96794 RVA: 0x00293ECF File Offset: 0x002920CF
		public MdxMetadata()
		{
		}

		// Token: 0x06017A1B RID: 96795 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MdxMetadata(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A1C RID: 96796 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MdxMetadata(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A1D RID: 96797 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MdxMetadata(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A1E RID: 96798 RVA: 0x003394E3 File Offset: 0x003376E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "mdx" == name)
			{
				return new Mdx();
			}
			return null;
		}

		// Token: 0x06017A1F RID: 96799 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A20 RID: 96800 RVA: 0x003394FE File Offset: 0x003376FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MdxMetadata>(deep);
		}

		// Token: 0x06017A21 RID: 96801 RVA: 0x00339508 File Offset: 0x00337708
		// Note: this type is marked as 'beforefieldinit'.
		static MdxMetadata()
		{
			byte[] array = new byte[1];
			MdxMetadata.attributeNamespaceIds = array;
		}

		// Token: 0x04009CF7 RID: 40183
		private const string tagName = "mdxMetadata";

		// Token: 0x04009CF8 RID: 40184
		private const byte tagNsId = 22;

		// Token: 0x04009CF9 RID: 40185
		internal const int ElementTypeIdConst = 11233;

		// Token: 0x04009CFA RID: 40186
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009CFB RID: 40187
		private static byte[] attributeNamespaceIds;
	}
}
