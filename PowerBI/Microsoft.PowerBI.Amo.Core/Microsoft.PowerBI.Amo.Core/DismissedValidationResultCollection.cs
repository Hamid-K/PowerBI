using System;
using System.Collections;
using System.Xml;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200007E RID: 126
	public sealed class DismissedValidationResultCollection : ICollection, IEnumerable
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x000239D8 File Offset: 0x00021BD8
		internal DismissedValidationResultCollection(AnnotationCollection parent)
		{
			ArrayList arrayList = new ArrayList();
			Annotation annotation = parent.Find("B77886FF900E4C18A95F79A6BFA488A9");
			if (annotation != null)
			{
				XmlNode value = annotation.Value;
				if (value != null)
				{
					foreach (object obj in value.ChildNodes)
					{
						DismissedValidationResult dismissedValidationResult = DismissedValidationResult.TryToLoadFrom((XmlNode)obj);
						if (dismissedValidationResult != null)
						{
							arrayList.Add(dismissedValidationResult);
						}
					}
				}
			}
			this.parent = parent;
			this.items = arrayList;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00023A74 File Offset: 0x00021C74
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

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00023A8B File Offset: 0x00021C8B
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

		// Token: 0x0600068D RID: 1677 RVA: 0x00023AA2 File Offset: 0x00021CA2
		void ICollection.CopyTo(Array array, int index)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00023AC5 File Offset: 0x00021CC5
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			return this.items.GetEnumerator();
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00023AE6 File Offset: 0x00021CE6
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

		// Token: 0x06000690 RID: 1680 RVA: 0x00023B08 File Offset: 0x00021D08
		public DismissedValidationResult Add(ValidationResult validationResult, string comments)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			if (validationResult == null)
			{
				throw new ArgumentNullException("validationResult");
			}
			if (validationResult.Rule.Type == ValidationRuleType.Error)
			{
				throw new ArgumentException(SR.CannotDismissValidationErrors, "validationResult");
			}
			if (this.Find(validationResult) != null)
			{
				throw new InvalidOperationException(SR.Collections_ItemAlreadyExists);
			}
			DismissedValidationResult dismissedValidationResult = new DismissedValidationResult(validationResult, this.parent, comments);
			this.items.Add(dismissedValidationResult);
			return dismissedValidationResult;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00023B84 File Offset: 0x00021D84
		public void Remove(DismissedValidationResult item)
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

		// Token: 0x06000692 RID: 1682 RVA: 0x00023BC8 File Offset: 0x00021DC8
		private DismissedValidationResult Find(ValidationResult validationResult)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				DismissedValidationResult dismissedValidationResult = (DismissedValidationResult)this.items[i];
				if (validationResult.IsTheSame(dismissedValidationResult.ValidationResult))
				{
					return dismissedValidationResult;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00023C24 File Offset: 0x00021E24
		internal bool Contains(ModelComponent obj, ValidationRule rule, params string[] parameters)
		{
			return this.Find(new ValidationResult(obj, rule, parameters)) != null;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00023C37 File Offset: 0x00021E37
		internal void Dispose()
		{
			this.disposed = true;
			this.items = null;
			this.parent = null;
		}

		// Token: 0x04000438 RID: 1080
		private bool disposed;

		// Token: 0x04000439 RID: 1081
		private AnnotationCollection parent;

		// Token: 0x0400043A RID: 1082
		private ArrayList items;
	}
}
