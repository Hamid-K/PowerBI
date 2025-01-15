using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FC RID: 9980
	[ChildElementInfo(typeof(FillToRectangle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PathGradientFill : OpenXmlCompositeElement
	{
		// Token: 0x17005E4C RID: 24140
		// (get) Token: 0x060130E1 RID: 78049 RVA: 0x002BFFB6 File Offset: 0x002BE1B6
		public override string LocalName
		{
			get
			{
				return "path";
			}
		}

		// Token: 0x17005E4D RID: 24141
		// (get) Token: 0x060130E2 RID: 78050 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E4E RID: 24142
		// (get) Token: 0x060130E3 RID: 78051 RVA: 0x00303083 File Offset: 0x00301283
		internal override int ElementTypeId
		{
			get
			{
				return 10044;
			}
		}

		// Token: 0x060130E4 RID: 78052 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E4F RID: 24143
		// (get) Token: 0x060130E5 RID: 78053 RVA: 0x0030308A File Offset: 0x0030128A
		internal override string[] AttributeTagNames
		{
			get
			{
				return PathGradientFill.attributeTagNames;
			}
		}

		// Token: 0x17005E50 RID: 24144
		// (get) Token: 0x060130E6 RID: 78054 RVA: 0x00303091 File Offset: 0x00301291
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PathGradientFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E51 RID: 24145
		// (get) Token: 0x060130E7 RID: 78055 RVA: 0x00303098 File Offset: 0x00301298
		// (set) Token: 0x060130E8 RID: 78056 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "path")]
		public EnumValue<PathShadeValues> Path
		{
			get
			{
				return (EnumValue<PathShadeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060130E9 RID: 78057 RVA: 0x00293ECF File Offset: 0x002920CF
		public PathGradientFill()
		{
		}

		// Token: 0x060130EA RID: 78058 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PathGradientFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060130EB RID: 78059 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PathGradientFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060130EC RID: 78060 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PathGradientFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060130ED RID: 78061 RVA: 0x003030A7 File Offset: 0x003012A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "fillToRect" == name)
			{
				return new FillToRectangle();
			}
			return null;
		}

		// Token: 0x17005E52 RID: 24146
		// (get) Token: 0x060130EE RID: 78062 RVA: 0x003030C2 File Offset: 0x003012C2
		internal override string[] ElementTagNames
		{
			get
			{
				return PathGradientFill.eleTagNames;
			}
		}

		// Token: 0x17005E53 RID: 24147
		// (get) Token: 0x060130EF RID: 78063 RVA: 0x003030C9 File Offset: 0x003012C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PathGradientFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005E54 RID: 24148
		// (get) Token: 0x060130F0 RID: 78064 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E55 RID: 24149
		// (get) Token: 0x060130F1 RID: 78065 RVA: 0x003030D0 File Offset: 0x003012D0
		// (set) Token: 0x060130F2 RID: 78066 RVA: 0x003030D9 File Offset: 0x003012D9
		public FillToRectangle FillToRectangle
		{
			get
			{
				return base.GetElement<FillToRectangle>(0);
			}
			set
			{
				base.SetElement<FillToRectangle>(0, value);
			}
		}

		// Token: 0x060130F3 RID: 78067 RVA: 0x003030E3 File Offset: 0x003012E3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "path" == name)
			{
				return new EnumValue<PathShadeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060130F4 RID: 78068 RVA: 0x00303103 File Offset: 0x00301303
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PathGradientFill>(deep);
		}

		// Token: 0x060130F5 RID: 78069 RVA: 0x0030310C File Offset: 0x0030130C
		// Note: this type is marked as 'beforefieldinit'.
		static PathGradientFill()
		{
			byte[] array = new byte[1];
			PathGradientFill.attributeNamespaceIds = array;
			PathGradientFill.eleTagNames = new string[] { "fillToRect" };
			PathGradientFill.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04008474 RID: 33908
		private const string tagName = "path";

		// Token: 0x04008475 RID: 33909
		private const byte tagNsId = 10;

		// Token: 0x04008476 RID: 33910
		internal const int ElementTypeIdConst = 10044;

		// Token: 0x04008477 RID: 33911
		private static string[] attributeTagNames = new string[] { "path" };

		// Token: 0x04008478 RID: 33912
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008479 RID: 33913
		private static readonly string[] eleTagNames;

		// Token: 0x0400847A RID: 33914
		private static readonly byte[] eleNamespaceIds;
	}
}
