using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD3 RID: 12243
	[ChildElementInfo(typeof(Gallery))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Name))]
	internal class Category : OpenXmlCompositeElement
	{
		// Token: 0x1700944C RID: 37964
		// (get) Token: 0x0601A925 RID: 108837 RVA: 0x002DCAFB File Offset: 0x002DACFB
		public override string LocalName
		{
			get
			{
				return "category";
			}
		}

		// Token: 0x1700944D RID: 37965
		// (get) Token: 0x0601A926 RID: 108838 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700944E RID: 37966
		// (get) Token: 0x0601A927 RID: 108839 RVA: 0x003645F1 File Offset: 0x003627F1
		internal override int ElementTypeId
		{
			get
			{
				return 11950;
			}
		}

		// Token: 0x0601A928 RID: 108840 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A929 RID: 108841 RVA: 0x00293ECF File Offset: 0x002920CF
		public Category()
		{
		}

		// Token: 0x0601A92A RID: 108842 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Category(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A92B RID: 108843 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Category(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A92C RID: 108844 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Category(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A92D RID: 108845 RVA: 0x003645F8 File Offset: 0x003627F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new Name();
			}
			if (23 == namespaceId && "gallery" == name)
			{
				return new Gallery();
			}
			return null;
		}

		// Token: 0x1700944F RID: 37967
		// (get) Token: 0x0601A92E RID: 108846 RVA: 0x0036462B File Offset: 0x0036282B
		internal override string[] ElementTagNames
		{
			get
			{
				return Category.eleTagNames;
			}
		}

		// Token: 0x17009450 RID: 37968
		// (get) Token: 0x0601A92F RID: 108847 RVA: 0x00364632 File Offset: 0x00362832
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Category.eleNamespaceIds;
			}
		}

		// Token: 0x17009451 RID: 37969
		// (get) Token: 0x0601A930 RID: 108848 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009452 RID: 37970
		// (get) Token: 0x0601A931 RID: 108849 RVA: 0x00364639 File Offset: 0x00362839
		// (set) Token: 0x0601A932 RID: 108850 RVA: 0x00364642 File Offset: 0x00362842
		public Name Name
		{
			get
			{
				return base.GetElement<Name>(0);
			}
			set
			{
				base.SetElement<Name>(0, value);
			}
		}

		// Token: 0x17009453 RID: 37971
		// (get) Token: 0x0601A933 RID: 108851 RVA: 0x0036464C File Offset: 0x0036284C
		// (set) Token: 0x0601A934 RID: 108852 RVA: 0x00364655 File Offset: 0x00362855
		public Gallery Gallery
		{
			get
			{
				return base.GetElement<Gallery>(1);
			}
			set
			{
				base.SetElement<Gallery>(1, value);
			}
		}

		// Token: 0x0601A935 RID: 108853 RVA: 0x0036465F File Offset: 0x0036285F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Category>(deep);
		}

		// Token: 0x0400AD90 RID: 44432
		private const string tagName = "category";

		// Token: 0x0400AD91 RID: 44433
		private const byte tagNsId = 23;

		// Token: 0x0400AD92 RID: 44434
		internal const int ElementTypeIdConst = 11950;

		// Token: 0x0400AD93 RID: 44435
		private static readonly string[] eleTagNames = new string[] { "name", "gallery" };

		// Token: 0x0400AD94 RID: 44436
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
