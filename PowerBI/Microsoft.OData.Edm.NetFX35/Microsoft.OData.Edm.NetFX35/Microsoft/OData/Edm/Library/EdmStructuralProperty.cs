using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000206 RID: 518
	public class EdmStructuralProperty : EdmProperty, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x00022854 File Offset: 0x00020A54
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: this(declaringType, name, type, null, EdmConcurrencyMode.None)
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00022861 File Offset: 0x00020A61
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "defaultValue might be confused for an IEdmValue.")]
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type, string defaultValueString, EdmConcurrencyMode concurrencyMode)
			: base(declaringType, name, type)
		{
			this.defaultValueString = defaultValueString;
			this.concurrencyMode = concurrencyMode;
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0002287C File Offset: 0x00020A7C
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "defaultValue might be confused for an IEdmValue.")]
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00022884 File Offset: 0x00020A84
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				return this.concurrencyMode;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0002288C File Offset: 0x00020A8C
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x04000591 RID: 1425
		private readonly string defaultValueString;

		// Token: 0x04000592 RID: 1426
		private readonly EdmConcurrencyMode concurrencyMode;
	}
}
