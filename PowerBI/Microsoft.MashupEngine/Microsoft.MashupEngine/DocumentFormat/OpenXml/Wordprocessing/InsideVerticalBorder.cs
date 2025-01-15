using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA7 RID: 11943
	[GeneratedCode("DomGen", "2.0")]
	internal class InsideVerticalBorder : BorderType
	{
		// Token: 0x17008B90 RID: 35728
		// (get) Token: 0x06019612 RID: 103954 RVA: 0x0030E41B File Offset: 0x0030C61B
		public override string LocalName
		{
			get
			{
				return "insideV";
			}
		}

		// Token: 0x17008B91 RID: 35729
		// (get) Token: 0x06019613 RID: 103955 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B92 RID: 35730
		// (get) Token: 0x06019614 RID: 103956 RVA: 0x003490C8 File Offset: 0x003472C8
		internal override int ElementTypeId
		{
			get
			{
				return 12124;
			}
		}

		// Token: 0x06019615 RID: 103957 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019617 RID: 103959 RVA: 0x003490CF File Offset: 0x003472CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsideVerticalBorder>(deep);
		}

		// Token: 0x0400A8AB RID: 43179
		private const string tagName = "insideV";

		// Token: 0x0400A8AC RID: 43180
		private const byte tagNsId = 23;

		// Token: 0x0400A8AD RID: 43181
		internal const int ElementTypeIdConst = 12124;
	}
}
