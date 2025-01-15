using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000E0 RID: 224
	internal class AmbiguousFunctionImportBinding : AmbiguousBinding<IEdmFunctionImport>, IEdmFunctionImport, IEdmFunctionBase, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0000C04D File Offset: 0x0000A24D
		public AmbiguousFunctionImportBinding(IEdmFunctionImport first, IEdmFunctionImport second)
			: base(first, second)
		{
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000C057 File Offset: 0x0000A257
		public IEdmTypeReference ReturnType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000C05C File Offset: 0x0000A25C
		public IEdmEntityContainer Container
		{
			get
			{
				IEdmFunctionImport edmFunctionImport = Enumerable.FirstOrDefault<IEdmFunctionImport>(base.Bindings);
				if (edmFunctionImport == null)
				{
					return null;
				}
				return edmFunctionImport.Container;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000C080 File Offset: 0x0000A280
		public IEnumerable<IEdmFunctionParameter> Parameters
		{
			get
			{
				IEdmFunctionImport edmFunctionImport = Enumerable.FirstOrDefault<IEdmFunctionImport>(base.Bindings);
				if (edmFunctionImport == null)
				{
					return Enumerable.Empty<IEdmFunctionParameter>();
				}
				return edmFunctionImport.Parameters;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000C0AB File Offset: 0x0000A2AB
		public IEdmExpression EntitySet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000C0AE File Offset: 0x0000A2AE
		public bool IsSideEffecting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000C0B1 File Offset: 0x0000A2B1
		public bool IsComposable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		public bool IsBindable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public IEdmFunctionParameter FindParameter(string name)
		{
			IEdmFunctionImport edmFunctionImport = Enumerable.FirstOrDefault<IEdmFunctionImport>(base.Bindings);
			if (edmFunctionImport == null)
			{
				return null;
			}
			return edmFunctionImport.FindParameter(name);
		}
	}
}
