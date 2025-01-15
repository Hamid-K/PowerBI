using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000ED RID: 237
	internal struct AdvancePropertiesChange
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x0001A52C File Offset: 0x0001872C
		public static AdvanceChanges MapStringToEnum(string elementName)
		{
			switch (elementName)
			{
			case "partitionStoreConnectionSettings":
				return AdvanceChanges.CASConfigChange;
			case "memoryPressureMonitor":
				return AdvanceChanges.MemoryPressureMonitorChange;
			case "regionProperties":
				return AdvanceChanges.RegionPropertiesChange;
			case "requestRetry":
				return AdvanceChanges.RequestRetryChange;
			case "routingLookupRetry":
				return AdvanceChanges.RoutingLookupChange;
			case "securityProperties":
				return AdvanceChanges.SecurityPropertiesChange;
			case "storeProperties":
				return AdvanceChanges.StorePropertiesChange;
			case "transportProperties":
				return AdvanceChanges.TransportPropertiesChange;
			case "quotaProperties":
				return AdvanceChanges.QuotaPropertiesChange;
			case "usageProperties":
				return AdvanceChanges.UsagePropertiesChange;
			case "versionProperties":
				return AdvanceChanges.VersionPropertiesChange;
			case "dnsDomain":
				return AdvanceChanges.DNSDomainChange;
			case "storeVersionProperties":
				return AdvanceChanges.StoreVersionChange;
			case "diagnosticMode":
				return AdvanceChanges.DiagnosticModeChange;
			case "diagnosticBufferSize":
				return AdvanceChanges.DiagnosticBufferChange;
			}
			throw new ArgumentException("Unknown Config Element", "elementName");
		}

		// Token: 0x17000135 RID: 309
		public bool this[string key]
		{
			get
			{
				return (this._changeFlag & AdvancePropertiesChange.MapStringToEnum(key)) != AdvanceChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= AdvancePropertiesChange.MapStringToEnum(key);
					if (key != null)
					{
						if (!(key == "memoryPressureMonitor"))
						{
							if (!(key == "versionProperties"))
							{
								return;
							}
							if (!this._allowedVersionsChange.Changed)
							{
								this._allowedVersionsChange[VersionPropertiesChanges.ChangeAll] = true;
								return;
							}
						}
						else if (!this._memoryPressureMonitorChange.Changed)
						{
							this._memoryPressureMonitorChange[MemoryPressureMonitorChanges.ChangeAll] = true;
							return;
						}
					}
				}
				else
				{
					this._changeFlag &= ~AdvancePropertiesChange.MapStringToEnum(key);
					if (key != null)
					{
						if (key == "memoryPressureMonitor")
						{
							this._memoryPressureMonitorChange[MemoryPressureMonitorChanges.ChangeAll] = false;
							return;
						}
						if (!(key == "versionProperties"))
						{
							return;
						}
						this._allowedVersionsChange[VersionPropertiesChanges.ChangeAll] = false;
					}
				}
			}
		}

		// Token: 0x17000136 RID: 310
		public bool this[AdvanceChanges key]
		{
			get
			{
				return (this._changeFlag & key) != AdvanceChanges.NoChange;
			}
			set
			{
				if (value)
				{
					this._changeFlag |= key;
					if ((key & AdvanceChanges.MemoryPressureMonitorChange) != AdvanceChanges.NoChange && !this._memoryPressureMonitorChange.Changed)
					{
						this._memoryPressureMonitorChange[MemoryPressureMonitorChanges.ChangeAll] = true;
					}
					if ((key & AdvanceChanges.VersionPropertiesChange) != AdvanceChanges.NoChange && !this._allowedVersionsChange.Changed)
					{
						this._allowedVersionsChange[VersionPropertiesChanges.ChangeAll] = true;
						return;
					}
				}
				else
				{
					this._changeFlag &= ~key;
					if ((key & AdvanceChanges.MemoryPressureMonitorChange) != AdvanceChanges.NoChange)
					{
						this._memoryPressureMonitorChange[MemoryPressureMonitorChanges.ChangeAll] = false;
					}
					if ((key & AdvanceChanges.VersionPropertiesChange) != AdvanceChanges.NoChange)
					{
						this._allowedVersionsChange[VersionPropertiesChanges.ChangeAll] = false;
					}
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001A860 File Offset: 0x00018A60
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0001A868 File Offset: 0x00018A68
		public MemoryPressureMonitorChange MemPressureChange
		{
			get
			{
				return this._memoryPressureMonitorChange;
			}
			set
			{
				this._memoryPressureMonitorChange = value;
				if (this._memoryPressureMonitorChange.Changed)
				{
					this[AdvanceChanges.MemoryPressureMonitorChange] = true;
					return;
				}
				this[AdvanceChanges.MemoryPressureMonitorChange] = false;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001A891 File Offset: 0x00018A91
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0001A899 File Offset: 0x00018A99
		public VersionPropertiesChange AllowedVersionsChange
		{
			get
			{
				return this._allowedVersionsChange;
			}
			set
			{
				this._allowedVersionsChange = value;
				if (this._allowedVersionsChange.Changed)
				{
					this[AdvanceChanges.VersionPropertiesChange] = true;
					return;
				}
				this[AdvanceChanges.VersionPropertiesChange] = false;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		public AdvancePropertiesChange(AdvancePropertiesChange original)
		{
			this._changeFlag = original._changeFlag;
			this._memoryPressureMonitorChange = original._memoryPressureMonitorChange;
			this._allowedVersionsChange = original._allowedVersionsChange;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001A8F1 File Offset: 0x00018AF1
		public bool Changed
		{
			get
			{
				return this._changeFlag != AdvanceChanges.NoChange;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001A900 File Offset: 0x00018B00
		internal bool CanUpdateConfigDynamically()
		{
			AdvancePropertiesChange advancePropertiesChange = new AdvancePropertiesChange(this);
			bool flag = this._memoryPressureMonitorChange.CanUpdateConfigDynamically();
			advancePropertiesChange[AdvanceChanges.MemoryPressureMonitorChange] = false;
			flag &= this._allowedVersionsChange.CanUpdateConfigDynamically();
			advancePropertiesChange[AdvanceChanges.VersionPropertiesChange] = false;
			return flag && !advancePropertiesChange.Changed;
		}

		// Token: 0x0400042E RID: 1070
		private AdvanceChanges _changeFlag;

		// Token: 0x0400042F RID: 1071
		private MemoryPressureMonitorChange _memoryPressureMonitorChange;

		// Token: 0x04000430 RID: 1072
		private VersionPropertiesChange _allowedVersionsChange;
	}
}
