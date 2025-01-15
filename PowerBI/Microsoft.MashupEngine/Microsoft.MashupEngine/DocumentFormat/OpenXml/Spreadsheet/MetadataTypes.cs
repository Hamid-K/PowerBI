using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF3 RID: 11251
	[ChildElementInfo(typeof(MetadataType))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MetadataTypes : OpenXmlCompositeElement
	{
		// Token: 0x17007ECC RID: 32460
		// (get) Token: 0x060179F2 RID: 96754 RVA: 0x003393E7 File Offset: 0x003375E7
		public override string LocalName
		{
			get
			{
				return "metadataTypes";
			}
		}

		// Token: 0x17007ECD RID: 32461
		// (get) Token: 0x060179F3 RID: 96755 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007ECE RID: 32462
		// (get) Token: 0x060179F4 RID: 96756 RVA: 0x003393EE File Offset: 0x003375EE
		internal override int ElementTypeId
		{
			get
			{
				return 11231;
			}
		}

		// Token: 0x060179F5 RID: 96757 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007ECF RID: 32463
		// (get) Token: 0x060179F6 RID: 96758 RVA: 0x003393F5 File Offset: 0x003375F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return MetadataTypes.attributeTagNames;
			}
		}

		// Token: 0x17007ED0 RID: 32464
		// (get) Token: 0x060179F7 RID: 96759 RVA: 0x003393FC File Offset: 0x003375FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MetadataTypes.attributeNamespaceIds;
			}
		}

		// Token: 0x17007ED1 RID: 32465
		// (get) Token: 0x060179F8 RID: 96760 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060179F9 RID: 96761 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060179FA RID: 96762 RVA: 0x00293ECF File Offset: 0x002920CF
		public MetadataTypes()
		{
		}

		// Token: 0x060179FB RID: 96763 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MetadataTypes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179FC RID: 96764 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MetadataTypes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179FD RID: 96765 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MetadataTypes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060179FE RID: 96766 RVA: 0x00339403 File Offset: 0x00337603
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "metadataType" == name)
			{
				return new MetadataType();
			}
			return null;
		}

		// Token: 0x060179FF RID: 96767 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A00 RID: 96768 RVA: 0x0033941E File Offset: 0x0033761E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MetadataTypes>(deep);
		}

		// Token: 0x06017A01 RID: 96769 RVA: 0x00339428 File Offset: 0x00337628
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataTypes()
		{
			byte[] array = new byte[1];
			MetadataTypes.attributeNamespaceIds = array;
		}

		// Token: 0x04009CED RID: 40173
		private const string tagName = "metadataTypes";

		// Token: 0x04009CEE RID: 40174
		private const byte tagNsId = 22;

		// Token: 0x04009CEF RID: 40175
		internal const int ElementTypeIdConst = 11231;

		// Token: 0x04009CF0 RID: 40176
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009CF1 RID: 40177
		private static byte[] attributeNamespaceIds;
	}
}
