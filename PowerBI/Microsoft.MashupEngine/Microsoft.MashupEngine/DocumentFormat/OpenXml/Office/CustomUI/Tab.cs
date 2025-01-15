using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002292 RID: 8850
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Group))]
	internal class Tab : OpenXmlCompositeElement
	{
		// Token: 0x170040A6 RID: 16550
		// (get) Token: 0x0600EF87 RID: 61319 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x170040A7 RID: 16551
		// (get) Token: 0x0600EF88 RID: 61320 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040A8 RID: 16552
		// (get) Token: 0x0600EF89 RID: 61321 RVA: 0x002D0022 File Offset: 0x002CE222
		internal override int ElementTypeId
		{
			get
			{
				return 12608;
			}
		}

		// Token: 0x0600EF8A RID: 61322 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040A9 RID: 16553
		// (get) Token: 0x0600EF8B RID: 61323 RVA: 0x002D0029 File Offset: 0x002CE229
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tab.attributeTagNames;
			}
		}

		// Token: 0x170040AA RID: 16554
		// (get) Token: 0x0600EF8C RID: 61324 RVA: 0x002D0030 File Offset: 0x002CE230
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tab.attributeNamespaceIds;
			}
		}

		// Token: 0x170040AB RID: 16555
		// (get) Token: 0x0600EF8D RID: 61325 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EF8E RID: 61326 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
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

		// Token: 0x170040AC RID: 16556
		// (get) Token: 0x0600EF8F RID: 61327 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EF90 RID: 61328 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x170040AD RID: 16557
		// (get) Token: 0x0600EF91 RID: 61329 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EF92 RID: 61330 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170040AE RID: 16558
		// (get) Token: 0x0600EF93 RID: 61331 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EF94 RID: 61332 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170040AF RID: 16559
		// (get) Token: 0x0600EF95 RID: 61333 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EF96 RID: 61334 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170040B0 RID: 16560
		// (get) Token: 0x0600EF97 RID: 61335 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EF98 RID: 61336 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170040B1 RID: 16561
		// (get) Token: 0x0600EF99 RID: 61337 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EF9A RID: 61338 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170040B2 RID: 16562
		// (get) Token: 0x0600EF9B RID: 61339 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EF9C RID: 61340 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170040B3 RID: 16563
		// (get) Token: 0x0600EF9D RID: 61341 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EF9E RID: 61342 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170040B4 RID: 16564
		// (get) Token: 0x0600EF9F RID: 61343 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EFA0 RID: 61344 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170040B5 RID: 16565
		// (get) Token: 0x0600EFA1 RID: 61345 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600EFA2 RID: 61346 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170040B6 RID: 16566
		// (get) Token: 0x0600EFA3 RID: 61347 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EFA4 RID: 61348 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170040B7 RID: 16567
		// (get) Token: 0x0600EFA5 RID: 61349 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EFA6 RID: 61350 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170040B8 RID: 16568
		// (get) Token: 0x0600EFA7 RID: 61351 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EFA8 RID: 61352 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x0600EFA9 RID: 61353 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tab()
		{
		}

		// Token: 0x0600EFAA RID: 61354 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tab(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFAB RID: 61355 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tab(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFAC RID: 61356 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tab(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EFAD RID: 61357 RVA: 0x002D0037 File Offset: 0x002CE237
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "group" == name)
			{
				return new Group();
			}
			return null;
		}

		// Token: 0x0600EFAE RID: 61358 RVA: 0x002D0054 File Offset: 0x002CE254
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EFAF RID: 61359 RVA: 0x002D019D File Offset: 0x002CE39D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tab>(deep);
		}

		// Token: 0x0600EFB0 RID: 61360 RVA: 0x002D01A8 File Offset: 0x002CE3A8
		// Note: this type is marked as 'beforefieldinit'.
		static Tab()
		{
			byte[] array = new byte[14];
			Tab.attributeNamespaceIds = array;
		}

		// Token: 0x04007032 RID: 28722
		private const string tagName = "tab";

		// Token: 0x04007033 RID: 28723
		private const byte tagNsId = 34;

		// Token: 0x04007034 RID: 28724
		internal const int ElementTypeIdConst = 12608;

		// Token: 0x04007035 RID: 28725
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "idMso", "tag", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ",
			"visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x04007036 RID: 28726
		private static byte[] attributeNamespaceIds;
	}
}
