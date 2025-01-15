using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B61 RID: 11105
	[GeneratedCode("DomGen", "2.0")]
	internal class Kpi : OpenXmlLeafElement
	{
		// Token: 0x170078D3 RID: 30931
		// (get) Token: 0x06016D45 RID: 93509 RVA: 0x0032F8DB File Offset: 0x0032DADB
		public override string LocalName
		{
			get
			{
				return "kpi";
			}
		}

		// Token: 0x170078D4 RID: 30932
		// (get) Token: 0x06016D46 RID: 93510 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078D5 RID: 30933
		// (get) Token: 0x06016D47 RID: 93511 RVA: 0x0032F8E2 File Offset: 0x0032DAE2
		internal override int ElementTypeId
		{
			get
			{
				return 11084;
			}
		}

		// Token: 0x06016D48 RID: 93512 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078D6 RID: 30934
		// (get) Token: 0x06016D49 RID: 93513 RVA: 0x0032F8E9 File Offset: 0x0032DAE9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Kpi.attributeTagNames;
			}
		}

		// Token: 0x170078D7 RID: 30935
		// (get) Token: 0x06016D4A RID: 93514 RVA: 0x0032F8F0 File Offset: 0x0032DAF0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Kpi.attributeNamespaceIds;
			}
		}

		// Token: 0x170078D8 RID: 30936
		// (get) Token: 0x06016D4B RID: 93515 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016D4C RID: 93516 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x170078D9 RID: 30937
		// (get) Token: 0x06016D4D RID: 93517 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016D4E RID: 93518 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caption")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170078DA RID: 30938
		// (get) Token: 0x06016D4F RID: 93519 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016D50 RID: 93520 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "displayFolder")]
		public StringValue DisplayFolder
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170078DB RID: 30939
		// (get) Token: 0x06016D51 RID: 93521 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016D52 RID: 93522 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "measureGroup")]
		public StringValue MeasureGroup
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170078DC RID: 30940
		// (get) Token: 0x06016D53 RID: 93523 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06016D54 RID: 93524 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "parent")]
		public StringValue ParentKpi
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

		// Token: 0x170078DD RID: 30941
		// (get) Token: 0x06016D55 RID: 93525 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06016D56 RID: 93526 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "value")]
		public StringValue Value
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

		// Token: 0x170078DE RID: 30942
		// (get) Token: 0x06016D57 RID: 93527 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016D58 RID: 93528 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "goal")]
		public StringValue Goal
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

		// Token: 0x170078DF RID: 30943
		// (get) Token: 0x06016D59 RID: 93529 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06016D5A RID: 93530 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "status")]
		public StringValue Status
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

		// Token: 0x170078E0 RID: 30944
		// (get) Token: 0x06016D5B RID: 93531 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06016D5C RID: 93532 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "trend")]
		public StringValue Trend
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170078E1 RID: 30945
		// (get) Token: 0x06016D5D RID: 93533 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06016D5E RID: 93534 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "weight")]
		public StringValue Weight
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06016D60 RID: 93536 RVA: 0x0032F8F8 File Offset: 0x0032DAF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "displayFolder" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "measureGroup" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "parent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "value" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "goal" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "status" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "trend" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "weight" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D61 RID: 93537 RVA: 0x0032F9E9 File Offset: 0x0032DBE9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Kpi>(deep);
		}

		// Token: 0x06016D62 RID: 93538 RVA: 0x0032F9F4 File Offset: 0x0032DBF4
		// Note: this type is marked as 'beforefieldinit'.
		static Kpi()
		{
			byte[] array = new byte[10];
			Kpi.attributeNamespaceIds = array;
		}

		// Token: 0x04009A15 RID: 39445
		private const string tagName = "kpi";

		// Token: 0x04009A16 RID: 39446
		private const byte tagNsId = 22;

		// Token: 0x04009A17 RID: 39447
		internal const int ElementTypeIdConst = 11084;

		// Token: 0x04009A18 RID: 39448
		private static string[] attributeTagNames = new string[] { "uniqueName", "caption", "displayFolder", "measureGroup", "parent", "value", "goal", "status", "trend", "weight" };

		// Token: 0x04009A19 RID: 39449
		private static byte[] attributeNamespaceIds;
	}
}
