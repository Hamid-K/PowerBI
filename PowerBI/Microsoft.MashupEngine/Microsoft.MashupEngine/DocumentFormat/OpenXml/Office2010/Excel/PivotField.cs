using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E0 RID: 9184
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotField : OpenXmlLeafElement
	{
		// Token: 0x17004D6C RID: 19820
		// (get) Token: 0x06010B25 RID: 68389 RVA: 0x002E618B File Offset: 0x002E438B
		public override string LocalName
		{
			get
			{
				return "pivotField";
			}
		}

		// Token: 0x17004D6D RID: 19821
		// (get) Token: 0x06010B26 RID: 68390 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D6E RID: 19822
		// (get) Token: 0x06010B27 RID: 68391 RVA: 0x002E6192 File Offset: 0x002E4392
		internal override int ElementTypeId
		{
			get
			{
				return 12910;
			}
		}

		// Token: 0x06010B28 RID: 68392 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D6F RID: 19823
		// (get) Token: 0x06010B29 RID: 68393 RVA: 0x002E6199 File Offset: 0x002E4399
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotField.attributeTagNames;
			}
		}

		// Token: 0x17004D70 RID: 19824
		// (get) Token: 0x06010B2A RID: 68394 RVA: 0x002E61A0 File Offset: 0x002E43A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotField.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D71 RID: 19825
		// (get) Token: 0x06010B2B RID: 68395 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010B2C RID: 68396 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fillDownLabels")]
		public BooleanValue FillDownLabels
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

		// Token: 0x17004D72 RID: 19826
		// (get) Token: 0x06010B2D RID: 68397 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010B2E RID: 68398 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ignore")]
		public BooleanValue Ignore
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010B30 RID: 68400 RVA: 0x002E61A7 File Offset: 0x002E43A7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fillDownLabels" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ignore" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B31 RID: 68401 RVA: 0x002E61DD File Offset: 0x002E43DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotField>(deep);
		}

		// Token: 0x06010B32 RID: 68402 RVA: 0x002E61E8 File Offset: 0x002E43E8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotField()
		{
			byte[] array = new byte[2];
			PivotField.attributeNamespaceIds = array;
		}

		// Token: 0x040075F5 RID: 30197
		private const string tagName = "pivotField";

		// Token: 0x040075F6 RID: 30198
		private const byte tagNsId = 53;

		// Token: 0x040075F7 RID: 30199
		internal const int ElementTypeIdConst = 12910;

		// Token: 0x040075F8 RID: 30200
		private static string[] attributeTagNames = new string[] { "fillDownLabels", "ignore" };

		// Token: 0x040075F9 RID: 30201
		private static byte[] attributeNamespaceIds;
	}
}
