using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BF RID: 10431
	[GeneratedCode("DomGen", "2.0")]
	internal class Broadcaster : OpenXmlLeafTextElement
	{
		// Token: 0x170068E5 RID: 26853
		// (get) Token: 0x060148EC RID: 84204 RVA: 0x003149A0 File Offset: 0x00312BA0
		public override string LocalName
		{
			get
			{
				return "Broadcaster";
			}
		}

		// Token: 0x170068E6 RID: 26854
		// (get) Token: 0x060148ED RID: 84205 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068E7 RID: 26855
		// (get) Token: 0x060148EE RID: 84206 RVA: 0x003149A7 File Offset: 0x00312BA7
		internal override int ElementTypeId
		{
			get
			{
				return 10785;
			}
		}

		// Token: 0x060148EF RID: 84207 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148F0 RID: 84208 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Broadcaster()
		{
		}

		// Token: 0x060148F1 RID: 84209 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Broadcaster(string text)
			: base(text)
		{
		}

		// Token: 0x060148F2 RID: 84210 RVA: 0x003149B0 File Offset: 0x00312BB0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148F3 RID: 84211 RVA: 0x003149CB File Offset: 0x00312BCB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Broadcaster>(deep);
		}

		// Token: 0x04008EC9 RID: 36553
		private const string tagName = "Broadcaster";

		// Token: 0x04008ECA RID: 36554
		private const byte tagNsId = 9;

		// Token: 0x04008ECB RID: 36555
		internal const int ElementTypeIdConst = 10785;
	}
}
