using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CB RID: 10443
	[GeneratedCode("DomGen", "2.0")]
	internal class Distributor : OpenXmlLeafTextElement
	{
		// Token: 0x17006909 RID: 26889
		// (get) Token: 0x0601494C RID: 84300 RVA: 0x00314C10 File Offset: 0x00312E10
		public override string LocalName
		{
			get
			{
				return "Distributor";
			}
		}

		// Token: 0x1700690A RID: 26890
		// (get) Token: 0x0601494D RID: 84301 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700690B RID: 26891
		// (get) Token: 0x0601494E RID: 84302 RVA: 0x00314C17 File Offset: 0x00312E17
		internal override int ElementTypeId
		{
			get
			{
				return 10797;
			}
		}

		// Token: 0x0601494F RID: 84303 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014950 RID: 84304 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Distributor()
		{
		}

		// Token: 0x06014951 RID: 84305 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Distributor(string text)
			: base(text)
		{
		}

		// Token: 0x06014952 RID: 84306 RVA: 0x00314C20 File Offset: 0x00312E20
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014953 RID: 84307 RVA: 0x00314C3B File Offset: 0x00312E3B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Distributor>(deep);
		}

		// Token: 0x04008EED RID: 36589
		private const string tagName = "Distributor";

		// Token: 0x04008EEE RID: 36590
		private const byte tagNsId = 9;

		// Token: 0x04008EEF RID: 36591
		internal const int ElementTypeIdConst = 10797;
	}
}
