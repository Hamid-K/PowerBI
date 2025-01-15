using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002547 RID: 9543
	[ChildElementInfo(typeof(StringReference))]
	[ChildElementInfo(typeof(NumericValue))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SeriesText : OpenXmlCompositeElement
	{
		// Token: 0x170054FA RID: 21754
		// (get) Token: 0x06011C0F RID: 72719 RVA: 0x002F16DD File Offset: 0x002EF8DD
		public override string LocalName
		{
			get
			{
				return "tx";
			}
		}

		// Token: 0x170054FB RID: 21755
		// (get) Token: 0x06011C10 RID: 72720 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054FC RID: 21756
		// (get) Token: 0x06011C11 RID: 72721 RVA: 0x002F19F7 File Offset: 0x002EFBF7
		internal override int ElementTypeId
		{
			get
			{
				return 10359;
			}
		}

		// Token: 0x06011C12 RID: 72722 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011C13 RID: 72723 RVA: 0x00293ECF File Offset: 0x002920CF
		public SeriesText()
		{
		}

		// Token: 0x06011C14 RID: 72724 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SeriesText(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C15 RID: 72725 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SeriesText(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011C16 RID: 72726 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SeriesText(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011C17 RID: 72727 RVA: 0x002F19FE File Offset: 0x002EFBFE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "strRef" == name)
			{
				return new StringReference();
			}
			if (11 == namespaceId && "v" == name)
			{
				return new NumericValue();
			}
			return null;
		}

		// Token: 0x170054FD RID: 21757
		// (get) Token: 0x06011C18 RID: 72728 RVA: 0x002F1A31 File Offset: 0x002EFC31
		internal override string[] ElementTagNames
		{
			get
			{
				return SeriesText.eleTagNames;
			}
		}

		// Token: 0x170054FE RID: 21758
		// (get) Token: 0x06011C19 RID: 72729 RVA: 0x002F1A38 File Offset: 0x002EFC38
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SeriesText.eleNamespaceIds;
			}
		}

		// Token: 0x170054FF RID: 21759
		// (get) Token: 0x06011C1A RID: 72730 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005500 RID: 21760
		// (get) Token: 0x06011C1B RID: 72731 RVA: 0x002F1750 File Offset: 0x002EF950
		// (set) Token: 0x06011C1C RID: 72732 RVA: 0x002F1759 File Offset: 0x002EF959
		public StringReference StringReference
		{
			get
			{
				return base.GetElement<StringReference>(0);
			}
			set
			{
				base.SetElement<StringReference>(0, value);
			}
		}

		// Token: 0x17005501 RID: 21761
		// (get) Token: 0x06011C1D RID: 72733 RVA: 0x002F1A3F File Offset: 0x002EFC3F
		// (set) Token: 0x06011C1E RID: 72734 RVA: 0x002F1A48 File Offset: 0x002EFC48
		public NumericValue NumericValue
		{
			get
			{
				return base.GetElement<NumericValue>(1);
			}
			set
			{
				base.SetElement<NumericValue>(1, value);
			}
		}

		// Token: 0x06011C1F RID: 72735 RVA: 0x002F1A52 File Offset: 0x002EFC52
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeriesText>(deep);
		}

		// Token: 0x04007C6B RID: 31851
		private const string tagName = "tx";

		// Token: 0x04007C6C RID: 31852
		private const byte tagNsId = 11;

		// Token: 0x04007C6D RID: 31853
		internal const int ElementTypeIdConst = 10359;

		// Token: 0x04007C6E RID: 31854
		private static readonly string[] eleTagNames = new string[] { "strRef", "v" };

		// Token: 0x04007C6F RID: 31855
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11 };
	}
}
