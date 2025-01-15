using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002294 RID: 8852
	[GeneratedCode("DomGen", "2.0")]
	internal class RepurposedCommand : OpenXmlLeafElement
	{
		// Token: 0x170040C1 RID: 16577
		// (get) Token: 0x0600EFC5 RID: 61381 RVA: 0x002D031F File Offset: 0x002CE51F
		public override string LocalName
		{
			get
			{
				return "command";
			}
		}

		// Token: 0x170040C2 RID: 16578
		// (get) Token: 0x0600EFC6 RID: 61382 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040C3 RID: 16579
		// (get) Token: 0x0600EFC7 RID: 61383 RVA: 0x002D0326 File Offset: 0x002CE526
		internal override int ElementTypeId
		{
			get
			{
				return 12610;
			}
		}

		// Token: 0x0600EFC8 RID: 61384 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040C4 RID: 16580
		// (get) Token: 0x0600EFC9 RID: 61385 RVA: 0x002D032D File Offset: 0x002CE52D
		internal override string[] AttributeTagNames
		{
			get
			{
				return RepurposedCommand.attributeTagNames;
			}
		}

		// Token: 0x170040C5 RID: 16581
		// (get) Token: 0x0600EFCA RID: 61386 RVA: 0x002D0334 File Offset: 0x002CE534
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RepurposedCommand.attributeNamespaceIds;
			}
		}

		// Token: 0x170040C6 RID: 16582
		// (get) Token: 0x0600EFCB RID: 61387 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EFCC RID: 61388 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170040C7 RID: 16583
		// (get) Token: 0x0600EFCD RID: 61389 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600EFCE RID: 61390 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170040C8 RID: 16584
		// (get) Token: 0x0600EFCF RID: 61391 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EFD0 RID: 61392 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170040C9 RID: 16585
		// (get) Token: 0x0600EFD1 RID: 61393 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EFD2 RID: 61394 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0600EFD4 RID: 61396 RVA: 0x002D033C File Offset: 0x002CE53C
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

		// Token: 0x0600EFD5 RID: 61397 RVA: 0x002D03A9 File Offset: 0x002CE5A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RepurposedCommand>(deep);
		}

		// Token: 0x0600EFD6 RID: 61398 RVA: 0x002D03B4 File Offset: 0x002CE5B4
		// Note: this type is marked as 'beforefieldinit'.
		static RepurposedCommand()
		{
			byte[] array = new byte[4];
			RepurposedCommand.attributeNamespaceIds = array;
		}

		// Token: 0x0400703C RID: 28732
		private const string tagName = "command";

		// Token: 0x0400703D RID: 28733
		private const byte tagNsId = 34;

		// Token: 0x0400703E RID: 28734
		internal const int ElementTypeIdConst = 12610;

		// Token: 0x0400703F RID: 28735
		private static string[] attributeTagNames = new string[] { "onAction", "enabled", "getEnabled", "idMso" };

		// Token: 0x04007040 RID: 28736
		private static byte[] attributeNamespaceIds;
	}
}
