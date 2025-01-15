using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet
{
	// Token: 0x02001A6B RID: 6763
	public class JValue : JToken
	{
		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x0600DED4 RID: 57044 RVA: 0x002F52A9 File Offset: 0x002F34A9
		internal new JValue Wrapped
		{
			get
			{
				return (JValue)base.Wrapped;
			}
		}

		// Token: 0x0600DED5 RID: 57045 RVA: 0x002F52B6 File Offset: 0x002F34B6
		internal JValue(JValue toWrap)
			: base(toWrap)
		{
		}

		// Token: 0x0600DED6 RID: 57046 RVA: 0x002F52BF File Offset: 0x002F34BF
		public JValue(string str)
			: this(new JValue(str))
		{
		}

		// Token: 0x0600DED7 RID: 57047 RVA: 0x002F52CD File Offset: 0x002F34CD
		public JValue(int i)
			: this(new JValue((long)i))
		{
		}

		// Token: 0x0600DED8 RID: 57048 RVA: 0x002F52DC File Offset: 0x002F34DC
		public JValue(double d)
			: this(new JValue(d))
		{
		}

		// Token: 0x0600DED9 RID: 57049 RVA: 0x002F52EA File Offset: 0x002F34EA
		public JValue(bool b)
			: this(new JValue(b))
		{
		}

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x0600DEDA RID: 57050 RVA: 0x002F52F8 File Offset: 0x002F34F8
		public object Value
		{
			get
			{
				return this.Wrapped.Value;
			}
		}

		// Token: 0x0600DEDB RID: 57051 RVA: 0x002F5305 File Offset: 0x002F3505
		public string ToString(IFormatProvider formatProvider)
		{
			return this.Wrapped.ToString(formatProvider);
		}
	}
}
