using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E3 RID: 10467
	[GeneratedCode("DomGen", "2.0")]
	internal class Station : OpenXmlLeafTextElement
	{
		// Token: 0x17006951 RID: 26961
		// (get) Token: 0x06014A0C RID: 84492 RVA: 0x003150F0 File Offset: 0x003132F0
		public override string LocalName
		{
			get
			{
				return "Station";
			}
		}

		// Token: 0x17006952 RID: 26962
		// (get) Token: 0x06014A0D RID: 84493 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006953 RID: 26963
		// (get) Token: 0x06014A0E RID: 84494 RVA: 0x003150F7 File Offset: 0x003132F7
		internal override int ElementTypeId
		{
			get
			{
				return 10822;
			}
		}

		// Token: 0x06014A0F RID: 84495 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A10 RID: 84496 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Station()
		{
		}

		// Token: 0x06014A11 RID: 84497 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Station(string text)
			: base(text)
		{
		}

		// Token: 0x06014A12 RID: 84498 RVA: 0x00315100 File Offset: 0x00313300
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A13 RID: 84499 RVA: 0x0031511B File Offset: 0x0031331B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Station>(deep);
		}

		// Token: 0x04008F35 RID: 36661
		private const string tagName = "Station";

		// Token: 0x04008F36 RID: 36662
		private const byte tagNsId = 9;

		// Token: 0x04008F37 RID: 36663
		internal const int ElementTypeIdConst = 10822;
	}
}
