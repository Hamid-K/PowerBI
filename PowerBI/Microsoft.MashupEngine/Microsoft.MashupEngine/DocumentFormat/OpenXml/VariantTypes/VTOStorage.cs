using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002915 RID: 10517
	[GeneratedCode("DomGen", "2.0")]
	internal class VTOStorage : OpenXmlLeafTextElement
	{
		// Token: 0x17006A9A RID: 27290
		// (get) Token: 0x06014D1F RID: 85279 RVA: 0x00317DB4 File Offset: 0x00315FB4
		public override string LocalName
		{
			get
			{
				return "ostorage";
			}
		}

		// Token: 0x17006A9B RID: 27291
		// (get) Token: 0x06014D20 RID: 85280 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A9C RID: 27292
		// (get) Token: 0x06014D21 RID: 85281 RVA: 0x00317DBB File Offset: 0x00315FBB
		internal override int ElementTypeId
		{
			get
			{
				return 10994;
			}
		}

		// Token: 0x06014D22 RID: 85282 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D23 RID: 85283 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTOStorage()
		{
		}

		// Token: 0x06014D24 RID: 85284 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTOStorage(string text)
			: base(text)
		{
		}

		// Token: 0x06014D25 RID: 85285 RVA: 0x00317DC4 File Offset: 0x00315FC4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D26 RID: 85286 RVA: 0x00317DDF File Offset: 0x00315FDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTOStorage>(deep);
		}

		// Token: 0x04008FF1 RID: 36849
		private const string tagName = "ostorage";

		// Token: 0x04008FF2 RID: 36850
		private const byte tagNsId = 5;

		// Token: 0x04008FF3 RID: 36851
		internal const int ElementTypeIdConst = 10994;
	}
}
