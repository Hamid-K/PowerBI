using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002923 RID: 10531
	[GeneratedCode("DomGen", "2.0")]
	internal class VTDouble : OpenXmlLeafTextElement
	{
		// Token: 0x17006AC4 RID: 27332
		// (get) Token: 0x06014D8B RID: 85387 RVA: 0x0031804C File Offset: 0x0031624C
		public override string LocalName
		{
			get
			{
				return "r8";
			}
		}

		// Token: 0x17006AC5 RID: 27333
		// (get) Token: 0x06014D8C RID: 85388 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AC6 RID: 27334
		// (get) Token: 0x06014D8D RID: 85389 RVA: 0x00318053 File Offset: 0x00316253
		internal override int ElementTypeId
		{
			get
			{
				return 10981;
			}
		}

		// Token: 0x06014D8E RID: 85390 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D8F RID: 85391 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTDouble()
		{
		}

		// Token: 0x06014D90 RID: 85392 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTDouble(string text)
			: base(text)
		{
		}

		// Token: 0x06014D91 RID: 85393 RVA: 0x0031805C File Offset: 0x0031625C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DoubleValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D92 RID: 85394 RVA: 0x00318077 File Offset: 0x00316277
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTDouble>(deep);
		}

		// Token: 0x0400901B RID: 36891
		private const string tagName = "r8";

		// Token: 0x0400901C RID: 36892
		private const byte tagNsId = 5;

		// Token: 0x0400901D RID: 36893
		internal const int ElementTypeIdConst = 10981;
	}
}
