using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272C RID: 10028
	[GeneratedCode("DomGen", "2.0")]
	internal class PresetDash : OpenXmlLeafElement
	{
		// Token: 0x17005FEF RID: 24559
		// (get) Token: 0x06013453 RID: 78931 RVA: 0x002ED194 File Offset: 0x002EB394
		public override string LocalName
		{
			get
			{
				return "prstDash";
			}
		}

		// Token: 0x17005FF0 RID: 24560
		// (get) Token: 0x06013454 RID: 78932 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FF1 RID: 24561
		// (get) Token: 0x06013455 RID: 78933 RVA: 0x003059D3 File Offset: 0x00303BD3
		internal override int ElementTypeId
		{
			get
			{
				return 10091;
			}
		}

		// Token: 0x06013456 RID: 78934 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FF2 RID: 24562
		// (get) Token: 0x06013457 RID: 78935 RVA: 0x003059DA File Offset: 0x00303BDA
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetDash.attributeTagNames;
			}
		}

		// Token: 0x17005FF3 RID: 24563
		// (get) Token: 0x06013458 RID: 78936 RVA: 0x003059E1 File Offset: 0x00303BE1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetDash.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FF4 RID: 24564
		// (get) Token: 0x06013459 RID: 78937 RVA: 0x003059E8 File Offset: 0x00303BE8
		// (set) Token: 0x0601345A RID: 78938 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<PresetLineDashValues> Val
		{
			get
			{
				return (EnumValue<PresetLineDashValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601345C RID: 78940 RVA: 0x003059F7 File Offset: 0x00303BF7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<PresetLineDashValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601345D RID: 78941 RVA: 0x00305A17 File Offset: 0x00303C17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetDash>(deep);
		}

		// Token: 0x0601345E RID: 78942 RVA: 0x00305A20 File Offset: 0x00303C20
		// Note: this type is marked as 'beforefieldinit'.
		static PresetDash()
		{
			byte[] array = new byte[1];
			PresetDash.attributeNamespaceIds = array;
		}

		// Token: 0x04008567 RID: 34151
		private const string tagName = "prstDash";

		// Token: 0x04008568 RID: 34152
		private const byte tagNsId = 10;

		// Token: 0x04008569 RID: 34153
		internal const int ElementTypeIdConst = 10091;

		// Token: 0x0400856A RID: 34154
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400856B RID: 34155
		private static byte[] attributeNamespaceIds;
	}
}
