using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C9 RID: 10697
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AlignScripts))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class SubSuperscriptProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DFE RID: 28158
		// (get) Token: 0x060154E1 RID: 87265 RVA: 0x0031DCBC File Offset: 0x0031BEBC
		public override string LocalName
		{
			get
			{
				return "sSubSupPr";
			}
		}

		// Token: 0x17006DFF RID: 28159
		// (get) Token: 0x060154E2 RID: 87266 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E00 RID: 28160
		// (get) Token: 0x060154E3 RID: 87267 RVA: 0x0031DCC3 File Offset: 0x0031BEC3
		internal override int ElementTypeId
		{
			get
			{
				return 10941;
			}
		}

		// Token: 0x060154E4 RID: 87268 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060154E5 RID: 87269 RVA: 0x00293ECF File Offset: 0x002920CF
		public SubSuperscriptProperties()
		{
		}

		// Token: 0x060154E6 RID: 87270 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SubSuperscriptProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154E7 RID: 87271 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SubSuperscriptProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154E8 RID: 87272 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SubSuperscriptProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060154E9 RID: 87273 RVA: 0x0031DCCA File Offset: 0x0031BECA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "alnScr" == name)
			{
				return new AlignScripts();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006E01 RID: 28161
		// (get) Token: 0x060154EA RID: 87274 RVA: 0x0031DCFD File Offset: 0x0031BEFD
		internal override string[] ElementTagNames
		{
			get
			{
				return SubSuperscriptProperties.eleTagNames;
			}
		}

		// Token: 0x17006E02 RID: 28162
		// (get) Token: 0x060154EB RID: 87275 RVA: 0x0031DD04 File Offset: 0x0031BF04
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SubSuperscriptProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006E03 RID: 28163
		// (get) Token: 0x060154EC RID: 87276 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006E04 RID: 28164
		// (get) Token: 0x060154ED RID: 87277 RVA: 0x0031DD0B File Offset: 0x0031BF0B
		// (set) Token: 0x060154EE RID: 87278 RVA: 0x0031DD14 File Offset: 0x0031BF14
		public AlignScripts AlignScripts
		{
			get
			{
				return base.GetElement<AlignScripts>(0);
			}
			set
			{
				base.SetElement<AlignScripts>(0, value);
			}
		}

		// Token: 0x17006E05 RID: 28165
		// (get) Token: 0x060154EF RID: 87279 RVA: 0x0031BAC1 File Offset: 0x00319CC1
		// (set) Token: 0x060154F0 RID: 87280 RVA: 0x0031BACA File Offset: 0x00319CCA
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(1);
			}
			set
			{
				base.SetElement<ControlProperties>(1, value);
			}
		}

		// Token: 0x060154F1 RID: 87281 RVA: 0x0031DD1E File Offset: 0x0031BF1E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubSuperscriptProperties>(deep);
		}

		// Token: 0x04009296 RID: 37526
		private const string tagName = "sSubSupPr";

		// Token: 0x04009297 RID: 37527
		private const byte tagNsId = 21;

		// Token: 0x04009298 RID: 37528
		internal const int ElementTypeIdConst = 10941;

		// Token: 0x04009299 RID: 37529
		private static readonly string[] eleTagNames = new string[] { "alnScr", "ctrlPr" };

		// Token: 0x0400929A RID: 37530
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
