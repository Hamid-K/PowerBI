using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1D RID: 10781
	[ChildElementInfo(typeof(ByPosition))]
	[ChildElementInfo(typeof(FromPosition))]
	[ChildElementInfo(typeof(ToPosition))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonBehavior))]
	internal class AnimateScale : OpenXmlCompositeElement
	{
		// Token: 0x1700703C RID: 28732
		// (get) Token: 0x060159CF RID: 88527 RVA: 0x0032141B File Offset: 0x0031F61B
		public override string LocalName
		{
			get
			{
				return "animScale";
			}
		}

		// Token: 0x1700703D RID: 28733
		// (get) Token: 0x060159D0 RID: 88528 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700703E RID: 28734
		// (get) Token: 0x060159D1 RID: 88529 RVA: 0x00321422 File Offset: 0x0031F622
		internal override int ElementTypeId
		{
			get
			{
				return 12207;
			}
		}

		// Token: 0x060159D2 RID: 88530 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700703F RID: 28735
		// (get) Token: 0x060159D3 RID: 88531 RVA: 0x00321429 File Offset: 0x0031F629
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateScale.attributeTagNames;
			}
		}

		// Token: 0x17007040 RID: 28736
		// (get) Token: 0x060159D4 RID: 88532 RVA: 0x00321430 File Offset: 0x0031F630
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateScale.attributeNamespaceIds;
			}
		}

		// Token: 0x17007041 RID: 28737
		// (get) Token: 0x060159D5 RID: 88533 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060159D6 RID: 88534 RVA: 0x0029402B File Offset: 0x0029222B
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(49, "bounceEnd")]
		public Int32Value BounceEnd
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060159D7 RID: 88535 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnimateScale()
		{
		}

		// Token: 0x060159D8 RID: 88536 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnimateScale(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159D9 RID: 88537 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnimateScale(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159DA RID: 88538 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnimateScale(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060159DB RID: 88539 RVA: 0x00321438 File Offset: 0x0031F638
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			if (24 == namespaceId && "by" == name)
			{
				return new ByPosition();
			}
			if (24 == namespaceId && "from" == name)
			{
				return new FromPosition();
			}
			if (24 == namespaceId && "to" == name)
			{
				return new ToPosition();
			}
			return null;
		}

		// Token: 0x17007042 RID: 28738
		// (get) Token: 0x060159DC RID: 88540 RVA: 0x003214A6 File Offset: 0x0031F6A6
		internal override string[] ElementTagNames
		{
			get
			{
				return AnimateScale.eleTagNames;
			}
		}

		// Token: 0x17007043 RID: 28739
		// (get) Token: 0x060159DD RID: 88541 RVA: 0x003214AD File Offset: 0x0031F6AD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnimateScale.eleNamespaceIds;
			}
		}

		// Token: 0x17007044 RID: 28740
		// (get) Token: 0x060159DE RID: 88542 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007045 RID: 28741
		// (get) Token: 0x060159DF RID: 88543 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x060159E0 RID: 88544 RVA: 0x00320C30 File Offset: 0x0031EE30
		public CommonBehavior CommonBehavior
		{
			get
			{
				return base.GetElement<CommonBehavior>(0);
			}
			set
			{
				base.SetElement<CommonBehavior>(0, value);
			}
		}

		// Token: 0x17007046 RID: 28742
		// (get) Token: 0x060159E1 RID: 88545 RVA: 0x00321154 File Offset: 0x0031F354
		// (set) Token: 0x060159E2 RID: 88546 RVA: 0x0032115D File Offset: 0x0031F35D
		public ByPosition ByPosition
		{
			get
			{
				return base.GetElement<ByPosition>(1);
			}
			set
			{
				base.SetElement<ByPosition>(1, value);
			}
		}

		// Token: 0x17007047 RID: 28743
		// (get) Token: 0x060159E3 RID: 88547 RVA: 0x00321167 File Offset: 0x0031F367
		// (set) Token: 0x060159E4 RID: 88548 RVA: 0x00321170 File Offset: 0x0031F370
		public FromPosition FromPosition
		{
			get
			{
				return base.GetElement<FromPosition>(2);
			}
			set
			{
				base.SetElement<FromPosition>(2, value);
			}
		}

		// Token: 0x17007048 RID: 28744
		// (get) Token: 0x060159E5 RID: 88549 RVA: 0x0032117A File Offset: 0x0031F37A
		// (set) Token: 0x060159E6 RID: 88550 RVA: 0x00321183 File Offset: 0x0031F383
		public ToPosition ToPosition
		{
			get
			{
				return base.GetElement<ToPosition>(3);
			}
			set
			{
				base.SetElement<ToPosition>(3, value);
			}
		}

		// Token: 0x060159E7 RID: 88551 RVA: 0x003214B4 File Offset: 0x0031F6B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "bounceEnd" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060159E8 RID: 88552 RVA: 0x003214D6 File Offset: 0x0031F6D6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateScale>(deep);
		}

		// Token: 0x04009416 RID: 37910
		private const string tagName = "animScale";

		// Token: 0x04009417 RID: 37911
		private const byte tagNsId = 24;

		// Token: 0x04009418 RID: 37912
		internal const int ElementTypeIdConst = 12207;

		// Token: 0x04009419 RID: 37913
		private static string[] attributeTagNames = new string[] { "bounceEnd" };

		// Token: 0x0400941A RID: 37914
		private static byte[] attributeNamespaceIds = new byte[] { 49 };

		// Token: 0x0400941B RID: 37915
		private static readonly string[] eleTagNames = new string[] { "cBhvr", "by", "from", "to" };

		// Token: 0x0400941C RID: 37916
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24 };
	}
}
