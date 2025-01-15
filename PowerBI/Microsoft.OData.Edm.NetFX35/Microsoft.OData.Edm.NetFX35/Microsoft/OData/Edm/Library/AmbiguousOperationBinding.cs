using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200011B RID: 283
	internal class AmbiguousOperationBinding : AmbiguousBinding<IEdmOperation>, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x0000DE2A File Offset: 0x0000C02A
		public AmbiguousOperationBinding(IEdmOperation first, IEdmOperation second)
			: base(first, second)
		{
			this.first = first;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000DE3B File Offset: 0x0000C03B
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000DE3E File Offset: 0x0000C03E
		public string Namespace
		{
			get
			{
				return this.first.Namespace;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000DE4B File Offset: 0x0000C04B
		public IEnumerable<IEdmOperationParameter> Parameters
		{
			get
			{
				return this.first.Parameters;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000DE58 File Offset: 0x0000C058
		public bool IsBound
		{
			get
			{
				return this.first.IsBound;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000DE65 File Offset: 0x0000C065
		public IEdmPathExpression EntitySetPath
		{
			get
			{
				return this.first.EntitySetPath;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000DE72 File Offset: 0x0000C072
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return this.first.SchemaElementKind;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000DE7F File Offset: 0x0000C07F
		public IEdmOperationParameter FindParameter(string name)
		{
			return this.first.FindParameter(name);
		}

		// Token: 0x0400021C RID: 540
		private IEdmOperation first;
	}
}
