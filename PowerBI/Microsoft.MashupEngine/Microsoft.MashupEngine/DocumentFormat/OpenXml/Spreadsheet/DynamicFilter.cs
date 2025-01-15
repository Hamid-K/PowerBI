using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CE0 RID: 11488
	[GeneratedCode("DomGen", "2.0")]
	internal class DynamicFilter : OpenXmlLeafElement
	{
		// Token: 0x17008609 RID: 34313
		// (get) Token: 0x06018ACC RID: 101068 RVA: 0x00343D97 File Offset: 0x00341F97
		public override string LocalName
		{
			get
			{
				return "dynamicFilter";
			}
		}

		// Token: 0x1700860A RID: 34314
		// (get) Token: 0x06018ACD RID: 101069 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700860B RID: 34315
		// (get) Token: 0x06018ACE RID: 101070 RVA: 0x00343D9E File Offset: 0x00341F9E
		internal override int ElementTypeId
		{
			get
			{
				return 11470;
			}
		}

		// Token: 0x06018ACF RID: 101071 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700860C RID: 34316
		// (get) Token: 0x06018AD0 RID: 101072 RVA: 0x00343DA5 File Offset: 0x00341FA5
		internal override string[] AttributeTagNames
		{
			get
			{
				return DynamicFilter.attributeTagNames;
			}
		}

		// Token: 0x1700860D RID: 34317
		// (get) Token: 0x06018AD1 RID: 101073 RVA: 0x00343DAC File Offset: 0x00341FAC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DynamicFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x1700860E RID: 34318
		// (get) Token: 0x06018AD2 RID: 101074 RVA: 0x00343DB3 File Offset: 0x00341FB3
		// (set) Token: 0x06018AD3 RID: 101075 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<DynamicFilterValues> Type
		{
			get
			{
				return (EnumValue<DynamicFilterValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700860F RID: 34319
		// (get) Token: 0x06018AD4 RID: 101076 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x06018AD5 RID: 101077 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008610 RID: 34320
		// (get) Token: 0x06018AD6 RID: 101078 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06018AD7 RID: 101079 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "maxVal")]
		public DoubleValue MaxVal
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008611 RID: 34321
		// (get) Token: 0x06018AD8 RID: 101080 RVA: 0x00335DB2 File Offset: 0x00333FB2
		// (set) Token: 0x06018AD9 RID: 101081 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "valIso")]
		public DateTimeValue ValIso
		{
			get
			{
				return (DateTimeValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008612 RID: 34322
		// (get) Token: 0x06018ADA RID: 101082 RVA: 0x00343DC2 File Offset: 0x00341FC2
		// (set) Token: 0x06018ADB RID: 101083 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "maxValIso")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public DateTimeValue MaxValIso
		{
			get
			{
				return (DateTimeValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06018ADD RID: 101085 RVA: 0x00343DD4 File Offset: 0x00341FD4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<DynamicFilterValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "maxVal" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "valIso" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "maxValIso" == name)
			{
				return new DateTimeValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018ADE RID: 101086 RVA: 0x00343E57 File Offset: 0x00342057
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DynamicFilter>(deep);
		}

		// Token: 0x06018ADF RID: 101087 RVA: 0x00343E60 File Offset: 0x00342060
		// Note: this type is marked as 'beforefieldinit'.
		static DynamicFilter()
		{
			byte[] array = new byte[5];
			DynamicFilter.attributeNamespaceIds = array;
		}

		// Token: 0x0400A141 RID: 41281
		private const string tagName = "dynamicFilter";

		// Token: 0x0400A142 RID: 41282
		private const byte tagNsId = 22;

		// Token: 0x0400A143 RID: 41283
		internal const int ElementTypeIdConst = 11470;

		// Token: 0x0400A144 RID: 41284
		private static string[] attributeTagNames = new string[] { "type", "val", "maxVal", "valIso", "maxValIso" };

		// Token: 0x0400A145 RID: 41285
		private static byte[] attributeNamespaceIds;
	}
}
