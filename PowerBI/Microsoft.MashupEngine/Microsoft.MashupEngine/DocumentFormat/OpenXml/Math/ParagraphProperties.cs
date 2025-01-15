using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029D3 RID: 10707
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Justification))]
	internal class ParagraphProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006E35 RID: 28213
		// (get) Token: 0x06015556 RID: 87382 RVA: 0x0031E0C4 File Offset: 0x0031C2C4
		public override string LocalName
		{
			get
			{
				return "oMathParaPr";
			}
		}

		// Token: 0x17006E36 RID: 28214
		// (get) Token: 0x06015557 RID: 87383 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E37 RID: 28215
		// (get) Token: 0x06015558 RID: 87384 RVA: 0x0031E0CB File Offset: 0x0031C2CB
		internal override int ElementTypeId
		{
			get
			{
				return 10962;
			}
		}

		// Token: 0x06015559 RID: 87385 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601555A RID: 87386 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphProperties()
		{
		}

		// Token: 0x0601555B RID: 87387 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601555C RID: 87388 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601555D RID: 87389 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601555E RID: 87390 RVA: 0x0031E0D2 File Offset: 0x0031C2D2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "jc" == name)
			{
				return new Justification();
			}
			return null;
		}

		// Token: 0x17006E38 RID: 28216
		// (get) Token: 0x0601555F RID: 87391 RVA: 0x0031E0ED File Offset: 0x0031C2ED
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphProperties.eleTagNames;
			}
		}

		// Token: 0x17006E39 RID: 28217
		// (get) Token: 0x06015560 RID: 87392 RVA: 0x0031E0F4 File Offset: 0x0031C2F4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006E3A RID: 28218
		// (get) Token: 0x06015561 RID: 87393 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006E3B RID: 28219
		// (get) Token: 0x06015562 RID: 87394 RVA: 0x0031E0FB File Offset: 0x0031C2FB
		// (set) Token: 0x06015563 RID: 87395 RVA: 0x0031E104 File Offset: 0x0031C304
		public Justification Justification
		{
			get
			{
				return base.GetElement<Justification>(0);
			}
			set
			{
				base.SetElement<Justification>(0, value);
			}
		}

		// Token: 0x06015564 RID: 87396 RVA: 0x0031E10E File Offset: 0x0031C30E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphProperties>(deep);
		}

		// Token: 0x040092C1 RID: 37569
		private const string tagName = "oMathParaPr";

		// Token: 0x040092C2 RID: 37570
		private const byte tagNsId = 21;

		// Token: 0x040092C3 RID: 37571
		internal const int ElementTypeIdConst = 10962;

		// Token: 0x040092C4 RID: 37572
		private static readonly string[] eleTagNames = new string[] { "jc" };

		// Token: 0x040092C5 RID: 37573
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
