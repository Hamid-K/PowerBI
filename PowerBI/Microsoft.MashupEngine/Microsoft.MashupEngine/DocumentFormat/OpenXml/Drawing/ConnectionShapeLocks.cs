using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002792 RID: 10130
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ConnectionShapeLocks : OpenXmlCompositeElement
	{
		// Token: 0x17006200 RID: 25088
		// (get) Token: 0x0601393A RID: 80186 RVA: 0x00308702 File Offset: 0x00306902
		public override string LocalName
		{
			get
			{
				return "cxnSpLocks";
			}
		}

		// Token: 0x17006201 RID: 25089
		// (get) Token: 0x0601393B RID: 80187 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006202 RID: 25090
		// (get) Token: 0x0601393C RID: 80188 RVA: 0x00308709 File Offset: 0x00306909
		internal override int ElementTypeId
		{
			get
			{
				return 10167;
			}
		}

		// Token: 0x0601393D RID: 80189 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006203 RID: 25091
		// (get) Token: 0x0601393E RID: 80190 RVA: 0x00308710 File Offset: 0x00306910
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionShapeLocks.attributeTagNames;
			}
		}

		// Token: 0x17006204 RID: 25092
		// (get) Token: 0x0601393F RID: 80191 RVA: 0x00308717 File Offset: 0x00306917
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionShapeLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x17006205 RID: 25093
		// (get) Token: 0x06013940 RID: 80192 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013941 RID: 80193 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "noGrp")]
		public BooleanValue NoGrouping
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006206 RID: 25094
		// (get) Token: 0x06013942 RID: 80194 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013943 RID: 80195 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "noSelect")]
		public BooleanValue NoSelection
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

		// Token: 0x17006207 RID: 25095
		// (get) Token: 0x06013944 RID: 80196 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013945 RID: 80197 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "noRot")]
		public BooleanValue NoRotation
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006208 RID: 25096
		// (get) Token: 0x06013946 RID: 80198 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013947 RID: 80199 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "noChangeAspect")]
		public BooleanValue NoChangeAspect
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006209 RID: 25097
		// (get) Token: 0x06013948 RID: 80200 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06013949 RID: 80201 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "noMove")]
		public BooleanValue NoMove
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700620A RID: 25098
		// (get) Token: 0x0601394A RID: 80202 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601394B RID: 80203 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "noResize")]
		public BooleanValue NoResize
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700620B RID: 25099
		// (get) Token: 0x0601394C RID: 80204 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0601394D RID: 80205 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "noEditPoints")]
		public BooleanValue NoEditPoints
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700620C RID: 25100
		// (get) Token: 0x0601394E RID: 80206 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601394F RID: 80207 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "noAdjustHandles")]
		public BooleanValue NoAdjustHandles
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700620D RID: 25101
		// (get) Token: 0x06013950 RID: 80208 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06013951 RID: 80209 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "noChangeArrowheads")]
		public BooleanValue NoChangeArrowheads
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700620E RID: 25102
		// (get) Token: 0x06013952 RID: 80210 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06013953 RID: 80211 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "noChangeShapeType")]
		public BooleanValue NoChangeShapeType
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06013954 RID: 80212 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionShapeLocks()
		{
		}

		// Token: 0x06013955 RID: 80213 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionShapeLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013956 RID: 80214 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionShapeLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013957 RID: 80215 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionShapeLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013958 RID: 80216 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700620F RID: 25103
		// (get) Token: 0x06013959 RID: 80217 RVA: 0x0030871E File Offset: 0x0030691E
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionShapeLocks.eleTagNames;
			}
		}

		// Token: 0x17006210 RID: 25104
		// (get) Token: 0x0601395A RID: 80218 RVA: 0x00308725 File Offset: 0x00306925
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionShapeLocks.eleNamespaceIds;
			}
		}

		// Token: 0x17006211 RID: 25105
		// (get) Token: 0x0601395B RID: 80219 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006212 RID: 25106
		// (get) Token: 0x0601395C RID: 80220 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x0601395D RID: 80221 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601395E RID: 80222 RVA: 0x0030872C File Offset: 0x0030692C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "noGrp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noSelect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noRot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeAspect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noMove" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noResize" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noEditPoints" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noAdjustHandles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeArrowheads" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeShapeType" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601395F RID: 80223 RVA: 0x0030881D File Offset: 0x00306A1D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionShapeLocks>(deep);
		}

		// Token: 0x06013960 RID: 80224 RVA: 0x00308828 File Offset: 0x00306A28
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionShapeLocks()
		{
			byte[] array = new byte[10];
			ConnectionShapeLocks.attributeNamespaceIds = array;
			ConnectionShapeLocks.eleTagNames = new string[] { "extLst" };
			ConnectionShapeLocks.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040086D4 RID: 34516
		private const string tagName = "cxnSpLocks";

		// Token: 0x040086D5 RID: 34517
		private const byte tagNsId = 10;

		// Token: 0x040086D6 RID: 34518
		internal const int ElementTypeIdConst = 10167;

		// Token: 0x040086D7 RID: 34519
		private static string[] attributeTagNames = new string[] { "noGrp", "noSelect", "noRot", "noChangeAspect", "noMove", "noResize", "noEditPoints", "noAdjustHandles", "noChangeArrowheads", "noChangeShapeType" };

		// Token: 0x040086D8 RID: 34520
		private static byte[] attributeNamespaceIds;

		// Token: 0x040086D9 RID: 34521
		private static readonly string[] eleTagNames;

		// Token: 0x040086DA RID: 34522
		private static readonly byte[] eleNamespaceIds;
	}
}
