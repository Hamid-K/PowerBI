using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CA RID: 10698
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class SuperscriptProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006E06 RID: 28166
		// (get) Token: 0x060154F3 RID: 87283 RVA: 0x0031DD69 File Offset: 0x0031BF69
		public override string LocalName
		{
			get
			{
				return "sSupPr";
			}
		}

		// Token: 0x17006E07 RID: 28167
		// (get) Token: 0x060154F4 RID: 87284 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E08 RID: 28168
		// (get) Token: 0x060154F5 RID: 87285 RVA: 0x0031DD70 File Offset: 0x0031BF70
		internal override int ElementTypeId
		{
			get
			{
				return 10942;
			}
		}

		// Token: 0x060154F6 RID: 87286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060154F7 RID: 87287 RVA: 0x00293ECF File Offset: 0x002920CF
		public SuperscriptProperties()
		{
		}

		// Token: 0x060154F8 RID: 87288 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SuperscriptProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154F9 RID: 87289 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SuperscriptProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154FA RID: 87290 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SuperscriptProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060154FB RID: 87291 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006E09 RID: 28169
		// (get) Token: 0x060154FC RID: 87292 RVA: 0x0031DD77 File Offset: 0x0031BF77
		internal override string[] ElementTagNames
		{
			get
			{
				return SuperscriptProperties.eleTagNames;
			}
		}

		// Token: 0x17006E0A RID: 28170
		// (get) Token: 0x060154FD RID: 87293 RVA: 0x0031DD7E File Offset: 0x0031BF7E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SuperscriptProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006E0B RID: 28171
		// (get) Token: 0x060154FE RID: 87294 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006E0C RID: 28172
		// (get) Token: 0x060154FF RID: 87295 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x06015500 RID: 87296 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
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

		// Token: 0x06015501 RID: 87297 RVA: 0x0031DD85 File Offset: 0x0031BF85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuperscriptProperties>(deep);
		}

		// Token: 0x0400929B RID: 37531
		private const string tagName = "sSupPr";

		// Token: 0x0400929C RID: 37532
		private const byte tagNsId = 21;

		// Token: 0x0400929D RID: 37533
		internal const int ElementTypeIdConst = 10942;

		// Token: 0x0400929E RID: 37534
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x0400929F RID: 37535
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
