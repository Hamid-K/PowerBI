using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000141 RID: 321
	internal class BadProperty : BadElement, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0000E629 File Offset: 0x0000C829
		public BadProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000E654 File Offset: 0x0000C854
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000E65C File Offset: 0x0000C85C
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000E664 File Offset: 0x0000C864
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000E678 File Offset: 0x0000C878
		public string DefaultValueString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000E67B File Offset: 0x0000C87B
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				return EdmConcurrencyMode.None;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000E67E File Offset: 0x0000C87E
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000E684 File Offset: 0x0000C884
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x04000247 RID: 583
		private readonly string name;

		// Token: 0x04000248 RID: 584
		private readonly IEdmStructuredType declaringType;

		// Token: 0x04000249 RID: 585
		private readonly Cache<BadProperty, IEdmTypeReference> type = new Cache<BadProperty, IEdmTypeReference>();

		// Token: 0x0400024A RID: 586
		private static readonly Func<BadProperty, IEdmTypeReference> ComputeTypeFunc = (BadProperty me) => me.ComputeType();
	}
}
