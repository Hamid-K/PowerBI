using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000071 RID: 113
	[Guid("660FA3D1-97B1-4c33-86AF-8F4381D4317E")]
	public sealed class AnnotationCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00022729 File Offset: 0x00020929
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000158 RID: 344
		public Annotation this[int index]
		{
			get
			{
				return (Annotation)this.items[index];
			}
		}

		// Token: 0x17000159 RID: 345
		public Annotation this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				if (num == -1)
				{
					throw Utils.CreateItemNotFoundException(name, "Name", typeof(Annotation).Name);
				}
				return (Annotation)this.items[num];
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00022794 File Offset: 0x00020994
		public int Add(Annotation item)
		{
			int count = this.Count;
			this.Insert(count, item);
			return count;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000227B4 File Offset: 0x000209B4
		public Annotation Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Annotation annotation = new Annotation(name, value);
			this.Add(annotation);
			return annotation;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000227E0 File Offset: 0x000209E0
		public void Clear()
		{
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				this[i].owningCollection = null;
				i++;
			}
			this.items.Clear();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0002281D File Offset: 0x00020A1D
		public bool Contains(Annotation item)
		{
			return item != null && item.owningCollection == this;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002282D File Offset: 0x00020A2D
		public bool Contains(string name)
		{
			return this.IndexOf(name) != -1;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002283C File Offset: 0x00020A3C
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002284C File Offset: 0x00020A4C
		public void CopyTo(AnnotationCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			if (col == this)
			{
				return;
			}
			col.Clear();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				col.Insert(i, this[i].Clone());
				i++;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000228A0 File Offset: 0x00020AA0
		public Annotation Find(string name)
		{
			int num = this.IndexOf(name);
			if (num != -1)
			{
				return (Annotation)this.items[num];
			}
			return null;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000228CC File Offset: 0x00020ACC
		public int IndexOf(Annotation item)
		{
			if (item != null && item.owningCollection == this)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000228E8 File Offset: 0x00020AE8
		public int IndexOf(string name)
		{
			name = Utils.Trim(name);
			if (name == null)
			{
				return -1;
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				if (string.Compare(Utils.Trim(((Annotation)this.items[i]).Name), name, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00022948 File Offset: 0x00020B48
		public void Insert(int index, Annotation item)
		{
			if (index < 0 || index > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (item.owningCollection == this)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(item.Name, typeof(Annotation).Name, typeof(AnnotationCollection).Name));
			}
			if (item.owningCollection != null)
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInAnotherCollection(typeof(Annotation).Name, typeof(AnnotationCollection).Name));
			}
			if (item.Name == null)
			{
				throw new ArgumentException(SR.ValueIsRequired("Name"), "item");
			}
			if (this.Contains(item.Name))
			{
				throw new InvalidOperationException(SR.Collections_NameIsNotUnique(item.Name, typeof(Annotation).Name));
			}
			this.items.Insert(index, item);
			item.owningCollection = this;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00022A58 File Offset: 0x00020C58
		public Annotation Insert(int index, string name, string value)
		{
			if (index < 0 || index > this.items.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Annotation annotation = new Annotation(name, value);
			this.Insert(index, annotation);
			return annotation;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00022AAC File Offset: 0x00020CAC
		public void Remove(Annotation item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_RemoveNullObject(typeof(AnnotationCollection).Name));
			}
			int num = this.items.IndexOf(item);
			if (num == -1)
			{
				throw new ArgumentException(SR.Collections_RemoveInexistentObject(typeof(AnnotationCollection).Name), "item");
			}
			this.items.RemoveAt(num);
			item.owningCollection = null;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00022B20 File Offset: 0x00020D20
		public void Remove(string name)
		{
			int num = this.IndexOf(name);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00022B41 File Offset: 0x00020D41
		public void RemoveAt(int index)
		{
			((Annotation)this.items[index]).owningCollection = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00022B66 File Offset: 0x00020D66
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00022B69 File Offset: 0x00020D69
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015C RID: 348
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				throw new InvalidOperationException(SR.Collections_CannotSetItems(typeof(AnnotationCollection).Name));
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00022B98 File Offset: 0x00020D98
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			Annotation annotation = value as Annotation;
			if (annotation == null)
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			return this.Add(annotation);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00022BE4 File Offset: 0x00020DE4
		bool IList.Contains(object value)
		{
			Annotation annotation = value as Annotation;
			return annotation != null && this.Contains(annotation);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00022C04 File Offset: 0x00020E04
		int IList.IndexOf(object value)
		{
			Annotation annotation = value as Annotation;
			if (annotation != null)
			{
				return this.IndexOf(annotation);
			}
			return -1;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00022C24 File Offset: 0x00020E24
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			Annotation annotation = value as Annotation;
			if (annotation == null)
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			this.Insert(index, annotation);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00022C74 File Offset: 0x00020E74
		void IList.Remove(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_RemoveNullObject(typeof(AnnotationCollection).Name));
			}
			Annotation annotation = value as Annotation;
			if (annotation == null)
			{
				throw new ArgumentException(SR.Collections_RemoveObjectOfInvalidType(typeof(AnnotationCollection).Name, value.GetType().Name));
			}
			this.Remove(annotation);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00022CD9 File Offset: 0x00020ED9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00022CDC File Offset: 0x00020EDC
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00022CDF File Offset: 0x00020EDF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00022CEC File Offset: 0x00020EEC
		public string GetText(string name)
		{
			Annotation annotation = this.Find(name);
			if (annotation != null)
			{
				return annotation.TextValue;
			}
			return null;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00022D0C File Offset: 0x00020F0C
		public void SetText(string name, string text, bool removeIfNull)
		{
			if (removeIfNull && text == null)
			{
				Annotation annotation = this.Find(name);
				if (annotation != null)
				{
					this.Remove(name);
					return;
				}
			}
			else
			{
				this.SetText(name, text);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00022D3C File Offset: 0x00020F3C
		public void SetText(string name, string text)
		{
			Annotation annotation = this.Find(name);
			if (annotation == null)
			{
				this.Add(name, text);
				return;
			}
			annotation.TextValue = text;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00022D65 File Offset: 0x00020F65
		public void SetText(string name, bool value)
		{
			this.SetText(name, XmlConvert.ToString(value));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00022D74 File Offset: 0x00020F74
		public void SetText(string name, int value)
		{
			this.SetText(name, XmlConvert.ToString(value));
		}

		// Token: 0x04000413 RID: 1043
		private ArrayList items = new ArrayList();
	}
}
