using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295B RID: 10587
	[ChildElementInfo(typeof(LimitLowerProperties))]
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(Limit))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LimitLower : OpenXmlCompositeElement
	{
		// Token: 0x17006BCD RID: 27597
		// (get) Token: 0x0601500B RID: 86027 RVA: 0x00319CB1 File Offset: 0x00317EB1
		public override string LocalName
		{
			get
			{
				return "limLow";
			}
		}

		// Token: 0x17006BCE RID: 27598
		// (get) Token: 0x0601500C RID: 86028 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BCF RID: 27599
		// (get) Token: 0x0601500D RID: 86029 RVA: 0x00319CB8 File Offset: 0x00317EB8
		internal override int ElementTypeId
		{
			get
			{
				return 10851;
			}
		}

		// Token: 0x0601500E RID: 86030 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601500F RID: 86031 RVA: 0x00293ECF File Offset: 0x002920CF
		public LimitLower()
		{
		}

		// Token: 0x06015010 RID: 86032 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LimitLower(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015011 RID: 86033 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LimitLower(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015012 RID: 86034 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LimitLower(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015013 RID: 86035 RVA: 0x00319CC0 File Offset: 0x00317EC0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "limLowPr" == name)
			{
				return new LimitLowerProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			if (21 == namespaceId && "lim" == name)
			{
				return new Limit();
			}
			return null;
		}

		// Token: 0x17006BD0 RID: 27600
		// (get) Token: 0x06015014 RID: 86036 RVA: 0x00319D16 File Offset: 0x00317F16
		internal override string[] ElementTagNames
		{
			get
			{
				return LimitLower.eleTagNames;
			}
		}

		// Token: 0x17006BD1 RID: 27601
		// (get) Token: 0x06015015 RID: 86037 RVA: 0x00319D1D File Offset: 0x00317F1D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LimitLower.eleNamespaceIds;
			}
		}

		// Token: 0x17006BD2 RID: 27602
		// (get) Token: 0x06015016 RID: 86038 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BD3 RID: 27603
		// (get) Token: 0x06015017 RID: 86039 RVA: 0x00319D24 File Offset: 0x00317F24
		// (set) Token: 0x06015018 RID: 86040 RVA: 0x00319D2D File Offset: 0x00317F2D
		public LimitLowerProperties LimitLowerProperties
		{
			get
			{
				return base.GetElement<LimitLowerProperties>(0);
			}
			set
			{
				base.SetElement<LimitLowerProperties>(0, value);
			}
		}

		// Token: 0x17006BD4 RID: 27604
		// (get) Token: 0x06015019 RID: 86041 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x0601501A RID: 86042 RVA: 0x0031965F File Offset: 0x0031785F
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

		// Token: 0x17006BD5 RID: 27605
		// (get) Token: 0x0601501B RID: 86043 RVA: 0x00319D37 File Offset: 0x00317F37
		// (set) Token: 0x0601501C RID: 86044 RVA: 0x00319D40 File Offset: 0x00317F40
		public Limit Limit
		{
			get
			{
				return base.GetElement<Limit>(2);
			}
			set
			{
				base.SetElement<Limit>(2, value);
			}
		}

		// Token: 0x0601501D RID: 86045 RVA: 0x00319D4A File Offset: 0x00317F4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LimitLower>(deep);
		}

		// Token: 0x04009106 RID: 37126
		private const string tagName = "limLow";

		// Token: 0x04009107 RID: 37127
		private const byte tagNsId = 21;

		// Token: 0x04009108 RID: 37128
		internal const int ElementTypeIdConst = 10851;

		// Token: 0x04009109 RID: 37129
		private static readonly string[] eleTagNames = new string[] { "limLowPr", "e", "lim" };

		// Token: 0x0400910A RID: 37130
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
