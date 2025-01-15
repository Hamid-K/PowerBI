using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A9 RID: 10665
	[ChildElementInfo(typeof(RowSpacing))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MaxDistribution))]
	[ChildElementInfo(typeof(BaseJustification))]
	[ChildElementInfo(typeof(ObjectDistribution))]
	[ChildElementInfo(typeof(RowSpacingRule))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class EquationArrayProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D40 RID: 27968
		// (get) Token: 0x0601533F RID: 86847 RVA: 0x0031CCDE File Offset: 0x0031AEDE
		public override string LocalName
		{
			get
			{
				return "eqArrPr";
			}
		}

		// Token: 0x17006D41 RID: 27969
		// (get) Token: 0x06015340 RID: 86848 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D42 RID: 27970
		// (get) Token: 0x06015341 RID: 86849 RVA: 0x0031CCE5 File Offset: 0x0031AEE5
		internal override int ElementTypeId
		{
			get
			{
				return 10900;
			}
		}

		// Token: 0x06015342 RID: 86850 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015343 RID: 86851 RVA: 0x00293ECF File Offset: 0x002920CF
		public EquationArrayProperties()
		{
		}

		// Token: 0x06015344 RID: 86852 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EquationArrayProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015345 RID: 86853 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EquationArrayProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015346 RID: 86854 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EquationArrayProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015347 RID: 86855 RVA: 0x0031CCEC File Offset: 0x0031AEEC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "baseJc" == name)
			{
				return new BaseJustification();
			}
			if (21 == namespaceId && "maxDist" == name)
			{
				return new MaxDistribution();
			}
			if (21 == namespaceId && "objDist" == name)
			{
				return new ObjectDistribution();
			}
			if (21 == namespaceId && "rSpRule" == name)
			{
				return new RowSpacingRule();
			}
			if (21 == namespaceId && "rSp" == name)
			{
				return new RowSpacing();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D43 RID: 27971
		// (get) Token: 0x06015348 RID: 86856 RVA: 0x0031CD8A File Offset: 0x0031AF8A
		internal override string[] ElementTagNames
		{
			get
			{
				return EquationArrayProperties.eleTagNames;
			}
		}

		// Token: 0x17006D44 RID: 27972
		// (get) Token: 0x06015349 RID: 86857 RVA: 0x0031CD91 File Offset: 0x0031AF91
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EquationArrayProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D45 RID: 27973
		// (get) Token: 0x0601534A RID: 86858 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D46 RID: 27974
		// (get) Token: 0x0601534B RID: 86859 RVA: 0x0031CD98 File Offset: 0x0031AF98
		// (set) Token: 0x0601534C RID: 86860 RVA: 0x0031CDA1 File Offset: 0x0031AFA1
		public BaseJustification BaseJustification
		{
			get
			{
				return base.GetElement<BaseJustification>(0);
			}
			set
			{
				base.SetElement<BaseJustification>(0, value);
			}
		}

		// Token: 0x17006D47 RID: 27975
		// (get) Token: 0x0601534D RID: 86861 RVA: 0x0031CDAB File Offset: 0x0031AFAB
		// (set) Token: 0x0601534E RID: 86862 RVA: 0x0031CDB4 File Offset: 0x0031AFB4
		public MaxDistribution MaxDistribution
		{
			get
			{
				return base.GetElement<MaxDistribution>(1);
			}
			set
			{
				base.SetElement<MaxDistribution>(1, value);
			}
		}

		// Token: 0x17006D48 RID: 27976
		// (get) Token: 0x0601534F RID: 86863 RVA: 0x0031CDBE File Offset: 0x0031AFBE
		// (set) Token: 0x06015350 RID: 86864 RVA: 0x0031CDC7 File Offset: 0x0031AFC7
		public ObjectDistribution ObjectDistribution
		{
			get
			{
				return base.GetElement<ObjectDistribution>(2);
			}
			set
			{
				base.SetElement<ObjectDistribution>(2, value);
			}
		}

		// Token: 0x17006D49 RID: 27977
		// (get) Token: 0x06015351 RID: 86865 RVA: 0x0031CDD1 File Offset: 0x0031AFD1
		// (set) Token: 0x06015352 RID: 86866 RVA: 0x0031CDDA File Offset: 0x0031AFDA
		public RowSpacingRule RowSpacingRule
		{
			get
			{
				return base.GetElement<RowSpacingRule>(3);
			}
			set
			{
				base.SetElement<RowSpacingRule>(3, value);
			}
		}

		// Token: 0x17006D4A RID: 27978
		// (get) Token: 0x06015353 RID: 86867 RVA: 0x0031CDE4 File Offset: 0x0031AFE4
		// (set) Token: 0x06015354 RID: 86868 RVA: 0x0031CDED File Offset: 0x0031AFED
		public RowSpacing RowSpacing
		{
			get
			{
				return base.GetElement<RowSpacing>(4);
			}
			set
			{
				base.SetElement<RowSpacing>(4, value);
			}
		}

		// Token: 0x17006D4B RID: 27979
		// (get) Token: 0x06015355 RID: 86869 RVA: 0x0031C65F File Offset: 0x0031A85F
		// (set) Token: 0x06015356 RID: 86870 RVA: 0x0031C668 File Offset: 0x0031A868
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(5);
			}
			set
			{
				base.SetElement<ControlProperties>(5, value);
			}
		}

		// Token: 0x06015357 RID: 86871 RVA: 0x0031CDF7 File Offset: 0x0031AFF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EquationArrayProperties>(deep);
		}

		// Token: 0x04009216 RID: 37398
		private const string tagName = "eqArrPr";

		// Token: 0x04009217 RID: 37399
		private const byte tagNsId = 21;

		// Token: 0x04009218 RID: 37400
		internal const int ElementTypeIdConst = 10900;

		// Token: 0x04009219 RID: 37401
		private static readonly string[] eleTagNames = new string[] { "baseJc", "maxDist", "objDist", "rSpRule", "rSp", "ctrlPr" };

		// Token: 0x0400921A RID: 37402
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
