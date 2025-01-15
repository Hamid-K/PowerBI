using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238E RID: 9102
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ApplicationNonVisualDrawingProperties : OpenXmlLeafElement
	{
		// Token: 0x17004BA1 RID: 19361
		// (get) Token: 0x06010731 RID: 67377 RVA: 0x002DFF99 File Offset: 0x002DE199
		public override string LocalName
		{
			get
			{
				return "nvPr";
			}
		}

		// Token: 0x17004BA2 RID: 19362
		// (get) Token: 0x06010732 RID: 67378 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004BA3 RID: 19363
		// (get) Token: 0x06010733 RID: 67379 RVA: 0x002E3AD7 File Offset: 0x002E1CD7
		internal override int ElementTypeId
		{
			get
			{
				return 13016;
			}
		}

		// Token: 0x06010734 RID: 67380 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004BA4 RID: 19364
		// (get) Token: 0x06010735 RID: 67381 RVA: 0x002E3ADE File Offset: 0x002E1CDE
		internal override string[] AttributeTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004BA5 RID: 19365
		// (get) Token: 0x06010736 RID: 67382 RVA: 0x002E3AE5 File Offset: 0x002E1CE5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BA6 RID: 19366
		// (get) Token: 0x06010737 RID: 67383 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010738 RID: 67384 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x17004BA7 RID: 19367
		// (get) Token: 0x06010739 RID: 67385 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601073A RID: 67386 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPublished")]
		public BooleanValue PublishedFlag
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

		// Token: 0x0601073C RID: 67388 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601073D RID: 67389 RVA: 0x002E3AEC File Offset: 0x002E1CEC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingProperties>(deep);
		}

		// Token: 0x0601073E RID: 67390 RVA: 0x002E3AF8 File Offset: 0x002E1CF8
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationNonVisualDrawingProperties()
		{
			byte[] array = new byte[2];
			ApplicationNonVisualDrawingProperties.attributeNamespaceIds = array;
		}

		// Token: 0x040074A6 RID: 29862
		private const string tagName = "nvPr";

		// Token: 0x040074A7 RID: 29863
		private const byte tagNsId = 54;

		// Token: 0x040074A8 RID: 29864
		internal const int ElementTypeIdConst = 13016;

		// Token: 0x040074A9 RID: 29865
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x040074AA RID: 29866
		private static byte[] attributeNamespaceIds;
	}
}
