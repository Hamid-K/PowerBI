using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028B9 RID: 10425
	[GeneratedCode("DomGen", "2.0")]
	internal class First : OpenXmlLeafTextElement
	{
		// Token: 0x170068D3 RID: 26835
		// (get) Token: 0x060148BC RID: 84156 RVA: 0x00314868 File Offset: 0x00312A68
		public override string LocalName
		{
			get
			{
				return "First";
			}
		}

		// Token: 0x170068D4 RID: 26836
		// (get) Token: 0x060148BD RID: 84157 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068D5 RID: 26837
		// (get) Token: 0x060148BE RID: 84158 RVA: 0x0031486F File Offset: 0x00312A6F
		internal override int ElementTypeId
		{
			get
			{
				return 10761;
			}
		}

		// Token: 0x060148BF RID: 84159 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148C0 RID: 84160 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public First()
		{
		}

		// Token: 0x060148C1 RID: 84161 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public First(string text)
			: base(text)
		{
		}

		// Token: 0x060148C2 RID: 84162 RVA: 0x00314878 File Offset: 0x00312A78
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148C3 RID: 84163 RVA: 0x00314893 File Offset: 0x00312A93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<First>(deep);
		}

		// Token: 0x04008EB7 RID: 36535
		private const string tagName = "First";

		// Token: 0x04008EB8 RID: 36536
		private const byte tagNsId = 9;

		// Token: 0x04008EB9 RID: 36537
		internal const int ElementTypeIdConst = 10761;
	}
}
