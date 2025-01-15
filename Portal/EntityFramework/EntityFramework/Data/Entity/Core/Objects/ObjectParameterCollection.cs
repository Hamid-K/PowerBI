using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000418 RID: 1048
	public class ObjectParameterCollection : ICollection<ObjectParameter>, IEnumerable<ObjectParameter>, IEnumerable
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x000A0E68 File Offset: 0x0009F068
		internal ObjectParameterCollection(ClrPerspective perspective)
		{
			this._perspective = perspective;
			this._parameters = new List<ObjectParameter>();
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000A0E82 File Offset: 0x0009F082
		public int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000A0E8F File Offset: 0x0009F08F
		bool ICollection<ObjectParameter>.IsReadOnly
		{
			get
			{
				return this._locked;
			}
		}

		// Token: 0x170009AE RID: 2478
		public ObjectParameter this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				if (num == -1)
				{
					throw new ArgumentOutOfRangeException("name", Strings.ObjectParameterCollection_ParameterNameNotFound(name));
				}
				return this._parameters[num];
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000A0ED0 File Offset: 0x0009F0D0
		public void Add(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			this.CheckUnlocked();
			if (this.Contains(item))
			{
				throw new ArgumentException(Strings.ObjectParameterCollection_ParameterAlreadyExists(item.Name), "item");
			}
			if (this.Contains(item.Name))
			{
				throw new ArgumentException(Strings.ObjectParameterCollection_DuplicateParameterName(item.Name), "item");
			}
			if (!item.ValidateParameterType(this._perspective))
			{
				throw new ArgumentOutOfRangeException("item", Strings.ObjectParameter_InvalidParameterType(item.ParameterType.FullName));
			}
			this._parameters.Add(item);
			this._cacheKey = null;
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000A0F6E File Offset: 0x0009F16E
		public void Clear()
		{
			this.CheckUnlocked();
			this._parameters.Clear();
			this._cacheKey = null;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000A0F88 File Offset: 0x0009F188
		public bool Contains(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			return this._parameters.Contains(item);
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000A0FA2 File Offset: 0x0009F1A2
		public bool Contains(string name)
		{
			Check.NotNull<string>(name, "name");
			return this.IndexOf(name) != -1;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000A0FBD File Offset: 0x0009F1BD
		public void CopyTo(ObjectParameter[] array, int arrayIndex)
		{
			this._parameters.CopyTo(array, arrayIndex);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000A0FCC File Offset: 0x0009F1CC
		public bool Remove(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			this.CheckUnlocked();
			bool flag = this._parameters.Remove(item);
			if (flag)
			{
				this._cacheKey = null;
			}
			return flag;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000A0FF6 File Offset: 0x0009F1F6
		public virtual IEnumerator<ObjectParameter> GetEnumerator()
		{
			return ((IEnumerable<ObjectParameter>)this._parameters).GetEnumerator();
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000A1003 File Offset: 0x0009F203
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._parameters).GetEnumerator();
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000A1010 File Offset: 0x0009F210
		internal string GetCacheKey()
		{
			if (this._cacheKey == null && this._parameters.Count > 0)
			{
				if (1 == this._parameters.Count)
				{
					ObjectParameter objectParameter = this._parameters[0];
					this._cacheKey = "@@1" + objectParameter.Name + ":" + objectParameter.ParameterType.FullName;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(this._parameters.Count * 20);
					stringBuilder.Append("@@");
					stringBuilder.Append(this._parameters.Count);
					for (int i = 0; i < this._parameters.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(";");
						}
						ObjectParameter objectParameter2 = this._parameters[i];
						stringBuilder.Append(objectParameter2.Name);
						stringBuilder.Append(":");
						stringBuilder.Append(objectParameter2.ParameterType.FullName);
					}
					this._cacheKey = stringBuilder.ToString();
				}
			}
			return this._cacheKey;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000A1121 File Offset: 0x0009F321
		internal void SetReadOnly(bool isReadOnly)
		{
			this._locked = isReadOnly;
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000A112C File Offset: 0x0009F32C
		internal static ObjectParameterCollection DeepCopy(ObjectParameterCollection copyParams)
		{
			if (copyParams == null)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection = new ObjectParameterCollection(copyParams._perspective);
			foreach (ObjectParameter objectParameter in copyParams)
			{
				objectParameterCollection.Add(objectParameter.ShallowCopy());
			}
			return objectParameterCollection;
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000A118C File Offset: 0x0009F38C
		private int IndexOf(string name)
		{
			int num = 0;
			foreach (ObjectParameter objectParameter in this._parameters)
			{
				if (string.Compare(name, objectParameter.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000A11F4 File Offset: 0x0009F3F4
		private void CheckUnlocked()
		{
			if (this._locked)
			{
				throw new InvalidOperationException(Strings.ObjectParameterCollection_ParametersLocked);
			}
		}

		// Token: 0x04001070 RID: 4208
		private bool _locked;

		// Token: 0x04001071 RID: 4209
		private readonly List<ObjectParameter> _parameters;

		// Token: 0x04001072 RID: 4210
		private readonly ClrPerspective _perspective;

		// Token: 0x04001073 RID: 4211
		private string _cacheKey;
	}
}
