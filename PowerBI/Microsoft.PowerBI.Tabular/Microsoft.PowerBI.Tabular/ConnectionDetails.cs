using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F0 RID: 240
	[CompatibilityRequirement("1400")]
	public sealed class ConnectionDetails : CustomJsonProperty<StructuredDataSource>
	{
		// Token: 0x06000FD5 RID: 4053 RVA: 0x00077738 File Offset: 0x00075938
		public ConnectionDetails()
		{
			this.Address = new ConnectionAddress(this);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0007774C File Offset: 0x0007594C
		public ConnectionDetails(string json)
			: base(json)
		{
			this.Address = new ConnectionAddress(this);
			this.ParseJsonImpl(json);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00077768 File Offset: 0x00075968
		internal ConnectionDetails(JToken json)
			: this()
		{
			this.ParseJsonObjectProperties((JObject)json);
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0007777C File Offset: 0x0007597C
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00077784 File Offset: 0x00075984
		public ConnectionAddress Address { get; private set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0007778D File Offset: 0x0007598D
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000777A0 File Offset: 0x000759A0
		public string Protocol
		{
			get
			{
				return this.protocol ?? string.Empty;
			}
			set
			{
				if (string.Compare(this.protocol, value, StringComparison.Ordinal) != 0)
				{
					string text;
					KeyValuePair<CompatibilityMode, Stack<string>>[] array;
					base.OnChanging(out text, out array);
					this.protocol = value;
					base.OnChanged(text, array);
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000777D8 File Offset: 0x000759D8
		public override string ToJson()
		{
			if (string.IsNullOrEmpty(this.protocol) && this.address.IsNullOrEmpty && (this.additionalProperties == null || this.additionalProperties.Count == 0))
			{
				return string.Empty;
			}
			return this.GetJsonImpl().ToString();
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00077825 File Offset: 0x00075A25
		internal override JToken GetJson()
		{
			if (string.IsNullOrEmpty(this.protocol) && this.address.IsNullOrEmpty && (this.additionalProperties == null || this.additionalProperties.Count == 0))
			{
				return null;
			}
			return this.GetJsonImpl();
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0007785E File Offset: 0x00075A5E
		private protected override string PropertyName
		{
			get
			{
				return "ConnectionDetails";
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00077865 File Offset: 0x00075A65
		private protected override CompatibilityRestrictionSet Restrictions
		{
			get
			{
				return CompatibilityRestrictions.StructuredDataSource_ConnectionDetails;
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0007786C File Offset: 0x00075A6C
		internal JToken GetAddressAsJsonObject()
		{
			return this.address.ToJson();
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00077879 File Offset: 0x00075A79
		internal TValue GetAddressNestedPropertyValue<TValue>(string key)
		{
			return this.address.GetValue<TValue>(key);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00077888 File Offset: 0x00075A88
		internal void SetAddressNestedPropertyValue<TValue>(string key, TValue value)
		{
			if (!object.Equals(this.address.GetValue<TValue>(key), value))
			{
				string text;
				KeyValuePair<CompatibilityMode, Stack<string>>[] array;
				base.OnChanging(out text, out array);
				this.address.SetValue<TValue>(key, value);
				base.OnChanged(text, array);
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000778D4 File Offset: 0x00075AD4
		private protected override object GetValueImpl(string key)
		{
			if (key == "protocol")
			{
				return this.protocol;
			}
			if (key == "address")
			{
				return this.Address;
			}
			if (this.additionalProperties == null)
			{
				return null;
			}
			JToken jtoken = this.additionalProperties[key];
			object obj;
			if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue(jtoken, out obj))
			{
				return obj;
			}
			return jtoken.ToString();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00077934 File Offset: 0x00075B34
		private protected override void SetValueImpl(string key, object value)
		{
			if (!(key == "protocol"))
			{
				if (!(key == "address"))
				{
					if (this.additionalProperties == null)
					{
						this.additionalProperties = new JObject();
					}
					JToken jtoken;
					if (CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value, false, out jtoken))
					{
						this.additionalProperties[key] = jtoken;
					}
					return;
				}
				ConnectionAddress connectionAddress = value as ConnectionAddress;
				if (connectionAddress != null)
				{
					this.address = new CustomJsonPropertyHelper(connectionAddress.ToJson());
					return;
				}
				throw new ArgumentException(TomSR.Exception_CustomPropertyValueHasInvalidType("address", typeof(ConnectionAddress).FullName, value.GetType().FullName), "value");
			}
			else
			{
				string text = value as string;
				if (text != null)
				{
					this.protocol = text;
					return;
				}
				throw new ArgumentException(TomSR.Exception_CustomPropertyValueHasInvalidType("protocol", typeof(string).FullName, value.GetType().FullName), "value");
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00077A18 File Offset: 0x00075C18
		private protected override void ParseJsonImpl(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				this.protocol = null;
				this.address = default(CustomJsonPropertyHelper);
				this.additionalProperties = null;
				return;
			}
			JObject jobject = JObject.Parse(json);
			this.ParseJsonObjectProperties(jobject);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00077A56 File Offset: 0x00075C56
		private protected override void ParseJsonImpl(JToken json)
		{
			if (json == null)
			{
				this.protocol = null;
				this.address = default(CustomJsonPropertyHelper);
				this.additionalProperties = null;
				return;
			}
			this.ParseJsonObjectProperties((JObject)json);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00077A82 File Offset: 0x00075C82
		private protected override void MarkAsDirty()
		{
			this.owner.connectionDetails.MarkAsDirty();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00077A94 File Offset: 0x00075C94
		private JObject GetJsonImpl()
		{
			JObject jobject = new JObject();
			if (!string.IsNullOrEmpty(this.protocol))
			{
				jobject.Add("protocol", new JValue(this.protocol));
			}
			if (!this.address.IsNullOrEmpty)
			{
				jobject.Add("address", this.address.ToJson());
			}
			if (this.additionalProperties != null)
			{
				foreach (JProperty jproperty in this.additionalProperties.Properties())
				{
					jobject.Add(jproperty.Name, jproperty.Value.DeepClone());
				}
			}
			return jobject;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00077B4C File Offset: 0x00075D4C
		private void ParseJsonObjectProperties(JObject jObject)
		{
			this.additionalProperties = null;
			foreach (JProperty jproperty in jObject.Properties())
			{
				if (jproperty.Name.Equals("protocol"))
				{
					this.protocol = (string)jproperty.Value;
				}
				else if (jproperty.Name.Equals("address"))
				{
					this.address = new CustomJsonPropertyHelper(jproperty.Value);
				}
				else
				{
					if (this.additionalProperties == null)
					{
						this.additionalProperties = new JObject();
					}
					this.additionalProperties.Add(jproperty.Name, jproperty.Value);
				}
			}
		}

		// Token: 0x04000209 RID: 521
		private string protocol;

		// Token: 0x0400020A RID: 522
		private CustomJsonPropertyHelper address;

		// Token: 0x0400020B RID: 523
		private JObject additionalProperties;

		// Token: 0x020002F5 RID: 757
		internal static class JsonPropertyName
		{
			// Token: 0x04000ADF RID: 2783
			public const string Protocol = "protocol";

			// Token: 0x04000AE0 RID: 2784
			public const string Address = "address";
		}
	}
}
