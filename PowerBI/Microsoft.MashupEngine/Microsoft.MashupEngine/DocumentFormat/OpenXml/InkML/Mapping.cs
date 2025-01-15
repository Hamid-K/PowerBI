using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308A RID: 12426
	[ChildElementInfo(typeof(Bind))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Table))]
	[ChildElementInfo(typeof(Matrix))]
	[ChildElementInfo(typeof(Mapping))]
	internal class Mapping : OpenXmlCompositeElement
	{
		// Token: 0x1700976E RID: 38766
		// (get) Token: 0x0601AFFD RID: 110589 RVA: 0x0036A847 File Offset: 0x00368A47
		public override string LocalName
		{
			get
			{
				return "mapping";
			}
		}

		// Token: 0x1700976F RID: 38767
		// (get) Token: 0x0601AFFE RID: 110590 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009770 RID: 38768
		// (get) Token: 0x0601AFFF RID: 110591 RVA: 0x0036A84E File Offset: 0x00368A4E
		internal override int ElementTypeId
		{
			get
			{
				return 12647;
			}
		}

		// Token: 0x0601B000 RID: 110592 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009771 RID: 38769
		// (get) Token: 0x0601B001 RID: 110593 RVA: 0x0036A855 File Offset: 0x00368A55
		internal override string[] AttributeTagNames
		{
			get
			{
				return Mapping.attributeTagNames;
			}
		}

		// Token: 0x17009772 RID: 38770
		// (get) Token: 0x0601B002 RID: 110594 RVA: 0x0036A85C File Offset: 0x00368A5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Mapping.attributeNamespaceIds;
			}
		}

		// Token: 0x17009773 RID: 38771
		// (get) Token: 0x0601B003 RID: 110595 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B004 RID: 110596 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
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

		// Token: 0x17009774 RID: 38772
		// (get) Token: 0x0601B005 RID: 110597 RVA: 0x0036A863 File Offset: 0x00368A63
		// (set) Token: 0x0601B006 RID: 110598 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<MappingTypeValues> Type
		{
			get
			{
				return (EnumValue<MappingTypeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009775 RID: 38773
		// (get) Token: 0x0601B007 RID: 110599 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B008 RID: 110600 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "mappingRef")]
		public StringValue MappingRef
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601B009 RID: 110601 RVA: 0x00293ECF File Offset: 0x002920CF
		public Mapping()
		{
		}

		// Token: 0x0601B00A RID: 110602 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Mapping(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B00B RID: 110603 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Mapping(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B00C RID: 110604 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Mapping(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B00D RID: 110605 RVA: 0x0036A874 File Offset: 0x00368A74
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "bind" == name)
			{
				return new Bind();
			}
			if (43 == namespaceId && "table" == name)
			{
				return new Table();
			}
			if (43 == namespaceId && "matrix" == name)
			{
				return new Matrix();
			}
			if (43 == namespaceId && "mapping" == name)
			{
				return new Mapping();
			}
			return null;
		}

		// Token: 0x0601B00E RID: 110606 RVA: 0x0036A8E4 File Offset: 0x00368AE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<MappingTypeValues>();
			}
			if (namespaceId == 0 && "mappingRef" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B00F RID: 110607 RVA: 0x0036A93C File Offset: 0x00368B3C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Mapping>(deep);
		}

		// Token: 0x0601B010 RID: 110608 RVA: 0x0036A948 File Offset: 0x00368B48
		// Note: this type is marked as 'beforefieldinit'.
		static Mapping()
		{
			byte[] array = new byte[3];
			array[0] = 1;
			Mapping.attributeNamespaceIds = array;
		}

		// Token: 0x0400B281 RID: 45697
		private const string tagName = "mapping";

		// Token: 0x0400B282 RID: 45698
		private const byte tagNsId = 43;

		// Token: 0x0400B283 RID: 45699
		internal const int ElementTypeIdConst = 12647;

		// Token: 0x0400B284 RID: 45700
		private static string[] attributeTagNames = new string[] { "id", "type", "mappingRef" };

		// Token: 0x0400B285 RID: 45701
		private static byte[] attributeNamespaceIds;
	}
}
