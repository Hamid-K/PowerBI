using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomXsn
{
	// Token: 0x020022B5 RID: 8885
	[GeneratedCode("DomGen", "2.0")]
	internal class Scope : OpenXmlLeafTextElement
	{
		// Token: 0x17004167 RID: 16743
		// (get) Token: 0x0600F153 RID: 61779 RVA: 0x002D13D0 File Offset: 0x002CF5D0
		public override string LocalName
		{
			get
			{
				return "xsnScope";
			}
		}

		// Token: 0x17004168 RID: 16744
		// (get) Token: 0x0600F154 RID: 61780 RVA: 0x002D1203 File Offset: 0x002CF403
		internal override byte NamespaceId
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x17004169 RID: 16745
		// (get) Token: 0x0600F155 RID: 61781 RVA: 0x002D13D7 File Offset: 0x002CF5D7
		internal override int ElementTypeId
		{
			get
			{
				return 12639;
			}
		}

		// Token: 0x0600F156 RID: 61782 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F157 RID: 61783 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Scope()
		{
		}

		// Token: 0x0600F158 RID: 61784 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Scope(string text)
			: base(text)
		{
		}

		// Token: 0x0600F159 RID: 61785 RVA: 0x002D13E0 File Offset: 0x002CF5E0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F15A RID: 61786 RVA: 0x002D13FB File Offset: 0x002CF5FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scope>(deep);
		}

		// Token: 0x040070BD RID: 28861
		private const string tagName = "xsnScope";

		// Token: 0x040070BE RID: 28862
		private const byte tagNsId = 39;

		// Token: 0x040070BF RID: 28863
		internal const int ElementTypeIdConst = 12639;
	}
}
