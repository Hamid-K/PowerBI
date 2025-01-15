using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002583 RID: 9603
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(PointCount))]
	[ChildElementInfo(typeof(Level))]
	internal class MultiLevelStringCache : OpenXmlCompositeElement
	{
		// Token: 0x17005628 RID: 22056
		// (get) Token: 0x06011EC6 RID: 73414 RVA: 0x002F3AEB File Offset: 0x002F1CEB
		public override string LocalName
		{
			get
			{
				return "multiLvlStrCache";
			}
		}

		// Token: 0x17005629 RID: 22057
		// (get) Token: 0x06011EC7 RID: 73415 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700562A RID: 22058
		// (get) Token: 0x06011EC8 RID: 73416 RVA: 0x002F3AF2 File Offset: 0x002F1CF2
		internal override int ElementTypeId
		{
			get
			{
				return 10402;
			}
		}

		// Token: 0x06011EC9 RID: 73417 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011ECA RID: 73418 RVA: 0x00293ECF File Offset: 0x002920CF
		public MultiLevelStringCache()
		{
		}

		// Token: 0x06011ECB RID: 73419 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MultiLevelStringCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011ECC RID: 73420 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MultiLevelStringCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011ECD RID: 73421 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MultiLevelStringCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011ECE RID: 73422 RVA: 0x002F3AFC File Offset: 0x002F1CFC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ptCount" == name)
			{
				return new PointCount();
			}
			if (11 == namespaceId && "lvl" == name)
			{
				return new Level();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700562B RID: 22059
		// (get) Token: 0x06011ECF RID: 73423 RVA: 0x002F3B52 File Offset: 0x002F1D52
		internal override string[] ElementTagNames
		{
			get
			{
				return MultiLevelStringCache.eleTagNames;
			}
		}

		// Token: 0x1700562C RID: 22060
		// (get) Token: 0x06011ED0 RID: 73424 RVA: 0x002F3B59 File Offset: 0x002F1D59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MultiLevelStringCache.eleNamespaceIds;
			}
		}

		// Token: 0x1700562D RID: 22061
		// (get) Token: 0x06011ED1 RID: 73425 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700562E RID: 22062
		// (get) Token: 0x06011ED2 RID: 73426 RVA: 0x002F3A0C File Offset: 0x002F1C0C
		// (set) Token: 0x06011ED3 RID: 73427 RVA: 0x002F3A15 File Offset: 0x002F1C15
		public PointCount PointCount
		{
			get
			{
				return base.GetElement<PointCount>(0);
			}
			set
			{
				base.SetElement<PointCount>(0, value);
			}
		}

		// Token: 0x06011ED4 RID: 73428 RVA: 0x002F3B60 File Offset: 0x002F1D60
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultiLevelStringCache>(deep);
		}

		// Token: 0x04007D44 RID: 32068
		private const string tagName = "multiLvlStrCache";

		// Token: 0x04007D45 RID: 32069
		private const byte tagNsId = 11;

		// Token: 0x04007D46 RID: 32070
		internal const int ElementTypeIdConst = 10402;

		// Token: 0x04007D47 RID: 32071
		private static readonly string[] eleTagNames = new string[] { "ptCount", "lvl", "extLst" };

		// Token: 0x04007D48 RID: 32072
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
