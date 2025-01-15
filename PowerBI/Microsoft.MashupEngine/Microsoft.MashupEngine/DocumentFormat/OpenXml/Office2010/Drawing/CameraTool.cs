using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002343 RID: 9027
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CameraTool : OpenXmlLeafElement
	{
		// Token: 0x17004969 RID: 18793
		// (get) Token: 0x0601025D RID: 66141 RVA: 0x002E039B File Offset: 0x002DE59B
		public override string LocalName
		{
			get
			{
				return "cameraTool";
			}
		}

		// Token: 0x1700496A RID: 18794
		// (get) Token: 0x0601025E RID: 66142 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x1700496B RID: 18795
		// (get) Token: 0x0601025F RID: 66143 RVA: 0x002E03A6 File Offset: 0x002DE5A6
		internal override int ElementTypeId
		{
			get
			{
				return 12712;
			}
		}

		// Token: 0x06010260 RID: 66144 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700496C RID: 18796
		// (get) Token: 0x06010261 RID: 66145 RVA: 0x002E03AD File Offset: 0x002DE5AD
		internal override string[] AttributeTagNames
		{
			get
			{
				return CameraTool.attributeTagNames;
			}
		}

		// Token: 0x1700496D RID: 18797
		// (get) Token: 0x06010262 RID: 66146 RVA: 0x002E03B4 File Offset: 0x002DE5B4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CameraTool.attributeNamespaceIds;
			}
		}

		// Token: 0x1700496E RID: 18798
		// (get) Token: 0x06010263 RID: 66147 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010264 RID: 66148 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cellRange")]
		public StringValue CellRange
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700496F RID: 18799
		// (get) Token: 0x06010265 RID: 66149 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010266 RID: 66150 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010268 RID: 66152 RVA: 0x002E03BB File Offset: 0x002DE5BB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cellRange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010269 RID: 66153 RVA: 0x002E03F1 File Offset: 0x002DE5F1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CameraTool>(deep);
		}

		// Token: 0x0601026A RID: 66154 RVA: 0x002E03FC File Offset: 0x002DE5FC
		// Note: this type is marked as 'beforefieldinit'.
		static CameraTool()
		{
			byte[] array = new byte[2];
			CameraTool.attributeNamespaceIds = array;
		}

		// Token: 0x04007348 RID: 29512
		private const string tagName = "cameraTool";

		// Token: 0x04007349 RID: 29513
		private const byte tagNsId = 48;

		// Token: 0x0400734A RID: 29514
		internal const int ElementTypeIdConst = 12712;

		// Token: 0x0400734B RID: 29515
		private static string[] attributeTagNames = new string[] { "cellRange", "spid" };

		// Token: 0x0400734C RID: 29516
		private static byte[] attributeNamespaceIds;
	}
}
