using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BD RID: 10429
	[GeneratedCode("DomGen", "2.0")]
	internal class AlbumTitle : OpenXmlLeafTextElement
	{
		// Token: 0x170068DF RID: 26847
		// (get) Token: 0x060148DC RID: 84188 RVA: 0x00314938 File Offset: 0x00312B38
		public override string LocalName
		{
			get
			{
				return "AlbumTitle";
			}
		}

		// Token: 0x170068E0 RID: 26848
		// (get) Token: 0x060148DD RID: 84189 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068E1 RID: 26849
		// (get) Token: 0x060148DE RID: 84190 RVA: 0x0031493F File Offset: 0x00312B3F
		internal override int ElementTypeId
		{
			get
			{
				return 10782;
			}
		}

		// Token: 0x060148DF RID: 84191 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148E0 RID: 84192 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AlbumTitle()
		{
		}

		// Token: 0x060148E1 RID: 84193 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AlbumTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060148E2 RID: 84194 RVA: 0x00314948 File Offset: 0x00312B48
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148E3 RID: 84195 RVA: 0x00314963 File Offset: 0x00312B63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlbumTitle>(deep);
		}

		// Token: 0x04008EC3 RID: 36547
		private const string tagName = "AlbumTitle";

		// Token: 0x04008EC4 RID: 36548
		private const byte tagNsId = 9;

		// Token: 0x04008EC5 RID: 36549
		internal const int ElementTypeIdConst = 10782;
	}
}
