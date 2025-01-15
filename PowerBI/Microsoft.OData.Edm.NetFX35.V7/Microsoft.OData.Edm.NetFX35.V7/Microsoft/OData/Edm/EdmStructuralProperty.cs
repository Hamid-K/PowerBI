using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000076 RID: 118
	public class EdmStructuralProperty : EdmProperty, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000C295 File Offset: 0x0000A495
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: this(declaringType, name, type, null)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "defaultValue might be confused for an IEdmValue.")]
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type, string defaultValueString)
			: base(declaringType, name, type)
		{
			this.defaultValueString = defaultValueString;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "defaultValue might be confused for an IEdmValue.")]
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00008D76 File Offset: 0x00006F76
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x04000104 RID: 260
		private readonly string defaultValueString;
	}
}
