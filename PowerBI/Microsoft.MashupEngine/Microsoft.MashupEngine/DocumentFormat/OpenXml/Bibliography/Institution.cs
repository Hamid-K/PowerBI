using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CE RID: 10446
	[GeneratedCode("DomGen", "2.0")]
	internal class Institution : OpenXmlLeafTextElement
	{
		// Token: 0x17006912 RID: 26898
		// (get) Token: 0x06014964 RID: 84324 RVA: 0x00314CAC File Offset: 0x00312EAC
		public override string LocalName
		{
			get
			{
				return "Institution";
			}
		}

		// Token: 0x17006913 RID: 26899
		// (get) Token: 0x06014965 RID: 84325 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006914 RID: 26900
		// (get) Token: 0x06014966 RID: 84326 RVA: 0x00314CB3 File Offset: 0x00312EB3
		internal override int ElementTypeId
		{
			get
			{
				return 10800;
			}
		}

		// Token: 0x06014967 RID: 84327 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014968 RID: 84328 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Institution()
		{
		}

		// Token: 0x06014969 RID: 84329 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Institution(string text)
			: base(text)
		{
		}

		// Token: 0x0601496A RID: 84330 RVA: 0x00314CBC File Offset: 0x00312EBC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601496B RID: 84331 RVA: 0x00314CD7 File Offset: 0x00312ED7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Institution>(deep);
		}

		// Token: 0x04008EF6 RID: 36598
		private const string tagName = "Institution";

		// Token: 0x04008EF7 RID: 36599
		private const byte tagNsId = 9;

		// Token: 0x04008EF8 RID: 36600
		internal const int ElementTypeIdConst = 10800;
	}
}
