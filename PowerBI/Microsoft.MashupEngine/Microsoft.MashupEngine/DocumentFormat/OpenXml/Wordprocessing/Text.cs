using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E58 RID: 11864
	[GeneratedCode("DomGen", "2.0")]
	internal class Text : TextType
	{
		// Token: 0x17008A4C RID: 35404
		// (get) Token: 0x06019370 RID: 103280 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x17008A4D RID: 35405
		// (get) Token: 0x06019371 RID: 103281 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A4E RID: 35406
		// (get) Token: 0x06019372 RID: 103282 RVA: 0x00347933 File Offset: 0x00345B33
		internal override int ElementTypeId
		{
			get
			{
				return 11544;
			}
		}

		// Token: 0x06019373 RID: 103283 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019374 RID: 103284 RVA: 0x0034793A File Offset: 0x00345B3A
		public Text()
		{
		}

		// Token: 0x06019375 RID: 103285 RVA: 0x00347942 File Offset: 0x00345B42
		public Text(string text)
			: base(text)
		{
		}

		// Token: 0x06019376 RID: 103286 RVA: 0x0034794C File Offset: 0x00345B4C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06019377 RID: 103287 RVA: 0x00347967 File Offset: 0x00345B67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Text>(deep);
		}

		// Token: 0x0400A7A3 RID: 42915
		private const string tagName = "t";

		// Token: 0x0400A7A4 RID: 42916
		private const byte tagNsId = 23;

		// Token: 0x0400A7A5 RID: 42917
		internal const int ElementTypeIdConst = 11544;
	}
}
