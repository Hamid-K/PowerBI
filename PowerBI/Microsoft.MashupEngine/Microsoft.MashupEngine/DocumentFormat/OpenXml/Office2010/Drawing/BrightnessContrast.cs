using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002373 RID: 9075
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BrightnessContrast : OpenXmlLeafElement
	{
		// Token: 0x17004AD9 RID: 19161
		// (get) Token: 0x06010567 RID: 66919 RVA: 0x002E23DF File Offset: 0x002E05DF
		public override string LocalName
		{
			get
			{
				return "brightnessContrast";
			}
		}

		// Token: 0x17004ADA RID: 19162
		// (get) Token: 0x06010568 RID: 66920 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004ADB RID: 19163
		// (get) Token: 0x06010569 RID: 66921 RVA: 0x002E23E6 File Offset: 0x002E05E6
		internal override int ElementTypeId
		{
			get
			{
				return 12758;
			}
		}

		// Token: 0x0601056A RID: 66922 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004ADC RID: 19164
		// (get) Token: 0x0601056B RID: 66923 RVA: 0x002E23ED File Offset: 0x002E05ED
		internal override string[] AttributeTagNames
		{
			get
			{
				return BrightnessContrast.attributeTagNames;
			}
		}

		// Token: 0x17004ADD RID: 19165
		// (get) Token: 0x0601056C RID: 66924 RVA: 0x002E23F4 File Offset: 0x002E05F4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BrightnessContrast.attributeNamespaceIds;
			}
		}

		// Token: 0x17004ADE RID: 19166
		// (get) Token: 0x0601056D RID: 66925 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601056E RID: 66926 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "bright")]
		public Int32Value Bright
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004ADF RID: 19167
		// (get) Token: 0x0601056F RID: 66927 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010570 RID: 66928 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "contrast")]
		public Int32Value Contrast
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010572 RID: 66930 RVA: 0x002E23FB File Offset: 0x002E05FB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "bright" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "contrast" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010573 RID: 66931 RVA: 0x002E2431 File Offset: 0x002E0631
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BrightnessContrast>(deep);
		}

		// Token: 0x06010574 RID: 66932 RVA: 0x002E243C File Offset: 0x002E063C
		// Note: this type is marked as 'beforefieldinit'.
		static BrightnessContrast()
		{
			byte[] array = new byte[2];
			BrightnessContrast.attributeNamespaceIds = array;
		}

		// Token: 0x04007432 RID: 29746
		private const string tagName = "brightnessContrast";

		// Token: 0x04007433 RID: 29747
		private const byte tagNsId = 48;

		// Token: 0x04007434 RID: 29748
		internal const int ElementTypeIdConst = 12758;

		// Token: 0x04007435 RID: 29749
		private static string[] attributeTagNames = new string[] { "bright", "contrast" };

		// Token: 0x04007436 RID: 29750
		private static byte[] attributeNamespaceIds;
	}
}
