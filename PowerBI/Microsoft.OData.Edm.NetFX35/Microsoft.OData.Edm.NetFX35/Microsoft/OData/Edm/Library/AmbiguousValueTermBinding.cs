using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000121 RID: 289
	internal class AmbiguousValueTermBinding : AmbiguousBinding<IEdmValueTerm>, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x0000DFD2 File Offset: 0x0000C1D2
		public AmbiguousValueTermBinding(IEdmValueTerm first, IEdmValueTerm second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000DFEE File Offset: 0x0000C1EE
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000DFF1 File Offset: 0x0000C1F1
		public string Namespace
		{
			get
			{
				return this.first.Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000E007 File Offset: 0x0000C207
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousValueTermBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000E01B File Offset: 0x0000C21B
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000E023 File Offset: 0x0000C223
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000E02B File Offset: 0x0000C22B
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000E02E File Offset: 0x0000C22E
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000226 RID: 550
		private readonly IEdmValueTerm first;

		// Token: 0x04000227 RID: 551
		private readonly Cache<AmbiguousValueTermBinding, IEdmTypeReference> type = new Cache<AmbiguousValueTermBinding, IEdmTypeReference>();

		// Token: 0x04000228 RID: 552
		private static readonly Func<AmbiguousValueTermBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousValueTermBinding me) => me.ComputeType();

		// Token: 0x04000229 RID: 553
		private readonly string appliesTo;

		// Token: 0x0400022A RID: 554
		private readonly string defaultValue;
	}
}
