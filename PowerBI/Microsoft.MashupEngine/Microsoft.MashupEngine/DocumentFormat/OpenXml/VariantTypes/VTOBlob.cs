using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002911 RID: 10513
	[GeneratedCode("DomGen", "2.0")]
	internal class VTOBlob : OpenXmlLeafTextElement
	{
		// Token: 0x17006A8E RID: 27278
		// (get) Token: 0x06014CFF RID: 85247 RVA: 0x00317CE4 File Offset: 0x00315EE4
		public override string LocalName
		{
			get
			{
				return "oblob";
			}
		}

		// Token: 0x17006A8F RID: 27279
		// (get) Token: 0x06014D00 RID: 85248 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A90 RID: 27280
		// (get) Token: 0x06014D01 RID: 85249 RVA: 0x00317CEB File Offset: 0x00315EEB
		internal override int ElementTypeId
		{
			get
			{
				return 10967;
			}
		}

		// Token: 0x06014D02 RID: 85250 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D03 RID: 85251 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTOBlob()
		{
		}

		// Token: 0x06014D04 RID: 85252 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTOBlob(string text)
			: base(text)
		{
		}

		// Token: 0x06014D05 RID: 85253 RVA: 0x00317CF4 File Offset: 0x00315EF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D06 RID: 85254 RVA: 0x00317D0F File Offset: 0x00315F0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTOBlob>(deep);
		}

		// Token: 0x04008FE5 RID: 36837
		private const string tagName = "oblob";

		// Token: 0x04008FE6 RID: 36838
		private const byte tagNsId = 5;

		// Token: 0x04008FE7 RID: 36839
		internal const int ElementTypeIdConst = 10967;
	}
}
