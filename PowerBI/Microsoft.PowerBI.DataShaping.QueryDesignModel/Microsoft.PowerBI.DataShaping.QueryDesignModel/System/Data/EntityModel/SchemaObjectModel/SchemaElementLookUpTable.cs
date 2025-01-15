using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200004B RID: 75
	internal sealed class SchemaElementLookUpTable<T> : IEnumerable<T>, IEnumerable, ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001107E File Offset: 0x0000F27E
		public int Count
		{
			get
			{
				return this.KeyToType.Count;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001108B File Offset: 0x0000F28B
		public bool ContainsKey(string key)
		{
			return this.KeyToType.ContainsKey(SchemaElementLookUpTable<T>.KeyFromName(key));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000110A0 File Offset: 0x0000F2A0
		public T LookUpEquivalentKey(string key)
		{
			key = SchemaElementLookUpTable<T>.KeyFromName(key);
			T t;
			if (this.KeyToType.TryGetValue(key, out t))
			{
				return t;
			}
			return default(T);
		}

		// Token: 0x17000317 RID: 791
		public T this[string key]
		{
			get
			{
				return this.KeyToType[SchemaElementLookUpTable<T>.KeyFromName(key)];
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000110E3 File Offset: 0x0000F2E3
		public T GetElementAt(int index)
		{
			return this.KeyToType[this._keysInDefOrder[index]];
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000110FC File Offset: 0x0000F2FC
		public IEnumerator<T> GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001110F File Offset: 0x0000F30F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00011122 File Offset: 0x0000F322
		public IEnumerator<S> GetFilteredEnumerator<S>() where S : T
		{
			return new SchemaElementLookUpTableEnumerator<S, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00011138 File Offset: 0x0000F338
		public AddErrorKind TryAdd(T type)
		{
			if (string.IsNullOrEmpty(type.Identity))
			{
				return AddErrorKind.MissingNameError;
			}
			string text = SchemaElementLookUpTable<T>.KeyFromElement(type);
			T t;
			if (this.KeyToType.TryGetValue(text, out t))
			{
				return AddErrorKind.DuplicateNameError;
			}
			this.KeyToType.Add(text, type);
			this._keysInDefOrder.Add(text);
			return AddErrorKind.Succeeded;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001118C File Offset: 0x0000F38C
		public void Add(T type, bool doNotAddErrorForEmptyName, Func<object, string> duplicateKeyErrorFormat)
		{
			AddErrorKind addErrorKind = this.TryAdd(type);
			if (addErrorKind == AddErrorKind.MissingNameError)
			{
				if (!doNotAddErrorForEmptyName)
				{
					type.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, Strings.MissingName);
				}
				return;
			}
			if (addErrorKind == AddErrorKind.DuplicateNameError)
			{
				type.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, duplicateKeyErrorFormat(type.FQName));
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000111DF File Offset: 0x0000F3DF
		private static string KeyFromElement(T type)
		{
			return SchemaElementLookUpTable<T>.KeyFromName(type.Identity);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000111F1 File Offset: 0x0000F3F1
		private static string KeyFromName(string unnormalizedKey)
		{
			return unnormalizedKey;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x000111F4 File Offset: 0x0000F3F4
		private Dictionary<string, T> KeyToType
		{
			get
			{
				if (this._keyToType == null)
				{
					this._keyToType = new Dictionary<string, T>(StringComparer.Ordinal);
				}
				return this._keyToType;
			}
		}

		// Token: 0x040006B2 RID: 1714
		private Dictionary<string, T> _keyToType;

		// Token: 0x040006B3 RID: 1715
		private List<string> _keysInDefOrder = new List<string>();
	}
}
