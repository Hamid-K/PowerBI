using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC9 RID: 11721
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowEnvelope : OnOffType
	{
		// Token: 0x170087F7 RID: 34807
		// (get) Token: 0x06018EAE RID: 102062 RVA: 0x0034521C File Offset: 0x0034341C
		public override string LocalName
		{
			get
			{
				return "showEnvelope";
			}
		}

		// Token: 0x170087F8 RID: 34808
		// (get) Token: 0x06018EAF RID: 102063 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087F9 RID: 34809
		// (get) Token: 0x06018EB0 RID: 102064 RVA: 0x00345223 File Offset: 0x00343423
		internal override int ElementTypeId
		{
			get
			{
				return 12001;
			}
		}

		// Token: 0x06018EB1 RID: 102065 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EB3 RID: 102067 RVA: 0x0034522A File Offset: 0x0034342A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowEnvelope>(deep);
		}

		// Token: 0x0400A5C6 RID: 42438
		private const string tagName = "showEnvelope";

		// Token: 0x0400A5C7 RID: 42439
		private const byte tagNsId = 23;

		// Token: 0x0400A5C8 RID: 42440
		internal const int ElementTypeIdConst = 12001;
	}
}
