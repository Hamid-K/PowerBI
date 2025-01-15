using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomXsn
{
	// Token: 0x020022B2 RID: 8882
	[GeneratedCode("DomGen", "2.0")]
	internal class XsnLocation : OpenXmlLeafTextElement
	{
		// Token: 0x1700415E RID: 16734
		// (get) Token: 0x0600F13B RID: 61755 RVA: 0x002D1334 File Offset: 0x002CF534
		public override string LocalName
		{
			get
			{
				return "xsnLocation";
			}
		}

		// Token: 0x1700415F RID: 16735
		// (get) Token: 0x0600F13C RID: 61756 RVA: 0x002D1203 File Offset: 0x002CF403
		internal override byte NamespaceId
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x17004160 RID: 16736
		// (get) Token: 0x0600F13D RID: 61757 RVA: 0x002D133B File Offset: 0x002CF53B
		internal override int ElementTypeId
		{
			get
			{
				return 12636;
			}
		}

		// Token: 0x0600F13E RID: 61758 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F13F RID: 61759 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public XsnLocation()
		{
		}

		// Token: 0x0600F140 RID: 61760 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public XsnLocation(string text)
			: base(text)
		{
		}

		// Token: 0x0600F141 RID: 61761 RVA: 0x002D1344 File Offset: 0x002CF544
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F142 RID: 61762 RVA: 0x002D135F File Offset: 0x002CF55F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XsnLocation>(deep);
		}

		// Token: 0x040070B4 RID: 28852
		private const string tagName = "xsnLocation";

		// Token: 0x040070B5 RID: 28853
		private const byte tagNsId = 39;

		// Token: 0x040070B6 RID: 28854
		internal const int ElementTypeIdConst = 12636;
	}
}
