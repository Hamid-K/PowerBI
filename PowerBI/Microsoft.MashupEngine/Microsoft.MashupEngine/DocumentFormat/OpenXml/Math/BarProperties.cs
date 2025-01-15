using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299D RID: 10653
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BarProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006CF3 RID: 27891
		// (get) Token: 0x0601529B RID: 86683 RVA: 0x0031C497 File Offset: 0x0031A697
		public override string LocalName
		{
			get
			{
				return "barPr";
			}
		}

		// Token: 0x17006CF4 RID: 27892
		// (get) Token: 0x0601529C RID: 86684 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CF5 RID: 27893
		// (get) Token: 0x0601529D RID: 86685 RVA: 0x0031C49E File Offset: 0x0031A69E
		internal override int ElementTypeId
		{
			get
			{
				return 10875;
			}
		}

		// Token: 0x0601529E RID: 86686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601529F RID: 86687 RVA: 0x00293ECF File Offset: 0x002920CF
		public BarProperties()
		{
		}

		// Token: 0x060152A0 RID: 86688 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BarProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152A1 RID: 86689 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BarProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152A2 RID: 86690 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BarProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060152A3 RID: 86691 RVA: 0x0031C4A5 File Offset: 0x0031A6A5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006CF6 RID: 27894
		// (get) Token: 0x060152A4 RID: 86692 RVA: 0x0031C4D8 File Offset: 0x0031A6D8
		internal override string[] ElementTagNames
		{
			get
			{
				return BarProperties.eleTagNames;
			}
		}

		// Token: 0x17006CF7 RID: 27895
		// (get) Token: 0x060152A5 RID: 86693 RVA: 0x0031C4DF File Offset: 0x0031A6DF
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BarProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006CF8 RID: 27896
		// (get) Token: 0x060152A6 RID: 86694 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006CF9 RID: 27897
		// (get) Token: 0x060152A7 RID: 86695 RVA: 0x0031C4E6 File Offset: 0x0031A6E6
		// (set) Token: 0x060152A8 RID: 86696 RVA: 0x0031C4EF File Offset: 0x0031A6EF
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(0);
			}
			set
			{
				base.SetElement<Position>(0, value);
			}
		}

		// Token: 0x17006CFA RID: 27898
		// (get) Token: 0x060152A9 RID: 86697 RVA: 0x0031BAC1 File Offset: 0x00319CC1
		// (set) Token: 0x060152AA RID: 86698 RVA: 0x0031BACA File Offset: 0x00319CCA
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

		// Token: 0x060152AB RID: 86699 RVA: 0x0031C4F9 File Offset: 0x0031A6F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarProperties>(deep);
		}

		// Token: 0x040091E8 RID: 37352
		private const string tagName = "barPr";

		// Token: 0x040091E9 RID: 37353
		private const byte tagNsId = 21;

		// Token: 0x040091EA RID: 37354
		internal const int ElementTypeIdConst = 10875;

		// Token: 0x040091EB RID: 37355
		private static readonly string[] eleTagNames = new string[] { "pos", "ctrlPr" };

		// Token: 0x040091EC RID: 37356
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
