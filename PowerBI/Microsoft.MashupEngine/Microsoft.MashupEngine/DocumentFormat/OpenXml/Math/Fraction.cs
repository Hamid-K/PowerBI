using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002958 RID: 10584
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FractionProperties))]
	[ChildElementInfo(typeof(Numerator))]
	[ChildElementInfo(typeof(Denominator))]
	internal class Fraction : OpenXmlCompositeElement
	{
		// Token: 0x17006BB3 RID: 27571
		// (get) Token: 0x06014FD1 RID: 85969 RVA: 0x002C81ED File Offset: 0x002C63ED
		public override string LocalName
		{
			get
			{
				return "f";
			}
		}

		// Token: 0x17006BB4 RID: 27572
		// (get) Token: 0x06014FD2 RID: 85970 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BB5 RID: 27573
		// (get) Token: 0x06014FD3 RID: 85971 RVA: 0x00319A0D File Offset: 0x00317C0D
		internal override int ElementTypeId
		{
			get
			{
				return 10848;
			}
		}

		// Token: 0x06014FD4 RID: 85972 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FD5 RID: 85973 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fraction()
		{
		}

		// Token: 0x06014FD6 RID: 85974 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fraction(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FD7 RID: 85975 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fraction(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FD8 RID: 85976 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fraction(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014FD9 RID: 85977 RVA: 0x00319A14 File Offset: 0x00317C14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "fPr" == name)
			{
				return new FractionProperties();
			}
			if (21 == namespaceId && "num" == name)
			{
				return new Numerator();
			}
			if (21 == namespaceId && "den" == name)
			{
				return new Denominator();
			}
			return null;
		}

		// Token: 0x17006BB6 RID: 27574
		// (get) Token: 0x06014FDA RID: 85978 RVA: 0x00319A6A File Offset: 0x00317C6A
		internal override string[] ElementTagNames
		{
			get
			{
				return Fraction.eleTagNames;
			}
		}

		// Token: 0x17006BB7 RID: 27575
		// (get) Token: 0x06014FDB RID: 85979 RVA: 0x00319A71 File Offset: 0x00317C71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Fraction.eleNamespaceIds;
			}
		}

		// Token: 0x17006BB8 RID: 27576
		// (get) Token: 0x06014FDC RID: 85980 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BB9 RID: 27577
		// (get) Token: 0x06014FDD RID: 85981 RVA: 0x00319A78 File Offset: 0x00317C78
		// (set) Token: 0x06014FDE RID: 85982 RVA: 0x00319A81 File Offset: 0x00317C81
		public FractionProperties FractionProperties
		{
			get
			{
				return base.GetElement<FractionProperties>(0);
			}
			set
			{
				base.SetElement<FractionProperties>(0, value);
			}
		}

		// Token: 0x17006BBA RID: 27578
		// (get) Token: 0x06014FDF RID: 85983 RVA: 0x00319A8B File Offset: 0x00317C8B
		// (set) Token: 0x06014FE0 RID: 85984 RVA: 0x00319A94 File Offset: 0x00317C94
		public Numerator Numerator
		{
			get
			{
				return base.GetElement<Numerator>(1);
			}
			set
			{
				base.SetElement<Numerator>(1, value);
			}
		}

		// Token: 0x17006BBB RID: 27579
		// (get) Token: 0x06014FE1 RID: 85985 RVA: 0x00319A9E File Offset: 0x00317C9E
		// (set) Token: 0x06014FE2 RID: 85986 RVA: 0x00319AA7 File Offset: 0x00317CA7
		public Denominator Denominator
		{
			get
			{
				return base.GetElement<Denominator>(2);
			}
			set
			{
				base.SetElement<Denominator>(2, value);
			}
		}

		// Token: 0x06014FE3 RID: 85987 RVA: 0x00319AB1 File Offset: 0x00317CB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fraction>(deep);
		}

		// Token: 0x040090F7 RID: 37111
		private const string tagName = "f";

		// Token: 0x040090F8 RID: 37112
		private const byte tagNsId = 21;

		// Token: 0x040090F9 RID: 37113
		internal const int ElementTypeIdConst = 10848;

		// Token: 0x040090FA RID: 37114
		private static readonly string[] eleTagNames = new string[] { "fPr", "num", "den" };

		// Token: 0x040090FB RID: 37115
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
