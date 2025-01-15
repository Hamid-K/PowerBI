using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4C RID: 11596
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlDelRangeEnd : MarkupType
	{
		// Token: 0x17008680 RID: 34432
		// (get) Token: 0x06018BBE RID: 101310 RVA: 0x003446AB File Offset: 0x003428AB
		public override string LocalName
		{
			get
			{
				return "customXmlDelRangeEnd";
			}
		}

		// Token: 0x17008681 RID: 34433
		// (get) Token: 0x06018BBF RID: 101311 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008682 RID: 34434
		// (get) Token: 0x06018BC0 RID: 101312 RVA: 0x003446B2 File Offset: 0x003428B2
		internal override int ElementTypeId
		{
			get
			{
				return 11487;
			}
		}

		// Token: 0x06018BC1 RID: 101313 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BC3 RID: 101315 RVA: 0x003446B9 File Offset: 0x003428B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlDelRangeEnd>(deep);
		}

		// Token: 0x0400A451 RID: 42065
		private const string tagName = "customXmlDelRangeEnd";

		// Token: 0x0400A452 RID: 42066
		private const byte tagNsId = 23;

		// Token: 0x0400A453 RID: 42067
		internal const int ElementTypeIdConst = 11487;
	}
}
