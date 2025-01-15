using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA4 RID: 11940
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class StartBorder : BorderType
	{
		// Token: 0x17008B87 RID: 35719
		// (get) Token: 0x06019600 RID: 103936 RVA: 0x00313F27 File Offset: 0x00312127
		public override string LocalName
		{
			get
			{
				return "start";
			}
		}

		// Token: 0x17008B88 RID: 35720
		// (get) Token: 0x06019601 RID: 103937 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B89 RID: 35721
		// (get) Token: 0x06019602 RID: 103938 RVA: 0x00349098 File Offset: 0x00347298
		internal override int ElementTypeId
		{
			get
			{
				return 12121;
			}
		}

		// Token: 0x06019603 RID: 103939 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06019605 RID: 103941 RVA: 0x0034909F File Offset: 0x0034729F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartBorder>(deep);
		}

		// Token: 0x0400A8A2 RID: 43170
		private const string tagName = "start";

		// Token: 0x0400A8A3 RID: 43171
		private const byte tagNsId = 23;

		// Token: 0x0400A8A4 RID: 43172
		internal const int ElementTypeIdConst = 12121;
	}
}
