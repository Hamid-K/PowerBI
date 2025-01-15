using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FA6 RID: 12198
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleName : OpenXmlLeafElement
	{
		// Token: 0x170092E3 RID: 37603
		// (get) Token: 0x0601A61C RID: 108060 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x170092E4 RID: 37604
		// (get) Token: 0x0601A61D RID: 108061 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170092E5 RID: 37605
		// (get) Token: 0x0601A61E RID: 108062 RVA: 0x003617AC File Offset: 0x0035F9AC
		internal override int ElementTypeId
		{
			get
			{
				return 11892;
			}
		}

		// Token: 0x0601A61F RID: 108063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170092E6 RID: 37606
		// (get) Token: 0x0601A620 RID: 108064 RVA: 0x003617B3 File Offset: 0x0035F9B3
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleName.attributeTagNames;
			}
		}

		// Token: 0x170092E7 RID: 37607
		// (get) Token: 0x0601A621 RID: 108065 RVA: 0x003617BA File Offset: 0x0035F9BA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleName.attributeNamespaceIds;
			}
		}

		// Token: 0x170092E8 RID: 37608
		// (get) Token: 0x0601A622 RID: 108066 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A623 RID: 108067 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x0601A625 RID: 108069 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A626 RID: 108070 RVA: 0x003617C1 File Offset: 0x0035F9C1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleName>(deep);
		}

		// Token: 0x0400ACCF RID: 44239
		private const string tagName = "name";

		// Token: 0x0400ACD0 RID: 44240
		private const byte tagNsId = 23;

		// Token: 0x0400ACD1 RID: 44241
		internal const int ElementTypeIdConst = 11892;

		// Token: 0x0400ACD2 RID: 44242
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ACD3 RID: 44243
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
