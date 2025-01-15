using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002713 RID: 10003
	[GeneratedCode("DomGen", "2.0")]
	internal class Blur : OpenXmlLeafElement
	{
		// Token: 0x17005EED RID: 24301
		// (get) Token: 0x0601323B RID: 78395 RVA: 0x003040F6 File Offset: 0x003022F6
		public override string LocalName
		{
			get
			{
				return "blur";
			}
		}

		// Token: 0x17005EEE RID: 24302
		// (get) Token: 0x0601323C RID: 78396 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EEF RID: 24303
		// (get) Token: 0x0601323D RID: 78397 RVA: 0x003040FD File Offset: 0x003022FD
		internal override int ElementTypeId
		{
			get
			{
				return 10065;
			}
		}

		// Token: 0x0601323E RID: 78398 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EF0 RID: 24304
		// (get) Token: 0x0601323F RID: 78399 RVA: 0x00304104 File Offset: 0x00302304
		internal override string[] AttributeTagNames
		{
			get
			{
				return Blur.attributeTagNames;
			}
		}

		// Token: 0x17005EF1 RID: 24305
		// (get) Token: 0x06013240 RID: 78400 RVA: 0x0030410B File Offset: 0x0030230B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Blur.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EF2 RID: 24306
		// (get) Token: 0x06013241 RID: 78401 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013242 RID: 78402 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rad")]
		public Int64Value Radius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005EF3 RID: 24307
		// (get) Token: 0x06013243 RID: 78403 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013244 RID: 78404 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "grow")]
		public BooleanValue Grow
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

		// Token: 0x06013246 RID: 78406 RVA: 0x00304112 File Offset: 0x00302312
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rad" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "grow" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013247 RID: 78407 RVA: 0x00304148 File Offset: 0x00302348
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Blur>(deep);
		}

		// Token: 0x06013248 RID: 78408 RVA: 0x00304154 File Offset: 0x00302354
		// Note: this type is marked as 'beforefieldinit'.
		static Blur()
		{
			byte[] array = new byte[2];
			Blur.attributeNamespaceIds = array;
		}

		// Token: 0x040084E2 RID: 34018
		private const string tagName = "blur";

		// Token: 0x040084E3 RID: 34019
		private const byte tagNsId = 10;

		// Token: 0x040084E4 RID: 34020
		internal const int ElementTypeIdConst = 10065;

		// Token: 0x040084E5 RID: 34021
		private static string[] attributeTagNames = new string[] { "rad", "grow" };

		// Token: 0x040084E6 RID: 34022
		private static byte[] attributeNamespaceIds;
	}
}
