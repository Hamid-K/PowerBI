using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F1 RID: 8945
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Tab), FileFormatVersions.Office2010)]
	internal class TabSet : OpenXmlCompositeElement
	{
		// Token: 0x170046E9 RID: 18153
		// (get) Token: 0x0600FCCC RID: 64716 RVA: 0x002D0246 File Offset: 0x002CE446
		public override string LocalName
		{
			get
			{
				return "tabSet";
			}
		}

		// Token: 0x170046EA RID: 18154
		// (get) Token: 0x0600FCCD RID: 64717 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046EB RID: 18155
		// (get) Token: 0x0600FCCE RID: 64718 RVA: 0x002DBD22 File Offset: 0x002D9F22
		internal override int ElementTypeId
		{
			get
			{
				return 13089;
			}
		}

		// Token: 0x0600FCCF RID: 64719 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170046EC RID: 18156
		// (get) Token: 0x0600FCD0 RID: 64720 RVA: 0x002DBD29 File Offset: 0x002D9F29
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabSet.attributeTagNames;
			}
		}

		// Token: 0x170046ED RID: 18157
		// (get) Token: 0x0600FCD1 RID: 64721 RVA: 0x002DBD30 File Offset: 0x002D9F30
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabSet.attributeNamespaceIds;
			}
		}

		// Token: 0x170046EE RID: 18158
		// (get) Token: 0x0600FCD2 RID: 64722 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FCD3 RID: 64723 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170046EF RID: 18159
		// (get) Token: 0x0600FCD4 RID: 64724 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600FCD5 RID: 64725 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170046F0 RID: 18160
		// (get) Token: 0x0600FCD6 RID: 64726 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FCD7 RID: 64727 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x0600FCD8 RID: 64728 RVA: 0x00293ECF File Offset: 0x002920CF
		public TabSet()
		{
		}

		// Token: 0x0600FCD9 RID: 64729 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TabSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCDA RID: 64730 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TabSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCDB RID: 64731 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TabSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FCDC RID: 64732 RVA: 0x002DBD37 File Offset: 0x002D9F37
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "tab" == name)
			{
				return new Tab();
			}
			return null;
		}

		// Token: 0x0600FCDD RID: 64733 RVA: 0x002DBD54 File Offset: 0x002D9F54
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idMso" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FCDE RID: 64734 RVA: 0x002DBDAB File Offset: 0x002D9FAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabSet>(deep);
		}

		// Token: 0x0600FCDF RID: 64735 RVA: 0x002DBDB4 File Offset: 0x002D9FB4
		// Note: this type is marked as 'beforefieldinit'.
		static TabSet()
		{
			byte[] array = new byte[3];
			TabSet.attributeNamespaceIds = array;
		}

		// Token: 0x040071DB RID: 29147
		private const string tagName = "tabSet";

		// Token: 0x040071DC RID: 29148
		private const byte tagNsId = 57;

		// Token: 0x040071DD RID: 29149
		internal const int ElementTypeIdConst = 13089;

		// Token: 0x040071DE RID: 29150
		private static string[] attributeTagNames = new string[] { "idMso", "visible", "getVisible" };

		// Token: 0x040071DF RID: 29151
		private static byte[] attributeNamespaceIds;
	}
}
