using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307A RID: 12410
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(Interpretation))]
	[ChildElementInfo(typeof(OneOf))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Sequence))]
	internal class Derivation : OpenXmlCompositeElement
	{
		// Token: 0x1700970B RID: 38667
		// (get) Token: 0x0601AF10 RID: 110352 RVA: 0x00369C77 File Offset: 0x00367E77
		public override string LocalName
		{
			get
			{
				return "derivation";
			}
		}

		// Token: 0x1700970C RID: 38668
		// (get) Token: 0x0601AF11 RID: 110353 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700970D RID: 38669
		// (get) Token: 0x0601AF12 RID: 110354 RVA: 0x00369C7E File Offset: 0x00367E7E
		internal override int ElementTypeId
		{
			get
			{
				return 12679;
			}
		}

		// Token: 0x0601AF13 RID: 110355 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AF14 RID: 110356 RVA: 0x00293ECF File Offset: 0x002920CF
		public Derivation()
		{
		}

		// Token: 0x0601AF15 RID: 110357 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Derivation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF16 RID: 110358 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Derivation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF17 RID: 110359 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Derivation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF18 RID: 110360 RVA: 0x00369C88 File Offset: 0x00367E88
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "interpretation" == name)
			{
				return new Interpretation();
			}
			if (44 == namespaceId && "one-of" == name)
			{
				return new OneOf();
			}
			if (44 == namespaceId && "sequence" == name)
			{
				return new Sequence();
			}
			if (44 == namespaceId && "group" == name)
			{
				return new Group();
			}
			return null;
		}

		// Token: 0x0601AF19 RID: 110361 RVA: 0x00369CF6 File Offset: 0x00367EF6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Derivation>(deep);
		}

		// Token: 0x0400B236 RID: 45622
		private const string tagName = "derivation";

		// Token: 0x0400B237 RID: 45623
		private const byte tagNsId = 44;

		// Token: 0x0400B238 RID: 45624
		internal const int ElementTypeIdConst = 12679;
	}
}
