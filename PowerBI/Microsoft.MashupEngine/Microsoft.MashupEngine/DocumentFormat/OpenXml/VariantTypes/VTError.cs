using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292C RID: 10540
	[GeneratedCode("DomGen", "2.0")]
	internal class VTError : OpenXmlLeafTextElement
	{
		// Token: 0x17006ADF RID: 27359
		// (get) Token: 0x06014DD3 RID: 85459 RVA: 0x00318220 File Offset: 0x00316420
		public override string LocalName
		{
			get
			{
				return "error";
			}
		}

		// Token: 0x17006AE0 RID: 27360
		// (get) Token: 0x06014DD4 RID: 85460 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AE1 RID: 27361
		// (get) Token: 0x06014DD5 RID: 85461 RVA: 0x00318227 File Offset: 0x00316427
		internal override int ElementTypeId
		{
			get
			{
				return 10990;
			}
		}

		// Token: 0x06014DD6 RID: 85462 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DD7 RID: 85463 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTError()
		{
		}

		// Token: 0x06014DD8 RID: 85464 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTError(string text)
			: base(text)
		{
		}

		// Token: 0x06014DD9 RID: 85465 RVA: 0x00318230 File Offset: 0x00316430
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DDA RID: 85466 RVA: 0x0031824B File Offset: 0x0031644B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTError>(deep);
		}

		// Token: 0x04009036 RID: 36918
		private const string tagName = "error";

		// Token: 0x04009037 RID: 36919
		private const byte tagNsId = 5;

		// Token: 0x04009038 RID: 36920
		internal const int ElementTypeIdConst = 10990;
	}
}
