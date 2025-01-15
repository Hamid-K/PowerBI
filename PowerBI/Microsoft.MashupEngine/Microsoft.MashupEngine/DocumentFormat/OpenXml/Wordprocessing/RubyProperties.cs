using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4B RID: 12107
	[ChildElementInfo(typeof(PhoneticGuideTextFontSize))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RubyAlign))]
	[ChildElementInfo(typeof(PhoneticGuideBaseTextSize))]
	[ChildElementInfo(typeof(Dirty))]
	[ChildElementInfo(typeof(PhoneticGuideRaise))]
	[ChildElementInfo(typeof(LanguageId))]
	internal class RubyProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009001 RID: 36865
		// (get) Token: 0x06019FE2 RID: 106466 RVA: 0x0035A9B8 File Offset: 0x00358BB8
		public override string LocalName
		{
			get
			{
				return "rubyPr";
			}
		}

		// Token: 0x17009002 RID: 36866
		// (get) Token: 0x06019FE3 RID: 106467 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009003 RID: 36867
		// (get) Token: 0x06019FE4 RID: 106468 RVA: 0x0035A9BF File Offset: 0x00358BBF
		internal override int ElementTypeId
		{
			get
			{
				return 11758;
			}
		}

		// Token: 0x06019FE5 RID: 106469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019FE6 RID: 106470 RVA: 0x00293ECF File Offset: 0x002920CF
		public RubyProperties()
		{
		}

		// Token: 0x06019FE7 RID: 106471 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RubyProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FE8 RID: 106472 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RubyProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FE9 RID: 106473 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RubyProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019FEA RID: 106474 RVA: 0x0035A9C8 File Offset: 0x00358BC8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rubyAlign" == name)
			{
				return new RubyAlign();
			}
			if (23 == namespaceId && "hps" == name)
			{
				return new PhoneticGuideTextFontSize();
			}
			if (23 == namespaceId && "hpsRaise" == name)
			{
				return new PhoneticGuideRaise();
			}
			if (23 == namespaceId && "hpsBaseText" == name)
			{
				return new PhoneticGuideBaseTextSize();
			}
			if (23 == namespaceId && "lid" == name)
			{
				return new LanguageId();
			}
			if (23 == namespaceId && "dirty" == name)
			{
				return new Dirty();
			}
			return null;
		}

		// Token: 0x17009004 RID: 36868
		// (get) Token: 0x06019FEB RID: 106475 RVA: 0x0035AA66 File Offset: 0x00358C66
		internal override string[] ElementTagNames
		{
			get
			{
				return RubyProperties.eleTagNames;
			}
		}

		// Token: 0x17009005 RID: 36869
		// (get) Token: 0x06019FEC RID: 106476 RVA: 0x0035AA6D File Offset: 0x00358C6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RubyProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009006 RID: 36870
		// (get) Token: 0x06019FED RID: 106477 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009007 RID: 36871
		// (get) Token: 0x06019FEE RID: 106478 RVA: 0x0035AA74 File Offset: 0x00358C74
		// (set) Token: 0x06019FEF RID: 106479 RVA: 0x0035AA7D File Offset: 0x00358C7D
		public RubyAlign RubyAlign
		{
			get
			{
				return base.GetElement<RubyAlign>(0);
			}
			set
			{
				base.SetElement<RubyAlign>(0, value);
			}
		}

		// Token: 0x17009008 RID: 36872
		// (get) Token: 0x06019FF0 RID: 106480 RVA: 0x0035AA87 File Offset: 0x00358C87
		// (set) Token: 0x06019FF1 RID: 106481 RVA: 0x0035AA90 File Offset: 0x00358C90
		public PhoneticGuideTextFontSize PhoneticGuideTextFontSize
		{
			get
			{
				return base.GetElement<PhoneticGuideTextFontSize>(1);
			}
			set
			{
				base.SetElement<PhoneticGuideTextFontSize>(1, value);
			}
		}

		// Token: 0x17009009 RID: 36873
		// (get) Token: 0x06019FF2 RID: 106482 RVA: 0x0035AA9A File Offset: 0x00358C9A
		// (set) Token: 0x06019FF3 RID: 106483 RVA: 0x0035AAA3 File Offset: 0x00358CA3
		public PhoneticGuideRaise PhoneticGuideRaise
		{
			get
			{
				return base.GetElement<PhoneticGuideRaise>(2);
			}
			set
			{
				base.SetElement<PhoneticGuideRaise>(2, value);
			}
		}

		// Token: 0x1700900A RID: 36874
		// (get) Token: 0x06019FF4 RID: 106484 RVA: 0x0035AAAD File Offset: 0x00358CAD
		// (set) Token: 0x06019FF5 RID: 106485 RVA: 0x0035AAB6 File Offset: 0x00358CB6
		public PhoneticGuideBaseTextSize PhoneticGuideBaseTextSize
		{
			get
			{
				return base.GetElement<PhoneticGuideBaseTextSize>(3);
			}
			set
			{
				base.SetElement<PhoneticGuideBaseTextSize>(3, value);
			}
		}

		// Token: 0x1700900B RID: 36875
		// (get) Token: 0x06019FF6 RID: 106486 RVA: 0x0035AAC0 File Offset: 0x00358CC0
		// (set) Token: 0x06019FF7 RID: 106487 RVA: 0x0035AAC9 File Offset: 0x00358CC9
		public LanguageId LanguageId
		{
			get
			{
				return base.GetElement<LanguageId>(4);
			}
			set
			{
				base.SetElement<LanguageId>(4, value);
			}
		}

		// Token: 0x1700900C RID: 36876
		// (get) Token: 0x06019FF8 RID: 106488 RVA: 0x0035AAD3 File Offset: 0x00358CD3
		// (set) Token: 0x06019FF9 RID: 106489 RVA: 0x0035AADC File Offset: 0x00358CDC
		public Dirty Dirty
		{
			get
			{
				return base.GetElement<Dirty>(5);
			}
			set
			{
				base.SetElement<Dirty>(5, value);
			}
		}

		// Token: 0x06019FFA RID: 106490 RVA: 0x0035AAE6 File Offset: 0x00358CE6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RubyProperties>(deep);
		}

		// Token: 0x0400AB4D RID: 43853
		private const string tagName = "rubyPr";

		// Token: 0x0400AB4E RID: 43854
		private const byte tagNsId = 23;

		// Token: 0x0400AB4F RID: 43855
		internal const int ElementTypeIdConst = 11758;

		// Token: 0x0400AB50 RID: 43856
		private static readonly string[] eleTagNames = new string[] { "rubyAlign", "hps", "hpsRaise", "hpsBaseText", "lid", "dirty" };

		// Token: 0x0400AB51 RID: 43857
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
