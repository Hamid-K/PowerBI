using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F3 RID: 243
	public abstract class CustomJsonProperty<TOwner> : ICustomProperty<TOwner, string> where TOwner : MetadataObject
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x00077EE0 File Offset: 0x000760E0
		private protected CustomJsonProperty()
		{
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00077EE8 File Offset: 0x000760E8
		private protected CustomJsonProperty(string json)
		{
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00077EF0 File Offset: 0x000760F0
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.ToJson());
			}
		}

		// Token: 0x170003FB RID: 1019
		public object this[string key]
		{
			get
			{
				return this.GetValueImpl(key);
			}
			set
			{
				if (this.GetValueImpl(key) != value)
				{
					string text;
					KeyValuePair<CompatibilityMode, Stack<string>>[] array;
					this.OnChanging(out text, out array);
					this.SetValueImpl(key, value);
					this.OnChanged(text, array);
				}
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001006 RID: 4102
		private protected abstract string PropertyName { get; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001007 RID: 4103
		private protected abstract CompatibilityRestrictionSet Restrictions { get; }

		// Token: 0x06001008 RID: 4104
		public abstract string ToJson();

		// Token: 0x06001009 RID: 4105 RVA: 0x00077F3C File Offset: 0x0007613C
		public void ParseJson(string json)
		{
			string text;
			KeyValuePair<CompatibilityMode, Stack<string>>[] array;
			this.OnChanging(out text, out array);
			this.ParseJsonImpl(json);
			this.OnChanged(text, array);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00077F62 File Offset: 0x00076162
		public override string ToString()
		{
			return this.ToJson();
		}

		// Token: 0x0600100B RID: 4107
		internal abstract JToken GetJson();

		// Token: 0x0600100C RID: 4108
		private protected abstract object GetValueImpl(string key);

		// Token: 0x0600100D RID: 4109
		private protected abstract void SetValueImpl(string key, object value);

		// Token: 0x0600100E RID: 4110
		private protected abstract void ParseJsonImpl(string json);

		// Token: 0x0600100F RID: 4111
		private protected abstract void ParseJsonImpl(JToken json);

		// Token: 0x06001010 RID: 4112
		private protected abstract void MarkAsDirty();

		// Token: 0x06001011 RID: 4113 RVA: 0x00077F6C File Offset: 0x0007616C
		private protected void OnChanging(out string oldValue, out KeyValuePair<CompatibilityMode, Stack<string>>[] requestingPaths)
		{
			if (this.owner == null)
			{
				oldValue = null;
				requestingPaths = null;
				return;
			}
			oldValue = this.ToJson();
			requestingPaths = this.owner.ValidateCompatibilityRequirement(this.Restrictions, string.Format("[{0}]::[{1}]", this.owner.GetFormattedObjectPath(), this.PropertyName));
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00077FD0 File Offset: 0x000761D0
		private protected void OnChanged(string oldValue, KeyValuePair<CompatibilityMode, Stack<string>>[] requestingPaths)
		{
			if (this.owner != null)
			{
				try
				{
					string text = this.ToJson();
					ObjectChangeTracker.RegisterPropertyChanging(this.owner, this.PropertyName, typeof(string), oldValue, text);
					this.owner.SetCompatibilityRequirement(this.Restrictions, requestingPaths);
					ObjectChangeTracker.RegisterPropertyChanged(this.owner, this.PropertyName, typeof(string), oldValue, text);
					this.MarkAsDirty();
				}
				catch (Exception)
				{
					this.ParseJsonImpl(oldValue);
					throw;
				}
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00078070 File Offset: 0x00076270
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x00078078 File Offset: 0x00076278
		TOwner ICustomProperty<TOwner, string>.Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00078081 File Offset: 0x00076281
		void ICustomProperty<TOwner, string>.Parse(string value)
		{
			this.ParseJsonImpl(value);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0007808A File Offset: 0x0007628A
		string ICustomProperty<TOwner, string>.Convert()
		{
			return this.ToJson();
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00078092 File Offset: 0x00076292
		bool ICustomProperty<TOwner, string>.TryParseJson(JToken json, out string value)
		{
			this.ParseJsonImpl(json.DeepClone());
			value = this.ToJson();
			return true;
		}

		// Token: 0x0400021C RID: 540
		private protected TOwner owner;
	}
}
