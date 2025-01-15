using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002883 RID: 10371
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(Paragraph))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ListStyle))]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x1700674E RID: 26446
		// (get) Token: 0x0601456B RID: 83307 RVA: 0x002DF074 File Offset: 0x002DD274
		public override string LocalName
		{
			get
			{
				return "txBody";
			}
		}

		// Token: 0x1700674F RID: 26447
		// (get) Token: 0x0601456C RID: 83308 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006750 RID: 26448
		// (get) Token: 0x0601456D RID: 83309 RVA: 0x0031248C File Offset: 0x0031068C
		internal override int ElementTypeId
		{
			get
			{
				return 10733;
			}
		}

		// Token: 0x0601456E RID: 83310 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601456F RID: 83311 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x06014570 RID: 83312 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014571 RID: 83313 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014572 RID: 83314 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014573 RID: 83315 RVA: 0x00312494 File Offset: 0x00310694
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

		// Token: 0x17006751 RID: 26449
		// (get) Token: 0x06014574 RID: 83316 RVA: 0x003124EA File Offset: 0x003106EA
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x17006752 RID: 26450
		// (get) Token: 0x06014575 RID: 83317 RVA: 0x003124F1 File Offset: 0x003106F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x17006753 RID: 26451
		// (get) Token: 0x06014576 RID: 83318 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006754 RID: 26452
		// (get) Token: 0x06014577 RID: 83319 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x06014578 RID: 83320 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
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

		// Token: 0x17006755 RID: 26453
		// (get) Token: 0x06014579 RID: 83321 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x0601457A RID: 83322 RVA: 0x002DF104 File Offset: 0x002DD304
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

		// Token: 0x0601457B RID: 83323 RVA: 0x003124F8 File Offset: 0x003106F8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x04008DA4 RID: 36260
		private const string tagName = "txBody";

		// Token: 0x04008DA5 RID: 36261
		private const byte tagNsId = 18;

		// Token: 0x04008DA6 RID: 36262
		internal const int ElementTypeIdConst = 10733;

		// Token: 0x04008DA7 RID: 36263
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x04008DA8 RID: 36264
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
