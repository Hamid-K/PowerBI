using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000051 RID: 81
	internal class AmbiguousTermBinding : AmbiguousBinding<IEdmTerm>, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00004D0B File Offset: 0x00002F0B
		public AmbiguousTermBinding(IEdmTerm first, IEdmTerm second)
			: base(first, second)
		{
			this.first = first;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.Namespace, base.Name);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00002732 File Offset: 0x00000932
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00004D3E File Offset: 0x00002F3E
		public string Namespace
		{
			get
			{
				return this.first.Namespace ?? string.Empty;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004D54 File Offset: 0x00002F54
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004D5C File Offset: 0x00002F5C
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, AmbiguousTermBinding.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004D70 File Offset: 0x00002F70
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004D78 File Offset: 0x00002F78
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004C9B File Offset: 0x00002E9B
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x0400009A RID: 154
		private readonly IEdmTerm first;

		// Token: 0x0400009B RID: 155
		private readonly string fullName;

		// Token: 0x0400009C RID: 156
		private readonly Cache<AmbiguousTermBinding, IEdmTypeReference> type = new Cache<AmbiguousTermBinding, IEdmTypeReference>();

		// Token: 0x0400009D RID: 157
		private static readonly Func<AmbiguousTermBinding, IEdmTypeReference> ComputeTypeFunc = (AmbiguousTermBinding me) => me.ComputeType();

		// Token: 0x0400009E RID: 158
		private readonly string appliesTo;

		// Token: 0x0400009F RID: 159
		private readonly string defaultValue;
	}
}
