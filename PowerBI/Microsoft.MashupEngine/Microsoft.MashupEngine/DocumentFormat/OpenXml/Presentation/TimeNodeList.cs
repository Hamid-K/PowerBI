using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3B RID: 10811
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ParallelTimeNode))]
	internal class TimeNodeList : OpenXmlCompositeElement
	{
		// Token: 0x17007123 RID: 28963
		// (get) Token: 0x06015BD4 RID: 89044 RVA: 0x003229CB File Offset: 0x00320BCB
		public override string LocalName
		{
			get
			{
				return "tnLst";
			}
		}

		// Token: 0x17007124 RID: 28964
		// (get) Token: 0x06015BD5 RID: 89045 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007125 RID: 28965
		// (get) Token: 0x06015BD6 RID: 89046 RVA: 0x003229D2 File Offset: 0x00320BD2
		internal override int ElementTypeId
		{
			get
			{
				return 12229;
			}
		}

		// Token: 0x06015BD7 RID: 89047 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015BD8 RID: 89048 RVA: 0x00293ECF File Offset: 0x002920CF
		public TimeNodeList()
		{
		}

		// Token: 0x06015BD9 RID: 89049 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TimeNodeList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BDA RID: 89050 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TimeNodeList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BDB RID: 89051 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TimeNodeList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015BDC RID: 89052 RVA: 0x003229D9 File Offset: 0x00320BD9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "par" == name)
			{
				return new ParallelTimeNode();
			}
			return null;
		}

		// Token: 0x17007126 RID: 28966
		// (get) Token: 0x06015BDD RID: 89053 RVA: 0x003229F4 File Offset: 0x00320BF4
		internal override string[] ElementTagNames
		{
			get
			{
				return TimeNodeList.eleTagNames;
			}
		}

		// Token: 0x17007127 RID: 28967
		// (get) Token: 0x06015BDE RID: 89054 RVA: 0x003229FB File Offset: 0x00320BFB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TimeNodeList.eleNamespaceIds;
			}
		}

		// Token: 0x17007128 RID: 28968
		// (get) Token: 0x06015BDF RID: 89055 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007129 RID: 28969
		// (get) Token: 0x06015BE0 RID: 89056 RVA: 0x00322A02 File Offset: 0x00320C02
		// (set) Token: 0x06015BE1 RID: 89057 RVA: 0x00322A0B File Offset: 0x00320C0B
		public ParallelTimeNode ParallelTimeNode
		{
			get
			{
				return base.GetElement<ParallelTimeNode>(0);
			}
			set
			{
				base.SetElement<ParallelTimeNode>(0, value);
			}
		}

		// Token: 0x06015BE2 RID: 89058 RVA: 0x00322A15 File Offset: 0x00320C15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimeNodeList>(deep);
		}

		// Token: 0x0400949B RID: 38043
		private const string tagName = "tnLst";

		// Token: 0x0400949C RID: 38044
		private const byte tagNsId = 24;

		// Token: 0x0400949D RID: 38045
		internal const int ElementTypeIdConst = 12229;

		// Token: 0x0400949E RID: 38046
		private static readonly string[] eleTagNames = new string[] { "par" };

		// Token: 0x0400949F RID: 38047
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
