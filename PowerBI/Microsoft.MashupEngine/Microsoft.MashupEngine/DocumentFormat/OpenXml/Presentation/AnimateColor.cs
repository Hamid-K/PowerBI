using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A19 RID: 10777
	[ChildElementInfo(typeof(ToColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonBehavior))]
	[ChildElementInfo(typeof(ByColor))]
	[ChildElementInfo(typeof(FromColor))]
	internal class AnimateColor : OpenXmlCompositeElement
	{
		// Token: 0x17007001 RID: 28673
		// (get) Token: 0x06015955 RID: 88405 RVA: 0x00320D84 File Offset: 0x0031EF84
		public override string LocalName
		{
			get
			{
				return "animClr";
			}
		}

		// Token: 0x17007002 RID: 28674
		// (get) Token: 0x06015956 RID: 88406 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007003 RID: 28675
		// (get) Token: 0x06015957 RID: 88407 RVA: 0x00320D8B File Offset: 0x0031EF8B
		internal override int ElementTypeId
		{
			get
			{
				return 12203;
			}
		}

		// Token: 0x06015958 RID: 88408 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007004 RID: 28676
		// (get) Token: 0x06015959 RID: 88409 RVA: 0x00320D92 File Offset: 0x0031EF92
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateColor.attributeTagNames;
			}
		}

		// Token: 0x17007005 RID: 28677
		// (get) Token: 0x0601595A RID: 88410 RVA: 0x00320D99 File Offset: 0x0031EF99
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17007006 RID: 28678
		// (get) Token: 0x0601595B RID: 88411 RVA: 0x00320DA0 File Offset: 0x0031EFA0
		// (set) Token: 0x0601595C RID: 88412 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "clrSpc")]
		public EnumValue<AnimateColorSpaceValues> ColorSpace
		{
			get
			{
				return (EnumValue<AnimateColorSpaceValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007007 RID: 28679
		// (get) Token: 0x0601595D RID: 88413 RVA: 0x00320DAF File Offset: 0x0031EFAF
		// (set) Token: 0x0601595E RID: 88414 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dir")]
		public EnumValue<AnimateColorDirectionValues> Direction
		{
			get
			{
				return (EnumValue<AnimateColorDirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601595F RID: 88415 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnimateColor()
		{
		}

		// Token: 0x06015960 RID: 88416 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnimateColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015961 RID: 88417 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnimateColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015962 RID: 88418 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnimateColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015963 RID: 88419 RVA: 0x00320DC0 File Offset: 0x0031EFC0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			if (24 == namespaceId && "by" == name)
			{
				return new ByColor();
			}
			if (24 == namespaceId && "from" == name)
			{
				return new FromColor();
			}
			if (24 == namespaceId && "to" == name)
			{
				return new ToColor();
			}
			return null;
		}

		// Token: 0x17007008 RID: 28680
		// (get) Token: 0x06015964 RID: 88420 RVA: 0x00320E2E File Offset: 0x0031F02E
		internal override string[] ElementTagNames
		{
			get
			{
				return AnimateColor.eleTagNames;
			}
		}

		// Token: 0x17007009 RID: 28681
		// (get) Token: 0x06015965 RID: 88421 RVA: 0x00320E35 File Offset: 0x0031F035
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnimateColor.eleNamespaceIds;
			}
		}

		// Token: 0x1700700A RID: 28682
		// (get) Token: 0x06015966 RID: 88422 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700700B RID: 28683
		// (get) Token: 0x06015967 RID: 88423 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x06015968 RID: 88424 RVA: 0x00320C30 File Offset: 0x0031EE30
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

		// Token: 0x1700700C RID: 28684
		// (get) Token: 0x06015969 RID: 88425 RVA: 0x00320E3C File Offset: 0x0031F03C
		// (set) Token: 0x0601596A RID: 88426 RVA: 0x00320E45 File Offset: 0x0031F045
		public ByColor ByColor
		{
			get
			{
				return base.GetElement<ByColor>(1);
			}
			set
			{
				base.SetElement<ByColor>(1, value);
			}
		}

		// Token: 0x1700700D RID: 28685
		// (get) Token: 0x0601596B RID: 88427 RVA: 0x00320E4F File Offset: 0x0031F04F
		// (set) Token: 0x0601596C RID: 88428 RVA: 0x00320E58 File Offset: 0x0031F058
		public FromColor FromColor
		{
			get
			{
				return base.GetElement<FromColor>(2);
			}
			set
			{
				base.SetElement<FromColor>(2, value);
			}
		}

		// Token: 0x1700700E RID: 28686
		// (get) Token: 0x0601596D RID: 88429 RVA: 0x00320E62 File Offset: 0x0031F062
		// (set) Token: 0x0601596E RID: 88430 RVA: 0x00320E6B File Offset: 0x0031F06B
		public ToColor ToColor
		{
			get
			{
				return base.GetElement<ToColor>(3);
			}
			set
			{
				base.SetElement<ToColor>(3, value);
			}
		}

		// Token: 0x0601596F RID: 88431 RVA: 0x00320E75 File Offset: 0x0031F075
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "clrSpc" == name)
			{
				return new EnumValue<AnimateColorSpaceValues>();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<AnimateColorDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015970 RID: 88432 RVA: 0x00320EAB File Offset: 0x0031F0AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateColor>(deep);
		}

		// Token: 0x06015971 RID: 88433 RVA: 0x00320EB4 File Offset: 0x0031F0B4
		// Note: this type is marked as 'beforefieldinit'.
		static AnimateColor()
		{
			byte[] array = new byte[2];
			AnimateColor.attributeNamespaceIds = array;
			AnimateColor.eleTagNames = new string[] { "cBhvr", "by", "from", "to" };
			AnimateColor.eleNamespaceIds = new byte[] { 24, 24, 24, 24 };
		}

		// Token: 0x040093FA RID: 37882
		private const string tagName = "animClr";

		// Token: 0x040093FB RID: 37883
		private const byte tagNsId = 24;

		// Token: 0x040093FC RID: 37884
		internal const int ElementTypeIdConst = 12203;

		// Token: 0x040093FD RID: 37885
		private static string[] attributeTagNames = new string[] { "clrSpc", "dir" };

		// Token: 0x040093FE RID: 37886
		private static byte[] attributeNamespaceIds;

		// Token: 0x040093FF RID: 37887
		private static readonly string[] eleTagNames;

		// Token: 0x04009400 RID: 37888
		private static readonly byte[] eleNamespaceIds;
	}
}
