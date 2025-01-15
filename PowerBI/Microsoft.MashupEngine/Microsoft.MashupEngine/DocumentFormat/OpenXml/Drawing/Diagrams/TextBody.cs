using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002694 RID: 9876
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	[ChildElementInfo(typeof(Paragraph))]
	internal class TextBody : OpenXmlCompositeElement
	{
		// Token: 0x17005D53 RID: 23891
		// (get) Token: 0x06012EC1 RID: 77505 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x17005D54 RID: 23892
		// (get) Token: 0x06012EC2 RID: 77506 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D55 RID: 23893
		// (get) Token: 0x06012EC3 RID: 77507 RVA: 0x00300F71 File Offset: 0x002FF171
		internal override int ElementTypeId
		{
			get
			{
				return 10691;
			}
		}

		// Token: 0x06012EC4 RID: 77508 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012EC5 RID: 77509 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBody()
		{
		}

		// Token: 0x06012EC6 RID: 77510 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EC7 RID: 77511 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EC8 RID: 77512 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EC9 RID: 77513 RVA: 0x00300F78 File Offset: 0x002FF178
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

		// Token: 0x17005D56 RID: 23894
		// (get) Token: 0x06012ECA RID: 77514 RVA: 0x00300FCE File Offset: 0x002FF1CE
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBody.eleTagNames;
			}
		}

		// Token: 0x17005D57 RID: 23895
		// (get) Token: 0x06012ECB RID: 77515 RVA: 0x00300FD5 File Offset: 0x002FF1D5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBody.eleNamespaceIds;
			}
		}

		// Token: 0x17005D58 RID: 23896
		// (get) Token: 0x06012ECC RID: 77516 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D59 RID: 23897
		// (get) Token: 0x06012ECD RID: 77517 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x06012ECE RID: 77518 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
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

		// Token: 0x17005D5A RID: 23898
		// (get) Token: 0x06012ECF RID: 77519 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x06012ED0 RID: 77520 RVA: 0x002DF104 File Offset: 0x002DD304
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

		// Token: 0x06012ED1 RID: 77521 RVA: 0x00300FDC File Offset: 0x002FF1DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBody>(deep);
		}

		// Token: 0x0400822E RID: 33326
		private const string tagName = "t";

		// Token: 0x0400822F RID: 33327
		private const byte tagNsId = 14;

		// Token: 0x04008230 RID: 33328
		internal const int ElementTypeIdConst = 10691;

		// Token: 0x04008231 RID: 33329
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x04008232 RID: 33330
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
