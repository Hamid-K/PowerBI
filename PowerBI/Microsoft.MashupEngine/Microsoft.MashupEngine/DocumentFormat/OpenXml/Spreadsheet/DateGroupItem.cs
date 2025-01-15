using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDC RID: 11484
	[GeneratedCode("DomGen", "2.0")]
	internal class DateGroupItem : OpenXmlLeafElement
	{
		// Token: 0x170085E7 RID: 34279
		// (get) Token: 0x06018A80 RID: 100992 RVA: 0x003439DF File Offset: 0x00341BDF
		public override string LocalName
		{
			get
			{
				return "dateGroupItem";
			}
		}

		// Token: 0x170085E8 RID: 34280
		// (get) Token: 0x06018A81 RID: 100993 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085E9 RID: 34281
		// (get) Token: 0x06018A82 RID: 100994 RVA: 0x003439E6 File Offset: 0x00341BE6
		internal override int ElementTypeId
		{
			get
			{
				return 11466;
			}
		}

		// Token: 0x06018A83 RID: 100995 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085EA RID: 34282
		// (get) Token: 0x06018A84 RID: 100996 RVA: 0x003439ED File Offset: 0x00341BED
		internal override string[] AttributeTagNames
		{
			get
			{
				return DateGroupItem.attributeTagNames;
			}
		}

		// Token: 0x170085EB RID: 34283
		// (get) Token: 0x06018A85 RID: 100997 RVA: 0x003439F4 File Offset: 0x00341BF4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DateGroupItem.attributeNamespaceIds;
			}
		}

		// Token: 0x170085EC RID: 34284
		// (get) Token: 0x06018A86 RID: 100998 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06018A87 RID: 100999 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "year")]
		public UInt16Value Year
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170085ED RID: 34285
		// (get) Token: 0x06018A88 RID: 101000 RVA: 0x002F0823 File Offset: 0x002EEA23
		// (set) Token: 0x06018A89 RID: 101001 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "month")]
		public UInt16Value Month
		{
			get
			{
				return (UInt16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170085EE RID: 34286
		// (get) Token: 0x06018A8A RID: 101002 RVA: 0x003439FB File Offset: 0x00341BFB
		// (set) Token: 0x06018A8B RID: 101003 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "day")]
		public UInt16Value Day
		{
			get
			{
				return (UInt16Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170085EF RID: 34287
		// (get) Token: 0x06018A8C RID: 101004 RVA: 0x00343A0A File Offset: 0x00341C0A
		// (set) Token: 0x06018A8D RID: 101005 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hour")]
		public UInt16Value Hour
		{
			get
			{
				return (UInt16Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170085F0 RID: 34288
		// (get) Token: 0x06018A8E RID: 101006 RVA: 0x00343A19 File Offset: 0x00341C19
		// (set) Token: 0x06018A8F RID: 101007 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "minute")]
		public UInt16Value Minute
		{
			get
			{
				return (UInt16Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170085F1 RID: 34289
		// (get) Token: 0x06018A90 RID: 101008 RVA: 0x00343A28 File Offset: 0x00341C28
		// (set) Token: 0x06018A91 RID: 101009 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "second")]
		public UInt16Value Second
		{
			get
			{
				return (UInt16Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170085F2 RID: 34290
		// (get) Token: 0x06018A92 RID: 101010 RVA: 0x00343A37 File Offset: 0x00341C37
		// (set) Token: 0x06018A93 RID: 101011 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "dateTimeGrouping")]
		public EnumValue<DateTimeGroupingValues> DateTimeGrouping
		{
			get
			{
				return (EnumValue<DateTimeGroupingValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06018A95 RID: 101013 RVA: 0x00343A48 File Offset: 0x00341C48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "year" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "month" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "day" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "hour" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "minute" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "second" == name)
			{
				return new UInt16Value();
			}
			if (namespaceId == 0 && "dateTimeGrouping" == name)
			{
				return new EnumValue<DateTimeGroupingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A96 RID: 101014 RVA: 0x00343AF7 File Offset: 0x00341CF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DateGroupItem>(deep);
		}

		// Token: 0x06018A97 RID: 101015 RVA: 0x00343B00 File Offset: 0x00341D00
		// Note: this type is marked as 'beforefieldinit'.
		static DateGroupItem()
		{
			byte[] array = new byte[7];
			DateGroupItem.attributeNamespaceIds = array;
		}

		// Token: 0x0400A12D RID: 41261
		private const string tagName = "dateGroupItem";

		// Token: 0x0400A12E RID: 41262
		private const byte tagNsId = 22;

		// Token: 0x0400A12F RID: 41263
		internal const int ElementTypeIdConst = 11466;

		// Token: 0x0400A130 RID: 41264
		private static string[] attributeTagNames = new string[] { "year", "month", "day", "hour", "minute", "second", "dateTimeGrouping" };

		// Token: 0x0400A131 RID: 41265
		private static byte[] attributeNamespaceIds;
	}
}
