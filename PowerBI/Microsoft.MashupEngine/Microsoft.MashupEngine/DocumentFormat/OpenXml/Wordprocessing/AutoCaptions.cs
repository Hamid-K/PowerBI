using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FCC RID: 12236
	[ChildElementInfo(typeof(AutoCaption))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoCaptions : OpenXmlCompositeElement
	{
		// Token: 0x1700942C RID: 37932
		// (get) Token: 0x0601A8D1 RID: 108753 RVA: 0x00363FE8 File Offset: 0x003621E8
		public override string LocalName
		{
			get
			{
				return "autoCaptions";
			}
		}

		// Token: 0x1700942D RID: 37933
		// (get) Token: 0x0601A8D2 RID: 108754 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700942E RID: 37934
		// (get) Token: 0x0601A8D3 RID: 108755 RVA: 0x00363FEF File Offset: 0x003621EF
		internal override int ElementTypeId
		{
			get
			{
				return 11944;
			}
		}

		// Token: 0x0601A8D4 RID: 108756 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A8D5 RID: 108757 RVA: 0x00293ECF File Offset: 0x002920CF
		public AutoCaptions()
		{
		}

		// Token: 0x0601A8D6 RID: 108758 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AutoCaptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A8D7 RID: 108759 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AutoCaptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A8D8 RID: 108760 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AutoCaptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A8D9 RID: 108761 RVA: 0x00363FF6 File Offset: 0x003621F6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "autoCaption" == name)
			{
				return new AutoCaption();
			}
			return null;
		}

		// Token: 0x0601A8DA RID: 108762 RVA: 0x00364011 File Offset: 0x00362211
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoCaptions>(deep);
		}

		// Token: 0x0400AD78 RID: 44408
		private const string tagName = "autoCaptions";

		// Token: 0x0400AD79 RID: 44409
		private const byte tagNsId = 23;

		// Token: 0x0400AD7A RID: 44410
		internal const int ElementTypeIdConst = 11944;
	}
}
