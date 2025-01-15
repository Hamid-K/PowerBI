using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF4 RID: 11252
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CharacterValue))]
	internal class MetadataStrings : OpenXmlCompositeElement
	{
		// Token: 0x17007ED2 RID: 32466
		// (get) Token: 0x06017A02 RID: 96770 RVA: 0x00339457 File Offset: 0x00337657
		public override string LocalName
		{
			get
			{
				return "metadataStrings";
			}
		}

		// Token: 0x17007ED3 RID: 32467
		// (get) Token: 0x06017A03 RID: 96771 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007ED4 RID: 32468
		// (get) Token: 0x06017A04 RID: 96772 RVA: 0x0033945E File Offset: 0x0033765E
		internal override int ElementTypeId
		{
			get
			{
				return 11232;
			}
		}

		// Token: 0x06017A05 RID: 96773 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007ED5 RID: 32469
		// (get) Token: 0x06017A06 RID: 96774 RVA: 0x00339465 File Offset: 0x00337665
		internal override string[] AttributeTagNames
		{
			get
			{
				return MetadataStrings.attributeTagNames;
			}
		}

		// Token: 0x17007ED6 RID: 32470
		// (get) Token: 0x06017A07 RID: 96775 RVA: 0x0033946C File Offset: 0x0033766C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MetadataStrings.attributeNamespaceIds;
			}
		}

		// Token: 0x17007ED7 RID: 32471
		// (get) Token: 0x06017A08 RID: 96776 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017A09 RID: 96777 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017A0A RID: 96778 RVA: 0x00293ECF File Offset: 0x002920CF
		public MetadataStrings()
		{
		}

		// Token: 0x06017A0B RID: 96779 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MetadataStrings(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A0C RID: 96780 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MetadataStrings(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A0D RID: 96781 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MetadataStrings(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A0E RID: 96782 RVA: 0x00339473 File Offset: 0x00337673
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "s" == name)
			{
				return new CharacterValue();
			}
			return null;
		}

		// Token: 0x06017A0F RID: 96783 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A10 RID: 96784 RVA: 0x0033948E File Offset: 0x0033768E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MetadataStrings>(deep);
		}

		// Token: 0x06017A11 RID: 96785 RVA: 0x00339498 File Offset: 0x00337698
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataStrings()
		{
			byte[] array = new byte[1];
			MetadataStrings.attributeNamespaceIds = array;
		}

		// Token: 0x04009CF2 RID: 40178
		private const string tagName = "metadataStrings";

		// Token: 0x04009CF3 RID: 40179
		private const byte tagNsId = 22;

		// Token: 0x04009CF4 RID: 40180
		internal const int ElementTypeIdConst = 11232;

		// Token: 0x04009CF5 RID: 40181
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009CF6 RID: 40182
		private static byte[] attributeNamespaceIds;
	}
}
