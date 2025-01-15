using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AB RID: 10667
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FractionType))]
	internal class FractionProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D52 RID: 27986
		// (get) Token: 0x06015365 RID: 86885 RVA: 0x0031CEEC File Offset: 0x0031B0EC
		public override string LocalName
		{
			get
			{
				return "fPr";
			}
		}

		// Token: 0x17006D53 RID: 27987
		// (get) Token: 0x06015366 RID: 86886 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D54 RID: 27988
		// (get) Token: 0x06015367 RID: 86887 RVA: 0x0031CEF3 File Offset: 0x0031B0F3
		internal override int ElementTypeId
		{
			get
			{
				return 10902;
			}
		}

		// Token: 0x06015368 RID: 86888 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015369 RID: 86889 RVA: 0x00293ECF File Offset: 0x002920CF
		public FractionProperties()
		{
		}

		// Token: 0x0601536A RID: 86890 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FractionProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601536B RID: 86891 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FractionProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601536C RID: 86892 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FractionProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601536D RID: 86893 RVA: 0x0031CEFA File Offset: 0x0031B0FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "type" == name)
			{
				return new FractionType();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D55 RID: 27989
		// (get) Token: 0x0601536E RID: 86894 RVA: 0x0031CF2D File Offset: 0x0031B12D
		internal override string[] ElementTagNames
		{
			get
			{
				return FractionProperties.eleTagNames;
			}
		}

		// Token: 0x17006D56 RID: 27990
		// (get) Token: 0x0601536F RID: 86895 RVA: 0x0031CF34 File Offset: 0x0031B134
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FractionProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D57 RID: 27991
		// (get) Token: 0x06015370 RID: 86896 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D58 RID: 27992
		// (get) Token: 0x06015371 RID: 86897 RVA: 0x0031CF3B File Offset: 0x0031B13B
		// (set) Token: 0x06015372 RID: 86898 RVA: 0x0031CF44 File Offset: 0x0031B144
		public FractionType FractionType
		{
			get
			{
				return base.GetElement<FractionType>(0);
			}
			set
			{
				base.SetElement<FractionType>(0, value);
			}
		}

		// Token: 0x17006D59 RID: 27993
		// (get) Token: 0x06015373 RID: 86899 RVA: 0x0031BAC1 File Offset: 0x00319CC1
		// (set) Token: 0x06015374 RID: 86900 RVA: 0x0031BACA File Offset: 0x00319CCA
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

		// Token: 0x06015375 RID: 86901 RVA: 0x0031CF4E File Offset: 0x0031B14E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FractionProperties>(deep);
		}

		// Token: 0x04009220 RID: 37408
		private const string tagName = "fPr";

		// Token: 0x04009221 RID: 37409
		private const byte tagNsId = 21;

		// Token: 0x04009222 RID: 37410
		internal const int ElementTypeIdConst = 10902;

		// Token: 0x04009223 RID: 37411
		private static readonly string[] eleTagNames = new string[] { "type", "ctrlPr" };

		// Token: 0x04009224 RID: 37412
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
