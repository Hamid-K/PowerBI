using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002825 RID: 10277
	[GeneratedCode("DomGen", "2.0")]
	internal class SpacingPoints : OpenXmlLeafElement
	{
		// Token: 0x170065CF RID: 26063
		// (get) Token: 0x060141FD RID: 82429 RVA: 0x0030F993 File Offset: 0x0030DB93
		public override string LocalName
		{
			get
			{
				return "spcPts";
			}
		}

		// Token: 0x170065D0 RID: 26064
		// (get) Token: 0x060141FE RID: 82430 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065D1 RID: 26065
		// (get) Token: 0x060141FF RID: 82431 RVA: 0x0030F99A File Offset: 0x0030DB9A
		internal override int ElementTypeId
		{
			get
			{
				return 10310;
			}
		}

		// Token: 0x06014200 RID: 82432 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065D2 RID: 26066
		// (get) Token: 0x06014201 RID: 82433 RVA: 0x0030F9A1 File Offset: 0x0030DBA1
		internal override string[] AttributeTagNames
		{
			get
			{
				return SpacingPoints.attributeTagNames;
			}
		}

		// Token: 0x170065D3 RID: 26067
		// (get) Token: 0x06014202 RID: 82434 RVA: 0x0030F9A8 File Offset: 0x0030DBA8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SpacingPoints.attributeNamespaceIds;
			}
		}

		// Token: 0x170065D4 RID: 26068
		// (get) Token: 0x06014203 RID: 82435 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06014204 RID: 82436 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
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

		// Token: 0x06014206 RID: 82438 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014207 RID: 82439 RVA: 0x0030F9AF File Offset: 0x0030DBAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SpacingPoints>(deep);
		}

		// Token: 0x06014208 RID: 82440 RVA: 0x0030F9B8 File Offset: 0x0030DBB8
		// Note: this type is marked as 'beforefieldinit'.
		static SpacingPoints()
		{
			byte[] array = new byte[1];
			SpacingPoints.attributeNamespaceIds = array;
		}

		// Token: 0x04008920 RID: 35104
		private const string tagName = "spcPts";

		// Token: 0x04008921 RID: 35105
		private const byte tagNsId = 10;

		// Token: 0x04008922 RID: 35106
		internal const int ElementTypeIdConst = 10310;

		// Token: 0x04008923 RID: 35107
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04008924 RID: 35108
		private static byte[] attributeNamespaceIds;
	}
}
