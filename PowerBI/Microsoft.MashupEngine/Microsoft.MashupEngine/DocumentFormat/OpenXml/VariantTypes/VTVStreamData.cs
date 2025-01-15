using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292D RID: 10541
	[GeneratedCode("DomGen", "2.0")]
	internal class VTVStreamData : OpenXmlLeafTextElement
	{
		// Token: 0x17006AE2 RID: 27362
		// (get) Token: 0x06014DDB RID: 85467 RVA: 0x00318254 File Offset: 0x00316454
		public override string LocalName
		{
			get
			{
				return "vstream";
			}
		}

		// Token: 0x17006AE3 RID: 27363
		// (get) Token: 0x06014DDC RID: 85468 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AE4 RID: 27364
		// (get) Token: 0x06014DDD RID: 85469 RVA: 0x0031825B File Offset: 0x0031645B
		internal override int ElementTypeId
		{
			get
			{
				return 10995;
			}
		}

		// Token: 0x06014DDE RID: 85470 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006AE5 RID: 27365
		// (get) Token: 0x06014DDF RID: 85471 RVA: 0x00318262 File Offset: 0x00316462
		internal override string[] AttributeTagNames
		{
			get
			{
				return VTVStreamData.attributeTagNames;
			}
		}

		// Token: 0x17006AE6 RID: 27366
		// (get) Token: 0x06014DE0 RID: 85472 RVA: 0x00318269 File Offset: 0x00316469
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VTVStreamData.attributeNamespaceIds;
			}
		}

		// Token: 0x17006AE7 RID: 27367
		// (get) Token: 0x06014DE1 RID: 85473 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014DE2 RID: 85474 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "version")]
		public StringValue Version
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014DE3 RID: 85475 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTVStreamData()
		{
		}

		// Token: 0x06014DE4 RID: 85476 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTVStreamData(string text)
			: base(text)
		{
		}

		// Token: 0x06014DE5 RID: 85477 RVA: 0x00318270 File Offset: 0x00316470
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DE6 RID: 85478 RVA: 0x0031828B File Offset: 0x0031648B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "version" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014DE7 RID: 85479 RVA: 0x003182AB File Offset: 0x003164AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTVStreamData>(deep);
		}

		// Token: 0x06014DE8 RID: 85480 RVA: 0x003182B4 File Offset: 0x003164B4
		// Note: this type is marked as 'beforefieldinit'.
		static VTVStreamData()
		{
			byte[] array = new byte[1];
			VTVStreamData.attributeNamespaceIds = array;
		}

		// Token: 0x04009039 RID: 36921
		private const string tagName = "vstream";

		// Token: 0x0400903A RID: 36922
		private const byte tagNsId = 5;

		// Token: 0x0400903B RID: 36923
		internal const int ElementTypeIdConst = 10995;

		// Token: 0x0400903C RID: 36924
		private static string[] attributeTagNames = new string[] { "version" };

		// Token: 0x0400903D RID: 36925
		private static byte[] attributeNamespaceIds;
	}
}
