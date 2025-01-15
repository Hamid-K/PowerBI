using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.CustomXmlSchemaReferences
{
	// Token: 0x02002B13 RID: 11027
	[GeneratedCode("DomGen", "2.0")]
	internal class Schema : OpenXmlLeafElement
	{
		// Token: 0x170075A8 RID: 30120
		// (get) Token: 0x060165F4 RID: 91636 RVA: 0x00329617 File Offset: 0x00327817
		public override string LocalName
		{
			get
			{
				return "schema";
			}
		}

		// Token: 0x170075A9 RID: 30121
		// (get) Token: 0x060165F5 RID: 91637 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		internal override byte NamespaceId
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170075AA RID: 30122
		// (get) Token: 0x060165F6 RID: 91638 RVA: 0x0032961E File Offset: 0x0032781E
		internal override int ElementTypeId
		{
			get
			{
				return 12398;
			}
		}

		// Token: 0x060165F7 RID: 91639 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170075AB RID: 30123
		// (get) Token: 0x060165F8 RID: 91640 RVA: 0x00329625 File Offset: 0x00327825
		internal override string[] AttributeTagNames
		{
			get
			{
				return Schema.attributeTagNames;
			}
		}

		// Token: 0x170075AC RID: 30124
		// (get) Token: 0x060165F9 RID: 91641 RVA: 0x0032962C File Offset: 0x0032782C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Schema.attributeNamespaceIds;
			}
		}

		// Token: 0x170075AD RID: 30125
		// (get) Token: 0x060165FA RID: 91642 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060165FB RID: 91643 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(25, "uri")]
		public StringValue Uri
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

		// Token: 0x170075AE RID: 30126
		// (get) Token: 0x060165FC RID: 91644 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060165FD RID: 91645 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(25, "manifestLocation")]
		public StringValue ManifestLocation
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

		// Token: 0x170075AF RID: 30127
		// (get) Token: 0x060165FE RID: 91646 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060165FF RID: 91647 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(25, "schemaLocation")]
		public StringValue SchemaLocation
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

		// Token: 0x06016601 RID: 91649 RVA: 0x00329634 File Offset: 0x00327834
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (25 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (25 == namespaceId && "manifestLocation" == name)
			{
				return new StringValue();
			}
			if (25 == namespaceId && "schemaLocation" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016602 RID: 91650 RVA: 0x00329691 File Offset: 0x00327891
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Schema>(deep);
		}

		// Token: 0x040098C0 RID: 39104
		private const string tagName = "schema";

		// Token: 0x040098C1 RID: 39105
		private const byte tagNsId = 25;

		// Token: 0x040098C2 RID: 39106
		internal const int ElementTypeIdConst = 12398;

		// Token: 0x040098C3 RID: 39107
		private static string[] attributeTagNames = new string[] { "uri", "manifestLocation", "schemaLocation" };

		// Token: 0x040098C4 RID: 39108
		private static byte[] attributeNamespaceIds = new byte[] { 25, 25, 25 };
	}
}
