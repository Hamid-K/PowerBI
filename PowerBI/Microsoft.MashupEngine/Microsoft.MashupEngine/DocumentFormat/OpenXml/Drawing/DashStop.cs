using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D9 RID: 10201
	[GeneratedCode("DomGen", "2.0")]
	internal class DashStop : OpenXmlLeafElement
	{
		// Token: 0x170063F3 RID: 25587
		// (get) Token: 0x06013D83 RID: 81283 RVA: 0x0030C371 File Offset: 0x0030A571
		public override string LocalName
		{
			get
			{
				return "ds";
			}
		}

		// Token: 0x170063F4 RID: 25588
		// (get) Token: 0x06013D84 RID: 81284 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063F5 RID: 25589
		// (get) Token: 0x06013D85 RID: 81285 RVA: 0x0030C378 File Offset: 0x0030A578
		internal override int ElementTypeId
		{
			get
			{
				return 10234;
			}
		}

		// Token: 0x06013D86 RID: 81286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170063F6 RID: 25590
		// (get) Token: 0x06013D87 RID: 81287 RVA: 0x0030C37F File Offset: 0x0030A57F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DashStop.attributeTagNames;
			}
		}

		// Token: 0x170063F7 RID: 25591
		// (get) Token: 0x06013D88 RID: 81288 RVA: 0x0030C386 File Offset: 0x0030A586
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DashStop.attributeNamespaceIds;
			}
		}

		// Token: 0x170063F8 RID: 25592
		// (get) Token: 0x06013D89 RID: 81289 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013D8A RID: 81290 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "d")]
		public Int32Value DashLength
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170063F9 RID: 25593
		// (get) Token: 0x06013D8B RID: 81291 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013D8C RID: 81292 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sp")]
		public Int32Value SpaceLength
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013D8E RID: 81294 RVA: 0x0030C38D File Offset: 0x0030A58D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "d" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sp" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013D8F RID: 81295 RVA: 0x0030C3C3 File Offset: 0x0030A5C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DashStop>(deep);
		}

		// Token: 0x06013D90 RID: 81296 RVA: 0x0030C3CC File Offset: 0x0030A5CC
		// Note: this type is marked as 'beforefieldinit'.
		static DashStop()
		{
			byte[] array = new byte[2];
			DashStop.attributeNamespaceIds = array;
		}

		// Token: 0x0400880A RID: 34826
		private const string tagName = "ds";

		// Token: 0x0400880B RID: 34827
		private const byte tagNsId = 10;

		// Token: 0x0400880C RID: 34828
		internal const int ElementTypeIdConst = 10234;

		// Token: 0x0400880D RID: 34829
		private static string[] attributeTagNames = new string[] { "d", "sp" };

		// Token: 0x0400880E RID: 34830
		private static byte[] attributeNamespaceIds;
	}
}
