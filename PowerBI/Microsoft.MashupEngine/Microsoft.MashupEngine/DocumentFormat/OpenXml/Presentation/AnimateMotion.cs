using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1B RID: 10779
	[ChildElementInfo(typeof(CommonBehavior))]
	[ChildElementInfo(typeof(FromPosition))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ByPosition))]
	[ChildElementInfo(typeof(ToPosition))]
	[ChildElementInfo(typeof(RotationCenter))]
	internal class AnimateMotion : OpenXmlCompositeElement
	{
		// Token: 0x1700701C RID: 28700
		// (get) Token: 0x0601598D RID: 88461 RVA: 0x00321083 File Offset: 0x0031F283
		public override string LocalName
		{
			get
			{
				return "animMotion";
			}
		}

		// Token: 0x1700701D RID: 28701
		// (get) Token: 0x0601598E RID: 88462 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700701E RID: 28702
		// (get) Token: 0x0601598F RID: 88463 RVA: 0x0032108A File Offset: 0x0031F28A
		internal override int ElementTypeId
		{
			get
			{
				return 12205;
			}
		}

		// Token: 0x06015990 RID: 88464 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700701F RID: 28703
		// (get) Token: 0x06015991 RID: 88465 RVA: 0x00321091 File Offset: 0x0031F291
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateMotion.attributeTagNames;
			}
		}

		// Token: 0x17007020 RID: 28704
		// (get) Token: 0x06015992 RID: 88466 RVA: 0x00321098 File Offset: 0x0031F298
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateMotion.attributeNamespaceIds;
			}
		}

		// Token: 0x17007021 RID: 28705
		// (get) Token: 0x06015993 RID: 88467 RVA: 0x0032109F File Offset: 0x0031F29F
		// (set) Token: 0x06015994 RID: 88468 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "origin")]
		public EnumValue<AnimateMotionBehaviorOriginValues> Origin
		{
			get
			{
				return (EnumValue<AnimateMotionBehaviorOriginValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007022 RID: 28706
		// (get) Token: 0x06015995 RID: 88469 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015996 RID: 88470 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "path")]
		public StringValue Path
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007023 RID: 28707
		// (get) Token: 0x06015997 RID: 88471 RVA: 0x003210AE File Offset: 0x0031F2AE
		// (set) Token: 0x06015998 RID: 88472 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "pathEditMode")]
		public EnumValue<AnimateMotionPathEditModeValues> PathEditMode
		{
			get
			{
				return (EnumValue<AnimateMotionPathEditModeValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007024 RID: 28708
		// (get) Token: 0x06015999 RID: 88473 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601599A RID: 88474 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "rAng")]
		public Int32Value RelativeAngle
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007025 RID: 28709
		// (get) Token: 0x0601599B RID: 88475 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601599C RID: 88476 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ptsTypes")]
		public StringValue PointTypes
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007026 RID: 28710
		// (get) Token: 0x0601599D RID: 88477 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x0601599E RID: 88478 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(49, "bounceEnd")]
		public Int32Value BounceEnd
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601599F RID: 88479 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnimateMotion()
		{
		}

		// Token: 0x060159A0 RID: 88480 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnimateMotion(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159A1 RID: 88481 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnimateMotion(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159A2 RID: 88482 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnimateMotion(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060159A3 RID: 88483 RVA: 0x003210C0 File Offset: 0x0031F2C0
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
			if (24 == namespaceId && "rCtr" == name)
			{
				return new RotationCenter();
			}
			return null;
		}

		// Token: 0x17007027 RID: 28711
		// (get) Token: 0x060159A4 RID: 88484 RVA: 0x00321146 File Offset: 0x0031F346
		internal override string[] ElementTagNames
		{
			get
			{
				return AnimateMotion.eleTagNames;
			}
		}

		// Token: 0x17007028 RID: 28712
		// (get) Token: 0x060159A5 RID: 88485 RVA: 0x0032114D File Offset: 0x0031F34D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnimateMotion.eleNamespaceIds;
			}
		}

		// Token: 0x17007029 RID: 28713
		// (get) Token: 0x060159A6 RID: 88486 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700702A RID: 28714
		// (get) Token: 0x060159A7 RID: 88487 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x060159A8 RID: 88488 RVA: 0x00320C30 File Offset: 0x0031EE30
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

		// Token: 0x1700702B RID: 28715
		// (get) Token: 0x060159A9 RID: 88489 RVA: 0x00321154 File Offset: 0x0031F354
		// (set) Token: 0x060159AA RID: 88490 RVA: 0x0032115D File Offset: 0x0031F35D
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

		// Token: 0x1700702C RID: 28716
		// (get) Token: 0x060159AB RID: 88491 RVA: 0x00321167 File Offset: 0x0031F367
		// (set) Token: 0x060159AC RID: 88492 RVA: 0x00321170 File Offset: 0x0031F370
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

		// Token: 0x1700702D RID: 28717
		// (get) Token: 0x060159AD RID: 88493 RVA: 0x0032117A File Offset: 0x0031F37A
		// (set) Token: 0x060159AE RID: 88494 RVA: 0x00321183 File Offset: 0x0031F383
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

		// Token: 0x1700702E RID: 28718
		// (get) Token: 0x060159AF RID: 88495 RVA: 0x0032118D File Offset: 0x0031F38D
		// (set) Token: 0x060159B0 RID: 88496 RVA: 0x00321196 File Offset: 0x0031F396
		public RotationCenter RotationCenter
		{
			get
			{
				return base.GetElement<RotationCenter>(4);
			}
			set
			{
				base.SetElement<RotationCenter>(4, value);
			}
		}

		// Token: 0x060159B1 RID: 88497 RVA: 0x003211A0 File Offset: 0x0031F3A0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "origin" == name)
			{
				return new EnumValue<AnimateMotionBehaviorOriginValues>();
			}
			if (namespaceId == 0 && "path" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pathEditMode" == name)
			{
				return new EnumValue<AnimateMotionPathEditModeValues>();
			}
			if (namespaceId == 0 && "rAng" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ptsTypes" == name)
			{
				return new StringValue();
			}
			if (49 == namespaceId && "bounceEnd" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060159B2 RID: 88498 RVA: 0x0032123B File Offset: 0x0031F43B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateMotion>(deep);
		}

		// Token: 0x04009408 RID: 37896
		private const string tagName = "animMotion";

		// Token: 0x04009409 RID: 37897
		private const byte tagNsId = 24;

		// Token: 0x0400940A RID: 37898
		internal const int ElementTypeIdConst = 12205;

		// Token: 0x0400940B RID: 37899
		private static string[] attributeTagNames = new string[] { "origin", "path", "pathEditMode", "rAng", "ptsTypes", "bounceEnd" };

		// Token: 0x0400940C RID: 37900
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 0, 49 };

		// Token: 0x0400940D RID: 37901
		private static readonly string[] eleTagNames = new string[] { "cBhvr", "by", "from", "to", "rCtr" };

		// Token: 0x0400940E RID: 37902
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
	}
}
