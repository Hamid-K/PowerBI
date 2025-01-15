using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002333 RID: 9011
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupShape : GroupShapeType
	{
		// Token: 0x170048F7 RID: 18679
		// (get) Token: 0x0601015A RID: 65882 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x170048F8 RID: 18680
		// (get) Token: 0x0601015B RID: 65883 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048F9 RID: 18681
		// (get) Token: 0x0601015C RID: 65884 RVA: 0x002DF953 File Offset: 0x002DDB53
		internal override int ElementTypeId
		{
			get
			{
				return 13033;
			}
		}

		// Token: 0x0601015D RID: 65885 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601015E RID: 65886 RVA: 0x002DF95A File Offset: 0x002DDB5A
		public GroupShape()
		{
		}

		// Token: 0x0601015F RID: 65887 RVA: 0x002DF962 File Offset: 0x002DDB62
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010160 RID: 65888 RVA: 0x002DF96B File Offset: 0x002DDB6B
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010161 RID: 65889 RVA: 0x002DF974 File Offset: 0x002DDB74
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010162 RID: 65890 RVA: 0x002DF97D File Offset: 0x002DDB7D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04007302 RID: 29442
		private const string tagName = "grpSp";

		// Token: 0x04007303 RID: 29443
		private const byte tagNsId = 56;

		// Token: 0x04007304 RID: 29444
		internal const int ElementTypeIdConst = 13033;
	}
}
