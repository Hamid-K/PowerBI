using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295A RID: 10586
	[ChildElementInfo(typeof(GroupCharProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Base))]
	internal class GroupChar : OpenXmlCompositeElement
	{
		// Token: 0x17006BC5 RID: 27589
		// (get) Token: 0x06014FF9 RID: 86009 RVA: 0x00319C04 File Offset: 0x00317E04
		public override string LocalName
		{
			get
			{
				return "groupChr";
			}
		}

		// Token: 0x17006BC6 RID: 27590
		// (get) Token: 0x06014FFA RID: 86010 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BC7 RID: 27591
		// (get) Token: 0x06014FFB RID: 86011 RVA: 0x00319C0B File Offset: 0x00317E0B
		internal override int ElementTypeId
		{
			get
			{
				return 10850;
			}
		}

		// Token: 0x06014FFC RID: 86012 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FFD RID: 86013 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupChar()
		{
		}

		// Token: 0x06014FFE RID: 86014 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupChar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FFF RID: 86015 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupChar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015000 RID: 86016 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupChar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015001 RID: 86017 RVA: 0x00319C12 File Offset: 0x00317E12
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "groupChrPr" == name)
			{
				return new GroupCharProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BC8 RID: 27592
		// (get) Token: 0x06015002 RID: 86018 RVA: 0x00319C45 File Offset: 0x00317E45
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupChar.eleTagNames;
			}
		}

		// Token: 0x17006BC9 RID: 27593
		// (get) Token: 0x06015003 RID: 86019 RVA: 0x00319C4C File Offset: 0x00317E4C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupChar.eleNamespaceIds;
			}
		}

		// Token: 0x17006BCA RID: 27594
		// (get) Token: 0x06015004 RID: 86020 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BCB RID: 27595
		// (get) Token: 0x06015005 RID: 86021 RVA: 0x00319C53 File Offset: 0x00317E53
		// (set) Token: 0x06015006 RID: 86022 RVA: 0x00319C5C File Offset: 0x00317E5C
		public GroupCharProperties GroupCharProperties
		{
			get
			{
				return base.GetElement<GroupCharProperties>(0);
			}
			set
			{
				base.SetElement<GroupCharProperties>(0, value);
			}
		}

		// Token: 0x17006BCC RID: 27596
		// (get) Token: 0x06015007 RID: 86023 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06015008 RID: 86024 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x06015009 RID: 86025 RVA: 0x00319C66 File Offset: 0x00317E66
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupChar>(deep);
		}

		// Token: 0x04009101 RID: 37121
		private const string tagName = "groupChr";

		// Token: 0x04009102 RID: 37122
		private const byte tagNsId = 21;

		// Token: 0x04009103 RID: 37123
		internal const int ElementTypeIdConst = 10850;

		// Token: 0x04009104 RID: 37124
		private static readonly string[] eleTagNames = new string[] { "groupChrPr", "e" };

		// Token: 0x04009105 RID: 37125
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
