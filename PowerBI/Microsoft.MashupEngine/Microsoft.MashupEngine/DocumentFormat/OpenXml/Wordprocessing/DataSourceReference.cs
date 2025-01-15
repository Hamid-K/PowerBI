using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E51 RID: 11857
	[GeneratedCode("DomGen", "2.0")]
	internal class DataSourceReference : RelationshipType
	{
		// Token: 0x17008A33 RID: 35379
		// (get) Token: 0x0601933B RID: 103227 RVA: 0x003477AA File Offset: 0x003459AA
		public override string LocalName
		{
			get
			{
				return "dataSource";
			}
		}

		// Token: 0x17008A34 RID: 35380
		// (get) Token: 0x0601933C RID: 103228 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A35 RID: 35381
		// (get) Token: 0x0601933D RID: 103229 RVA: 0x003477B1 File Offset: 0x003459B1
		internal override int ElementTypeId
		{
			get
			{
				return 11817;
			}
		}

		// Token: 0x0601933E RID: 103230 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019340 RID: 103232 RVA: 0x003477B8 File Offset: 0x003459B8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataSourceReference>(deep);
		}

		// Token: 0x0400A78D RID: 42893
		private const string tagName = "dataSource";

		// Token: 0x0400A78E RID: 42894
		private const byte tagNsId = 23;

		// Token: 0x0400A78F RID: 42895
		internal const int ElementTypeIdConst = 11817;
	}
}
