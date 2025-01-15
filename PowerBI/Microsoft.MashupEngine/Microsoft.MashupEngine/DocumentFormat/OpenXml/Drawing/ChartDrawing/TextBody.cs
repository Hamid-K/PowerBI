using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002633 RID: 9779
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	[ChildElementInfo(typeof(Paragraph))]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x17005A6E RID: 23150
		// (get) Token: 0x06012820 RID: 75808 RVA: 0x002DF074 File Offset: 0x002DD274
		public override string LocalName
		{
			get
			{
				return "txBody";
			}
		}

		// Token: 0x17005A6F RID: 23151
		// (get) Token: 0x06012821 RID: 75809 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A70 RID: 23152
		// (get) Token: 0x06012822 RID: 75810 RVA: 0x002FC14C File Offset: 0x002FA34C
		internal override int ElementTypeId
		{
			get
			{
				return 10598;
			}
		}

		// Token: 0x06012823 RID: 75811 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012824 RID: 75812 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x06012825 RID: 75813 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012826 RID: 75814 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012827 RID: 75815 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012828 RID: 75816 RVA: 0x002FC154 File Offset: 0x002FA354
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bodyPr" == name)
			{
				return new BodyProperties();
			}
			if (10 == namespaceId && "lstStyle" == name)
			{
				return new ListStyle();
			}
			if (10 == namespaceId && "p" == name)
			{
				return new Paragraph();
			}
			return null;
		}

		// Token: 0x17005A71 RID: 23153
		// (get) Token: 0x06012829 RID: 75817 RVA: 0x002FC1AA File Offset: 0x002FA3AA
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x17005A72 RID: 23154
		// (get) Token: 0x0601282A RID: 75818 RVA: 0x002FC1B1 File Offset: 0x002FA3B1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x17005A73 RID: 23155
		// (get) Token: 0x0601282B RID: 75819 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A74 RID: 23156
		// (get) Token: 0x0601282C RID: 75820 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x0601282D RID: 75821 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
		public BodyProperties BodyProperties
		{
			get
			{
				return base.GetElement<BodyProperties>(0);
			}
			set
			{
				base.SetElement<BodyProperties>(0, value);
			}
		}

		// Token: 0x17005A75 RID: 23157
		// (get) Token: 0x0601282E RID: 75822 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x0601282F RID: 75823 RVA: 0x002DF104 File Offset: 0x002DD304
		public ListStyle ListStyle
		{
			get
			{
				return base.GetElement<ListStyle>(1);
			}
			set
			{
				base.SetElement<ListStyle>(1, value);
			}
		}

		// Token: 0x06012830 RID: 75824 RVA: 0x002FC1B8 File Offset: 0x002FA3B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x0400806F RID: 32879
		private const string tagName = "txBody";

		// Token: 0x04008070 RID: 32880
		private const byte tagNsId = 12;

		// Token: 0x04008071 RID: 32881
		internal const int ElementTypeIdConst = 10598;

		// Token: 0x04008072 RID: 32882
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x04008073 RID: 32883
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
