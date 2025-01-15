using System;
using System.Collections;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000080 RID: 128
	public sealed class DismissedValidationRuleCollection : ICollection, IEnumerable
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x00023DA4 File Offset: 0x00021FA4
		internal DismissedValidationRuleCollection(AnnotationCollection parent)
		{
			ArrayList arrayList = new ArrayList();
			Annotation annotation = parent.Find("CA1792B06E6B48BDAD71EFE2A95A0F02");
			if (annotation != null)
			{
				XmlNode value = annotation.Value;
				if (value != null)
				{
					foreach (object obj in value.ChildNodes)
					{
						DismissedValidationRule dismissedValidationRule = DismissedValidationRule.TryToLoadFrom((XmlNode)obj);
						if (dismissedValidationRule != null)
						{
							arrayList.Add(dismissedValidationRule);
						}
					}
				}
			}
			this.parent = parent;
			this.items = arrayList;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00023E40 File Offset: 0x00022040
		bool ICollection.IsSynchronized
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				return false;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00023E57 File Offset: 0x00022057
		object ICollection.SyncRoot
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				return this;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00023E6E File Offset: 0x0002206E
		void ICollection.CopyTo(Array array, int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			this.items.CopyTo(array, index);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00023E91 File Offset: 0x00022091
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			return this.items.GetEnumerator();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00023EB2 File Offset: 0x000220B2
		public int Count
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				return this.items.Count;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00023ED4 File Offset: 0x000220D4
		public DismissedValidationRule Add(ValidationRule validationRule, string comments)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			if (validationRule == null)
			{
				throw new ArgumentNullException("validationRule");
			}
			if (validationRule.Type == ValidationRuleType.Error)
			{
				throw new ArgumentException(SR.CannotDismissValidationErrors, "validationRule");
			}
			if (this.Find(validationRule) != null)
			{
				throw new InvalidOperationException(SR.Collections_ItemAlreadyExists);
			}
			DismissedValidationRule dismissedValidationRule = new DismissedValidationRule(validationRule, this.parent, comments);
			this.items.Add(dismissedValidationRule);
			return dismissedValidationRule;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00023F4C File Offset: 0x0002214C
		public void Remove(DismissedValidationRule item)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			int num = this.items.IndexOf(item);
			if (num >= 0)
			{
				item.RemoveFromAnnotations();
				this.items.RemoveAt(num);
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00023F90 File Offset: 0x00022190
		private DismissedValidationRule Find(ValidationRule validationRule)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				DismissedValidationRule dismissedValidationRule = (DismissedValidationRule)this.items[i];
				if (dismissedValidationRule.ValidationRule == validationRule)
				{
					return dismissedValidationRule;
				}
				i++;
			}
			return null;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00023FE7 File Offset: 0x000221E7
		internal bool Contains(ValidationRule rule)
		{
			return this.Find(rule) != null;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00023FF3 File Offset: 0x000221F3
		internal void Dispose()
		{
			this.disposed = true;
			this.items = null;
			this.parent = null;
		}

		// Token: 0x0400043F RID: 1087
		private bool disposed;

		// Token: 0x04000440 RID: 1088
		private AnnotationCollection parent;

		// Token: 0x04000441 RID: 1089
		private ArrayList items;
	}
}
