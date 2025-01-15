using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word.Drawing;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A9 RID: 10409
	[ChildElementInfo(typeof(HorizontalAlignment))]
	[ChildElementInfo(typeof(PositionOffset))]
	[ChildElementInfo(typeof(PercentagePositionHeightOffset), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class HorizontalPosition : OpenXmlCompositeElement
	{
		// Token: 0x17006885 RID: 26757
		// (get) Token: 0x06014808 RID: 83976 RVA: 0x0031415B File Offset: 0x0031235B
		public override string LocalName
		{
			get
			{
				return "positionH";
			}
		}

		// Token: 0x17006886 RID: 26758
		// (get) Token: 0x06014809 RID: 83977 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006887 RID: 26759
		// (get) Token: 0x0601480A RID: 83978 RVA: 0x00314162 File Offset: 0x00312362
		internal override int ElementTypeId
		{
			get
			{
				return 10706;
			}
		}

		// Token: 0x0601480B RID: 83979 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006888 RID: 26760
		// (get) Token: 0x0601480C RID: 83980 RVA: 0x00314169 File Offset: 0x00312369
		internal override string[] AttributeTagNames
		{
			get
			{
				return HorizontalPosition.attributeTagNames;
			}
		}

		// Token: 0x17006889 RID: 26761
		// (get) Token: 0x0601480D RID: 83981 RVA: 0x00314170 File Offset: 0x00312370
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HorizontalPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x1700688A RID: 26762
		// (get) Token: 0x0601480E RID: 83982 RVA: 0x00314177 File Offset: 0x00312377
		// (set) Token: 0x0601480F RID: 83983 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "relativeFrom")]
		public EnumValue<HorizontalRelativePositionValues> RelativeFrom
		{
			get
			{
				return (EnumValue<HorizontalRelativePositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014810 RID: 83984 RVA: 0x00293ECF File Offset: 0x002920CF
		public HorizontalPosition()
		{
		}

		// Token: 0x06014811 RID: 83985 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HorizontalPosition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014812 RID: 83986 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HorizontalPosition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014813 RID: 83987 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HorizontalPosition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014814 RID: 83988 RVA: 0x00314188 File Offset: 0x00312388
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "align" == name)
			{
				return new HorizontalAlignment();
			}
			if (16 == namespaceId && "posOffset" == name)
			{
				return new PositionOffset();
			}
			if (51 == namespaceId && "pctPosHOffset" == name)
			{
				return new PercentagePositionHeightOffset();
			}
			return null;
		}

		// Token: 0x1700688B RID: 26763
		// (get) Token: 0x06014815 RID: 83989 RVA: 0x003141DE File Offset: 0x003123DE
		internal override string[] ElementTagNames
		{
			get
			{
				return HorizontalPosition.eleTagNames;
			}
		}

		// Token: 0x1700688C RID: 26764
		// (get) Token: 0x06014816 RID: 83990 RVA: 0x003141E5 File Offset: 0x003123E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HorizontalPosition.eleNamespaceIds;
			}
		}

		// Token: 0x1700688D RID: 26765
		// (get) Token: 0x06014817 RID: 83991 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700688E RID: 26766
		// (get) Token: 0x06014818 RID: 83992 RVA: 0x003141EC File Offset: 0x003123EC
		// (set) Token: 0x06014819 RID: 83993 RVA: 0x003141F5 File Offset: 0x003123F5
		public HorizontalAlignment HorizontalAlignment
		{
			get
			{
				return base.GetElement<HorizontalAlignment>(0);
			}
			set
			{
				base.SetElement<HorizontalAlignment>(0, value);
			}
		}

		// Token: 0x1700688F RID: 26767
		// (get) Token: 0x0601481A RID: 83994 RVA: 0x003141FF File Offset: 0x003123FF
		// (set) Token: 0x0601481B RID: 83995 RVA: 0x00314208 File Offset: 0x00312408
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

		// Token: 0x17006890 RID: 26768
		// (get) Token: 0x0601481C RID: 83996 RVA: 0x00314212 File Offset: 0x00312412
		// (set) Token: 0x0601481D RID: 83997 RVA: 0x0031421B File Offset: 0x0031241B
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public PercentagePositionHeightOffset PercentagePositionHeightOffset
		{
			get
			{
				return base.GetElement<PercentagePositionHeightOffset>(2);
			}
			set
			{
				base.SetElement<PercentagePositionHeightOffset>(2, value);
			}
		}

		// Token: 0x0601481E RID: 83998 RVA: 0x00314225 File Offset: 0x00312425
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "relativeFrom" == name)
			{
				return new EnumValue<HorizontalRelativePositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601481F RID: 83999 RVA: 0x00314245 File Offset: 0x00312445
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HorizontalPosition>(deep);
		}

		// Token: 0x06014820 RID: 84000 RVA: 0x00314250 File Offset: 0x00312450
		// Note: this type is marked as 'beforefieldinit'.
		static HorizontalPosition()
		{
			byte[] array = new byte[1];
			HorizontalPosition.attributeNamespaceIds = array;
			HorizontalPosition.eleTagNames = new string[] { "align", "posOffset", "pctPosHOffset" };
			HorizontalPosition.eleNamespaceIds = new byte[] { 16, 16, 51 };
		}

		// Token: 0x04008E61 RID: 36449
		private const string tagName = "positionH";

		// Token: 0x04008E62 RID: 36450
		private const byte tagNsId = 16;

		// Token: 0x04008E63 RID: 36451
		internal const int ElementTypeIdConst = 10706;

		// Token: 0x04008E64 RID: 36452
		private static string[] attributeTagNames = new string[] { "relativeFrom" };

		// Token: 0x04008E65 RID: 36453
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E66 RID: 36454
		private static readonly string[] eleTagNames;

		// Token: 0x04008E67 RID: 36455
		private static readonly byte[] eleNamespaceIds;
	}
}
