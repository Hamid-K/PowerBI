using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AE RID: 8878
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowOnOpen : OpenXmlLeafTextElement
	{
		// Token: 0x1700413C RID: 16700
		// (get) Token: 0x0600F0ED RID: 61677 RVA: 0x002D0F94 File Offset: 0x002CF194
		public override string LocalName
		{
			get
			{
				return "showOnOpen";
			}
		}

		// Token: 0x1700413D RID: 16701
		// (get) Token: 0x0600F0EE RID: 61678 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x1700413E RID: 16702
		// (get) Token: 0x0600F0EF RID: 61679 RVA: 0x002D0F9B File Offset: 0x002CF19B
		internal override int ElementTypeId
		{
			get
			{
				return 12631;
			}
		}

		// Token: 0x0600F0F0 RID: 61680 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0F1 RID: 61681 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ShowOnOpen()
		{
		}

		// Token: 0x0600F0F2 RID: 61682 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ShowOnOpen(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0F3 RID: 61683 RVA: 0x002D0FA4 File Offset: 0x002CF1A4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0F4 RID: 61684 RVA: 0x002D0FBF File Offset: 0x002CF1BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowOnOpen>(deep);
		}

		// Token: 0x040070A2 RID: 28834
		private const string tagName = "showOnOpen";

		// Token: 0x040070A3 RID: 28835
		private const byte tagNsId = 37;

		// Token: 0x040070A4 RID: 28836
		internal const int ElementTypeIdConst = 12631;
	}
}
