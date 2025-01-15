using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000026 RID: 38
	public sealed class MashupConnectionStringBuilder : DbConnectionStringBuilder
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00009360 File Offset: 0x00007560
		public MashupConnectionStringBuilder()
		{
			XmlSerializationAssembly.EnsureLoadRelative();
			this.values = new Dictionary<MashupConnectionStringBuilder.Property, object>();
			foreach (MashupConnectionStringBuilder.Property property in MashupConnectionStringBuilder.properties.Values)
			{
				this.values.Add(property, property.Default);
			}
			if (MashupConnection.EnablePreviewConnectionStringProperties)
			{
				foreach (MashupConnectionStringBuilder.Property property2 in MashupConnectionStringBuilder.previewProperties.Values)
				{
					this.values.Add(property2, property2.Default);
				}
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009430 File Offset: 0x00007630
		public MashupConnectionStringBuilder(string connectionString)
			: this()
		{
			if (connectionString != null)
			{
				base.ConnectionString = connectionString;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009442 File Offset: 0x00007642
		public override ICollection Keys
		{
			get
			{
				if (MashupConnection.EnablePreviewConnectionStringProperties)
				{
					return MashupConnectionStringBuilder.properties.Keys.Concat(MashupConnectionStringBuilder.previewProperties.Keys).ToList<string>();
				}
				return MashupConnectionStringBuilder.properties.Keys;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009474 File Offset: 0x00007674
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00009486 File Offset: 0x00007686
		public string Mashup
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.Mashup);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Mashup, value);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009494 File Offset: 0x00007694
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000094A6 File Offset: 0x000076A6
		public string Package
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.Package);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Package, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000094B4 File Offset: 0x000076B4
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000094C6 File Offset: 0x000076C6
		public string Location
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.Location);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Location, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000094D4 File Offset: 0x000076D4
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000094E6 File Offset: 0x000076E6
		public int Timeout
		{
			get
			{
				return (int)this.GetValue(MashupConnectionStringBuilder.Property.Timeout);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Timeout, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000094F9 File Offset: 0x000076F9
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000950B File Offset: 0x0000770B
		public string CachePath
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.CachePath);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.CachePath, value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00009519 File Offset: 0x00007719
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000952B File Offset: 0x0000772B
		public bool LegacyRedirects
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.LegacyRedirects);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.LegacyRedirects, value);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000953E File Offset: 0x0000773E
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009550 File Offset: 0x00007750
		public string TempPath
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.TempPath);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.TempPath, value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000955E File Offset: 0x0000775E
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009570 File Offset: 0x00007770
		public bool ReturnErrorValuesAsNull
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.ReturnErrorValuesAsNull);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.ReturnErrorValuesAsNull, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009583 File Offset: 0x00007783
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00009595 File Offset: 0x00007795
		public bool ThrowEnumerationErrors
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.ThrowEnumerationErrors);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.ThrowEnumerationErrors, value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000095A8 File Offset: 0x000077A8
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000095BA File Offset: 0x000077BA
		public string DataSourceSettings
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.DataSourceSettings);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.DataSourceSettings, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000095C8 File Offset: 0x000077C8
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000095DA File Offset: 0x000077DA
		public string PrivacyPartitionDataSources
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.PrivacyPartitionDataSources);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.PrivacyPartitionDataSources, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000095E8 File Offset: 0x000077E8
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000095FA File Offset: 0x000077FA
		public bool FastCombine
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.FastCombine);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.FastCombine, value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000960D File Offset: 0x0000780D
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000961F File Offset: 0x0000781F
		public bool PartitionValues
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.PartitionValues);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.PartitionValues, value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009632 File Offset: 0x00007832
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009644 File Offset: 0x00007844
		public bool AllowNativeQueries
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.AllowNativeQueries);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.AllowNativeQueries, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009657 File Offset: 0x00007857
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009669 File Offset: 0x00007869
		public long MaxCacheSize
		{
			get
			{
				return (long)this.GetValue(MashupConnectionStringBuilder.Property.MaxCacheSize);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.MaxCacheSize, value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000967C File Offset: 0x0000787C
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000968E File Offset: 0x0000788E
		public long MaxTempSize
		{
			get
			{
				return (long)this.GetValue(MashupConnectionStringBuilder.Property.MaxTempSize);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.MaxTempSize, value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000096A1 File Offset: 0x000078A1
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000096B3 File Offset: 0x000078B3
		public bool IgnorePreviouslyCachedData
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.IgnorePreviouslyCachedData);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.IgnorePreviouslyCachedData, value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000096C6 File Offset: 0x000078C6
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000096D8 File Offset: 0x000078D8
		public string CacheEncryptionCertificateThumbprint
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.CacheEncryptionCertificateThumbprint);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.CacheEncryptionCertificateThumbprint, value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000096E6 File Offset: 0x000078E6
		// (set) Token: 0x06000226 RID: 550 RVA: 0x000096F8 File Offset: 0x000078F8
		public Guid? ActivityId
		{
			get
			{
				return (Guid?)this.GetValue(MashupConnectionStringBuilder.Property.ActivityId);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.ActivityId, value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000970B File Offset: 0x0000790B
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009726 File Offset: 0x00007926
		public string CorrelationId
		{
			get
			{
				return ((string)this.GetValue(MashupConnectionStringBuilder.Property.CorrelationId)) ?? string.Empty;
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.CorrelationId, value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009734 File Offset: 0x00007934
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00009746 File Offset: 0x00007946
		public bool PersistSecurityInfo
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.PersistSecurityInfo);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.PersistSecurityInfo, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009759 File Offset: 0x00007959
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000976B File Offset: 0x0000796B
		public bool Debug
		{
			get
			{
				return (bool)this.GetValue(MashupConnectionStringBuilder.Property.Debug);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Debug, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000977E File Offset: 0x0000797E
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009790 File Offset: 0x00007990
		public string FeatureSwitches
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.FeatureSwitches);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.FeatureSwitches, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000979E File Offset: 0x0000799E
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000097B0 File Offset: 0x000079B0
		public string ConfigurationProperties
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.ConfigurationProperties);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.ConfigurationProperties, value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000097BE File Offset: 0x000079BE
		// (set) Token: 0x06000232 RID: 562 RVA: 0x000097D0 File Offset: 0x000079D0
		public string Optional
		{
			get
			{
				return (string)this.GetValue(MashupConnectionStringBuilder.Property.Optional);
			}
			set
			{
				this.SetValue(MashupConnectionStringBuilder.Property.Optional, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		public override object this[string keyword]
		{
			get
			{
				return this.GetValue(this.GetProperty(keyword));
			}
			set
			{
				this.SetValue(this.GetProperty(keyword), value);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009800 File Offset: 0x00007A00
		public override void Clear()
		{
			base.Clear();
			foreach (MashupConnectionStringBuilder.Property property in MashupConnectionStringBuilder.properties.Values)
			{
				this.values[property] = property.Default;
			}
			if (MashupConnection.EnablePreviewConnectionStringProperties)
			{
				foreach (MashupConnectionStringBuilder.Property property2 in MashupConnectionStringBuilder.previewProperties.Values)
				{
					this.values[property2] = property2.Default;
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000098C0 File Offset: 0x00007AC0
		public override bool ContainsKey(string keyword)
		{
			MashupConnectionStringBuilder.Property property;
			return this.TryGetProperty(keyword, out property);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000098D8 File Offset: 0x00007AD8
		public override bool TryGetValue(string keyword, out object value)
		{
			MashupConnectionStringBuilder.Property property;
			if (this.TryGetProperty(keyword, out property))
			{
				value = this.values[property];
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009904 File Offset: 0x00007B04
		public override bool Remove(string keyword)
		{
			MashupConnectionStringBuilder.Property property = this.GetProperty(keyword);
			this.values[property] = property.Default;
			return base.Remove(property.Name);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009938 File Offset: 0x00007B38
		private bool TryGetProperty(string keyword, out MashupConnectionStringBuilder.Property property)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			return MashupConnectionStringBuilder.properties.TryGetValue(keyword, out property) || MashupConnectionStringBuilder.aliases.TryGetValue(keyword, out property) || (MashupConnection.EnablePreviewConnectionStringProperties && MashupConnectionStringBuilder.previewProperties.TryGetValue(keyword, out property));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009988 File Offset: 0x00007B88
		private MashupConnectionStringBuilder.Property GetProperty(string keyword)
		{
			MashupConnectionStringBuilder.Property property;
			if (!this.TryGetProperty(keyword, out property))
			{
				throw new ArgumentException(ProviderErrorStrings.KeywordNotSupported(keyword), "keyword");
			}
			return property;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000099B2 File Offset: 0x00007BB2
		private object GetValue(MashupConnectionStringBuilder.Property property)
		{
			return this.values[property];
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000099C0 File Offset: 0x00007BC0
		private void SetValue(MashupConnectionStringBuilder.Property property, object value)
		{
			value = this.ConvertValue(property.Type, value);
			if (property == MashupConnectionStringBuilder.Property.DataSourceSettings)
			{
				value = Microsoft.Data.Mashup.DataSourceSettings.Create(Microsoft.Data.Mashup.DataSourceSettings.ToDictionary((string)value));
			}
			if (property == MashupConnectionStringBuilder.Property.FeatureSwitches)
			{
				value = ConfigurationProperty.Create(ConfigurationProperty.ToDictionary((string)value));
			}
			if (property == MashupConnectionStringBuilder.Property.ConfigurationProperties)
			{
				value = ConfigurationProperty.Create(ConfigurationProperty.ToDictionary((string)value));
			}
			this.values[property] = value;
			base[property.Name] = ((value != null) ? value.ToString() : null);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009A50 File Offset: 0x00007C50
		private object ConvertValue(Type type, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.GetType() == type)
			{
				return value;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				try
				{
					if (type == typeof(Guid))
					{
						return new Guid(convertible.ToString(CultureInfo.InvariantCulture));
					}
					return convertible.ToType(type, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
				}
			}
			throw new ArgumentException(ProviderErrorStrings.UnexpectedValueType(type), "value");
		}

		// Token: 0x04000110 RID: 272
		public const long DefaultMaxCacheSize = -1L;

		// Token: 0x04000111 RID: 273
		public const long DefaultMaxTempSize = -1L;

		// Token: 0x04000112 RID: 274
		public const int DefaultTimeout = 0;

		// Token: 0x04000113 RID: 275
		internal const string MemoryCache = "$MemoryCache$";

		// Token: 0x04000114 RID: 276
		private static readonly Dictionary<string, MashupConnectionStringBuilder.Property> properties = new Dictionary<string, MashupConnectionStringBuilder.Property>(StringComparer.OrdinalIgnoreCase)
		{
			{
				MashupConnectionStringBuilder.Property.Mashup.Name,
				MashupConnectionStringBuilder.Property.Mashup
			},
			{
				MashupConnectionStringBuilder.Property.Package.Name,
				MashupConnectionStringBuilder.Property.Package
			},
			{
				MashupConnectionStringBuilder.Property.Location.Name,
				MashupConnectionStringBuilder.Property.Location
			},
			{
				MashupConnectionStringBuilder.Property.Timeout.Name,
				MashupConnectionStringBuilder.Property.Timeout
			},
			{
				MashupConnectionStringBuilder.Property.DataSourceSettings.Name,
				MashupConnectionStringBuilder.Property.DataSourceSettings
			},
			{
				MashupConnectionStringBuilder.Property.PrivacyPartitionDataSources.Name,
				MashupConnectionStringBuilder.Property.PrivacyPartitionDataSources
			},
			{
				MashupConnectionStringBuilder.Property.FastCombine.Name,
				MashupConnectionStringBuilder.Property.FastCombine
			},
			{
				MashupConnectionStringBuilder.Property.PartitionValues.Name,
				MashupConnectionStringBuilder.Property.PartitionValues
			},
			{
				MashupConnectionStringBuilder.Property.AllowNativeQueries.Name,
				MashupConnectionStringBuilder.Property.AllowNativeQueries
			},
			{
				MashupConnectionStringBuilder.Property.CachePath.Name,
				MashupConnectionStringBuilder.Property.CachePath
			},
			{
				MashupConnectionStringBuilder.Property.MaxCacheSize.Name,
				MashupConnectionStringBuilder.Property.MaxCacheSize
			},
			{
				MashupConnectionStringBuilder.Property.TempPath.Name,
				MashupConnectionStringBuilder.Property.TempPath
			},
			{
				MashupConnectionStringBuilder.Property.MaxTempSize.Name,
				MashupConnectionStringBuilder.Property.MaxTempSize
			},
			{
				MashupConnectionStringBuilder.Property.CacheEncryptionCertificateThumbprint.Name,
				MashupConnectionStringBuilder.Property.CacheEncryptionCertificateThumbprint
			},
			{
				MashupConnectionStringBuilder.Property.ActivityId.Name,
				MashupConnectionStringBuilder.Property.ActivityId
			},
			{
				MashupConnectionStringBuilder.Property.CorrelationId.Name,
				MashupConnectionStringBuilder.Property.CorrelationId
			},
			{
				MashupConnectionStringBuilder.Property.LegacyRedirects.Name,
				MashupConnectionStringBuilder.Property.LegacyRedirects
			},
			{
				MashupConnectionStringBuilder.Property.ReturnErrorValuesAsNull.Name,
				MashupConnectionStringBuilder.Property.ReturnErrorValuesAsNull
			},
			{
				MashupConnectionStringBuilder.Property.PersistSecurityInfo.Name,
				MashupConnectionStringBuilder.Property.PersistSecurityInfo
			},
			{
				MashupConnectionStringBuilder.Property.ThrowEnumerationErrors.Name,
				MashupConnectionStringBuilder.Property.ThrowEnumerationErrors
			},
			{
				MashupConnectionStringBuilder.Property.IgnorePreviouslyCachedData.Name,
				MashupConnectionStringBuilder.Property.IgnorePreviouslyCachedData
			},
			{
				MashupConnectionStringBuilder.Property.Debug.Name,
				MashupConnectionStringBuilder.Property.Debug
			},
			{
				MashupConnectionStringBuilder.Property.FeatureSwitches.Name,
				MashupConnectionStringBuilder.Property.FeatureSwitches
			},
			{
				MashupConnectionStringBuilder.Property.ConfigurationProperties.Name,
				MashupConnectionStringBuilder.Property.ConfigurationProperties
			},
			{
				MashupConnectionStringBuilder.Property.Optional.Name,
				MashupConnectionStringBuilder.Property.Optional
			}
		};

		// Token: 0x04000115 RID: 277
		private static readonly Dictionary<string, MashupConnectionStringBuilder.Property> previewProperties = new Dictionary<string, MashupConnectionStringBuilder.Property>(StringComparer.OrdinalIgnoreCase)
		{
			{
				MashupConnectionStringBuilder.Property.Pool.Name,
				MashupConnectionStringBuilder.Property.Pool
			},
			{
				MashupConnectionStringBuilder.Property.Session.Name,
				MashupConnectionStringBuilder.Property.Session
			},
			{
				MashupConnectionStringBuilder.Property.ThrowFoldingFailures.Name,
				MashupConnectionStringBuilder.Property.ThrowFoldingFailures
			},
			{
				MashupConnectionStringBuilder.Property.ThrowOnVolatileFunctions.Name,
				MashupConnectionStringBuilder.Property.ThrowOnVolatileFunctions
			},
			{
				MashupConnectionStringBuilder.Property.DataSourcePool.Name,
				MashupConnectionStringBuilder.Property.DataSourcePool
			},
			{
				MashupConnectionStringBuilder.Property.TracingOptions.Name,
				MashupConnectionStringBuilder.Property.TracingOptions
			},
			{
				MashupConnectionStringBuilder.Property.MashupCommandBehavior.Name,
				MashupConnectionStringBuilder.Property.MashupCommandBehavior
			},
			{
				MashupConnectionStringBuilder.Property.MetadataCache.Name,
				MashupConnectionStringBuilder.Property.MetadataCache
			},
			{
				MashupConnectionStringBuilder.Property.CacheGroup.Name,
				MashupConnectionStringBuilder.Property.CacheGroup
			}
		};

		// Token: 0x04000116 RID: 278
		private static readonly Dictionary<string, MashupConnectionStringBuilder.Property> aliases = new Dictionary<string, MashupConnectionStringBuilder.Property>(StringComparer.OrdinalIgnoreCase) { 
		{
			"PersistSecurityInfo",
			MashupConnectionStringBuilder.Property.PersistSecurityInfo
		} };

		// Token: 0x04000117 RID: 279
		private Dictionary<MashupConnectionStringBuilder.Property, object> values;

		// Token: 0x02000077 RID: 119
		private class Property
		{
			// Token: 0x060004C2 RID: 1218 RVA: 0x0001170F File Offset: 0x0000F90F
			private Property(string name, Type type, object defaultValue)
			{
				this.name = name;
				this.type = type;
				this.defaultValue = defaultValue;
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001172C File Offset: 0x0000F92C
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00011734 File Offset: 0x0000F934
			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001173C File Offset: 0x0000F93C
			public object Default
			{
				get
				{
					return this.defaultValue;
				}
			}

			// Token: 0x0400025B RID: 603
			public static readonly MashupConnectionStringBuilder.Property Mashup = new MashupConnectionStringBuilder.Property("Mashup", typeof(string), null);

			// Token: 0x0400025C RID: 604
			public static readonly MashupConnectionStringBuilder.Property Package = new MashupConnectionStringBuilder.Property("Package", typeof(string), null);

			// Token: 0x0400025D RID: 605
			public static readonly MashupConnectionStringBuilder.Property Location = new MashupConnectionStringBuilder.Property("Location", typeof(string), null);

			// Token: 0x0400025E RID: 606
			public static readonly MashupConnectionStringBuilder.Property Timeout = new MashupConnectionStringBuilder.Property("Timeout", typeof(int), 0);

			// Token: 0x0400025F RID: 607
			public static readonly MashupConnectionStringBuilder.Property FastCombine = new MashupConnectionStringBuilder.Property("FastCombine", typeof(bool), false);

			// Token: 0x04000260 RID: 608
			public static readonly MashupConnectionStringBuilder.Property DataSourceSettings = new MashupConnectionStringBuilder.Property("DataSourceSettings", typeof(string), null);

			// Token: 0x04000261 RID: 609
			public static readonly MashupConnectionStringBuilder.Property PrivacyPartitionDataSources = new MashupConnectionStringBuilder.Property("PrivacyPartitionDataSources", typeof(string), null);

			// Token: 0x04000262 RID: 610
			public static readonly MashupConnectionStringBuilder.Property PartitionValues = new MashupConnectionStringBuilder.Property("PartitionValues", typeof(bool), false);

			// Token: 0x04000263 RID: 611
			public static readonly MashupConnectionStringBuilder.Property AllowNativeQueries = new MashupConnectionStringBuilder.Property("AllowNativeQueries", typeof(bool), false);

			// Token: 0x04000264 RID: 612
			public static readonly MashupConnectionStringBuilder.Property CachePath = new MashupConnectionStringBuilder.Property("CachePath", typeof(string), null);

			// Token: 0x04000265 RID: 613
			public static readonly MashupConnectionStringBuilder.Property MaxCacheSize = new MashupConnectionStringBuilder.Property("MaxCacheSize", typeof(long), -1L);

			// Token: 0x04000266 RID: 614
			public static readonly MashupConnectionStringBuilder.Property TempPath = new MashupConnectionStringBuilder.Property("TempPath", typeof(string), null);

			// Token: 0x04000267 RID: 615
			public static readonly MashupConnectionStringBuilder.Property MaxTempSize = new MashupConnectionStringBuilder.Property("MaxTempSize", typeof(long), -1L);

			// Token: 0x04000268 RID: 616
			public static readonly MashupConnectionStringBuilder.Property CacheEncryptionCertificateThumbprint = new MashupConnectionStringBuilder.Property("CacheEncryptionCertificateThumbprint", typeof(string), null);

			// Token: 0x04000269 RID: 617
			public static readonly MashupConnectionStringBuilder.Property ActivityId = new MashupConnectionStringBuilder.Property("ActivityId", typeof(Guid), null);

			// Token: 0x0400026A RID: 618
			public static readonly MashupConnectionStringBuilder.Property CorrelationId = new MashupConnectionStringBuilder.Property("CorrelationId", typeof(string), null);

			// Token: 0x0400026B RID: 619
			public static readonly MashupConnectionStringBuilder.Property LegacyRedirects = new MashupConnectionStringBuilder.Property("LegacyRedirects", typeof(bool), false);

			// Token: 0x0400026C RID: 620
			public static readonly MashupConnectionStringBuilder.Property ReturnErrorValuesAsNull = new MashupConnectionStringBuilder.Property("ReturnErrorValuesAsNull", typeof(bool), true);

			// Token: 0x0400026D RID: 621
			public static readonly MashupConnectionStringBuilder.Property PersistSecurityInfo = new MashupConnectionStringBuilder.Property("Persist Security Info", typeof(bool), true);

			// Token: 0x0400026E RID: 622
			public static readonly MashupConnectionStringBuilder.Property ThrowEnumerationErrors = new MashupConnectionStringBuilder.Property("ThrowEnumerationErrors", typeof(bool), true);

			// Token: 0x0400026F RID: 623
			public static readonly MashupConnectionStringBuilder.Property IgnorePreviouslyCachedData = new MashupConnectionStringBuilder.Property("IgnorePreviouslyCachedData", typeof(bool), false);

			// Token: 0x04000270 RID: 624
			public static readonly MashupConnectionStringBuilder.Property FeatureSwitches = new MashupConnectionStringBuilder.Property("FeatureSwitches", typeof(string), null);

			// Token: 0x04000271 RID: 625
			public static readonly MashupConnectionStringBuilder.Property ConfigurationProperties = new MashupConnectionStringBuilder.Property("ConfigurationProperties", typeof(string), null);

			// Token: 0x04000272 RID: 626
			public static readonly MashupConnectionStringBuilder.Property Optional = new MashupConnectionStringBuilder.Property("Optional", typeof(string), null);

			// Token: 0x04000273 RID: 627
			public static readonly MashupConnectionStringBuilder.Property Pool = new MashupConnectionStringBuilder.Property("Pool", typeof(string), null);

			// Token: 0x04000274 RID: 628
			public static readonly MashupConnectionStringBuilder.Property Session = new MashupConnectionStringBuilder.Property("Session", typeof(string), null);

			// Token: 0x04000275 RID: 629
			public static readonly MashupConnectionStringBuilder.Property ThrowFoldingFailures = new MashupConnectionStringBuilder.Property("ThrowFoldingFailures", typeof(bool), false);

			// Token: 0x04000276 RID: 630
			public static readonly MashupConnectionStringBuilder.Property ThrowOnVolatileFunctions = new MashupConnectionStringBuilder.Property("ThrowOnVolatileFunctions", typeof(bool), false);

			// Token: 0x04000277 RID: 631
			public static readonly MashupConnectionStringBuilder.Property DataSourcePool = new MashupConnectionStringBuilder.Property("DataSourcePool", typeof(string), null);

			// Token: 0x04000278 RID: 632
			public static readonly MashupConnectionStringBuilder.Property Debug = new MashupConnectionStringBuilder.Property("Debug", typeof(bool), false);

			// Token: 0x04000279 RID: 633
			public static readonly MashupConnectionStringBuilder.Property TracingOptions = new MashupConnectionStringBuilder.Property("TracingOptions", typeof(string), null);

			// Token: 0x0400027A RID: 634
			public static readonly MashupConnectionStringBuilder.Property MashupCommandBehavior = new MashupConnectionStringBuilder.Property("MashupCommandBehavior", typeof(string), null);

			// Token: 0x0400027B RID: 635
			public static readonly MashupConnectionStringBuilder.Property MetadataCache = new MashupConnectionStringBuilder.Property("MetadataCache", typeof(string), null);

			// Token: 0x0400027C RID: 636
			public static readonly MashupConnectionStringBuilder.Property CacheGroup = new MashupConnectionStringBuilder.Property("CacheGroup", typeof(string), null);

			// Token: 0x0400027D RID: 637
			private readonly string name;

			// Token: 0x0400027E RID: 638
			private readonly Type type;

			// Token: 0x0400027F RID: 639
			private readonly object defaultValue;
		}
	}
}
