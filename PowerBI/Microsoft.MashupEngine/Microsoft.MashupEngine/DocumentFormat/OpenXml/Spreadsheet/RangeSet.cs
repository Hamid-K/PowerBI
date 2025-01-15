using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B53 RID: 11091
	[GeneratedCode("DomGen", "2.0")]
	internal class RangeSet : OpenXmlLeafElement
	{
		// Token: 0x17007856 RID: 30806
		// (get) Token: 0x06016C26 RID: 93222 RVA: 0x0032EB97 File Offset: 0x0032CD97
		public override string LocalName
		{
			get
			{
				return "rangeSet";
			}
		}

		// Token: 0x17007857 RID: 30807
		// (get) Token: 0x06016C27 RID: 93223 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007858 RID: 30808
		// (get) Token: 0x06016C28 RID: 93224 RVA: 0x0032EB9E File Offset: 0x0032CD9E
		internal override int ElementTypeId
		{
			get
			{
				return 11074;
			}
		}

		// Token: 0x06016C29 RID: 93225 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007859 RID: 30809
		// (get) Token: 0x06016C2A RID: 93226 RVA: 0x0032EBA5 File Offset: 0x0032CDA5
		internal override string[] AttributeTagNames
		{
			get
			{
				return RangeSet.attributeTagNames;
			}
		}

		// Token: 0x1700785A RID: 30810
		// (get) Token: 0x06016C2B RID: 93227 RVA: 0x0032EBAC File Offset: 0x0032CDAC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RangeSet.attributeNamespaceIds;
			}
		}

		// Token: 0x1700785B RID: 30811
		// (get) Token: 0x06016C2C RID: 93228 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016C2D RID: 93229 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "i1")]
		public UInt32Value FieldItemIndexPage1
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

		// Token: 0x1700785C RID: 30812
		// (get) Token: 0x06016C2E RID: 93230 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016C2F RID: 93231 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "i2")]
		public UInt32Value FieldItemIndexPage2
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

		// Token: 0x1700785D RID: 30813
		// (get) Token: 0x06016C30 RID: 93232 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06016C31 RID: 93233 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "i3")]
		public UInt32Value FieldItemIndexPage3
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700785E RID: 30814
		// (get) Token: 0x06016C32 RID: 93234 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06016C33 RID: 93235 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "i4")]
		public UInt32Value FieldItemIndexPage4
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700785F RID: 30815
		// (get) Token: 0x06016C34 RID: 93236 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06016C35 RID: 93237 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ref")]
		public StringValue Reference
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007860 RID: 30816
		// (get) Token: 0x06016C36 RID: 93238 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06016C37 RID: 93239 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007861 RID: 30817
		// (get) Token: 0x06016C38 RID: 93240 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016C39 RID: 93241 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "sheet")]
		public StringValue Sheet
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007862 RID: 30818
		// (get) Token: 0x06016C3A RID: 93242 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06016C3B RID: 93243 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06016C3D RID: 93245 RVA: 0x0032EBB4 File Offset: 0x0032CDB4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "i1" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "i2" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "i3" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "i4" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sheet" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016C3E RID: 93246 RVA: 0x0032EC7B File Offset: 0x0032CE7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RangeSet>(deep);
		}

		// Token: 0x040099DD RID: 39389
		private const string tagName = "rangeSet";

		// Token: 0x040099DE RID: 39390
		private const byte tagNsId = 22;

		// Token: 0x040099DF RID: 39391
		internal const int ElementTypeIdConst = 11074;

		// Token: 0x040099E0 RID: 39392
		private static string[] attributeTagNames = new string[] { "i1", "i2", "i3", "i4", "ref", "name", "sheet", "id" };

		// Token: 0x040099E1 RID: 39393
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 0, 0, 0, 19 };
	}
}
