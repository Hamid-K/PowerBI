using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F3 RID: 499
	public sealed class EdmFieldRelationship
	{
		// Token: 0x060017CD RID: 6093 RVA: 0x00041979 File Offset: 0x0003FB79
		internal EdmFieldRelationship(EdmField field)
		{
			this._field = field;
			this._relatedTo = Util.EmptyReadOnlyCollection<EdmField>();
			this._pathMemberships = new HashSet<EdmField>();
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0004199E File Offset: 0x0003FB9E
		internal IEnumerable<EdmField> PathMemberships
		{
			get
			{
				return this._pathMemberships;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000419A6 File Offset: 0x0003FBA6
		internal ReadOnlyCollection<EdmField> RelatedToFields
		{
			get
			{
				return this._relatedTo;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000419AE File Offset: 0x0003FBAE
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x000419B6 File Offset: 0x0003FBB6
		internal EdmField RelatedToSource
		{
			get
			{
				return this._relatedToSource;
			}
			private set
			{
				this._relatedToSource = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000419BF File Offset: 0x0003FBBF
		private EdmField LowestRelatedToField
		{
			get
			{
				return this._lowestRelatedField;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000419C7 File Offset: 0x0003FBC7
		internal int DistanceToLowestRelatedField
		{
			get
			{
				return this._distanceToLowestRelatedField;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000419D0 File Offset: 0x0003FBD0
		internal bool HasDefaultMemberOnPath
		{
			get
			{
				if (!this._haveCheckedForDefaultMemberOnPath)
				{
					if (this._relatedTo.Count == 0)
					{
						this._hasDefaultMemberOnPath = this.AllFieldsOnPath.Any((EdmField f) => f.DefaultMember != null);
					}
					else
					{
						this._hasDefaultMemberOnPath = this._pathMemberships.Any((EdmField f) => f.Relationship.HasDefaultMemberOnPath);
					}
					this._haveCheckedForDefaultMemberOnPath = true;
				}
				return this._hasDefaultMemberOnPath;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00041A64 File Offset: 0x0003FC64
		internal ReadOnlyCollection<EdmField> AllFieldsOnPath
		{
			get
			{
				if (this._allFieldsOnPath == null)
				{
					this._allFieldsOnPath = this._pathMemberships.SelectMany((EdmField f) => f.Relationship.GetLowerRelationshipPath()).ToReadOnlyCollection<EdmField>();
				}
				return this._allFieldsOnPath;
			}
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		internal void Initialize(ReadOnlyCollection<EdmField> relatedToFields)
		{
			this._relatedTo = relatedToFields;
			foreach (EdmField edmField in relatedToFields)
			{
				edmField.Relationship.RelatedToSource = this._field;
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00041B0C File Offset: 0x0003FD0C
		internal void CompleteInitialization()
		{
			if (this._relatedTo.Count == 0)
			{
				this.CompleteInitializationCore(this._field, out this._lowestRelatedField);
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00041B30 File Offset: 0x0003FD30
		private int CompleteInitializationCore(EdmField pathLeaf, out EdmField lowestRelationship)
		{
			this._pathMemberships.Add(pathLeaf);
			if (this._relatedToSource == null)
			{
				this._lowestRelatedField = this._field;
			}
			else
			{
				this._distanceToLowestRelatedField = this._relatedToSource.Relationship.CompleteInitializationCore(pathLeaf, out this._lowestRelatedField) + 1;
			}
			lowestRelationship = this._lowestRelatedField;
			return this._distanceToLowestRelatedField;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00041B8D File Offset: 0x0003FD8D
		internal bool IsInRelationship()
		{
			return this._relatedToSource != null || this._relatedTo.Count > 0;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00041BA7 File Offset: 0x0003FDA7
		private static bool AreOnSameAttributeRelationshipPath(EdmField field1, EdmField field2)
		{
			return field1 == field2 || field1.Relationship._pathMemberships.Overlaps(field2.Relationship._pathMemberships);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00041BCC File Offset: 0x0003FDCC
		internal static IEnumerable<Tuple<EdmField, IEnumerable<TSource>>> GroupByPathMembership<TSource>(IEnumerable<TSource> fieldSource, Func<TSource, EdmField> fieldSelector)
		{
			return (from fieldItem in fieldSource
				from pathMembership in fieldSelector(fieldItem).Relationship._pathMemberships
				select new { fieldItem, pathMembership }).GroupBy(o => o.pathMembership, (EdmField field, fieldItems) => Tuple.Create<EdmField, IEnumerable<TSource>>(field, fieldItems.Select(f => f.fieldItem)));
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00041C5A File Offset: 0x0003FE5A
		internal static EdmField GetLowestAttributeRelationshipField(IEnumerable<EdmField> fieldSource)
		{
			return EdmFieldRelationship.GetLowestAttributeRelationshipField<EdmField>(fieldSource, (EdmField f) => f);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00041C84 File Offset: 0x0003FE84
		internal static TSource GetLowestAttributeRelationshipField<TSource>(IEnumerable<TSource> fieldSource, Func<TSource, EdmField> fieldSelector)
		{
			int num = int.MaxValue;
			EdmField fieldAtLowestDepth = null;
			TSource tsource = default(TSource);
			EdmField edmField = null;
			foreach (TSource tsource2 in fieldSource)
			{
				EdmField edmField2 = fieldSelector(tsource2);
				if (edmField2 == null)
				{
					return tsource2;
				}
				if (edmField2.Relationship.LowestRelatedToField == null || (edmField != null && edmField != edmField2.Relationship.LowestRelatedToField))
				{
					return default(TSource);
				}
				edmField = edmField2.Relationship.LowestRelatedToField;
				if (edmField2.Relationship._distanceToLowestRelatedField < num)
				{
					fieldAtLowestDepth = edmField2;
					tsource = tsource2;
					num = edmField2.Relationship._distanceToLowestRelatedField;
				}
			}
			if (fieldSource.Any((TSource fieldSourceItem) => !EdmFieldRelationship.AreOnSameAttributeRelationshipPath(fieldAtLowestDepth, fieldSelector(fieldSourceItem))))
			{
				return default(TSource);
			}
			return tsource;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00041D98 File Offset: 0x0003FF98
		internal IEnumerable<EdmField> GetLowerRelationshipPath()
		{
			yield return this._field;
			for (EdmField currentField = this.RelatedToSource; currentField != null; currentField = currentField.Relationship.RelatedToSource)
			{
				yield return currentField;
			}
			yield break;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00041DA8 File Offset: 0x0003FFA8
		internal void AddAllFieldsOnHigherRelationshipPaths(IList<EdmField> allHigherFields)
		{
			allHigherFields.Add(this._field);
			if (this.RelatedToFields == null)
			{
				return;
			}
			foreach (EdmField edmField in this.RelatedToFields)
			{
				edmField.Relationship.AddAllFieldsOnHigherRelationshipPaths(allHigherFields);
			}
		}

		// Token: 0x04000CC0 RID: 3264
		private readonly EdmField _field;

		// Token: 0x04000CC1 RID: 3265
		private readonly HashSet<EdmField> _pathMemberships;

		// Token: 0x04000CC2 RID: 3266
		private ReadOnlyCollection<EdmField> _relatedTo;

		// Token: 0x04000CC3 RID: 3267
		private EdmField _relatedToSource;

		// Token: 0x04000CC4 RID: 3268
		private EdmField _lowestRelatedField;

		// Token: 0x04000CC5 RID: 3269
		private int _distanceToLowestRelatedField;

		// Token: 0x04000CC6 RID: 3270
		private bool _haveCheckedForDefaultMemberOnPath;

		// Token: 0x04000CC7 RID: 3271
		private bool _hasDefaultMemberOnPath;

		// Token: 0x04000CC8 RID: 3272
		private ReadOnlyCollection<EdmField> _allFieldsOnPath;
	}
}
