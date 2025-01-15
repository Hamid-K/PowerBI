using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291F RID: 10527
	[GeneratedCode("DomGen", "2.0")]
	internal class VTUnsignedInt32 : OpenXmlLeafTextElement
	{
		// Token: 0x17006AB8 RID: 27320
		// (get) Token: 0x06014D6B RID: 85355 RVA: 0x00317F7C File Offset: 0x0031617C
		public override string LocalName
		{
			get
			{
				return "ui4";
			}
		}

		// Token: 0x17006AB9 RID: 27321
		// (get) Token: 0x06014D6C RID: 85356 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ABA RID: 27322
		// (get) Token: 0x06014D6D RID: 85357 RVA: 0x00317F83 File Offset: 0x00316183
		internal override int ElementTypeId
		{
			get
			{
				return 10977;
			}
		}

		// Token: 0x06014D6E RID: 85358 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D6F RID: 85359 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTUnsignedInt32()
		{
		}

		// Token: 0x06014D70 RID: 85360 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTUnsignedInt32(string text)
			: base(text)
		{
		}

		// Token: 0x06014D71 RID: 85361 RVA: 0x00317F8C File Offset: 0x0031618C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new UInt32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D72 RID: 85362 RVA: 0x00317FA7 File Offset: 0x003161A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTUnsignedInt32>(deep);
		}

		// Token: 0x0400900F RID: 36879
		private const string tagName = "ui4";

		// Token: 0x04009010 RID: 36880
		private const byte tagNsId = 5;

		// Token: 0x04009011 RID: 36881
		internal const int ElementTypeIdConst = 10977;
	}
}
