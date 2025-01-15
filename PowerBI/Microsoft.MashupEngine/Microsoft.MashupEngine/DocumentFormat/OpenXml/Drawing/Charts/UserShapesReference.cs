using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200256D RID: 9581
	[GeneratedCode("DomGen", "2.0")]
	internal class UserShapesReference : RelationshipIdType
	{
		// Token: 0x170055CB RID: 21963
		// (get) Token: 0x06011DD8 RID: 73176 RVA: 0x002F3280 File Offset: 0x002F1480
		public override string LocalName
		{
			get
			{
				return "userShapes";
			}
		}

		// Token: 0x170055CC RID: 21964
		// (get) Token: 0x06011DD9 RID: 73177 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055CD RID: 21965
		// (get) Token: 0x06011DDA RID: 73178 RVA: 0x002F334B File Offset: 0x002F154B
		internal override int ElementTypeId
		{
			get
			{
				return 10581;
			}
		}

		// Token: 0x06011DDB RID: 73179 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011DDD RID: 73181 RVA: 0x002F3352 File Offset: 0x002F1552
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UserShapesReference>(deep);
		}

		// Token: 0x04007CF8 RID: 31992
		private const string tagName = "userShapes";

		// Token: 0x04007CF9 RID: 31993
		private const byte tagNsId = 11;

		// Token: 0x04007CFA RID: 31994
		internal const int ElementTypeIdConst = 10581;
	}
}
