using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF6 RID: 11254
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FutureMetadataBlock))]
	internal class FutureMetadata : OpenXmlCompositeElement
	{
		// Token: 0x17007EDE RID: 32478
		// (get) Token: 0x06017A22 RID: 96802 RVA: 0x00339537 File Offset: 0x00337737
		public override string LocalName
		{
			get
			{
				return "futureMetadata";
			}
		}

		// Token: 0x17007EDF RID: 32479
		// (get) Token: 0x06017A23 RID: 96803 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EE0 RID: 32480
		// (get) Token: 0x06017A24 RID: 96804 RVA: 0x0033953E File Offset: 0x0033773E
		internal override int ElementTypeId
		{
			get
			{
				return 11234;
			}
		}

		// Token: 0x06017A25 RID: 96805 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007EE1 RID: 32481
		// (get) Token: 0x06017A26 RID: 96806 RVA: 0x00339545 File Offset: 0x00337745
		internal override string[] AttributeTagNames
		{
			get
			{
				return FutureMetadata.attributeTagNames;
			}
		}

		// Token: 0x17007EE2 RID: 32482
		// (get) Token: 0x06017A27 RID: 96807 RVA: 0x0033954C File Offset: 0x0033774C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FutureMetadata.attributeNamespaceIds;
			}
		}

		// Token: 0x17007EE3 RID: 32483
		// (get) Token: 0x06017A28 RID: 96808 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017A29 RID: 96809 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007EE4 RID: 32484
		// (get) Token: 0x06017A2A RID: 96810 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017A2B RID: 96811 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017A2C RID: 96812 RVA: 0x00293ECF File Offset: 0x002920CF
		public FutureMetadata()
		{
		}

		// Token: 0x06017A2D RID: 96813 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FutureMetadata(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A2E RID: 96814 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FutureMetadata(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A2F RID: 96815 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FutureMetadata(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A30 RID: 96816 RVA: 0x00339553 File Offset: 0x00337753
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "bk" == name)
			{
				return new FutureMetadataBlock();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06017A31 RID: 96817 RVA: 0x00339586 File Offset: 0x00337786
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A32 RID: 96818 RVA: 0x003395BC File Offset: 0x003377BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FutureMetadata>(deep);
		}

		// Token: 0x06017A33 RID: 96819 RVA: 0x003395C8 File Offset: 0x003377C8
		// Note: this type is marked as 'beforefieldinit'.
		static FutureMetadata()
		{
			byte[] array = new byte[2];
			FutureMetadata.attributeNamespaceIds = array;
		}

		// Token: 0x04009CFC RID: 40188
		private const string tagName = "futureMetadata";

		// Token: 0x04009CFD RID: 40189
		private const byte tagNsId = 22;

		// Token: 0x04009CFE RID: 40190
		internal const int ElementTypeIdConst = 11234;

		// Token: 0x04009CFF RID: 40191
		private static string[] attributeTagNames = new string[] { "name", "count" };

		// Token: 0x04009D00 RID: 40192
		private static byte[] attributeNamespaceIds;
	}
}
