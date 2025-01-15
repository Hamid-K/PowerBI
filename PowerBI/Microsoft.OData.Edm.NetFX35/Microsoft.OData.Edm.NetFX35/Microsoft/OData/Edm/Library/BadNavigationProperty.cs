using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000143 RID: 323
	internal class BadNavigationProperty : BadElement, IEdmNavigationProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x0000E6F7 File Offset: 0x0000C8F7
		public BadNavigationProperty(IEdmStructuredType declaringType, string name, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.declaringType = declaringType;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000E722 File Offset: 0x0000C922
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000E72A File Offset: 0x0000C92A
		public IEdmStructuredType DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000E732 File Offset: 0x0000C932
		public IEdmTypeReference Type
		{
			get
			{
				return this.type.GetValue(this, BadNavigationProperty.ComputeTypeFunc, null);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000E746 File Offset: 0x0000C946
		public string DefaultValueString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000E749 File Offset: 0x0000C949
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				return EdmConcurrencyMode.None;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000E74C File Offset: 0x0000C94C
		public EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.None;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0000E74F File Offset: 0x0000C94F
		public IEdmNavigationProperty Partner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000E752 File Offset: 0x0000C952
		public EdmOnDeleteAction OnDelete
		{
			get
			{
				return EdmOnDeleteAction.None;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0000E755 File Offset: 0x0000C955
		public IEdmReferentialConstraint ReferentialConstraint
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000E758 File Offset: 0x0000C958
		public bool ContainsTarget
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000E75C File Offset: 0x0000C95C
		public override string ToString()
		{
			EdmError edmError = Enumerable.FirstOrDefault<EdmError>(base.Errors);
			return edmError.ErrorCode + ":" + this.ToTraceString();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000E790 File Offset: 0x0000C990
		private IEdmTypeReference ComputeType()
		{
			return new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x0400024C RID: 588
		private readonly string name;

		// Token: 0x0400024D RID: 589
		private readonly IEdmStructuredType declaringType;

		// Token: 0x0400024E RID: 590
		private readonly Cache<BadNavigationProperty, IEdmTypeReference> type = new Cache<BadNavigationProperty, IEdmTypeReference>();

		// Token: 0x0400024F RID: 591
		private static readonly Func<BadNavigationProperty, IEdmTypeReference> ComputeTypeFunc = (BadNavigationProperty me) => me.ComputeType();
	}
}
