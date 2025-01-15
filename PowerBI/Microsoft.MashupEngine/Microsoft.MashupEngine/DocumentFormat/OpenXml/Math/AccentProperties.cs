using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002990 RID: 10640
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AccentChar))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class AccentProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006CC6 RID: 27846
		// (get) Token: 0x06015223 RID: 86563 RVA: 0x0031BA5F File Offset: 0x00319C5F
		public override string LocalName
		{
			get
			{
				return "accPr";
			}
		}

		// Token: 0x17006CC7 RID: 27847
		// (get) Token: 0x06015224 RID: 86564 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CC8 RID: 27848
		// (get) Token: 0x06015225 RID: 86565 RVA: 0x0031BA66 File Offset: 0x00319C66
		internal override int ElementTypeId
		{
			get
			{
				return 10872;
			}
		}

		// Token: 0x06015226 RID: 86566 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015227 RID: 86567 RVA: 0x00293ECF File Offset: 0x002920CF
		public AccentProperties()
		{
		}

		// Token: 0x06015228 RID: 86568 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AccentProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015229 RID: 86569 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AccentProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601522A RID: 86570 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AccentProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601522B RID: 86571 RVA: 0x0031BA6D File Offset: 0x00319C6D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "chr" == name)
			{
				return new AccentChar();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006CC9 RID: 27849
		// (get) Token: 0x0601522C RID: 86572 RVA: 0x0031BAA0 File Offset: 0x00319CA0
		internal override string[] ElementTagNames
		{
			get
			{
				return AccentProperties.eleTagNames;
			}
		}

		// Token: 0x17006CCA RID: 27850
		// (get) Token: 0x0601522D RID: 86573 RVA: 0x0031BAA7 File Offset: 0x00319CA7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AccentProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006CCB RID: 27851
		// (get) Token: 0x0601522E RID: 86574 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006CCC RID: 27852
		// (get) Token: 0x0601522F RID: 86575 RVA: 0x0031BAAE File Offset: 0x00319CAE
		// (set) Token: 0x06015230 RID: 86576 RVA: 0x0031BAB7 File Offset: 0x00319CB7
		public AccentChar AccentChar
		{
			get
			{
				return base.GetElement<AccentChar>(0);
			}
			set
			{
				base.SetElement<AccentChar>(0, value);
			}
		}

		// Token: 0x17006CCD RID: 27853
		// (get) Token: 0x06015231 RID: 86577 RVA: 0x0031BAC1 File Offset: 0x00319CC1
		// (set) Token: 0x06015232 RID: 86578 RVA: 0x0031BACA File Offset: 0x00319CCA
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

		// Token: 0x06015233 RID: 86579 RVA: 0x0031BAD4 File Offset: 0x00319CD4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AccentProperties>(deep);
		}

		// Token: 0x040091C1 RID: 37313
		private const string tagName = "accPr";

		// Token: 0x040091C2 RID: 37314
		private const byte tagNsId = 21;

		// Token: 0x040091C3 RID: 37315
		internal const int ElementTypeIdConst = 10872;

		// Token: 0x040091C4 RID: 37316
		private static readonly string[] eleTagNames = new string[] { "chr", "ctrlPr" };

		// Token: 0x040091C5 RID: 37317
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
