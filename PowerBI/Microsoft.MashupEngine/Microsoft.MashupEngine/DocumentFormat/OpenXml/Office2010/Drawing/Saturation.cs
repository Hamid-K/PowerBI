using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002375 RID: 9077
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Saturation : OpenXmlLeafElement
	{
		// Token: 0x17004AE6 RID: 19174
		// (get) Token: 0x06010581 RID: 66945 RVA: 0x002E24E7 File Offset: 0x002E06E7
		public override string LocalName
		{
			get
			{
				return "saturation";
			}
		}

		// Token: 0x17004AE7 RID: 19175
		// (get) Token: 0x06010582 RID: 66946 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AE8 RID: 19176
		// (get) Token: 0x06010583 RID: 66947 RVA: 0x002E24EE File Offset: 0x002E06EE
		internal override int ElementTypeId
		{
			get
			{
				return 12760;
			}
		}

		// Token: 0x06010584 RID: 66948 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AE9 RID: 19177
		// (get) Token: 0x06010585 RID: 66949 RVA: 0x002E24F5 File Offset: 0x002E06F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Saturation.attributeTagNames;
			}
		}

		// Token: 0x17004AEA RID: 19178
		// (get) Token: 0x06010586 RID: 66950 RVA: 0x002E24FC File Offset: 0x002E06FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Saturation.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AEB RID: 19179
		// (get) Token: 0x06010587 RID: 66951 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010588 RID: 66952 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sat")]
		public Int32Value SaturationAmount
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

		// Token: 0x0601058A RID: 66954 RVA: 0x002E2503 File Offset: 0x002E0703
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sat" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601058B RID: 66955 RVA: 0x002E2523 File Offset: 0x002E0723
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Saturation>(deep);
		}

		// Token: 0x0601058C RID: 66956 RVA: 0x002E252C File Offset: 0x002E072C
		// Note: this type is marked as 'beforefieldinit'.
		static Saturation()
		{
			byte[] array = new byte[1];
			Saturation.attributeNamespaceIds = array;
		}

		// Token: 0x0400743C RID: 29756
		private const string tagName = "saturation";

		// Token: 0x0400743D RID: 29757
		private const byte tagNsId = 48;

		// Token: 0x0400743E RID: 29758
		internal const int ElementTypeIdConst = 12760;

		// Token: 0x0400743F RID: 29759
		private static string[] attributeTagNames = new string[] { "sat" };

		// Token: 0x04007440 RID: 29760
		private static byte[] attributeNamespaceIds;
	}
}
