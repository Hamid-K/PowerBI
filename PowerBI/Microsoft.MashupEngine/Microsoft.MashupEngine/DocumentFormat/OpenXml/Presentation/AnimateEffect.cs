using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1A RID: 10778
	[ChildElementInfo(typeof(CommonBehavior))]
	[ChildElementInfo(typeof(Progress))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AnimateEffect : OpenXmlCompositeElement
	{
		// Token: 0x1700700F RID: 28687
		// (get) Token: 0x06015972 RID: 88434 RVA: 0x00320F2E File Offset: 0x0031F12E
		public override string LocalName
		{
			get
			{
				return "animEffect";
			}
		}

		// Token: 0x17007010 RID: 28688
		// (get) Token: 0x06015973 RID: 88435 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007011 RID: 28689
		// (get) Token: 0x06015974 RID: 88436 RVA: 0x00320F35 File Offset: 0x0031F135
		internal override int ElementTypeId
		{
			get
			{
				return 12204;
			}
		}

		// Token: 0x06015975 RID: 88437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007012 RID: 28690
		// (get) Token: 0x06015976 RID: 88438 RVA: 0x00320F3C File Offset: 0x0031F13C
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateEffect.attributeTagNames;
			}
		}

		// Token: 0x17007013 RID: 28691
		// (get) Token: 0x06015977 RID: 88439 RVA: 0x00320F43 File Offset: 0x0031F143
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateEffect.attributeNamespaceIds;
			}
		}

		// Token: 0x17007014 RID: 28692
		// (get) Token: 0x06015978 RID: 88440 RVA: 0x00320F4A File Offset: 0x0031F14A
		// (set) Token: 0x06015979 RID: 88441 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "transition")]
		public EnumValue<AnimateEffectTransitionValues> Transition
		{
			get
			{
				return (EnumValue<AnimateEffectTransitionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007015 RID: 28693
		// (get) Token: 0x0601597A RID: 88442 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601597B RID: 88443 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "filter")]
		public StringValue Filter
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

		// Token: 0x17007016 RID: 28694
		// (get) Token: 0x0601597C RID: 88444 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601597D RID: 88445 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "prLst")]
		public StringValue PropertyList
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

		// Token: 0x0601597E RID: 88446 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnimateEffect()
		{
		}

		// Token: 0x0601597F RID: 88447 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnimateEffect(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015980 RID: 88448 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnimateEffect(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015981 RID: 88449 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnimateEffect(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015982 RID: 88450 RVA: 0x00320F59 File Offset: 0x0031F159
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			if (24 == namespaceId && "progress" == name)
			{
				return new Progress();
			}
			return null;
		}

		// Token: 0x17007017 RID: 28695
		// (get) Token: 0x06015983 RID: 88451 RVA: 0x00320F8C File Offset: 0x0031F18C
		internal override string[] ElementTagNames
		{
			get
			{
				return AnimateEffect.eleTagNames;
			}
		}

		// Token: 0x17007018 RID: 28696
		// (get) Token: 0x06015984 RID: 88452 RVA: 0x00320F93 File Offset: 0x0031F193
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnimateEffect.eleNamespaceIds;
			}
		}

		// Token: 0x17007019 RID: 28697
		// (get) Token: 0x06015985 RID: 88453 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700701A RID: 28698
		// (get) Token: 0x06015986 RID: 88454 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x06015987 RID: 88455 RVA: 0x00320C30 File Offset: 0x0031EE30
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

		// Token: 0x1700701B RID: 28699
		// (get) Token: 0x06015988 RID: 88456 RVA: 0x00320F9A File Offset: 0x0031F19A
		// (set) Token: 0x06015989 RID: 88457 RVA: 0x00320FA3 File Offset: 0x0031F1A3
		public Progress Progress
		{
			get
			{
				return base.GetElement<Progress>(1);
			}
			set
			{
				base.SetElement<Progress>(1, value);
			}
		}

		// Token: 0x0601598A RID: 88458 RVA: 0x00320FB0 File Offset: 0x0031F1B0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "transition" == name)
			{
				return new EnumValue<AnimateEffectTransitionValues>();
			}
			if (namespaceId == 0 && "filter" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "prLst" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601598B RID: 88459 RVA: 0x00321007 File Offset: 0x0031F207
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateEffect>(deep);
		}

		// Token: 0x0601598C RID: 88460 RVA: 0x00321010 File Offset: 0x0031F210
		// Note: this type is marked as 'beforefieldinit'.
		static AnimateEffect()
		{
			byte[] array = new byte[3];
			AnimateEffect.attributeNamespaceIds = array;
			AnimateEffect.eleTagNames = new string[] { "cBhvr", "progress" };
			AnimateEffect.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009401 RID: 37889
		private const string tagName = "animEffect";

		// Token: 0x04009402 RID: 37890
		private const byte tagNsId = 24;

		// Token: 0x04009403 RID: 37891
		internal const int ElementTypeIdConst = 12204;

		// Token: 0x04009404 RID: 37892
		private static string[] attributeTagNames = new string[] { "transition", "filter", "prLst" };

		// Token: 0x04009405 RID: 37893
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009406 RID: 37894
		private static readonly string[] eleTagNames;

		// Token: 0x04009407 RID: 37895
		private static readonly byte[] eleNamespaceIds;
	}
}
