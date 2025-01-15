using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000CD RID: 205
	[Guid("287DFF2E-4C90-4907-B235-A7C2CBE0706B")]
	public sealed class ValidationErrorCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00029C3E File Offset: 0x00027E3E
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000238 RID: 568
		public ValidationError this[int index]
		{
			get
			{
				return (ValidationError)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				this.items[index] = value;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00029C80 File Offset: 0x00027E80
		public int Add(ValidationError item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			return this.items.Add(item);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00029CC0 File Offset: 0x00027EC0
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				ValidationError validationError = (ValidationError)obj;
				this.Add(validationError);
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00029D24 File Offset: 0x00027F24
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00029D31 File Offset: 0x00027F31
		public bool Contains(ValidationError item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00029D44 File Offset: 0x00027F44
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00029D53 File Offset: 0x00027F53
		public int IndexOf(ValidationError item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00029D66 File Offset: 0x00027F66
		public void Insert(int index, ValidationError item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			this.items.Insert(index, item);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00029DA6 File Offset: 0x00027FA6
		public void Remove(ValidationError item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00029DB8 File Offset: 0x00027FB8
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00029DC6 File Offset: 0x00027FC6
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00029DC9 File Offset: 0x00027FC9
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023B RID: 571
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				if (!(value is ValidationError))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00029E2C File Offset: 0x0002802C
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ValidationError))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00029E99 File Offset: 0x00028099
		bool IList.Contains(object value)
		{
			return value != null && value is ValidationError && this.items.Contains(value);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00029EB4 File Offset: 0x000280B4
		int IList.IndexOf(object value)
		{
			if (value != null && value is ValidationError)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00029ED0 File Offset: 0x000280D0
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ValidationError))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00029F3E File Offset: 0x0002813E
		void IList.Remove(object value)
		{
			if (value == null || !(value is ValidationError))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00029F58 File Offset: 0x00028158
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00029F5B File Offset: 0x0002815B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00029F5E File Offset: 0x0002815E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00029F6B File Offset: 0x0002816B
		public int Add(IModelComponent source, string error)
		{
			return this.items.Add(new ValidationError(source, error, ErrorPriority.High));
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00029F80 File Offset: 0x00028180
		public int Add(IModelComponent source, string error, ErrorPriority priority)
		{
			return this.items.Add(new ValidationError(source, error, priority));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00029F95 File Offset: 0x00028195
		public int Add(IModelComponent source, string error, int errorCode)
		{
			return this.items.Add(new ValidationError(source, error, errorCode));
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00029FAA File Offset: 0x000281AA
		public int Add(IModelComponent source, string error, ErrorPriority priority, int errorCode)
		{
			return this.items.Add(new ValidationError(source, error, priority, errorCode));
		}

		// Token: 0x04000708 RID: 1800
		private ArrayList items = new ArrayList();
	}
}
