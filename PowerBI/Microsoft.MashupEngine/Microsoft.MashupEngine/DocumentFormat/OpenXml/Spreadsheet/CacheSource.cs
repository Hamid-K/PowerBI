using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC8 RID: 11464
	[ChildElementInfo(typeof(WorksheetSource))]
	[ChildElementInfo(typeof(Consolidation))]
	[ChildElementInfo(typeof(CacheSourceExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheSource : OpenXmlCompositeElement
	{
		// Token: 0x17008524 RID: 34084
		// (get) Token: 0x060188C8 RID: 100552 RVA: 0x00342782 File Offset: 0x00340982
		public override string LocalName
		{
			get
			{
				return "cacheSource";
			}
		}

		// Token: 0x17008525 RID: 34085
		// (get) Token: 0x060188C9 RID: 100553 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008526 RID: 34086
		// (get) Token: 0x060188CA RID: 100554 RVA: 0x00342789 File Offset: 0x00340989
		internal override int ElementTypeId
		{
			get
			{
				return 11445;
			}
		}

		// Token: 0x060188CB RID: 100555 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008527 RID: 34087
		// (get) Token: 0x060188CC RID: 100556 RVA: 0x00342790 File Offset: 0x00340990
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheSource.attributeTagNames;
			}
		}

		// Token: 0x17008528 RID: 34088
		// (get) Token: 0x060188CD RID: 100557 RVA: 0x00342797 File Offset: 0x00340997
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheSource.attributeNamespaceIds;
			}
		}

		// Token: 0x17008529 RID: 34089
		// (get) Token: 0x060188CE RID: 100558 RVA: 0x0034279E File Offset: 0x0034099E
		// (set) Token: 0x060188CF RID: 100559 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<SourceValues> Type
		{
			get
			{
				return (EnumValue<SourceValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700852A RID: 34090
		// (get) Token: 0x060188D0 RID: 100560 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060188D1 RID: 100561 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "connectionId")]
		public UInt32Value ConnectionId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060188D2 RID: 100562 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheSource()
		{
		}

		// Token: 0x060188D3 RID: 100563 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheSource(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188D4 RID: 100564 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheSource(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188D5 RID: 100565 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheSource(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188D6 RID: 100566 RVA: 0x003427B0 File Offset: 0x003409B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "worksheetSource" == name)
			{
				return new WorksheetSource();
			}
			if (22 == namespaceId && "consolidation" == name)
			{
				return new Consolidation();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new CacheSourceExtensionList();
			}
			return null;
		}

		// Token: 0x1700852B RID: 34091
		// (get) Token: 0x060188D7 RID: 100567 RVA: 0x00342806 File Offset: 0x00340A06
		internal override string[] ElementTagNames
		{
			get
			{
				return CacheSource.eleTagNames;
			}
		}

		// Token: 0x1700852C RID: 34092
		// (get) Token: 0x060188D8 RID: 100568 RVA: 0x0034280D File Offset: 0x00340A0D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CacheSource.eleNamespaceIds;
			}
		}

		// Token: 0x1700852D RID: 34093
		// (get) Token: 0x060188D9 RID: 100569 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700852E RID: 34094
		// (get) Token: 0x060188DA RID: 100570 RVA: 0x00342814 File Offset: 0x00340A14
		// (set) Token: 0x060188DB RID: 100571 RVA: 0x0034281D File Offset: 0x00340A1D
		public WorksheetSource WorksheetSource
		{
			get
			{
				return base.GetElement<WorksheetSource>(0);
			}
			set
			{
				base.SetElement<WorksheetSource>(0, value);
			}
		}

		// Token: 0x1700852F RID: 34095
		// (get) Token: 0x060188DC RID: 100572 RVA: 0x00342827 File Offset: 0x00340A27
		// (set) Token: 0x060188DD RID: 100573 RVA: 0x00342830 File Offset: 0x00340A30
		public Consolidation Consolidation
		{
			get
			{
				return base.GetElement<Consolidation>(1);
			}
			set
			{
				base.SetElement<Consolidation>(1, value);
			}
		}

		// Token: 0x17008530 RID: 34096
		// (get) Token: 0x060188DE RID: 100574 RVA: 0x0034283A File Offset: 0x00340A3A
		// (set) Token: 0x060188DF RID: 100575 RVA: 0x00342843 File Offset: 0x00340A43
		public CacheSourceExtensionList CacheSourceExtensionList
		{
			get
			{
				return base.GetElement<CacheSourceExtensionList>(2);
			}
			set
			{
				base.SetElement<CacheSourceExtensionList>(2, value);
			}
		}

		// Token: 0x060188E0 RID: 100576 RVA: 0x0034284D File Offset: 0x00340A4D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<SourceValues>();
			}
			if (namespaceId == 0 && "connectionId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060188E1 RID: 100577 RVA: 0x00342883 File Offset: 0x00340A83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheSource>(deep);
		}

		// Token: 0x060188E2 RID: 100578 RVA: 0x0034288C File Offset: 0x00340A8C
		// Note: this type is marked as 'beforefieldinit'.
		static CacheSource()
		{
			byte[] array = new byte[2];
			CacheSource.attributeNamespaceIds = array;
			CacheSource.eleTagNames = new string[] { "worksheetSource", "consolidation", "extLst" };
			CacheSource.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x0400A0C5 RID: 41157
		private const string tagName = "cacheSource";

		// Token: 0x0400A0C6 RID: 41158
		private const byte tagNsId = 22;

		// Token: 0x0400A0C7 RID: 41159
		internal const int ElementTypeIdConst = 11445;

		// Token: 0x0400A0C8 RID: 41160
		private static string[] attributeTagNames = new string[] { "type", "connectionId" };

		// Token: 0x0400A0C9 RID: 41161
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400A0CA RID: 41162
		private static readonly string[] eleTagNames;

		// Token: 0x0400A0CB RID: 41163
		private static readonly byte[] eleNamespaceIds;
	}
}
