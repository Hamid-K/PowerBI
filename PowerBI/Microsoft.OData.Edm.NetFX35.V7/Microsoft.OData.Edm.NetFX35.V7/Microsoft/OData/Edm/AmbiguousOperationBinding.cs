using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000022 RID: 34
	internal class AmbiguousOperationBinding : AmbiguousBinding<IEdmOperation>, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00008E0F File Offset: 0x0000700F
		public AmbiguousOperationBinding(IEdmOperation first, IEdmOperation second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008E20 File Offset: 0x00007020
		public string Namespace
		{
			get
			{
				return this.first.Namespace;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008E2D File Offset: 0x0000702D
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.first.Parameters;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008E3A File Offset: 0x0000703A
		public bool IsBound
		{
			get
			{
				return this.first.IsBound;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008E47 File Offset: 0x00007047
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.first.EntitySetPath;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008E54 File Offset: 0x00007054
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return this.first.SchemaElementKind;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008E61 File Offset: 0x00007061
		public IEdmOperationParameter FindParameter(string name)
		{
			return this.first.FindParameter(name);
		}

		// Token: 0x0400003B RID: 59
		private IEdmOperation first;
	}
}
