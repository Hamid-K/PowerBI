using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E54 RID: 11860
	[GeneratedCode("DomGen", "2.0")]
	internal class AttachedTemplate : RelationshipType
	{
		// Token: 0x17008A3C RID: 35388
		// (get) Token: 0x0601934D RID: 103245 RVA: 0x003477EF File Offset: 0x003459EF
		public override string LocalName
		{
			get
			{
				return "attachedTemplate";
			}
		}

		// Token: 0x17008A3D RID: 35389
		// (get) Token: 0x0601934E RID: 103246 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A3E RID: 35390
		// (get) Token: 0x0601934F RID: 103247 RVA: 0x003477F6 File Offset: 0x003459F6
		internal override int ElementTypeId
		{
			get
			{
				return 11982;
			}
		}

		// Token: 0x06019350 RID: 103248 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019352 RID: 103250 RVA: 0x003477FD File Offset: 0x003459FD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AttachedTemplate>(deep);
		}

		// Token: 0x0400A796 RID: 42902
		private const string tagName = "attachedTemplate";

		// Token: 0x0400A797 RID: 42903
		private const byte tagNsId = 23;

		// Token: 0x0400A798 RID: 42904
		internal const int ElementTypeIdConst = 11982;
	}
}
