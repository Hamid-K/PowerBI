using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002927 RID: 10535
	[GeneratedCode("DomGen", "2.0")]
	internal class VTBString : OpenXmlLeafTextElement
	{
		// Token: 0x17006AD0 RID: 27344
		// (get) Token: 0x06014DAB RID: 85419 RVA: 0x0031811C File Offset: 0x0031631C
		public override string LocalName
		{
			get
			{
				return "bstr";
			}
		}

		// Token: 0x17006AD1 RID: 27345
		// (get) Token: 0x06014DAC RID: 85420 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AD2 RID: 27346
		// (get) Token: 0x06014DAD RID: 85421 RVA: 0x00318123 File Offset: 0x00316323
		internal override int ElementTypeId
		{
			get
			{
				return 10985;
			}
		}

		// Token: 0x06014DAE RID: 85422 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DAF RID: 85423 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTBString()
		{
		}

		// Token: 0x06014DB0 RID: 85424 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTBString(string text)
			: base(text)
		{
		}

		// Token: 0x06014DB1 RID: 85425 RVA: 0x0031812C File Offset: 0x0031632C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DB2 RID: 85426 RVA: 0x00318147 File Offset: 0x00316347
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTBString>(deep);
		}

		// Token: 0x04009027 RID: 36903
		private const string tagName = "bstr";

		// Token: 0x04009028 RID: 36904
		private const byte tagNsId = 5;

		// Token: 0x04009029 RID: 36905
		internal const int ElementTypeIdConst = 10985;
	}
}
