using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002481 RID: 9345
	[ChildElementInfo(typeof(RecordIncluded))]
	[ChildElementInfo(typeof(RecordHashCode))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SingleDataSourceRecord : OpenXmlCompositeElement
	{
		// Token: 0x1700514C RID: 20812
		// (get) Token: 0x060113F3 RID: 70643 RVA: 0x002EC0C0 File Offset: 0x002EA2C0
		public override string LocalName
		{
			get
			{
				return "recipientData";
			}
		}

		// Token: 0x1700514D RID: 20813
		// (get) Token: 0x060113F4 RID: 70644 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700514E RID: 20814
		// (get) Token: 0x060113F5 RID: 70645 RVA: 0x002EC0C7 File Offset: 0x002EA2C7
		internal override int ElementTypeId
		{
			get
			{
				return 12572;
			}
		}

		// Token: 0x060113F6 RID: 70646 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060113F7 RID: 70647 RVA: 0x00293ECF File Offset: 0x002920CF
		public SingleDataSourceRecord()
		{
		}

		// Token: 0x060113F8 RID: 70648 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SingleDataSourceRecord(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113F9 RID: 70649 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SingleDataSourceRecord(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113FA RID: 70650 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SingleDataSourceRecord(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113FB RID: 70651 RVA: 0x002EC0CE File Offset: 0x002EA2CE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "active" == name)
			{
				return new RecordIncluded();
			}
			if (33 == namespaceId && "hash" == name)
			{
				return new RecordHashCode();
			}
			return null;
		}

		// Token: 0x1700514F RID: 20815
		// (get) Token: 0x060113FC RID: 70652 RVA: 0x002EC101 File Offset: 0x002EA301
		internal override string[] ElementTagNames
		{
			get
			{
				return SingleDataSourceRecord.eleTagNames;
			}
		}

		// Token: 0x17005150 RID: 20816
		// (get) Token: 0x060113FD RID: 70653 RVA: 0x002EC108 File Offset: 0x002EA308
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SingleDataSourceRecord.eleNamespaceIds;
			}
		}

		// Token: 0x17005151 RID: 20817
		// (get) Token: 0x060113FE RID: 70654 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005152 RID: 20818
		// (get) Token: 0x060113FF RID: 70655 RVA: 0x002EC10F File Offset: 0x002EA30F
		// (set) Token: 0x06011400 RID: 70656 RVA: 0x002EC118 File Offset: 0x002EA318
		public RecordIncluded RecordIncluded
		{
			get
			{
				return base.GetElement<RecordIncluded>(0);
			}
			set
			{
				base.SetElement<RecordIncluded>(0, value);
			}
		}

		// Token: 0x17005153 RID: 20819
		// (get) Token: 0x06011401 RID: 70657 RVA: 0x002EC122 File Offset: 0x002EA322
		// (set) Token: 0x06011402 RID: 70658 RVA: 0x002EC12B File Offset: 0x002EA32B
		public RecordHashCode RecordHashCode
		{
			get
			{
				return base.GetElement<RecordHashCode>(1);
			}
			set
			{
				base.SetElement<RecordHashCode>(1, value);
			}
		}

		// Token: 0x06011403 RID: 70659 RVA: 0x002EC135 File Offset: 0x002EA335
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SingleDataSourceRecord>(deep);
		}

		// Token: 0x040078DC RID: 30940
		private const string tagName = "recipientData";

		// Token: 0x040078DD RID: 30941
		private const byte tagNsId = 33;

		// Token: 0x040078DE RID: 30942
		internal const int ElementTypeIdConst = 12572;

		// Token: 0x040078DF RID: 30943
		private static readonly string[] eleTagNames = new string[] { "active", "hash" };

		// Token: 0x040078E0 RID: 30944
		private static readonly byte[] eleNamespaceIds = new byte[] { 33, 33 };
	}
}
