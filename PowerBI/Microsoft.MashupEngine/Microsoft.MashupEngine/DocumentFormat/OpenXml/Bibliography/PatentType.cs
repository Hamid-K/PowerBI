using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E8 RID: 10472
	[GeneratedCode("DomGen", "2.0")]
	internal class PatentType : OpenXmlLeafTextElement
	{
		// Token: 0x17006960 RID: 26976
		// (get) Token: 0x06014A34 RID: 84532 RVA: 0x001E491D File Offset: 0x001E2B1D
		public override string LocalName
		{
			get
			{
				return "Type";
			}
		}

		// Token: 0x17006961 RID: 26977
		// (get) Token: 0x06014A35 RID: 84533 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006962 RID: 26978
		// (get) Token: 0x06014A36 RID: 84534 RVA: 0x003151F4 File Offset: 0x003133F4
		internal override int ElementTypeId
		{
			get
			{
				return 10827;
			}
		}

		// Token: 0x06014A37 RID: 84535 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A38 RID: 84536 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PatentType()
		{
		}

		// Token: 0x06014A39 RID: 84537 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PatentType(string text)
			: base(text)
		{
		}

		// Token: 0x06014A3A RID: 84538 RVA: 0x003151FC File Offset: 0x003133FC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A3B RID: 84539 RVA: 0x00315217 File Offset: 0x00313417
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PatentType>(deep);
		}

		// Token: 0x04008F44 RID: 36676
		private const string tagName = "Type";

		// Token: 0x04008F45 RID: 36677
		private const byte tagNsId = 9;

		// Token: 0x04008F46 RID: 36678
		internal const int ElementTypeIdConst = 10827;
	}
}
