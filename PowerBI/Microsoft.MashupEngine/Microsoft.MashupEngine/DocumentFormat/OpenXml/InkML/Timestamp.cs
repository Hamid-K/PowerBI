using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200309B RID: 12443
	[GeneratedCode("DomGen", "2.0")]
	internal class Timestamp : OpenXmlLeafElement
	{
		// Token: 0x170097FD RID: 38909
		// (get) Token: 0x0601B140 RID: 110912 RVA: 0x0036B777 File Offset: 0x00369977
		public override string LocalName
		{
			get
			{
				return "timestamp";
			}
		}

		// Token: 0x170097FE RID: 38910
		// (get) Token: 0x0601B141 RID: 110913 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097FF RID: 38911
		// (get) Token: 0x0601B142 RID: 110914 RVA: 0x0036B77E File Offset: 0x0036997E
		internal override int ElementTypeId
		{
			get
			{
				return 12664;
			}
		}

		// Token: 0x0601B143 RID: 110915 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009800 RID: 38912
		// (get) Token: 0x0601B144 RID: 110916 RVA: 0x0036B785 File Offset: 0x00369985
		internal override string[] AttributeTagNames
		{
			get
			{
				return Timestamp.attributeTagNames;
			}
		}

		// Token: 0x17009801 RID: 38913
		// (get) Token: 0x0601B145 RID: 110917 RVA: 0x0036B78C File Offset: 0x0036998C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Timestamp.attributeNamespaceIds;
			}
		}

		// Token: 0x17009802 RID: 38914
		// (get) Token: 0x0601B146 RID: 110918 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B147 RID: 110919 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
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

		// Token: 0x17009803 RID: 38915
		// (get) Token: 0x0601B148 RID: 110920 RVA: 0x0036A078 File Offset: 0x00368278
		// (set) Token: 0x0601B149 RID: 110921 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "time")]
		public DecimalValue Time
		{
			get
			{
				return (DecimalValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009804 RID: 38916
		// (get) Token: 0x0601B14A RID: 110922 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B14B RID: 110923 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "timestampRef")]
		public StringValue TimestampRef
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

		// Token: 0x17009805 RID: 38917
		// (get) Token: 0x0601B14C RID: 110924 RVA: 0x00335DB2 File Offset: 0x00333FB2
		// (set) Token: 0x0601B14D RID: 110925 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "timeString")]
		public DateTimeValue TimeString
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

		// Token: 0x17009806 RID: 38918
		// (get) Token: 0x0601B14E RID: 110926 RVA: 0x0036A9B6 File Offset: 0x00368BB6
		// (set) Token: 0x0601B14F RID: 110927 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "timeOffset")]
		public DecimalValue TimeOffset
		{
			get
			{
				return (DecimalValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0601B151 RID: 110929 RVA: 0x0036B794 File Offset: 0x00369994
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "time" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "timestampRef" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "timeString" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "timeOffset" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B152 RID: 110930 RVA: 0x0036B818 File Offset: 0x00369A18
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Timestamp>(deep);
		}

		// Token: 0x0601B153 RID: 110931 RVA: 0x0036B824 File Offset: 0x00369A24
		// Note: this type is marked as 'beforefieldinit'.
		static Timestamp()
		{
			byte[] array = new byte[5];
			array[0] = 1;
			Timestamp.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2D8 RID: 45784
		private const string tagName = "timestamp";

		// Token: 0x0400B2D9 RID: 45785
		private const byte tagNsId = 43;

		// Token: 0x0400B2DA RID: 45786
		internal const int ElementTypeIdConst = 12664;

		// Token: 0x0400B2DB RID: 45787
		private static string[] attributeTagNames = new string[] { "id", "time", "timestampRef", "timeString", "timeOffset" };

		// Token: 0x0400B2DC RID: 45788
		private static byte[] attributeNamespaceIds;
	}
}
