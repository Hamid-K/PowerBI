using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet
{
	// Token: 0x02001A70 RID: 6768
	public class JArray : JToken, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x0600DEF3 RID: 57075 RVA: 0x002F54BD File Offset: 0x002F36BD
		internal new JArray Wrapped
		{
			get
			{
				return (JArray)base.Wrapped;
			}
		}

		// Token: 0x0600DEF4 RID: 57076 RVA: 0x002F52B6 File Offset: 0x002F34B6
		internal JArray(JArray toWrap)
			: base(toWrap)
		{
		}

		// Token: 0x0600DEF5 RID: 57077 RVA: 0x002F54CA File Offset: 0x002F36CA
		public JArray()
			: this(new JArray())
		{
		}

		// Token: 0x0600DEF6 RID: 57078 RVA: 0x002F54D7 File Offset: 0x002F36D7
		public JArray(IEnumerable<JToken> elements)
			: this(new JArray(elements.Select((JToken t) => t.Wrapped)))
		{
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x0600DEF7 RID: 57079 RVA: 0x002F5509 File Offset: 0x002F3709
		public int Count
		{
			get
			{
				return this.Wrapped.Count;
			}
		}

		// Token: 0x17002538 RID: 9528
		public JToken this[int i]
		{
			get
			{
				return JToken.From(this.Wrapped[i]);
			}
			set
			{
				this.Wrapped[i] = value.Wrapped;
			}
		}

		// Token: 0x0600DEFA RID: 57082 RVA: 0x002F553D File Offset: 0x002F373D
		public IEnumerator<JToken> GetEnumerator()
		{
			IEnumerable<JToken> wrapped = this.Wrapped;
			Func<JToken, JToken> func;
			if ((func = JArray.<>O.<0>__From) == null)
			{
				func = (JArray.<>O.<0>__From = new Func<JToken, JToken>(JToken.From));
			}
			return wrapped.Select(func).GetEnumerator();
		}

		// Token: 0x0600DEFB RID: 57083 RVA: 0x002F556A File Offset: 0x002F376A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x02001A71 RID: 6769
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040054A2 RID: 21666
			public static Func<JToken, JToken> <0>__From;
		}
	}
}
