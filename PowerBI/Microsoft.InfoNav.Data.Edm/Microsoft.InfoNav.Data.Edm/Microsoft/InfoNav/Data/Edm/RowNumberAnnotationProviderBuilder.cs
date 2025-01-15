using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000C RID: 12
	internal sealed class RowNumberAnnotationProviderBuilder
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000029FE File Offset: 0x00000BFE
		internal RowNumberAnnotationProviderBuilder()
		{
			this._entityNameToRowNumberName = new Dictionary<string, string>(ConceptualNameComparer.Instance);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A16 File Offset: 0x00000C16
		internal void Register(string entityName, string rowNumberName)
		{
			this._entityNameToRowNumberName.Add(entityName, rowNumberName);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A25 File Offset: 0x00000C25
		internal IAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity> Build()
		{
			return new RowNumberAnnotationProviderBuilder.AnnotationProvider(this._entityNameToRowNumberName);
		}

		// Token: 0x04000039 RID: 57
		private readonly Dictionary<string, string> _entityNameToRowNumberName;

		// Token: 0x02000032 RID: 50
		private sealed class AnnotationProvider : IAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity>
		{
			// Token: 0x060001AD RID: 429 RVA: 0x000091DE File Offset: 0x000073DE
			internal AnnotationProvider(IReadOnlyDictionary<string, string> entityNameToRowNumberName)
			{
				this._entityNameToRowNumberName = entityNameToRowNumberName;
			}

			// Token: 0x060001AE RID: 430 RVA: 0x000091F0 File Offset: 0x000073F0
			public bool TryGetAnnotation(IConceptualEntity target, out EntityRowNumberAnnotation annotation)
			{
				string text;
				if (!this._entityNameToRowNumberName.TryGetValue(target.Name, out text))
				{
					annotation = default(EntityRowNumberAnnotation);
					return false;
				}
				annotation = new EntityRowNumberAnnotation(text);
				return true;
			}

			// Token: 0x0400019F RID: 415
			private readonly IReadOnlyDictionary<string, string> _entityNameToRowNumberName;
		}
	}
}
