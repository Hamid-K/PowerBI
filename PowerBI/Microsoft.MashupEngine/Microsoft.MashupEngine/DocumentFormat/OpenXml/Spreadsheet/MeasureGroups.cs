using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD0 RID: 11472
	[ChildElementInfo(typeof(MeasureGroup))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MeasureGroups : OpenXmlCompositeElement
	{
		// Token: 0x17008560 RID: 34144
		// (get) Token: 0x0601895B RID: 100699 RVA: 0x00342CD3 File Offset: 0x00340ED3
		public override string LocalName
		{
			get
			{
				return "measureGroups";
			}
		}

		// Token: 0x17008561 RID: 34145
		// (get) Token: 0x0601895C RID: 100700 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008562 RID: 34146
		// (get) Token: 0x0601895D RID: 100701 RVA: 0x00342CDA File Offset: 0x00340EDA
		internal override int ElementTypeId
		{
			get
			{
				return 11453;
			}
		}

		// Token: 0x0601895E RID: 100702 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008563 RID: 34147
		// (get) Token: 0x0601895F RID: 100703 RVA: 0x00342CE1 File Offset: 0x00340EE1
		internal override string[] AttributeTagNames
		{
			get
			{
				return MeasureGroups.attributeTagNames;
			}
		}

		// Token: 0x17008564 RID: 34148
		// (get) Token: 0x06018960 RID: 100704 RVA: 0x00342CE8 File Offset: 0x00340EE8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MeasureGroups.attributeNamespaceIds;
			}
		}

		// Token: 0x17008565 RID: 34149
		// (get) Token: 0x06018961 RID: 100705 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018962 RID: 100706 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018963 RID: 100707 RVA: 0x00293ECF File Offset: 0x002920CF
		public MeasureGroups()
		{
		}

		// Token: 0x06018964 RID: 100708 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MeasureGroups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018965 RID: 100709 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MeasureGroups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018966 RID: 100710 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MeasureGroups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018967 RID: 100711 RVA: 0x00342CEF File Offset: 0x00340EEF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "measureGroup" == name)
			{
				return new MeasureGroup();
			}
			return null;
		}

		// Token: 0x06018968 RID: 100712 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018969 RID: 100713 RVA: 0x00342D0A File Offset: 0x00340F0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MeasureGroups>(deep);
		}

		// Token: 0x0601896A RID: 100714 RVA: 0x00342D14 File Offset: 0x00340F14
		// Note: this type is marked as 'beforefieldinit'.
		static MeasureGroups()
		{
			byte[] array = new byte[1];
			MeasureGroups.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0EF RID: 41199
		private const string tagName = "measureGroups";

		// Token: 0x0400A0F0 RID: 41200
		private const byte tagNsId = 22;

		// Token: 0x0400A0F1 RID: 41201
		internal const int ElementTypeIdConst = 11453;

		// Token: 0x0400A0F2 RID: 41202
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A0F3 RID: 41203
		private static byte[] attributeNamespaceIds;
	}
}
