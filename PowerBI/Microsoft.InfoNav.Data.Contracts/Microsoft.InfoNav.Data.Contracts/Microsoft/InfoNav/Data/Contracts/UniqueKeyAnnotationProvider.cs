using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x0200007E RID: 126
	[ImmutableObject(true)]
	public sealed class UniqueKeyAnnotationProvider : IAnnotationProvider<IUniqueKeyAnnotation, IConceptualEntity>
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00007A09 File Offset: 0x00005C09
		private UniqueKeyAnnotationProvider(IReadOnlyDictionary<string, IUniqueKeyAnnotation> uniqueKeyAnnotationData)
		{
			this._uniqueKeyAnnotationData = uniqueKeyAnnotationData;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00007A18 File Offset: 0x00005C18
		public static IAnnotationProvider<IUniqueKeyAnnotation, IConceptualEntity> Create(IConceptualSchema conceptualSchema)
		{
			return UniqueKeyAnnotationProvider.CreateProvider(conceptualSchema);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00007A20 File Offset: 0x00005C20
		public bool TryGetAnnotation(IConceptualEntity target, out IUniqueKeyAnnotation annotation)
		{
			return this._uniqueKeyAnnotationData.TryGetValue(target.Name, out annotation);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00007A34 File Offset: 0x00005C34
		private static UniqueKeyAnnotationProvider CreateProvider(IConceptualSchema conceptualSchema)
		{
			Dictionary<string, HashSet<IReadOnlyList<IConceptualColumn>>> dictionary = new Dictionary<string, HashSet<IReadOnlyList<IConceptualColumn>>>(ConceptualNameComparer.Instance);
			foreach (IConceptualEntity conceptualEntity in conceptualSchema.Entities)
			{
				foreach (IConceptualNavigationProperty conceptualNavigationProperty in conceptualEntity.NavigationProperties)
				{
					if (conceptualNavigationProperty.TargetEntity != null)
					{
						if (conceptualNavigationProperty.SourceMultiplicity != ConceptualMultiplicity.Many)
						{
							dictionary.Add(conceptualEntity.Name, conceptualNavigationProperty.SourceColumn.ArrayWrap<IConceptualColumn>(), UniqueKeyAnnotationProvider.UniqueKeyComparer.Instance);
						}
						if (conceptualNavigationProperty.TargetColumn != null && conceptualNavigationProperty.TargetMultiplicity != ConceptualMultiplicity.Many)
						{
							dictionary.Add(conceptualNavigationProperty.TargetEntity.Name, conceptualNavigationProperty.TargetColumn.ArrayWrap<IConceptualColumn>(), UniqueKeyAnnotationProvider.UniqueKeyComparer.Instance);
						}
					}
				}
			}
			return new UniqueKeyAnnotationProvider(dictionary.ToDictionary((KeyValuePair<string, HashSet<IReadOnlyList<IConceptualColumn>>> e) => e.Key, (KeyValuePair<string, HashSet<IReadOnlyList<IConceptualColumn>>> e) => new UniqueKeyAnnotationProvider.UniqueKeyAnnotation(e.Value), ConceptualNameComparer.Instance));
		}

		// Token: 0x040001A6 RID: 422
		private readonly IReadOnlyDictionary<string, IUniqueKeyAnnotation> _uniqueKeyAnnotationData;

		// Token: 0x02000300 RID: 768
		[ImmutableObject(true)]
		private sealed class UniqueKeyAnnotation : IUniqueKeyAnnotation
		{
			// Token: 0x06001940 RID: 6464 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
			internal UniqueKeyAnnotation(HashSet<IReadOnlyList<IConceptualColumn>> uniqueKeys)
			{
				this.UniqueKeys = uniqueKeys.OrderBy((IReadOnlyList<IConceptualColumn> e) => e.First<IConceptualColumn>().Ordinal).AsReadOnlyList<IReadOnlyList<IConceptualColumn>>();
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x06001941 RID: 6465 RVA: 0x0002D908 File Offset: 0x0002BB08
			public IReadOnlyList<IReadOnlyList<IConceptualColumn>> UniqueKeys { get; }
		}

		// Token: 0x02000301 RID: 769
		private sealed class UniqueKeyComparer : IEqualityComparer<IReadOnlyList<IConceptualColumn>>
		{
			// Token: 0x06001942 RID: 6466 RVA: 0x0002D910 File Offset: 0x0002BB10
			private UniqueKeyComparer()
			{
			}

			// Token: 0x06001943 RID: 6467 RVA: 0x0002D918 File Offset: 0x0002BB18
			public bool Equals(IReadOnlyList<IConceptualColumn> x, IReadOnlyList<IConceptualColumn> y)
			{
				return x.SequenceEqualReadOnly(y);
			}

			// Token: 0x06001944 RID: 6468 RVA: 0x0002D921 File Offset: 0x0002BB21
			public int GetHashCode(IReadOnlyList<IConceptualColumn> obj)
			{
				return Hashing.CombineHash<IConceptualColumn>(obj, obj.Count, null);
			}

			// Token: 0x04000954 RID: 2388
			internal static readonly UniqueKeyAnnotationProvider.UniqueKeyComparer Instance = new UniqueKeyAnnotationProvider.UniqueKeyComparer();
		}
	}
}
