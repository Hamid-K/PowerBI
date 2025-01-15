using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C86 RID: 11398
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DrawingHeaderFooter : OpenXmlLeafElement
	{
		// Token: 0x1700836B RID: 33643
		// (get) Token: 0x0601847D RID: 99453 RVA: 0x003400DC File Offset: 0x0033E2DC
		public override string LocalName
		{
			get
			{
				return "drawingHF";
			}
		}

		// Token: 0x1700836C RID: 33644
		// (get) Token: 0x0601847E RID: 99454 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700836D RID: 33645
		// (get) Token: 0x0601847F RID: 99455 RVA: 0x003400E3 File Offset: 0x0033E2E3
		internal override int ElementTypeId
		{
			get
			{
				return 11377;
			}
		}

		// Token: 0x06018480 RID: 99456 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700836E RID: 33646
		// (get) Token: 0x06018481 RID: 99457 RVA: 0x003400EA File Offset: 0x0033E2EA
		internal override string[] AttributeTagNames
		{
			get
			{
				return DrawingHeaderFooter.attributeTagNames;
			}
		}

		// Token: 0x1700836F RID: 33647
		// (get) Token: 0x06018482 RID: 99458 RVA: 0x003400F1 File Offset: 0x0033E2F1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DrawingHeaderFooter.attributeNamespaceIds;
			}
		}

		// Token: 0x17008370 RID: 33648
		// (get) Token: 0x06018483 RID: 99459 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018484 RID: 99460 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x17008371 RID: 33649
		// (get) Token: 0x06018485 RID: 99461 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06018486 RID: 99462 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lho")]
		public UInt32Value Lho
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008372 RID: 33650
		// (get) Token: 0x06018487 RID: 99463 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06018488 RID: 99464 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lhe")]
		public UInt32Value Lhe
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008373 RID: 33651
		// (get) Token: 0x06018489 RID: 99465 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601848A RID: 99466 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "lhf")]
		public UInt32Value Lhf
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008374 RID: 33652
		// (get) Token: 0x0601848B RID: 99467 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601848C RID: 99468 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cho")]
		public UInt32Value Cho
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008375 RID: 33653
		// (get) Token: 0x0601848D RID: 99469 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x0601848E RID: 99470 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "che")]
		public UInt32Value Che
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008376 RID: 33654
		// (get) Token: 0x0601848F RID: 99471 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06018490 RID: 99472 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "chf")]
		public UInt32Value Chf
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008377 RID: 33655
		// (get) Token: 0x06018491 RID: 99473 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x06018492 RID: 99474 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "rho")]
		public UInt32Value Rho
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008378 RID: 33656
		// (get) Token: 0x06018493 RID: 99475 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06018494 RID: 99476 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "rhe")]
		public UInt32Value Rhe
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008379 RID: 33657
		// (get) Token: 0x06018495 RID: 99477 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06018496 RID: 99478 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "rhf")]
		public UInt32Value Rhf
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700837A RID: 33658
		// (get) Token: 0x06018497 RID: 99479 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x06018498 RID: 99480 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "lfo")]
		public UInt32Value Lfo
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700837B RID: 33659
		// (get) Token: 0x06018499 RID: 99481 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x0601849A RID: 99482 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "lfe")]
		public UInt32Value Lfe
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700837C RID: 33660
		// (get) Token: 0x0601849B RID: 99483 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x0601849C RID: 99484 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "lff")]
		public UInt32Value Lff
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700837D RID: 33661
		// (get) Token: 0x0601849D RID: 99485 RVA: 0x0032C7AF File Offset: 0x0032A9AF
		// (set) Token: 0x0601849E RID: 99486 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "cfo")]
		public UInt32Value Cfo
		{
			get
			{
				return (UInt32Value)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x1700837E RID: 33662
		// (get) Token: 0x0601849F RID: 99487 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x060184A0 RID: 99488 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "cfe")]
		public UInt32Value Cfe
		{
			get
			{
				return (UInt32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x1700837F RID: 33663
		// (get) Token: 0x060184A1 RID: 99489 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x060184A2 RID: 99490 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "cff")]
		public UInt32Value Cff
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17008380 RID: 33664
		// (get) Token: 0x060184A3 RID: 99491 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x060184A4 RID: 99492 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "rfo")]
		public UInt32Value Rfo
		{
			get
			{
				return (UInt32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17008381 RID: 33665
		// (get) Token: 0x060184A5 RID: 99493 RVA: 0x0030F16C File Offset: 0x0030D36C
		// (set) Token: 0x060184A6 RID: 99494 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "rfe")]
		public UInt32Value Rfe
		{
			get
			{
				return (UInt32Value)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17008382 RID: 33666
		// (get) Token: 0x060184A7 RID: 99495 RVA: 0x003389D0 File Offset: 0x00336BD0
		// (set) Token: 0x060184A8 RID: 99496 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "rff")]
		public UInt32Value Rff
		{
			get
			{
				return (UInt32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x060184AA RID: 99498 RVA: 0x003400F8 File Offset: 0x0033E2F8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lho" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "lhe" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "lhf" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cho" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "che" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "chf" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rho" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rhe" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rhf" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "lfo" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "lfe" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "lff" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cfo" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cfe" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cff" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rfo" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rfe" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rff" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060184AB RID: 99499 RVA: 0x003402B1 File Offset: 0x0033E4B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DrawingHeaderFooter>(deep);
		}

		// Token: 0x060184AC RID: 99500 RVA: 0x003402BC File Offset: 0x0033E4BC
		// Note: this type is marked as 'beforefieldinit'.
		static DrawingHeaderFooter()
		{
			byte[] array = new byte[19];
			array[0] = 19;
			DrawingHeaderFooter.attributeNamespaceIds = array;
		}

		// Token: 0x04009FA3 RID: 40867
		private const string tagName = "drawingHF";

		// Token: 0x04009FA4 RID: 40868
		private const byte tagNsId = 22;

		// Token: 0x04009FA5 RID: 40869
		internal const int ElementTypeIdConst = 11377;

		// Token: 0x04009FA6 RID: 40870
		private static string[] attributeTagNames = new string[]
		{
			"id", "lho", "lhe", "lhf", "cho", "che", "chf", "rho", "rhe", "rhf",
			"lfo", "lfe", "lff", "cfo", "cfe", "cff", "rfo", "rfe", "rff"
		};

		// Token: 0x04009FA7 RID: 40871
		private static byte[] attributeNamespaceIds;
	}
}
