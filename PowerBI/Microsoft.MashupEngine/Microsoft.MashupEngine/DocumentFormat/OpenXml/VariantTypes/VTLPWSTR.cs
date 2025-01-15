using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002926 RID: 10534
	[GeneratedCode("DomGen", "2.0")]
	internal class VTLPWSTR : OpenXmlLeafTextElement
	{
		// Token: 0x17006ACD RID: 27341
		// (get) Token: 0x06014DA3 RID: 85411 RVA: 0x003180E8 File Offset: 0x003162E8
		public override string LocalName
		{
			get
			{
				return "lpwstr";
			}
		}

		// Token: 0x17006ACE RID: 27342
		// (get) Token: 0x06014DA4 RID: 85412 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ACF RID: 27343
		// (get) Token: 0x06014DA5 RID: 85413 RVA: 0x003180EF File Offset: 0x003162EF
		internal override int ElementTypeId
		{
			get
			{
				return 10984;
			}
		}

		// Token: 0x06014DA6 RID: 85414 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DA7 RID: 85415 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTLPWSTR()
		{
		}

		// Token: 0x06014DA8 RID: 85416 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTLPWSTR(string text)
			: base(text)
		{
		}

		// Token: 0x06014DA9 RID: 85417 RVA: 0x003180F8 File Offset: 0x003162F8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DAA RID: 85418 RVA: 0x00318113 File Offset: 0x00316313
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTLPWSTR>(deep);
		}

		// Token: 0x04009024 RID: 36900
		private const string tagName = "lpwstr";

		// Token: 0x04009025 RID: 36901
		private const byte tagNsId = 5;

		// Token: 0x04009026 RID: 36902
		internal const int ElementTypeIdConst = 10984;
	}
}
