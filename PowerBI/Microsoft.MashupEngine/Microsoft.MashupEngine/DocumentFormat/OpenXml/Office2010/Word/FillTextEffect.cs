using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A7 RID: 9383
	[ChildElementInfo(typeof(SolidColorFillProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NoFillEmpty), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GradientFillProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class FillTextEffect : OpenXmlCompositeElement
	{
		// Token: 0x17005213 RID: 21011
		// (get) Token: 0x060115A5 RID: 71077 RVA: 0x002ED954 File Offset: 0x002EBB54
		public override string LocalName
		{
			get
			{
				return "textFill";
			}
		}

		// Token: 0x17005214 RID: 21012
		// (get) Token: 0x060115A6 RID: 71078 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005215 RID: 21013
		// (get) Token: 0x060115A7 RID: 71079 RVA: 0x002ED95B File Offset: 0x002EBB5B
		internal override int ElementTypeId
		{
			get
			{
				return 12857;
			}
		}

		// Token: 0x060115A8 RID: 71080 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060115A9 RID: 71081 RVA: 0x00293ECF File Offset: 0x002920CF
		public FillTextEffect()
		{
		}

		// Token: 0x060115AA RID: 71082 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FillTextEffect(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115AB RID: 71083 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FillTextEffect(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060115AC RID: 71084 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FillTextEffect(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060115AD RID: 71085 RVA: 0x002ED964 File Offset: 0x002EBB64
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "noFill" == name)
			{
				return new NoFillEmpty();
			}
			if (52 == namespaceId && "solidFill" == name)
			{
				return new SolidColorFillProperties();
			}
			if (52 == namespaceId && "gradFill" == name)
			{
				return new GradientFillProperties();
			}
			return null;
		}

		// Token: 0x17005216 RID: 21014
		// (get) Token: 0x060115AE RID: 71086 RVA: 0x002ED9BA File Offset: 0x002EBBBA
		internal override string[] ElementTagNames
		{
			get
			{
				return FillTextEffect.eleTagNames;
			}
		}

		// Token: 0x17005217 RID: 21015
		// (get) Token: 0x060115AF RID: 71087 RVA: 0x002ED9C1 File Offset: 0x002EBBC1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FillTextEffect.eleNamespaceIds;
			}
		}

		// Token: 0x17005218 RID: 21016
		// (get) Token: 0x060115B0 RID: 71088 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005219 RID: 21017
		// (get) Token: 0x060115B1 RID: 71089 RVA: 0x002ED9C8 File Offset: 0x002EBBC8
		// (set) Token: 0x060115B2 RID: 71090 RVA: 0x002ED9D1 File Offset: 0x002EBBD1
		public NoFillEmpty NoFillEmpty
		{
			get
			{
				return base.GetElement<NoFillEmpty>(0);
			}
			set
			{
				base.SetElement<NoFillEmpty>(0, value);
			}
		}

		// Token: 0x1700521A RID: 21018
		// (get) Token: 0x060115B3 RID: 71091 RVA: 0x002ED9DB File Offset: 0x002EBBDB
		// (set) Token: 0x060115B4 RID: 71092 RVA: 0x002ED9E4 File Offset: 0x002EBBE4
		public SolidColorFillProperties SolidColorFillProperties
		{
			get
			{
				return base.GetElement<SolidColorFillProperties>(1);
			}
			set
			{
				base.SetElement<SolidColorFillProperties>(1, value);
			}
		}

		// Token: 0x1700521B RID: 21019
		// (get) Token: 0x060115B5 RID: 71093 RVA: 0x002ED9EE File Offset: 0x002EBBEE
		// (set) Token: 0x060115B6 RID: 71094 RVA: 0x002ED9F7 File Offset: 0x002EBBF7
		public GradientFillProperties GradientFillProperties
		{
			get
			{
				return base.GetElement<GradientFillProperties>(2);
			}
			set
			{
				base.SetElement<GradientFillProperties>(2, value);
			}
		}

		// Token: 0x060115B7 RID: 71095 RVA: 0x002EDA01 File Offset: 0x002EBC01
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillTextEffect>(deep);
		}

		// Token: 0x04007969 RID: 31081
		private const string tagName = "textFill";

		// Token: 0x0400796A RID: 31082
		private const byte tagNsId = 52;

		// Token: 0x0400796B RID: 31083
		internal const int ElementTypeIdConst = 12857;

		// Token: 0x0400796C RID: 31084
		private static readonly string[] eleTagNames = new string[] { "noFill", "solidFill", "gradFill" };

		// Token: 0x0400796D RID: 31085
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
