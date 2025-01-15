using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003008 RID: 12296
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LanguageId))]
	[ChildElementInfo(typeof(Calendar))]
	[ChildElementInfo(typeof(DateFormat))]
	[ChildElementInfo(typeof(SdtDateMappingType))]
	internal class SdtContentDate : OpenXmlCompositeElement
	{
		// Token: 0x17009645 RID: 38469
		// (get) Token: 0x0601AD54 RID: 109908 RVA: 0x00318150 File Offset: 0x00316350
		public override string LocalName
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x17009646 RID: 38470
		// (get) Token: 0x0601AD55 RID: 109909 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009647 RID: 38471
		// (get) Token: 0x0601AD56 RID: 109910 RVA: 0x003683C4 File Offset: 0x003665C4
		internal override int ElementTypeId
		{
			get
			{
				return 12149;
			}
		}

		// Token: 0x0601AD57 RID: 109911 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009648 RID: 38472
		// (get) Token: 0x0601AD58 RID: 109912 RVA: 0x003683CB File Offset: 0x003665CB
		internal override string[] AttributeTagNames
		{
			get
			{
				return SdtContentDate.attributeTagNames;
			}
		}

		// Token: 0x17009649 RID: 38473
		// (get) Token: 0x0601AD59 RID: 109913 RVA: 0x003683D2 File Offset: 0x003665D2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SdtContentDate.attributeNamespaceIds;
			}
		}

		// Token: 0x1700964A RID: 38474
		// (get) Token: 0x0601AD5A RID: 109914 RVA: 0x0032F5D1 File Offset: 0x0032D7D1
		// (set) Token: 0x0601AD5B RID: 109915 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "fullDate")]
		public DateTimeValue FullDate
		{
			get
			{
				return (DateTimeValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AD5C RID: 109916 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtContentDate()
		{
		}

		// Token: 0x0601AD5D RID: 109917 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtContentDate(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD5E RID: 109918 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtContentDate(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD5F RID: 109919 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtContentDate(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD60 RID: 109920 RVA: 0x003683DC File Offset: 0x003665DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "dateFormat" == name)
			{
				return new DateFormat();
			}
			if (23 == namespaceId && "lid" == name)
			{
				return new LanguageId();
			}
			if (23 == namespaceId && "storeMappedDataAs" == name)
			{
				return new SdtDateMappingType();
			}
			if (23 == namespaceId && "calendar" == name)
			{
				return new Calendar();
			}
			return null;
		}

		// Token: 0x1700964B RID: 38475
		// (get) Token: 0x0601AD61 RID: 109921 RVA: 0x0036844A File Offset: 0x0036664A
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtContentDate.eleTagNames;
			}
		}

		// Token: 0x1700964C RID: 38476
		// (get) Token: 0x0601AD62 RID: 109922 RVA: 0x00368451 File Offset: 0x00366651
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtContentDate.eleNamespaceIds;
			}
		}

		// Token: 0x1700964D RID: 38477
		// (get) Token: 0x0601AD63 RID: 109923 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700964E RID: 38478
		// (get) Token: 0x0601AD64 RID: 109924 RVA: 0x00368458 File Offset: 0x00366658
		// (set) Token: 0x0601AD65 RID: 109925 RVA: 0x00368461 File Offset: 0x00366661
		public DateFormat DateFormat
		{
			get
			{
				return base.GetElement<DateFormat>(0);
			}
			set
			{
				base.SetElement<DateFormat>(0, value);
			}
		}

		// Token: 0x1700964F RID: 38479
		// (get) Token: 0x0601AD66 RID: 109926 RVA: 0x0036846B File Offset: 0x0036666B
		// (set) Token: 0x0601AD67 RID: 109927 RVA: 0x00368474 File Offset: 0x00366674
		public LanguageId LanguageId
		{
			get
			{
				return base.GetElement<LanguageId>(1);
			}
			set
			{
				base.SetElement<LanguageId>(1, value);
			}
		}

		// Token: 0x17009650 RID: 38480
		// (get) Token: 0x0601AD68 RID: 109928 RVA: 0x0036847E File Offset: 0x0036667E
		// (set) Token: 0x0601AD69 RID: 109929 RVA: 0x00368487 File Offset: 0x00366687
		public SdtDateMappingType SdtDateMappingType
		{
			get
			{
				return base.GetElement<SdtDateMappingType>(2);
			}
			set
			{
				base.SetElement<SdtDateMappingType>(2, value);
			}
		}

		// Token: 0x17009651 RID: 38481
		// (get) Token: 0x0601AD6A RID: 109930 RVA: 0x00368491 File Offset: 0x00366691
		// (set) Token: 0x0601AD6B RID: 109931 RVA: 0x0036849A File Offset: 0x0036669A
		public Calendar Calendar
		{
			get
			{
				return base.GetElement<Calendar>(3);
			}
			set
			{
				base.SetElement<Calendar>(3, value);
			}
		}

		// Token: 0x0601AD6C RID: 109932 RVA: 0x003684A4 File Offset: 0x003666A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fullDate" == name)
			{
				return new DateTimeValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AD6D RID: 109933 RVA: 0x003684C6 File Offset: 0x003666C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentDate>(deep);
		}

		// Token: 0x0400AE75 RID: 44661
		private const string tagName = "date";

		// Token: 0x0400AE76 RID: 44662
		private const byte tagNsId = 23;

		// Token: 0x0400AE77 RID: 44663
		internal const int ElementTypeIdConst = 12149;

		// Token: 0x0400AE78 RID: 44664
		private static string[] attributeTagNames = new string[] { "fullDate" };

		// Token: 0x0400AE79 RID: 44665
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400AE7A RID: 44666
		private static readonly string[] eleTagNames = new string[] { "dateFormat", "lid", "storeMappedDataAs", "calendar" };

		// Token: 0x0400AE7B RID: 44667
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
