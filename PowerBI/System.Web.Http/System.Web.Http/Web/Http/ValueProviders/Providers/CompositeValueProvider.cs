using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x0200003F RID: 63
	public class CompositeValueProvider : Collection<IValueProvider>, IValueProvider, IEnumerableValueProvider
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00005A72 File Offset: 0x00003C72
		public CompositeValueProvider()
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005A7A File Offset: 0x00003C7A
		public CompositeValueProvider(IList<IValueProvider> list)
			: base(list)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005A84 File Offset: 0x00003C84
		public virtual bool ContainsPrefix(string prefix)
		{
			using (IEnumerator<IValueProvider> enumerator = base.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsPrefix(prefix))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public virtual ValueProviderResult GetValue(string key)
		{
			int count = base.Items.Count;
			for (int i = 0; i < count; i++)
			{
				ValueProviderResult value = base.Items[i].GetValue(key);
				if (value != null)
				{
					return value;
				}
			}
			return null;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005B14 File Offset: 0x00003D14
		public virtual IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			foreach (IValueProvider valueProvider in this)
			{
				IDictionary<string, string> keysFromPrefixFromProvider = CompositeValueProvider.GetKeysFromPrefixFromProvider(valueProvider, prefix);
				if (keysFromPrefixFromProvider != null && keysFromPrefixFromProvider.Count > 0)
				{
					return keysFromPrefixFromProvider;
				}
			}
			return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005B78 File Offset: 0x00003D78
		internal static IDictionary<string, string> GetKeysFromPrefixFromProvider(IValueProvider provider, string prefix)
		{
			IEnumerableValueProvider enumerableValueProvider = provider as IEnumerableValueProvider;
			if (enumerableValueProvider == null)
			{
				return null;
			}
			return enumerableValueProvider.GetKeysFromPrefix(prefix);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005B98 File Offset: 0x00003D98
		protected override void InsertItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005BB0 File Offset: 0x00003DB0
		protected override void SetItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw Error.ArgumentNull("item");
			}
			base.SetItem(index, item);
		}
	}
}
