using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F9F RID: 12191
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PictureBulletBase))]
	internal class NumberingPictureBullet : OpenXmlCompositeElement
	{
		// Token: 0x17009281 RID: 37505
		// (get) Token: 0x0601A54B RID: 107851 RVA: 0x00360BD0 File Offset: 0x0035EDD0
		public override string LocalName
		{
			get
			{
				return "numPicBullet";
			}
		}

		// Token: 0x17009282 RID: 37506
		// (get) Token: 0x0601A54C RID: 107852 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009283 RID: 37507
		// (get) Token: 0x0601A54D RID: 107853 RVA: 0x00360BD7 File Offset: 0x0035EDD7
		internal override int ElementTypeId
		{
			get
			{
				return 11884;
			}
		}

		// Token: 0x0601A54E RID: 107854 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009284 RID: 37508
		// (get) Token: 0x0601A54F RID: 107855 RVA: 0x00360BDE File Offset: 0x0035EDDE
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingPictureBullet.attributeTagNames;
			}
		}

		// Token: 0x17009285 RID: 37509
		// (get) Token: 0x0601A550 RID: 107856 RVA: 0x00360BE5 File Offset: 0x0035EDE5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingPictureBullet.attributeNamespaceIds;
			}
		}

		// Token: 0x17009286 RID: 37510
		// (get) Token: 0x0601A551 RID: 107857 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A552 RID: 107858 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "numPicBulletId")]
		public Int32Value NumberingPictureBulletId
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A553 RID: 107859 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberingPictureBullet()
		{
		}

		// Token: 0x0601A554 RID: 107860 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberingPictureBullet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A555 RID: 107861 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberingPictureBullet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A556 RID: 107862 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberingPictureBullet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A557 RID: 107863 RVA: 0x00360BEC File Offset: 0x0035EDEC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pict" == name)
			{
				return new PictureBulletBase();
			}
			return null;
		}

		// Token: 0x17009287 RID: 37511
		// (get) Token: 0x0601A558 RID: 107864 RVA: 0x00360C07 File Offset: 0x0035EE07
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberingPictureBullet.eleTagNames;
			}
		}

		// Token: 0x17009288 RID: 37512
		// (get) Token: 0x0601A559 RID: 107865 RVA: 0x00360C0E File Offset: 0x0035EE0E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberingPictureBullet.eleNamespaceIds;
			}
		}

		// Token: 0x17009289 RID: 37513
		// (get) Token: 0x0601A55A RID: 107866 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700928A RID: 37514
		// (get) Token: 0x0601A55B RID: 107867 RVA: 0x00360C15 File Offset: 0x0035EE15
		// (set) Token: 0x0601A55C RID: 107868 RVA: 0x00360C1E File Offset: 0x0035EE1E
		public PictureBulletBase PictureBulletBase
		{
			get
			{
				return base.GetElement<PictureBulletBase>(0);
			}
			set
			{
				base.SetElement<PictureBulletBase>(0, value);
			}
		}

		// Token: 0x0601A55D RID: 107869 RVA: 0x00360C28 File Offset: 0x0035EE28
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "numPicBulletId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A55E RID: 107870 RVA: 0x00360C4A File Offset: 0x0035EE4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingPictureBullet>(deep);
		}

		// Token: 0x0400ACA8 RID: 44200
		private const string tagName = "numPicBullet";

		// Token: 0x0400ACA9 RID: 44201
		private const byte tagNsId = 23;

		// Token: 0x0400ACAA RID: 44202
		internal const int ElementTypeIdConst = 11884;

		// Token: 0x0400ACAB RID: 44203
		private static string[] attributeTagNames = new string[] { "numPicBulletId" };

		// Token: 0x0400ACAC RID: 44204
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400ACAD RID: 44205
		private static readonly string[] eleTagNames = new string[] { "pict" };

		// Token: 0x0400ACAE RID: 44206
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
