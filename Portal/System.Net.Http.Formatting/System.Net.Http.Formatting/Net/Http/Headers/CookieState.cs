using System;
using System.Collections.Specialized;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Headers
{
	// Token: 0x02000027 RID: 39
	public class CookieState : ICloneable
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000580C File Offset: 0x00003A0C
		public CookieState(string name)
			: this(name, string.Empty)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000581A File Offset: 0x00003A1A
		public CookieState(string name, string value)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			CookieState.CheckNameFormat(name, "name");
			this._name = name;
			CookieState.CheckValueFormat(value, "value");
			this.Value = value;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005851 File Offset: 0x00003A51
		public CookieState(string name, NameValueCollection values)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			CookieState.CheckNameFormat(name, "name");
			this._name = name;
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this.Values.Add(values);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005890 File Offset: 0x00003A90
		private CookieState(CookieState source)
		{
			this._values = HttpValueCollection.Create();
			base..ctor();
			this._name = source._name;
			if (source._values != null)
			{
				this.Values.Add(source._values);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000058C8 File Offset: 0x00003AC8
		// (set) Token: 0x0600017B RID: 379 RVA: 0x000058D0 File Offset: 0x00003AD0
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				CookieState.CheckNameFormat(value, "value");
				this._name = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000058E4 File Offset: 0x00003AE4
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005907 File Offset: 0x00003B07
		public string Value
		{
			get
			{
				if (this.Values.Count <= 0)
				{
					return string.Empty;
				}
				return this.Values.AllKeys[0];
			}
			set
			{
				CookieState.CheckValueFormat(value, "value");
				if (this.Values.Count > 0)
				{
					this.Values.AllKeys[0] = value;
					return;
				}
				this.Values.Add(value, string.Empty);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005942 File Offset: 0x00003B42
		public NameValueCollection Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x1700007E RID: 126
		public string this[string subName]
		{
			get
			{
				return this.Values[subName];
			}
			set
			{
				this.Values[subName] = value;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005967 File Offset: 0x00003B67
		public override string ToString()
		{
			return this._name + "=" + ((this._values != null) ? this._values.ToString() : string.Empty);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005993 File Offset: 0x00003B93
		public object Clone()
		{
			return new CookieState(this);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000599B File Offset: 0x00003B9B
		private static void CheckNameFormat(string name, string parameterName)
		{
			if (name == null)
			{
				throw Error.ArgumentNull("name");
			}
			if (!FormattingUtilities.ValidateHeaderToken(name))
			{
				throw Error.Argument(parameterName, Resources.CookieInvalidName, new object[0]);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000059C5 File Offset: 0x00003BC5
		private static void CheckValueFormat(string value, string parameterName)
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
		}

		// Token: 0x04000075 RID: 117
		private string _name;

		// Token: 0x04000076 RID: 118
		private NameValueCollection _values;
	}
}
