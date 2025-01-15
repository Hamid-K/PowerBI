using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B63 RID: 11107
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Groups))]
	internal class GroupLevel : OpenXmlCompositeElement
	{
		// Token: 0x170078E8 RID: 30952
		// (get) Token: 0x06016D6F RID: 93551 RVA: 0x0032FAE3 File Offset: 0x0032DCE3
		public override string LocalName
		{
			get
			{
				return "groupLevel";
			}
		}

		// Token: 0x170078E9 RID: 30953
		// (get) Token: 0x06016D70 RID: 93552 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078EA RID: 30954
		// (get) Token: 0x06016D71 RID: 93553 RVA: 0x0032FAEA File Offset: 0x0032DCEA
		internal override int ElementTypeId
		{
			get
			{
				return 11086;
			}
		}

		// Token: 0x06016D72 RID: 93554 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170078EB RID: 30955
		// (get) Token: 0x06016D73 RID: 93555 RVA: 0x0032FAF1 File Offset: 0x0032DCF1
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupLevel.attributeTagNames;
			}
		}

		// Token: 0x170078EC RID: 30956
		// (get) Token: 0x06016D74 RID: 93556 RVA: 0x0032FAF8 File Offset: 0x0032DCF8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupLevel.attributeNamespaceIds;
			}
		}

		// Token: 0x170078ED RID: 30957
		// (get) Token: 0x06016D75 RID: 93557 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016D76 RID: 93558 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueName")]
		public StringValue UniqueName
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

		// Token: 0x170078EE RID: 30958
		// (get) Token: 0x06016D77 RID: 93559 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016D78 RID: 93560 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caption")]
		public StringValue Caption
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

		// Token: 0x170078EF RID: 30959
		// (get) Token: 0x06016D79 RID: 93561 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016D7A RID: 93562 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "user")]
		public BooleanValue User
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

		// Token: 0x170078F0 RID: 30960
		// (get) Token: 0x06016D7B RID: 93563 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016D7C RID: 93564 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "customRollUp")]
		public BooleanValue CustomRollUp
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

		// Token: 0x06016D7D RID: 93565 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupLevel()
		{
		}

		// Token: 0x06016D7E RID: 93566 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupLevel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D7F RID: 93567 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupLevel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016D80 RID: 93568 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupLevel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016D81 RID: 93569 RVA: 0x0032FAFF File Offset: 0x0032DCFF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "groups" == name)
			{
				return new Groups();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170078F1 RID: 30961
		// (get) Token: 0x06016D82 RID: 93570 RVA: 0x0032FB32 File Offset: 0x0032DD32
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupLevel.eleTagNames;
			}
		}

		// Token: 0x170078F2 RID: 30962
		// (get) Token: 0x06016D83 RID: 93571 RVA: 0x0032FB39 File Offset: 0x0032DD39
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupLevel.eleNamespaceIds;
			}
		}

		// Token: 0x170078F3 RID: 30963
		// (get) Token: 0x06016D84 RID: 93572 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170078F4 RID: 30964
		// (get) Token: 0x06016D85 RID: 93573 RVA: 0x0032FB40 File Offset: 0x0032DD40
		// (set) Token: 0x06016D86 RID: 93574 RVA: 0x0032FB49 File Offset: 0x0032DD49
		public Groups Groups
		{
			get
			{
				return base.GetElement<Groups>(0);
			}
			set
			{
				base.SetElement<Groups>(0, value);
			}
		}

		// Token: 0x170078F5 RID: 30965
		// (get) Token: 0x06016D87 RID: 93575 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06016D88 RID: 93576 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06016D89 RID: 93577 RVA: 0x0032FB54 File Offset: 0x0032DD54
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "user" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customRollUp" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016D8A RID: 93578 RVA: 0x0032FBC1 File Offset: 0x0032DDC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupLevel>(deep);
		}

		// Token: 0x06016D8B RID: 93579 RVA: 0x0032FBCC File Offset: 0x0032DDCC
		// Note: this type is marked as 'beforefieldinit'.
		static GroupLevel()
		{
			byte[] array = new byte[4];
			GroupLevel.attributeNamespaceIds = array;
			GroupLevel.eleTagNames = new string[] { "groups", "extLst" };
			GroupLevel.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009A1F RID: 39455
		private const string tagName = "groupLevel";

		// Token: 0x04009A20 RID: 39456
		private const byte tagNsId = 22;

		// Token: 0x04009A21 RID: 39457
		internal const int ElementTypeIdConst = 11086;

		// Token: 0x04009A22 RID: 39458
		private static string[] attributeTagNames = new string[] { "uniqueName", "caption", "user", "customRollUp" };

		// Token: 0x04009A23 RID: 39459
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A24 RID: 39460
		private static readonly string[] eleTagNames;

		// Token: 0x04009A25 RID: 39461
		private static readonly byte[] eleNamespaceIds;
	}
}
