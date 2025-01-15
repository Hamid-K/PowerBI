using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C7 RID: 9415
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Camera : OpenXmlLeafElement
	{
		// Token: 0x170052DC RID: 21212
		// (get) Token: 0x06011761 RID: 71521 RVA: 0x002EEAD0 File Offset: 0x002ECCD0
		public override string LocalName
		{
			get
			{
				return "camera";
			}
		}

		// Token: 0x170052DD RID: 21213
		// (get) Token: 0x06011762 RID: 71522 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052DE RID: 21214
		// (get) Token: 0x06011763 RID: 71523 RVA: 0x002EEAD7 File Offset: 0x002ECCD7
		internal override int ElementTypeId
		{
			get
			{
				return 12887;
			}
		}

		// Token: 0x06011764 RID: 71524 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052DF RID: 21215
		// (get) Token: 0x06011765 RID: 71525 RVA: 0x002EEADE File Offset: 0x002ECCDE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Camera.attributeTagNames;
			}
		}

		// Token: 0x170052E0 RID: 21216
		// (get) Token: 0x06011766 RID: 71526 RVA: 0x002EEAE5 File Offset: 0x002ECCE5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Camera.attributeNamespaceIds;
			}
		}

		// Token: 0x170052E1 RID: 21217
		// (get) Token: 0x06011767 RID: 71527 RVA: 0x002EEAEC File Offset: 0x002ECCEC
		// (set) Token: 0x06011768 RID: 71528 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "prst")]
		public EnumValue<PresetCameraTypeValues> PresetCameraType
		{
			get
			{
				return (EnumValue<PresetCameraTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601176A RID: 71530 RVA: 0x002EEAFB File Offset: 0x002ECCFB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "prst" == name)
			{
				return new EnumValue<PresetCameraTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601176B RID: 71531 RVA: 0x002EEB1D File Offset: 0x002ECD1D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Camera>(deep);
		}

		// Token: 0x040079EF RID: 31215
		private const string tagName = "camera";

		// Token: 0x040079F0 RID: 31216
		private const byte tagNsId = 52;

		// Token: 0x040079F1 RID: 31217
		internal const int ElementTypeIdConst = 12887;

		// Token: 0x040079F2 RID: 31218
		private static string[] attributeTagNames = new string[] { "prst" };

		// Token: 0x040079F3 RID: 31219
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
