using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FE1 RID: 4065
	internal abstract class ActiveDirectoryServiceSearchResult
	{
		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x06006AAA RID: 27306 RVA: 0x0016F383 File Offset: 0x0016D583
		public string DistinguishedName
		{
			get
			{
				if (this.distinguishedName == null)
				{
					this.distinguishedName = this.GetSingleValueAttribute<string>("distinguishedName");
				}
				return this.distinguishedName;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x06006AAB RID: 27307
		public abstract IList<string> AttributeNames { get; }

		// Token: 0x06006AAC RID: 27308
		public abstract bool TryGetAttribute(string attributeName, out object[] value);

		// Token: 0x06006AAD RID: 27309 RVA: 0x0016F3A4 File Offset: 0x0016D5A4
		public object[] GetAttribute(string attributeName)
		{
			object[] array;
			if (this.TryGetAttribute(attributeName, out array))
			{
				return array;
			}
			throw ActiveDirectoryExceptions.NewAttributeNotFoundException(attributeName);
		}

		// Token: 0x06006AAE RID: 27310 RVA: 0x0016F3C4 File Offset: 0x0016D5C4
		public T GetSingleValueAttribute<T>(string attributeName)
		{
			T t;
			if (!this.TryGetSingleValueAttribute<T>(attributeName, out t))
			{
				throw ActiveDirectoryExceptions.NewAttributeNotFoundException(attributeName);
			}
			return t;
		}

		// Token: 0x06006AAF RID: 27311 RVA: 0x0016F3E4 File Offset: 0x0016D5E4
		public T[] GetMultiValueAttribute<T>(string attributeName)
		{
			object[] array;
			if (this.TryGetAttribute(attributeName, out array))
			{
				T[] array2 = new T[array.Length];
				Array.Copy(array, array2, array2.Length);
				return array2;
			}
			return new T[0];
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x0016F418 File Offset: 0x0016D618
		public bool TryGetSingleValueAttribute<T>(string attributeName, out T attribute)
		{
			object[] array;
			if (this.TryGetAttribute(attributeName, out array) && array.Length != 0)
			{
				attribute = (T)((object)array[0]);
				return true;
			}
			attribute = default(T);
			return false;
		}

		// Token: 0x04003B4C RID: 15180
		private string distinguishedName;
	}
}
