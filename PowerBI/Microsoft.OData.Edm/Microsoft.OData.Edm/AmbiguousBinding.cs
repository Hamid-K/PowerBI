using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000049 RID: 73
	internal class AmbiguousBinding<TElement> : BadElement where TElement : class, IEdmNamedElement
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00004A34 File Offset: 0x00002C34
		public AmbiguousBinding(TElement first, TElement second)
			: base(new EdmError[]
			{
				new EdmError(null, EdmErrorCode.BadAmbiguousElementBinding, Strings.Bad_AmbiguousElementBinding(first.Name))
			})
		{
			this.AddBinding(first);
			this.AddBinding(second);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004A84 File Offset: 0x00002C84
		public IEnumerable<TElement> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00004A8C File Offset: 0x00002C8C
		public string Name
		{
			get
			{
				return this.bindings.First<TElement>().Name ?? string.Empty;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004AAC File Offset: 0x00002CAC
		public void AddBinding(TElement binding)
		{
			if (!this.bindings.Contains(binding))
			{
				this.bindings.Add(binding);
			}
		}

		// Token: 0x0400008D RID: 141
		private readonly List<TElement> bindings = new List<TElement>();
	}
}
