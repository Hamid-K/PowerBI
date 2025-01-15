using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C0 RID: 10432
	[GeneratedCode("DomGen", "2.0")]
	internal class BroadcastTitle : OpenXmlLeafTextElement
	{
		// Token: 0x170068E8 RID: 26856
		// (get) Token: 0x060148F4 RID: 84212 RVA: 0x003149D4 File Offset: 0x00312BD4
		public override string LocalName
		{
			get
			{
				return "BroadcastTitle";
			}
		}

		// Token: 0x170068E9 RID: 26857
		// (get) Token: 0x060148F5 RID: 84213 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068EA RID: 26858
		// (get) Token: 0x060148F6 RID: 84214 RVA: 0x003149DB File Offset: 0x00312BDB
		internal override int ElementTypeId
		{
			get
			{
				return 10786;
			}
		}

		// Token: 0x060148F7 RID: 84215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148F8 RID: 84216 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public BroadcastTitle()
		{
		}

		// Token: 0x060148F9 RID: 84217 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public BroadcastTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060148FA RID: 84218 RVA: 0x003149E4 File Offset: 0x00312BE4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148FB RID: 84219 RVA: 0x003149FF File Offset: 0x00312BFF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BroadcastTitle>(deep);
		}

		// Token: 0x04008ECC RID: 36556
		private const string tagName = "BroadcastTitle";

		// Token: 0x04008ECD RID: 36557
		private const byte tagNsId = 9;

		// Token: 0x04008ECE RID: 36558
		internal const int ElementTypeIdConst = 10786;
	}
}
