using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000DF RID: 223
	internal class AmbiguousFunctionBinding : AmbiguousBinding<IEdmFunction>, IEdmFunction, IEdmFunctionBase, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		public AmbiguousFunctionBinding(IEdmFunction first, IEdmFunction second)
			: base(first, second)
		{
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000BFCF File Offset: 0x0000A1CF
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public string Namespace
		{
			get
			{
				IEdmFunction edmFunction = Enumerable.FirstOrDefault<IEdmFunction>(base.Bindings);
				if (edmFunction == null)
				{
					return string.Empty;
				}
				return edmFunction.Namespace;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public string DefiningExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000C000 File Offset: 0x0000A200
		public IEnumerable<IEdmFunctionParameter> Parameters
		{
			get
			{
				IEdmFunction edmFunction = Enumerable.FirstOrDefault<IEdmFunction>(base.Bindings);
				if (edmFunction == null)
				{
					return Enumerable.Empty<IEdmFunctionParameter>();
				}
				return edmFunction.Parameters;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000C028 File Offset: 0x0000A228
		public IEdmFunctionParameter FindParameter(string name)
		{
			IEdmFunction edmFunction = Enumerable.FirstOrDefault<IEdmFunction>(base.Bindings);
			if (edmFunction == null)
			{
				return null;
			}
			return edmFunction.FindParameter(name);
		}
	}
}
