using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002963 RID: 10595
	[ChildElementInfo(typeof(SubArgument))]
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(SubSuperscriptProperties))]
	[ChildElementInfo(typeof(SuperArgument))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SubSuperscript : OpenXmlCompositeElement
	{
		// Token: 0x17006C14 RID: 27668
		// (get) Token: 0x060150A9 RID: 86185 RVA: 0x0031A3C8 File Offset: 0x003185C8
		public override string LocalName
		{
			get
			{
				return "sSubSup";
			}
		}

		// Token: 0x17006C15 RID: 27669
		// (get) Token: 0x060150AA RID: 86186 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C16 RID: 27670
		// (get) Token: 0x060150AB RID: 86187 RVA: 0x0031A3CF File Offset: 0x003185CF
		internal override int ElementTypeId
		{
			get
			{
				return 10859;
			}
		}

		// Token: 0x060150AC RID: 86188 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060150AD RID: 86189 RVA: 0x00293ECF File Offset: 0x002920CF
		public SubSuperscript()
		{
		}

		// Token: 0x060150AE RID: 86190 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SubSuperscript(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150AF RID: 86191 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SubSuperscript(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060150B0 RID: 86192 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SubSuperscript(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060150B1 RID: 86193 RVA: 0x0031A3D8 File Offset: 0x003185D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "sSubSupPr" == name)
			{
				return new SubSuperscriptProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			if (21 == namespaceId && "sub" == name)
			{
				return new SubArgument();
			}
			if (21 == namespaceId && "sup" == name)
			{
				return new SuperArgument();
			}
			return null;
		}

		// Token: 0x17006C17 RID: 27671
		// (get) Token: 0x060150B2 RID: 86194 RVA: 0x0031A446 File Offset: 0x00318646
		internal override string[] ElementTagNames
		{
			get
			{
				return SubSuperscript.eleTagNames;
			}
		}

		// Token: 0x17006C18 RID: 27672
		// (get) Token: 0x060150B3 RID: 86195 RVA: 0x0031A44D File Offset: 0x0031864D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SubSuperscript.eleNamespaceIds;
			}
		}

		// Token: 0x17006C19 RID: 27673
		// (get) Token: 0x060150B4 RID: 86196 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006C1A RID: 27674
		// (get) Token: 0x060150B5 RID: 86197 RVA: 0x0031A454 File Offset: 0x00318654
		// (set) Token: 0x060150B6 RID: 86198 RVA: 0x0031A45D File Offset: 0x0031865D
		public SubSuperscriptProperties SubSuperscriptProperties
		{
			get
			{
				return base.GetElement<SubSuperscriptProperties>(0);
			}
			set
			{
				base.SetElement<SubSuperscriptProperties>(0, value);
			}
		}

		// Token: 0x17006C1B RID: 27675
		// (get) Token: 0x060150B7 RID: 86199 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x060150B8 RID: 86200 RVA: 0x0031965F File Offset: 0x0031785F
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

		// Token: 0x17006C1C RID: 27676
		// (get) Token: 0x060150B9 RID: 86201 RVA: 0x0031A363 File Offset: 0x00318563
		// (set) Token: 0x060150BA RID: 86202 RVA: 0x0031A36C File Offset: 0x0031856C
		public SubArgument SubArgument
		{
			get
			{
				return base.GetElement<SubArgument>(2);
			}
			set
			{
				base.SetElement<SubArgument>(2, value);
			}
		}

		// Token: 0x17006C1D RID: 27677
		// (get) Token: 0x060150BB RID: 86203 RVA: 0x0031A467 File Offset: 0x00318667
		// (set) Token: 0x060150BC RID: 86204 RVA: 0x0031A470 File Offset: 0x00318670
		public SuperArgument SuperArgument
		{
			get
			{
				return base.GetElement<SuperArgument>(3);
			}
			set
			{
				base.SetElement<SuperArgument>(3, value);
			}
		}

		// Token: 0x060150BD RID: 86205 RVA: 0x0031A47A File Offset: 0x0031867A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubSuperscript>(deep);
		}

		// Token: 0x0400912E RID: 37166
		private const string tagName = "sSubSup";

		// Token: 0x0400912F RID: 37167
		private const byte tagNsId = 21;

		// Token: 0x04009130 RID: 37168
		internal const int ElementTypeIdConst = 10859;

		// Token: 0x04009131 RID: 37169
		private static readonly string[] eleTagNames = new string[] { "sSubSupPr", "e", "sub", "sup" };

		// Token: 0x04009132 RID: 37170
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21 };
	}
}
