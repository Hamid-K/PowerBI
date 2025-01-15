using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AF RID: 10671
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LimitUpperProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D72 RID: 28018
		// (get) Token: 0x060153AD RID: 86957 RVA: 0x0031D19C File Offset: 0x0031B39C
		public override string LocalName
		{
			get
			{
				return "limUppPr";
			}
		}

		// Token: 0x17006D73 RID: 28019
		// (get) Token: 0x060153AE RID: 86958 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D74 RID: 28020
		// (get) Token: 0x060153AF RID: 86959 RVA: 0x0031D1A3 File Offset: 0x0031B3A3
		internal override int ElementTypeId
		{
			get
			{
				return 10911;
			}
		}

		// Token: 0x060153B0 RID: 86960 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060153B1 RID: 86961 RVA: 0x00293ECF File Offset: 0x002920CF
		public LimitUpperProperties()
		{
		}

		// Token: 0x060153B2 RID: 86962 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LimitUpperProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153B3 RID: 86963 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LimitUpperProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153B4 RID: 86964 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LimitUpperProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060153B5 RID: 86965 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D75 RID: 28021
		// (get) Token: 0x060153B6 RID: 86966 RVA: 0x0031D1AA File Offset: 0x0031B3AA
		internal override string[] ElementTagNames
		{
			get
			{
				return LimitUpperProperties.eleTagNames;
			}
		}

		// Token: 0x17006D76 RID: 28022
		// (get) Token: 0x060153B7 RID: 86967 RVA: 0x0031D1B1 File Offset: 0x0031B3B1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LimitUpperProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D77 RID: 28023
		// (get) Token: 0x060153B8 RID: 86968 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D78 RID: 28024
		// (get) Token: 0x060153B9 RID: 86969 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x060153BA RID: 86970 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
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

		// Token: 0x060153BB RID: 86971 RVA: 0x0031D1B8 File Offset: 0x0031B3B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LimitUpperProperties>(deep);
		}

		// Token: 0x04009234 RID: 37428
		private const string tagName = "limUppPr";

		// Token: 0x04009235 RID: 37429
		private const byte tagNsId = 21;

		// Token: 0x04009236 RID: 37430
		internal const int ElementTypeIdConst = 10911;

		// Token: 0x04009237 RID: 37431
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x04009238 RID: 37432
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
