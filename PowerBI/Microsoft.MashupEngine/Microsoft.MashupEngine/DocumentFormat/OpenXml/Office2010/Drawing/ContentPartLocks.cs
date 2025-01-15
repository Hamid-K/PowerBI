using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002351 RID: 9041
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ContentPartLocks : OpenXmlCompositeElement
	{
		// Token: 0x170049DB RID: 18907
		// (get) Token: 0x06010353 RID: 66387 RVA: 0x002E0FF6 File Offset: 0x002DF1F6
		public override string LocalName
		{
			get
			{
				return "cpLocks";
			}
		}

		// Token: 0x170049DC RID: 18908
		// (get) Token: 0x06010354 RID: 66388 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049DD RID: 18909
		// (get) Token: 0x06010355 RID: 66389 RVA: 0x002E0FFD File Offset: 0x002DF1FD
		internal override int ElementTypeId
		{
			get
			{
				return 12726;
			}
		}

		// Token: 0x06010356 RID: 66390 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170049DE RID: 18910
		// (get) Token: 0x06010357 RID: 66391 RVA: 0x002E1004 File Offset: 0x002DF204
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPartLocks.attributeTagNames;
			}
		}

		// Token: 0x170049DF RID: 18911
		// (get) Token: 0x06010358 RID: 66392 RVA: 0x002E100B File Offset: 0x002DF20B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPartLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x170049E0 RID: 18912
		// (get) Token: 0x06010359 RID: 66393 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601035A RID: 66394 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "noGrp")]
		public BooleanValue NoGrouping
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

		// Token: 0x170049E1 RID: 18913
		// (get) Token: 0x0601035B RID: 66395 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601035C RID: 66396 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "noSelect")]
		public BooleanValue NoSelection
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

		// Token: 0x170049E2 RID: 18914
		// (get) Token: 0x0601035D RID: 66397 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601035E RID: 66398 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "noRot")]
		public BooleanValue NoRotation
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

		// Token: 0x170049E3 RID: 18915
		// (get) Token: 0x0601035F RID: 66399 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010360 RID: 66400 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "noChangeAspect")]
		public BooleanValue NoChangeAspect
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

		// Token: 0x170049E4 RID: 18916
		// (get) Token: 0x06010361 RID: 66401 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010362 RID: 66402 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "noMove")]
		public BooleanValue NoMove
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

		// Token: 0x170049E5 RID: 18917
		// (get) Token: 0x06010363 RID: 66403 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06010364 RID: 66404 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "noResize")]
		public BooleanValue NoResize
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

		// Token: 0x170049E6 RID: 18918
		// (get) Token: 0x06010365 RID: 66405 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010366 RID: 66406 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "noEditPoints")]
		public BooleanValue NoEditPoints
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

		// Token: 0x170049E7 RID: 18919
		// (get) Token: 0x06010367 RID: 66407 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06010368 RID: 66408 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "noAdjustHandles")]
		public BooleanValue NoAdjustHandles
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

		// Token: 0x170049E8 RID: 18920
		// (get) Token: 0x06010369 RID: 66409 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601036A RID: 66410 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "noChangeArrowheads")]
		public BooleanValue NoChangeArrowheads
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

		// Token: 0x170049E9 RID: 18921
		// (get) Token: 0x0601036B RID: 66411 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601036C RID: 66412 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "noChangeShapeType")]
		public BooleanValue NoChangeShapeType
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

		// Token: 0x0601036D RID: 66413 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPartLocks()
		{
		}

		// Token: 0x0601036E RID: 66414 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPartLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601036F RID: 66415 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPartLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010370 RID: 66416 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPartLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010371 RID: 66417 RVA: 0x002E1012 File Offset: 0x002DF212
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x170049EA RID: 18922
		// (get) Token: 0x06010372 RID: 66418 RVA: 0x002E102D File Offset: 0x002DF22D
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPartLocks.eleTagNames;
			}
		}

		// Token: 0x170049EB RID: 18923
		// (get) Token: 0x06010373 RID: 66419 RVA: 0x002E1034 File Offset: 0x002DF234
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPartLocks.eleNamespaceIds;
			}
		}

		// Token: 0x170049EC RID: 18924
		// (get) Token: 0x06010374 RID: 66420 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170049ED RID: 18925
		// (get) Token: 0x06010375 RID: 66421 RVA: 0x002E103B File Offset: 0x002DF23B
		// (set) Token: 0x06010376 RID: 66422 RVA: 0x002E1044 File Offset: 0x002DF244
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(0);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(0, value);
			}
		}

		// Token: 0x06010377 RID: 66423 RVA: 0x002E1050 File Offset: 0x002DF250
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "noGrp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noSelect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noRot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeAspect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noMove" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noResize" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noEditPoints" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noAdjustHandles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeArrowheads" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeShapeType" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010378 RID: 66424 RVA: 0x002E1141 File Offset: 0x002DF341
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPartLocks>(deep);
		}

		// Token: 0x06010379 RID: 66425 RVA: 0x002E114C File Offset: 0x002DF34C
		// Note: this type is marked as 'beforefieldinit'.
		static ContentPartLocks()
		{
			byte[] array = new byte[10];
			ContentPartLocks.attributeNamespaceIds = array;
			ContentPartLocks.eleTagNames = new string[] { "extLst" };
			ContentPartLocks.eleNamespaceIds = new byte[] { 48 };
		}

		// Token: 0x0400738E RID: 29582
		private const string tagName = "cpLocks";

		// Token: 0x0400738F RID: 29583
		private const byte tagNsId = 48;

		// Token: 0x04007390 RID: 29584
		internal const int ElementTypeIdConst = 12726;

		// Token: 0x04007391 RID: 29585
		private static string[] attributeTagNames = new string[] { "noGrp", "noSelect", "noRot", "noChangeAspect", "noMove", "noResize", "noEditPoints", "noAdjustHandles", "noChangeArrowheads", "noChangeShapeType" };

		// Token: 0x04007392 RID: 29586
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007393 RID: 29587
		private static readonly string[] eleTagNames;

		// Token: 0x04007394 RID: 29588
		private static readonly byte[] eleNamespaceIds;
	}
}
