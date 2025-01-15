using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet
{
	// Token: 0x02001A69 RID: 6761
	public class JToken
	{
		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x0600DEC4 RID: 57028 RVA: 0x002F510A File Offset: 0x002F330A
		internal JToken Wrapped { get; }

		// Token: 0x0600DEC5 RID: 57029 RVA: 0x002F5112 File Offset: 0x002F3312
		protected JToken(JToken toWrap)
		{
			if (toWrap == null)
			{
				throw new ArgumentNullException("toWrap");
			}
			this.Wrapped = toWrap;
		}

		// Token: 0x0600DEC6 RID: 57030 RVA: 0x002F5130 File Offset: 0x002F3330
		internal static JToken From(JToken toWrap)
		{
			if (toWrap == null)
			{
				return null;
			}
			JValue jvalue = toWrap as JValue;
			if (jvalue != null)
			{
				return new JValue(jvalue);
			}
			JProperty jproperty = toWrap as JProperty;
			if (jproperty != null)
			{
				return new JProperty(jproperty);
			}
			JObject jobject = toWrap as JObject;
			if (jobject != null)
			{
				return new JObject(jobject);
			}
			JArray jarray = toWrap as JArray;
			if (jarray == null)
			{
				return new JToken(toWrap);
			}
			return new JArray(jarray);
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x0600DEC7 RID: 57031 RVA: 0x002F518E File Offset: 0x002F338E
		public JTokenType Type
		{
			get
			{
				return this.Wrapped.Type;
			}
		}

		// Token: 0x0600DEC8 RID: 57032 RVA: 0x002F519B File Offset: 0x002F339B
		public JToken SelectFirstToken(JPath path)
		{
			return JToken.From(this.Wrapped.SelectFirstToken(path));
		}

		// Token: 0x0600DEC9 RID: 57033 RVA: 0x002F51AE File Offset: 0x002F33AE
		public JProperty SelectFirstProperty(JPath path)
		{
			return new JProperty(this.Wrapped.SelectFirstProperty(path));
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x0600DECA RID: 57034 RVA: 0x002F51C1 File Offset: 0x002F33C1
		public JToken Parent
		{
			get
			{
				return JToken.From(this.Wrapped.Parent);
			}
		}

		// Token: 0x0600DECB RID: 57035 RVA: 0x002F51D3 File Offset: 0x002F33D3
		public IEnumerable<JToken> AfterSelf()
		{
			IEnumerable<JToken> enumerable = this.Wrapped.AfterSelf();
			Func<JToken, JToken> func;
			if ((func = JToken.<>O.<0>__From) == null)
			{
				func = (JToken.<>O.<0>__From = new Func<JToken, JToken>(JToken.From));
			}
			return enumerable.Select(func);
		}

		// Token: 0x0600DECC RID: 57036 RVA: 0x002F5200 File Offset: 0x002F3400
		public IEnumerable<JToken> DescendantsAndSelf()
		{
			IEnumerable<JToken> enumerable = this.Wrapped.DescendantsAndSelf();
			Func<JToken, JToken> func;
			if ((func = JToken.<>O.<0>__From) == null)
			{
				func = (JToken.<>O.<0>__From = new Func<JToken, JToken>(JToken.From));
			}
			return enumerable.Select(func);
		}

		// Token: 0x0600DECD RID: 57037 RVA: 0x002F522D File Offset: 0x002F342D
		public IEnumerable<JPath> GetAllPathsTo(JToken dest)
		{
			return this.Wrapped.GetAllPathsTo(dest.Wrapped);
		}

		// Token: 0x0600DECE RID: 57038 RVA: 0x002F5240 File Offset: 0x002F3440
		public static JPath GetJPath(JToken src, JToken dst)
		{
			return JPath.GetPath(src.Wrapped, dst.Wrapped);
		}

		// Token: 0x0600DECF RID: 57039 RVA: 0x0000BE9E File Offset: 0x0000A09E
		internal static bool DeepEquals(JToken a, JToken b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x0600DED0 RID: 57040 RVA: 0x002F5253 File Offset: 0x002F3453
		private bool Equals(JToken other)
		{
			return JToken.EqualityComparer.Equals(this.Wrapped, (other != null) ? other.Wrapped : null);
		}

		// Token: 0x0600DED1 RID: 57041 RVA: 0x002F5271 File Offset: 0x002F3471
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || this.Equals(obj as JToken));
		}

		// Token: 0x0600DED2 RID: 57042 RVA: 0x002F528A File Offset: 0x002F348A
		public override int GetHashCode()
		{
			return JToken.EqualityComparer.GetHashCode(this.Wrapped);
		}

		// Token: 0x0600DED3 RID: 57043 RVA: 0x002F529C File Offset: 0x002F349C
		public override string ToString()
		{
			return this.Wrapped.ToString();
		}

		// Token: 0x02001A6A RID: 6762
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400549D RID: 21661
			public static Func<JToken, JToken> <0>__From;
		}
	}
}
