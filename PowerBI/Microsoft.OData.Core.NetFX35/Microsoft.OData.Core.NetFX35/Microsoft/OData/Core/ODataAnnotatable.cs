using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.OData.Core
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public abstract class ODataAnnotatable
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000297C File Offset: 0x00000B7C
		public T GetAnnotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					return this.annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029E6 File Offset: 0x00000BE6
		public void SetAnnotation<T>(T annotation) where T : class
		{
			if (annotation == null)
			{
				this.RemoveAnnotation<T>();
				return;
			}
			this.AddOrReplaceAnnotation<T>(annotation);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A00 File Offset: 0x00000C00
		internal T GetOrCreateAnnotation<T>() where T : class, new()
		{
			T t = this.GetAnnotation<T>();
			if (t == null)
			{
				t = new T();
				this.SetAnnotation<T>(t);
			}
			return t;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A2A File Offset: 0x00000C2A
		internal ICollection<ODataInstanceAnnotation> GetInstanceAnnotations()
		{
			return this.instanceAnnotations;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A32 File Offset: 0x00000C32
		internal void SetInstanceAnnotations(ICollection<ODataInstanceAnnotation> value)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<ODataInstanceAnnotation>>(value, "value");
			this.instanceAnnotations = value;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A46 File Offset: 0x00000C46
		private static bool IsOfType(object instance, Type type)
		{
			return instance.GetType() == type;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A54 File Offset: 0x00000C54
		private void AddOrReplaceAnnotation<T>(T annotation) where T : class
		{
			if (this.annotations == null)
			{
				this.annotations = annotation;
				return;
			}
			object[] array = this.annotations as object[];
			if (array != null)
			{
				int i;
				for (i = 0; i < array.Length; i++)
				{
					object obj = array[i];
					if (obj == null || ODataAnnotatable.IsOfType(obj, typeof(T)))
					{
						array[i] = annotation;
						break;
					}
				}
				if (i == array.Length)
				{
					Array.Resize<object>(ref array, i * 2);
					this.annotations = array;
					array[i] = annotation;
				}
				return;
			}
			if (ODataAnnotatable.IsOfType(this.annotations, typeof(T)))
			{
				this.annotations = annotation;
				return;
			}
			this.annotations = new object[] { this.annotations, annotation };
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B1C File Offset: 0x00000D1C
		private void RemoveAnnotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (ODataAnnotatable.IsOfType(this.annotations, typeof(T)))
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int num = -1;
					int num2 = array.Length;
					while (i < num2)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (ODataAnnotatable.IsOfType(obj, typeof(T)))
						{
							num = i;
							break;
						}
						i++;
					}
					if (num >= 0)
					{
						for (int j = num; j < num2 - 1; j++)
						{
							array[j] = array[j + 1];
						}
						array[num2 - 1] = null;
					}
				}
			}
		}

		// Token: 0x04000018 RID: 24
		[NonSerialized]
		private object annotations;

		// Token: 0x04000019 RID: 25
		[NonSerialized]
		private ICollection<ODataInstanceAnnotation> instanceAnnotations = new Collection<ODataInstanceAnnotation>();
	}
}
