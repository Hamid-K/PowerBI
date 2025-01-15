using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023DC RID: 9180
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class WorkbookProperties : OpenXmlLeafElement
	{
		// Token: 0x17004D41 RID: 19777
		// (get) Token: 0x06010ACD RID: 68301 RVA: 0x002E5D66 File Offset: 0x002E3F66
		public override string LocalName
		{
			get
			{
				return "workbookPr";
			}
		}

		// Token: 0x17004D42 RID: 19778
		// (get) Token: 0x06010ACE RID: 68302 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D43 RID: 19779
		// (get) Token: 0x06010ACF RID: 68303 RVA: 0x002E5D6D File Offset: 0x002E3F6D
		internal override int ElementTypeId
		{
			get
			{
				return 12906;
			}
		}

		// Token: 0x06010AD0 RID: 68304 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D44 RID: 19780
		// (get) Token: 0x06010AD1 RID: 68305 RVA: 0x002E5D74 File Offset: 0x002E3F74
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorkbookProperties.attributeTagNames;
			}
		}

		// Token: 0x17004D45 RID: 19781
		// (get) Token: 0x06010AD2 RID: 68306 RVA: 0x002E5D7B File Offset: 0x002E3F7B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorkbookProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D46 RID: 19782
		// (get) Token: 0x06010AD3 RID: 68307 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010AD4 RID: 68308 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "defaultImageDpi")]
		public UInt32Value DefaultImageDpi
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

		// Token: 0x17004D47 RID: 19783
		// (get) Token: 0x06010AD5 RID: 68309 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010AD6 RID: 68310 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "discardImageEditData")]
		public BooleanValue DiscardImageEditData
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

		// Token: 0x17004D48 RID: 19784
		// (get) Token: 0x06010AD7 RID: 68311 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06010AD8 RID: 68312 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "accuracyVersion")]
		public UInt32Value AccuracyVersion
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06010ADA RID: 68314 RVA: 0x002E5D84 File Offset: 0x002E3F84
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "defaultImageDpi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "discardImageEditData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "accuracyVersion" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010ADB RID: 68315 RVA: 0x002E5DDB File Offset: 0x002E3FDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookProperties>(deep);
		}

		// Token: 0x06010ADC RID: 68316 RVA: 0x002E5DE4 File Offset: 0x002E3FE4
		// Note: this type is marked as 'beforefieldinit'.
		static WorkbookProperties()
		{
			byte[] array = new byte[3];
			WorkbookProperties.attributeNamespaceIds = array;
		}

		// Token: 0x040075DD RID: 30173
		private const string tagName = "workbookPr";

		// Token: 0x040075DE RID: 30174
		private const byte tagNsId = 53;

		// Token: 0x040075DF RID: 30175
		internal const int ElementTypeIdConst = 12906;

		// Token: 0x040075E0 RID: 30176
		private static string[] attributeTagNames = new string[] { "defaultImageDpi", "discardImageEditData", "accuracyVersion" };

		// Token: 0x040075E1 RID: 30177
		private static byte[] attributeNamespaceIds;
	}
}
