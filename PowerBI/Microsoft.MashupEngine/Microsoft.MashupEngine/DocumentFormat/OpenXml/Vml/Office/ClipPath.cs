using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002212 RID: 8722
	[GeneratedCode("DomGen", "2.0")]
	internal class ClipPath : OpenXmlLeafElement
	{
		// Token: 0x170038F3 RID: 14579
		// (get) Token: 0x0600DF7F RID: 57215 RVA: 0x002BF3DB File Offset: 0x002BD5DB
		public override string LocalName
		{
			get
			{
				return "clippath";
			}
		}

		// Token: 0x170038F4 RID: 14580
		// (get) Token: 0x0600DF80 RID: 57216 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038F5 RID: 14581
		// (get) Token: 0x0600DF81 RID: 57217 RVA: 0x002BF3E2 File Offset: 0x002BD5E2
		internal override int ElementTypeId
		{
			get
			{
				return 12415;
			}
		}

		// Token: 0x0600DF82 RID: 57218 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170038F6 RID: 14582
		// (get) Token: 0x0600DF83 RID: 57219 RVA: 0x002BF3E9 File Offset: 0x002BD5E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ClipPath.attributeTagNames;
			}
		}

		// Token: 0x170038F7 RID: 14583
		// (get) Token: 0x0600DF84 RID: 57220 RVA: 0x002BF3F0 File Offset: 0x002BD5F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ClipPath.attributeNamespaceIds;
			}
		}

		// Token: 0x170038F8 RID: 14584
		// (get) Token: 0x0600DF85 RID: 57221 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600DF86 RID: 57222 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(27, "v")]
		public StringValue Value
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

		// Token: 0x0600DF88 RID: 57224 RVA: 0x002BF3F7 File Offset: 0x002BD5F7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "v" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DF89 RID: 57225 RVA: 0x002BF419 File Offset: 0x002BD619
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ClipPath>(deep);
		}

		// Token: 0x04006D9E RID: 28062
		private const string tagName = "clippath";

		// Token: 0x04006D9F RID: 28063
		private const byte tagNsId = 27;

		// Token: 0x04006DA0 RID: 28064
		internal const int ElementTypeIdConst = 12415;

		// Token: 0x04006DA1 RID: 28065
		private static string[] attributeTagNames = new string[] { "v" };

		// Token: 0x04006DA2 RID: 28066
		private static byte[] attributeNamespaceIds = new byte[] { 27 };
	}
}
