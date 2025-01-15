using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E45 RID: 11845
	[GeneratedCode("DomGen", "2.0")]
	internal class PaperSource : OpenXmlLeafElement
	{
		// Token: 0x170089E5 RID: 35301
		// (get) Token: 0x06019299 RID: 103065 RVA: 0x00346FF8 File Offset: 0x003451F8
		public override string LocalName
		{
			get
			{
				return "paperSrc";
			}
		}

		// Token: 0x170089E6 RID: 35302
		// (get) Token: 0x0601929A RID: 103066 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089E7 RID: 35303
		// (get) Token: 0x0601929B RID: 103067 RVA: 0x00346FFF File Offset: 0x003451FF
		internal override int ElementTypeId
		{
			get
			{
				return 11531;
			}
		}

		// Token: 0x0601929C RID: 103068 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089E8 RID: 35304
		// (get) Token: 0x0601929D RID: 103069 RVA: 0x00347006 File Offset: 0x00345206
		internal override string[] AttributeTagNames
		{
			get
			{
				return PaperSource.attributeTagNames;
			}
		}

		// Token: 0x170089E9 RID: 35305
		// (get) Token: 0x0601929E RID: 103070 RVA: 0x0034700D File Offset: 0x0034520D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PaperSource.attributeNamespaceIds;
			}
		}

		// Token: 0x170089EA RID: 35306
		// (get) Token: 0x0601929F RID: 103071 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x060192A0 RID: 103072 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "first")]
		public UInt16Value First
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170089EB RID: 35307
		// (get) Token: 0x060192A1 RID: 103073 RVA: 0x002F0823 File Offset: 0x002EEA23
		// (set) Token: 0x060192A2 RID: 103074 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "other")]
		public UInt16Value Other
		{
			get
			{
				return (UInt16Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060192A4 RID: 103076 RVA: 0x00347014 File Offset: 0x00345214
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "first" == name)
			{
				return new UInt16Value();
			}
			if (23 == namespaceId && "other" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060192A5 RID: 103077 RVA: 0x0034704E File Offset: 0x0034524E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PaperSource>(deep);
		}

		// Token: 0x0400A75A RID: 42842
		private const string tagName = "paperSrc";

		// Token: 0x0400A75B RID: 42843
		private const byte tagNsId = 23;

		// Token: 0x0400A75C RID: 42844
		internal const int ElementTypeIdConst = 11531;

		// Token: 0x0400A75D RID: 42845
		private static string[] attributeTagNames = new string[] { "first", "other" };

		// Token: 0x0400A75E RID: 42846
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
