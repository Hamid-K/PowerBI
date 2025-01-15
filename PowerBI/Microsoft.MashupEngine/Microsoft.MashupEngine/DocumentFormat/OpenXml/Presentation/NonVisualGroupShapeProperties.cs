using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA2 RID: 10914
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGroupShapeDrawingProperties))]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties))]
	[ChildElementInfo(typeof(NonVisualDrawingProperties))]
	internal class NonVisualGroupShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700743C RID: 29756
		// (get) Token: 0x060162C4 RID: 90820 RVA: 0x002DF395 File Offset: 0x002DD595
		public override string LocalName
		{
			get
			{
				return "nvGrpSpPr";
			}
		}

		// Token: 0x1700743D RID: 29757
		// (get) Token: 0x060162C5 RID: 90821 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700743E RID: 29758
		// (get) Token: 0x060162C6 RID: 90822 RVA: 0x00327396 File Offset: 0x00325596
		internal override int ElementTypeId
		{
			get
			{
				return 12327;
			}
		}

		// Token: 0x060162C7 RID: 90823 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060162C8 RID: 90824 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGroupShapeProperties()
		{
		}

		// Token: 0x060162C9 RID: 90825 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGroupShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162CA RID: 90826 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGroupShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060162CB RID: 90827 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGroupShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060162CC RID: 90828 RVA: 0x003273A0 File Offset: 0x003255A0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (24 == namespaceId && "cNvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeDrawingProperties();
			}
			if (24 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			return null;
		}

		// Token: 0x1700743F RID: 29759
		// (get) Token: 0x060162CD RID: 90829 RVA: 0x003273F6 File Offset: 0x003255F6
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGroupShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17007440 RID: 29760
		// (get) Token: 0x060162CE RID: 90830 RVA: 0x003273FD File Offset: 0x003255FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGroupShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17007441 RID: 29761
		// (get) Token: 0x060162CF RID: 90831 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007442 RID: 29762
		// (get) Token: 0x060162D0 RID: 90832 RVA: 0x00324478 File Offset: 0x00322678
		// (set) Token: 0x060162D1 RID: 90833 RVA: 0x00324481 File Offset: 0x00322681
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x17007443 RID: 29763
		// (get) Token: 0x060162D2 RID: 90834 RVA: 0x00327404 File Offset: 0x00325604
		// (set) Token: 0x060162D3 RID: 90835 RVA: 0x0032740D File Offset: 0x0032560D
		public NonVisualGroupShapeDrawingProperties NonVisualGroupShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeDrawingProperties>(1);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeDrawingProperties>(1, value);
			}
		}

		// Token: 0x17007444 RID: 29764
		// (get) Token: 0x060162D4 RID: 90836 RVA: 0x0032449E File Offset: 0x0032269E
		// (set) Token: 0x060162D5 RID: 90837 RVA: 0x003244A7 File Offset: 0x003226A7
		public ApplicationNonVisualDrawingProperties ApplicationNonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<ApplicationNonVisualDrawingProperties>(2);
			}
			set
			{
				base.SetElement<ApplicationNonVisualDrawingProperties>(2, value);
			}
		}

		// Token: 0x060162D6 RID: 90838 RVA: 0x00327417 File Offset: 0x00325617
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGroupShapeProperties>(deep);
		}

		// Token: 0x0400968C RID: 38540
		private const string tagName = "nvGrpSpPr";

		// Token: 0x0400968D RID: 38541
		private const byte tagNsId = 24;

		// Token: 0x0400968E RID: 38542
		internal const int ElementTypeIdConst = 12327;

		// Token: 0x0400968F RID: 38543
		private static readonly string[] eleTagNames = new string[] { "cNvPr", "cNvGrpSpPr", "nvPr" };

		// Token: 0x04009690 RID: 38544
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24 };
	}
}
