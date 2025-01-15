using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002920 RID: 10528
	[GeneratedCode("DomGen", "2.0")]
	internal class VTUnsignedInteger : OpenXmlLeafTextElement
	{
		// Token: 0x17006ABB RID: 27323
		// (get) Token: 0x06014D73 RID: 85363 RVA: 0x00317FB0 File Offset: 0x003161B0
		public override string LocalName
		{
			get
			{
				return "uint";
			}
		}

		// Token: 0x17006ABC RID: 27324
		// (get) Token: 0x06014D74 RID: 85364 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ABD RID: 27325
		// (get) Token: 0x06014D75 RID: 85365 RVA: 0x00317FB7 File Offset: 0x003161B7
		internal override int ElementTypeId
		{
			get
			{
				return 10979;
			}
		}

		// Token: 0x06014D76 RID: 85366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D77 RID: 85367 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTUnsignedInteger()
		{
		}

		// Token: 0x06014D78 RID: 85368 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTUnsignedInteger(string text)
			: base(text)
		{
		}

		// Token: 0x06014D79 RID: 85369 RVA: 0x00317FC0 File Offset: 0x003161C0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new UInt32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D7A RID: 85370 RVA: 0x00317FDB File Offset: 0x003161DB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTUnsignedInteger>(deep);
		}

		// Token: 0x04009012 RID: 36882
		private const string tagName = "uint";

		// Token: 0x04009013 RID: 36883
		private const byte tagNsId = 5;

		// Token: 0x04009014 RID: 36884
		internal const int ElementTypeIdConst = 10979;
	}
}
