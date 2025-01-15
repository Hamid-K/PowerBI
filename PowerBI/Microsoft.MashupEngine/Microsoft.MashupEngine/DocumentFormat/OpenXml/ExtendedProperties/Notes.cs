using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002940 RID: 10560
	[GeneratedCode("DomGen", "2.0")]
	internal class Notes : OpenXmlLeafTextElement
	{
		// Token: 0x17006B3B RID: 27451
		// (get) Token: 0x06014EB2 RID: 85682 RVA: 0x00318C44 File Offset: 0x00316E44
		public override string LocalName
		{
			get
			{
				return "Notes";
			}
		}

		// Token: 0x17006B3C RID: 27452
		// (get) Token: 0x06014EB3 RID: 85683 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B3D RID: 27453
		// (get) Token: 0x06014EB4 RID: 85684 RVA: 0x00318C4B File Offset: 0x00316E4B
		internal override int ElementTypeId
		{
			get
			{
				return 11009;
			}
		}

		// Token: 0x06014EB5 RID: 85685 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EB6 RID: 85686 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Notes()
		{
		}

		// Token: 0x06014EB7 RID: 85687 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Notes(string text)
			: base(text)
		{
		}

		// Token: 0x06014EB8 RID: 85688 RVA: 0x00318C54 File Offset: 0x00316E54
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EB9 RID: 85689 RVA: 0x00318C6F File Offset: 0x00316E6F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Notes>(deep);
		}

		// Token: 0x0400909A RID: 37018
		private const string tagName = "Notes";

		// Token: 0x0400909B RID: 37019
		private const byte tagNsId = 3;

		// Token: 0x0400909C RID: 37020
		internal const int ElementTypeIdConst = 11009;
	}
}
