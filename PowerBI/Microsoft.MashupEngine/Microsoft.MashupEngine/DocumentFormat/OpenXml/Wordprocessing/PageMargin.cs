using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E44 RID: 11844
	[GeneratedCode("DomGen", "2.0")]
	internal class PageMargin : OpenXmlLeafElement
	{
		// Token: 0x170089D9 RID: 35289
		// (get) Token: 0x06019281 RID: 103041 RVA: 0x00346EAC File Offset: 0x003450AC
		public override string LocalName
		{
			get
			{
				return "pgMar";
			}
		}

		// Token: 0x170089DA RID: 35290
		// (get) Token: 0x06019282 RID: 103042 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089DB RID: 35291
		// (get) Token: 0x06019283 RID: 103043 RVA: 0x00346EB3 File Offset: 0x003450B3
		internal override int ElementTypeId
		{
			get
			{
				return 11530;
			}
		}

		// Token: 0x06019284 RID: 103044 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089DC RID: 35292
		// (get) Token: 0x06019285 RID: 103045 RVA: 0x00346EBA File Offset: 0x003450BA
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageMargin.attributeTagNames;
			}
		}

		// Token: 0x170089DD RID: 35293
		// (get) Token: 0x06019286 RID: 103046 RVA: 0x00346EC1 File Offset: 0x003450C1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageMargin.attributeNamespaceIds;
			}
		}

		// Token: 0x170089DE RID: 35294
		// (get) Token: 0x06019287 RID: 103047 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019288 RID: 103048 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "top")]
		public Int32Value Top
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

		// Token: 0x170089DF RID: 35295
		// (get) Token: 0x06019289 RID: 103049 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601928A RID: 103050 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "right")]
		public UInt32Value Right
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

		// Token: 0x170089E0 RID: 35296
		// (get) Token: 0x0601928B RID: 103051 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601928C RID: 103052 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "bottom")]
		public Int32Value Bottom
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170089E1 RID: 35297
		// (get) Token: 0x0601928D RID: 103053 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601928E RID: 103054 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "left")]
		public UInt32Value Left
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170089E2 RID: 35298
		// (get) Token: 0x0601928F RID: 103055 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06019290 RID: 103056 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "header")]
		public UInt32Value Header
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170089E3 RID: 35299
		// (get) Token: 0x06019291 RID: 103057 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06019292 RID: 103058 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "footer")]
		public UInt32Value Footer
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170089E4 RID: 35300
		// (get) Token: 0x06019293 RID: 103059 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06019294 RID: 103060 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "gutter")]
		public UInt32Value Gutter
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06019296 RID: 103062 RVA: 0x00346EC8 File Offset: 0x003450C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "header" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "footer" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "gutter" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019297 RID: 103063 RVA: 0x00346F85 File Offset: 0x00345185
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageMargin>(deep);
		}

		// Token: 0x0400A755 RID: 42837
		private const string tagName = "pgMar";

		// Token: 0x0400A756 RID: 42838
		private const byte tagNsId = 23;

		// Token: 0x0400A757 RID: 42839
		internal const int ElementTypeIdConst = 11530;

		// Token: 0x0400A758 RID: 42840
		private static string[] attributeTagNames = new string[] { "top", "right", "bottom", "left", "header", "footer", "gutter" };

		// Token: 0x0400A759 RID: 42841
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
