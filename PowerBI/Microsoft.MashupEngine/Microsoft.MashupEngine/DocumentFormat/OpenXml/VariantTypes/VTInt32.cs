using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291A RID: 10522
	[GeneratedCode("DomGen", "2.0")]
	internal class VTInt32 : OpenXmlLeafTextElement
	{
		// Token: 0x17006AA9 RID: 27305
		// (get) Token: 0x06014D43 RID: 85315 RVA: 0x00317E78 File Offset: 0x00316078
		public override string LocalName
		{
			get
			{
				return "i4";
			}
		}

		// Token: 0x17006AAA RID: 27306
		// (get) Token: 0x06014D44 RID: 85316 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AAB RID: 27307
		// (get) Token: 0x06014D45 RID: 85317 RVA: 0x00317E7F File Offset: 0x0031607F
		internal override int ElementTypeId
		{
			get
			{
				return 10972;
			}
		}

		// Token: 0x06014D46 RID: 85318 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D47 RID: 85319 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTInt32()
		{
		}

		// Token: 0x06014D48 RID: 85320 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTInt32(string text)
			: base(text)
		{
		}

		// Token: 0x06014D49 RID: 85321 RVA: 0x00317E88 File Offset: 0x00316088
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D4A RID: 85322 RVA: 0x00317EA3 File Offset: 0x003160A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTInt32>(deep);
		}

		// Token: 0x04009000 RID: 36864
		private const string tagName = "i4";

		// Token: 0x04009001 RID: 36865
		private const byte tagNsId = 5;

		// Token: 0x04009002 RID: 36866
		internal const int ElementTypeIdConst = 10972;
	}
}
