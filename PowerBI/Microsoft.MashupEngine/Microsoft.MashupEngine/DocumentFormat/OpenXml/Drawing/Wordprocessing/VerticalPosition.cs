using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word.Drawing;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AA RID: 10410
	[ChildElementInfo(typeof(PercentagePositionVerticalOffset), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(VerticalAlignment))]
	[ChildElementInfo(typeof(PositionOffset))]
	internal class VerticalPosition : OpenXmlCompositeElement
	{
		// Token: 0x17006891 RID: 26769
		// (get) Token: 0x06014821 RID: 84001 RVA: 0x003142BA File Offset: 0x003124BA
		public override string LocalName
		{
			get
			{
				return "positionV";
			}
		}

		// Token: 0x17006892 RID: 26770
		// (get) Token: 0x06014822 RID: 84002 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006893 RID: 26771
		// (get) Token: 0x06014823 RID: 84003 RVA: 0x003142C1 File Offset: 0x003124C1
		internal override int ElementTypeId
		{
			get
			{
				return 10707;
			}
		}

		// Token: 0x06014824 RID: 84004 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006894 RID: 26772
		// (get) Token: 0x06014825 RID: 84005 RVA: 0x003142C8 File Offset: 0x003124C8
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalPosition.attributeTagNames;
			}
		}

		// Token: 0x17006895 RID: 26773
		// (get) Token: 0x06014826 RID: 84006 RVA: 0x003142CF File Offset: 0x003124CF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x17006896 RID: 26774
		// (get) Token: 0x06014827 RID: 84007 RVA: 0x003142D6 File Offset: 0x003124D6
		// (set) Token: 0x06014828 RID: 84008 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "relativeFrom")]
		public EnumValue<VerticalRelativePositionValues> RelativeFrom
		{
			get
			{
				return (EnumValue<VerticalRelativePositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014829 RID: 84009 RVA: 0x00293ECF File Offset: 0x002920CF
		public VerticalPosition()
		{
		}

		// Token: 0x0601482A RID: 84010 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VerticalPosition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601482B RID: 84011 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VerticalPosition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601482C RID: 84012 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VerticalPosition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601482D RID: 84013 RVA: 0x003142E8 File Offset: 0x003124E8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "align" == name)
			{
				return new VerticalAlignment();
			}
			if (16 == namespaceId && "posOffset" == name)
			{
				return new PositionOffset();
			}
			if (51 == namespaceId && "pctPosVOffset" == name)
			{
				return new PercentagePositionVerticalOffset();
			}
			return null;
		}

		// Token: 0x17006897 RID: 26775
		// (get) Token: 0x0601482E RID: 84014 RVA: 0x0031433E File Offset: 0x0031253E
		internal override string[] ElementTagNames
		{
			get
			{
				return VerticalPosition.eleTagNames;
			}
		}

		// Token: 0x17006898 RID: 26776
		// (get) Token: 0x0601482F RID: 84015 RVA: 0x00314345 File Offset: 0x00312545
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return VerticalPosition.eleNamespaceIds;
			}
		}

		// Token: 0x17006899 RID: 26777
		// (get) Token: 0x06014830 RID: 84016 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700689A RID: 26778
		// (get) Token: 0x06014831 RID: 84017 RVA: 0x0031434C File Offset: 0x0031254C
		// (set) Token: 0x06014832 RID: 84018 RVA: 0x00314355 File Offset: 0x00312555
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return base.GetElement<VerticalAlignment>(0);
			}
			set
			{
				base.SetElement<VerticalAlignment>(0, value);
			}
		}

		// Token: 0x1700689B RID: 26779
		// (get) Token: 0x06014833 RID: 84019 RVA: 0x003141FF File Offset: 0x003123FF
		// (set) Token: 0x06014834 RID: 84020 RVA: 0x00314208 File Offset: 0x00312408
		public PositionOffset PositionOffset
		{
			get
			{
				return base.GetElement<PositionOffset>(1);
			}
			set
			{
				base.SetElement<PositionOffset>(1, value);
			}
		}

		// Token: 0x1700689C RID: 26780
		// (get) Token: 0x06014835 RID: 84021 RVA: 0x0031435F File Offset: 0x0031255F
		// (set) Token: 0x06014836 RID: 84022 RVA: 0x00314368 File Offset: 0x00312568
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public PercentagePositionVerticalOffset PercentagePositionVerticalOffset
		{
			get
			{
				return base.GetElement<PercentagePositionVerticalOffset>(2);
			}
			set
			{
				base.SetElement<PercentagePositionVerticalOffset>(2, value);
			}
		}

		// Token: 0x06014837 RID: 84023 RVA: 0x00314372 File Offset: 0x00312572
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "relativeFrom" == name)
			{
				return new EnumValue<VerticalRelativePositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014838 RID: 84024 RVA: 0x00314392 File Offset: 0x00312592
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalPosition>(deep);
		}

		// Token: 0x06014839 RID: 84025 RVA: 0x0031439C File Offset: 0x0031259C
		// Note: this type is marked as 'beforefieldinit'.
		static VerticalPosition()
		{
			byte[] array = new byte[1];
			VerticalPosition.attributeNamespaceIds = array;
			VerticalPosition.eleTagNames = new string[] { "align", "posOffset", "pctPosVOffset" };
			VerticalPosition.eleNamespaceIds = new byte[] { 16, 16, 51 };
		}

		// Token: 0x04008E68 RID: 36456
		private const string tagName = "positionV";

		// Token: 0x04008E69 RID: 36457
		private const byte tagNsId = 16;

		// Token: 0x04008E6A RID: 36458
		internal const int ElementTypeIdConst = 10707;

		// Token: 0x04008E6B RID: 36459
		private static string[] attributeTagNames = new string[] { "relativeFrom" };

		// Token: 0x04008E6C RID: 36460
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E6D RID: 36461
		private static readonly string[] eleTagNames;

		// Token: 0x04008E6E RID: 36462
		private static readonly byte[] eleNamespaceIds;
	}
}
