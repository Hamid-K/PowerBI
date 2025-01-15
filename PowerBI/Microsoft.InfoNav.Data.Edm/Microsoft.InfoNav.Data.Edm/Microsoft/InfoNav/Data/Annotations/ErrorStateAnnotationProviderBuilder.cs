using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Annotations
{
	// Token: 0x0200002D RID: 45
	internal sealed class ErrorStateAnnotationProviderBuilder
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00009080 File Offset: 0x00007280
		internal ErrorStateAnnotationProviderBuilder()
		{
			this._errorProperties = new HashSet<IConceptualProperty>();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009093 File Offset: 0x00007293
		internal void RegisterErrorProperty(IConceptualProperty property)
		{
			this._errorProperties.Add(property);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000090A4 File Offset: 0x000072A4
		internal void BuildAndRegisterAnnotationProvider(IConceptualSchema schema)
		{
			ErrorStateAnnotationProviderBuilder.AnnotationProvider annotationProvider = new ErrorStateAnnotationProviderBuilder.AnnotationProvider(this._errorProperties.AsReadOnlySet(null));
			schema.RegisterAnnotationProvider<ErrorStateAnnotation, IConceptualProperty>(annotationProvider);
		}

		// Token: 0x04000196 RID: 406
		private readonly HashSet<IConceptualProperty> _errorProperties;

		// Token: 0x0200004E RID: 78
		private sealed class AnnotationProvider : IAnnotationProvider<ErrorStateAnnotation, IConceptualProperty>
		{
			// Token: 0x0600022F RID: 559 RVA: 0x00009A02 File Offset: 0x00007C02
			internal AnnotationProvider(ReadOnlySet<IConceptualProperty> errorProperties)
			{
				this._errorProperties = errorProperties;
			}

			// Token: 0x06000230 RID: 560 RVA: 0x00009A11 File Offset: 0x00007C11
			public bool TryGetAnnotation(IConceptualProperty target, out ErrorStateAnnotation annotation)
			{
				annotation = (this._errorProperties.Contains(target) ? ErrorStateAnnotation.True : ErrorStateAnnotation.False);
				return true;
			}

			// Token: 0x040001ED RID: 493
			private readonly ReadOnlySet<IConceptualProperty> _errorProperties;
		}
	}
}
