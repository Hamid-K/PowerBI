using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomXsn
{
	// Token: 0x020022B3 RID: 8883
	[GeneratedCode("DomGen", "2.0")]
	internal class CachedView : OpenXmlLeafTextElement
	{
		// Token: 0x17004161 RID: 16737
		// (get) Token: 0x0600F143 RID: 61763 RVA: 0x002D1368 File Offset: 0x002CF568
		public override string LocalName
		{
			get
			{
				return "cached";
			}
		}

		// Token: 0x17004162 RID: 16738
		// (get) Token: 0x0600F144 RID: 61764 RVA: 0x002D1203 File Offset: 0x002CF403
		internal override byte NamespaceId
		{
			get
			{
				return 39;
			}
		}

		// Token: 0x17004163 RID: 16739
		// (get) Token: 0x0600F145 RID: 61765 RVA: 0x002D136F File Offset: 0x002CF56F
		internal override int ElementTypeId
		{
			get
			{
				return 12637;
			}
		}

		// Token: 0x0600F146 RID: 61766 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F147 RID: 61767 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CachedView()
		{
		}

		// Token: 0x0600F148 RID: 61768 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CachedView(string text)
			: base(text)
		{
		}

		// Token: 0x0600F149 RID: 61769 RVA: 0x002D1378 File Offset: 0x002CF578
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F14A RID: 61770 RVA: 0x002D1393 File Offset: 0x002CF593
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CachedView>(deep);
		}

		// Token: 0x040070B7 RID: 28855
		private const string tagName = "cached";

		// Token: 0x040070B8 RID: 28856
		private const byte tagNsId = 39;

		// Token: 0x040070B9 RID: 28857
		internal const int ElementTypeIdConst = 12637;
	}
}
