using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001DA RID: 474
	public class EdmStructuralProperty : EdmProperty, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: this(declaringType, name, type, null, EdmConcurrencyMode.None)
		{
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00020DB9 File Offset: 0x0001EFB9
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type, string defaultValueString, EdmConcurrencyMode concurrencyMode)
			: base(declaringType, name, type)
		{
			this.defaultValueString = defaultValueString;
			this.concurrencyMode = concurrencyMode;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00020DDC File Offset: 0x0001EFDC
		public EdmConcurrencyMode ConcurrencyMode
		{
			get
			{
				return this.concurrencyMode;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x0400054A RID: 1354
		private readonly string defaultValueString;

		// Token: 0x0400054B RID: 1355
		private readonly EdmConcurrencyMode concurrencyMode;
	}
}
