using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002914 RID: 10516
	[GeneratedCode("DomGen", "2.0")]
	internal class VTStorage : OpenXmlLeafTextElement
	{
		// Token: 0x17006A97 RID: 27287
		// (get) Token: 0x06014D17 RID: 85271 RVA: 0x00317D80 File Offset: 0x00315F80
		public override string LocalName
		{
			get
			{
				return "storage";
			}
		}

		// Token: 0x17006A98 RID: 27288
		// (get) Token: 0x06014D18 RID: 85272 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A99 RID: 27289
		// (get) Token: 0x06014D19 RID: 85273 RVA: 0x00317D87 File Offset: 0x00315F87
		internal override int ElementTypeId
		{
			get
			{
				return 10993;
			}
		}

		// Token: 0x06014D1A RID: 85274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D1B RID: 85275 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTStorage()
		{
		}

		// Token: 0x06014D1C RID: 85276 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTStorage(string text)
			: base(text)
		{
		}

		// Token: 0x06014D1D RID: 85277 RVA: 0x00317D90 File Offset: 0x00315F90
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D1E RID: 85278 RVA: 0x00317DAB File Offset: 0x00315FAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTStorage>(deep);
		}

		// Token: 0x04008FEE RID: 36846
		private const string tagName = "storage";

		// Token: 0x04008FEF RID: 36847
		private const byte tagNsId = 5;

		// Token: 0x04008FF0 RID: 36848
		internal const int ElementTypeIdConst = 10993;
	}
}
