using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024E9 RID: 9449
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PercentageWidth), FileFormatVersions.Office2010)]
	internal class RelativeWidth : OpenXmlCompositeElement
	{
		// Token: 0x1700532F RID: 21295
		// (get) Token: 0x06011823 RID: 71715 RVA: 0x002EF330 File Offset: 0x002ED530
		public override string LocalName
		{
			get
			{
				return "sizeRelH";
			}
		}

		// Token: 0x17005330 RID: 21296
		// (get) Token: 0x06011824 RID: 71716 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x17005331 RID: 21297
		// (get) Token: 0x06011825 RID: 71717 RVA: 0x002EF337 File Offset: 0x002ED537
		internal override int ElementTypeId
		{
			get
			{
				return 12824;
			}
		}

		// Token: 0x06011826 RID: 71718 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005332 RID: 21298
		// (get) Token: 0x06011827 RID: 71719 RVA: 0x002EF33E File Offset: 0x002ED53E
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelativeWidth.attributeTagNames;
			}
		}

		// Token: 0x17005333 RID: 21299
		// (get) Token: 0x06011828 RID: 71720 RVA: 0x002EF345 File Offset: 0x002ED545
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelativeWidth.attributeNamespaceIds;
			}
		}

		// Token: 0x17005334 RID: 21300
		// (get) Token: 0x06011829 RID: 71721 RVA: 0x002EF34C File Offset: 0x002ED54C
		// (set) Token: 0x0601182A RID: 71722 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "relativeFrom")]
		public EnumValue<SizeRelativeHorizontallyValues> ObjectId
		{
			get
			{
				return (EnumValue<SizeRelativeHorizontallyValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601182B RID: 71723 RVA: 0x00293ECF File Offset: 0x002920CF
		public RelativeWidth()
		{
		}

		// Token: 0x0601182C RID: 71724 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RelativeWidth(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601182D RID: 71725 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RelativeWidth(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601182E RID: 71726 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RelativeWidth(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601182F RID: 71727 RVA: 0x002EF35B File Offset: 0x002ED55B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (51 == namespaceId && "pctWidth" == name)
			{
				return new PercentageWidth();
			}
			return null;
		}

		// Token: 0x17005335 RID: 21301
		// (get) Token: 0x06011830 RID: 71728 RVA: 0x002EF376 File Offset: 0x002ED576
		internal override string[] ElementTagNames
		{
			get
			{
				return RelativeWidth.eleTagNames;
			}
		}

		// Token: 0x17005336 RID: 21302
		// (get) Token: 0x06011831 RID: 71729 RVA: 0x002EF37D File Offset: 0x002ED57D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RelativeWidth.eleNamespaceIds;
			}
		}

		// Token: 0x17005337 RID: 21303
		// (get) Token: 0x06011832 RID: 71730 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005338 RID: 21304
		// (get) Token: 0x06011833 RID: 71731 RVA: 0x002EF384 File Offset: 0x002ED584
		// (set) Token: 0x06011834 RID: 71732 RVA: 0x002EF38D File Offset: 0x002ED58D
		public PercentageWidth PercentageWidth
		{
			get
			{
				return base.GetElement<PercentageWidth>(0);
			}
			set
			{
				base.SetElement<PercentageWidth>(0, value);
			}
		}

		// Token: 0x06011835 RID: 71733 RVA: 0x002EF397 File Offset: 0x002ED597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "relativeFrom" == name)
			{
				return new EnumValue<SizeRelativeHorizontallyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011836 RID: 71734 RVA: 0x002EF3B7 File Offset: 0x002ED5B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelativeWidth>(deep);
		}

		// Token: 0x06011837 RID: 71735 RVA: 0x002EF3C0 File Offset: 0x002ED5C0
		// Note: this type is marked as 'beforefieldinit'.
		static RelativeWidth()
		{
			byte[] array = new byte[1];
			RelativeWidth.attributeNamespaceIds = array;
			RelativeWidth.eleTagNames = new string[] { "pctWidth" };
			RelativeWidth.eleNamespaceIds = new byte[] { 51 };
		}

		// Token: 0x04007B08 RID: 31496
		private const string tagName = "sizeRelH";

		// Token: 0x04007B09 RID: 31497
		private const byte tagNsId = 51;

		// Token: 0x04007B0A RID: 31498
		internal const int ElementTypeIdConst = 12824;

		// Token: 0x04007B0B RID: 31499
		private static string[] attributeTagNames = new string[] { "relativeFrom" };

		// Token: 0x04007B0C RID: 31500
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B0D RID: 31501
		private static readonly string[] eleTagNames;

		// Token: 0x04007B0E RID: 31502
		private static readonly byte[] eleNamespaceIds;
	}
}
