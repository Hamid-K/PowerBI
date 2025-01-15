using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C7 RID: 10695
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PreSubSuperProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DF0 RID: 28144
		// (get) Token: 0x060154C1 RID: 87233 RVA: 0x0031DC05 File Offset: 0x0031BE05
		public override string LocalName
		{
			get
			{
				return "sPrePr";
			}
		}

		// Token: 0x17006DF1 RID: 28145
		// (get) Token: 0x060154C2 RID: 87234 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DF2 RID: 28146
		// (get) Token: 0x060154C3 RID: 87235 RVA: 0x0031DC0C File Offset: 0x0031BE0C
		internal override int ElementTypeId
		{
			get
			{
				return 10938;
			}
		}

		// Token: 0x060154C4 RID: 87236 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060154C5 RID: 87237 RVA: 0x00293ECF File Offset: 0x002920CF
		public PreSubSuperProperties()
		{
		}

		// Token: 0x060154C6 RID: 87238 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PreSubSuperProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154C7 RID: 87239 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PreSubSuperProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154C8 RID: 87240 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PreSubSuperProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060154C9 RID: 87241 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DF3 RID: 28147
		// (get) Token: 0x060154CA RID: 87242 RVA: 0x0031DC13 File Offset: 0x0031BE13
		internal override string[] ElementTagNames
		{
			get
			{
				return PreSubSuperProperties.eleTagNames;
			}
		}

		// Token: 0x17006DF4 RID: 28148
		// (get) Token: 0x060154CB RID: 87243 RVA: 0x0031DC1A File Offset: 0x0031BE1A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PreSubSuperProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DF5 RID: 28149
		// (get) Token: 0x060154CC RID: 87244 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DF6 RID: 28150
		// (get) Token: 0x060154CD RID: 87245 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x060154CE RID: 87246 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(0);
			}
			set
			{
				base.SetElement<ControlProperties>(0, value);
			}
		}

		// Token: 0x060154CF RID: 87247 RVA: 0x0031DC21 File Offset: 0x0031BE21
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreSubSuperProperties>(deep);
		}

		// Token: 0x0400928C RID: 37516
		private const string tagName = "sPrePr";

		// Token: 0x0400928D RID: 37517
		private const byte tagNsId = 21;

		// Token: 0x0400928E RID: 37518
		internal const int ElementTypeIdConst = 10938;

		// Token: 0x0400928F RID: 37519
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x04009290 RID: 37520
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
