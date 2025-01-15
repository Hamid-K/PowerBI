using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E9 RID: 8937
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Separator : OpenXmlLeafElement
	{
		// Token: 0x1700467C RID: 18044
		// (get) Token: 0x0600FBDD RID: 64477 RVA: 0x002CF519 File Offset: 0x002CD719
		public override string LocalName
		{
			get
			{
				return "separator";
			}
		}

		// Token: 0x1700467D RID: 18045
		// (get) Token: 0x0600FBDE RID: 64478 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700467E RID: 18046
		// (get) Token: 0x0600FBDF RID: 64479 RVA: 0x002DAFAD File Offset: 0x002D91AD
		internal override int ElementTypeId
		{
			get
			{
				return 13082;
			}
		}

		// Token: 0x0600FBE0 RID: 64480 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700467F RID: 18047
		// (get) Token: 0x0600FBE1 RID: 64481 RVA: 0x002DAFB4 File Offset: 0x002D91B4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Separator.attributeTagNames;
			}
		}

		// Token: 0x17004680 RID: 18048
		// (get) Token: 0x0600FBE2 RID: 64482 RVA: 0x002DAFBB File Offset: 0x002D91BB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Separator.attributeNamespaceIds;
			}
		}

		// Token: 0x17004681 RID: 18049
		// (get) Token: 0x0600FBE3 RID: 64483 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FBE4 RID: 64484 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004682 RID: 18050
		// (get) Token: 0x0600FBE5 RID: 64485 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FBE6 RID: 64486 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004683 RID: 18051
		// (get) Token: 0x0600FBE7 RID: 64487 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FBE8 RID: 64488 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004684 RID: 18052
		// (get) Token: 0x0600FBE9 RID: 64489 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600FBEA RID: 64490 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004685 RID: 18053
		// (get) Token: 0x0600FBEB RID: 64491 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FBEC RID: 64492 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004686 RID: 18054
		// (get) Token: 0x0600FBED RID: 64493 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FBEE RID: 64494 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004687 RID: 18055
		// (get) Token: 0x0600FBEF RID: 64495 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FBF0 RID: 64496 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004688 RID: 18056
		// (get) Token: 0x0600FBF1 RID: 64497 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FBF2 RID: 64498 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004689 RID: 18057
		// (get) Token: 0x0600FBF3 RID: 64499 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FBF4 RID: 64500 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0600FBF6 RID: 64502 RVA: 0x002DAFC4 File Offset: 0x002D91C4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FBF7 RID: 64503 RVA: 0x002DB09F File Offset: 0x002D929F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Separator>(deep);
		}

		// Token: 0x0600FBF8 RID: 64504 RVA: 0x002DB0A8 File Offset: 0x002D92A8
		// Note: this type is marked as 'beforefieldinit'.
		static Separator()
		{
			byte[] array = new byte[9];
			Separator.attributeNamespaceIds = array;
		}

		// Token: 0x040071BC RID: 29116
		private const string tagName = "separator";

		// Token: 0x040071BD RID: 29117
		private const byte tagNsId = 57;

		// Token: 0x040071BE RID: 29118
		internal const int ElementTypeIdConst = 13082;

		// Token: 0x040071BF RID: 29119
		private static string[] attributeTagNames = new string[] { "id", "idQ", "tag", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ" };

		// Token: 0x040071C0 RID: 29120
		private static byte[] attributeNamespaceIds;
	}
}
