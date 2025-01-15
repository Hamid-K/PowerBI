using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E5 RID: 9189
	[ChildElementInfo(typeof(SlicerStyle), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlicerStyles : OpenXmlCompositeElement
	{
		// Token: 0x17004DA4 RID: 19876
		// (get) Token: 0x06010B97 RID: 68503 RVA: 0x002E6713 File Offset: 0x002E4913
		public override string LocalName
		{
			get
			{
				return "slicerStyles";
			}
		}

		// Token: 0x17004DA5 RID: 19877
		// (get) Token: 0x06010B98 RID: 68504 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DA6 RID: 19878
		// (get) Token: 0x06010B99 RID: 68505 RVA: 0x002E671A File Offset: 0x002E491A
		internal override int ElementTypeId
		{
			get
			{
				return 12915;
			}
		}

		// Token: 0x06010B9A RID: 68506 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DA7 RID: 19879
		// (get) Token: 0x06010B9B RID: 68507 RVA: 0x002E6721 File Offset: 0x002E4921
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerStyles.attributeTagNames;
			}
		}

		// Token: 0x17004DA8 RID: 19880
		// (get) Token: 0x06010B9C RID: 68508 RVA: 0x002E6728 File Offset: 0x002E4928
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerStyles.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DA9 RID: 19881
		// (get) Token: 0x06010B9D RID: 68509 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010B9E RID: 68510 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "defaultSlicerStyle")]
		public StringValue DefaultSlicerStyle
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

		// Token: 0x06010B9F RID: 68511 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerStyles()
		{
		}

		// Token: 0x06010BA0 RID: 68512 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerStyles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BA1 RID: 68513 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerStyles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010BA2 RID: 68514 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerStyles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010BA3 RID: 68515 RVA: 0x002E672F File Offset: 0x002E492F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicerStyle" == name)
			{
				return new SlicerStyle();
			}
			return null;
		}

		// Token: 0x06010BA4 RID: 68516 RVA: 0x002E674A File Offset: 0x002E494A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "defaultSlicerStyle" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010BA5 RID: 68517 RVA: 0x002E676A File Offset: 0x002E496A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerStyles>(deep);
		}

		// Token: 0x06010BA6 RID: 68518 RVA: 0x002E6774 File Offset: 0x002E4974
		// Note: this type is marked as 'beforefieldinit'.
		static SlicerStyles()
		{
			byte[] array = new byte[1];
			SlicerStyles.attributeNamespaceIds = array;
		}

		// Token: 0x04007612 RID: 30226
		private const string tagName = "slicerStyles";

		// Token: 0x04007613 RID: 30227
		private const byte tagNsId = 53;

		// Token: 0x04007614 RID: 30228
		internal const int ElementTypeIdConst = 12915;

		// Token: 0x04007615 RID: 30229
		private static string[] attributeTagNames = new string[] { "defaultSlicerStyle" };

		// Token: 0x04007616 RID: 30230
		private static byte[] attributeNamespaceIds;
	}
}
