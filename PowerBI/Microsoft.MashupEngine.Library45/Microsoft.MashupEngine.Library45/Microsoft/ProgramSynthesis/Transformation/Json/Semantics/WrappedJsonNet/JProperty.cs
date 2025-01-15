using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet
{
	// Token: 0x02001A6C RID: 6764
	public class JProperty : JToken
	{
		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x0600DEDC RID: 57052 RVA: 0x002F5313 File Offset: 0x002F3513
		internal new JProperty Wrapped
		{
			get
			{
				return (JProperty)base.Wrapped;
			}
		}

		// Token: 0x0600DEDD RID: 57053 RVA: 0x002F52B6 File Offset: 0x002F34B6
		internal JProperty(JProperty toWrap)
			: base(toWrap)
		{
		}

		// Token: 0x0600DEDE RID: 57054 RVA: 0x002F5320 File Offset: 0x002F3520
		public JProperty(string key, JToken value)
			: base(new JProperty(key, value.Wrapped))
		{
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x0600DEDF RID: 57055 RVA: 0x002F5334 File Offset: 0x002F3534
		public string Name
		{
			get
			{
				return this.Wrapped.Name;
			}
		}

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x0600DEE0 RID: 57056 RVA: 0x002F5341 File Offset: 0x002F3541
		public JToken Value
		{
			get
			{
				return JToken.From(this.Wrapped.Value);
			}
		}
	}
}
