using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002415 RID: 9237
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TupleSetHeader : OpenXmlLeafElement
	{
		// Token: 0x17004F08 RID: 20232
		// (get) Token: 0x06010EAF RID: 69295 RVA: 0x002A6937 File Offset: 0x002A4B37
		public override string LocalName
		{
			get
			{
				return "header";
			}
		}

		// Token: 0x17004F09 RID: 20233
		// (get) Token: 0x06010EB0 RID: 69296 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F0A RID: 20234
		// (get) Token: 0x06010EB1 RID: 69297 RVA: 0x002E89DF File Offset: 0x002E6BDF
		internal override int ElementTypeId
		{
			get
			{
				return 12955;
			}
		}

		// Token: 0x06010EB2 RID: 69298 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F0B RID: 20235
		// (get) Token: 0x06010EB3 RID: 69299 RVA: 0x002E89E6 File Offset: 0x002E6BE6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TupleSetHeader.attributeTagNames;
			}
		}

		// Token: 0x17004F0C RID: 20236
		// (get) Token: 0x06010EB4 RID: 69300 RVA: 0x002E89ED File Offset: 0x002E6BED
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TupleSetHeader.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F0D RID: 20237
		// (get) Token: 0x06010EB5 RID: 69301 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010EB6 RID: 69302 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x17004F0E RID: 20238
		// (get) Token: 0x06010EB7 RID: 69303 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010EB8 RID: 69304 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hierarchyName")]
		public StringValue HierarchyName
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

		// Token: 0x06010EBA RID: 69306 RVA: 0x002E89F4 File Offset: 0x002E6BF4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hierarchyName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010EBB RID: 69307 RVA: 0x002E8A2A File Offset: 0x002E6C2A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSetHeader>(deep);
		}

		// Token: 0x06010EBC RID: 69308 RVA: 0x002E8A34 File Offset: 0x002E6C34
		// Note: this type is marked as 'beforefieldinit'.
		static TupleSetHeader()
		{
			byte[] array = new byte[2];
			TupleSetHeader.attributeNamespaceIds = array;
		}

		// Token: 0x040076E0 RID: 30432
		private const string tagName = "header";

		// Token: 0x040076E1 RID: 30433
		private const byte tagNsId = 53;

		// Token: 0x040076E2 RID: 30434
		internal const int ElementTypeIdConst = 12955;

		// Token: 0x040076E3 RID: 30435
		private static string[] attributeTagNames = new string[] { "uniqueName", "hierarchyName" };

		// Token: 0x040076E4 RID: 30436
		private static byte[] attributeNamespaceIds;
	}
}
