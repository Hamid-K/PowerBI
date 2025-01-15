using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A71 RID: 10865
	[ChildElementInfo(typeof(DefaultParagraphProperties))]
	[ChildElementInfo(typeof(Level4ParagraphProperties))]
	[ChildElementInfo(typeof(Level5ParagraphProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Level1ParagraphProperties))]
	[ChildElementInfo(typeof(Level2ParagraphProperties))]
	[ChildElementInfo(typeof(Level3ParagraphProperties))]
	[ChildElementInfo(typeof(Level6ParagraphProperties))]
	[ChildElementInfo(typeof(Level7ParagraphProperties))]
	[ChildElementInfo(typeof(Level8ParagraphProperties))]
	[ChildElementInfo(typeof(Level9ParagraphProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class TextListStyleType : OpenXmlCompositeElement
	{
		// Token: 0x06015FDE RID: 90078 RVA: 0x00325870 File Offset: 0x00323A70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "defPPr" == name)
			{
				return new DefaultParagraphProperties();
			}
			if (10 == namespaceId && "lvl1pPr" == name)
			{
				return new Level1ParagraphProperties();
			}
			if (10 == namespaceId && "lvl2pPr" == name)
			{
				return new Level2ParagraphProperties();
			}
			if (10 == namespaceId && "lvl3pPr" == name)
			{
				return new Level3ParagraphProperties();
			}
			if (10 == namespaceId && "lvl4pPr" == name)
			{
				return new Level4ParagraphProperties();
			}
			if (10 == namespaceId && "lvl5pPr" == name)
			{
				return new Level5ParagraphProperties();
			}
			if (10 == namespaceId && "lvl6pPr" == name)
			{
				return new Level6ParagraphProperties();
			}
			if (10 == namespaceId && "lvl7pPr" == name)
			{
				return new Level7ParagraphProperties();
			}
			if (10 == namespaceId && "lvl8pPr" == name)
			{
				return new Level8ParagraphProperties();
			}
			if (10 == namespaceId && "lvl9pPr" == name)
			{
				return new Level9ParagraphProperties();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170072FF RID: 29439
		// (get) Token: 0x06015FDF RID: 90079 RVA: 0x00325986 File Offset: 0x00323B86
		internal override string[] ElementTagNames
		{
			get
			{
				return TextListStyleType.eleTagNames;
			}
		}

		// Token: 0x17007300 RID: 29440
		// (get) Token: 0x06015FE0 RID: 90080 RVA: 0x0032598D File Offset: 0x00323B8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextListStyleType.eleNamespaceIds;
			}
		}

		// Token: 0x17007301 RID: 29441
		// (get) Token: 0x06015FE1 RID: 90081 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007302 RID: 29442
		// (get) Token: 0x06015FE2 RID: 90082 RVA: 0x0030CBBC File Offset: 0x0030ADBC
		// (set) Token: 0x06015FE3 RID: 90083 RVA: 0x0030CBC5 File Offset: 0x0030ADC5
		public DefaultParagraphProperties DefaultParagraphProperties
		{
			get
			{
				return base.GetElement<DefaultParagraphProperties>(0);
			}
			set
			{
				base.SetElement<DefaultParagraphProperties>(0, value);
			}
		}

		// Token: 0x17007303 RID: 29443
		// (get) Token: 0x06015FE4 RID: 90084 RVA: 0x0030CBCF File Offset: 0x0030ADCF
		// (set) Token: 0x06015FE5 RID: 90085 RVA: 0x0030CBD8 File Offset: 0x0030ADD8
		public Level1ParagraphProperties Level1ParagraphProperties
		{
			get
			{
				return base.GetElement<Level1ParagraphProperties>(1);
			}
			set
			{
				base.SetElement<Level1ParagraphProperties>(1, value);
			}
		}

		// Token: 0x17007304 RID: 29444
		// (get) Token: 0x06015FE6 RID: 90086 RVA: 0x0030CBE2 File Offset: 0x0030ADE2
		// (set) Token: 0x06015FE7 RID: 90087 RVA: 0x0030CBEB File Offset: 0x0030ADEB
		public Level2ParagraphProperties Level2ParagraphProperties
		{
			get
			{
				return base.GetElement<Level2ParagraphProperties>(2);
			}
			set
			{
				base.SetElement<Level2ParagraphProperties>(2, value);
			}
		}

		// Token: 0x17007305 RID: 29445
		// (get) Token: 0x06015FE8 RID: 90088 RVA: 0x0030CBF5 File Offset: 0x0030ADF5
		// (set) Token: 0x06015FE9 RID: 90089 RVA: 0x0030CBFE File Offset: 0x0030ADFE
		public Level3ParagraphProperties Level3ParagraphProperties
		{
			get
			{
				return base.GetElement<Level3ParagraphProperties>(3);
			}
			set
			{
				base.SetElement<Level3ParagraphProperties>(3, value);
			}
		}

		// Token: 0x17007306 RID: 29446
		// (get) Token: 0x06015FEA RID: 90090 RVA: 0x0030CC08 File Offset: 0x0030AE08
		// (set) Token: 0x06015FEB RID: 90091 RVA: 0x0030CC11 File Offset: 0x0030AE11
		public Level4ParagraphProperties Level4ParagraphProperties
		{
			get
			{
				return base.GetElement<Level4ParagraphProperties>(4);
			}
			set
			{
				base.SetElement<Level4ParagraphProperties>(4, value);
			}
		}

		// Token: 0x17007307 RID: 29447
		// (get) Token: 0x06015FEC RID: 90092 RVA: 0x0030CC1B File Offset: 0x0030AE1B
		// (set) Token: 0x06015FED RID: 90093 RVA: 0x0030CC24 File Offset: 0x0030AE24
		public Level5ParagraphProperties Level5ParagraphProperties
		{
			get
			{
				return base.GetElement<Level5ParagraphProperties>(5);
			}
			set
			{
				base.SetElement<Level5ParagraphProperties>(5, value);
			}
		}

		// Token: 0x17007308 RID: 29448
		// (get) Token: 0x06015FEE RID: 90094 RVA: 0x0030CC2E File Offset: 0x0030AE2E
		// (set) Token: 0x06015FEF RID: 90095 RVA: 0x0030CC37 File Offset: 0x0030AE37
		public Level6ParagraphProperties Level6ParagraphProperties
		{
			get
			{
				return base.GetElement<Level6ParagraphProperties>(6);
			}
			set
			{
				base.SetElement<Level6ParagraphProperties>(6, value);
			}
		}

		// Token: 0x17007309 RID: 29449
		// (get) Token: 0x06015FF0 RID: 90096 RVA: 0x0030CC41 File Offset: 0x0030AE41
		// (set) Token: 0x06015FF1 RID: 90097 RVA: 0x0030CC4A File Offset: 0x0030AE4A
		public Level7ParagraphProperties Level7ParagraphProperties
		{
			get
			{
				return base.GetElement<Level7ParagraphProperties>(7);
			}
			set
			{
				base.SetElement<Level7ParagraphProperties>(7, value);
			}
		}

		// Token: 0x1700730A RID: 29450
		// (get) Token: 0x06015FF2 RID: 90098 RVA: 0x0030CC54 File Offset: 0x0030AE54
		// (set) Token: 0x06015FF3 RID: 90099 RVA: 0x0030CC5D File Offset: 0x0030AE5D
		public Level8ParagraphProperties Level8ParagraphProperties
		{
			get
			{
				return base.GetElement<Level8ParagraphProperties>(8);
			}
			set
			{
				base.SetElement<Level8ParagraphProperties>(8, value);
			}
		}

		// Token: 0x1700730B RID: 29451
		// (get) Token: 0x06015FF4 RID: 90100 RVA: 0x0030CC67 File Offset: 0x0030AE67
		// (set) Token: 0x06015FF5 RID: 90101 RVA: 0x0030CC71 File Offset: 0x0030AE71
		public Level9ParagraphProperties Level9ParagraphProperties
		{
			get
			{
				return base.GetElement<Level9ParagraphProperties>(9);
			}
			set
			{
				base.SetElement<Level9ParagraphProperties>(9, value);
			}
		}

		// Token: 0x1700730C RID: 29452
		// (get) Token: 0x06015FF6 RID: 90102 RVA: 0x0030CC7C File Offset: 0x0030AE7C
		// (set) Token: 0x06015FF7 RID: 90103 RVA: 0x0030CC86 File Offset: 0x0030AE86
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(10);
			}
			set
			{
				base.SetElement<ExtensionList>(10, value);
			}
		}

		// Token: 0x06015FF8 RID: 90104 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TextListStyleType()
		{
		}

		// Token: 0x06015FF9 RID: 90105 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TextListStyleType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FFA RID: 90106 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TextListStyleType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FFB RID: 90107 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TextListStyleType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040095B7 RID: 38327
		private static readonly string[] eleTagNames = new string[]
		{
			"defPPr", "lvl1pPr", "lvl2pPr", "lvl3pPr", "lvl4pPr", "lvl5pPr", "lvl6pPr", "lvl7pPr", "lvl8pPr", "lvl9pPr",
			"extLst"
		};

		// Token: 0x040095B8 RID: 38328
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
			10
		};
	}
}
