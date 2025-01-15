using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000F9 RID: 249
	internal class AmbiguousBinding<TElement> : BadElement where TElement : class, IEdmNamedElement
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D08C File Offset: 0x0000B28C
		public AmbiguousBinding(TElement first, TElement second)
			: base(new EdmError[]
			{
				new EdmError(null, EdmErrorCode.BadAmbiguousElementBinding, Strings.Bad_AmbiguousElementBinding(first.Name))
			})
		{
			this.AddBinding(first);
			this.AddBinding(second);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public IEnumerable<TElement> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public string Name
		{
			get
			{
				TElement telement = Enumerable.First<TElement>(this.bindings);
				return telement.Name ?? string.Empty;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000D117 File Offset: 0x0000B317
		public void AddBinding(TElement binding)
		{
			if (!this.bindings.Contains(binding))
			{
				this.bindings.Add(binding);
			}
		}

		// Token: 0x040001DB RID: 475
		private readonly List<TElement> bindings = new List<TElement>();
	}
}
