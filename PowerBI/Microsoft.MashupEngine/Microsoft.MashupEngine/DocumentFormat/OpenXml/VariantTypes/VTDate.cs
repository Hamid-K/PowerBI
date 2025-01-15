using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002928 RID: 10536
	[GeneratedCode("DomGen", "2.0")]
	internal class VTDate : OpenXmlLeafTextElement
	{
		// Token: 0x17006AD3 RID: 27347
		// (get) Token: 0x06014DB3 RID: 85427 RVA: 0x00318150 File Offset: 0x00316350
		public override string LocalName
		{
			get
			{
				return "date";
			}
		}

		// Token: 0x17006AD4 RID: 27348
		// (get) Token: 0x06014DB4 RID: 85428 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AD5 RID: 27349
		// (get) Token: 0x06014DB5 RID: 85429 RVA: 0x00318157 File Offset: 0x00316357
		internal override int ElementTypeId
		{
			get
			{
				return 10986;
			}
		}

		// Token: 0x06014DB6 RID: 85430 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DB7 RID: 85431 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTDate()
		{
		}

		// Token: 0x06014DB8 RID: 85432 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTDate(string text)
			: base(text)
		{
		}

		// Token: 0x06014DB9 RID: 85433 RVA: 0x00318160 File Offset: 0x00316360
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DateTimeValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DBA RID: 85434 RVA: 0x0031817B File Offset: 0x0031637B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTDate>(deep);
		}

		// Token: 0x0400902A RID: 36906
		private const string tagName = "date";

		// Token: 0x0400902B RID: 36907
		private const byte tagNsId = 5;

		// Token: 0x0400902C RID: 36908
		internal const int ElementTypeIdConst = 10986;
	}
}
