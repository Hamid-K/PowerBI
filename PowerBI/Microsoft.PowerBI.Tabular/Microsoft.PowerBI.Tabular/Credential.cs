using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F2 RID: 242
	[CompatibilityRequirement("1400")]
	public sealed class Credential : CustomJsonProperty<StructuredDataSource>
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x00077C10 File Offset: 0x00075E10
		public Credential()
		{
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00077C18 File Offset: 0x00075E18
		public Credential(string json)
			: base(json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00077C2D File Offset: 0x00075E2D
		internal Credential(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00077C41 File Offset: 0x00075E41
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x00077C53 File Offset: 0x00075E53
		public string AuthenticationKind
		{
			get
			{
				return this.json.GetValue<string>("AuthenticationKind");
			}
			set
			{
				if (this.json.GetValue<string>("AuthenticationKind") != value)
				{
					this.SetValue<string>("AuthenticationKind", value);
				}
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00077C79 File Offset: 0x00075E79
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x00077C8B File Offset: 0x00075E8B
		public string PrivacySetting
		{
			get
			{
				return this.json.GetValue<string>("PrivacySetting");
			}
			set
			{
				if (this.json.GetValue<string>("PrivacySetting") != value)
				{
					this.SetValue<string>("PrivacySetting", value);
				}
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00077CB1 File Offset: 0x00075EB1
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00077CC3 File Offset: 0x00075EC3
		public string Username
		{
			get
			{
				return this.json.GetValue<string>("Username");
			}
			set
			{
				if (this.json.GetValue<string>("Username") != value)
				{
					this.SetValue<string>("Username", value);
				}
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00077CE9 File Offset: 0x00075EE9
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00077CFB File Offset: 0x00075EFB
		public string Password
		{
			get
			{
				return this.json.GetValue<string>("Password");
			}
			set
			{
				if (this.json.GetValue<string>("Password") != value)
				{
					this.SetValue<string>("Password", value);
				}
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00077D21 File Offset: 0x00075F21
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00077D33 File Offset: 0x00075F33
		public bool EncryptConnection
		{
			get
			{
				return this.json.GetValue<bool>("EncryptConnection");
			}
			set
			{
				if (this.json.GetValue<bool>("EncryptConnection") != value)
				{
					this.SetValue<bool>("EncryptConnection", value);
				}
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00077D54 File Offset: 0x00075F54
		public override string ToJson()
		{
			return this.json.ToString();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00077D67 File Offset: 0x00075F67
		internal override JToken GetJson()
		{
			return this.json.ToJson();
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00077D74 File Offset: 0x00075F74
		private protected override string PropertyName
		{
			get
			{
				return "Credential";
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00077D7B File Offset: 0x00075F7B
		private protected override CompatibilityRestrictionSet Restrictions
		{
			get
			{
				return CompatibilityRestrictions.StructuredDataSource_Credential;
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00077D84 File Offset: 0x00075F84
		private protected override object GetValueImpl(string key)
		{
			if (key == "AuthenticationKind" || key == "PrivacySetting" || key == "Username" || key == "Password")
			{
				return this.json.GetValue<string>(key);
			}
			if (!(key == "EncryptConnection"))
			{
				return this.json.GetValue<object>(key);
			}
			return this.json.GetValue<bool>(key);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00077E00 File Offset: 0x00076000
		private protected override void SetValueImpl(string key, object value)
		{
			if (key == "AuthenticationKind" || key == "PrivacySetting" || key == "Username" || key == "Password")
			{
				this.json.SetValue<string>(key, Convert.ToString(value));
				return;
			}
			if (!(key == "EncryptConnection"))
			{
				this.json.SetValue<object>(key, value);
				return;
			}
			this.json.SetValue<bool>(key, Convert.ToBoolean(value));
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00077E83 File Offset: 0x00076083
		private protected override void ParseJsonImpl(string json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00077E91 File Offset: 0x00076091
		private protected override void ParseJsonImpl(JToken json)
		{
			this.json = new CustomJsonPropertyHelper(json);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00077E9F File Offset: 0x0007609F
		private protected override void MarkAsDirty()
		{
			this.owner.credential.MarkAsDirty();
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00077EB4 File Offset: 0x000760B4
		private void SetValue<TValue>(string key, TValue value)
		{
			string text;
			KeyValuePair<CompatibilityMode, Stack<string>>[] array;
			base.OnChanging(out text, out array);
			this.json.SetValue<TValue>(key, value);
			base.OnChanged(text, array);
		}

		// Token: 0x0400021B RID: 539
		private CustomJsonPropertyHelper json;

		// Token: 0x020002F6 RID: 758
		internal static class JsonPropertyName
		{
			// Token: 0x04000AE1 RID: 2785
			public const string AuthenticationKind = "AuthenticationKind";

			// Token: 0x04000AE2 RID: 2786
			public const string PrivacySetting = "PrivacySetting";
		}
	}
}
