using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C80 RID: 11392
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartSheetProtection : OpenXmlLeafElement
	{
		// Token: 0x1700834D RID: 33613
		// (get) Token: 0x0601843C RID: 99388 RVA: 0x0033FEB8 File Offset: 0x0033E0B8
		public override string LocalName
		{
			get
			{
				return "sheetProtection";
			}
		}

		// Token: 0x1700834E RID: 33614
		// (get) Token: 0x0601843D RID: 99389 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700834F RID: 33615
		// (get) Token: 0x0601843E RID: 99390 RVA: 0x0033FEBF File Offset: 0x0033E0BF
		internal override int ElementTypeId
		{
			get
			{
				return 11372;
			}
		}

		// Token: 0x0601843F RID: 99391 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008350 RID: 33616
		// (get) Token: 0x06018440 RID: 99392 RVA: 0x0033FEC6 File Offset: 0x0033E0C6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartSheetProtection.attributeTagNames;
			}
		}

		// Token: 0x17008351 RID: 33617
		// (get) Token: 0x06018441 RID: 99393 RVA: 0x0033FECD File Offset: 0x0033E0CD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartSheetProtection.attributeNamespaceIds;
			}
		}

		// Token: 0x17008352 RID: 33618
		// (get) Token: 0x06018442 RID: 99394 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06018443 RID: 99395 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "password")]
		public HexBinaryValue Password
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008353 RID: 33619
		// (get) Token: 0x06018444 RID: 99396 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06018445 RID: 99397 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "algorithmName")]
		public StringValue AlgorithmName
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

		// Token: 0x17008354 RID: 33620
		// (get) Token: 0x06018446 RID: 99398 RVA: 0x002EA13F File Offset: 0x002E833F
		// (set) Token: 0x06018447 RID: 99399 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008355 RID: 33621
		// (get) Token: 0x06018448 RID: 99400 RVA: 0x002EA14E File Offset: 0x002E834E
		// (set) Token: 0x06018449 RID: 99401 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008356 RID: 33622
		// (get) Token: 0x0601844A RID: 99402 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601844B RID: 99403 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008357 RID: 33623
		// (get) Token: 0x0601844C RID: 99404 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601844D RID: 99405 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "content")]
		public BooleanValue Content
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008358 RID: 33624
		// (get) Token: 0x0601844E RID: 99406 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0601844F RID: 99407 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "objects")]
		public BooleanValue Objects
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06018451 RID: 99409 RVA: 0x0033FED4 File Offset: 0x0033E0D4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "password" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "content" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "objects" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018452 RID: 99410 RVA: 0x0033FF83 File Offset: 0x0033E183
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSheetProtection>(deep);
		}

		// Token: 0x06018453 RID: 99411 RVA: 0x0033FF8C File Offset: 0x0033E18C
		// Note: this type is marked as 'beforefieldinit'.
		static ChartSheetProtection()
		{
			byte[] array = new byte[7];
			ChartSheetProtection.attributeNamespaceIds = array;
		}

		// Token: 0x04009F8E RID: 40846
		private const string tagName = "sheetProtection";

		// Token: 0x04009F8F RID: 40847
		private const byte tagNsId = 22;

		// Token: 0x04009F90 RID: 40848
		internal const int ElementTypeIdConst = 11372;

		// Token: 0x04009F91 RID: 40849
		private static string[] attributeTagNames = new string[] { "password", "algorithmName", "hashValue", "saltValue", "spinCount", "content", "objects" };

		// Token: 0x04009F92 RID: 40850
		private static byte[] attributeNamespaceIds;
	}
}
