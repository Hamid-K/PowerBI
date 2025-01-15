using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DE RID: 9694
	[GeneratedCode("DomGen", "2.0")]
	internal class RadarStyle : OpenXmlLeafElement
	{
		// Token: 0x17005871 RID: 22641
		// (get) Token: 0x060123C4 RID: 74692 RVA: 0x002F7B0F File Offset: 0x002F5D0F
		public override string LocalName
		{
			get
			{
				return "radarStyle";
			}
		}

		// Token: 0x17005872 RID: 22642
		// (get) Token: 0x060123C5 RID: 74693 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005873 RID: 22643
		// (get) Token: 0x060123C6 RID: 74694 RVA: 0x002F7B16 File Offset: 0x002F5D16
		internal override int ElementTypeId
		{
			get
			{
				return 10539;
			}
		}

		// Token: 0x060123C7 RID: 74695 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005874 RID: 22644
		// (get) Token: 0x060123C8 RID: 74696 RVA: 0x002F7B1D File Offset: 0x002F5D1D
		internal override string[] AttributeTagNames
		{
			get
			{
				return RadarStyle.attributeTagNames;
			}
		}

		// Token: 0x17005875 RID: 22645
		// (get) Token: 0x060123C9 RID: 74697 RVA: 0x002F7B24 File Offset: 0x002F5D24
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RadarStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x17005876 RID: 22646
		// (get) Token: 0x060123CA RID: 74698 RVA: 0x002F7B2B File Offset: 0x002F5D2B
		// (set) Token: 0x060123CB RID: 74699 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<RadarStyleValues> Val
		{
			get
			{
				return (EnumValue<RadarStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060123CD RID: 74701 RVA: 0x002F7B3A File Offset: 0x002F5D3A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<RadarStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060123CE RID: 74702 RVA: 0x002F7B5A File Offset: 0x002F5D5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RadarStyle>(deep);
		}

		// Token: 0x060123CF RID: 74703 RVA: 0x002F7B64 File Offset: 0x002F5D64
		// Note: this type is marked as 'beforefieldinit'.
		static RadarStyle()
		{
			byte[] array = new byte[1];
			RadarStyle.attributeNamespaceIds = array;
		}

		// Token: 0x04007EC5 RID: 32453
		private const string tagName = "radarStyle";

		// Token: 0x04007EC6 RID: 32454
		private const byte tagNsId = 11;

		// Token: 0x04007EC7 RID: 32455
		internal const int ElementTypeIdConst = 10539;

		// Token: 0x04007EC8 RID: 32456
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007EC9 RID: 32457
		private static byte[] attributeNamespaceIds;
	}
}
