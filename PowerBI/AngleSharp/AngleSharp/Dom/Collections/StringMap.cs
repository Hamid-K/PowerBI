using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000404 RID: 1028
	internal sealed class StringMap : IStringMap, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x00058093 File Offset: 0x00056293
		internal StringMap(string prefix, Element parent)
		{
			this._prefix = prefix;
			this._parent = parent;
		}

		// Token: 0x17000A4D RID: 2637
		public string this[string name]
		{
			get
			{
				return this._parent.GetOwnAttribute(this._prefix + StringMap.Check(name));
			}
			set
			{
				this._parent.SetOwnAttribute(this._prefix + StringMap.Check(name), value, false);
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000580E7 File Offset: 0x000562E7
		public void Remove(string name)
		{
			if (this.Contains(name))
			{
				this[name] = null;
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000580FA File Offset: 0x000562FA
		public bool Contains(string name)
		{
			return this._parent.HasOwnAttribute(this._prefix + StringMap.Check(name));
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00058118 File Offset: 0x00056318
		private static string Check(string name)
		{
			if (name.StartsWith(TagNames.Xml, StringComparison.OrdinalIgnoreCase))
			{
				throw new DomException(DomError.Syntax);
			}
			if (name.IndexOf(';') >= 0)
			{
				throw new DomException(DomError.Syntax);
			}
			for (int i = 0; i < name.Length; i++)
			{
				if (name[i].IsUppercaseAscii())
				{
					throw new DomException(DomError.Syntax);
				}
			}
			return name;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00058176 File Offset: 0x00056376
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			foreach (IAttr attr in this._parent.Attributes)
			{
				if (attr.NamespaceUri == null && attr.Name.StartsWith(this._prefix, StringComparison.OrdinalIgnoreCase))
				{
					string text = attr.Name.Remove(0, this._prefix.Length);
					string value = attr.Value;
					yield return new KeyValuePair<string, string>(text, value);
				}
			}
			IEnumerator<IAttr> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00058185 File Offset: 0x00056385
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000D21 RID: 3361
		private readonly string _prefix;

		// Token: 0x04000D22 RID: 3362
		private readonly Element _parent;
	}
}
