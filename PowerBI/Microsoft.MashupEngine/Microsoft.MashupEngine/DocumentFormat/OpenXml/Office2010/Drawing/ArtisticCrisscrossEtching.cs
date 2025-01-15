using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235F RID: 9055
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticCrisscrossEtching : OpenXmlLeafElement
	{
		// Token: 0x17004A4B RID: 19019
		// (get) Token: 0x06010447 RID: 66631 RVA: 0x002E19CB File Offset: 0x002DFBCB
		public override string LocalName
		{
			get
			{
				return "artisticCrisscrossEtching";
			}
		}

		// Token: 0x17004A4C RID: 19020
		// (get) Token: 0x06010448 RID: 66632 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A4D RID: 19021
		// (get) Token: 0x06010449 RID: 66633 RVA: 0x002E19D2 File Offset: 0x002DFBD2
		internal override int ElementTypeId
		{
			get
			{
				return 12738;
			}
		}

		// Token: 0x0601044A RID: 66634 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A4E RID: 19022
		// (get) Token: 0x0601044B RID: 66635 RVA: 0x002E19D9 File Offset: 0x002DFBD9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticCrisscrossEtching.attributeTagNames;
			}
		}

		// Token: 0x17004A4F RID: 19023
		// (get) Token: 0x0601044C RID: 66636 RVA: 0x002E19E0 File Offset: 0x002DFBE0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticCrisscrossEtching.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A50 RID: 19024
		// (get) Token: 0x0601044D RID: 66637 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601044E RID: 66638 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "trans")]
		public Int32Value Transparancy
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

		// Token: 0x17004A51 RID: 19025
		// (get) Token: 0x0601044F RID: 66639 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010450 RID: 66640 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pressure")]
		public Int32Value Pressure
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

		// Token: 0x06010452 RID: 66642 RVA: 0x002E1953 File Offset: 0x002DFB53
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "pressure" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010453 RID: 66643 RVA: 0x002E19E7 File Offset: 0x002DFBE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticCrisscrossEtching>(deep);
		}

		// Token: 0x06010454 RID: 66644 RVA: 0x002E19F0 File Offset: 0x002DFBF0
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticCrisscrossEtching()
		{
			byte[] array = new byte[2];
			ArtisticCrisscrossEtching.attributeNamespaceIds = array;
		}

		// Token: 0x040073CE RID: 29646
		private const string tagName = "artisticCrisscrossEtching";

		// Token: 0x040073CF RID: 29647
		private const byte tagNsId = 48;

		// Token: 0x040073D0 RID: 29648
		internal const int ElementTypeIdConst = 12738;

		// Token: 0x040073D1 RID: 29649
		private static string[] attributeTagNames = new string[] { "trans", "pressure" };

		// Token: 0x040073D2 RID: 29650
		private static byte[] attributeNamespaceIds;
	}
}
