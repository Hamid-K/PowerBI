using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8D RID: 11405
	[ChildElementInfo(typeof(DataReference))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataReferences : OpenXmlCompositeElement
	{
		// Token: 0x170083A6 RID: 33702
		// (get) Token: 0x06018507 RID: 99591 RVA: 0x00340672 File Offset: 0x0033E872
		public override string LocalName
		{
			get
			{
				return "dataRefs";
			}
		}

		// Token: 0x170083A7 RID: 33703
		// (get) Token: 0x06018508 RID: 99592 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083A8 RID: 33704
		// (get) Token: 0x06018509 RID: 99593 RVA: 0x00340679 File Offset: 0x0033E879
		internal override int ElementTypeId
		{
			get
			{
				return 11384;
			}
		}

		// Token: 0x0601850A RID: 99594 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083A9 RID: 33705
		// (get) Token: 0x0601850B RID: 99595 RVA: 0x00340680 File Offset: 0x0033E880
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataReferences.attributeTagNames;
			}
		}

		// Token: 0x170083AA RID: 33706
		// (get) Token: 0x0601850C RID: 99596 RVA: 0x00340687 File Offset: 0x0033E887
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataReferences.attributeNamespaceIds;
			}
		}

		// Token: 0x170083AB RID: 33707
		// (get) Token: 0x0601850D RID: 99597 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601850E RID: 99598 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601850F RID: 99599 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataReferences()
		{
		}

		// Token: 0x06018510 RID: 99600 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataReferences(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018511 RID: 99601 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataReferences(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018512 RID: 99602 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataReferences(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018513 RID: 99603 RVA: 0x0034068E File Offset: 0x0033E88E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "dataRef" == name)
			{
				return new DataReference();
			}
			return null;
		}

		// Token: 0x06018514 RID: 99604 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018515 RID: 99605 RVA: 0x003406A9 File Offset: 0x0033E8A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataReferences>(deep);
		}

		// Token: 0x06018516 RID: 99606 RVA: 0x003406B4 File Offset: 0x0033E8B4
		// Note: this type is marked as 'beforefieldinit'.
		static DataReferences()
		{
			byte[] array = new byte[1];
			DataReferences.attributeNamespaceIds = array;
		}

		// Token: 0x04009FC2 RID: 40898
		private const string tagName = "dataRefs";

		// Token: 0x04009FC3 RID: 40899
		private const byte tagNsId = 22;

		// Token: 0x04009FC4 RID: 40900
		internal const int ElementTypeIdConst = 11384;

		// Token: 0x04009FC5 RID: 40901
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009FC6 RID: 40902
		private static byte[] attributeNamespaceIds;
	}
}
