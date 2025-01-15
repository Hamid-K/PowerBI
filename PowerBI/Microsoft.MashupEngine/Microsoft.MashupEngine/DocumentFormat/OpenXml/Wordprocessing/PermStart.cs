using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC0 RID: 11968
	[GeneratedCode("DomGen", "2.0")]
	internal class PermStart : OpenXmlLeafElement
	{
		// Token: 0x17008C5E RID: 35934
		// (get) Token: 0x060197D5 RID: 104405 RVA: 0x0034C578 File Offset: 0x0034A778
		public override string LocalName
		{
			get
			{
				return "permStart";
			}
		}

		// Token: 0x17008C5F RID: 35935
		// (get) Token: 0x060197D6 RID: 104406 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C60 RID: 35936
		// (get) Token: 0x060197D7 RID: 104407 RVA: 0x0034C57F File Offset: 0x0034A77F
		internal override int ElementTypeId
		{
			get
			{
				return 11624;
			}
		}

		// Token: 0x060197D8 RID: 104408 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C61 RID: 35937
		// (get) Token: 0x060197D9 RID: 104409 RVA: 0x0034C586 File Offset: 0x0034A786
		internal override string[] AttributeTagNames
		{
			get
			{
				return PermStart.attributeTagNames;
			}
		}

		// Token: 0x17008C62 RID: 35938
		// (get) Token: 0x060197DA RID: 104410 RVA: 0x0034C58D File Offset: 0x0034A78D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PermStart.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C63 RID: 35939
		// (get) Token: 0x060197DB RID: 104411 RVA: 0x0034C594 File Offset: 0x0034A794
		// (set) Token: 0x060197DC RID: 104412 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "edGrp")]
		public EnumValue<RangePermissionEditingGroupValues> EditorGroup
		{
			get
			{
				return (EnumValue<RangePermissionEditingGroupValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008C64 RID: 35940
		// (get) Token: 0x060197DD RID: 104413 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060197DE RID: 104414 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "ed")]
		public StringValue Ed
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

		// Token: 0x17008C65 RID: 35941
		// (get) Token: 0x060197DF RID: 104415 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060197E0 RID: 104416 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "colFirst")]
		public Int32Value ColumnFirst
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008C66 RID: 35942
		// (get) Token: 0x060197E1 RID: 104417 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x060197E2 RID: 104418 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "colLast")]
		public Int32Value ColumnLast
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008C67 RID: 35943
		// (get) Token: 0x060197E3 RID: 104419 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x060197E4 RID: 104420 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008C68 RID: 35944
		// (get) Token: 0x060197E5 RID: 104421 RVA: 0x003444DA File Offset: 0x003426DA
		// (set) Token: 0x060197E6 RID: 104422 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "displacedByCustomXml")]
		public EnumValue<DisplacedByCustomXmlValues> DisplacedByCustomXml
		{
			get
			{
				return (EnumValue<DisplacedByCustomXmlValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060197E8 RID: 104424 RVA: 0x0034C5A4 File Offset: 0x0034A7A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "edGrp" == name)
			{
				return new EnumValue<RangePermissionEditingGroupValues>();
			}
			if (23 == namespaceId && "ed" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "colFirst" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "colLast" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "displacedByCustomXml" == name)
			{
				return new EnumValue<DisplacedByCustomXmlValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060197E9 RID: 104425 RVA: 0x0034C649 File Offset: 0x0034A849
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PermStart>(deep);
		}

		// Token: 0x0400A912 RID: 43282
		private const string tagName = "permStart";

		// Token: 0x0400A913 RID: 43283
		private const byte tagNsId = 23;

		// Token: 0x0400A914 RID: 43284
		internal const int ElementTypeIdConst = 11624;

		// Token: 0x0400A915 RID: 43285
		private static string[] attributeTagNames = new string[] { "edGrp", "ed", "colFirst", "colLast", "id", "displacedByCustomXml" };

		// Token: 0x0400A916 RID: 43286
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
