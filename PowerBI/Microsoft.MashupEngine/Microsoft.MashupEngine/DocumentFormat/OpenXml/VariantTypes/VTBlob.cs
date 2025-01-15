using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002910 RID: 10512
	[GeneratedCode("DomGen", "2.0")]
	internal class VTBlob : OpenXmlLeafTextElement
	{
		// Token: 0x17006A8B RID: 27275
		// (get) Token: 0x06014CF7 RID: 85239 RVA: 0x00317CAF File Offset: 0x00315EAF
		public override string LocalName
		{
			get
			{
				return "blob";
			}
		}

		// Token: 0x17006A8C RID: 27276
		// (get) Token: 0x06014CF8 RID: 85240 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A8D RID: 27277
		// (get) Token: 0x06014CF9 RID: 85241 RVA: 0x00317CB6 File Offset: 0x00315EB6
		internal override int ElementTypeId
		{
			get
			{
				return 10966;
			}
		}

		// Token: 0x06014CFA RID: 85242 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014CFB RID: 85243 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTBlob()
		{
		}

		// Token: 0x06014CFC RID: 85244 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTBlob(string text)
			: base(text)
		{
		}

		// Token: 0x06014CFD RID: 85245 RVA: 0x00317CC0 File Offset: 0x00315EC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014CFE RID: 85246 RVA: 0x00317CDB File Offset: 0x00315EDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTBlob>(deep);
		}

		// Token: 0x04008FE2 RID: 36834
		private const string tagName = "blob";

		// Token: 0x04008FE3 RID: 36835
		private const byte tagNsId = 5;

		// Token: 0x04008FE4 RID: 36836
		internal const int ElementTypeIdConst = 10966;
	}
}
