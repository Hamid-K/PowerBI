using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.ActiveX
{
	// Token: 0x0200229E RID: 8862
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ActiveXObjectProperty))]
	internal class ActiveXControlData : OpenXmlCompositeElement
	{
		// Token: 0x170040EB RID: 16619
		// (get) Token: 0x0600F02C RID: 61484 RVA: 0x002D07BA File Offset: 0x002CE9BA
		public override string LocalName
		{
			get
			{
				return "ocx";
			}
		}

		// Token: 0x170040EC RID: 16620
		// (get) Token: 0x0600F02D RID: 61485 RVA: 0x002D07C1 File Offset: 0x002CE9C1
		internal override byte NamespaceId
		{
			get
			{
				return 35;
			}
		}

		// Token: 0x170040ED RID: 16621
		// (get) Token: 0x0600F02E RID: 61486 RVA: 0x002D07C5 File Offset: 0x002CE9C5
		internal override int ElementTypeId
		{
			get
			{
				return 12617;
			}
		}

		// Token: 0x0600F02F RID: 61487 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040EE RID: 16622
		// (get) Token: 0x0600F030 RID: 61488 RVA: 0x002D07CC File Offset: 0x002CE9CC
		internal override string[] AttributeTagNames
		{
			get
			{
				return ActiveXControlData.attributeTagNames;
			}
		}

		// Token: 0x170040EF RID: 16623
		// (get) Token: 0x0600F031 RID: 61489 RVA: 0x002D07D3 File Offset: 0x002CE9D3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ActiveXControlData.attributeNamespaceIds;
			}
		}

		// Token: 0x170040F0 RID: 16624
		// (get) Token: 0x0600F032 RID: 61490 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F033 RID: 61491 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(35, "classid")]
		public StringValue ActiveXControlClassId
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

		// Token: 0x170040F1 RID: 16625
		// (get) Token: 0x0600F034 RID: 61492 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F035 RID: 61493 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(35, "license")]
		public StringValue License
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

		// Token: 0x170040F2 RID: 16626
		// (get) Token: 0x0600F036 RID: 61494 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F037 RID: 61495 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x170040F3 RID: 16627
		// (get) Token: 0x0600F038 RID: 61496 RVA: 0x002D07DA File Offset: 0x002CE9DA
		// (set) Token: 0x0600F039 RID: 61497 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(35, "persistence")]
		public EnumValue<PersistenceValues> Persistence
		{
			get
			{
				return (EnumValue<PersistenceValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0600F03A RID: 61498 RVA: 0x00293ECF File Offset: 0x002920CF
		public ActiveXControlData()
		{
		}

		// Token: 0x0600F03B RID: 61499 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ActiveXControlData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F03C RID: 61500 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ActiveXControlData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F03D RID: 61501 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ActiveXControlData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F03E RID: 61502 RVA: 0x002D07E9 File Offset: 0x002CE9E9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "ocxPr" == name)
			{
				return new ActiveXObjectProperty();
			}
			return null;
		}

		// Token: 0x0600F03F RID: 61503 RVA: 0x002D0804 File Offset: 0x002CEA04
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "classid" == name)
			{
				return new StringValue();
			}
			if (35 == namespaceId && "license" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (35 == namespaceId && "persistence" == name)
			{
				return new EnumValue<PersistenceValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F040 RID: 61504 RVA: 0x002D0879 File Offset: 0x002CEA79
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ActiveXControlData>(deep);
		}

		// Token: 0x04007062 RID: 28770
		private const string tagName = "ocx";

		// Token: 0x04007063 RID: 28771
		private const byte tagNsId = 35;

		// Token: 0x04007064 RID: 28772
		internal const int ElementTypeIdConst = 12617;

		// Token: 0x04007065 RID: 28773
		private static string[] attributeTagNames = new string[] { "classid", "license", "id", "persistence" };

		// Token: 0x04007066 RID: 28774
		private static byte[] attributeNamespaceIds = new byte[] { 35, 35, 19, 35 };
	}
}
