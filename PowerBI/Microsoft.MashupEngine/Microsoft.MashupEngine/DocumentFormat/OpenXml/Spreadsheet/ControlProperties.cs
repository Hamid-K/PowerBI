using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7C RID: 11388
	[ChildElementInfo(typeof(ObjectAnchor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ControlProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008312 RID: 33554
		// (get) Token: 0x060183BF RID: 99263 RVA: 0x0033F8D4 File Offset: 0x0033DAD4
		public override string LocalName
		{
			get
			{
				return "controlPr";
			}
		}

		// Token: 0x17008313 RID: 33555
		// (get) Token: 0x060183C0 RID: 99264 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008314 RID: 33556
		// (get) Token: 0x060183C1 RID: 99265 RVA: 0x0033F8DB File Offset: 0x0033DADB
		internal override int ElementTypeId
		{
			get
			{
				return 11368;
			}
		}

		// Token: 0x060183C2 RID: 99266 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17008315 RID: 33557
		// (get) Token: 0x060183C3 RID: 99267 RVA: 0x0033F8E2 File Offset: 0x0033DAE2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ControlProperties.attributeTagNames;
			}
		}

		// Token: 0x17008316 RID: 33558
		// (get) Token: 0x060183C4 RID: 99268 RVA: 0x0033F8E9 File Offset: 0x0033DAE9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ControlProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17008317 RID: 33559
		// (get) Token: 0x060183C5 RID: 99269 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060183C6 RID: 99270 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "locked")]
		public BooleanValue Locked
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008318 RID: 33560
		// (get) Token: 0x060183C7 RID: 99271 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060183C8 RID: 99272 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "defaultSize")]
		public BooleanValue DefaultSize
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008319 RID: 33561
		// (get) Token: 0x060183C9 RID: 99273 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060183CA RID: 99274 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "print")]
		public BooleanValue Print
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700831A RID: 33562
		// (get) Token: 0x060183CB RID: 99275 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060183CC RID: 99276 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "disabled")]
		public BooleanValue Disabled
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

		// Token: 0x1700831B RID: 33563
		// (get) Token: 0x060183CD RID: 99277 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060183CE RID: 99278 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "recalcAlways")]
		public BooleanValue RecalcAlways
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700831C RID: 33564
		// (get) Token: 0x060183CF RID: 99279 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060183D0 RID: 99280 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "uiObject")]
		public BooleanValue UiObject
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700831D RID: 33565
		// (get) Token: 0x060183D1 RID: 99281 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060183D2 RID: 99282 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "autoFill")]
		public BooleanValue AutoFill
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

		// Token: 0x1700831E RID: 33566
		// (get) Token: 0x060183D3 RID: 99283 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060183D4 RID: 99284 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "autoLine")]
		public BooleanValue AutoLine
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700831F RID: 33567
		// (get) Token: 0x060183D5 RID: 99285 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060183D6 RID: 99286 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "autoPict")]
		public BooleanValue AutoPict
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

		// Token: 0x17008320 RID: 33568
		// (get) Token: 0x060183D7 RID: 99287 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x060183D8 RID: 99288 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x17008321 RID: 33569
		// (get) Token: 0x060183D9 RID: 99289 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x060183DA RID: 99290 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "altText")]
		public StringValue AltText
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

		// Token: 0x17008322 RID: 33570
		// (get) Token: 0x060183DB RID: 99291 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x060183DC RID: 99292 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "linkedCell")]
		public StringValue LinkedCell
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

		// Token: 0x17008323 RID: 33571
		// (get) Token: 0x060183DD RID: 99293 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x060183DE RID: 99294 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "listFillRange")]
		public StringValue ListFillRange
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

		// Token: 0x17008324 RID: 33572
		// (get) Token: 0x060183DF RID: 99295 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x060183E0 RID: 99296 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "cf")]
		public StringValue Cf
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

		// Token: 0x17008325 RID: 33573
		// (get) Token: 0x060183E1 RID: 99297 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x060183E2 RID: 99298 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x060183E3 RID: 99299 RVA: 0x00293ECF File Offset: 0x002920CF
		public ControlProperties()
		{
		}

		// Token: 0x060183E4 RID: 99300 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ControlProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183E5 RID: 99301 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ControlProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183E6 RID: 99302 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ControlProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060183E7 RID: 99303 RVA: 0x0033F8F0 File Offset: 0x0033DAF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "anchor" == name)
			{
				return new ObjectAnchor();
			}
			return null;
		}

		// Token: 0x17008326 RID: 33574
		// (get) Token: 0x060183E8 RID: 99304 RVA: 0x0033F90B File Offset: 0x0033DB0B
		internal override string[] ElementTagNames
		{
			get
			{
				return ControlProperties.eleTagNames;
			}
		}

		// Token: 0x17008327 RID: 33575
		// (get) Token: 0x060183E9 RID: 99305 RVA: 0x0033F912 File Offset: 0x0033DB12
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ControlProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008328 RID: 33576
		// (get) Token: 0x060183EA RID: 99306 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008329 RID: 33577
		// (get) Token: 0x060183EB RID: 99307 RVA: 0x0033F919 File Offset: 0x0033DB19
		// (set) Token: 0x060183EC RID: 99308 RVA: 0x0033F922 File Offset: 0x0033DB22
		public ObjectAnchor ObjectAnchor
		{
			get
			{
				return base.GetElement<ObjectAnchor>(0);
			}
			set
			{
				base.SetElement<ObjectAnchor>(0, value);
			}
		}

		// Token: 0x060183ED RID: 99309 RVA: 0x0033F92C File Offset: 0x0033DB2C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "locked" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultSize" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "print" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "disabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "recalcAlways" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "uiObject" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoFill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoLine" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoPict" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "altText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "linkedCell" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "listFillRange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cf" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060183EE RID: 99310 RVA: 0x0033FA8D File Offset: 0x0033DC8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlProperties>(deep);
		}

		// Token: 0x04009F76 RID: 40822
		private const string tagName = "controlPr";

		// Token: 0x04009F77 RID: 40823
		private const byte tagNsId = 22;

		// Token: 0x04009F78 RID: 40824
		internal const int ElementTypeIdConst = 11368;

		// Token: 0x04009F79 RID: 40825
		private static string[] attributeTagNames = new string[]
		{
			"locked", "defaultSize", "print", "disabled", "recalcAlways", "uiObject", "autoFill", "autoLine", "autoPict", "macro",
			"altText", "linkedCell", "listFillRange", "cf", "id"
		};

		// Token: 0x04009F7A RID: 40826
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 19
		};

		// Token: 0x04009F7B RID: 40827
		private static readonly string[] eleTagNames = new string[] { "anchor" };

		// Token: 0x04009F7C RID: 40828
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
