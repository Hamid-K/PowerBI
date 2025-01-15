using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E3 RID: 8931
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TaskGroupCategory), FileFormatVersions.Office2010)]
	internal class TaskGroup : OpenXmlCompositeElement
	{
		// Token: 0x17004601 RID: 17921
		// (get) Token: 0x0600FADB RID: 64219 RVA: 0x002DA162 File Offset: 0x002D8362
		public override string LocalName
		{
			get
			{
				return "taskGroup";
			}
		}

		// Token: 0x17004602 RID: 17922
		// (get) Token: 0x0600FADC RID: 64220 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004603 RID: 17923
		// (get) Token: 0x0600FADD RID: 64221 RVA: 0x002DA169 File Offset: 0x002D8369
		internal override int ElementTypeId
		{
			get
			{
				return 13076;
			}
		}

		// Token: 0x0600FADE RID: 64222 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004604 RID: 17924
		// (get) Token: 0x0600FADF RID: 64223 RVA: 0x002DA170 File Offset: 0x002D8370
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskGroup.attributeTagNames;
			}
		}

		// Token: 0x17004605 RID: 17925
		// (get) Token: 0x0600FAE0 RID: 64224 RVA: 0x002DA177 File Offset: 0x002D8377
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17004606 RID: 17926
		// (get) Token: 0x0600FAE1 RID: 64225 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FAE2 RID: 64226 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004607 RID: 17927
		// (get) Token: 0x0600FAE3 RID: 64227 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FAE4 RID: 64228 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004608 RID: 17928
		// (get) Token: 0x0600FAE5 RID: 64229 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FAE6 RID: 64230 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004609 RID: 17929
		// (get) Token: 0x0600FAE7 RID: 64231 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FAE8 RID: 64232 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700460A RID: 17930
		// (get) Token: 0x0600FAE9 RID: 64233 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FAEA RID: 64234 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700460B RID: 17931
		// (get) Token: 0x0600FAEB RID: 64235 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FAEC RID: 64236 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700460C RID: 17932
		// (get) Token: 0x0600FAED RID: 64237 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FAEE RID: 64238 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700460D RID: 17933
		// (get) Token: 0x0600FAEF RID: 64239 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FAF0 RID: 64240 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700460E RID: 17934
		// (get) Token: 0x0600FAF1 RID: 64241 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FAF2 RID: 64242 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x1700460F RID: 17935
		// (get) Token: 0x0600FAF3 RID: 64243 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FAF4 RID: 64244 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004610 RID: 17936
		// (get) Token: 0x0600FAF5 RID: 64245 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600FAF6 RID: 64246 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004611 RID: 17937
		// (get) Token: 0x0600FAF7 RID: 64247 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FAF8 RID: 64248 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004612 RID: 17938
		// (get) Token: 0x0600FAF9 RID: 64249 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FAFA RID: 64250 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "helperText")]
		public StringValue HelperText
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004613 RID: 17939
		// (get) Token: 0x0600FAFB RID: 64251 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FAFC RID: 64252 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getHelperText")]
		public StringValue GetHelperText
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004614 RID: 17940
		// (get) Token: 0x0600FAFD RID: 64253 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600FAFE RID: 64254 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004615 RID: 17941
		// (get) Token: 0x0600FAFF RID: 64255 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FB00 RID: 64256 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004616 RID: 17942
		// (get) Token: 0x0600FB01 RID: 64257 RVA: 0x002DA17E File Offset: 0x002D837E
		// (set) Token: 0x0600FB02 RID: 64258 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "allowedTaskSizes")]
		public EnumValue<TaskSizesValues> AllowedTaskSizes
		{
			get
			{
				return (EnumValue<TaskSizesValues>)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x0600FB03 RID: 64259 RVA: 0x00293ECF File Offset: 0x002920CF
		public TaskGroup()
		{
		}

		// Token: 0x0600FB04 RID: 64260 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TaskGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB05 RID: 64261 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TaskGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FB06 RID: 64262 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TaskGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FB07 RID: 64263 RVA: 0x002DA18E File Offset: 0x002D838E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "category" == name)
			{
				return new TaskGroupCategory();
			}
			return null;
		}

		// Token: 0x0600FB08 RID: 64264 RVA: 0x002DA1AC File Offset: 0x002D83AC
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
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
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
			if (namespaceId == 0 && "helperText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getHelperText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "allowedTaskSizes" == name)
			{
				return new EnumValue<TaskSizesValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FB09 RID: 64265 RVA: 0x002DA337 File Offset: 0x002D8537
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskGroup>(deep);
		}

		// Token: 0x0600FB0A RID: 64266 RVA: 0x002DA340 File Offset: 0x002D8540
		// Note: this type is marked as 'beforefieldinit'.
		static TaskGroup()
		{
			byte[] array = new byte[17];
			TaskGroup.attributeNamespaceIds = array;
		}

		// Token: 0x0400719C RID: 29084
		private const string tagName = "taskGroup";

		// Token: 0x0400719D RID: 29085
		private const byte tagNsId = 57;

		// Token: 0x0400719E RID: 29086
		internal const int ElementTypeIdConst = 13076;

		// Token: 0x0400719F RID: 29087
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "label", "getLabel",
			"visible", "getVisible", "helperText", "getHelperText", "showLabel", "getShowLabel", "allowedTaskSizes"
		};

		// Token: 0x040071A0 RID: 29088
		private static byte[] attributeNamespaceIds;
	}
}
