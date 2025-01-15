using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028B8 RID: 10424
	[GeneratedCode("DomGen", "2.0")]
	internal class Last : OpenXmlLeafTextElement
	{
		// Token: 0x170068D0 RID: 26832
		// (get) Token: 0x060148B4 RID: 84148 RVA: 0x00314833 File Offset: 0x00312A33
		public override string LocalName
		{
			get
			{
				return "Last";
			}
		}

		// Token: 0x170068D1 RID: 26833
		// (get) Token: 0x060148B5 RID: 84149 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068D2 RID: 26834
		// (get) Token: 0x060148B6 RID: 84150 RVA: 0x0031483A File Offset: 0x00312A3A
		internal override int ElementTypeId
		{
			get
			{
				return 10760;
			}
		}

		// Token: 0x060148B7 RID: 84151 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148B8 RID: 84152 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Last()
		{
		}

		// Token: 0x060148B9 RID: 84153 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Last(string text)
			: base(text)
		{
		}

		// Token: 0x060148BA RID: 84154 RVA: 0x00314844 File Offset: 0x00312A44
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148BB RID: 84155 RVA: 0x0031485F File Offset: 0x00312A5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Last>(deep);
		}

		// Token: 0x04008EB4 RID: 36532
		private const string tagName = "Last";

		// Token: 0x04008EB5 RID: 36533
		private const byte tagNsId = 9;

		// Token: 0x04008EB6 RID: 36534
		internal const int ElementTypeIdConst = 10760;
	}
}
