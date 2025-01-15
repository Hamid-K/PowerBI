using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002303 RID: 8963
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TaskFormGroupTask), FileFormatVersions.Office2010)]
	internal class TaskFormGroupCategory : OpenXmlCompositeElement
	{
		// Token: 0x1700478F RID: 18319
		// (get) Token: 0x0600FE44 RID: 65092 RVA: 0x002DCAFB File Offset: 0x002DACFB
		public override string LocalName
		{
			get
			{
				return "category";
			}
		}

		// Token: 0x17004790 RID: 18320
		// (get) Token: 0x0600FE45 RID: 65093 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004791 RID: 18321
		// (get) Token: 0x0600FE46 RID: 65094 RVA: 0x002DD00B File Offset: 0x002DB20B
		internal override int ElementTypeId
		{
			get
			{
				return 13105;
			}
		}

		// Token: 0x0600FE47 RID: 65095 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004792 RID: 18322
		// (get) Token: 0x0600FE48 RID: 65096 RVA: 0x002DD012 File Offset: 0x002DB212
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskFormGroupCategory.attributeTagNames;
			}
		}

		// Token: 0x17004793 RID: 18323
		// (get) Token: 0x0600FE49 RID: 65097 RVA: 0x002DD019 File Offset: 0x002DB219
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskFormGroupCategory.attributeNamespaceIds;
			}
		}

		// Token: 0x17004794 RID: 18324
		// (get) Token: 0x0600FE4A RID: 65098 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FE4B RID: 65099 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004795 RID: 18325
		// (get) Token: 0x0600FE4C RID: 65100 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FE4D RID: 65101 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004796 RID: 18326
		// (get) Token: 0x0600FE4E RID: 65102 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FE4F RID: 65103 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004797 RID: 18327
		// (get) Token: 0x0600FE50 RID: 65104 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FE51 RID: 65105 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004798 RID: 18328
		// (get) Token: 0x0600FE52 RID: 65106 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FE53 RID: 65107 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004799 RID: 18329
		// (get) Token: 0x0600FE54 RID: 65108 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FE55 RID: 65109 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700479A RID: 18330
		// (get) Token: 0x0600FE56 RID: 65110 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FE57 RID: 65111 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700479B RID: 18331
		// (get) Token: 0x0600FE58 RID: 65112 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FE59 RID: 65113 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700479C RID: 18332
		// (get) Token: 0x0600FE5A RID: 65114 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600FE5B RID: 65115 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x1700479D RID: 18333
		// (get) Token: 0x0600FE5C RID: 65116 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FE5D RID: 65117 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x1700479E RID: 18334
		// (get) Token: 0x0600FE5E RID: 65118 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FE5F RID: 65119 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x1700479F RID: 18335
		// (get) Token: 0x0600FE60 RID: 65120 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FE61 RID: 65121 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x0600FE62 RID: 65122 RVA: 0x00293ECF File Offset: 0x002920CF
		public TaskFormGroupCategory()
		{
		}

		// Token: 0x0600FE63 RID: 65123 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TaskFormGroupCategory(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE64 RID: 65124 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TaskFormGroupCategory(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE65 RID: 65125 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TaskFormGroupCategory(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FE66 RID: 65126 RVA: 0x002DD020 File Offset: 0x002DB220
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "task" == name)
			{
				return new TaskFormGroupTask();
			}
			return null;
		}

		// Token: 0x0600FE67 RID: 65127 RVA: 0x002DD03C File Offset: 0x002DB23C
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

		// Token: 0x0600FE68 RID: 65128 RVA: 0x002DD159 File Offset: 0x002DB359
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskFormGroupCategory>(deep);
		}

		// Token: 0x0600FE69 RID: 65129 RVA: 0x002DD164 File Offset: 0x002DB364
		// Note: this type is marked as 'beforefieldinit'.
		static TaskFormGroupCategory()
		{
			byte[] array = new byte[12];
			TaskFormGroupCategory.attributeNamespaceIds = array;
		}

		// Token: 0x04007221 RID: 29217
		private const string tagName = "category";

		// Token: 0x04007222 RID: 29218
		private const byte tagNsId = 57;

		// Token: 0x04007223 RID: 29219
		internal const int ElementTypeIdConst = 13105;

		// Token: 0x04007224 RID: 29220
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible",
			"label", "getLabel"
		};

		// Token: 0x04007225 RID: 29221
		private static byte[] attributeNamespaceIds;
	}
}
