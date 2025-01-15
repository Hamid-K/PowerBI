using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD1 RID: 11473
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MeasureDimensionMap))]
	internal class Maps : OpenXmlCompositeElement
	{
		// Token: 0x17008566 RID: 34150
		// (get) Token: 0x0601896B RID: 100715 RVA: 0x00342D43 File Offset: 0x00340F43
		public override string LocalName
		{
			get
			{
				return "maps";
			}
		}

		// Token: 0x17008567 RID: 34151
		// (get) Token: 0x0601896C RID: 100716 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008568 RID: 34152
		// (get) Token: 0x0601896D RID: 100717 RVA: 0x00342D4A File Offset: 0x00340F4A
		internal override int ElementTypeId
		{
			get
			{
				return 11454;
			}
		}

		// Token: 0x0601896E RID: 100718 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008569 RID: 34153
		// (get) Token: 0x0601896F RID: 100719 RVA: 0x00342D51 File Offset: 0x00340F51
		internal override string[] AttributeTagNames
		{
			get
			{
				return Maps.attributeTagNames;
			}
		}

		// Token: 0x1700856A RID: 34154
		// (get) Token: 0x06018970 RID: 100720 RVA: 0x00342D58 File Offset: 0x00340F58
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Maps.attributeNamespaceIds;
			}
		}

		// Token: 0x1700856B RID: 34155
		// (get) Token: 0x06018971 RID: 100721 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018972 RID: 100722 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018973 RID: 100723 RVA: 0x00293ECF File Offset: 0x002920CF
		public Maps()
		{
		}

		// Token: 0x06018974 RID: 100724 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Maps(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018975 RID: 100725 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Maps(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018976 RID: 100726 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Maps(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018977 RID: 100727 RVA: 0x00342D5F File Offset: 0x00340F5F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "map" == name)
			{
				return new MeasureDimensionMap();
			}
			return null;
		}

		// Token: 0x06018978 RID: 100728 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018979 RID: 100729 RVA: 0x00342D7A File Offset: 0x00340F7A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Maps>(deep);
		}

		// Token: 0x0601897A RID: 100730 RVA: 0x00342D84 File Offset: 0x00340F84
		// Note: this type is marked as 'beforefieldinit'.
		static Maps()
		{
			byte[] array = new byte[1];
			Maps.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0F4 RID: 41204
		private const string tagName = "maps";

		// Token: 0x0400A0F5 RID: 41205
		private const byte tagNsId = 22;

		// Token: 0x0400A0F6 RID: 41206
		internal const int ElementTypeIdConst = 11454;

		// Token: 0x0400A0F7 RID: 41207
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0F8 RID: 41208
		private static byte[] attributeNamespaceIds;
	}
}
