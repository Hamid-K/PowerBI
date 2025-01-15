using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA5 RID: 11941
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class EndBorder : BorderType
	{
		// Token: 0x17008B8A RID: 35722
		// (get) Token: 0x06019606 RID: 103942 RVA: 0x0030761A File Offset: 0x0030581A
		public override string LocalName
		{
			get
			{
				return "end";
			}
		}

		// Token: 0x17008B8B RID: 35723
		// (get) Token: 0x06019607 RID: 103943 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B8C RID: 35724
		// (get) Token: 0x06019608 RID: 103944 RVA: 0x003490A8 File Offset: 0x003472A8
		internal override int ElementTypeId
		{
			get
			{
				return 12122;
			}
		}

		// Token: 0x06019609 RID: 103945 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601960B RID: 103947 RVA: 0x003490AF File Offset: 0x003472AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndBorder>(deep);
		}

		// Token: 0x0400A8A5 RID: 43173
		private const string tagName = "end";

		// Token: 0x0400A8A6 RID: 43174
		private const byte tagNsId = 23;

		// Token: 0x0400A8A7 RID: 43175
		internal const int ElementTypeIdConst = 12122;
	}
}
