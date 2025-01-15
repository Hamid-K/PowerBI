using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F37 RID: 12087
	[GeneratedCode("DomGen", "2.0")]
	internal class StatusText : OpenXmlLeafElement
	{
		// Token: 0x17008F86 RID: 36742
		// (get) Token: 0x06019EDF RID: 106207 RVA: 0x00359FAD File Offset: 0x003581AD
		public override string LocalName
		{
			get
			{
				return "statusText";
			}
		}

		// Token: 0x17008F87 RID: 36743
		// (get) Token: 0x06019EE0 RID: 106208 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F88 RID: 36744
		// (get) Token: 0x06019EE1 RID: 106209 RVA: 0x00359FB4 File Offset: 0x003581B4
		internal override int ElementTypeId
		{
			get
			{
				return 11732;
			}
		}

		// Token: 0x06019EE2 RID: 106210 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F89 RID: 36745
		// (get) Token: 0x06019EE3 RID: 106211 RVA: 0x00359FBB File Offset: 0x003581BB
		internal override string[] AttributeTagNames
		{
			get
			{
				return StatusText.attributeTagNames;
			}
		}

		// Token: 0x17008F8A RID: 36746
		// (get) Token: 0x06019EE4 RID: 106212 RVA: 0x00359FC2 File Offset: 0x003581C2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StatusText.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F8B RID: 36747
		// (get) Token: 0x06019EE5 RID: 106213 RVA: 0x00359F1A File Offset: 0x0035811A
		// (set) Token: 0x06019EE6 RID: 106214 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<InfoTextValues> Type
		{
			get
			{
				return (EnumValue<InfoTextValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008F8C RID: 36748
		// (get) Token: 0x06019EE7 RID: 106215 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019EE8 RID: 106216 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x06019EEA RID: 106218 RVA: 0x00359F29 File Offset: 0x00358129
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<InfoTextValues>();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019EEB RID: 106219 RVA: 0x00359FC9 File Offset: 0x003581C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StatusText>(deep);
		}

		// Token: 0x0400AAF2 RID: 43762
		private const string tagName = "statusText";

		// Token: 0x0400AAF3 RID: 43763
		private const byte tagNsId = 23;

		// Token: 0x0400AAF4 RID: 43764
		internal const int ElementTypeIdConst = 11732;

		// Token: 0x0400AAF5 RID: 43765
		private static string[] attributeTagNames = new string[] { "type", "val" };

		// Token: 0x0400AAF6 RID: 43766
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
