using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000056 RID: 86
	public abstract class TargetWithContext : TargetWithLayout, IIncludeContext
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000140B6 File Offset: 0x000122B6
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x000140BE File Offset: 0x000122BE
		public sealed override Layout Layout
		{
			get
			{
				return this._contextLayout;
			}
			set
			{
				if (this._contextLayout != null)
				{
					this._contextLayout.TargetLayout = value;
					return;
				}
				this._contextLayout = new TargetWithContext.TargetWithContextLayout(this, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000140E2 File Offset: 0x000122E2
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000140EA File Offset: 0x000122EA
		bool IIncludeContext.IncludeAllProperties
		{
			get
			{
				return this.IncludeEventProperties;
			}
			set
			{
				this.IncludeEventProperties = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000140F3 File Offset: 0x000122F3
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00014100 File Offset: 0x00012300
		public bool IncludeEventProperties
		{
			get
			{
				return this._contextLayout.IncludeAllProperties;
			}
			set
			{
				this._contextLayout.IncludeAllProperties = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001410E File Offset: 0x0001230E
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001411B File Offset: 0x0001231B
		public bool IncludeMdc
		{
			get
			{
				return this._contextLayout.IncludeMdc;
			}
			set
			{
				this._contextLayout.IncludeMdc = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00014129 File Offset: 0x00012329
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00014136 File Offset: 0x00012336
		public bool IncludeNdc
		{
			get
			{
				return this._contextLayout.IncludeNdc;
			}
			set
			{
				this._contextLayout.IncludeNdc = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00014144 File Offset: 0x00012344
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00014151 File Offset: 0x00012351
		public bool IncludeMdlc
		{
			get
			{
				return this._contextLayout.IncludeMdlc;
			}
			set
			{
				this._contextLayout.IncludeMdlc = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001415F File Offset: 0x0001235F
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001416C File Offset: 0x0001236C
		public bool IncludeNdlc
		{
			get
			{
				return this._contextLayout.IncludeNdlc;
			}
			set
			{
				this._contextLayout.IncludeNdlc = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0001417A File Offset: 0x0001237A
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00014182 File Offset: 0x00012382
		public bool IncludeGdc { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0001418B File Offset: 0x0001238B
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00014198 File Offset: 0x00012398
		public bool IncludeCallSite
		{
			get
			{
				return this._contextLayout.IncludeCallSite;
			}
			set
			{
				this._contextLayout.IncludeCallSite = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x000141A6 File Offset: 0x000123A6
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x000141B3 File Offset: 0x000123B3
		public bool IncludeCallSiteStackTrace
		{
			get
			{
				return this._contextLayout.IncludeCallSiteStackTrace;
			}
			set
			{
				this._contextLayout.IncludeCallSiteStackTrace = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000141C1 File Offset: 0x000123C1
		[ArrayParameter(typeof(TargetPropertyWithContext), "contextproperty")]
		public virtual IList<TargetPropertyWithContext> ContextProperties { get; } = new List<TargetPropertyWithContext>();

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x000141CC File Offset: 0x000123CC
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x000141F6 File Offset: 0x000123F6
		private IPropertyTypeConverter PropertyTypeConverter
		{
			get
			{
				IPropertyTypeConverter propertyTypeConverter;
				if ((propertyTypeConverter = this._propertyTypeConverter) == null)
				{
					propertyTypeConverter = (this._propertyTypeConverter = ConfigurationItemFactory.Default.PropertyTypeConverter);
				}
				return propertyTypeConverter;
			}
			set
			{
				this._propertyTypeConverter = value;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000141FF File Offset: 0x000123FF
		protected TargetWithContext()
		{
			this._contextLayout = this._contextLayout ?? new TargetWithContext.TargetWithContextLayout(this, base.Layout);
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00014235 File Offset: 0x00012435
		protected override void CloseTarget()
		{
			this.PropertyTypeConverter = null;
			base.CloseTarget();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00014244 File Offset: 0x00012444
		protected bool ShouldIncludeProperties(LogEventInfo logEvent)
		{
			return this.IncludeGdc || this.IncludeMdc || this.IncludeMdlc || (this.IncludeEventProperties && logEvent != null && logEvent.HasProperties);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00014275 File Offset: 0x00012475
		protected IDictionary<string, object> GetContextProperties(LogEventInfo logEvent)
		{
			return this.GetContextProperties(logEvent, null);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00014280 File Offset: 0x00012480
		protected IDictionary<string, object> GetContextProperties(LogEventInfo logEvent, IDictionary<string, object> combinedProperties)
		{
			IList<TargetPropertyWithContext> contextProperties = this.ContextProperties;
			if (contextProperties != null && contextProperties.Count > 0)
			{
				combinedProperties = this.CaptureContextProperties(logEvent, combinedProperties);
			}
			if (this.IncludeMdlc && !this.CombineProperties(logEvent, this._contextLayout.MdlcLayout, ref combinedProperties))
			{
				combinedProperties = this.CaptureContextMdlc(logEvent, combinedProperties);
			}
			if (this.IncludeMdc && !this.CombineProperties(logEvent, this._contextLayout.MdcLayout, ref combinedProperties))
			{
				combinedProperties = this.CaptureContextMdc(logEvent, combinedProperties);
			}
			if (this.IncludeGdc)
			{
				combinedProperties = this.CaptureContextGdc(logEvent, combinedProperties);
			}
			return combinedProperties;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00014311 File Offset: 0x00012511
		protected IDictionary<string, object> GetAllProperties(LogEventInfo logEvent)
		{
			return this.GetAllProperties(logEvent, null);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001431C File Offset: 0x0001251C
		protected IDictionary<string, object> GetAllProperties(LogEventInfo logEvent, IDictionary<string, object> combinedProperties)
		{
			if (this.IncludeEventProperties && logEvent.HasProperties)
			{
				IDictionary<string, object> dictionary;
				if ((dictionary = combinedProperties) == null)
				{
					int count = logEvent.Properties.Count;
					IList<TargetPropertyWithContext> contextProperties = this.ContextProperties;
					dictionary = TargetWithContext.CreateNewDictionary(count + ((contextProperties != null) ? contextProperties.Count : 0));
				}
				combinedProperties = dictionary;
				bool flag = combinedProperties.Count > 0;
				foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
				{
					string text = keyValuePair.Key.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						this.AddContextProperty(logEvent, text, keyValuePair.Value, flag, combinedProperties);
					}
				}
			}
			combinedProperties = this.GetContextProperties(logEvent, combinedProperties);
			return combinedProperties ?? new Dictionary<string, object>();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000143E8 File Offset: 0x000125E8
		private static IDictionary<string, object> CreateNewDictionary(int initialCapacity)
		{
			return new Dictionary<string, object>(Math.Max(initialCapacity, 3));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000143F8 File Offset: 0x000125F8
		protected virtual string GenerateUniqueItemName(LogEventInfo logEvent, string itemName, object itemValue, IDictionary<string, object> combinedProperties)
		{
			itemName = itemName ?? string.Empty;
			int num = 1;
			string text = itemName + "_1";
			while (combinedProperties.ContainsKey(text))
			{
				string text2 = itemName;
				string text3 = "_";
				int num2;
				num = (num2 = num + 1);
				text = text2 + text3 + num2.ToString();
			}
			return text;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00014448 File Offset: 0x00012648
		private bool CombineProperties(LogEventInfo logEvent, Layout contextLayout, ref IDictionary<string, object> combinedProperties)
		{
			object obj;
			if (!logEvent.TryGetCachedLayoutValue(contextLayout, out obj))
			{
				return false;
			}
			IDictionary<string, object> dictionary;
			if ((dictionary = obj as IDictionary<string, object>) != null)
			{
				if (combinedProperties != null)
				{
					bool flag = combinedProperties.Count > 0;
					using (IEnumerator<KeyValuePair<string, object>> enumerator = dictionary.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							this.AddContextProperty(logEvent, keyValuePair.Key, keyValuePair.Value, flag, combinedProperties);
						}
						return true;
					}
				}
				combinedProperties = dictionary;
			}
			return true;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000144CC File Offset: 0x000126CC
		private void AddContextProperty(LogEventInfo logEvent, string itemName, object itemValue, bool checkForDuplicates, IDictionary<string, object> combinedProperties)
		{
			if (checkForDuplicates && combinedProperties.ContainsKey(itemName))
			{
				itemName = this.GenerateUniqueItemName(logEvent, itemName, itemValue, combinedProperties);
				if (itemName == null)
				{
					return;
				}
			}
			combinedProperties[itemName] = itemValue;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000144F8 File Offset: 0x000126F8
		protected IDictionary<string, object> GetContextMdc(LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.TryGetCachedLayoutValue(this._contextLayout.MdcLayout, out obj))
			{
				return obj as IDictionary<string, object>;
			}
			return this.CaptureContextMdc(logEvent, null);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001452C File Offset: 0x0001272C
		protected IDictionary<string, object> GetContextMdlc(LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.TryGetCachedLayoutValue(this._contextLayout.MdlcLayout, out obj))
			{
				return obj as IDictionary<string, object>;
			}
			return this.CaptureContextMdlc(logEvent, null);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00014560 File Offset: 0x00012760
		protected IList<object> GetContextNdc(LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.TryGetCachedLayoutValue(this._contextLayout.NdcLayout, out obj))
			{
				return obj as IList<object>;
			}
			return this.CaptureContextNdc(logEvent);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00014590 File Offset: 0x00012790
		protected IList<object> GetContextNdlc(LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.TryGetCachedLayoutValue(this._contextLayout.NdlcLayout, out obj))
			{
				return obj as IList<object>;
			}
			return this.CaptureContextNdlc(logEvent);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000145C0 File Offset: 0x000127C0
		private IDictionary<string, object> CaptureContextProperties(LogEventInfo logEvent, IDictionary<string, object> combinedProperties)
		{
			combinedProperties = combinedProperties ?? TargetWithContext.CreateNewDictionary(this.ContextProperties.Count);
			for (int i = 0; i < this.ContextProperties.Count; i++)
			{
				TargetPropertyWithContext targetPropertyWithContext = this.ContextProperties[i];
				if (!string.IsNullOrEmpty((targetPropertyWithContext != null) ? targetPropertyWithContext.Name : null) && targetPropertyWithContext.Layout != null)
				{
					try
					{
						object obj;
						if (this.TryGetContextPropertyValue(logEvent, targetPropertyWithContext, out obj))
						{
							combinedProperties[targetPropertyWithContext.Name] = obj;
						}
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrownImmediately())
						{
							throw;
						}
						InternalLogger.Warn(ex, "{0}(Name={1}): Failed to add context property {2}", new object[]
						{
							base.GetType(),
							base.Name,
							targetPropertyWithContext.Name
						});
					}
				}
			}
			return combinedProperties;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001468C File Offset: 0x0001288C
		private bool TryGetContextPropertyValue(LogEventInfo logEvent, TargetPropertyWithContext contextProperty, out object propertyValue)
		{
			Type type = contextProperty.PropertyType ?? typeof(string);
			bool flag = type == typeof(string);
			object obj;
			if (!flag && contextProperty.Layout.TryGetRawValue(logEvent, out obj))
			{
				if (type == typeof(object))
				{
					propertyValue = obj;
					return contextProperty.IncludeEmptyValue || propertyValue != null;
				}
				if (((obj != null) ? obj.GetType() : null) == type)
				{
					propertyValue = obj;
					return true;
				}
			}
			string text = base.RenderLogEvent(contextProperty.Layout, logEvent) ?? string.Empty;
			if (!contextProperty.IncludeEmptyValue && string.IsNullOrEmpty(text))
			{
				propertyValue = null;
				return false;
			}
			if (flag)
			{
				propertyValue = text;
				return true;
			}
			if (string.IsNullOrEmpty(text) && type.IsValueType())
			{
				propertyValue = Activator.CreateInstance(type);
				return true;
			}
			propertyValue = this.PropertyTypeConverter.Convert(text, type, null, CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00014774 File Offset: 0x00012974
		protected virtual IDictionary<string, object> CaptureContextGdc(LogEventInfo logEvent, IDictionary<string, object> contextProperties)
		{
			ICollection<string> names = GlobalDiagnosticsContext.GetNames();
			if (names.Count == 0)
			{
				return contextProperties;
			}
			contextProperties = contextProperties ?? TargetWithContext.CreateNewDictionary(names.Count);
			bool flag = contextProperties.Count > 0;
			foreach (string text in names)
			{
				object @object = GlobalDiagnosticsContext.GetObject(text);
				if (this.SerializeItemValue(logEvent, text, @object, out @object))
				{
					this.AddContextProperty(logEvent, text, @object, flag, contextProperties);
				}
			}
			return contextProperties;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00014804 File Offset: 0x00012A04
		protected virtual IDictionary<string, object> CaptureContextMdc(LogEventInfo logEvent, IDictionary<string, object> contextProperties)
		{
			ICollection<string> names = MappedDiagnosticsContext.GetNames();
			if (names.Count == 0)
			{
				return contextProperties;
			}
			contextProperties = contextProperties ?? TargetWithContext.CreateNewDictionary(names.Count);
			bool flag = contextProperties.Count > 0;
			foreach (string text in names)
			{
				object @object = MappedDiagnosticsContext.GetObject(text);
				object obj;
				if (this.SerializeMdcItem(logEvent, text, @object, out obj))
				{
					this.AddContextProperty(logEvent, text, obj, flag, contextProperties);
				}
			}
			return contextProperties;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00014894 File Offset: 0x00012A94
		protected virtual bool SerializeMdcItem(LogEventInfo logEvent, string name, object value, out object serializedValue)
		{
			if (string.IsNullOrEmpty(name))
			{
				serializedValue = null;
				return false;
			}
			return this.SerializeItemValue(logEvent, name, value, out serializedValue);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000148B0 File Offset: 0x00012AB0
		protected virtual IDictionary<string, object> CaptureContextMdlc(LogEventInfo logEvent, IDictionary<string, object> contextProperties)
		{
			ICollection<string> names = MappedDiagnosticsLogicalContext.GetNames();
			if (names.Count == 0)
			{
				return contextProperties;
			}
			contextProperties = contextProperties ?? TargetWithContext.CreateNewDictionary(names.Count);
			bool flag = contextProperties.Count > 0;
			foreach (string text in names)
			{
				object @object = MappedDiagnosticsLogicalContext.GetObject(text);
				object obj;
				if (this.SerializeMdlcItem(logEvent, text, @object, out obj))
				{
					this.AddContextProperty(logEvent, text, obj, flag, contextProperties);
				}
			}
			return contextProperties;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00014940 File Offset: 0x00012B40
		protected virtual bool SerializeMdlcItem(LogEventInfo logEvent, string name, object value, out object serializedValue)
		{
			if (string.IsNullOrEmpty(name))
			{
				serializedValue = null;
				return false;
			}
			return this.SerializeItemValue(logEvent, name, value, out serializedValue);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001495C File Offset: 0x00012B5C
		protected virtual IList<object> CaptureContextNdc(LogEventInfo logEvent)
		{
			object[] allObjects = NestedDiagnosticsContext.GetAllObjects();
			if (allObjects.Length == 0)
			{
				return allObjects;
			}
			IList<object> list = null;
			for (int i = 0; i < allObjects.Length; i++)
			{
				object obj = allObjects[i];
				object obj2;
				if (this.SerializeNdcItem(logEvent, obj, out obj2))
				{
					if (list != null)
					{
						list.Add(obj2);
					}
					else
					{
						allObjects[i] = obj2;
					}
				}
				else if (list == null)
				{
					list = new List<object>(allObjects.Length);
					for (int j = 0; j < i; j++)
					{
						list.Add(allObjects[j]);
					}
				}
			}
			return list ?? allObjects;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000149D5 File Offset: 0x00012BD5
		protected virtual bool SerializeNdcItem(LogEventInfo logEvent, object value, out object serializedValue)
		{
			return this.SerializeItemValue(logEvent, null, value, out serializedValue);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000149E4 File Offset: 0x00012BE4
		protected virtual IList<object> CaptureContextNdlc(LogEventInfo logEvent)
		{
			object[] allObjects = NestedDiagnosticsLogicalContext.GetAllObjects();
			if (allObjects.Length == 0)
			{
				return allObjects;
			}
			IList<object> list = null;
			for (int i = 0; i < allObjects.Length; i++)
			{
				object obj = allObjects[i];
				object obj2;
				if (this.SerializeNdlcItem(logEvent, obj, out obj2))
				{
					if (list != null)
					{
						list.Add(obj2);
					}
					else
					{
						allObjects[i] = obj2;
					}
				}
				else if (list == null)
				{
					list = new List<object>(allObjects.Length);
					for (int j = 0; j < i; j++)
					{
						list.Add(allObjects[j]);
					}
				}
			}
			return list ?? allObjects;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00014A5D File Offset: 0x00012C5D
		protected virtual bool SerializeNdlcItem(LogEventInfo logEvent, object value, out object serializedValue)
		{
			return this.SerializeItemValue(logEvent, null, value, out serializedValue);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00014A6C File Offset: 0x00012C6C
		protected virtual bool SerializeItemValue(LogEventInfo logEvent, string name, object value, out object serializedValue)
		{
			if (value == null)
			{
				serializedValue = null;
				return true;
			}
			if (value is string || Convert.GetTypeCode(value) != TypeCode.Object || value is Guid || value is TimeSpan || value is DateTimeOffset)
			{
				serializedValue = value;
				return true;
			}
			IFormatProvider formatProvider;
			if ((formatProvider = logEvent.FormatProvider) == null)
			{
				LoggingConfiguration loggingConfiguration = base.LoggingConfiguration;
				formatProvider = ((loggingConfiguration != null) ? loggingConfiguration.DefaultCultureInfo : null);
			}
			serializedValue = Convert.ToString(value, formatProvider);
			return true;
		}

		// Token: 0x04000195 RID: 405
		private TargetWithContext.TargetWithContextLayout _contextLayout;

		// Token: 0x04000198 RID: 408
		private IPropertyTypeConverter _propertyTypeConverter;

		// Token: 0x02000232 RID: 562
		[ThreadSafe]
		[ThreadAgnostic]
		private class TargetWithContextLayout : Layout, IIncludeContext, IUsesStackTrace
		{
			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06001531 RID: 5425 RVA: 0x0003832B File Offset: 0x0003652B
			// (set) Token: 0x06001532 RID: 5426 RVA: 0x00038333 File Offset: 0x00036533
			public Layout TargetLayout
			{
				get
				{
					return this._targetLayout;
				}
				set
				{
					this._targetLayout = ((this == value) ? this._targetLayout : value);
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x06001533 RID: 5427 RVA: 0x00038348 File Offset: 0x00036548
			internal TargetWithContext.TargetWithContextLayout.LayoutContextMdc MdcLayout { get; }

			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x06001534 RID: 5428 RVA: 0x00038350 File Offset: 0x00036550
			internal TargetWithContext.TargetWithContextLayout.LayoutContextNdc NdcLayout { get; }

			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x06001535 RID: 5429 RVA: 0x00038358 File Offset: 0x00036558
			internal TargetWithContext.TargetWithContextLayout.LayoutContextMdlc MdlcLayout { get; }

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06001536 RID: 5430 RVA: 0x00038360 File Offset: 0x00036560
			internal TargetWithContext.TargetWithContextLayout.LayoutContextNdlc NdlcLayout { get; }

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06001537 RID: 5431 RVA: 0x00038368 File Offset: 0x00036568
			// (set) Token: 0x06001538 RID: 5432 RVA: 0x00038370 File Offset: 0x00036570
			public bool IncludeAllProperties { get; set; }

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06001539 RID: 5433 RVA: 0x00038379 File Offset: 0x00036579
			// (set) Token: 0x0600153A RID: 5434 RVA: 0x00038381 File Offset: 0x00036581
			public bool IncludeCallSite { get; set; }

			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x0600153B RID: 5435 RVA: 0x0003838A File Offset: 0x0003658A
			// (set) Token: 0x0600153C RID: 5436 RVA: 0x00038392 File Offset: 0x00036592
			public bool IncludeCallSiteStackTrace { get; set; }

			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x0600153D RID: 5437 RVA: 0x0003839B File Offset: 0x0003659B
			// (set) Token: 0x0600153E RID: 5438 RVA: 0x000383A8 File Offset: 0x000365A8
			public bool IncludeMdc
			{
				get
				{
					return this.MdcLayout.IsActive;
				}
				set
				{
					this.MdcLayout.IsActive = value;
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x0600153F RID: 5439 RVA: 0x000383B6 File Offset: 0x000365B6
			// (set) Token: 0x06001540 RID: 5440 RVA: 0x000383C3 File Offset: 0x000365C3
			public bool IncludeNdc
			{
				get
				{
					return this.NdcLayout.IsActive;
				}
				set
				{
					this.NdcLayout.IsActive = value;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06001541 RID: 5441 RVA: 0x000383D1 File Offset: 0x000365D1
			// (set) Token: 0x06001542 RID: 5442 RVA: 0x000383DE File Offset: 0x000365DE
			public bool IncludeMdlc
			{
				get
				{
					return this.MdlcLayout.IsActive;
				}
				set
				{
					this.MdlcLayout.IsActive = value;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x06001543 RID: 5443 RVA: 0x000383EC File Offset: 0x000365EC
			// (set) Token: 0x06001544 RID: 5444 RVA: 0x000383F9 File Offset: 0x000365F9
			public bool IncludeNdlc
			{
				get
				{
					return this.NdlcLayout.IsActive;
				}
				set
				{
					this.NdlcLayout.IsActive = value;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06001545 RID: 5445 RVA: 0x00038407 File Offset: 0x00036607
			StackTraceUsage IUsesStackTrace.StackTraceUsage
			{
				get
				{
					if (this.IncludeCallSiteStackTrace)
					{
						return StackTraceUsage.WithSource;
					}
					if (this.IncludeCallSite)
					{
						return StackTraceUsage.WithoutSource;
					}
					return StackTraceUsage.None;
				}
			}

			// Token: 0x06001546 RID: 5446 RVA: 0x0003841E File Offset: 0x0003661E
			public TargetWithContextLayout(TargetWithContext owner, Layout targetLayout)
			{
				this.TargetLayout = targetLayout;
				this.MdcLayout = new TargetWithContext.TargetWithContextLayout.LayoutContextMdc(owner);
				this.NdcLayout = new TargetWithContext.TargetWithContextLayout.LayoutContextNdc(owner);
				this.MdlcLayout = new TargetWithContext.TargetWithContextLayout.LayoutContextMdlc(owner);
				this.NdlcLayout = new TargetWithContext.TargetWithContextLayout.LayoutContextNdlc(owner);
			}

			// Token: 0x06001547 RID: 5447 RVA: 0x00038460 File Offset: 0x00036660
			protected override void InitializeLayout()
			{
				base.InitializeLayout();
				if (this.IncludeMdc || this.IncludeNdc)
				{
					base.ThreadAgnostic = false;
				}
				if (this.IncludeMdlc || this.IncludeNdlc)
				{
					base.ThreadAgnostic = false;
				}
				if (this.IncludeAllProperties)
				{
					base.MutableUnsafe = true;
				}
			}

			// Token: 0x06001548 RID: 5448 RVA: 0x000384B0 File Offset: 0x000366B0
			public override string ToString()
			{
				Layout targetLayout = this.TargetLayout;
				return ((targetLayout != null) ? targetLayout.ToString() : null) ?? base.ToString();
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x000384D0 File Offset: 0x000366D0
			public override void Precalculate(LogEventInfo logEvent)
			{
				Layout targetLayout = this.TargetLayout;
				if (targetLayout == null || targetLayout.ThreadAgnostic)
				{
					Layout targetLayout2 = this.TargetLayout;
					if (targetLayout2 == null || !targetLayout2.MutableUnsafe)
					{
						goto IL_004C;
					}
				}
				this.TargetLayout.Precalculate(logEvent);
				object obj;
				if (logEvent.TryGetCachedLayoutValue(this.TargetLayout, out obj))
				{
					logEvent.AddCachedLayoutValue(this, obj);
				}
				IL_004C:
				this.PrecalculateContext(logEvent);
			}

			// Token: 0x0600154A RID: 5450 RVA: 0x00038530 File Offset: 0x00036730
			internal override void PrecalculateBuilder(LogEventInfo logEvent, StringBuilder target)
			{
				Layout targetLayout = this.TargetLayout;
				if (targetLayout == null || targetLayout.ThreadAgnostic)
				{
					Layout targetLayout2 = this.TargetLayout;
					if (targetLayout2 == null || !targetLayout2.MutableUnsafe)
					{
						goto IL_004D;
					}
				}
				this.TargetLayout.PrecalculateBuilder(logEvent, target);
				object obj;
				if (logEvent.TryGetCachedLayoutValue(this.TargetLayout, out obj))
				{
					logEvent.AddCachedLayoutValue(this, obj);
				}
				IL_004D:
				this.PrecalculateContext(logEvent);
			}

			// Token: 0x0600154B RID: 5451 RVA: 0x00038594 File Offset: 0x00036794
			private void PrecalculateContext(LogEventInfo logEvent)
			{
				if (this.IncludeMdc)
				{
					this.MdcLayout.Precalculate(logEvent);
				}
				if (this.IncludeNdc)
				{
					this.NdcLayout.Precalculate(logEvent);
				}
				if (this.IncludeMdlc)
				{
					this.MdlcLayout.Precalculate(logEvent);
				}
				if (this.IncludeNdlc)
				{
					this.NdlcLayout.Precalculate(logEvent);
				}
			}

			// Token: 0x0600154C RID: 5452 RVA: 0x000385F1 File Offset: 0x000367F1
			protected override string GetFormattedMessage(LogEventInfo logEvent)
			{
				Layout targetLayout = this.TargetLayout;
				return ((targetLayout != null) ? targetLayout.Render(logEvent) : null) ?? string.Empty;
			}

			// Token: 0x0600154D RID: 5453 RVA: 0x0003860F File Offset: 0x0003680F
			protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
			{
				Layout targetLayout = this.TargetLayout;
				if (targetLayout == null)
				{
					return;
				}
				targetLayout.RenderAppendBuilder(logEvent, target, false);
			}

			// Token: 0x0400060D RID: 1549
			private Layout _targetLayout;

			// Token: 0x020002CB RID: 715
			[ThreadSafe]
			public class LayoutContextMdc : Layout
			{
				// Token: 0x17000448 RID: 1096
				// (get) Token: 0x0600177D RID: 6013 RVA: 0x0003D62D File Offset: 0x0003B82D
				// (set) Token: 0x0600177E RID: 6014 RVA: 0x0003D635 File Offset: 0x0003B835
				public bool IsActive { get; set; }

				// Token: 0x0600177F RID: 6015 RVA: 0x0003D63E File Offset: 0x0003B83E
				public LayoutContextMdc(TargetWithContext owner)
				{
					this._owner = owner;
				}

				// Token: 0x06001780 RID: 6016 RVA: 0x0003D64D File Offset: 0x0003B84D
				protected override string GetFormattedMessage(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
					return string.Empty;
				}

				// Token: 0x06001781 RID: 6017 RVA: 0x0003D65B File Offset: 0x0003B85B
				public override void Precalculate(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
				}

				// Token: 0x06001782 RID: 6018 RVA: 0x0003D664 File Offset: 0x0003B864
				private void CaptureContext(LogEventInfo logEvent)
				{
					if (this.IsActive)
					{
						IDictionary<string, object> dictionary = this._owner.CaptureContextMdc(logEvent, null);
						logEvent.AddCachedLayoutValue(this, dictionary);
					}
				}

				// Token: 0x040007AC RID: 1964
				private readonly TargetWithContext _owner;
			}

			// Token: 0x020002CC RID: 716
			[ThreadSafe]
			public class LayoutContextMdlc : Layout
			{
				// Token: 0x17000449 RID: 1097
				// (get) Token: 0x06001783 RID: 6019 RVA: 0x0003D68F File Offset: 0x0003B88F
				// (set) Token: 0x06001784 RID: 6020 RVA: 0x0003D697 File Offset: 0x0003B897
				public bool IsActive { get; set; }

				// Token: 0x06001785 RID: 6021 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
				public LayoutContextMdlc(TargetWithContext owner)
				{
					this._owner = owner;
				}

				// Token: 0x06001786 RID: 6022 RVA: 0x0003D6AF File Offset: 0x0003B8AF
				protected override string GetFormattedMessage(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
					return string.Empty;
				}

				// Token: 0x06001787 RID: 6023 RVA: 0x0003D6BD File Offset: 0x0003B8BD
				public override void Precalculate(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
				}

				// Token: 0x06001788 RID: 6024 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
				private void CaptureContext(LogEventInfo logEvent)
				{
					if (this.IsActive)
					{
						IDictionary<string, object> dictionary = this._owner.CaptureContextMdlc(logEvent, null);
						logEvent.AddCachedLayoutValue(this, dictionary);
					}
				}

				// Token: 0x040007AE RID: 1966
				private readonly TargetWithContext _owner;
			}

			// Token: 0x020002CD RID: 717
			[ThreadSafe]
			public class LayoutContextNdc : Layout
			{
				// Token: 0x1700044A RID: 1098
				// (get) Token: 0x06001789 RID: 6025 RVA: 0x0003D6F3 File Offset: 0x0003B8F3
				// (set) Token: 0x0600178A RID: 6026 RVA: 0x0003D6FB File Offset: 0x0003B8FB
				public bool IsActive { get; set; }

				// Token: 0x0600178B RID: 6027 RVA: 0x0003D704 File Offset: 0x0003B904
				public LayoutContextNdc(TargetWithContext owner)
				{
					this._owner = owner;
				}

				// Token: 0x0600178C RID: 6028 RVA: 0x0003D713 File Offset: 0x0003B913
				protected override string GetFormattedMessage(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
					return string.Empty;
				}

				// Token: 0x0600178D RID: 6029 RVA: 0x0003D721 File Offset: 0x0003B921
				public override void Precalculate(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
				}

				// Token: 0x0600178E RID: 6030 RVA: 0x0003D72C File Offset: 0x0003B92C
				private void CaptureContext(LogEventInfo logEvent)
				{
					if (this.IsActive)
					{
						IList<object> list = this._owner.CaptureContextNdc(logEvent);
						logEvent.AddCachedLayoutValue(this, list);
					}
				}

				// Token: 0x040007B0 RID: 1968
				private readonly TargetWithContext _owner;
			}

			// Token: 0x020002CE RID: 718
			[ThreadSafe]
			public class LayoutContextNdlc : Layout
			{
				// Token: 0x1700044B RID: 1099
				// (get) Token: 0x0600178F RID: 6031 RVA: 0x0003D756 File Offset: 0x0003B956
				// (set) Token: 0x06001790 RID: 6032 RVA: 0x0003D75E File Offset: 0x0003B95E
				public bool IsActive { get; set; }

				// Token: 0x06001791 RID: 6033 RVA: 0x0003D767 File Offset: 0x0003B967
				public LayoutContextNdlc(TargetWithContext owner)
				{
					this._owner = owner;
				}

				// Token: 0x06001792 RID: 6034 RVA: 0x0003D776 File Offset: 0x0003B976
				protected override string GetFormattedMessage(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
					return string.Empty;
				}

				// Token: 0x06001793 RID: 6035 RVA: 0x0003D784 File Offset: 0x0003B984
				public override void Precalculate(LogEventInfo logEvent)
				{
					this.CaptureContext(logEvent);
				}

				// Token: 0x06001794 RID: 6036 RVA: 0x0003D790 File Offset: 0x0003B990
				private void CaptureContext(LogEventInfo logEvent)
				{
					if (this.IsActive)
					{
						IList<object> list = this._owner.CaptureContextNdlc(logEvent);
						logEvent.AddCachedLayoutValue(this, list);
					}
				}

				// Token: 0x040007B2 RID: 1970
				private readonly TargetWithContext _owner;
			}
		}
	}
}
