using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7D RID: 11389
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ObjectAnchor), FileFormatVersions.Office2010)]
	internal class EmbeddedObjectProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700832A RID: 33578
		// (get) Token: 0x060183F0 RID: 99312 RVA: 0x0033FB6C File Offset: 0x0033DD6C
		public override string LocalName
		{
			get
			{
				return "objectPr";
			}
		}

		// Token: 0x1700832B RID: 33579
		// (get) Token: 0x060183F1 RID: 99313 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700832C RID: 33580
		// (get) Token: 0x060183F2 RID: 99314 RVA: 0x0033FB73 File Offset: 0x0033DD73
		internal override int ElementTypeId
		{
			get
			{
				return 11369;
			}
		}

		// Token: 0x060183F3 RID: 99315 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700832D RID: 33581
		// (get) Token: 0x060183F4 RID: 99316 RVA: 0x0033FB7A File Offset: 0x0033DD7A
		internal override string[] AttributeTagNames
		{
			get
			{
				return EmbeddedObjectProperties.attributeTagNames;
			}
		}

		// Token: 0x1700832E RID: 33582
		// (get) Token: 0x060183F5 RID: 99317 RVA: 0x0033FB81 File Offset: 0x0033DD81
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EmbeddedObjectProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700832F RID: 33583
		// (get) Token: 0x060183F6 RID: 99318 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060183F7 RID: 99319 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008330 RID: 33584
		// (get) Token: 0x060183F8 RID: 99320 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060183F9 RID: 99321 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008331 RID: 33585
		// (get) Token: 0x060183FA RID: 99322 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060183FB RID: 99323 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17008332 RID: 33586
		// (get) Token: 0x060183FC RID: 99324 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060183FD RID: 99325 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17008333 RID: 33587
		// (get) Token: 0x060183FE RID: 99326 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060183FF RID: 99327 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "uiObject")]
		public BooleanValue UiObject
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

		// Token: 0x17008334 RID: 33588
		// (get) Token: 0x06018400 RID: 99328 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06018401 RID: 99329 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "autoFill")]
		public BooleanValue AutoFill
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

		// Token: 0x17008335 RID: 33589
		// (get) Token: 0x06018402 RID: 99330 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06018403 RID: 99331 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "autoLine")]
		public BooleanValue AutoLine
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

		// Token: 0x17008336 RID: 33590
		// (get) Token: 0x06018404 RID: 99332 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06018405 RID: 99333 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "autoPict")]
		public BooleanValue AutoPict
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

		// Token: 0x17008337 RID: 33591
		// (get) Token: 0x06018406 RID: 99334 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06018407 RID: 99335 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x17008338 RID: 33592
		// (get) Token: 0x06018408 RID: 99336 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06018409 RID: 99337 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "altText")]
		public StringValue AltText
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

		// Token: 0x17008339 RID: 33593
		// (get) Token: 0x0601840A RID: 99338 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0601840B RID: 99339 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "dde")]
		public BooleanValue Dde
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

		// Token: 0x1700833A RID: 33594
		// (get) Token: 0x0601840C RID: 99340 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601840D RID: 99341 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0601840E RID: 99342 RVA: 0x00293ECF File Offset: 0x002920CF
		public EmbeddedObjectProperties()
		{
		}

		// Token: 0x0601840F RID: 99343 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EmbeddedObjectProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018410 RID: 99344 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EmbeddedObjectProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018411 RID: 99345 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EmbeddedObjectProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018412 RID: 99346 RVA: 0x0033F8F0 File Offset: 0x0033DAF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "anchor" == name)
			{
				return new ObjectAnchor();
			}
			return null;
		}

		// Token: 0x1700833B RID: 33595
		// (get) Token: 0x06018413 RID: 99347 RVA: 0x0033FB88 File Offset: 0x0033DD88
		internal override string[] ElementTagNames
		{
			get
			{
				return EmbeddedObjectProperties.eleTagNames;
			}
		}

		// Token: 0x1700833C RID: 33596
		// (get) Token: 0x06018414 RID: 99348 RVA: 0x0033FB8F File Offset: 0x0033DD8F
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EmbeddedObjectProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700833D RID: 33597
		// (get) Token: 0x06018415 RID: 99349 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700833E RID: 33598
		// (get) Token: 0x06018416 RID: 99350 RVA: 0x0033F919 File Offset: 0x0033DB19
		// (set) Token: 0x06018417 RID: 99351 RVA: 0x0033F922 File Offset: 0x0033DB22
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

		// Token: 0x06018418 RID: 99352 RVA: 0x0033FB98 File Offset: 0x0033DD98
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
			if (namespaceId == 0 && "dde" == name)
			{
				return new BooleanValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018419 RID: 99353 RVA: 0x0033FCB7 File Offset: 0x0033DEB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbeddedObjectProperties>(deep);
		}

		// Token: 0x04009F7D RID: 40829
		private const string tagName = "objectPr";

		// Token: 0x04009F7E RID: 40830
		private const byte tagNsId = 22;

		// Token: 0x04009F7F RID: 40831
		internal const int ElementTypeIdConst = 11369;

		// Token: 0x04009F80 RID: 40832
		private static string[] attributeTagNames = new string[]
		{
			"locked", "defaultSize", "print", "disabled", "uiObject", "autoFill", "autoLine", "autoPict", "macro", "altText",
			"dde", "id"
		};

		// Token: 0x04009F81 RID: 40833
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 19
		};

		// Token: 0x04009F82 RID: 40834
		private static readonly string[] eleTagNames = new string[] { "anchor" };

		// Token: 0x04009F83 RID: 40835
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
