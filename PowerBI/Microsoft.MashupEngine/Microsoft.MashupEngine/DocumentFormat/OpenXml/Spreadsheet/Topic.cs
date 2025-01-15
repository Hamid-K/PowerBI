using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C32 RID: 11314
	[ChildElementInfo(typeof(Subtopic))]
	[ChildElementInfo(typeof(TopicReferences))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Xstring))]
	internal class Topic : OpenXmlCompositeElement
	{
		// Token: 0x170080F8 RID: 33016
		// (get) Token: 0x06017EC9 RID: 97993 RVA: 0x0033C9EB File Offset: 0x0033ABEB
		public override string LocalName
		{
			get
			{
				return "tp";
			}
		}

		// Token: 0x170080F9 RID: 33017
		// (get) Token: 0x06017ECA RID: 97994 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080FA RID: 33018
		// (get) Token: 0x06017ECB RID: 97995 RVA: 0x0033C9F2 File Offset: 0x0033ABF2
		internal override int ElementTypeId
		{
			get
			{
				return 11295;
			}
		}

		// Token: 0x06017ECC RID: 97996 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080FB RID: 33019
		// (get) Token: 0x06017ECD RID: 97997 RVA: 0x0033C9F9 File Offset: 0x0033ABF9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Topic.attributeTagNames;
			}
		}

		// Token: 0x170080FC RID: 33020
		// (get) Token: 0x06017ECE RID: 97998 RVA: 0x0033CA00 File Offset: 0x0033AC00
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Topic.attributeNamespaceIds;
			}
		}

		// Token: 0x170080FD RID: 33021
		// (get) Token: 0x06017ECF RID: 97999 RVA: 0x0033CA07 File Offset: 0x0033AC07
		// (set) Token: 0x06017ED0 RID: 98000 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public EnumValue<VolatileValues> ValueType
		{
			get
			{
				return (EnumValue<VolatileValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017ED1 RID: 98001 RVA: 0x00293ECF File Offset: 0x002920CF
		public Topic()
		{
		}

		// Token: 0x06017ED2 RID: 98002 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Topic(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017ED3 RID: 98003 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Topic(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017ED4 RID: 98004 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Topic(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017ED5 RID: 98005 RVA: 0x0033CA18 File Offset: 0x0033AC18
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "v" == name)
			{
				return new Xstring();
			}
			if (22 == namespaceId && "stp" == name)
			{
				return new Subtopic();
			}
			if (22 == namespaceId && "tr" == name)
			{
				return new TopicReferences();
			}
			return null;
		}

		// Token: 0x170080FE RID: 33022
		// (get) Token: 0x06017ED6 RID: 98006 RVA: 0x0033CA6E File Offset: 0x0033AC6E
		internal override string[] ElementTagNames
		{
			get
			{
				return Topic.eleTagNames;
			}
		}

		// Token: 0x170080FF RID: 33023
		// (get) Token: 0x06017ED7 RID: 98007 RVA: 0x0033CA75 File Offset: 0x0033AC75
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Topic.eleNamespaceIds;
			}
		}

		// Token: 0x17008100 RID: 33024
		// (get) Token: 0x06017ED8 RID: 98008 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008101 RID: 33025
		// (get) Token: 0x06017ED9 RID: 98009 RVA: 0x0033BC27 File Offset: 0x00339E27
		// (set) Token: 0x06017EDA RID: 98010 RVA: 0x0033BC30 File Offset: 0x00339E30
		public Xstring Xstring
		{
			get
			{
				return base.GetElement<Xstring>(0);
			}
			set
			{
				base.SetElement<Xstring>(0, value);
			}
		}

		// Token: 0x06017EDB RID: 98011 RVA: 0x0033CA7C File Offset: 0x0033AC7C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<VolatileValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EDC RID: 98012 RVA: 0x0033CA9C File Offset: 0x0033AC9C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Topic>(deep);
		}

		// Token: 0x06017EDD RID: 98013 RVA: 0x0033CAA8 File Offset: 0x0033ACA8
		// Note: this type is marked as 'beforefieldinit'.
		static Topic()
		{
			byte[] array = new byte[1];
			Topic.attributeNamespaceIds = array;
			Topic.eleTagNames = new string[] { "v", "stp", "tr" };
			Topic.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009E2A RID: 40490
		private const string tagName = "tp";

		// Token: 0x04009E2B RID: 40491
		private const byte tagNsId = 22;

		// Token: 0x04009E2C RID: 40492
		internal const int ElementTypeIdConst = 11295;

		// Token: 0x04009E2D RID: 40493
		private static string[] attributeTagNames = new string[] { "t" };

		// Token: 0x04009E2E RID: 40494
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E2F RID: 40495
		private static readonly string[] eleTagNames;

		// Token: 0x04009E30 RID: 40496
		private static readonly byte[] eleNamespaceIds;
	}
}
