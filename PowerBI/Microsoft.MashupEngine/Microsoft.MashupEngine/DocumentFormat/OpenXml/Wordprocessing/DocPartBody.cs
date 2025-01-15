using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD0 RID: 12240
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartBody : BodyType
	{
		// Token: 0x1700943F RID: 37951
		// (get) Token: 0x0601A904 RID: 108804 RVA: 0x0036450C File Offset: 0x0036270C
		public override string LocalName
		{
			get
			{
				return "docPartBody";
			}
		}

		// Token: 0x17009440 RID: 37952
		// (get) Token: 0x0601A905 RID: 108805 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009441 RID: 37953
		// (get) Token: 0x0601A906 RID: 108806 RVA: 0x00364513 File Offset: 0x00362713
		internal override int ElementTypeId
		{
			get
			{
				return 11956;
			}
		}

		// Token: 0x0601A907 RID: 108807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A908 RID: 108808 RVA: 0x003644E0 File Offset: 0x003626E0
		public DocPartBody()
		{
		}

		// Token: 0x0601A909 RID: 108809 RVA: 0x003644E8 File Offset: 0x003626E8
		public DocPartBody(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A90A RID: 108810 RVA: 0x003644F1 File Offset: 0x003626F1
		public DocPartBody(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A90B RID: 108811 RVA: 0x003644FA File Offset: 0x003626FA
		public DocPartBody(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A90C RID: 108812 RVA: 0x0036451A File Offset: 0x0036271A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartBody>(deep);
		}

		// Token: 0x0400AD85 RID: 44421
		private const string tagName = "docPartBody";

		// Token: 0x0400AD86 RID: 44422
		private const byte tagNsId = 23;

		// Token: 0x0400AD87 RID: 44423
		internal const int ElementTypeIdConst = 11956;
	}
}
