using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A1 RID: 10145
	[ChildElementInfo(typeof(Paragraph))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x17006277 RID: 25207
		// (get) Token: 0x06013A3B RID: 80443 RVA: 0x002DF074 File Offset: 0x002DD274
		public override string LocalName
		{
			get
			{
				return "txBody";
			}
		}

		// Token: 0x17006278 RID: 25208
		// (get) Token: 0x06013A3C RID: 80444 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006279 RID: 25209
		// (get) Token: 0x06013A3D RID: 80445 RVA: 0x0030A2F7 File Offset: 0x003084F7
		internal override int ElementTypeId
		{
			get
			{
				return 10178;
			}
		}

		// Token: 0x06013A3E RID: 80446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013A3F RID: 80447 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x06013A40 RID: 80448 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A41 RID: 80449 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A42 RID: 80450 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013A43 RID: 80451 RVA: 0x0030A300 File Offset: 0x00308500
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

		// Token: 0x1700627A RID: 25210
		// (get) Token: 0x06013A44 RID: 80452 RVA: 0x0030A356 File Offset: 0x00308556
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x1700627B RID: 25211
		// (get) Token: 0x06013A45 RID: 80453 RVA: 0x0030A35D File Offset: 0x0030855D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x1700627C RID: 25212
		// (get) Token: 0x06013A46 RID: 80454 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700627D RID: 25213
		// (get) Token: 0x06013A47 RID: 80455 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x06013A48 RID: 80456 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
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

		// Token: 0x1700627E RID: 25214
		// (get) Token: 0x06013A49 RID: 80457 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x06013A4A RID: 80458 RVA: 0x002DF104 File Offset: 0x002DD304
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

		// Token: 0x06013A4B RID: 80459 RVA: 0x0030A364 File Offset: 0x00308564
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x04008717 RID: 34583
		private const string tagName = "txBody";

		// Token: 0x04008718 RID: 34584
		private const byte tagNsId = 10;

		// Token: 0x04008719 RID: 34585
		internal const int ElementTypeIdConst = 10178;

		// Token: 0x0400871A RID: 34586
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x0400871B RID: 34587
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
