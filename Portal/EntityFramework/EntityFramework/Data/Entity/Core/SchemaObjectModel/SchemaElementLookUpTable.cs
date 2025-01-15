using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000314 RID: 788
	internal sealed class SchemaElementLookUpTable<T> : IEnumerable<T>, IEnumerable, ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x0006B683 File Offset: 0x00069883
		public int Count
		{
			get
			{
				return this.KeyToType.Count;
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0006B690 File Offset: 0x00069890
		public bool ContainsKey(string key)
		{
			return this.KeyToType.ContainsKey(SchemaElementLookUpTable<T>.KeyFromName(key));
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x0006B6A4 File Offset: 0x000698A4
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

		// Token: 0x170007FC RID: 2044
		public T this[string key]
		{
			get
			{
				return this.KeyToType[SchemaElementLookUpTable<T>.KeyFromName(key)];
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x0006B6E7 File Offset: 0x000698E7
		public T GetElementAt(int index)
		{
			return this.KeyToType[this._keysInDefOrder[index]];
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0006B700 File Offset: 0x00069900
		public IEnumerator<T> GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0006B713 File Offset: 0x00069913
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0006B726 File Offset: 0x00069926
		public IEnumerator<S> GetFilteredEnumerator<S>() where S : T
		{
			return new SchemaElementLookUpTableEnumerator<S, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0006B73C File Offset: 0x0006993C
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

		// Token: 0x060025A9 RID: 9641 RVA: 0x0006B790 File Offset: 0x00069990
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

		// Token: 0x060025AA RID: 9642 RVA: 0x0006B7E3 File Offset: 0x000699E3
		private static string KeyFromElement(T type)
		{
			return SchemaElementLookUpTable<T>.KeyFromName(type.Identity);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0006B7F5 File Offset: 0x000699F5
		private static string KeyFromName(string unnormalizedKey)
		{
			return unnormalizedKey;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x0006B7F8 File Offset: 0x000699F8
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

		// Token: 0x04000D3D RID: 3389
		private Dictionary<string, T> _keyToType;

		// Token: 0x04000D3E RID: 3390
		private readonly List<string> _keysInDefOrder = new List<string>();
	}
}
