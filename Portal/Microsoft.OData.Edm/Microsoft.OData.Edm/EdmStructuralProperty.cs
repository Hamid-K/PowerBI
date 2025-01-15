using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C0 RID: 192
	public class EdmStructuralProperty : EdmProperty, IEdmStructuralProperty, IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000BD9A File Offset: 0x00009F9A
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type)
			: this(declaringType, name, type, null)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "defaultValue might be confused for an IEdmValue.")]
		public EdmStructuralProperty(IEdmStructuredType declaringType, string name, IEdmTypeReference type, string defaultValueString)
			: base(declaringType, name, type)
		{
			this.defaultValueString = defaultValueString;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000BDB9 File Offset: 0x00009FB9
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", Justification = "defaultValue might be confused for an IEdmValue.")]
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmPropertyKind PropertyKind
		{
			get
			{
				return EdmPropertyKind.Structural;
			}
		}

		// Token: 0x04000170 RID: 368
		private readonly string defaultValueString;
	}
}
