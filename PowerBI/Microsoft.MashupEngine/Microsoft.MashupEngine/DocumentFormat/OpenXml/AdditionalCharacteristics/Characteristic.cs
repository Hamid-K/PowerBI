using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.AdditionalCharacteristics
{
	// Token: 0x02002906 RID: 10502
	[GeneratedCode("DomGen", "2.0")]
	internal class Characteristic : OpenXmlLeafElement
	{
		// Token: 0x17006A04 RID: 27140
		// (get) Token: 0x06014BCF RID: 84943 RVA: 0x00316566 File Offset: 0x00314766
		public override string LocalName
		{
			get
			{
				return "characteristic";
			}
		}

		// Token: 0x17006A05 RID: 27141
		// (get) Token: 0x06014BD0 RID: 84944 RVA: 0x000024ED File Offset: 0x000006ED
		internal override byte NamespaceId
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17006A06 RID: 27142
		// (get) Token: 0x06014BD1 RID: 84945 RVA: 0x0031656D File Offset: 0x0031476D
		internal override int ElementTypeId
		{
			get
			{
				return 10757;
			}
		}

		// Token: 0x06014BD2 RID: 84946 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006A07 RID: 27143
		// (get) Token: 0x06014BD3 RID: 84947 RVA: 0x00316574 File Offset: 0x00314774
		internal override string[] AttributeTagNames
		{
			get
			{
				return Characteristic.attributeTagNames;
			}
		}

		// Token: 0x17006A08 RID: 27144
		// (get) Token: 0x06014BD4 RID: 84948 RVA: 0x0031657B File Offset: 0x0031477B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Characteristic.attributeNamespaceIds;
			}
		}

		// Token: 0x17006A09 RID: 27145
		// (get) Token: 0x06014BD5 RID: 84949 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014BD6 RID: 84950 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17006A0A RID: 27146
		// (get) Token: 0x06014BD7 RID: 84951 RVA: 0x00316582 File Offset: 0x00314782
		// (set) Token: 0x06014BD8 RID: 84952 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "relation")]
		public EnumValue<RelationValues> Relation
		{
			get
			{
				return (EnumValue<RelationValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006A0B RID: 27147
		// (get) Token: 0x06014BD9 RID: 84953 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06014BDA RID: 84954 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x17006A0C RID: 27148
		// (get) Token: 0x06014BDB RID: 84955 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06014BDC RID: 84956 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "vocabulary")]
		public StringValue Vocabulary
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06014BDE RID: 84958 RVA: 0x00316594 File Offset: 0x00314794
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "relation" == name)
			{
				return new EnumValue<RelationValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "vocabulary" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014BDF RID: 84959 RVA: 0x00316601 File Offset: 0x00314801
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Characteristic>(deep);
		}

		// Token: 0x06014BE0 RID: 84960 RVA: 0x0031660C File Offset: 0x0031480C
		// Note: this type is marked as 'beforefieldinit'.
		static Characteristic()
		{
			byte[] array = new byte[4];
			Characteristic.attributeNamespaceIds = array;
		}

		// Token: 0x04008FAF RID: 36783
		private const string tagName = "characteristic";

		// Token: 0x04008FB0 RID: 36784
		private const byte tagNsId = 8;

		// Token: 0x04008FB1 RID: 36785
		internal const int ElementTypeIdConst = 10757;

		// Token: 0x04008FB2 RID: 36786
		private static string[] attributeTagNames = new string[] { "name", "relation", "val", "vocabulary" };

		// Token: 0x04008FB3 RID: 36787
		private static byte[] attributeNamespaceIds;
	}
}
