using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002961 RID: 10593
	[ChildElementInfo(typeof(PreSubSuperProperties))]
	[ChildElementInfo(typeof(SubArgument))]
	[ChildElementInfo(typeof(SuperArgument))]
	[ChildElementInfo(typeof(Base))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PreSubSuper : OpenXmlCompositeElement
	{
		// Token: 0x17006C01 RID: 27649
		// (get) Token: 0x0601507F RID: 86143 RVA: 0x0031A1E4 File Offset: 0x003183E4
		public override string LocalName
		{
			get
			{
				return "sPre";
			}
		}

		// Token: 0x17006C02 RID: 27650
		// (get) Token: 0x06015080 RID: 86144 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C03 RID: 27651
		// (get) Token: 0x06015081 RID: 86145 RVA: 0x0031A1EB File Offset: 0x003183EB
		internal override int ElementTypeId
		{
			get
			{
				return 10857;
			}
		}

		// Token: 0x06015082 RID: 86146 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015083 RID: 86147 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreSubSuper()
		{
		}

		// Token: 0x06015084 RID: 86148 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreSubSuper(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015085 RID: 86149 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreSubSuper(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015086 RID: 86150 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreSubSuper(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015087 RID: 86151 RVA: 0x0031A1F4 File Offset: 0x003183F4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "sPrePr" == name)
			{
				return new PreSubSuperProperties();
			}
			if (21 == namespaceId && "sub" == name)
			{
				return new SubArgument();
			}
			if (21 == namespaceId && "sup" == name)
			{
				return new SuperArgument();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006C04 RID: 27652
		// (get) Token: 0x06015088 RID: 86152 RVA: 0x0031A262 File Offset: 0x00318462
		internal override string[] ElementTagNames
		{
			get
			{
				return PreSubSuper.eleTagNames;
			}
		}

		// Token: 0x17006C05 RID: 27653
		// (get) Token: 0x06015089 RID: 86153 RVA: 0x0031A269 File Offset: 0x00318469
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreSubSuper.eleNamespaceIds;
			}
		}

		// Token: 0x17006C06 RID: 27654
		// (get) Token: 0x0601508A RID: 86154 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C07 RID: 27655
		// (get) Token: 0x0601508B RID: 86155 RVA: 0x0031A270 File Offset: 0x00318470
		// (set) Token: 0x0601508C RID: 86156 RVA: 0x0031A279 File Offset: 0x00318479
		public PreSubSuperProperties PreSubSuperProperties
		{
			get
			{
				return base.GetElement<PreSubSuperProperties>(0);
			}
			set
			{
				base.SetElement<PreSubSuperProperties>(0, value);
			}
		}

		// Token: 0x17006C08 RID: 27656
		// (get) Token: 0x0601508D RID: 86157 RVA: 0x00319FB7 File Offset: 0x003181B7
		// (set) Token: 0x0601508E RID: 86158 RVA: 0x00319FC0 File Offset: 0x003181C0
		public SubArgument SubArgument
		{
			get
			{
				return base.GetElement<SubArgument>(1);
			}
			set
			{
				base.SetElement<SubArgument>(1, value);
			}
		}

		// Token: 0x17006C09 RID: 27657
		// (get) Token: 0x0601508F RID: 86159 RVA: 0x00319FCA File Offset: 0x003181CA
		// (set) Token: 0x06015090 RID: 86160 RVA: 0x00319FD3 File Offset: 0x003181D3
		public SuperArgument SuperArgument
		{
			get
			{
				return base.GetElement<SuperArgument>(2);
			}
			set
			{
				base.SetElement<SuperArgument>(2, value);
			}
		}

		// Token: 0x17006C0A RID: 27658
		// (get) Token: 0x06015091 RID: 86161 RVA: 0x00319FDD File Offset: 0x003181DD
		// (set) Token: 0x06015092 RID: 86162 RVA: 0x00319FE6 File Offset: 0x003181E6
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(3);
			}
			set
			{
				base.SetElement<Base>(3, value);
			}
		}

		// Token: 0x06015093 RID: 86163 RVA: 0x0031A283 File Offset: 0x00318483
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreSubSuper>(deep);
		}

		// Token: 0x04009124 RID: 37156
		private const string tagName = "sPre";

		// Token: 0x04009125 RID: 37157
		private const byte tagNsId = 21;

		// Token: 0x04009126 RID: 37158
		internal const int ElementTypeIdConst = 10857;

		// Token: 0x04009127 RID: 37159
		private static readonly string[] eleTagNames = new string[] { "sPrePr", "sub", "sup", "e" };

		// Token: 0x04009128 RID: 37160
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21 };
	}
}
