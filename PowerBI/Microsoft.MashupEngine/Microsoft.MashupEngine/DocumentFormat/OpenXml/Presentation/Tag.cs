using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7A RID: 10874
	[GeneratedCode("DomGen", "2.0")]
	internal class Tag : OpenXmlLeafElement
	{
		// Token: 0x17007334 RID: 29492
		// (get) Token: 0x06016061 RID: 90209 RVA: 0x002AC58A File Offset: 0x002AA78A
		public override string LocalName
		{
			get
			{
				return "tag";
			}
		}

		// Token: 0x17007335 RID: 29493
		// (get) Token: 0x06016062 RID: 90210 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007336 RID: 29494
		// (get) Token: 0x06016063 RID: 90211 RVA: 0x00325CB4 File Offset: 0x00323EB4
		internal override int ElementTypeId
		{
			get
			{
				return 12290;
			}
		}

		// Token: 0x06016064 RID: 90212 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007337 RID: 29495
		// (get) Token: 0x06016065 RID: 90213 RVA: 0x00325CBB File Offset: 0x00323EBB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tag.attributeTagNames;
			}
		}

		// Token: 0x17007338 RID: 29496
		// (get) Token: 0x06016066 RID: 90214 RVA: 0x00325CC2 File Offset: 0x00323EC2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tag.attributeNamespaceIds;
			}
		}

		// Token: 0x17007339 RID: 29497
		// (get) Token: 0x06016067 RID: 90215 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016068 RID: 90216 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700733A RID: 29498
		// (get) Token: 0x06016069 RID: 90217 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601606A RID: 90218 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
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

		// Token: 0x0601606C RID: 90220 RVA: 0x00325CC9 File Offset: 0x00323EC9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601606D RID: 90221 RVA: 0x00325CFF File Offset: 0x00323EFF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tag>(deep);
		}

		// Token: 0x0601606E RID: 90222 RVA: 0x00325D08 File Offset: 0x00323F08
		// Note: this type is marked as 'beforefieldinit'.
		static Tag()
		{
			byte[] array = new byte[2];
			Tag.attributeNamespaceIds = array;
		}

		// Token: 0x040095D7 RID: 38359
		private const string tagName = "tag";

		// Token: 0x040095D8 RID: 38360
		private const byte tagNsId = 24;

		// Token: 0x040095D9 RID: 38361
		internal const int ElementTypeIdConst = 12290;

		// Token: 0x040095DA RID: 38362
		private static string[] attributeTagNames = new string[] { "name", "val" };

		// Token: 0x040095DB RID: 38363
		private static byte[] attributeNamespaceIds;
	}
}
