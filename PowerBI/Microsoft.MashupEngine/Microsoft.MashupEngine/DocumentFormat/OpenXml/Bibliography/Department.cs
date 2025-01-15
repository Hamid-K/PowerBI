using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028CA RID: 10442
	[GeneratedCode("DomGen", "2.0")]
	internal class Department : OpenXmlLeafTextElement
	{
		// Token: 0x17006906 RID: 26886
		// (get) Token: 0x06014944 RID: 84292 RVA: 0x00314BDC File Offset: 0x00312DDC
		public override string LocalName
		{
			get
			{
				return "Department";
			}
		}

		// Token: 0x17006907 RID: 26887
		// (get) Token: 0x06014945 RID: 84293 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006908 RID: 26888
		// (get) Token: 0x06014946 RID: 84294 RVA: 0x00314BE3 File Offset: 0x00312DE3
		internal override int ElementTypeId
		{
			get
			{
				return 10796;
			}
		}

		// Token: 0x06014947 RID: 84295 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014948 RID: 84296 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Department()
		{
		}

		// Token: 0x06014949 RID: 84297 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Department(string text)
			: base(text)
		{
		}

		// Token: 0x0601494A RID: 84298 RVA: 0x00314BEC File Offset: 0x00312DEC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601494B RID: 84299 RVA: 0x00314C07 File Offset: 0x00312E07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Department>(deep);
		}

		// Token: 0x04008EEA RID: 36586
		private const string tagName = "Department";

		// Token: 0x04008EEB RID: 36587
		private const byte tagNsId = 9;

		// Token: 0x04008EEC RID: 36588
		internal const int ElementTypeIdConst = 10796;
	}
}
