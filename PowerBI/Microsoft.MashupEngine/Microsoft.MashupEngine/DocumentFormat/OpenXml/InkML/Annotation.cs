using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003094 RID: 12436
	[GeneratedCode("DomGen", "2.0")]
	internal class Annotation : OpenXmlLeafTextElement
	{
		// Token: 0x170097B7 RID: 38839
		// (get) Token: 0x0601B0A3 RID: 110755 RVA: 0x0036AFD9 File Offset: 0x003691D9
		public override string LocalName
		{
			get
			{
				return "annotation";
			}
		}

		// Token: 0x170097B8 RID: 38840
		// (get) Token: 0x0601B0A4 RID: 110756 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097B9 RID: 38841
		// (get) Token: 0x0601B0A5 RID: 110757 RVA: 0x0036AFE0 File Offset: 0x003691E0
		internal override int ElementTypeId
		{
			get
			{
				return 12657;
			}
		}

		// Token: 0x0601B0A6 RID: 110758 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097BA RID: 38842
		// (get) Token: 0x0601B0A7 RID: 110759 RVA: 0x0036AFE7 File Offset: 0x003691E7
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentFormat.OpenXml.InkML.Annotation.attributeTagNames;
			}
		}

		// Token: 0x170097BB RID: 38843
		// (get) Token: 0x0601B0A8 RID: 110760 RVA: 0x0036AFEE File Offset: 0x003691EE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentFormat.OpenXml.InkML.Annotation.attributeNamespaceIds;
			}
		}

		// Token: 0x170097BC RID: 38844
		// (get) Token: 0x0601B0A9 RID: 110761 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B0AA RID: 110762 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x170097BD RID: 38845
		// (get) Token: 0x0601B0AB RID: 110763 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601B0AC RID: 110764 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "encoding")]
		public StringValue Encoding
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601B0AD RID: 110765 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Annotation()
		{
		}

		// Token: 0x0601B0AE RID: 110766 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Annotation(string text)
			: base(text)
		{
		}

		// Token: 0x0601B0AF RID: 110767 RVA: 0x0036AFF8 File Offset: 0x003691F8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601B0B0 RID: 110768 RVA: 0x0036B013 File Offset: 0x00369213
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "encoding" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B0B1 RID: 110769 RVA: 0x0036B049 File Offset: 0x00369249
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Annotation>(deep);
		}

		// Token: 0x0601B0B2 RID: 110770 RVA: 0x0036B054 File Offset: 0x00369254
		// Note: this type is marked as 'beforefieldinit'.
		static Annotation()
		{
			byte[] array = new byte[2];
			DocumentFormat.OpenXml.InkML.Annotation.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2AF RID: 45743
		private const string tagName = "annotation";

		// Token: 0x0400B2B0 RID: 45744
		private const byte tagNsId = 43;

		// Token: 0x0400B2B1 RID: 45745
		internal const int ElementTypeIdConst = 12657;

		// Token: 0x0400B2B2 RID: 45746
		private static string[] attributeTagNames = new string[] { "type", "encoding" };

		// Token: 0x0400B2B3 RID: 45747
		private static byte[] attributeNamespaceIds;
	}
}
