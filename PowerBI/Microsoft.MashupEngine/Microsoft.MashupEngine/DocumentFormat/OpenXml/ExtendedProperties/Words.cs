using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293B RID: 10555
	[GeneratedCode("DomGen", "2.0")]
	internal class Words : OpenXmlLeafTextElement
	{
		// Token: 0x17006B2C RID: 27436
		// (get) Token: 0x06014E8A RID: 85642 RVA: 0x00318B48 File Offset: 0x00316D48
		public override string LocalName
		{
			get
			{
				return "Words";
			}
		}

		// Token: 0x17006B2D RID: 27437
		// (get) Token: 0x06014E8B RID: 85643 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B2E RID: 27438
		// (get) Token: 0x06014E8C RID: 85644 RVA: 0x00318B4F File Offset: 0x00316D4F
		internal override int ElementTypeId
		{
			get
			{
				return 11003;
			}
		}

		// Token: 0x06014E8D RID: 85645 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E8E RID: 85646 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Words()
		{
		}

		// Token: 0x06014E8F RID: 85647 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Words(string text)
			: base(text)
		{
		}

		// Token: 0x06014E90 RID: 85648 RVA: 0x00318B58 File Offset: 0x00316D58
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014E91 RID: 85649 RVA: 0x00318B73 File Offset: 0x00316D73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Words>(deep);
		}

		// Token: 0x0400908B RID: 37003
		private const string tagName = "Words";

		// Token: 0x0400908C RID: 37004
		private const byte tagNsId = 3;

		// Token: 0x0400908D RID: 37005
		internal const int ElementTypeIdConst = 11003;
	}
}
