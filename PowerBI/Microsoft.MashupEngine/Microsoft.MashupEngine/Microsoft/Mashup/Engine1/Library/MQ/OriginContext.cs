using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200094C RID: 2380
	public class OriginContext
	{
		// Token: 0x060043F1 RID: 17393 RVA: 0x000E507D File Offset: 0x000E327D
		public OriginContext(object value)
		{
			this.RealValue = value;
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000E508C File Offset: 0x000E328C
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000E5094 File Offset: 0x000E3294
		public object RealValue { get; protected set; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000E509D File Offset: 0x000E329D
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x000E50A4 File Offset: 0x000E32A4
		private static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.OriginContext");

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x000E50AC File Offset: 0x000E32AC
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x000E50C4 File Offset: 0x000E32C4
		public string ApplicationData
		{
			get
			{
				return (string)OriginContext.ApplicationDataInfo.GetValue(this.RealValue, null);
			}
			set
			{
				OriginContext.ApplicationDataInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x000E50D8 File Offset: 0x000E32D8
		// (set) Token: 0x060043F9 RID: 17401 RVA: 0x000E50F0 File Offset: 0x000E32F0
		public string PutApplicationName
		{
			get
			{
				return (string)OriginContext.PutApplicationNameInfo.GetValue(this.RealValue, null);
			}
			set
			{
				OriginContext.PutApplicationNameInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000E5104 File Offset: 0x000E3304
		// (set) Token: 0x060043FB RID: 17403 RVA: 0x000E511C File Offset: 0x000E331C
		public OriginContext.PutApplicationType ApplicationType
		{
			get
			{
				return (OriginContext.PutApplicationType)OriginContext.PutApplicationTypeInfo.GetValue(this.RealValue, null);
			}
			set
			{
				OriginContext.PutApplicationTypeInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x000E5138 File Offset: 0x000E3338
		public Value PutApplicationTypeString
		{
			get
			{
				string text;
				if (OriginContext.putApplicationTypeStrings.TryGetValue(this.ApplicationType, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x000E5165 File Offset: 0x000E3365
		private string PutDate
		{
			get
			{
				return (string)OriginContext.PutDateInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x000E517D File Offset: 0x000E337D
		private string PutTime
		{
			get
			{
				return (string)OriginContext.PutTimeInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x000E5198 File Offset: 0x000E3398
		public Value PutDateTime
		{
			get
			{
				string putDate = this.PutDate;
				string putTime = this.PutTime;
				if (!string.IsNullOrEmpty(putDate) && !string.IsNullOrEmpty(putTime))
				{
					char[] array = putTime.ToCharArray();
					if (array.Length >= 6 && array[4] == '6')
					{
						array[4] = '5';
						array[5] = '9';
					}
					DateTime dateTime;
					if (DateTime.TryParseExact(new StringBuilder(putDate).Append(new string(array)).ToString(), "yyyyMMddHHmmssff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
					{
						return DateTimeValue.New(dateTime);
					}
				}
				return Value.Null;
			}
		}

		// Token: 0x040023D8 RID: 9176
		private static readonly Dictionary<OriginContext.PutApplicationType, string> putApplicationTypeStrings = new Dictionary<OriginContext.PutApplicationType, string>
		{
			{
				OriginContext.PutApplicationType.NoContext,
				"NoContext"
			},
			{
				OriginContext.PutApplicationType.Cics,
				"Cics"
			},
			{
				OriginContext.PutApplicationType.ZOs,
				"ZOs"
			},
			{
				OriginContext.PutApplicationType.Ims,
				"Ims"
			},
			{
				OriginContext.PutApplicationType.Os2,
				"Os2"
			},
			{
				OriginContext.PutApplicationType.Dos,
				"Dos"
			},
			{
				OriginContext.PutApplicationType.Unix,
				"Unix"
			},
			{
				OriginContext.PutApplicationType.QueueManager,
				"QueueManager"
			},
			{
				OriginContext.PutApplicationType.ISeries,
				"ISeries"
			},
			{
				OriginContext.PutApplicationType.Windows,
				"Windows"
			},
			{
				OriginContext.PutApplicationType.CiscVse,
				"CiscVse"
			},
			{
				OriginContext.PutApplicationType.Nt,
				"Windows NT"
			},
			{
				OriginContext.PutApplicationType.Vms,
				"Vms"
			},
			{
				OriginContext.PutApplicationType.Guardian,
				"Guardian"
			},
			{
				OriginContext.PutApplicationType.Vos,
				"Vos"
			},
			{
				OriginContext.PutApplicationType.OpenTp1,
				"OpenTp1"
			},
			{
				OriginContext.PutApplicationType.Vm,
				"Vm"
			},
			{
				OriginContext.PutApplicationType.ImsBridge,
				"ImsBridge"
			},
			{
				OriginContext.PutApplicationType.Xcf,
				"Xcf"
			},
			{
				OriginContext.PutApplicationType.CicsBridge,
				"CicsBridge"
			},
			{
				OriginContext.PutApplicationType.NotesAgent,
				"NotesAgent"
			},
			{
				OriginContext.PutApplicationType.Tpf,
				"Tpf"
			},
			{
				OriginContext.PutApplicationType.User,
				"User"
			},
			{
				OriginContext.PutApplicationType.Broker,
				"Broker"
			},
			{
				OriginContext.PutApplicationType.Java,
				"Java"
			},
			{
				OriginContext.PutApplicationType.Dqm,
				"Dqm"
			},
			{
				OriginContext.PutApplicationType.ChannelInitiator,
				"ChannelInitiator"
			},
			{
				OriginContext.PutApplicationType.Wlm,
				"Wlm"
			},
			{
				OriginContext.PutApplicationType.Batch,
				"Batch"
			},
			{
				OriginContext.PutApplicationType.RrsBatch,
				"RrsBatch"
			},
			{
				OriginContext.PutApplicationType.Sib,
				"Sib"
			}
		};

		// Token: 0x040023D9 RID: 9177
		private static readonly PropertyInfo ApplicationDataInfo = OriginContext.RealType.GetProperty("ApplicationData");

		// Token: 0x040023DA RID: 9178
		private static readonly PropertyInfo PutApplicationNameInfo = OriginContext.RealType.GetProperty("PutApplicationName");

		// Token: 0x040023DB RID: 9179
		private static readonly PropertyInfo PutApplicationTypeInfo = OriginContext.RealType.GetProperty("PutApplicationType");

		// Token: 0x040023DC RID: 9180
		private static readonly PropertyInfo PutDateInfo = OriginContext.RealType.GetProperty("PutDate");

		// Token: 0x040023DD RID: 9181
		private static readonly PropertyInfo PutTimeInfo = OriginContext.RealType.GetProperty("PutTime");

		// Token: 0x0200094D RID: 2381
		public enum PutApplicationType
		{
			// Token: 0x040023E1 RID: 9185
			NoContext,
			// Token: 0x040023E2 RID: 9186
			Cics,
			// Token: 0x040023E3 RID: 9187
			ZOs,
			// Token: 0x040023E4 RID: 9188
			Ims,
			// Token: 0x040023E5 RID: 9189
			Os2,
			// Token: 0x040023E6 RID: 9190
			Dos,
			// Token: 0x040023E7 RID: 9191
			Unix,
			// Token: 0x040023E8 RID: 9192
			QueueManager,
			// Token: 0x040023E9 RID: 9193
			ISeries,
			// Token: 0x040023EA RID: 9194
			Windows,
			// Token: 0x040023EB RID: 9195
			CiscVse,
			// Token: 0x040023EC RID: 9196
			Nt,
			// Token: 0x040023ED RID: 9197
			Vms,
			// Token: 0x040023EE RID: 9198
			Guardian,
			// Token: 0x040023EF RID: 9199
			Vos,
			// Token: 0x040023F0 RID: 9200
			OpenTp1,
			// Token: 0x040023F1 RID: 9201
			Vm = 18,
			// Token: 0x040023F2 RID: 9202
			ImsBridge,
			// Token: 0x040023F3 RID: 9203
			Xcf,
			// Token: 0x040023F4 RID: 9204
			CicsBridge,
			// Token: 0x040023F5 RID: 9205
			NotesAgent,
			// Token: 0x040023F6 RID: 9206
			Tpf,
			// Token: 0x040023F7 RID: 9207
			User = 25,
			// Token: 0x040023F8 RID: 9208
			Broker,
			// Token: 0x040023F9 RID: 9209
			Java = 28,
			// Token: 0x040023FA RID: 9210
			Dqm,
			// Token: 0x040023FB RID: 9211
			ChannelInitiator,
			// Token: 0x040023FC RID: 9212
			Wlm,
			// Token: 0x040023FD RID: 9213
			Batch,
			// Token: 0x040023FE RID: 9214
			RrsBatch,
			// Token: 0x040023FF RID: 9215
			Sib
		}
	}
}
