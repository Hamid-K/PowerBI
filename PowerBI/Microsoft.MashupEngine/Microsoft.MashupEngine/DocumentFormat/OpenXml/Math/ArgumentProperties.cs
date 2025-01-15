using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CC RID: 10700
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ArgumentSize))]
	internal class ArgumentProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006E13 RID: 28179
		// (get) Token: 0x0601550F RID: 87311 RVA: 0x0031DE20 File Offset: 0x0031C020
		public override string LocalName
		{
			get
			{
				return "argPr";
			}
		}

		// Token: 0x17006E14 RID: 28180
		// (get) Token: 0x06015510 RID: 87312 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E15 RID: 28181
		// (get) Token: 0x06015511 RID: 87313 RVA: 0x0031DE27 File Offset: 0x0031C027
		internal override int ElementTypeId
		{
			get
			{
				return 10944;
			}
		}

		// Token: 0x06015512 RID: 87314 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015513 RID: 87315 RVA: 0x00293ECF File Offset: 0x002920CF
		public ArgumentProperties()
		{
		}

		// Token: 0x06015514 RID: 87316 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ArgumentProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015515 RID: 87317 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ArgumentProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015516 RID: 87318 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ArgumentProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015517 RID: 87319 RVA: 0x0031DE2E File Offset: 0x0031C02E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "argSz" == name)
			{
				return new ArgumentSize();
			}
			return null;
		}

		// Token: 0x17006E16 RID: 28182
		// (get) Token: 0x06015518 RID: 87320 RVA: 0x0031DE49 File Offset: 0x0031C049
		internal override string[] ElementTagNames
		{
			get
			{
				return ArgumentProperties.eleTagNames;
			}
		}

		// Token: 0x17006E17 RID: 28183
		// (get) Token: 0x06015519 RID: 87321 RVA: 0x0031DE50 File Offset: 0x0031C050
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ArgumentProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006E18 RID: 28184
		// (get) Token: 0x0601551A RID: 87322 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006E19 RID: 28185
		// (get) Token: 0x0601551B RID: 87323 RVA: 0x0031DE57 File Offset: 0x0031C057
		// (set) Token: 0x0601551C RID: 87324 RVA: 0x0031DE60 File Offset: 0x0031C060
		public ArgumentSize ArgumentSize
		{
			get
			{
				return base.GetElement<ArgumentSize>(0);
			}
			set
			{
				base.SetElement<ArgumentSize>(0, value);
			}
		}

		// Token: 0x0601551D RID: 87325 RVA: 0x0031DE6A File Offset: 0x0031C06A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArgumentProperties>(deep);
		}

		// Token: 0x040092A5 RID: 37541
		private const string tagName = "argPr";

		// Token: 0x040092A6 RID: 37542
		private const byte tagNsId = 21;

		// Token: 0x040092A7 RID: 37543
		internal const int ElementTypeIdConst = 10944;

		// Token: 0x040092A8 RID: 37544
		private static readonly string[] eleTagNames = new string[] { "argSz" };

		// Token: 0x040092A9 RID: 37545
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
