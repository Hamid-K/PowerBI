using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Diagram
{
	// Token: 0x02002342 RID: 9026
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RecolorImages : OpenXmlLeafElement
	{
		// Token: 0x17004963 RID: 18787
		// (get) Token: 0x06010251 RID: 66129 RVA: 0x002E0346 File Offset: 0x002DE546
		public override string LocalName
		{
			get
			{
				return "recolorImg";
			}
		}

		// Token: 0x17004964 RID: 18788
		// (get) Token: 0x06010252 RID: 66130 RVA: 0x002E01B3 File Offset: 0x002DE3B3
		internal override byte NamespaceId
		{
			get
			{
				return 58;
			}
		}

		// Token: 0x17004965 RID: 18789
		// (get) Token: 0x06010253 RID: 66131 RVA: 0x002E034D File Offset: 0x002DE54D
		internal override int ElementTypeId
		{
			get
			{
				return 13117;
			}
		}

		// Token: 0x06010254 RID: 66132 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004966 RID: 18790
		// (get) Token: 0x06010255 RID: 66133 RVA: 0x002E0354 File Offset: 0x002DE554
		internal override string[] AttributeTagNames
		{
			get
			{
				return RecolorImages.attributeTagNames;
			}
		}

		// Token: 0x17004967 RID: 18791
		// (get) Token: 0x06010256 RID: 66134 RVA: 0x002E035B File Offset: 0x002DE55B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RecolorImages.attributeNamespaceIds;
			}
		}

		// Token: 0x17004968 RID: 18792
		// (get) Token: 0x06010257 RID: 66135 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010258 RID: 66136 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601025A RID: 66138 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601025B RID: 66139 RVA: 0x002E0362 File Offset: 0x002DE562
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecolorImages>(deep);
		}

		// Token: 0x0601025C RID: 66140 RVA: 0x002E036C File Offset: 0x002DE56C
		// Note: this type is marked as 'beforefieldinit'.
		static RecolorImages()
		{
			byte[] array = new byte[1];
			RecolorImages.attributeNamespaceIds = array;
		}

		// Token: 0x04007343 RID: 29507
		private const string tagName = "recolorImg";

		// Token: 0x04007344 RID: 29508
		private const byte tagNsId = 58;

		// Token: 0x04007345 RID: 29509
		internal const int ElementTypeIdConst = 13117;

		// Token: 0x04007346 RID: 29510
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007347 RID: 29511
		private static byte[] attributeNamespaceIds;
	}
}
