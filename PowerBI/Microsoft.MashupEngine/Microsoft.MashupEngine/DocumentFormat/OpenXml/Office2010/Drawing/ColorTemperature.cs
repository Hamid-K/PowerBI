using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002374 RID: 9076
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTemperature : OpenXmlLeafElement
	{
		// Token: 0x17004AE0 RID: 19168
		// (get) Token: 0x06010575 RID: 66933 RVA: 0x002E2473 File Offset: 0x002E0673
		public override string LocalName
		{
			get
			{
				return "colorTemperature";
			}
		}

		// Token: 0x17004AE1 RID: 19169
		// (get) Token: 0x06010576 RID: 66934 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AE2 RID: 19170
		// (get) Token: 0x06010577 RID: 66935 RVA: 0x002E247A File Offset: 0x002E067A
		internal override int ElementTypeId
		{
			get
			{
				return 12759;
			}
		}

		// Token: 0x06010578 RID: 66936 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AE3 RID: 19171
		// (get) Token: 0x06010579 RID: 66937 RVA: 0x002E2481 File Offset: 0x002E0681
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorTemperature.attributeTagNames;
			}
		}

		// Token: 0x17004AE4 RID: 19172
		// (get) Token: 0x0601057A RID: 66938 RVA: 0x002E2488 File Offset: 0x002E0688
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorTemperature.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AE5 RID: 19173
		// (get) Token: 0x0601057B RID: 66939 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601057C RID: 66940 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "colorTemp")]
		public Int32Value ColorTemperatureValue
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

		// Token: 0x0601057E RID: 66942 RVA: 0x002E248F File Offset: 0x002E068F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "colorTemp" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601057F RID: 66943 RVA: 0x002E24AF File Offset: 0x002E06AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTemperature>(deep);
		}

		// Token: 0x06010580 RID: 66944 RVA: 0x002E24B8 File Offset: 0x002E06B8
		// Note: this type is marked as 'beforefieldinit'.
		static ColorTemperature()
		{
			byte[] array = new byte[1];
			ColorTemperature.attributeNamespaceIds = array;
		}

		// Token: 0x04007437 RID: 29751
		private const string tagName = "colorTemperature";

		// Token: 0x04007438 RID: 29752
		private const byte tagNsId = 48;

		// Token: 0x04007439 RID: 29753
		internal const int ElementTypeIdConst = 12759;

		// Token: 0x0400743A RID: 29754
		private static string[] attributeTagNames = new string[] { "colorTemp" };

		// Token: 0x0400743B RID: 29755
		private static byte[] attributeNamespaceIds;
	}
}
