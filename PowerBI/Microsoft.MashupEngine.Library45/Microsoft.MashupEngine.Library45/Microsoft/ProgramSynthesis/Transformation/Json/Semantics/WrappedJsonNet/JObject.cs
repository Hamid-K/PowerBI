using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet
{
	// Token: 0x02001A6D RID: 6765
	public class JObject : JToken
	{
		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x0600DEE1 RID: 57057 RVA: 0x002F5353 File Offset: 0x002F3553
		internal new JObject Wrapped
		{
			get
			{
				return (JObject)base.Wrapped;
			}
		}

		// Token: 0x0600DEE2 RID: 57058 RVA: 0x002F52B6 File Offset: 0x002F34B6
		internal JObject(JObject toWrap)
			: base(toWrap)
		{
		}

		// Token: 0x0600DEE3 RID: 57059 RVA: 0x002F5360 File Offset: 0x002F3560
		public JObject()
			: this(new JObject())
		{
		}

		// Token: 0x0600DEE4 RID: 57060 RVA: 0x002F536D File Offset: 0x002F356D
		public JObject(JToken token)
			: this(new JObject(token.Wrapped))
		{
		}

		// Token: 0x0600DEE5 RID: 57061 RVA: 0x002F5380 File Offset: 0x002F3580
		public JObject(JObject obj)
			: this(new JObject(obj.Wrapped))
		{
		}

		// Token: 0x0600DEE6 RID: 57062 RVA: 0x002F5393 File Offset: 0x002F3593
		public JObject(IEnumerable<JToken> tokens)
			: this(new JObject(tokens.Select((JToken t) => t.Wrapped)))
		{
		}

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x0600DEE7 RID: 57063 RVA: 0x002F53C5 File Offset: 0x002F35C5
		public JToken First
		{
			get
			{
				return JToken.From(this.Wrapped.First);
			}
		}

		// Token: 0x0600DEE8 RID: 57064 RVA: 0x002F53D7 File Offset: 0x002F35D7
		public void Add(JToken token)
		{
			this.Wrapped.Add(token.Wrapped);
		}

		// Token: 0x0600DEE9 RID: 57065 RVA: 0x002F53EA File Offset: 0x002F35EA
		public void AddFirst(JToken token)
		{
			this.Wrapped.AddFirst(token.Wrapped);
		}

		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x0600DEEA RID: 57066 RVA: 0x002F53FD File Offset: 0x002F35FD
		public int Count
		{
			get
			{
				return this.Wrapped.Count;
			}
		}

		// Token: 0x17002534 RID: 9524
		public JToken this[string key]
		{
			get
			{
				return JToken.From(this.Wrapped[key]);
			}
			set
			{
				this.Wrapped[key] = value.Wrapped;
			}
		}

		// Token: 0x0600DEED RID: 57069 RVA: 0x002F5431 File Offset: 0x002F3631
		public IEnumerable<JToken> Children()
		{
			IEnumerable<JToken> enumerable = this.Wrapped.Children();
			Func<JToken, JToken> func;
			if ((func = JObject.<>O.<0>__From) == null)
			{
				func = (JObject.<>O.<0>__From = new Func<JToken, JToken>(JToken.From));
			}
			return enumerable.Select(func);
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x0600DEEE RID: 57070 RVA: 0x002F5463 File Offset: 0x002F3663
		internal IEnumerable<KeyValuePair<string, JToken>> PropertiesEnumerable
		{
			get
			{
				return this.Wrapped.Select((KeyValuePair<string, JToken> kvp) => new KeyValuePair<string, JToken>(kvp.Key, JToken.From(kvp.Value)));
			}
		}

		// Token: 0x02001A6E RID: 6766
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400549E RID: 21662
			public static Func<JToken, JToken> <0>__From;
		}
	}
}
