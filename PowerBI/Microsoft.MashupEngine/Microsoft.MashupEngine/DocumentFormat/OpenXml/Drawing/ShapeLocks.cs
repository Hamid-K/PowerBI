using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002791 RID: 10129
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ShapeLocks : OpenXmlCompositeElement
	{
		// Token: 0x170061EC RID: 25068
		// (get) Token: 0x06013911 RID: 80145 RVA: 0x0030851E File Offset: 0x0030671E
		public override string LocalName
		{
			get
			{
				return "spLocks";
			}
		}

		// Token: 0x170061ED RID: 25069
		// (get) Token: 0x06013912 RID: 80146 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061EE RID: 25070
		// (get) Token: 0x06013913 RID: 80147 RVA: 0x00308525 File Offset: 0x00306725
		internal override int ElementTypeId
		{
			get
			{
				return 10166;
			}
		}

		// Token: 0x06013914 RID: 80148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170061EF RID: 25071
		// (get) Token: 0x06013915 RID: 80149 RVA: 0x0030852C File Offset: 0x0030672C
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeLocks.attributeTagNames;
			}
		}

		// Token: 0x170061F0 RID: 25072
		// (get) Token: 0x06013916 RID: 80150 RVA: 0x00308533 File Offset: 0x00306733
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x170061F1 RID: 25073
		// (get) Token: 0x06013917 RID: 80151 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013918 RID: 80152 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170061F2 RID: 25074
		// (get) Token: 0x06013919 RID: 80153 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601391A RID: 80154 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170061F3 RID: 25075
		// (get) Token: 0x0601391B RID: 80155 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601391C RID: 80156 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170061F4 RID: 25076
		// (get) Token: 0x0601391D RID: 80157 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601391E RID: 80158 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170061F5 RID: 25077
		// (get) Token: 0x0601391F RID: 80159 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06013920 RID: 80160 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170061F6 RID: 25078
		// (get) Token: 0x06013921 RID: 80161 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06013922 RID: 80162 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170061F7 RID: 25079
		// (get) Token: 0x06013923 RID: 80163 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06013924 RID: 80164 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170061F8 RID: 25080
		// (get) Token: 0x06013925 RID: 80165 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06013926 RID: 80166 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170061F9 RID: 25081
		// (get) Token: 0x06013927 RID: 80167 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06013928 RID: 80168 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170061FA RID: 25082
		// (get) Token: 0x06013929 RID: 80169 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601392A RID: 80170 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170061FB RID: 25083
		// (get) Token: 0x0601392B RID: 80171 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0601392C RID: 80172 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "noTextEdit")]
		public BooleanValue NoTextEdit
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

		// Token: 0x0601392D RID: 80173 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeLocks()
		{
		}

		// Token: 0x0601392E RID: 80174 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601392F RID: 80175 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013930 RID: 80176 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013931 RID: 80177 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170061FC RID: 25084
		// (get) Token: 0x06013932 RID: 80178 RVA: 0x0030853A File Offset: 0x0030673A
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeLocks.eleTagNames;
			}
		}

		// Token: 0x170061FD RID: 25085
		// (get) Token: 0x06013933 RID: 80179 RVA: 0x00308541 File Offset: 0x00306741
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeLocks.eleNamespaceIds;
			}
		}

		// Token: 0x170061FE RID: 25086
		// (get) Token: 0x06013934 RID: 80180 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170061FF RID: 25087
		// (get) Token: 0x06013935 RID: 80181 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06013936 RID: 80182 RVA: 0x002FA750 File Offset: 0x002F8950
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

		// Token: 0x06013937 RID: 80183 RVA: 0x00308548 File Offset: 0x00306748
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
			if (namespaceId == 0 && "noTextEdit" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013938 RID: 80184 RVA: 0x0030864F File Offset: 0x0030684F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeLocks>(deep);
		}

		// Token: 0x06013939 RID: 80185 RVA: 0x00308658 File Offset: 0x00306858
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeLocks()
		{
			byte[] array = new byte[11];
			ShapeLocks.attributeNamespaceIds = array;
			ShapeLocks.eleTagNames = new string[] { "extLst" };
			ShapeLocks.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040086CD RID: 34509
		private const string tagName = "spLocks";

		// Token: 0x040086CE RID: 34510
		private const byte tagNsId = 10;

		// Token: 0x040086CF RID: 34511
		internal const int ElementTypeIdConst = 10166;

		// Token: 0x040086D0 RID: 34512
		private static string[] attributeTagNames = new string[]
		{
			"noGrp", "noSelect", "noRot", "noChangeAspect", "noMove", "noResize", "noEditPoints", "noAdjustHandles", "noChangeArrowheads", "noChangeShapeType",
			"noTextEdit"
		};

		// Token: 0x040086D1 RID: 34513
		private static byte[] attributeNamespaceIds;

		// Token: 0x040086D2 RID: 34514
		private static readonly string[] eleTagNames;

		// Token: 0x040086D3 RID: 34515
		private static readonly byte[] eleNamespaceIds;
	}
}
