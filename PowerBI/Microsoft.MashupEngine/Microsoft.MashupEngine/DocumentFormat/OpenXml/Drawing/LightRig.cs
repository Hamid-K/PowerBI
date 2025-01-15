using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B3 RID: 10163
	[ChildElementInfo(typeof(Rotation))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LightRig : OpenXmlCompositeElement
	{
		// Token: 0x1700631E RID: 25374
		// (get) Token: 0x06013BA3 RID: 80803 RVA: 0x002EEB5C File Offset: 0x002ECD5C
		public override string LocalName
		{
			get
			{
				return "lightRig";
			}
		}

		// Token: 0x1700631F RID: 25375
		// (get) Token: 0x06013BA4 RID: 80804 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006320 RID: 25376
		// (get) Token: 0x06013BA5 RID: 80805 RVA: 0x0030B1F2 File Offset: 0x003093F2
		internal override int ElementTypeId
		{
			get
			{
				return 10196;
			}
		}

		// Token: 0x06013BA6 RID: 80806 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006321 RID: 25377
		// (get) Token: 0x06013BA7 RID: 80807 RVA: 0x0030B1F9 File Offset: 0x003093F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return LightRig.attributeTagNames;
			}
		}

		// Token: 0x17006322 RID: 25378
		// (get) Token: 0x06013BA8 RID: 80808 RVA: 0x0030B200 File Offset: 0x00309400
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LightRig.attributeNamespaceIds;
			}
		}

		// Token: 0x17006323 RID: 25379
		// (get) Token: 0x06013BA9 RID: 80809 RVA: 0x0030B207 File Offset: 0x00309407
		// (set) Token: 0x06013BAA RID: 80810 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rig")]
		public EnumValue<LightRigValues> Rig
		{
			get
			{
				return (EnumValue<LightRigValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006324 RID: 25380
		// (get) Token: 0x06013BAB RID: 80811 RVA: 0x0030B216 File Offset: 0x00309416
		// (set) Token: 0x06013BAC RID: 80812 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dir")]
		public EnumValue<LightRigDirectionValues> Direction
		{
			get
			{
				return (EnumValue<LightRigDirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013BAD RID: 80813 RVA: 0x00293ECF File Offset: 0x002920CF
		public LightRig()
		{
		}

		// Token: 0x06013BAE RID: 80814 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LightRig(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013BAF RID: 80815 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LightRig(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013BB0 RID: 80816 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LightRig(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013BB1 RID: 80817 RVA: 0x0030B0EF File Offset: 0x003092EF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "rot" == name)
			{
				return new Rotation();
			}
			return null;
		}

		// Token: 0x17006325 RID: 25381
		// (get) Token: 0x06013BB2 RID: 80818 RVA: 0x0030B225 File Offset: 0x00309425
		internal override string[] ElementTagNames
		{
			get
			{
				return LightRig.eleTagNames;
			}
		}

		// Token: 0x17006326 RID: 25382
		// (get) Token: 0x06013BB3 RID: 80819 RVA: 0x0030B22C File Offset: 0x0030942C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LightRig.eleNamespaceIds;
			}
		}

		// Token: 0x17006327 RID: 25383
		// (get) Token: 0x06013BB4 RID: 80820 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006328 RID: 25384
		// (get) Token: 0x06013BB5 RID: 80821 RVA: 0x0030B118 File Offset: 0x00309318
		// (set) Token: 0x06013BB6 RID: 80822 RVA: 0x0030B121 File Offset: 0x00309321
		public Rotation Rotation
		{
			get
			{
				return base.GetElement<Rotation>(0);
			}
			set
			{
				base.SetElement<Rotation>(0, value);
			}
		}

		// Token: 0x06013BB7 RID: 80823 RVA: 0x0030B233 File Offset: 0x00309433
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rig" == name)
			{
				return new EnumValue<LightRigValues>();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<LightRigDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013BB8 RID: 80824 RVA: 0x0030B269 File Offset: 0x00309469
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LightRig>(deep);
		}

		// Token: 0x06013BB9 RID: 80825 RVA: 0x0030B274 File Offset: 0x00309474
		// Note: this type is marked as 'beforefieldinit'.
		static LightRig()
		{
			byte[] array = new byte[2];
			LightRig.attributeNamespaceIds = array;
			LightRig.eleTagNames = new string[] { "rot" };
			LightRig.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x0400877B RID: 34683
		private const string tagName = "lightRig";

		// Token: 0x0400877C RID: 34684
		private const byte tagNsId = 10;

		// Token: 0x0400877D RID: 34685
		internal const int ElementTypeIdConst = 10196;

		// Token: 0x0400877E RID: 34686
		private static string[] attributeTagNames = new string[] { "rig", "dir" };

		// Token: 0x0400877F RID: 34687
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008780 RID: 34688
		private static readonly string[] eleTagNames;

		// Token: 0x04008781 RID: 34689
		private static readonly byte[] eleNamespaceIds;
	}
}
