using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002301 RID: 8961
	[ChildElementInfo(typeof(TaskGroupTask), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TaskGroupCategory : OpenXmlCompositeElement
	{
		// Token: 0x17004762 RID: 18274
		// (get) Token: 0x0600FDE6 RID: 64998 RVA: 0x002DCAFB File Offset: 0x002DACFB
		public override string LocalName
		{
			get
			{
				return "category";
			}
		}

		// Token: 0x17004763 RID: 18275
		// (get) Token: 0x0600FDE7 RID: 64999 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004764 RID: 18276
		// (get) Token: 0x0600FDE8 RID: 65000 RVA: 0x002DCB02 File Offset: 0x002DAD02
		internal override int ElementTypeId
		{
			get
			{
				return 13103;
			}
		}

		// Token: 0x0600FDE9 RID: 65001 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004765 RID: 18277
		// (get) Token: 0x0600FDEA RID: 65002 RVA: 0x002DCB09 File Offset: 0x002DAD09
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskGroupCategory.attributeTagNames;
			}
		}

		// Token: 0x17004766 RID: 18278
		// (get) Token: 0x0600FDEB RID: 65003 RVA: 0x002DCB10 File Offset: 0x002DAD10
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskGroupCategory.attributeNamespaceIds;
			}
		}

		// Token: 0x17004767 RID: 18279
		// (get) Token: 0x0600FDEC RID: 65004 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FDED RID: 65005 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004768 RID: 18280
		// (get) Token: 0x0600FDEE RID: 65006 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FDEF RID: 65007 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004769 RID: 18281
		// (get) Token: 0x0600FDF0 RID: 65008 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FDF1 RID: 65009 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700476A RID: 18282
		// (get) Token: 0x0600FDF2 RID: 65010 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FDF3 RID: 65011 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700476B RID: 18283
		// (get) Token: 0x0600FDF4 RID: 65012 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FDF5 RID: 65013 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x1700476C RID: 18284
		// (get) Token: 0x0600FDF6 RID: 65014 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FDF7 RID: 65015 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x1700476D RID: 18285
		// (get) Token: 0x0600FDF8 RID: 65016 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FDF9 RID: 65017 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700476E RID: 18286
		// (get) Token: 0x0600FDFA RID: 65018 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FDFB RID: 65019 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700476F RID: 18287
		// (get) Token: 0x0600FDFC RID: 65020 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600FDFD RID: 65021 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004770 RID: 18288
		// (get) Token: 0x0600FDFE RID: 65022 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FDFF RID: 65023 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004771 RID: 18289
		// (get) Token: 0x0600FE00 RID: 65024 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FE01 RID: 65025 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004772 RID: 18290
		// (get) Token: 0x0600FE02 RID: 65026 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FE03 RID: 65027 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x0600FE04 RID: 65028 RVA: 0x00293ECF File Offset: 0x002920CF
		public TaskGroupCategory()
		{
		}

		// Token: 0x0600FE05 RID: 65029 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TaskGroupCategory(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE06 RID: 65030 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TaskGroupCategory(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE07 RID: 65031 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TaskGroupCategory(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FE08 RID: 65032 RVA: 0x002DCB17 File Offset: 0x002DAD17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "task" == name)
			{
				return new TaskGroupTask();
			}
			return null;
		}

		// Token: 0x0600FE09 RID: 65033 RVA: 0x002DCB34 File Offset: 0x002DAD34
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
			if (namespaceId == 0 && "idMso" == name)
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FE0A RID: 65034 RVA: 0x002DCC51 File Offset: 0x002DAE51
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskGroupCategory>(deep);
		}

		// Token: 0x0600FE0B RID: 65035 RVA: 0x002DCC5C File Offset: 0x002DAE5C
		// Note: this type is marked as 'beforefieldinit'.
		static TaskGroupCategory()
		{
			byte[] array = new byte[12];
			TaskGroupCategory.attributeNamespaceIds = array;
		}

		// Token: 0x04007217 RID: 29207
		private const string tagName = "category";

		// Token: 0x04007218 RID: 29208
		private const byte tagNsId = 57;

		// Token: 0x04007219 RID: 29209
		internal const int ElementTypeIdConst = 13103;

		// Token: 0x0400721A RID: 29210
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible",
			"label", "getLabel"
		};

		// Token: 0x0400721B RID: 29211
		private static byte[] attributeNamespaceIds;
	}
}
