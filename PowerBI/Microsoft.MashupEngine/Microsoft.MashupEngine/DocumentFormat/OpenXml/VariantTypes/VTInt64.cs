using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291C RID: 10524
	[GeneratedCode("DomGen", "2.0")]
	internal class VTInt64 : OpenXmlLeafTextElement
	{
		// Token: 0x17006AAF RID: 27311
		// (get) Token: 0x06014D53 RID: 85331 RVA: 0x00317EE0 File Offset: 0x003160E0
		public override string LocalName
		{
			get
			{
				return "i8";
			}
		}

		// Token: 0x17006AB0 RID: 27312
		// (get) Token: 0x06014D54 RID: 85332 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AB1 RID: 27313
		// (get) Token: 0x06014D55 RID: 85333 RVA: 0x00317EE7 File Offset: 0x003160E7
		internal override int ElementTypeId
		{
			get
			{
				return 10973;
			}
		}

		// Token: 0x06014D56 RID: 85334 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D57 RID: 85335 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTInt64()
		{
		}

		// Token: 0x06014D58 RID: 85336 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTInt64(string text)
			: base(text)
		{
		}

		// Token: 0x06014D59 RID: 85337 RVA: 0x00317EF0 File Offset: 0x003160F0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int64Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D5A RID: 85338 RVA: 0x00317F0B File Offset: 0x0031610B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTInt64>(deep);
		}

		// Token: 0x04009006 RID: 36870
		private const string tagName = "i8";

		// Token: 0x04009007 RID: 36871
		private const byte tagNsId = 5;

		// Token: 0x04009008 RID: 36872
		internal const int ElementTypeIdConst = 10973;
	}
}
