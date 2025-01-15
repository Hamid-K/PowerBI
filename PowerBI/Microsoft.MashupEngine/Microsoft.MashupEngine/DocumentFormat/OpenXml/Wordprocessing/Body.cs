using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FCF RID: 12239
	[GeneratedCode("DomGen", "2.0")]
	internal class Body : BodyType
	{
		// Token: 0x1700943C RID: 37948
		// (get) Token: 0x0601A8FB RID: 108795 RVA: 0x003644D2 File Offset: 0x003626D2
		public override string LocalName
		{
			get
			{
				return "body";
			}
		}

		// Token: 0x1700943D RID: 37949
		// (get) Token: 0x0601A8FC RID: 108796 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700943E RID: 37950
		// (get) Token: 0x0601A8FD RID: 108797 RVA: 0x003644D9 File Offset: 0x003626D9
		internal override int ElementTypeId
		{
			get
			{
				return 11946;
			}
		}

		// Token: 0x0601A8FE RID: 108798 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A8FF RID: 108799 RVA: 0x003644E0 File Offset: 0x003626E0
		public Body()
		{
		}

		// Token: 0x0601A900 RID: 108800 RVA: 0x003644E8 File Offset: 0x003626E8
		public Body(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A901 RID: 108801 RVA: 0x003644F1 File Offset: 0x003626F1
		public Body(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A902 RID: 108802 RVA: 0x003644FA File Offset: 0x003626FA
		public Body(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A903 RID: 108803 RVA: 0x00364503 File Offset: 0x00362703
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Body>(deep);
		}

		// Token: 0x0400AD82 RID: 44418
		private const string tagName = "body";

		// Token: 0x0400AD83 RID: 44419
		private const byte tagNsId = 23;

		// Token: 0x0400AD84 RID: 44420
		internal const int ElementTypeIdConst = 11946;
	}
}
