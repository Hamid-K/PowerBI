using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C6 RID: 10694
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HideDegree))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class RadicalProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DE8 RID: 28136
		// (get) Token: 0x060154AF RID: 87215 RVA: 0x0031DB58 File Offset: 0x0031BD58
		public override string LocalName
		{
			get
			{
				return "radPr";
			}
		}

		// Token: 0x17006DE9 RID: 28137
		// (get) Token: 0x060154B0 RID: 87216 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DEA RID: 28138
		// (get) Token: 0x060154B1 RID: 87217 RVA: 0x0031DB5F File Offset: 0x0031BD5F
		internal override int ElementTypeId
		{
			get
			{
				return 10936;
			}
		}

		// Token: 0x060154B2 RID: 87218 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060154B3 RID: 87219 RVA: 0x00293ECF File Offset: 0x002920CF
		public RadicalProperties()
		{
		}

		// Token: 0x060154B4 RID: 87220 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RadicalProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154B5 RID: 87221 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RadicalProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154B6 RID: 87222 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RadicalProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060154B7 RID: 87223 RVA: 0x0031DB66 File Offset: 0x0031BD66
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "degHide" == name)
			{
				return new HideDegree();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DEB RID: 28139
		// (get) Token: 0x060154B8 RID: 87224 RVA: 0x0031DB99 File Offset: 0x0031BD99
		internal override string[] ElementTagNames
		{
			get
			{
				return RadicalProperties.eleTagNames;
			}
		}

		// Token: 0x17006DEC RID: 28140
		// (get) Token: 0x060154B9 RID: 87225 RVA: 0x0031DBA0 File Offset: 0x0031BDA0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RadicalProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DED RID: 28141
		// (get) Token: 0x060154BA RID: 87226 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DEE RID: 28142
		// (get) Token: 0x060154BB RID: 87227 RVA: 0x0031DBA7 File Offset: 0x0031BDA7
		// (set) Token: 0x060154BC RID: 87228 RVA: 0x0031DBB0 File Offset: 0x0031BDB0
		public HideDegree HideDegree
		{
			get
			{
				return base.GetElement<HideDegree>(0);
			}
			set
			{
				base.SetElement<HideDegree>(0, value);
			}
		}

		// Token: 0x17006DEF RID: 28143
		// (get) Token: 0x060154BD RID: 87229 RVA: 0x0031BAC1 File Offset: 0x00319CC1
		// (set) Token: 0x060154BE RID: 87230 RVA: 0x0031BACA File Offset: 0x00319CCA
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

		// Token: 0x060154BF RID: 87231 RVA: 0x0031DBBA File Offset: 0x0031BDBA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadicalProperties>(deep);
		}

		// Token: 0x04009287 RID: 37511
		private const string tagName = "radPr";

		// Token: 0x04009288 RID: 37512
		private const byte tagNsId = 21;

		// Token: 0x04009289 RID: 37513
		internal const int ElementTypeIdConst = 10936;

		// Token: 0x0400928A RID: 37514
		private static readonly string[] eleTagNames = new string[] { "degHide", "ctrlPr" };

		// Token: 0x0400928B RID: 37515
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
