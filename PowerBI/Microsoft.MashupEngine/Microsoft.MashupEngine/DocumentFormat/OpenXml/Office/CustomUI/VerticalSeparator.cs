using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200228B RID: 8843
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalSeparator : OpenXmlLeafElement
	{
		// Token: 0x1700404F RID: 16463
		// (get) Token: 0x0600EEC8 RID: 61128 RVA: 0x002CF519 File Offset: 0x002CD719
		public override string LocalName
		{
			get
			{
				return "separator";
			}
		}

		// Token: 0x17004050 RID: 16464
		// (get) Token: 0x0600EEC9 RID: 61129 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17004051 RID: 16465
		// (get) Token: 0x0600EECA RID: 61130 RVA: 0x002CF520 File Offset: 0x002CD720
		internal override int ElementTypeId
		{
			get
			{
				return 12602;
			}
		}

		// Token: 0x0600EECB RID: 61131 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004052 RID: 16466
		// (get) Token: 0x0600EECC RID: 61132 RVA: 0x002CF527 File Offset: 0x002CD727
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalSeparator.attributeTagNames;
			}
		}

		// Token: 0x17004053 RID: 16467
		// (get) Token: 0x0600EECD RID: 61133 RVA: 0x002CF52E File Offset: 0x002CD72E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalSeparator.attributeNamespaceIds;
			}
		}

		// Token: 0x17004054 RID: 16468
		// (get) Token: 0x0600EECE RID: 61134 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EECF RID: 61135 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004055 RID: 16469
		// (get) Token: 0x0600EED0 RID: 61136 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EED1 RID: 61137 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17004056 RID: 16470
		// (get) Token: 0x0600EED2 RID: 61138 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600EED3 RID: 61139 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17004057 RID: 16471
		// (get) Token: 0x0600EED4 RID: 61140 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EED5 RID: 61141 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004058 RID: 16472
		// (get) Token: 0x0600EED6 RID: 61142 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EED7 RID: 61143 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004059 RID: 16473
		// (get) Token: 0x0600EED8 RID: 61144 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EED9 RID: 61145 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x1700405A RID: 16474
		// (get) Token: 0x0600EEDA RID: 61146 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EEDB RID: 61147 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x1700405B RID: 16475
		// (get) Token: 0x0600EEDC RID: 61148 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EEDD RID: 61149 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x0600EEDF RID: 61151 RVA: 0x002CF538 File Offset: 0x002CD738
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EEE0 RID: 61152 RVA: 0x002CF5FD File Offset: 0x002CD7FD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalSeparator>(deep);
		}

		// Token: 0x0600EEE1 RID: 61153 RVA: 0x002CF608 File Offset: 0x002CD808
		// Note: this type is marked as 'beforefieldinit'.
		static VerticalSeparator()
		{
			byte[] array = new byte[8];
			VerticalSeparator.attributeNamespaceIds = array;
		}

		// Token: 0x04007018 RID: 28696
		private const string tagName = "separator";

		// Token: 0x04007019 RID: 28697
		private const byte tagNsId = 34;

		// Token: 0x0400701A RID: 28698
		internal const int ElementTypeIdConst = 12602;

		// Token: 0x0400701B RID: 28699
		private static string[] attributeTagNames = new string[] { "id", "idQ", "visible", "getVisible", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ" };

		// Token: 0x0400701C RID: 28700
		private static byte[] attributeNamespaceIds;
	}
}
