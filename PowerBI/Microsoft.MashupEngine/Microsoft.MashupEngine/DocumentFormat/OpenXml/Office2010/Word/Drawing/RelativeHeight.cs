using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024EA RID: 9450
	[ChildElementInfo(typeof(PercentageHeight), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class RelativeHeight : OpenXmlCompositeElement
	{
		// Token: 0x17005339 RID: 21305
		// (get) Token: 0x06011838 RID: 71736 RVA: 0x002EF416 File Offset: 0x002ED616
		public override string LocalName
		{
			get
			{
				return "sizeRelV";
			}
		}

		// Token: 0x1700533A RID: 21306
		// (get) Token: 0x06011839 RID: 71737 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x1700533B RID: 21307
		// (get) Token: 0x0601183A RID: 71738 RVA: 0x002EF41D File Offset: 0x002ED61D
		internal override int ElementTypeId
		{
			get
			{
				return 12825;
			}
		}

		// Token: 0x0601183B RID: 71739 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700533C RID: 21308
		// (get) Token: 0x0601183C RID: 71740 RVA: 0x002EF424 File Offset: 0x002ED624
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelativeHeight.attributeTagNames;
			}
		}

		// Token: 0x1700533D RID: 21309
		// (get) Token: 0x0601183D RID: 71741 RVA: 0x002EF42B File Offset: 0x002ED62B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelativeHeight.attributeNamespaceIds;
			}
		}

		// Token: 0x1700533E RID: 21310
		// (get) Token: 0x0601183E RID: 71742 RVA: 0x002EF432 File Offset: 0x002ED632
		// (set) Token: 0x0601183F RID: 71743 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "relativeFrom")]
		public EnumValue<SizeRelativeVerticallyValues> RelativeFrom
		{
			get
			{
				return (EnumValue<SizeRelativeVerticallyValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011840 RID: 71744 RVA: 0x00293ECF File Offset: 0x002920CF
		public RelativeHeight()
		{
		}

		// Token: 0x06011841 RID: 71745 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RelativeHeight(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011842 RID: 71746 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RelativeHeight(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011843 RID: 71747 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RelativeHeight(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011844 RID: 71748 RVA: 0x002EF441 File Offset: 0x002ED641
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (51 == namespaceId && "pctHeight" == name)
			{
				return new PercentageHeight();
			}
			return null;
		}

		// Token: 0x1700533F RID: 21311
		// (get) Token: 0x06011845 RID: 71749 RVA: 0x002EF45C File Offset: 0x002ED65C
		internal override string[] ElementTagNames
		{
			get
			{
				return RelativeHeight.eleTagNames;
			}
		}

		// Token: 0x17005340 RID: 21312
		// (get) Token: 0x06011846 RID: 71750 RVA: 0x002EF463 File Offset: 0x002ED663
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RelativeHeight.eleNamespaceIds;
			}
		}

		// Token: 0x17005341 RID: 21313
		// (get) Token: 0x06011847 RID: 71751 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005342 RID: 21314
		// (get) Token: 0x06011848 RID: 71752 RVA: 0x002EF46A File Offset: 0x002ED66A
		// (set) Token: 0x06011849 RID: 71753 RVA: 0x002EF473 File Offset: 0x002ED673
		public PercentageHeight PercentageHeight
		{
			get
			{
				return base.GetElement<PercentageHeight>(0);
			}
			set
			{
				base.SetElement<PercentageHeight>(0, value);
			}
		}

		// Token: 0x0601184A RID: 71754 RVA: 0x002EF47D File Offset: 0x002ED67D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "relativeFrom" == name)
			{
				return new EnumValue<SizeRelativeVerticallyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601184B RID: 71755 RVA: 0x002EF49D File Offset: 0x002ED69D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelativeHeight>(deep);
		}

		// Token: 0x0601184C RID: 71756 RVA: 0x002EF4A8 File Offset: 0x002ED6A8
		// Note: this type is marked as 'beforefieldinit'.
		static RelativeHeight()
		{
			byte[] array = new byte[1];
			RelativeHeight.attributeNamespaceIds = array;
			RelativeHeight.eleTagNames = new string[] { "pctHeight" };
			RelativeHeight.eleNamespaceIds = new byte[] { 51 };
		}

		// Token: 0x04007B0F RID: 31503
		private const string tagName = "sizeRelV";

		// Token: 0x04007B10 RID: 31504
		private const byte tagNsId = 51;

		// Token: 0x04007B11 RID: 31505
		internal const int ElementTypeIdConst = 12825;

		// Token: 0x04007B12 RID: 31506
		private static string[] attributeTagNames = new string[] { "relativeFrom" };

		// Token: 0x04007B13 RID: 31507
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B14 RID: 31508
		private static readonly string[] eleTagNames;

		// Token: 0x04007B15 RID: 31509
		private static readonly byte[] eleNamespaceIds;
	}
}
