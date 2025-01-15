using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7E RID: 11902
	[ChildElementInfo(typeof(Anchor))]
	[ChildElementInfo(typeof(Inline))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Drawing : OpenXmlCompositeElement
	{
		// Token: 0x17008AD9 RID: 35545
		// (get) Token: 0x0601949F RID: 103583 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		public override string LocalName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x17008ADA RID: 35546
		// (get) Token: 0x060194A0 RID: 103584 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008ADB RID: 35547
		// (get) Token: 0x060194A1 RID: 103585 RVA: 0x00348437 File Offset: 0x00346637
		internal override int ElementTypeId
		{
			get
			{
				return 11572;
			}
		}

		// Token: 0x060194A2 RID: 103586 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060194A3 RID: 103587 RVA: 0x00293ECF File Offset: 0x002920CF
		public Drawing()
		{
		}

		// Token: 0x060194A4 RID: 103588 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Drawing(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060194A5 RID: 103589 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Drawing(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060194A6 RID: 103590 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Drawing(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060194A7 RID: 103591 RVA: 0x0034843E File Offset: 0x0034663E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "anchor" == name)
			{
				return new Anchor();
			}
			if (16 == namespaceId && "inline" == name)
			{
				return new Inline();
			}
			return null;
		}

		// Token: 0x17008ADC RID: 35548
		// (get) Token: 0x060194A8 RID: 103592 RVA: 0x00348471 File Offset: 0x00346671
		internal override string[] ElementTagNames
		{
			get
			{
				return Drawing.eleTagNames;
			}
		}

		// Token: 0x17008ADD RID: 35549
		// (get) Token: 0x060194A9 RID: 103593 RVA: 0x00348478 File Offset: 0x00346678
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Drawing.eleNamespaceIds;
			}
		}

		// Token: 0x17008ADE RID: 35550
		// (get) Token: 0x060194AA RID: 103594 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17008ADF RID: 35551
		// (get) Token: 0x060194AB RID: 103595 RVA: 0x0034847F File Offset: 0x0034667F
		// (set) Token: 0x060194AC RID: 103596 RVA: 0x00348488 File Offset: 0x00346688
		public Anchor Anchor
		{
			get
			{
				return base.GetElement<Anchor>(0);
			}
			set
			{
				base.SetElement<Anchor>(0, value);
			}
		}

		// Token: 0x17008AE0 RID: 35552
		// (get) Token: 0x060194AD RID: 103597 RVA: 0x00348492 File Offset: 0x00346692
		// (set) Token: 0x060194AE RID: 103598 RVA: 0x0034849B File Offset: 0x0034669B
		public Inline Inline
		{
			get
			{
				return base.GetElement<Inline>(1);
			}
			set
			{
				base.SetElement<Inline>(1, value);
			}
		}

		// Token: 0x060194AF RID: 103599 RVA: 0x003484A5 File Offset: 0x003466A5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Drawing>(deep);
		}

		// Token: 0x0400A81D RID: 43037
		private const string tagName = "drawing";

		// Token: 0x0400A81E RID: 43038
		private const byte tagNsId = 23;

		// Token: 0x0400A81F RID: 43039
		internal const int ElementTypeIdConst = 11572;

		// Token: 0x0400A820 RID: 43040
		private static readonly string[] eleTagNames = new string[] { "anchor", "inline" };

		// Token: 0x0400A821 RID: 43041
		private static readonly byte[] eleNamespaceIds = new byte[] { 16, 16 };
	}
}
