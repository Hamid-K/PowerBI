using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D0 RID: 10448
	[GeneratedCode("DomGen", "2.0")]
	internal class Issue : OpenXmlLeafTextElement
	{
		// Token: 0x17006918 RID: 26904
		// (get) Token: 0x06014974 RID: 84340 RVA: 0x00314D14 File Offset: 0x00312F14
		public override string LocalName
		{
			get
			{
				return "Issue";
			}
		}

		// Token: 0x17006919 RID: 26905
		// (get) Token: 0x06014975 RID: 84341 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700691A RID: 26906
		// (get) Token: 0x06014976 RID: 84342 RVA: 0x00314D1B File Offset: 0x00312F1B
		internal override int ElementTypeId
		{
			get
			{
				return 10802;
			}
		}

		// Token: 0x06014977 RID: 84343 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014978 RID: 84344 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Issue()
		{
		}

		// Token: 0x06014979 RID: 84345 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Issue(string text)
			: base(text)
		{
		}

		// Token: 0x0601497A RID: 84346 RVA: 0x00314D24 File Offset: 0x00312F24
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601497B RID: 84347 RVA: 0x00314D3F File Offset: 0x00312F3F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Issue>(deep);
		}

		// Token: 0x04008EFC RID: 36604
		private const string tagName = "Issue";

		// Token: 0x04008EFD RID: 36605
		private const byte tagNsId = 9;

		// Token: 0x04008EFE RID: 36606
		internal const int ElementTypeIdConst = 10802;
	}
}
