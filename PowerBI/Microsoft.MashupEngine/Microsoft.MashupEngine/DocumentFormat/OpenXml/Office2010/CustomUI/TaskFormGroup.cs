using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002305 RID: 8965
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TaskFormGroupCategory), FileFormatVersions.Office2010)]
	internal class TaskFormGroup : OpenXmlCompositeElement
	{
		// Token: 0x170047BA RID: 18362
		// (get) Token: 0x0600FEA2 RID: 65186 RVA: 0x002DD4E9 File Offset: 0x002DB6E9
		public override string LocalName
		{
			get
			{
				return "taskFormGroup";
			}
		}

		// Token: 0x170047BB RID: 18363
		// (get) Token: 0x0600FEA3 RID: 65187 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047BC RID: 18364
		// (get) Token: 0x0600FEA4 RID: 65188 RVA: 0x002DD4F0 File Offset: 0x002DB6F0
		internal override int ElementTypeId
		{
			get
			{
				return 13107;
			}
		}

		// Token: 0x0600FEA5 RID: 65189 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170047BD RID: 18365
		// (get) Token: 0x0600FEA6 RID: 65190 RVA: 0x002DD4F7 File Offset: 0x002DB6F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskFormGroup.attributeTagNames;
			}
		}

		// Token: 0x170047BE RID: 18366
		// (get) Token: 0x0600FEA7 RID: 65191 RVA: 0x002DD4FE File Offset: 0x002DB6FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskFormGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x170047BF RID: 18367
		// (get) Token: 0x0600FEA8 RID: 65192 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FEA9 RID: 65193 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170047C0 RID: 18368
		// (get) Token: 0x0600FEAA RID: 65194 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FEAB RID: 65195 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170047C1 RID: 18369
		// (get) Token: 0x0600FEAC RID: 65196 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FEAD RID: 65197 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170047C2 RID: 18370
		// (get) Token: 0x0600FEAE RID: 65198 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FEAF RID: 65199 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170047C3 RID: 18371
		// (get) Token: 0x0600FEB0 RID: 65200 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FEB1 RID: 65201 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170047C4 RID: 18372
		// (get) Token: 0x0600FEB2 RID: 65202 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FEB3 RID: 65203 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170047C5 RID: 18373
		// (get) Token: 0x0600FEB4 RID: 65204 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0600FEB5 RID: 65205 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170047C6 RID: 18374
		// (get) Token: 0x0600FEB6 RID: 65206 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FEB7 RID: 65207 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170047C7 RID: 18375
		// (get) Token: 0x0600FEB8 RID: 65208 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FEB9 RID: 65209 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "helperText")]
		public StringValue HelperText
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

		// Token: 0x170047C8 RID: 18376
		// (get) Token: 0x0600FEBA RID: 65210 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FEBB RID: 65211 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getHelperText")]
		public StringValue GetHelperText
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

		// Token: 0x170047C9 RID: 18377
		// (get) Token: 0x0600FEBC RID: 65212 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600FEBD RID: 65213 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x170047CA RID: 18378
		// (get) Token: 0x0600FEBE RID: 65214 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FEBF RID: 65215 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x170047CB RID: 18379
		// (get) Token: 0x0600FEC0 RID: 65216 RVA: 0x002DD505 File Offset: 0x002DB705
		// (set) Token: 0x0600FEC1 RID: 65217 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "allowedTaskSizes")]
		public EnumValue<TaskSizesValues> AllowedTaskSizes
		{
			get
			{
				return (EnumValue<TaskSizesValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x0600FEC2 RID: 65218 RVA: 0x00293ECF File Offset: 0x002920CF
		public TaskFormGroup()
		{
		}

		// Token: 0x0600FEC3 RID: 65219 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TaskFormGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FEC4 RID: 65220 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TaskFormGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FEC5 RID: 65221 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TaskFormGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FEC6 RID: 65222 RVA: 0x002DD515 File Offset: 0x002DB715
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "category" == name)
			{
				return new TaskFormGroupCategory();
			}
			return null;
		}

		// Token: 0x0600FEC7 RID: 65223 RVA: 0x002DD530 File Offset: 0x002DB730
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

		// Token: 0x0600FEC8 RID: 65224 RVA: 0x002DD663 File Offset: 0x002DB863
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskFormGroup>(deep);
		}

		// Token: 0x0600FEC9 RID: 65225 RVA: 0x002DD66C File Offset: 0x002DB86C
		// Note: this type is marked as 'beforefieldinit'.
		static TaskFormGroup()
		{
			byte[] array = new byte[13];
			TaskFormGroup.attributeNamespaceIds = array;
		}

		// Token: 0x0400722B RID: 29227
		private const string tagName = "taskFormGroup";

		// Token: 0x0400722C RID: 29228
		private const byte tagNsId = 57;

		// Token: 0x0400722D RID: 29229
		internal const int ElementTypeIdConst = 13107;

		// Token: 0x0400722E RID: 29230
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "label", "getLabel", "visible", "getVisible", "helperText", "getHelperText",
			"showLabel", "getShowLabel", "allowedTaskSizes"
		};

		// Token: 0x0400722F RID: 29231
		private static byte[] attributeNamespaceIds;
	}
}
