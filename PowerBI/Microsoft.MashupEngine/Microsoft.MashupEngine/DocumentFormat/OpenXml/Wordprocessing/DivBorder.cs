using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC1 RID: 12225
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	internal class DivBorder : OpenXmlCompositeElement
	{
		// Token: 0x170093DA RID: 37850
		// (get) Token: 0x0601A81B RID: 108571 RVA: 0x003632E8 File Offset: 0x003614E8
		public override string LocalName
		{
			get
			{
				return "divBdr";
			}
		}

		// Token: 0x170093DB RID: 37851
		// (get) Token: 0x0601A81C RID: 108572 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093DC RID: 37852
		// (get) Token: 0x0601A81D RID: 108573 RVA: 0x003632EF File Offset: 0x003614EF
		internal override int ElementTypeId
		{
			get
			{
				return 11933;
			}
		}

		// Token: 0x0601A81E RID: 108574 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A81F RID: 108575 RVA: 0x00293ECF File Offset: 0x002920CF
		public DivBorder()
		{
		}

		// Token: 0x0601A820 RID: 108576 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DivBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A821 RID: 108577 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DivBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A822 RID: 108578 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DivBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A823 RID: 108579 RVA: 0x003632F8 File Offset: 0x003614F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			return null;
		}

		// Token: 0x170093DD RID: 37853
		// (get) Token: 0x0601A824 RID: 108580 RVA: 0x00363366 File Offset: 0x00361566
		internal override string[] ElementTagNames
		{
			get
			{
				return DivBorder.eleTagNames;
			}
		}

		// Token: 0x170093DE RID: 37854
		// (get) Token: 0x0601A825 RID: 108581 RVA: 0x0036336D File Offset: 0x0036156D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DivBorder.eleNamespaceIds;
			}
		}

		// Token: 0x170093DF RID: 37855
		// (get) Token: 0x0601A826 RID: 108582 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170093E0 RID: 37856
		// (get) Token: 0x0601A827 RID: 108583 RVA: 0x00345F0C File Offset: 0x0034410C
		// (set) Token: 0x0601A828 RID: 108584 RVA: 0x00345F15 File Offset: 0x00344115
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(0);
			}
			set
			{
				base.SetElement<TopBorder>(0, value);
			}
		}

		// Token: 0x170093E1 RID: 37857
		// (get) Token: 0x0601A829 RID: 108585 RVA: 0x00345F1F File Offset: 0x0034411F
		// (set) Token: 0x0601A82A RID: 108586 RVA: 0x00345F28 File Offset: 0x00344128
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(1);
			}
			set
			{
				base.SetElement<LeftBorder>(1, value);
			}
		}

		// Token: 0x170093E2 RID: 37858
		// (get) Token: 0x0601A82B RID: 108587 RVA: 0x00345F32 File Offset: 0x00344132
		// (set) Token: 0x0601A82C RID: 108588 RVA: 0x00345F3B File Offset: 0x0034413B
		public BottomBorder BottomBorder
		{
			get
			{
				return base.GetElement<BottomBorder>(2);
			}
			set
			{
				base.SetElement<BottomBorder>(2, value);
			}
		}

		// Token: 0x170093E3 RID: 37859
		// (get) Token: 0x0601A82D RID: 108589 RVA: 0x00345F45 File Offset: 0x00344145
		// (set) Token: 0x0601A82E RID: 108590 RVA: 0x00345F4E File Offset: 0x0034414E
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(3);
			}
			set
			{
				base.SetElement<RightBorder>(3, value);
			}
		}

		// Token: 0x0601A82F RID: 108591 RVA: 0x00363374 File Offset: 0x00361574
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DivBorder>(deep);
		}

		// Token: 0x0400AD46 RID: 44358
		private const string tagName = "divBdr";

		// Token: 0x0400AD47 RID: 44359
		private const byte tagNsId = 23;

		// Token: 0x0400AD48 RID: 44360
		internal const int ElementTypeIdConst = 11933;

		// Token: 0x0400AD49 RID: 44361
		private static readonly string[] eleTagNames = new string[] { "top", "left", "bottom", "right" };

		// Token: 0x0400AD4A RID: 44362
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
