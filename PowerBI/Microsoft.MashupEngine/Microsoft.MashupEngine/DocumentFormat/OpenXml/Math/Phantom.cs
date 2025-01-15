using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295F RID: 10591
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PhantomProperties))]
	[ChildElementInfo(typeof(Base))]
	internal class Phantom : OpenXmlCompositeElement
	{
		// Token: 0x17006BF0 RID: 27632
		// (get) Token: 0x06015059 RID: 86105 RVA: 0x0031A04C File Offset: 0x0031824C
		public override string LocalName
		{
			get
			{
				return "phant";
			}
		}

		// Token: 0x17006BF1 RID: 27633
		// (get) Token: 0x0601505A RID: 86106 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BF2 RID: 27634
		// (get) Token: 0x0601505B RID: 86107 RVA: 0x0031A053 File Offset: 0x00318253
		internal override int ElementTypeId
		{
			get
			{
				return 10855;
			}
		}

		// Token: 0x0601505C RID: 86108 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601505D RID: 86109 RVA: 0x00293ECF File Offset: 0x002920CF
		public Phantom()
		{
		}

		// Token: 0x0601505E RID: 86110 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Phantom(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601505F RID: 86111 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Phantom(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015060 RID: 86112 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Phantom(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015061 RID: 86113 RVA: 0x0031A05A File Offset: 0x0031825A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "phantPr" == name)
			{
				return new PhantomProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BF3 RID: 27635
		// (get) Token: 0x06015062 RID: 86114 RVA: 0x0031A08D File Offset: 0x0031828D
		internal override string[] ElementTagNames
		{
			get
			{
				return Phantom.eleTagNames;
			}
		}

		// Token: 0x17006BF4 RID: 27636
		// (get) Token: 0x06015063 RID: 86115 RVA: 0x0031A094 File Offset: 0x00318294
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Phantom.eleNamespaceIds;
			}
		}

		// Token: 0x17006BF5 RID: 27637
		// (get) Token: 0x06015064 RID: 86116 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BF6 RID: 27638
		// (get) Token: 0x06015065 RID: 86117 RVA: 0x0031A09B File Offset: 0x0031829B
		// (set) Token: 0x06015066 RID: 86118 RVA: 0x0031A0A4 File Offset: 0x003182A4
		public PhantomProperties PhantomProperties
		{
			get
			{
				return base.GetElement<PhantomProperties>(0);
			}
			set
			{
				base.SetElement<PhantomProperties>(0, value);
			}
		}

		// Token: 0x17006BF7 RID: 27639
		// (get) Token: 0x06015067 RID: 86119 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06015068 RID: 86120 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x06015069 RID: 86121 RVA: 0x0031A0AE File Offset: 0x003182AE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Phantom>(deep);
		}

		// Token: 0x0400911A RID: 37146
		private const string tagName = "phant";

		// Token: 0x0400911B RID: 37147
		private const byte tagNsId = 21;

		// Token: 0x0400911C RID: 37148
		internal const int ElementTypeIdConst = 10855;

		// Token: 0x0400911D RID: 37149
		private static readonly string[] eleTagNames = new string[] { "phantPr", "e" };

		// Token: 0x0400911E RID: 37150
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
