using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002302 RID: 8962
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TaskGroupTask : OpenXmlLeafElement
	{
		// Token: 0x17004773 RID: 18291
		// (get) Token: 0x0600FE0C RID: 65036 RVA: 0x002DCCE8 File Offset: 0x002DAEE8
		public override string LocalName
		{
			get
			{
				return "task";
			}
		}

		// Token: 0x17004774 RID: 18292
		// (get) Token: 0x0600FE0D RID: 65037 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004775 RID: 18293
		// (get) Token: 0x0600FE0E RID: 65038 RVA: 0x002DCCEF File Offset: 0x002DAEEF
		internal override int ElementTypeId
		{
			get
			{
				return 13104;
			}
		}

		// Token: 0x0600FE0F RID: 65039 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004776 RID: 18294
		// (get) Token: 0x0600FE10 RID: 65040 RVA: 0x002DCCF6 File Offset: 0x002DAEF6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TaskGroupTask.attributeTagNames;
			}
		}

		// Token: 0x17004777 RID: 18295
		// (get) Token: 0x0600FE11 RID: 65041 RVA: 0x002DCCFD File Offset: 0x002DAEFD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TaskGroupTask.attributeNamespaceIds;
			}
		}

		// Token: 0x17004778 RID: 18296
		// (get) Token: 0x0600FE12 RID: 65042 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FE13 RID: 65043 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004779 RID: 18297
		// (get) Token: 0x0600FE14 RID: 65044 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FE15 RID: 65045 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700477A RID: 18298
		// (get) Token: 0x0600FE16 RID: 65046 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FE17 RID: 65047 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700477B RID: 18299
		// (get) Token: 0x0600FE18 RID: 65048 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FE19 RID: 65049 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700477C RID: 18300
		// (get) Token: 0x0600FE1A RID: 65050 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FE1B RID: 65051 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x1700477D RID: 18301
		// (get) Token: 0x0600FE1C RID: 65052 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FE1D RID: 65053 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700477E RID: 18302
		// (get) Token: 0x0600FE1E RID: 65054 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FE1F RID: 65055 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x1700477F RID: 18303
		// (get) Token: 0x0600FE20 RID: 65056 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FE21 RID: 65057 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004780 RID: 18304
		// (get) Token: 0x0600FE22 RID: 65058 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FE23 RID: 65059 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004781 RID: 18305
		// (get) Token: 0x0600FE24 RID: 65060 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600FE25 RID: 65061 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "isDefinitive")]
		public BooleanValue IsDefinitive
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004782 RID: 18306
		// (get) Token: 0x0600FE26 RID: 65062 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FE27 RID: 65063 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004783 RID: 18307
		// (get) Token: 0x0600FE28 RID: 65064 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FE29 RID: 65065 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004784 RID: 18308
		// (get) Token: 0x0600FE2A RID: 65066 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FE2B RID: 65067 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004785 RID: 18309
		// (get) Token: 0x0600FE2C RID: 65068 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x0600FE2D RID: 65069 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004786 RID: 18310
		// (get) Token: 0x0600FE2E RID: 65070 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FE2F RID: 65071 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004787 RID: 18311
		// (get) Token: 0x0600FE30 RID: 65072 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FE31 RID: 65073 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004788 RID: 18312
		// (get) Token: 0x0600FE32 RID: 65074 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FE33 RID: 65075 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004789 RID: 18313
		// (get) Token: 0x0600FE34 RID: 65076 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600FE35 RID: 65077 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x1700478A RID: 18314
		// (get) Token: 0x0600FE36 RID: 65078 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FE37 RID: 65079 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700478B RID: 18315
		// (get) Token: 0x0600FE38 RID: 65080 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FE39 RID: 65081 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x1700478C RID: 18316
		// (get) Token: 0x0600FE3A RID: 65082 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600FE3B RID: 65083 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x1700478D RID: 18317
		// (get) Token: 0x0600FE3C RID: 65084 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600FE3D RID: 65085 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x1700478E RID: 18318
		// (get) Token: 0x0600FE3E RID: 65086 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600FE3F RID: 65087 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x0600FE41 RID: 65089 RVA: 0x002DCD04 File Offset: 0x002DAF04
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
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "isDefinitive" == name)
			{
				return new BooleanValue();
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

		// Token: 0x0600FE42 RID: 65090 RVA: 0x002DCF13 File Offset: 0x002DB113
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TaskGroupTask>(deep);
		}

		// Token: 0x0600FE43 RID: 65091 RVA: 0x002DCF1C File Offset: 0x002DB11C
		// Note: this type is marked as 'beforefieldinit'.
		static TaskGroupTask()
		{
			byte[] array = new byte[23];
			TaskGroupTask.attributeNamespaceIds = array;
		}

		// Token: 0x0400721C RID: 29212
		private const string tagName = "task";

		// Token: 0x0400721D RID: 29213
		private const byte tagNsId = 57;

		// Token: 0x0400721E RID: 29214
		internal const int ElementTypeIdConst = 13104;

		// Token: 0x0400721F RID: 29215
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "onAction", "isDefinitive",
			"image", "imageMso", "getImage", "enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "description",
			"getDescription", "keytip", "getKeytip"
		};

		// Token: 0x04007220 RID: 29216
		private static byte[] attributeNamespaceIds;
	}
}
