using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A96 RID: 10902
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class OleObjectEmbed : OpenXmlCompositeElement
	{
		// Token: 0x170073F5 RID: 29685
		// (get) Token: 0x06016215 RID: 90645 RVA: 0x00326D14 File Offset: 0x00324F14
		public override string LocalName
		{
			get
			{
				return "embed";
			}
		}

		// Token: 0x170073F6 RID: 29686
		// (get) Token: 0x06016216 RID: 90646 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073F7 RID: 29687
		// (get) Token: 0x06016217 RID: 90647 RVA: 0x00326D1B File Offset: 0x00324F1B
		internal override int ElementTypeId
		{
			get
			{
				return 12317;
			}
		}

		// Token: 0x06016218 RID: 90648 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073F8 RID: 29688
		// (get) Token: 0x06016219 RID: 90649 RVA: 0x00326D22 File Offset: 0x00324F22
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleObjectEmbed.attributeTagNames;
			}
		}

		// Token: 0x170073F9 RID: 29689
		// (get) Token: 0x0601621A RID: 90650 RVA: 0x00326D29 File Offset: 0x00324F29
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleObjectEmbed.attributeNamespaceIds;
			}
		}

		// Token: 0x170073FA RID: 29690
		// (get) Token: 0x0601621B RID: 90651 RVA: 0x00326D30 File Offset: 0x00324F30
		// (set) Token: 0x0601621C RID: 90652 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "followColorScheme")]
		public EnumValue<OleObjectFollowColorSchemeValues> FollowColorScheme
		{
			get
			{
				return (EnumValue<OleObjectFollowColorSchemeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601621D RID: 90653 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObjectEmbed()
		{
		}

		// Token: 0x0601621E RID: 90654 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObjectEmbed(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601621F RID: 90655 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObjectEmbed(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016220 RID: 90656 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObjectEmbed(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016221 RID: 90657 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170073FB RID: 29691
		// (get) Token: 0x06016222 RID: 90658 RVA: 0x00326D3F File Offset: 0x00324F3F
		internal override string[] ElementTagNames
		{
			get
			{
				return OleObjectEmbed.eleTagNames;
			}
		}

		// Token: 0x170073FC RID: 29692
		// (get) Token: 0x06016223 RID: 90659 RVA: 0x00326D46 File Offset: 0x00324F46
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleObjectEmbed.eleNamespaceIds;
			}
		}

		// Token: 0x170073FD RID: 29693
		// (get) Token: 0x06016224 RID: 90660 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073FE RID: 29694
		// (get) Token: 0x06016225 RID: 90661 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06016226 RID: 90662 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06016227 RID: 90663 RVA: 0x00326D4D File Offset: 0x00324F4D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "followColorScheme" == name)
			{
				return new EnumValue<OleObjectFollowColorSchemeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016228 RID: 90664 RVA: 0x00326D6D File Offset: 0x00324F6D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObjectEmbed>(deep);
		}

		// Token: 0x06016229 RID: 90665 RVA: 0x00326D78 File Offset: 0x00324F78
		// Note: this type is marked as 'beforefieldinit'.
		static OleObjectEmbed()
		{
			byte[] array = new byte[1];
			OleObjectEmbed.attributeNamespaceIds = array;
			OleObjectEmbed.eleTagNames = new string[] { "extLst" };
			OleObjectEmbed.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009659 RID: 38489
		private const string tagName = "embed";

		// Token: 0x0400965A RID: 38490
		private const byte tagNsId = 24;

		// Token: 0x0400965B RID: 38491
		internal const int ElementTypeIdConst = 12317;

		// Token: 0x0400965C RID: 38492
		private static string[] attributeTagNames = new string[] { "followColorScheme" };

		// Token: 0x0400965D RID: 38493
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400965E RID: 38494
		private static readonly string[] eleTagNames;

		// Token: 0x0400965F RID: 38495
		private static readonly byte[] eleNamespaceIds;
	}
}
