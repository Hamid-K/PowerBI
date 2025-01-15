using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002685 RID: 9861
	[GeneratedCode("DomGen", "2.0")]
	internal class HierarchyBranch : OpenXmlLeafElement
	{
		// Token: 0x17005CBF RID: 23743
		// (get) Token: 0x06012D89 RID: 77193 RVA: 0x002FFFA7 File Offset: 0x002FE1A7
		public override string LocalName
		{
			get
			{
				return "hierBranch";
			}
		}

		// Token: 0x17005CC0 RID: 23744
		// (get) Token: 0x06012D8A RID: 77194 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CC1 RID: 23745
		// (get) Token: 0x06012D8B RID: 77195 RVA: 0x002FFFAE File Offset: 0x002FE1AE
		internal override int ElementTypeId
		{
			get
			{
				return 10676;
			}
		}

		// Token: 0x06012D8C RID: 77196 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CC2 RID: 23746
		// (get) Token: 0x06012D8D RID: 77197 RVA: 0x002FFFB5 File Offset: 0x002FE1B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return HierarchyBranch.attributeTagNames;
			}
		}

		// Token: 0x17005CC3 RID: 23747
		// (get) Token: 0x06012D8E RID: 77198 RVA: 0x002FFFBC File Offset: 0x002FE1BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HierarchyBranch.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CC4 RID: 23748
		// (get) Token: 0x06012D8F RID: 77199 RVA: 0x002FFFC3 File Offset: 0x002FE1C3
		// (set) Token: 0x06012D90 RID: 77200 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<HierarchyBranchStyleValues> Val
		{
			get
			{
				return (EnumValue<HierarchyBranchStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012D92 RID: 77202 RVA: 0x002FFFD2 File Offset: 0x002FE1D2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<HierarchyBranchStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D93 RID: 77203 RVA: 0x002FFFF2 File Offset: 0x002FE1F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HierarchyBranch>(deep);
		}

		// Token: 0x06012D94 RID: 77204 RVA: 0x002FFFFC File Offset: 0x002FE1FC
		// Note: this type is marked as 'beforefieldinit'.
		static HierarchyBranch()
		{
			byte[] array = new byte[1];
			HierarchyBranch.attributeNamespaceIds = array;
		}

		// Token: 0x040081DF RID: 33247
		private const string tagName = "hierBranch";

		// Token: 0x040081E0 RID: 33248
		private const byte tagNsId = 14;

		// Token: 0x040081E1 RID: 33249
		internal const int ElementTypeIdConst = 10676;

		// Token: 0x040081E2 RID: 33250
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081E3 RID: 33251
		private static byte[] attributeNamespaceIds;
	}
}
