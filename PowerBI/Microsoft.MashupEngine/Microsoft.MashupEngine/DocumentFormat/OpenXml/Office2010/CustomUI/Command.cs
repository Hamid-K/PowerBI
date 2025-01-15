using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022F2 RID: 8946
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Command : OpenXmlLeafElement
	{
		// Token: 0x170046F1 RID: 18161
		// (get) Token: 0x0600FCE0 RID: 64736 RVA: 0x002D031F File Offset: 0x002CE51F
		public override string LocalName
		{
			get
			{
				return "command";
			}
		}

		// Token: 0x170046F2 RID: 18162
		// (get) Token: 0x0600FCE1 RID: 64737 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046F3 RID: 18163
		// (get) Token: 0x0600FCE2 RID: 64738 RVA: 0x002DBDF3 File Offset: 0x002D9FF3
		internal override int ElementTypeId
		{
			get
			{
				return 13090;
			}
		}

		// Token: 0x0600FCE3 RID: 64739 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170046F4 RID: 18164
		// (get) Token: 0x0600FCE4 RID: 64740 RVA: 0x002DBDFA File Offset: 0x002D9FFA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Command.attributeTagNames;
			}
		}

		// Token: 0x170046F5 RID: 18165
		// (get) Token: 0x0600FCE5 RID: 64741 RVA: 0x002DBE01 File Offset: 0x002DA001
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Command.attributeNamespaceIds;
			}
		}

		// Token: 0x170046F6 RID: 18166
		// (get) Token: 0x0600FCE6 RID: 64742 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FCE7 RID: 64743 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170046F7 RID: 18167
		// (get) Token: 0x0600FCE8 RID: 64744 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600FCE9 RID: 64745 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170046F8 RID: 18168
		// (get) Token: 0x0600FCEA RID: 64746 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FCEB RID: 64747 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170046F9 RID: 18169
		// (get) Token: 0x0600FCEC RID: 64748 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FCED RID: 64749 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0600FCEF RID: 64751 RVA: 0x002DBE08 File Offset: 0x002DA008
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "onAction" == name)
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FCF0 RID: 64752 RVA: 0x002DBE75 File Offset: 0x002DA075
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Command>(deep);
		}

		// Token: 0x0600FCF1 RID: 64753 RVA: 0x002DBE80 File Offset: 0x002DA080
		// Note: this type is marked as 'beforefieldinit'.
		static Command()
		{
			byte[] array = new byte[4];
			Command.attributeNamespaceIds = array;
		}

		// Token: 0x040071E0 RID: 29152
		private const string tagName = "command";

		// Token: 0x040071E1 RID: 29153
		private const byte tagNsId = 57;

		// Token: 0x040071E2 RID: 29154
		internal const int ElementTypeIdConst = 13090;

		// Token: 0x040071E3 RID: 29155
		private static string[] attributeTagNames = new string[] { "onAction", "enabled", "getEnabled", "idMso" };

		// Token: 0x040071E4 RID: 29156
		private static byte[] attributeNamespaceIds;
	}
}
