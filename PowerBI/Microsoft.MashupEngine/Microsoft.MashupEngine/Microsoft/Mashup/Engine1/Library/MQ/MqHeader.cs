using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200094A RID: 2378
	public abstract class MqHeader
	{
		// Token: 0x060043E3 RID: 17379 RVA: 0x000E4CD0 File Offset: 0x000E2ED0
		static MqHeader()
		{
			Type type = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.MqHeader");
			MqHeader.HeaderTypeInfo = type.GetProperty("HeaderType");
			MqHeader.NameInfo = type.GetProperty("Name");
			MqHeader.FormatStringInfo = type.GetProperty("FormatString");
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x000020FD File Offset: 0x000002FD
		protected MqHeader()
		{
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000E4D79 File Offset: 0x000E2F79
		protected MqHeader(object header)
		{
			this.RealValue = header;
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000E4D88 File Offset: 0x000E2F88
		// (set) Token: 0x060043E7 RID: 17383 RVA: 0x000E4D90 File Offset: 0x000E2F90
		public object RealValue { get; protected set; }

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x000E4D99 File Offset: 0x000E2F99
		// (set) Token: 0x060043E9 RID: 17385 RVA: 0x000E4DB1 File Offset: 0x000E2FB1
		public MqHeaderType HeaderType
		{
			get
			{
				return (MqHeaderType)MqHeader.HeaderTypeInfo.GetValue(this.RealValue, null);
			}
			set
			{
				MqHeader.HeaderTypeInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x000E4DCC File Offset: 0x000E2FCC
		public Value HeaderTypeString
		{
			get
			{
				MqHeaderType mqHeaderType = (MqHeaderType)MqHeader.HeaderTypeInfo.GetValue(this.RealValue, null);
				string text;
				if (MqHeader.mqHeaderTypeStrings.TryGetValue(mqHeaderType, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x000E4E0B File Offset: 0x000E300B
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x000E4E23 File Offset: 0x000E3023
		public string Name
		{
			get
			{
				return (string)MqHeader.NameInfo.GetValue(this.RealValue, null);
			}
			set
			{
				MqHeader.NameInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x000E4E37 File Offset: 0x000E3037
		// (set) Token: 0x060043EE RID: 17390 RVA: 0x000E4E4F File Offset: 0x000E304F
		public string FormatString
		{
			get
			{
				return (string)MqHeader.FormatStringInfo.GetValue(this.RealValue, null);
			}
			set
			{
				MqHeader.FormatStringInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x060043EF RID: 17391
		public abstract Value GetRecordValue(Value binaryDisplay, TypeValue binaryDisplayTypeValue);

		// Token: 0x040023CB RID: 9163
		private static readonly PropertyInfo HeaderTypeInfo;

		// Token: 0x040023CC RID: 9164
		private static readonly PropertyInfo NameInfo;

		// Token: 0x040023CD RID: 9165
		private static readonly PropertyInfo FormatStringInfo;

		// Token: 0x040023CF RID: 9167
		private static readonly Dictionary<MqHeaderType, string> mqHeaderTypeStrings = new Dictionary<MqHeaderType, string>
		{
			{
				MqHeaderType.CicsBridge,
				"CicsBridge"
			},
			{
				MqHeaderType.DeadLetter,
				"DeadLetter"
			},
			{
				MqHeaderType.ImsBridge,
				"ImsBridge"
			},
			{
				MqHeaderType.RulesAndFormatting,
				"RulesAndFormatting"
			},
			{
				MqHeaderType.WorkInformation,
				"WorkInformation"
			},
			{
				MqHeaderType.EmbeddedProgrammableCommandFormat,
				"EmbeddedProgrammableCommandFormat"
			},
			{
				MqHeaderType.RulesAndFormattingVersion2,
				"RulesAndFormattingVersion2"
			}
		};
	}
}
