using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001E RID: 30
	internal class AmbiguousBinding<TElement> : BadElement where TElement : class, IEdmNamedElement
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00008CA4 File Offset: 0x00006EA4
		public AmbiguousBinding(TElement first, TElement second)
			: base(new EdmError[]
			{
				new EdmError(null, EdmErrorCode.BadAmbiguousElementBinding, Strings.Bad_AmbiguousElementBinding(first.Name))
			})
		{
			this.AddBinding(first);
			this.AddBinding(second);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public IEnumerable<TElement> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008CFC File Offset: 0x00006EFC
		public string Name
		{
			get
			{
				return Enumerable.First<TElement>(this.bindings).Name ?? string.Empty;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008D1C File Offset: 0x00006F1C
		public void AddBinding(TElement binding)
		{
			if (!this.bindings.Contains(binding))
			{
				this.bindings.Add(binding);
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly List<TElement> bindings = new List<TElement>();
	}
}
