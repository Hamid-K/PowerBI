using System;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep
{
	// Token: 0x02000191 RID: 401
	public class AccessStep : JPathStep, IEquatable<AccessStep>
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x0001AE3E File Offset: 0x0001903E
		public AccessStep(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new InvalidOperationException("Key should not be null or empty!");
			}
			this.Key = key;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001AE60 File Offset: 0x00019060
		public string Key { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override JPathStepKind Kind
		{
			get
			{
				return JPathStepKind.Access;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001AE68 File Offset: 0x00019068
		public override double Score
		{
			get
			{
				return -1.0;
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001AE73 File Offset: 0x00019073
		public bool Equals(AccessStep other)
		{
			return other != null && (this == other || string.Equals(this.Key, other.Key));
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001AE94 File Offset: 0x00019094
		internal override string Serialize()
		{
			string text;
			if ((text = this._escapedKey) == null)
			{
				text = (this._escapedKey = XmlConvert.EncodeName(this.Key));
			}
			return text;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001AEBF File Offset: 0x000190BF
		internal static AccessStep Deserialize(string step)
		{
			return new AccessStep(XmlConvert.DecodeName(step));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001AECC File Offset: 0x000190CC
		public override JToken[] Apply(JToken token)
		{
			JObject jobject = token as JObject;
			if (((jobject != null) ? jobject[this.Key] : null) == null)
			{
				return new JToken[0];
			}
			return new JToken[] { jobject[this.Key] };
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001AF10 File Offset: 0x00019110
		public override string ToString()
		{
			return this.Key;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001AF18 File Offset: 0x00019118
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((AccessStep)obj)));
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001AF46 File Offset: 0x00019146
		public override int GetHashCode()
		{
			string key = this.Key;
			if (key == null)
			{
				return 0;
			}
			return key.GetHashCode();
		}

		// Token: 0x04000454 RID: 1108
		private string _escapedKey;
	}
}
