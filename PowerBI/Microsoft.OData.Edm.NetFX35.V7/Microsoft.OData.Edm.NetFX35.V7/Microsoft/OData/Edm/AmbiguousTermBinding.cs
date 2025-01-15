using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000026 RID: 38
	internal class AmbiguousTermBinding : AmbiguousBinding<IEdmTerm>, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00008F4C File Offset: 0x0000714C
		public AmbiguousTermBinding(IEdmTerm first, IEdmTerm second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008F6B File Offset: 0x0000716B
		public string Namespace
		{
			get
			{
				return this.first.Namespace ?? string.Empty;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008F81 File Offset: 0x00007181
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousTermBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008F95 File Offset: 0x00007195
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008F9D File Offset: 0x0000719D
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008EE2 File Offset: 0x000070E2
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000040 RID: 64
		private readonly IEdmTerm first;

		// Token: 0x04000041 RID: 65
		private readonly Cache<AmbiguousTermBinding, IEdmTypeReference> type = new Cache<AmbiguousTermBinding, IEdmTypeReference>();

		// Token: 0x04000042 RID: 66
		private static readonly Func<AmbiguousTermBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousTermBinding me) => me.ComputeType();

		// Token: 0x04000043 RID: 67
		private readonly string appliesTo;

		// Token: 0x04000044 RID: 68
		private readonly string defaultValue;
	}
}
