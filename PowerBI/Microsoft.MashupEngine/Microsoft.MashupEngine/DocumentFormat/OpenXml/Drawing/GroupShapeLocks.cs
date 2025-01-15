using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002796 RID: 10134
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GroupShapeLocks : OpenXmlCompositeElement
	{
		// Token: 0x1700621D RID: 25117
		// (get) Token: 0x06013976 RID: 80246 RVA: 0x0030897D File Offset: 0x00306B7D
		public override string LocalName
		{
			get
			{
				return "grpSpLocks";
			}
		}

		// Token: 0x1700621E RID: 25118
		// (get) Token: 0x06013977 RID: 80247 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700621F RID: 25119
		// (get) Token: 0x06013978 RID: 80248 RVA: 0x00308984 File Offset: 0x00306B84
		internal override int ElementTypeId
		{
			get
			{
				return 10170;
			}
		}

		// Token: 0x06013979 RID: 80249 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006220 RID: 25120
		// (get) Token: 0x0601397A RID: 80250 RVA: 0x0030898B File Offset: 0x00306B8B
		internal override string[] AttributeTagNames
		{
			get
			{
				return GroupShapeLocks.attributeTagNames;
			}
		}

		// Token: 0x17006221 RID: 25121
		// (get) Token: 0x0601397B RID: 80251 RVA: 0x00308992 File Offset: 0x00306B92
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GroupShapeLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x17006222 RID: 25122
		// (get) Token: 0x0601397C RID: 80252 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601397D RID: 80253 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006223 RID: 25123
		// (get) Token: 0x0601397E RID: 80254 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601397F RID: 80255 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "noUngrp")]
		public BooleanValue NoUngrouping
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

		// Token: 0x17006224 RID: 25124
		// (get) Token: 0x06013980 RID: 80256 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013981 RID: 80257 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "noSelect")]
		public BooleanValue NoSelection
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

		// Token: 0x17006225 RID: 25125
		// (get) Token: 0x06013982 RID: 80258 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013983 RID: 80259 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "noRot")]
		public BooleanValue NoRotation
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

		// Token: 0x17006226 RID: 25126
		// (get) Token: 0x06013984 RID: 80260 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06013985 RID: 80261 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "noChangeAspect")]
		public BooleanValue NoChangeAspect
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

		// Token: 0x17006227 RID: 25127
		// (get) Token: 0x06013986 RID: 80262 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06013987 RID: 80263 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "noMove")]
		public BooleanValue NoMove
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

		// Token: 0x17006228 RID: 25128
		// (get) Token: 0x06013988 RID: 80264 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06013989 RID: 80265 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "noResize")]
		public BooleanValue NoResize
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

		// Token: 0x0601398A RID: 80266 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShapeLocks()
		{
		}

		// Token: 0x0601398B RID: 80267 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShapeLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601398C RID: 80268 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShapeLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601398D RID: 80269 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShapeLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601398E RID: 80270 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006229 RID: 25129
		// (get) Token: 0x0601398F RID: 80271 RVA: 0x00308999 File Offset: 0x00306B99
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShapeLocks.eleTagNames;
			}
		}

		// Token: 0x1700622A RID: 25130
		// (get) Token: 0x06013990 RID: 80272 RVA: 0x003089A0 File Offset: 0x00306BA0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShapeLocks.eleNamespaceIds;
			}
		}

		// Token: 0x1700622B RID: 25131
		// (get) Token: 0x06013991 RID: 80273 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700622C RID: 25132
		// (get) Token: 0x06013992 RID: 80274 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06013993 RID: 80275 RVA: 0x002FA750 File Offset: 0x002F8950
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

		// Token: 0x06013994 RID: 80276 RVA: 0x003089A8 File Offset: 0x00306BA8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "noGrp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noUngrp" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013995 RID: 80277 RVA: 0x00308A57 File Offset: 0x00306C57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShapeLocks>(deep);
		}

		// Token: 0x06013996 RID: 80278 RVA: 0x00308A60 File Offset: 0x00306C60
		// Note: this type is marked as 'beforefieldinit'.
		static GroupShapeLocks()
		{
			byte[] array = new byte[7];
			GroupShapeLocks.attributeNamespaceIds = array;
			GroupShapeLocks.eleTagNames = new string[] { "extLst" };
			GroupShapeLocks.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040086E3 RID: 34531
		private const string tagName = "grpSpLocks";

		// Token: 0x040086E4 RID: 34532
		private const byte tagNsId = 10;

		// Token: 0x040086E5 RID: 34533
		internal const int ElementTypeIdConst = 10170;

		// Token: 0x040086E6 RID: 34534
		private static string[] attributeTagNames = new string[] { "noGrp", "noUngrp", "noSelect", "noRot", "noChangeAspect", "noMove", "noResize" };

		// Token: 0x040086E7 RID: 34535
		private static byte[] attributeNamespaceIds;

		// Token: 0x040086E8 RID: 34536
		private static readonly string[] eleTagNames;

		// Token: 0x040086E9 RID: 34537
		private static readonly byte[] eleNamespaceIds;
	}
}
