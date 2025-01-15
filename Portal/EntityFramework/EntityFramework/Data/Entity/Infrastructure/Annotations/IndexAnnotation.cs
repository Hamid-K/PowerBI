using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.Annotations
{
	// Token: 0x020002C5 RID: 709
	public class IndexAnnotation : IMergeableAnnotation
	{
		// Token: 0x0600222B RID: 8747 RVA: 0x00060007 File Offset: 0x0005E207
		public IndexAnnotation(IndexAttribute indexAttribute)
		{
			Check.NotNull<IndexAttribute>(indexAttribute, "indexAttribute");
			this._indexes.Add(indexAttribute);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00060032 File Offset: 0x0005E232
		public IndexAnnotation(IEnumerable<IndexAttribute> indexAttributes)
		{
			Check.NotNull<IEnumerable<IndexAttribute>>(indexAttributes, "indexAttributes");
			IndexAnnotation.MergeLists(this._indexes, indexAttributes, null);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0006005E File Offset: 0x0005E25E
		internal IndexAnnotation(PropertyInfo propertyInfo, IEnumerable<IndexAttribute> indexAttributes)
		{
			Check.NotNull<IEnumerable<IndexAttribute>>(indexAttributes, "indexAttributes");
			IndexAnnotation.MergeLists(this._indexes, indexAttributes, propertyInfo);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0006008C File Offset: 0x0005E28C
		private static void MergeLists(ICollection<IndexAttribute> existingIndexes, IEnumerable<IndexAttribute> newIndexes, PropertyInfo propertyInfo)
		{
			using (IEnumerator<IndexAttribute> enumerator = newIndexes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IndexAttribute index = enumerator.Current;
					if (index == null)
					{
						throw new ArgumentNullException("indexAttribute");
					}
					IndexAttribute indexAttribute = existingIndexes.SingleOrDefault((IndexAttribute i) => i.Name == index.Name);
					if (indexAttribute == null)
					{
						existingIndexes.Add(index);
					}
					else
					{
						CompatibilityResult compatibilityResult = index.IsCompatibleWith(indexAttribute, false);
						if (!compatibilityResult)
						{
							string text = Environment.NewLine + "\t" + compatibilityResult.ErrorMessage;
							throw new InvalidOperationException((propertyInfo == null) ? Strings.ConflictingIndexAttribute(indexAttribute.Name, text) : Strings.ConflictingIndexAttributesOnProperty(propertyInfo.Name, propertyInfo.ReflectedType.Name, indexAttribute.Name, text));
						}
						existingIndexes.Remove(indexAttribute);
						existingIndexes.Add(index.MergeWith(indexAttribute, false));
					}
				}
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public virtual IEnumerable<IndexAttribute> Indexes
		{
			get
			{
				return this._indexes;
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000601A8 File Offset: 0x0005E3A8
		public virtual CompatibilityResult IsCompatibleWith(object other)
		{
			if (this == other || other == null)
			{
				return new CompatibilityResult(true, null);
			}
			IndexAnnotation indexAnnotation = other as IndexAnnotation;
			if (indexAnnotation == null)
			{
				return new CompatibilityResult(false, Strings.IncompatibleTypes(other.GetType().Name, typeof(IndexAnnotation).Name));
			}
			using (IEnumerator<IndexAttribute> enumerator = indexAnnotation._indexes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IndexAttribute newIndex = enumerator.Current;
					IndexAttribute indexAttribute = this._indexes.SingleOrDefault((IndexAttribute i) => i.Name == newIndex.Name);
					if (indexAttribute != null)
					{
						CompatibilityResult compatibilityResult = indexAttribute.IsCompatibleWith(newIndex, false);
						if (!compatibilityResult)
						{
							return compatibilityResult;
						}
					}
				}
			}
			return new CompatibilityResult(true, null);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0006027C File Offset: 0x0005E47C
		public virtual object MergeWith(object other)
		{
			if (this == other || other == null)
			{
				return this;
			}
			IndexAnnotation indexAnnotation = other as IndexAnnotation;
			if (indexAnnotation == null)
			{
				throw new ArgumentException(Strings.IncompatibleTypes(other.GetType().Name, typeof(IndexAnnotation).Name));
			}
			List<IndexAttribute> list = this._indexes.ToList<IndexAttribute>();
			IndexAnnotation.MergeLists(list, indexAnnotation._indexes, null);
			return new IndexAnnotation(list);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000602DE File Offset: 0x0005E4DE
		public override string ToString()
		{
			return "IndexAnnotation: " + new IndexAnnotationSerializer().Serialize("Index", this);
		}

		// Token: 0x04000BE2 RID: 3042
		public const string AnnotationName = "Index";

		// Token: 0x04000BE3 RID: 3043
		private readonly IList<IndexAttribute> _indexes = new List<IndexAttribute>();
	}
}
