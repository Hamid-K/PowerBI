using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class ModelStateDictionary : IDictionary<string, ModelState>, ICollection<KeyValuePair<string, ModelState>>, IEnumerable<KeyValuePair<string, ModelState>>, IEnumerable
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00007EEA File Offset: 0x000060EA
		public ModelStateDictionary()
		{
			this._innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00007F02 File Offset: 0x00006102
		public ModelStateDictionary(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw Error.ArgumentNull("dictionary");
			}
			this._innerDictionary = new Dictionary<string, ModelState>(dictionary, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007F29 File Offset: 0x00006129
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00007F36 File Offset: 0x00006136
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00007F43 File Offset: 0x00006143
		public bool IsValid
		{
			get
			{
				return this.Values.All((ModelState modelState) => modelState.Errors.Count == 0);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00007F6F File Offset: 0x0000616F
		public ICollection<string> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00007F7C File Offset: 0x0000617C
		public ICollection<ModelState> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x1700008D RID: 141
		public ModelState this[string key]
		{
			get
			{
				ModelState modelState;
				this._innerDictionary.TryGetValue(key, out modelState);
				return modelState;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00007FB8 File Offset: 0x000061B8
		public void Add(KeyValuePair<string, ModelState> item)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Add(item);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00007FC6 File Offset: 0x000061C6
		public void Add(string key, ModelState value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007FD5 File Offset: 0x000061D5
		public void AddModelError(string key, Exception exception)
		{
			this.GetModelStateForKey(key).Errors.Add(exception);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007FE9 File Offset: 0x000061E9
		public void AddModelError(string key, string errorMessage)
		{
			this.GetModelStateForKey(key).Errors.Add(errorMessage);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00007FFD File Offset: 0x000061FD
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000800A File Offset: 0x0000620A
		public bool Contains(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008018 File Offset: 0x00006218
		public bool ContainsKey(string key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008026 File Offset: 0x00006226
		public void CopyTo(KeyValuePair<string, ModelState>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008035 File Offset: 0x00006235
		public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008048 File Offset: 0x00006248
		private ModelState GetModelStateForKey(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			ModelState modelState;
			if (!this.TryGetValue(key, out modelState))
			{
				modelState = new ModelState();
				this[key] = modelState;
			}
			return modelState;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008080 File Offset: 0x00006280
		public bool IsValidField(string key)
		{
			if (key == null)
			{
				throw Error.ArgumentNull("key");
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in this.FindKeysWithPrefix(key))
			{
				if (keyValuePair.Value.Errors.Count != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000080F0 File Offset: 0x000062F0
		public void Merge(ModelStateDictionary dictionary)
		{
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<string, ModelState> keyValuePair in dictionary)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000814C File Offset: 0x0000634C
		public bool Remove(KeyValuePair<string, ModelState> item)
		{
			return ((ICollection<KeyValuePair<string, ModelState>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000815A File Offset: 0x0000635A
		public bool Remove(string key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008168 File Offset: 0x00006368
		public void SetModelValue(string key, ValueProviderResult value)
		{
			this.GetModelStateForKey(key).Value = value;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008177 File Offset: 0x00006377
		public bool TryGetValue(string key, out ModelState value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008186 File Offset: 0x00006386
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x0400009F RID: 159
		private readonly Dictionary<string, ModelState> _innerDictionary;
	}
}
