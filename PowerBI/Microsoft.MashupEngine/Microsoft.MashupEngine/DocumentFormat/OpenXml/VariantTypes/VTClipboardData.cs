using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292F RID: 10543
	[GeneratedCode("DomGen", "2.0")]
	internal class VTClipboardData : OpenXmlLeafTextElement
	{
		// Token: 0x17006AEB RID: 27371
		// (get) Token: 0x06014DF1 RID: 85489 RVA: 0x00318318 File Offset: 0x00316518
		public override string LocalName
		{
			get
			{
				return "cf";
			}
		}

		// Token: 0x17006AEC RID: 27372
		// (get) Token: 0x06014DF2 RID: 85490 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AED RID: 27373
		// (get) Token: 0x06014DF3 RID: 85491 RVA: 0x0031831F File Offset: 0x0031651F
		internal override int ElementTypeId
		{
			get
			{
				return 10997;
			}
		}

		// Token: 0x06014DF4 RID: 85492 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006AEE RID: 27374
		// (get) Token: 0x06014DF5 RID: 85493 RVA: 0x00318326 File Offset: 0x00316526
		internal override string[] AttributeTagNames
		{
			get
			{
				return VTClipboardData.attributeTagNames;
			}
		}

		// Token: 0x17006AEF RID: 27375
		// (get) Token: 0x06014DF6 RID: 85494 RVA: 0x0031832D File Offset: 0x0031652D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VTClipboardData.attributeNamespaceIds;
			}
		}

		// Token: 0x17006AF0 RID: 27376
		// (get) Token: 0x06014DF7 RID: 85495 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06014DF8 RID: 85496 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "format")]
		public Int32Value Format
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

		// Token: 0x17006AF1 RID: 27377
		// (get) Token: 0x06014DF9 RID: 85497 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06014DFA RID: 85498 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "size")]
		public UInt32Value Size
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06014DFB RID: 85499 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTClipboardData()
		{
		}

		// Token: 0x06014DFC RID: 85500 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTClipboardData(string text)
			: base(text)
		{
		}

		// Token: 0x06014DFD RID: 85501 RVA: 0x00318334 File Offset: 0x00316534
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DFE RID: 85502 RVA: 0x0031834F File Offset: 0x0031654F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "format" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014DFF RID: 85503 RVA: 0x00318385 File Offset: 0x00316585
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTClipboardData>(deep);
		}

		// Token: 0x06014E00 RID: 85504 RVA: 0x00318390 File Offset: 0x00316590
		// Note: this type is marked as 'beforefieldinit'.
		static VTClipboardData()
		{
			byte[] array = new byte[2];
			VTClipboardData.attributeNamespaceIds = array;
		}

		// Token: 0x04009041 RID: 36929
		private const string tagName = "cf";

		// Token: 0x04009042 RID: 36930
		private const byte tagNsId = 5;

		// Token: 0x04009043 RID: 36931
		internal const int ElementTypeIdConst = 10997;

		// Token: 0x04009044 RID: 36932
		private static string[] attributeTagNames = new string[] { "format", "size" };

		// Token: 0x04009045 RID: 36933
		private static byte[] attributeNamespaceIds;
	}
}
