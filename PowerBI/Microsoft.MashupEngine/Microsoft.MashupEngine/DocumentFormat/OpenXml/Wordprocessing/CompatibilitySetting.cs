using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FFC RID: 12284
	[GeneratedCode("DomGen", "2.0")]
	internal class CompatibilitySetting : OpenXmlLeafElement
	{
		// Token: 0x170095CF RID: 38351
		// (get) Token: 0x0601AC58 RID: 109656 RVA: 0x003675F0 File Offset: 0x003657F0
		public override string LocalName
		{
			get
			{
				return "compatSetting";
			}
		}

		// Token: 0x170095D0 RID: 38352
		// (get) Token: 0x0601AC59 RID: 109657 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095D1 RID: 38353
		// (get) Token: 0x0601AC5A RID: 109658 RVA: 0x003675F7 File Offset: 0x003657F7
		internal override int ElementTypeId
		{
			get
			{
				return 12120;
			}
		}

		// Token: 0x0601AC5B RID: 109659 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170095D2 RID: 38354
		// (get) Token: 0x0601AC5C RID: 109660 RVA: 0x003675FE File Offset: 0x003657FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return CompatibilitySetting.attributeTagNames;
			}
		}

		// Token: 0x170095D3 RID: 38355
		// (get) Token: 0x0601AC5D RID: 109661 RVA: 0x00367605 File Offset: 0x00365805
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CompatibilitySetting.attributeNamespaceIds;
			}
		}

		// Token: 0x170095D4 RID: 38356
		// (get) Token: 0x0601AC5E RID: 109662 RVA: 0x0036760C File Offset: 0x0036580C
		// (set) Token: 0x0601AC5F RID: 109663 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public EnumValue<CompatSettingNameValues> Name
		{
			get
			{
				return (EnumValue<CompatSettingNameValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170095D5 RID: 38357
		// (get) Token: 0x0601AC60 RID: 109664 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AC61 RID: 109665 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "uri")]
		public StringValue Uri
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

		// Token: 0x170095D6 RID: 38358
		// (get) Token: 0x0601AC62 RID: 109666 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AC63 RID: 109667 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x0601AC65 RID: 109669 RVA: 0x0036761C File Offset: 0x0036581C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new EnumValue<CompatSettingNameValues>();
			}
			if (23 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AC66 RID: 109670 RVA: 0x00367679 File Offset: 0x00365879
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompatibilitySetting>(deep);
		}

		// Token: 0x0400AE40 RID: 44608
		private const string tagName = "compatSetting";

		// Token: 0x0400AE41 RID: 44609
		private const byte tagNsId = 23;

		// Token: 0x0400AE42 RID: 44610
		internal const int ElementTypeIdConst = 12120;

		// Token: 0x0400AE43 RID: 44611
		private static string[] attributeTagNames = new string[] { "name", "uri", "val" };

		// Token: 0x0400AE44 RID: 44612
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
