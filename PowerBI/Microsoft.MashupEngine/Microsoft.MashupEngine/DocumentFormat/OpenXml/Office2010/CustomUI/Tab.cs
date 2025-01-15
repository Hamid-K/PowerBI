using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F0 RID: 8944
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Group), FileFormatVersions.Office2010)]
	internal class Tab : OpenXmlCompositeElement
	{
		// Token: 0x170046D6 RID: 18134
		// (get) Token: 0x0600FCA2 RID: 64674 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x170046D7 RID: 18135
		// (get) Token: 0x0600FCA3 RID: 64675 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046D8 RID: 18136
		// (get) Token: 0x0600FCA4 RID: 64676 RVA: 0x002DBAFD File Offset: 0x002D9CFD
		internal override int ElementTypeId
		{
			get
			{
				return 13088;
			}
		}

		// Token: 0x0600FCA5 RID: 64677 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170046D9 RID: 18137
		// (get) Token: 0x0600FCA6 RID: 64678 RVA: 0x002DBB04 File Offset: 0x002D9D04
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tab.attributeTagNames;
			}
		}

		// Token: 0x170046DA RID: 18138
		// (get) Token: 0x0600FCA7 RID: 64679 RVA: 0x002DBB0B File Offset: 0x002D9D0B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tab.attributeNamespaceIds;
			}
		}

		// Token: 0x170046DB RID: 18139
		// (get) Token: 0x0600FCA8 RID: 64680 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FCA9 RID: 64681 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170046DC RID: 18140
		// (get) Token: 0x0600FCAA RID: 64682 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FCAB RID: 64683 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170046DD RID: 18141
		// (get) Token: 0x0600FCAC RID: 64684 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FCAD RID: 64685 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170046DE RID: 18142
		// (get) Token: 0x0600FCAE RID: 64686 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FCAF RID: 64687 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170046DF RID: 18143
		// (get) Token: 0x0600FCB0 RID: 64688 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FCB1 RID: 64689 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170046E0 RID: 18144
		// (get) Token: 0x0600FCB2 RID: 64690 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FCB3 RID: 64691 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170046E1 RID: 18145
		// (get) Token: 0x0600FCB4 RID: 64692 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FCB5 RID: 64693 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170046E2 RID: 18146
		// (get) Token: 0x0600FCB6 RID: 64694 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FCB7 RID: 64695 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170046E3 RID: 18147
		// (get) Token: 0x0600FCB8 RID: 64696 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FCB9 RID: 64697 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170046E4 RID: 18148
		// (get) Token: 0x0600FCBA RID: 64698 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FCBB RID: 64699 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170046E5 RID: 18149
		// (get) Token: 0x0600FCBC RID: 64700 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600FCBD RID: 64701 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170046E6 RID: 18150
		// (get) Token: 0x0600FCBE RID: 64702 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FCBF RID: 64703 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170046E7 RID: 18151
		// (get) Token: 0x0600FCC0 RID: 64704 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FCC1 RID: 64705 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170046E8 RID: 18152
		// (get) Token: 0x0600FCC2 RID: 64706 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FCC3 RID: 64707 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x0600FCC4 RID: 64708 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tab()
		{
		}

		// Token: 0x0600FCC5 RID: 64709 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tab(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCC6 RID: 64710 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tab(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCC7 RID: 64711 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tab(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FCC8 RID: 64712 RVA: 0x002DBB12 File Offset: 0x002D9D12
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "group" == name)
			{
				return new Group();
			}
			return null;
		}

		// Token: 0x0600FCC9 RID: 64713 RVA: 0x002DBB30 File Offset: 0x002D9D30
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

		// Token: 0x0600FCCA RID: 64714 RVA: 0x002DBC79 File Offset: 0x002D9E79
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tab>(deep);
		}

		// Token: 0x0600FCCB RID: 64715 RVA: 0x002DBC84 File Offset: 0x002D9E84
		// Note: this type is marked as 'beforefieldinit'.
		static Tab()
		{
			byte[] array = new byte[14];
			Tab.attributeNamespaceIds = array;
		}

		// Token: 0x040071D6 RID: 29142
		private const string tagName = "tab";

		// Token: 0x040071D7 RID: 29143
		private const byte tagNsId = 57;

		// Token: 0x040071D8 RID: 29144
		internal const int ElementTypeIdConst = 13088;

		// Token: 0x040071D9 RID: 29145
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "idMso", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x040071DA RID: 29146
		private static byte[] attributeNamespaceIds;
	}
}
