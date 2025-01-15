using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC1 RID: 10945
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TimeAbsolute))]
	[ChildElementInfo(typeof(TimePercentage))]
	internal class Iterate : OpenXmlCompositeElement
	{
		// Token: 0x1700751A RID: 29978
		// (get) Token: 0x060164BC RID: 91324 RVA: 0x00328A4C File Offset: 0x00326C4C
		public override string LocalName
		{
			get
			{
				return "iterate";
			}
		}

		// Token: 0x1700751B RID: 29979
		// (get) Token: 0x060164BD RID: 91325 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700751C RID: 29980
		// (get) Token: 0x060164BE RID: 91326 RVA: 0x00328A53 File Offset: 0x00326C53
		internal override int ElementTypeId
		{
			get
			{
				return 12363;
			}
		}

		// Token: 0x060164BF RID: 91327 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700751D RID: 29981
		// (get) Token: 0x060164C0 RID: 91328 RVA: 0x00328A5A File Offset: 0x00326C5A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Iterate.attributeTagNames;
			}
		}

		// Token: 0x1700751E RID: 29982
		// (get) Token: 0x060164C1 RID: 91329 RVA: 0x00328A61 File Offset: 0x00326C61
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Iterate.attributeNamespaceIds;
			}
		}

		// Token: 0x1700751F RID: 29983
		// (get) Token: 0x060164C2 RID: 91330 RVA: 0x00328A68 File Offset: 0x00326C68
		// (set) Token: 0x060164C3 RID: 91331 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<IterateValues> Type
		{
			get
			{
				return (EnumValue<IterateValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007520 RID: 29984
		// (get) Token: 0x060164C4 RID: 91332 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060164C5 RID: 91333 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "backwards")]
		public BooleanValue Backwards
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060164C6 RID: 91334 RVA: 0x00293ECF File Offset: 0x002920CF
		public Iterate()
		{
		}

		// Token: 0x060164C7 RID: 91335 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Iterate(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164C8 RID: 91336 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Iterate(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164C9 RID: 91337 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Iterate(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060164CA RID: 91338 RVA: 0x00328A77 File Offset: 0x00326C77
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tmAbs" == name)
			{
				return new TimeAbsolute();
			}
			if (24 == namespaceId && "tmPct" == name)
			{
				return new TimePercentage();
			}
			return null;
		}

		// Token: 0x17007521 RID: 29985
		// (get) Token: 0x060164CB RID: 91339 RVA: 0x00328AAA File Offset: 0x00326CAA
		internal override string[] ElementTagNames
		{
			get
			{
				return Iterate.eleTagNames;
			}
		}

		// Token: 0x17007522 RID: 29986
		// (get) Token: 0x060164CC RID: 91340 RVA: 0x00328AB1 File Offset: 0x00326CB1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Iterate.eleNamespaceIds;
			}
		}

		// Token: 0x17007523 RID: 29987
		// (get) Token: 0x060164CD RID: 91341 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007524 RID: 29988
		// (get) Token: 0x060164CE RID: 91342 RVA: 0x00328AB8 File Offset: 0x00326CB8
		// (set) Token: 0x060164CF RID: 91343 RVA: 0x00328AC1 File Offset: 0x00326CC1
		public TimeAbsolute TimeAbsolute
		{
			get
			{
				return base.GetElement<TimeAbsolute>(0);
			}
			set
			{
				base.SetElement<TimeAbsolute>(0, value);
			}
		}

		// Token: 0x17007525 RID: 29989
		// (get) Token: 0x060164D0 RID: 91344 RVA: 0x00328ACB File Offset: 0x00326CCB
		// (set) Token: 0x060164D1 RID: 91345 RVA: 0x00328AD4 File Offset: 0x00326CD4
		public TimePercentage TimePercentage
		{
			get
			{
				return base.GetElement<TimePercentage>(1);
			}
			set
			{
				base.SetElement<TimePercentage>(1, value);
			}
		}

		// Token: 0x060164D2 RID: 91346 RVA: 0x00328ADE File Offset: 0x00326CDE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<IterateValues>();
			}
			if (namespaceId == 0 && "backwards" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060164D3 RID: 91347 RVA: 0x00328B14 File Offset: 0x00326D14
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Iterate>(deep);
		}

		// Token: 0x060164D4 RID: 91348 RVA: 0x00328B20 File Offset: 0x00326D20
		// Note: this type is marked as 'beforefieldinit'.
		static Iterate()
		{
			byte[] array = new byte[2];
			Iterate.attributeNamespaceIds = array;
			Iterate.eleTagNames = new string[] { "tmAbs", "tmPct" };
			Iterate.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009714 RID: 38676
		private const string tagName = "iterate";

		// Token: 0x04009715 RID: 38677
		private const byte tagNsId = 24;

		// Token: 0x04009716 RID: 38678
		internal const int ElementTypeIdConst = 12363;

		// Token: 0x04009717 RID: 38679
		private static string[] attributeTagNames = new string[] { "type", "backwards" };

		// Token: 0x04009718 RID: 38680
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009719 RID: 38681
		private static readonly string[] eleTagNames;

		// Token: 0x0400971A RID: 38682
		private static readonly byte[] eleNamespaceIds;
	}
}
