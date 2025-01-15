using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CDD RID: 11485
	[ChildElementInfo(typeof(DateGroupItem))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Filter), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Filter))]
	internal class Filters : OpenXmlCompositeElement
	{
		// Token: 0x170085F3 RID: 34291
		// (get) Token: 0x06018A98 RID: 101016 RVA: 0x00341ECB File Offset: 0x003400CB
		public override string LocalName
		{
			get
			{
				return "filters";
			}
		}

		// Token: 0x170085F4 RID: 34292
		// (get) Token: 0x06018A99 RID: 101017 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085F5 RID: 34293
		// (get) Token: 0x06018A9A RID: 101018 RVA: 0x00343B5F File Offset: 0x00341D5F
		internal override int ElementTypeId
		{
			get
			{
				return 11467;
			}
		}

		// Token: 0x06018A9B RID: 101019 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170085F6 RID: 34294
		// (get) Token: 0x06018A9C RID: 101020 RVA: 0x00343B66 File Offset: 0x00341D66
		internal override string[] AttributeTagNames
		{
			get
			{
				return Filters.attributeTagNames;
			}
		}

		// Token: 0x170085F7 RID: 34295
		// (get) Token: 0x06018A9D RID: 101021 RVA: 0x00343B6D File Offset: 0x00341D6D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Filters.attributeNamespaceIds;
			}
		}

		// Token: 0x170085F8 RID: 34296
		// (get) Token: 0x06018A9E RID: 101022 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018A9F RID: 101023 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blank")]
		public BooleanValue Blank
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170085F9 RID: 34297
		// (get) Token: 0x06018AA0 RID: 101024 RVA: 0x00343B74 File Offset: 0x00341D74
		// (set) Token: 0x06018AA1 RID: 101025 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "calendarType")]
		public EnumValue<CalendarValues> CalendarType
		{
			get
			{
				return (EnumValue<CalendarValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06018AA2 RID: 101026 RVA: 0x00293ECF File Offset: 0x002920CF
		public Filters()
		{
		}

		// Token: 0x06018AA3 RID: 101027 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Filters(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018AA4 RID: 101028 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Filters(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018AA5 RID: 101029 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Filters(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018AA6 RID: 101030 RVA: 0x00343B84 File Offset: 0x00341D84
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "filter" == name)
			{
				return new Filter();
			}
			if (22 == namespaceId && "filter" == name)
			{
				return new Filter();
			}
			if (22 == namespaceId && "dateGroupItem" == name)
			{
				return new DateGroupItem();
			}
			return null;
		}

		// Token: 0x06018AA7 RID: 101031 RVA: 0x00343BDA File Offset: 0x00341DDA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blank" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "calendarType" == name)
			{
				return new EnumValue<CalendarValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018AA8 RID: 101032 RVA: 0x00343C10 File Offset: 0x00341E10
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Filters>(deep);
		}

		// Token: 0x06018AA9 RID: 101033 RVA: 0x00343C1C File Offset: 0x00341E1C
		// Note: this type is marked as 'beforefieldinit'.
		static Filters()
		{
			byte[] array = new byte[2];
			Filters.attributeNamespaceIds = array;
		}

		// Token: 0x0400A132 RID: 41266
		private const string tagName = "filters";

		// Token: 0x0400A133 RID: 41267
		private const byte tagNsId = 22;

		// Token: 0x0400A134 RID: 41268
		internal const int ElementTypeIdConst = 11467;

		// Token: 0x0400A135 RID: 41269
		private static string[] attributeTagNames = new string[] { "blank", "calendarType" };

		// Token: 0x0400A136 RID: 41270
		private static byte[] attributeNamespaceIds;
	}
}
