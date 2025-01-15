using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AB RID: 9131
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class WheelReverseTransition : OpenXmlLeafElement
	{
		// Token: 0x17004C3C RID: 19516
		// (get) Token: 0x06010878 RID: 67704 RVA: 0x002E46F7 File Offset: 0x002E28F7
		public override string LocalName
		{
			get
			{
				return "wheelReverse";
			}
		}

		// Token: 0x17004C3D RID: 19517
		// (get) Token: 0x06010879 RID: 67705 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C3E RID: 19518
		// (get) Token: 0x0601087A RID: 67706 RVA: 0x002E46FE File Offset: 0x002E28FE
		internal override int ElementTypeId
		{
			get
			{
				return 12786;
			}
		}

		// Token: 0x0601087B RID: 67707 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C3F RID: 19519
		// (get) Token: 0x0601087C RID: 67708 RVA: 0x002E4705 File Offset: 0x002E2905
		internal override string[] AttributeTagNames
		{
			get
			{
				return WheelReverseTransition.attributeTagNames;
			}
		}

		// Token: 0x17004C40 RID: 19520
		// (get) Token: 0x0601087D RID: 67709 RVA: 0x002E470C File Offset: 0x002E290C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WheelReverseTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C41 RID: 19521
		// (get) Token: 0x0601087E RID: 67710 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601087F RID: 67711 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spokes")]
		public UInt32Value Spokes
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

		// Token: 0x06010881 RID: 67713 RVA: 0x002E4713 File Offset: 0x002E2913
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spokes" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010882 RID: 67714 RVA: 0x002E4733 File Offset: 0x002E2933
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WheelReverseTransition>(deep);
		}

		// Token: 0x06010883 RID: 67715 RVA: 0x002E473C File Offset: 0x002E293C
		// Note: this type is marked as 'beforefieldinit'.
		static WheelReverseTransition()
		{
			byte[] array = new byte[1];
			WheelReverseTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04007517 RID: 29975
		private const string tagName = "wheelReverse";

		// Token: 0x04007518 RID: 29976
		private const byte tagNsId = 49;

		// Token: 0x04007519 RID: 29977
		internal const int ElementTypeIdConst = 12786;

		// Token: 0x0400751A RID: 29978
		private static string[] attributeTagNames = new string[] { "spokes" };

		// Token: 0x0400751B RID: 29979
		private static byte[] attributeNamespaceIds;
	}
}
