using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002513 RID: 9491
	[GeneratedCode("DomGen", "2.0")]
	internal class Delete : BooleanType
	{
		// Token: 0x17005452 RID: 21586
		// (get) Token: 0x06011A9F RID: 72351 RVA: 0x002F12DE File Offset: 0x002EF4DE
		public override string LocalName
		{
			get
			{
				return "delete";
			}
		}

		// Token: 0x17005453 RID: 21587
		// (get) Token: 0x06011AA0 RID: 72352 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005454 RID: 21588
		// (get) Token: 0x06011AA1 RID: 72353 RVA: 0x002F12E5 File Offset: 0x002EF4E5
		internal override int ElementTypeId
		{
			get
			{
				return 10375;
			}
		}

		// Token: 0x06011AA2 RID: 72354 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AA4 RID: 72356 RVA: 0x002F12EC File Offset: 0x002EF4EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Delete>(deep);
		}

		// Token: 0x04007BCD RID: 31693
		private const string tagName = "delete";

		// Token: 0x04007BCE RID: 31694
		private const byte tagNsId = 11;

		// Token: 0x04007BCF RID: 31695
		internal const int ElementTypeIdConst = 10375;
	}
}
