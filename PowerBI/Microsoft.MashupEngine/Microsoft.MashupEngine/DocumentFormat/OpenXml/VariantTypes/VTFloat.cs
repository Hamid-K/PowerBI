using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002922 RID: 10530
	[GeneratedCode("DomGen", "2.0")]
	internal class VTFloat : OpenXmlLeafTextElement
	{
		// Token: 0x17006AC1 RID: 27329
		// (get) Token: 0x06014D83 RID: 85379 RVA: 0x00318018 File Offset: 0x00316218
		public override string LocalName
		{
			get
			{
				return "r4";
			}
		}

		// Token: 0x17006AC2 RID: 27330
		// (get) Token: 0x06014D84 RID: 85380 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AC3 RID: 27331
		// (get) Token: 0x06014D85 RID: 85381 RVA: 0x0031801F File Offset: 0x0031621F
		internal override int ElementTypeId
		{
			get
			{
				return 10980;
			}
		}

		// Token: 0x06014D86 RID: 85382 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D87 RID: 85383 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTFloat()
		{
		}

		// Token: 0x06014D88 RID: 85384 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTFloat(string text)
			: base(text)
		{
		}

		// Token: 0x06014D89 RID: 85385 RVA: 0x00318028 File Offset: 0x00316228
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new SingleValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D8A RID: 85386 RVA: 0x00318043 File Offset: 0x00316243
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTFloat>(deep);
		}

		// Token: 0x04009018 RID: 36888
		private const string tagName = "r4";

		// Token: 0x04009019 RID: 36889
		private const byte tagNsId = 5;

		// Token: 0x0400901A RID: 36890
		internal const int ElementTypeIdConst = 10980;
	}
}
