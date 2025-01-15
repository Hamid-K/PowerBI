using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002604 RID: 9732
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AutoUpdate))]
	internal class ExternalData : OpenXmlCompositeElement
	{
		// Token: 0x170059CA RID: 22986
		// (get) Token: 0x060126B8 RID: 75448 RVA: 0x002FAE8A File Offset: 0x002F908A
		public override string LocalName
		{
			get
			{
				return "externalData";
			}
		}

		// Token: 0x170059CB RID: 22987
		// (get) Token: 0x060126B9 RID: 75449 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059CC RID: 22988
		// (get) Token: 0x060126BA RID: 75450 RVA: 0x002FAE91 File Offset: 0x002F9091
		internal override int ElementTypeId
		{
			get
			{
				return 10579;
			}
		}

		// Token: 0x060126BB RID: 75451 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170059CD RID: 22989
		// (get) Token: 0x060126BC RID: 75452 RVA: 0x002FAE98 File Offset: 0x002F9098
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalData.attributeTagNames;
			}
		}

		// Token: 0x170059CE RID: 22990
		// (get) Token: 0x060126BD RID: 75453 RVA: 0x002FAE9F File Offset: 0x002F909F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalData.attributeNamespaceIds;
			}
		}

		// Token: 0x170059CF RID: 22991
		// (get) Token: 0x060126BE RID: 75454 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060126BF RID: 75455 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x060126C0 RID: 75456 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalData()
		{
		}

		// Token: 0x060126C1 RID: 75457 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126C2 RID: 75458 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126C3 RID: 75459 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060126C4 RID: 75460 RVA: 0x002FAEA6 File Offset: 0x002F90A6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "autoUpdate" == name)
			{
				return new AutoUpdate();
			}
			return null;
		}

		// Token: 0x170059D0 RID: 22992
		// (get) Token: 0x060126C5 RID: 75461 RVA: 0x002FAEC1 File Offset: 0x002F90C1
		internal override string[] ElementTagNames
		{
			get
			{
				return ExternalData.eleTagNames;
			}
		}

		// Token: 0x170059D1 RID: 22993
		// (get) Token: 0x060126C6 RID: 75462 RVA: 0x002FAEC8 File Offset: 0x002F90C8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ExternalData.eleNamespaceIds;
			}
		}

		// Token: 0x170059D2 RID: 22994
		// (get) Token: 0x060126C7 RID: 75463 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059D3 RID: 22995
		// (get) Token: 0x060126C8 RID: 75464 RVA: 0x002FAECF File Offset: 0x002F90CF
		// (set) Token: 0x060126C9 RID: 75465 RVA: 0x002FAED8 File Offset: 0x002F90D8
		public AutoUpdate AutoUpdate
		{
			get
			{
				return base.GetElement<AutoUpdate>(0);
			}
			set
			{
				base.SetElement<AutoUpdate>(0, value);
			}
		}

		// Token: 0x060126CA RID: 75466 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060126CB RID: 75467 RVA: 0x002FAEE2 File Offset: 0x002F90E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalData>(deep);
		}

		// Token: 0x04007F7A RID: 32634
		private const string tagName = "externalData";

		// Token: 0x04007F7B RID: 32635
		private const byte tagNsId = 11;

		// Token: 0x04007F7C RID: 32636
		internal const int ElementTypeIdConst = 10579;

		// Token: 0x04007F7D RID: 32637
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04007F7E RID: 32638
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x04007F7F RID: 32639
		private static readonly string[] eleTagNames = new string[] { "autoUpdate" };

		// Token: 0x04007F80 RID: 32640
		private static readonly byte[] eleNamespaceIds = new byte[] { 11 };
	}
}
