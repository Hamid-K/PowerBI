using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002959 RID: 10585
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FunctionName))]
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(FunctionProperties))]
	internal class MathFunction : OpenXmlCompositeElement
	{
		// Token: 0x17006BBC RID: 27580
		// (get) Token: 0x06014FE5 RID: 85989 RVA: 0x00319B04 File Offset: 0x00317D04
		public override string LocalName
		{
			get
			{
				return "func";
			}
		}

		// Token: 0x17006BBD RID: 27581
		// (get) Token: 0x06014FE6 RID: 85990 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BBE RID: 27582
		// (get) Token: 0x06014FE7 RID: 85991 RVA: 0x00319B0B File Offset: 0x00317D0B
		internal override int ElementTypeId
		{
			get
			{
				return 10849;
			}
		}

		// Token: 0x06014FE8 RID: 85992 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FE9 RID: 85993 RVA: 0x00293ECF File Offset: 0x002920CF
		public MathFunction()
		{
		}

		// Token: 0x06014FEA RID: 85994 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MathFunction(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FEB RID: 85995 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MathFunction(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FEC RID: 85996 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MathFunction(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014FED RID: 85997 RVA: 0x00319B14 File Offset: 0x00317D14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "funcPr" == name)
			{
				return new FunctionProperties();
			}
			if (21 == namespaceId && "fName" == name)
			{
				return new FunctionName();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BBF RID: 27583
		// (get) Token: 0x06014FEE RID: 85998 RVA: 0x00319B6A File Offset: 0x00317D6A
		internal override string[] ElementTagNames
		{
			get
			{
				return MathFunction.eleTagNames;
			}
		}

		// Token: 0x17006BC0 RID: 27584
		// (get) Token: 0x06014FEF RID: 85999 RVA: 0x00319B71 File Offset: 0x00317D71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MathFunction.eleNamespaceIds;
			}
		}

		// Token: 0x17006BC1 RID: 27585
		// (get) Token: 0x06014FF0 RID: 86000 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BC2 RID: 27586
		// (get) Token: 0x06014FF1 RID: 86001 RVA: 0x00319B78 File Offset: 0x00317D78
		// (set) Token: 0x06014FF2 RID: 86002 RVA: 0x00319B81 File Offset: 0x00317D81
		public FunctionProperties FunctionProperties
		{
			get
			{
				return base.GetElement<FunctionProperties>(0);
			}
			set
			{
				base.SetElement<FunctionProperties>(0, value);
			}
		}

		// Token: 0x17006BC3 RID: 27587
		// (get) Token: 0x06014FF3 RID: 86003 RVA: 0x00319B8B File Offset: 0x00317D8B
		// (set) Token: 0x06014FF4 RID: 86004 RVA: 0x00319B94 File Offset: 0x00317D94
		public FunctionName FunctionName
		{
			get
			{
				return base.GetElement<FunctionName>(1);
			}
			set
			{
				base.SetElement<FunctionName>(1, value);
			}
		}

		// Token: 0x17006BC4 RID: 27588
		// (get) Token: 0x06014FF5 RID: 86005 RVA: 0x00319B9E File Offset: 0x00317D9E
		// (set) Token: 0x06014FF6 RID: 86006 RVA: 0x00319BA7 File Offset: 0x00317DA7
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(2);
			}
			set
			{
				base.SetElement<Base>(2, value);
			}
		}

		// Token: 0x06014FF7 RID: 86007 RVA: 0x00319BB1 File Offset: 0x00317DB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MathFunction>(deep);
		}

		// Token: 0x040090FC RID: 37116
		private const string tagName = "func";

		// Token: 0x040090FD RID: 37117
		private const byte tagNsId = 21;

		// Token: 0x040090FE RID: 37118
		internal const int ElementTypeIdConst = 10849;

		// Token: 0x040090FF RID: 37119
		private static readonly string[] eleTagNames = new string[] { "funcPr", "fName", "e" };

		// Token: 0x04009100 RID: 37120
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
