using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002304 RID: 8964
	[ChildElementInfo(typeof(BackstageGroup), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TaskFormGroupTask : OpenXmlCompositeElement
	{
		// Token: 0x170047A0 RID: 18336
		// (get) Token: 0x0600FE6A RID: 65130 RVA: 0x002DCCE8 File Offset: 0x002DAEE8
		public override string LocalName
		{
			get
			{
				return "task";
			}
		}

		// Token: 0x170047A1 RID: 18337
		// (get) Token: 0x0600FE6B RID: 65131 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047A2 RID: 18338
		// (get) Token: 0x0600FE6C RID: 65132 RVA: 0x002DD1F0 File Offset: 0x002DB3F0
		internal override int ElementTypeId
		{
			get
			{
				return 13106;
			}
		}

		// Token: 0x0600FE6D RID: 65133 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170047A3 RID: 18339
		// (get) Token: 0x0600FE6E RID: 65134 RVA: 0x002DD1F7 File Offset: 0x002DB3F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskFormGroupTask.attributeTagNames;
			}
		}

		// Token: 0x170047A4 RID: 18340
		// (get) Token: 0x0600FE6F RID: 65135 RVA: 0x002DD1FE File Offset: 0x002DB3FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskFormGroupTask.attributeNamespaceIds;
			}
		}

		// Token: 0x170047A5 RID: 18341
		// (get) Token: 0x0600FE70 RID: 65136 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FE71 RID: 65137 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170047A6 RID: 18342
		// (get) Token: 0x0600FE72 RID: 65138 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FE73 RID: 65139 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170047A7 RID: 18343
		// (get) Token: 0x0600FE74 RID: 65140 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FE75 RID: 65141 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170047A8 RID: 18344
		// (get) Token: 0x0600FE76 RID: 65142 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FE77 RID: 65143 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170047A9 RID: 18345
		// (get) Token: 0x0600FE78 RID: 65144 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FE79 RID: 65145 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170047AA RID: 18346
		// (get) Token: 0x0600FE7A RID: 65146 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FE7B RID: 65147 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170047AB RID: 18347
		// (get) Token: 0x0600FE7C RID: 65148 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FE7D RID: 65149 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170047AC RID: 18348
		// (get) Token: 0x0600FE7E RID: 65150 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FE7F RID: 65151 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170047AD RID: 18349
		// (get) Token: 0x0600FE80 RID: 65152 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FE81 RID: 65153 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170047AE RID: 18350
		// (get) Token: 0x0600FE82 RID: 65154 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FE83 RID: 65155 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170047AF RID: 18351
		// (get) Token: 0x0600FE84 RID: 65156 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FE85 RID: 65157 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170047B0 RID: 18352
		// (get) Token: 0x0600FE86 RID: 65158 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600FE87 RID: 65159 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170047B1 RID: 18353
		// (get) Token: 0x0600FE88 RID: 65160 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FE89 RID: 65161 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170047B2 RID: 18354
		// (get) Token: 0x0600FE8A RID: 65162 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FE8B RID: 65163 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170047B3 RID: 18355
		// (get) Token: 0x0600FE8C RID: 65164 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FE8D RID: 65165 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170047B4 RID: 18356
		// (get) Token: 0x0600FE8E RID: 65166 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x0600FE8F RID: 65167 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170047B5 RID: 18357
		// (get) Token: 0x0600FE90 RID: 65168 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FE91 RID: 65169 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170047B6 RID: 18358
		// (get) Token: 0x0600FE92 RID: 65170 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FE93 RID: 65171 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "description")]
		public StringValue Description
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170047B7 RID: 18359
		// (get) Token: 0x0600FE94 RID: 65172 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FE95 RID: 65173 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170047B8 RID: 18360
		// (get) Token: 0x0600FE96 RID: 65174 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FE97 RID: 65175 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170047B9 RID: 18361
		// (get) Token: 0x0600FE98 RID: 65176 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FE99 RID: 65177 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x0600FE9A RID: 65178 RVA: 0x00293ECF File Offset: 0x002920CF
		public TaskFormGroupTask()
		{
		}

		// Token: 0x0600FE9B RID: 65179 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TaskFormGroupTask(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE9C RID: 65180 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TaskFormGroupTask(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FE9D RID: 65181 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TaskFormGroupTask(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FE9E RID: 65182 RVA: 0x002DD205 File Offset: 0x002DB405
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "group" == name)
			{
				return new BackstageGroup();
			}
			return null;
		}

		// Token: 0x0600FE9F RID: 65183 RVA: 0x002DD220 File Offset: 0x002DB420
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
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FEA0 RID: 65184 RVA: 0x002DD403 File Offset: 0x002DB603
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskFormGroupTask>(deep);
		}

		// Token: 0x0600FEA1 RID: 65185 RVA: 0x002DD40C File Offset: 0x002DB60C
		// Note: this type is marked as 'beforefieldinit'.
		static TaskFormGroupTask()
		{
			byte[] array = new byte[21];
			TaskFormGroupTask.attributeNamespaceIds = array;
		}

		// Token: 0x04007226 RID: 29222
		private const string tagName = "task";

		// Token: 0x04007227 RID: 29223
		private const byte tagNsId = 57;

		// Token: 0x04007228 RID: 29224
		internal const int ElementTypeIdConst = 13106;

		// Token: 0x04007229 RID: 29225
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "image", "imageMso",
			"getImage", "enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "description", "getDescription", "keytip",
			"getKeytip"
		};

		// Token: 0x0400722A RID: 29226
		private static byte[] attributeNamespaceIds;
	}
}
