using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.VariantTypes;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200294A RID: 10570
	[ChildElementInfo(typeof(VTVector))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class VectorVariantType : OpenXmlCompositeElement
	{
		// Token: 0x06014F02 RID: 85762 RVA: 0x00318E4C File Offset: 0x0031704C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (5 == namespaceId && "vector" == name)
			{
				return new VTVector();
			}
			return null;
		}

		// Token: 0x17006B59 RID: 27481
		// (get) Token: 0x06014F03 RID: 85763 RVA: 0x00318E66 File Offset: 0x00317066
		internal override string[] ElementTagNames
		{
			get
			{
				return VectorVariantType.eleTagNames;
			}
		}

		// Token: 0x17006B5A RID: 27482
		// (get) Token: 0x06014F04 RID: 85764 RVA: 0x00318E6D File Offset: 0x0031706D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return VectorVariantType.eleNamespaceIds;
			}
		}

		// Token: 0x17006B5B RID: 27483
		// (get) Token: 0x06014F05 RID: 85765 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B5C RID: 27484
		// (get) Token: 0x06014F06 RID: 85766 RVA: 0x003169D2 File Offset: 0x00314BD2
		// (set) Token: 0x06014F07 RID: 85767 RVA: 0x003169DB File Offset: 0x00314BDB
		public VTVector VTVector
		{
			get
			{
				return base.GetElement<VTVector>(0);
			}
			set
			{
				base.SetElement<VTVector>(0, value);
			}
		}

		// Token: 0x06014F08 RID: 85768 RVA: 0x00293ECF File Offset: 0x002920CF
		protected VectorVariantType()
		{
		}

		// Token: 0x06014F09 RID: 85769 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected VectorVariantType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F0A RID: 85770 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected VectorVariantType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F0B RID: 85771 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected VectorVariantType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040090B8 RID: 37048
		private static readonly string[] eleTagNames = new string[] { "vector" };

		// Token: 0x040090B9 RID: 37049
		private static readonly byte[] eleNamespaceIds = new byte[] { 5 };
	}
}
